Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml.Linq
' ReSharper disable once ClassNeverInstantiated.Global
Public NotInheritable Class Updater
    Shared ReadOnly VersionDoc As XDocument = XDocument.Load(My.MySettings.Default.RemoteManifest)
    Shared ReadOnly NewestVersion As New Version(CStr(VersionDoc.Root.Element("version")))
    Public Shared ReadOnly Property AppVersion As Version
        Get
            Return Reflection.Assembly.GetExecutingAssembly().GetName().Version
        End Get
    End Property
    Public Shared ReadOnly Property NewVersion As Version
        Get
            Return NewestVersion
        End Get
    End Property
    Public Shared ReadOnly Property Document As XDocument
        Get
            Return VersionDoc
        End Get
    End Property
    Public Shared Function CheckForUpdate() As String
        Try
            ' if newer, display update dialog
            If VersionDoc.Root IsNot Nothing Then
                If NewestVersion > AppVersion Then
                    Return "Yes"
                End If
            End If
            Return "No"
        Catch ex As Exception
            Trace.WriteLine(ex)
        End Try
        Return "Failed"
    End Function
    Public Shared Sub LaunchUpdater(manifest As XDocument)
        ' save remote manifest locally
        Dim file As String = Application.ExecutablePath.Replace(".EXE", ".exe").Replace(".exe", ".manifest")
        manifest.Save(file)

        ' launch updater
        Dim strUpdater As String = GetUpdaterPath()
        Process.Start(strUpdater, """" & file & """ """ & Application.ExecutablePath & """")
    End Sub

    ' ReSharper disable once UnusedMember.Global
    Public Shared Sub UpdateUpdater()
        ThreadPool.QueueUserWorkItem(Sub(x)
                                         ' rename updater after update
                                         Thread.Sleep(1000)
                                         Try
                                             Dim updaterPath As String = GetUpdaterPath()
                                             Dim tmpUpdaterPath As String = updaterPath.Replace(".exe", ".exe.tmp")
                                             If File.Exists(tmpUpdaterPath) Then
                                                 File.Delete(updaterPath)
                                                 File.Move(tmpUpdaterPath, updaterPath)
                                             End If
                                         Catch ex As Exception
                                             Trace.WriteLine(ex)
                                         End Try
                                     End Sub)
    End Sub
    Private Shared Function GetUpdaterPath() As String
        Dim appPath As String = Path.GetDirectoryName(Application.ExecutablePath)
        ' ReSharper disable once AssignNullToNotNullAttribute
        Return Path.Combine(appPath, "Updater.exe")
    End Function
End Class

