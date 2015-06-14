' ReSharper disable once ClassNeverInstantiated.Global
Public Class Settings

#Region "Fields"
    Private ReadOnly _appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\"
    Private ReadOnly _appName As String = My.Application.Info.AssemblyName
#End Region

    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
    End Sub
    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strCompletedPath As String = IO.Path.GetDirectoryName(My.MySettings.Default.CompletedSound)
        Dim strCanceledPath As String = IO.Path.GetDirectoryName(My.MySettings.Default.CanceledSound)

        ofdCompleted.InitialDirectory = strCompletedPath
        ofdCanceled.InitialDirectory = strCanceledPath
        txtLogPath.Text = My.MySettings.Default.LogPath
        txtInputFolder.Text = My.MySettings.Default.InputFolder
        txtOutputFolder.Text = My.MySettings.Default.OutputFolder
        txtCompleted.Text = My.MySettings.Default.CompletedSound
        txtCanceled.Text = My.MySettings.Default.CanceledSound
        cbCompleted.Checked = My.MySettings.Default.PlayCompletedSound
        cbCanceled.Checked = My.MySettings.Default.PlayCanceledSound
        rbDefault.Checked = My.MySettings.Default.OSdefault
        rbSame.Checked = My.MySettings.Default.OSsame
        rbCustom.Checked = My.MySettings.Default.OScustom
        rbBatchDefault.Checked = My.MySettings.Default.BatchOSdefault
        rbBatchCustom.Checked = My.MySettings.Default.BatchOScustom
        rbJoinDefault.Checked = My.MySettings.Default.JoinOSdefault
        rbJoinCustom.Checked = My.MySettings.Default.JoinOScustom
        cbLastInputFile.Checked = My.MySettings.Default.LoadLastInputFile
    End Sub
    Private Sub btnLogPath_Click(sender As Object, e As EventArgs) Handles btnLogPath.Click
        Dim strLogPath As String = My.MySettings.Default.LogPath
        fbLogPath.SelectedPath = strLogPath
        Dim result As DialogResult = fbLogPath.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbLogPath.SelectedPath
            ' print the folder name on a label
            txtLogPath.Text = foldername
            My.MySettings.Default.LogPath = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        fbInput.SelectedPath = My.MySettings.Default.InputFolder
        Dim result As DialogResult = fbInput.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbInput.SelectedPath
            ' print the folder name on a label
            txtInputFolder.Text = foldername
            My.MySettings.Default.InputFolder = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
    Private Sub BtnOutputClick(sender As Object, e As EventArgs) Handles btnOutput.Click
        fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
        Dim result As DialogResult = fbOutput.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbOutput.SelectedPath
            ' print the folder name on a label
            txtOutputFolder.Text = foldername
            My.MySettings.Default.OutputFolder = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
    Private Sub btnCompleted_Click(sender As Object, e As EventArgs) Handles btnCompleted.Click
        ofdCompleted.ShowDialog()
    End Sub
    Private Sub btnCanaceled_Click(sender As Object, e As EventArgs) Handles btnCanaceled.Click
        ofdCanceled.ShowDialog()
    End Sub
    Private Sub ofdCompleted_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdCompleted.FileOk
        txtCompleted.Text = ofdCompleted.FileName
        My.MySettings.Default.CompletedSound = ofdCompleted.FileName
        My.MySettings.Default.Save()
    End Sub
    Private Sub ofdCanceled_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdCanceled.FileOk
        txtCanceled.Text = ofdCanceled.FileName
        My.MySettings.Default.CanceledSound = ofdCanceled.FileName
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbCompleted_CheckedChanged(sender As Object, e As EventArgs) Handles cbCompleted.CheckedChanged
        My.MySettings.Default.PlayCompletedSound = cbCompleted.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbCanceled_CheckedChanged(sender As Object, e As EventArgs) Handles cbCanceled.CheckedChanged
        My.MySettings.Default.PlayCanceledSound = cbCanceled.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbDefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbDefault.CheckedChanged
        My.MySettings.Default.OSdefault = rbDefault.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbSame_CheckedChanged(sender As Object, e As EventArgs) Handles rbSame.CheckedChanged
        My.MySettings.Default.OSsame = rbSame.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbCustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbCustom.CheckedChanged
        My.MySettings.Default.OScustom = rbCustom.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbBatchDefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbBatchDefault.CheckedChanged
        My.MySettings.Default.BatchOSdefault = rbBatchDefault.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbBatchCustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbBatchCustom.CheckedChanged
        My.MySettings.Default.BatchOScustom = rbBatchCustom.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbJoinDefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbJoinDefault.CheckedChanged
        My.MySettings.Default.JoinOSdefault = rbJoinDefault.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbJoinCustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbJoinCustom.CheckedChanged
        My.MySettings.Default.JoinOScustom = rbJoinCustom.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbLastInputFile_CheckedChanged(sender As Object, e As EventArgs) Handles cbLastInputFile.CheckedChanged
        My.MySettings.Default.LoadLastInputFile = cbLastInputFile.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        My.MySettings.Default.CurrentInputFolder = ""
        My.MySettings.Default.CurrentOutputFolder = ""
        My.MySettings.Default.CurrentFileName = ""
        My.MySettings.Default.CurrentInputExtension = ""
        My.MySettings.Default.Save()
    End Sub
    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click
        If Not IO.Directory.Exists(_appDataPath & _appName) Then
            IO.Directory.CreateDirectory(_appDataPath & _appName)
        End If

        My.MySettings.Default.InputFolder = _appDataPath & _appName & "\Input"
        My.MySettings.Default.OutputFolder = _appDataPath & _appName & "\Output"
        My.MySettings.Default.CompletedSound = _appDataPath & "Sounds\Completed.wav"
        My.MySettings.Default.CanceledSound = _appDataPath & "Sounds\Canceled.wav"
        My.MySettings.Default.LogPath = _appDataPath & _appName
        My.MySettings.Default.OSdefault = True
        My.MySettings.Default.OSsame = False
        My.MySettings.Default.OScustom = False
        My.MySettings.Default.BatchOSdefault = True
        My.MySettings.Default.BatchOScustom = False
        My.MySettings.Default.JoinOSdefault = True
        My.MySettings.Default.JoinOScustom = False
        My.MySettings.Default.LoadLastInputFile = True
        My.MySettings.Default.WriteLogFiles = False
        My.MySettings.Default.WriteErrorLogFiles = True
        My.MySettings.Default.IgnorePictures = True
        My.MySettings.Default.Save()

        txtLogPath.Text = My.MySettings.Default.LogPath
        txtInputFolder.Text = My.MySettings.Default.InputFolder
        txtOutputFolder.Text = My.MySettings.Default.OutputFolder
        txtCompleted.Text = My.MySettings.Default.CompletedSound
        txtCanceled.Text = My.MySettings.Default.CanceledSound
        rbDefault.Checked = My.MySettings.Default.OSdefault
        rbSame.Checked = My.MySettings.Default.OSsame
        rbCustom.Checked = My.MySettings.Default.OScustom
        rbBatchDefault.Checked = My.MySettings.Default.BatchOSdefault
        rbBatchCustom.Checked = My.MySettings.Default.BatchOScustom
        rbJoinDefault.Checked = My.MySettings.Default.JoinOSdefault
        rbJoinCustom.Checked = My.MySettings.Default.JoinOScustom
        cbLastInputFile.Checked = True
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class
