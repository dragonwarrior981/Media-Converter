Imports System.ComponentModel
Imports System.IO
Imports System.Globalization
Imports System.Xml
Imports System.Security
Public Class Main

#Region "Events"
    Private WithEvents _newEncoder As New FFLib.Encoder
    Private WithEvents _newFileInfo As New FFLib.GetFileInfo
#End Region

#Region "Fields"
    Private Property FilesListBox As ListBox = New ListBox
    Private Property ReadFilesListBox As ListBox = New ListBox
    Private _processBox As ProcessDialog
    Private ReadOnly _appPath As String = My.Application.Info.DirectoryPath & "\"
    Private ReadOnly _appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\"
    Private ReadOnly _appName As String = My.Application.Info.AssemblyName
    Private _strSafeFileName As String

    Private _hasSubtitles As Boolean = False
    Private _ignorePictures As Boolean
    Private _overwrite As Boolean

    Private _audioTrackCount As Integer
    ' ReSharper disable once NotAccessedField.Local
    Private _totalTrackCount As Integer
    Private _totalFileCount As Integer
#End Region

#Region "Form Loading"
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
        Text += " " & My.Application.Info.Version.ToString
    End Sub
    Private Sub Converter_Load(sender As Object, e As EventArgs) Handles Me.Load
        UpdateApp()
        ' Set window location
        If My.MySettings.Default.SaveWindowLocation Then
            If My.MySettings.Default.WindowLocation.ToString IsNot Nothing Then
                Location = My.MySettings.Default.WindowLocation
            End If
        End If

        ' Set window size
        If My.MySettings.Default.SaveWindowSize Then
            If My.MySettings.Default.WindowSize.ToString IsNot Nothing Then
                Size = My.MySettings.Default.WindowSize
            End If
        End If

        If TabControl.SelectedTab Is TabControl.TabPages("MainTabPage") Then
            AddHandler btnStart.Click, AddressOf btnStart_Click
        ElseIf TabControl.SelectedTab Is TabControl.TabPages("BatchTabPage") Then
            AddHandler btnStart.Click, AddressOf btnBatchStart_Click
        ElseIf TabControl.SelectedTab Is TabControl.TabPages("JoinTabPage") Then
            AddHandler btnStart.Click, AddressOf btnJoinStart_Click
        End If

        LoadSettings()
    End Sub
#End Region

#Region "Load Settings"
    Private Sub LoadSettings()
        _ignorePictures = My.MySettings.Default.IgnorePictures
        _overwrite = My.MySettings.Default.Overwrite
        Dim strCurrentOutputPath As String
        Dim newarray As New ArrayList

        My.Settings.KnownFiles = newarray
        My.Settings.KnownFiles.Add(".mkv")
        My.Settings.KnownFiles.Add(".VOB")
        My.Settings.KnownFiles.Add(".wmv")
        My.Settings.KnownFiles.Add(".avi")
        My.Settings.KnownFiles.Add(".m2ts")
        My.Settings.KnownFiles.Add(".MP4")
        My.Settings.KnownFiles.Add(".ASF")
        My.Settings.KnownFiles.Add(".flac")
        My.Settings.KnownFiles.Add(".vob")
        My.Settings.KnownFiles.Add(".flv")
        My.Settings.KnownFiles.Add(".mp3")

        'Create AppData Folder on first run
        If Not Directory.Exists(_appDataPath & _appName) Then
            Directory.CreateDirectory(_appDataPath & _appName)
        End If

        'Set default input folder on first run
        If String.IsNullOrEmpty(My.MySettings.Default.InputFolder) Then
            My.MySettings.Default.InputFolder = _appDataPath & _appName & "\Input"
            My.MySettings.Default.Save()
            Directory.CreateDirectory(_appDataPath & _appName & "\Input")
        Else
            If Not Directory.Exists(My.MySettings.Default.InputFolder) Then
                Directory.CreateDirectory(My.MySettings.Default.InputFolder)
            End If
        End If

        'Set default output folder on first run
        If String.IsNullOrEmpty(My.MySettings.Default.OutputFolder) Then
            My.MySettings.Default.OutputFolder = _appDataPath & _appName & "\Output"
            My.MySettings.Default.Save()
            Directory.CreateDirectory(_appDataPath & _appName & "\Output")
        Else
            If Not Directory.Exists(My.MySettings.Default.OutputFolder) Then
                Directory.CreateDirectory(My.MySettings.Default.OutputFolder)
            End If
        End If

        'Set default log folder on first run
        If String.IsNullOrEmpty(My.MySettings.Default.LogPath) Then
            My.MySettings.Default.LogPath = _appDataPath & _appName
            My.MySettings.Default.Save()
        Else
            If Not Directory.Exists(My.MySettings.Default.LogPath) Then
                Directory.CreateDirectory(My.MySettings.Default.LogPath)
            End If
        End If
        _newEncoder.LogPath = My.MySettings.Default.LogPath

        'Set default Completed Sound folder on first run
        If String.IsNullOrEmpty(My.MySettings.Default.CompletedSound) Then
            My.MySettings.Default.CompletedSound = _appPath & "Sounds\Completed.wav"
            My.MySettings.Default.Save()
        End If

        'Set default Canceled Sound folder on first run
        If String.IsNullOrEmpty(My.MySettings.Default.CanceledSound) Then
            My.MySettings.Default.CanceledSound = _appPath & "Sounds\Canceled.wav"
            My.MySettings.Default.Save()
        End If

        If My.MySettings.Default.LoadLastInputFile Then
            'Input file info start
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                If My.Computer.FileSystem.DirectoryExists(My.MySettings.Default.CurrentInputFolder) Then
                    txtInput.Text = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
                    ofdInput.InitialDirectory = My.MySettings.Default.CurrentInputFolder
                    ofdInput.FileName = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
                    If My.Computer.FileSystem.FileExists(ofdInput.FileName) Then
                        lbStreams.Items.Clear()
                        lbBatchStreams.Items.Clear()
                        lbJoinStreams.Items.Clear()
                        _newFileInfo.SourceFile = ofdInput.FileName
                        _newFileInfo.GetMediaInfo()
                        ReadSingleFile()
                    End If
                Else
                    ofdInput.InitialDirectory = My.MySettings.Default.InputFolder & "\"
                End If
            Else
                ofdInput.InitialDirectory = My.MySettings.Default.InputFolder & "\"
            End If
            'Input file info end
            'Output file info start
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                If My.Computer.FileSystem.DirectoryExists(My.MySettings.Default.CurrentInputFolder) Then
                    strCurrentOutputPath = My.MySettings.Default.CurrentInputFolder & "\"
                Else
                    strCurrentOutputPath = My.MySettings.Default.InputFolder & "\"
                End If
            Else
                strCurrentOutputPath = My.MySettings.Default.InputFolder & "\"
            End If
            If My.MySettings.Default.OSdefault Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtOutput.Text = ""
                End If
            ElseIf My.MySettings.Default.OSsame Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtOutput.Text = strCurrentOutputPath & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtOutput.Text = ""
                End If
            Else
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtOutput.Text = ""
                End If
            End If
            If My.MySettings.Default.BatchOSdefault Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtBatchOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtBatchOutput.Text = ""
                End If
            Else
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtBatchOutput.Text = ""
                End If
            End If
            If My.MySettings.Default.JoinOSdefault Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtJoinOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtJoinOutput.Text = ""
                End If
            Else
                If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentFileName) Then
                    txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtJoinOutput.Text = ""
                End If
            End If
            'Output file info end
        End If

        rbOSdefault.Checked = My.MySettings.Default.OSdefault
        rbOSsame.Checked = My.MySettings.Default.OSsame
        rbOScustom.Checked = My.MySettings.Default.OScustom
        rbBatchOSdefault.Checked = My.MySettings.Default.BatchOSdefault
        rbBatchOScustom.Checked = My.MySettings.Default.BatchOScustom
        rbJoinOSdefault.Checked = My.MySettings.Default.JoinOSdefault
        rbJoinOScustom.Checked = My.MySettings.Default.JoinOScustom
        btnPriorityRealTime.Checked = My.MySettings.Default.PriorityRealTime
        btnPriorityHigh.Checked = My.MySettings.Default.PriorityHigh
        btnPriorityAboveNormal.Checked = My.MySettings.Default.PriorityAboveNormal
        btnPriorityNormal.Checked = My.MySettings.Default.PriorityNormal
        btnPriorityBelowNormal.Checked = My.MySettings.Default.PriorityBelowNormal
        btnPriorityIdle.Checked = My.MySettings.Default.PriorityIdle
        btnWriteLogFiles.Checked = My.MySettings.Default.WriteLogFiles
        btnWriteErrorLogFiles.Checked = My.MySettings.Default.WriteErrorLogFiles
        btnAutoUpdate.Checked = My.MySettings.Default.AutoCheckForUpdate

        ShowSettings()
    End Sub
#End Region

#Region "Show Current Settings"
    Private Sub ShowSettings()
        If My.MySettings.Default.CurrentOutputExtension = ".mkv" Then
            lblGeneral.Text = "Output Format: MKV"
        Else
            lblGeneral.Text = "Output Format: MP4"
        End If
        lblGeneral.Text += Environment.NewLine & "Language: " & My.MySettings.Default.CurrentLanguage
        If My.MySettings.Default.SplitFile Then
            lblGeneral.Text += Environment.NewLine & "Extract By Duration: Start " & My.MySettings.Default.StartTime & " End " & My.MySettings.Default.EndTime
        End If
        If My.MySettings.Default.CopySubtitles Then
            lblSubtitles.Text = "CopySubtitles: Yes"
            lblSubtitles.Text += Environment.NewLine & "Codec: " & My.MySettings.Default.SubtitleCodec
            lblSubtitles.Text += Environment.NewLine & "Language: " & My.MySettings.Default.SubtitleLanguage
        Else
            lblSubtitles.Text = "CopySubtitles: No"
            lblSubtitles.Text += Environment.NewLine & "Codec: " & My.MySettings.Default.SubtitleCodec
            lblSubtitles.Text += Environment.NewLine & "Language: " & My.MySettings.Default.SubtitleLanguage
        End If

        lblVideo1.Text = "Codec: " & My.MySettings.Default.VideoCodec
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) And Not String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
            lblVideo1.Text += Environment.NewLine & "Resolution: " & My.MySettings.Default.VideoWidth & " x" & My.MySettings.Default.VideoHeight
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoFrameRate) Then
            If My.MySettings.Default.VideoCodec = "Source" Then
            ElseIf My.MySettings.Default.VideoFrameRate = "Auto" Then
                lblVideo1.Text += Environment.NewLine & "Frame Rate: " & My.MySettings.Default.VideoFrameRate
            Else
                lblVideo1.Text += Environment.NewLine & "Frame Rate: " & My.MySettings.Default.VideoFrameRate & " FPS"
            End If
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoBitrate) Then
            If My.MySettings.Default.VideoCodec = "Source" Then
            ElseIf My.MySettings.Default.VideoBitrate = "Auto" Then
                lblVideo1.Text += Environment.NewLine & "Bitrate: " & My.MySettings.Default.VideoBitrate
            Else
                lblVideo1.Text += Environment.NewLine & "Bitrate: " & My.MySettings.Default.VideoBitrate & " Kbps"
            End If
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AspectRatio) Then
            If My.MySettings.Default.VideoCodec = "Source" Then
            Else
                lblVideo1.Text += Environment.NewLine & "Aspect Ratio: " & My.MySettings.Default.AspectRatio
            End If
        End If
        lblVideo2.Text = ""
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
            If My.MySettings.Default.VideoCodec = "Source" Then
            Else
                lblVideo2.Text += Environment.NewLine & "Profile: " & My.MySettings.Default.VideoProfile
            End If
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
            If My.MySettings.Default.VideoCodec = "Source" Then
            Else
                lblVideo2.Text += Environment.NewLine & "Level: " & My.MySettings.Default.VideoLevel
            End If
        End If
        If My.MySettings.Default.VideoCrop Then
            lblVideo2.Text += String.Format("{0}Crop: {1} {2} {3} {4}", Environment.NewLine, My.MySettings.Default.CropTop, My.MySettings.Default.CropBottom, My.MySettings.Default.CropLeft, My.MySettings.Default.CropRight)
        End If
        If My.MySettings.Default.VideoPad Then
            lblVideo2.Text += String.Format("{0}Pad: {1} {2} {3} {4}", Environment.NewLine, My.MySettings.Default.PadTop, My.MySettings.Default.PadBottom, My.MySettings.Default.PadLeft, My.MySettings.Default.PadRight)
        End If

        lblAudio.Text = "Track 1"
        lblAudio.Text += Environment.NewLine & "Codec: " & My.MySettings.Default.AudioCodec
        If Not My.MySettings.Default.AudioCodec = "Source" Then
            lblAudio.Text += Environment.NewLine & "Channels: " & My.MySettings.Default.AudioChannels
            lblAudio.Text += Environment.NewLine & "Bitrate: " & My.MySettings.Default.AudioBitrate
            lblAudio.Text += Environment.NewLine & "Sampling: " & My.MySettings.Default.AudioSamplingRate
        End If

        lblAudio2.Text = "Track 2"
        lblAudio2.Text += Environment.NewLine & "Codec: " & My.MySettings.Default.AudioCodec2
        If My.MySettings.Default.AudioCodec2 = "None" Or My.MySettings.Default.AudioCodec2 = "Source" Then
        Else
            lblAudio2.Text += Environment.NewLine & "Channels: " & My.MySettings.Default.AudioChannels2
            lblAudio2.Text += Environment.NewLine & "Bitrate: " & My.MySettings.Default.AudioBitrate2
            lblAudio2.Text += Environment.NewLine & "Sampling: " & My.MySettings.Default.AudioSamplingRate2
        End If
    End Sub
#End Region

#Region "File Menu Bar"
    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Settings.ShowDialog()
        LoadSettings()
    End Sub
    Private Sub btnPriorityRealTime_Click(sender As Object, e As EventArgs) Handles btnPriorityRealTime.Click
        My.MySettings.Default.PriorityRealTime = True
        My.MySettings.Default.PriorityHigh = False
        My.MySettings.Default.PriorityAboveNormal = False
        My.MySettings.Default.PriorityNormal = False
        My.MySettings.Default.PriorityBelowNormal = False
        My.MySettings.Default.PriorityIdle = False
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = True
        btnPriorityHigh.Checked = False
        btnPriorityAboveNormal.Checked = False
        btnPriorityNormal.Checked = False
        btnPriorityBelowNormal.Checked = False
        btnPriorityIdle.Checked = False
    End Sub
    Private Sub btnPriorityHigh_Click(sender As Object, e As EventArgs) Handles btnPriorityHigh.Click
        My.MySettings.Default.PriorityRealTime = False
        My.MySettings.Default.PriorityHigh = True
        My.MySettings.Default.PriorityAboveNormal = False
        My.MySettings.Default.PriorityNormal = False
        My.MySettings.Default.PriorityBelowNormal = False
        My.MySettings.Default.PriorityIdle = False
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = False
        btnPriorityHigh.Checked = True
        btnPriorityAboveNormal.Checked = False
        btnPriorityNormal.Checked = False
        btnPriorityBelowNormal.Checked = False
        btnPriorityIdle.Checked = False
    End Sub
    Private Sub btnPriorityAboveNormal_Click(sender As Object, e As EventArgs) Handles btnPriorityAboveNormal.Click
        My.MySettings.Default.PriorityRealTime = False
        My.MySettings.Default.PriorityHigh = False
        My.MySettings.Default.PriorityAboveNormal = True
        My.MySettings.Default.PriorityNormal = False
        My.MySettings.Default.PriorityBelowNormal = False
        My.MySettings.Default.PriorityIdle = False
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = False
        btnPriorityHigh.Checked = False
        btnPriorityAboveNormal.Checked = True
        btnPriorityNormal.Checked = False
        btnPriorityBelowNormal.Checked = False
        btnPriorityIdle.Checked = False
    End Sub
    Private Sub btnPriorityNormal_Click(sender As Object, e As EventArgs) Handles btnPriorityNormal.Click
        My.MySettings.Default.PriorityRealTime = False
        My.MySettings.Default.PriorityHigh = False
        My.MySettings.Default.PriorityAboveNormal = False
        My.MySettings.Default.PriorityNormal = True
        My.MySettings.Default.PriorityBelowNormal = False
        My.MySettings.Default.PriorityIdle = False
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = False
        btnPriorityHigh.Checked = False
        btnPriorityAboveNormal.Checked = False
        btnPriorityNormal.Checked = True
        btnPriorityBelowNormal.Checked = False
        btnPriorityIdle.Checked = False
    End Sub
    Private Sub btnPriorityBelowNormal_Click(sender As Object, e As EventArgs) Handles btnPriorityBelowNormal.Click
        My.MySettings.Default.PriorityRealTime = False
        My.MySettings.Default.PriorityHigh = False
        My.MySettings.Default.PriorityAboveNormal = False
        My.MySettings.Default.PriorityNormal = False
        My.MySettings.Default.PriorityBelowNormal = True
        My.MySettings.Default.PriorityIdle = False
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = False
        btnPriorityHigh.Checked = False
        btnPriorityAboveNormal.Checked = False
        btnPriorityNormal.Checked = False
        btnPriorityBelowNormal.Checked = True
        btnPriorityIdle.Checked = False
    End Sub
    Private Sub btnPriorityIdle_Click(sender As Object, e As EventArgs) Handles btnPriorityIdle.Click
        My.MySettings.Default.PriorityRealTime = False
        My.MySettings.Default.PriorityHigh = False
        My.MySettings.Default.PriorityAboveNormal = False
        My.MySettings.Default.PriorityNormal = False
        My.MySettings.Default.PriorityBelowNormal = False
        My.MySettings.Default.PriorityIdle = True
        My.MySettings.Default.Save()
        btnPriorityRealTime.Checked = False
        btnPriorityHigh.Checked = False
        btnPriorityAboveNormal.Checked = False
        btnPriorityNormal.Checked = False
        btnPriorityBelowNormal.Checked = False
        btnPriorityIdle.Checked = True
    End Sub
    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        AboutBox.ShowDialog()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub
    Private Sub btnExitApp_Click(sender As Object, e As EventArgs) Handles btnExitApp.Click
        Application.Exit()
    End Sub
    Private Sub btnAutoUpdate_Click(sender As Object, e As EventArgs) Handles btnAutoUpdate.Click
        My.MySettings.Default.AutoCheckForUpdate = btnAutoUpdate.Checked
        My.MySettings.Default.Save()
        btnAutoUpdate.Checked = True
    End Sub
    Private Sub btnUpdateNow_Click(sender As Object, e As EventArgs) Handles btnUpdateNow.Click
        Updater.UpdateUpdater()
        ShowUpdateDialog()
    End Sub
#End Region

#Region "Options Menu Bar"
    Private Sub btnAdvancedOptions_Click(sender As Object, e As EventArgs) Handles btnAdvancedOptions.Click
        AdvancedOptions.ShowDialog()
        LoadSettings()
    End Sub
    Private Sub btnWriteLogFiles_Click(sender As Object, e As EventArgs) Handles btnWriteLogFiles.Click
        My.MySettings.Default.WriteLogFiles = Convert.ToBoolean(btnWriteLogFiles.CheckState)
        My.MySettings.Default.Save()
    End Sub
    Private Sub btnWriteErrorLogFiles_Click(sender As Object, e As EventArgs) Handles btnWriteErrorLogFiles.Click
        My.MySettings.Default.WriteErrorLogFiles = Convert.ToBoolean(btnWriteErrorLogFiles.CheckState)
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Log Menu Bar"
    Private Sub btnViewLog_Click(sender As Object, e As EventArgs) Handles btnViewLog.Click
        Dim logFile As String = My.MySettings.Default.LogPath & "\log.txt"
        FileViewer.Text = "Log"
        If My.Computer.FileSystem.FileExists(logFile) Then
            Try
                Using tr As TextReader = New StreamReader(logFile)
                    FileViewer.lblBody.Text += tr.ReadToEnd()
                End Using
                FileViewer.ShowDialog()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("No log file found!")
        End If
    End Sub
    Private Sub btnViewErrorLog_Click(sender As Object, e As EventArgs) Handles btnViewErrorLog.Click
        Dim logFile As String = My.MySettings.Default.LogPath & "\error_log.txt"
        FileViewer.Text = "Error Log"
        If My.Computer.FileSystem.FileExists(logFile) Then
            Try
                Using tr As TextReader = New StreamReader(logFile)
                    FileViewer.lblBody.Text += tr.ReadToEnd()
                End Using
                FileViewer.ShowDialog()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("No log file found!")
        End If
    End Sub
    Private Sub btnViewFileErrorLog_Click(sender As Object, e As EventArgs) Handles btnViewFileErrorLog.Click
        Dim logFile As String = My.MySettings.Default.LogPath & "\file_error_log.txt"
        FileViewer.Text = "File Error Log"
        If My.Computer.FileSystem.FileExists(logFile) Then
            Try
                Using tr As TextReader = New StreamReader(logFile)
                    FileViewer.lblBody.Text += tr.ReadToEnd()
                End Using
                FileViewer.ShowDialog()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("No log file found!")
        End If
    End Sub
    Private Sub btnClearLogs_Click(sender As Object, e As EventArgs) Handles btnClearLogs.Click
        File.Delete(My.MySettings.Default.LogPath & "\log.txt")
        File.Delete(My.MySettings.Default.LogPath & "\error_log.txt")
        File.Delete(My.MySettings.Default.LogPath & "\file_error_log.txt")
        File.Delete(My.MySettings.Default.LogPath & "\fflib_error_log.txt")
    End Sub
#End Region

#Region "Progress Bar Status"
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub ConOut(prog As String, tl As String) Handles _newEncoder.Progress
        _processBox.ProgressValue = Convert.ToInt32(prog)
        Text = prog & "% Completed"
        Application.DoEvents()
    End Sub
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub Stat(status As String) Handles _newEncoder.Status
        _processBox.Status = status & _newEncoder.OutputFileName
        Application.DoEvents()
    End Sub
#End Region

#Region "Input File Button Click"
    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        ofdInput.ShowDialog()
    End Sub
    Private Sub ofdInput_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofdInput.FileOk
        Dim strSavePath As String = Path.GetDirectoryName(ofdInput.FileName)
        Dim strFileName As String = Path.GetFileNameWithoutExtension(ofdInput.FileName)

        txtInput.Text = ofdInput.FileName

        If My.MySettings.Default.OSdefault Then
            txtOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
        Else
            If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                txtOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If

        My.MySettings.Default.CurrentInputFolder = Path.GetDirectoryName(ofdInput.FileName)
        My.MySettings.Default.CurrentFileName = Path.GetFileNameWithoutExtension(ofdInput.FileName)
        My.MySettings.Default.CurrentInputExtension = Path.GetExtension(ofdInput.FileName)
        My.MySettings.Default.Save()
        _strSafeFileName = Path.GetFileNameWithoutExtension(ofdInput.FileName) & Path.GetExtension(ofdInput.FileName)
        ofdInput.InitialDirectory = Path.GetDirectoryName(ofdInput.FileName)
        lbStreams.Items.Clear()
        _newFileInfo.SourceFile = ofdInput.FileName
        _newFileInfo.GetMediaInfo()
        ReadSingleFile()
        _totalFileCount = 1
    End Sub
#End Region

#Region "Select Files"
    Private Sub btnSelectFiles_Click(sender As Object, e As EventArgs) Handles btnSelectFiles.Click
        Dim openDialog As New OpenFileDialog With {.Title = "Select file(s) to convert",
                                                   .Filter = "Media files (*.3g2, *.3gp, *.asf, *.asx, *.avi, *.divx, *.flv, *.m4v, *.mkv, *.mov, *.mp4, *.mpg, *.rm, *.swf, *.ts, *.vob, *.wmv)|*.3g2;*.3gp;*.asf;*.asx;*.avi;*.divx;*.flv;*.m4v;*.mkv;*.mov;*.mp4;*.mpg;*.rm;*.swf;*.ts;*.vob;*.wmv|3g2 files (*.3g2)|*.3g2|3gp files (*.3gp)|*.3gp|asf files (*.asf)|*.asf|asx files (*.asx)|*.asx|avi files (*.avi)|*.avi|divx files (*.divx)|*.divx|flv files (*.flv)|*.flv|m4v files (*.m4v)|*.m4v|mkv files (*.mkv)|*.mkv|mov files (*.mov)|*.mov|mp4 files (*.mp4)|*.mp4|mpg files (*.mpg)|*.mpg|rm files (*.rm)|*.rm|swf files (*.swf)|*.swf|ts files (*.ts)|*.ts|vob files (*.vob)|*.vob|wmv files (*.wmv)|*.wmv|All files (*.*)|*.*",
                                                   .Multiselect = True}

        If openDialog.ShowDialog() = DialogResult.OK AndAlso openDialog.FileNames.Count > 0 Then
            lbxFilesJoin.Items.Clear()
            lbJoinStreams.Items.Clear()
            lbxFiles.Items.AddRange(openDialog.FileNames)
            FilesListBox.Items.AddRange(openDialog.FileNames)
            ReadFilesListBox.Items.AddRange(openDialog.FileNames)
            lbxFiles.SelectedIndex = 0
            FilesListBox.SelectedIndex = 0
            ReadFilesListBox.SelectedIndex = 0
            AddHandler lbxFiles.SelectedIndexChanged, AddressOf lbxFiles_SelectedIndexChanged
            _totalFileCount = _totalFileCount + openDialog.FileNames.Count

            Dim strSavePath As String = Path.GetDirectoryName(Convert.ToString(lbxFiles.SelectedItem))
            Dim strFileName As String = Path.GetFileNameWithoutExtension(Convert.ToString(lbxFiles.SelectedItem))

            If My.MySettings.Default.OSdefault Then
                txtBatchOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                    txtBatchOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                End If
            End If

            My.MySettings.Default.CurrentInputFolder = Path.GetDirectoryName(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.CurrentFileName = Path.GetFileNameWithoutExtension(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.CurrentInputExtension = Path.GetExtension(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.Save()
            _strSafeFileName = Path.GetFileNameWithoutExtension(Convert.ToString(FilesListBox.SelectedItem)) & Path.GetExtension(Convert.ToString(FilesListBox.SelectedItem))

            _newFileInfo.SourceFile = Convert.ToString(FilesListBox.SelectedItem)
            _newFileInfo.GetMediaInfo()
            ReadMultipleFiles()
            PrepReadMultipleFiles()
        End If
    End Sub
    Private Sub btnSelectFilesJoin_Click(sender As Object, e As EventArgs) Handles btnSelectFilesJoin.Click
        Dim openDialog As New OpenFileDialog With {.Title = "Select file(s) to convert",
                                                   .Filter = "Media files (*.3g2, *.3gp, *.asf, *.asx, *.avi, *.divx, *.flv, *.m4v, *.mkv, *.mov, *.mp4, *.mpg, *.rm, *.swf, *.ts, *.vob, *.wmv)|*.3g2;*.3gp;*.asf;*.asx;*.avi;*.divx;*.flv;*.m4v;*.mkv;*.mov;*.mp4;*.mpg;*.rm;*.swf;*.ts;*.vob;*.wmv|3g2 files (*.3g2)|*.3g2|3gp files (*.3gp)|*.3gp|asf files (*.asf)|*.asf|asx files (*.asx)|*.asx|avi files (*.avi)|*.avi|divx files (*.divx)|*.divx|flv files (*.flv)|*.flv|m4v files (*.m4v)|*.m4v|mkv files (*.mkv)|*.mkv|mov files (*.mov)|*.mov|mp4 files (*.mp4)|*.mp4|mpg files (*.mpg)|*.mpg|rm files (*.rm)|*.rm|swf files (*.swf)|*.swf|ts files (*.ts)|*.ts|vob files (*.vob)|*.vob|wmv files (*.wmv)|*.wmv|All files (*.*)|*.*",
                                                   .Multiselect = True}

        If openDialog.ShowDialog() = DialogResult.OK AndAlso openDialog.FileNames.Count > 0 Then
            lbxFiles.Items.Clear()
            FilesListBox.Items.Clear()
            lbxFilesJoin.Items.AddRange(openDialog.FileNames)
            ReadFilesListBox.Items.AddRange(openDialog.FileNames)
            lbxFilesJoin.SelectedIndex = 0
            ReadFilesListBox.SelectedIndex = 0
            AddHandler lbxFilesJoin.SelectedIndexChanged, AddressOf lbxFilesJoin_SelectedIndexChanged
            _totalFileCount = _totalFileCount + openDialog.FileNames.Count

            File.Delete(_appDataPath & _appName & "\join_list.txt")
            Dim writer As New StreamWriter(_appDataPath & _appName & "\join_list.txt", True)
            Dim files As String
            For Each files In openDialog.FileNames
                Try
                    writer.WriteLine("file '" & files & "'")
                Catch secEx As SecurityException
                    ' The user lacks appropriate permissions to read files, discover paths, etc.
                    MessageBox.Show("Security error. Please contact your administrator for details.\n\n" & _
                        "Error message: " & secEx.Message & "\n\n" & _
                        "Details (send to Support):\n\n" & secEx.StackTrace)
                Catch ex As Exception
                    MessageBox.Show("Reported error: " & ex.Message)
                End Try
            Next files
            writer.Flush()
            writer.Close()
            writer.Dispose()

            Dim strSavePath As String = Path.GetDirectoryName(Convert.ToString(lbxFilesJoin.SelectedItem))
            Dim strFileName As String = Path.GetFileNameWithoutExtension(Convert.ToString(lbxFilesJoin.SelectedItem))

            If My.MySettings.Default.OSdefault Then
                txtJoinOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                    txtJoinOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                End If
            End If

            My.MySettings.Default.CurrentInputFolder = Path.GetDirectoryName(Convert.ToString(lbxFilesJoin.SelectedItem))
            My.MySettings.Default.CurrentFileName = Path.GetFileNameWithoutExtension(Convert.ToString(lbxFilesJoin.SelectedItem))
            My.MySettings.Default.CurrentInputExtension = Path.GetExtension(Convert.ToString(lbxFilesJoin.SelectedItem))
            My.MySettings.Default.Save()
            _strSafeFileName = Path.GetFileNameWithoutExtension(Convert.ToString(lbxFilesJoin.SelectedItem)) & Path.GetExtension(Convert.ToString(lbxFilesJoin.SelectedItem))

            _newFileInfo.SourceFile = Convert.ToString(lbxFilesJoin.SelectedItem)
            _newFileInfo.GetMediaInfo()
            ReadMultipleFiles()
            PrepReadMultipleFiles()
        End If
    End Sub
#End Region

#Region "Drag and Drop Events"
    Private Sub Converter_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim file_Names = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
        'declares list of strings called "file_names". contains all the items dropped into list box       
        Dim listofiles As New ArrayList

        If TabControl.SelectedTab Is TabControl.TabPages("MainTabPage") Then
            Dim strInputFile = ""
            For Each file_Name As String In file_Names
                If My.Computer.FileSystem.FileExists(file_Name) = True Then
                    'a file was dragged
                    strInputFile = file_Name
                ElseIf My.Computer.FileSystem.DirectoryExists(file_Name) = True Then
                    'a folder was added..
                    MessageBox.Show("You can not drag a folder in this mode!")
                End If
            Next file_Name
            txtInput.Text = strInputFile
            ofdInput.FileName = strInputFile
            _totalFileCount = 1

            Dim strSavePath As String = Path.GetDirectoryName(strInputFile)
            Dim strFileName As String = Path.GetFileNameWithoutExtension(strInputFile)
            Dim strFileInputExtension As String = Path.GetExtension(strInputFile)
            ofdInput.InitialDirectory = strSavePath
            lbStreams.Items.Clear()

            If My.MySettings.Default.OSdefault Then
                txtOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                    txtOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                End If
            End If

            My.MySettings.Default.CurrentInputFolder = strSavePath
            My.MySettings.Default.CurrentFileName = strFileName
            My.MySettings.Default.CurrentInputExtension = strFileInputExtension
            My.MySettings.Default.Save()
            _strSafeFileName = strFileName & strFileInputExtension
            _newFileInfo.SourceFile = strInputFile
            _newFileInfo.GetMediaInfo()
            ReadSingleFile()
        ElseIf TabControl.SelectedTab Is TabControl.TabPages("BatchTabPage") Then
            For Each file_Name As String In file_Names
                _totalFileCount = _totalFileCount + 1
                If My.Computer.FileSystem.FileExists(file_Name) = True Then
                    'a file was dragged
                    listofiles.Add(file_Name)
                ElseIf My.Computer.FileSystem.DirectoryExists(file_Name) = True Then
                    'a folder was added..
                    Try
                        listofiles.AddRange(My.Computer.FileSystem.GetFiles(file_Name, FileIO.SearchOption.SearchAllSubDirectories))
                    Catch ex As Exception
                    End Try
                End If
            Next file_Name
            For Each file In listofiles
                'Create a file info object
                lbxFiles.Items.Add(file)
                FilesListBox.Items.Add(file)
                ReadFilesListBox.Items.Add(file)
            Next
            FilesListBox.SelectedIndex = 0
            ReadFilesListBox.SelectedIndex = 0
            AddHandler lbxFiles.SelectedIndexChanged, AddressOf lbxFiles_SelectedIndexChanged


            Dim strSavePath As String = Path.GetDirectoryName(Convert.ToString(FilesListBox.SelectedItem))
            Dim strFileName As String = Path.GetFileNameWithoutExtension(Convert.ToString(FilesListBox.SelectedItem))
            Dim strFileInputExtension As String = Path.GetExtension(Convert.ToString(FilesListBox.SelectedItem))
            lbBatchStreams.Items.Clear()

            If My.MySettings.Default.OSdefault Then
                txtBatchOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                    txtBatchOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                End If
            End If

            My.MySettings.Default.CurrentInputFolder = strSavePath
            My.MySettings.Default.CurrentFileName = strFileName
            My.MySettings.Default.CurrentInputExtension = strFileInputExtension
            My.MySettings.Default.Save()
            _strSafeFileName = strFileName & strFileInputExtension
            PrepReadMultipleFiles()
        Else
            For Each file_Name As String In file_Names
                _totalFileCount = _totalFileCount + 1
                If My.Computer.FileSystem.FileExists(file_Name) = True Then
                    'a file was dragged
                    listofiles.Add(file_Name)
                ElseIf My.Computer.FileSystem.DirectoryExists(file_Name) = True Then
                    'a folder was added..
                    Try
                        listofiles.AddRange(My.Computer.FileSystem.GetFiles(file_Name, FileIO.SearchOption.SearchAllSubDirectories))
                    Catch ex As Exception
                    End Try
                End If
            Next file_Name
            For Each file In listofiles
                'Create a file info object
                lbxFilesJoin.Items.Add(file)
                ReadFilesListBox.Items.Add(file)
            Next
            ReadFilesListBox.SelectedIndex = 0
            AddHandler lbxFilesJoin.SelectedIndexChanged, AddressOf lbxFilesJoin_SelectedIndexChanged


            Dim strSavePath As String = Path.GetDirectoryName(Convert.ToString(lbxFilesJoin.SelectedItem))
            Dim strFileName As String = Path.GetFileNameWithoutExtension(Convert.ToString(lbxFilesJoin.SelectedItem))
            Dim strFileInputExtension As String = Path.GetExtension(Convert.ToString(lbxFilesJoin.SelectedItem))
            lbJoinStreams.Items.Clear()

            If My.MySettings.Default.OSdefault Then
                txtJoinOutput.Text = My.MySettings.Default.OutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
            Else
                If String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                    txtJoinOutput.Text = strSavePath & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                Else
                    txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & strFileName & My.MySettings.Default.CurrentOutputExtension
                End If
            End If

            My.MySettings.Default.CurrentInputFolder = strSavePath
            My.MySettings.Default.CurrentFileName = strFileName
            My.MySettings.Default.CurrentInputExtension = strFileInputExtension
            My.MySettings.Default.Save()
            _strSafeFileName = strFileName & strFileInputExtension
            PrepReadMultipleFiles()
        End If
    End Sub
    Private Sub Converter_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
#End Region

#Region "Selected File Changed"
    Private Sub lbxFiles_SelectedIndexChanged(sender As Object, e As EventArgs)
        lbBatchStreams.Items.Clear()
        _newFileInfo.SourceFile = Convert.ToString(lbxFiles.SelectedItem)
        _newFileInfo.GetMediaInfo()
        ReadSingleFile()
    End Sub
    Private Sub lbxFilesJoin_SelectedIndexChanged(sender As Object, e As EventArgs)
        lbJoinStreams.Items.Clear()
        _newFileInfo.SourceFile = Convert.ToString(lbxFilesJoin.SelectedItem)
        _newFileInfo.GetMediaInfo()
        ReadSingleFile()
    End Sub
#End Region

#Region "Remove Selected File Button Clicked"
    Private Sub btnRemoveFile_Click(sender As Object, e As EventArgs) Handles btnRemoveFile.Click
        If lbxFiles.SelectedIndex = -1 Then
            MessageBox.Show("No file selected!")
        Else
            lbBatchStreams.Items.Clear()
            lbxFiles.Items.Remove(lbxFiles.SelectedItem)
            FilesListBox.Items.Remove(lbxFiles.SelectedItem)
            RemoveHandler lbxFiles.SelectedIndexChanged, AddressOf lbxFiles_SelectedIndexChanged
            AddHandler lbxFiles.SelectedIndexChanged, AddressOf lbxFiles_SelectedIndexChanged
            If MediaTags.AudioTrackCount > 1 Then
                MediaTags.TotalDualAudioFileCount = MediaTags.TotalDualAudioFileCount - 1
            End If
            _totalFileCount = _totalFileCount - 1
        End If
    End Sub
    Private Sub btnRemoveFileJoin_Click(sender As Object, e As EventArgs) Handles btnRemoveFileJoin.Click
        If lbxFilesJoin.SelectedIndex = -1 Then
            MessageBox.Show("No file selected!")
        Else
            lbJoinStreams.Items.Clear()
            lbxFilesJoin.Items.Remove(lbxFilesJoin.SelectedItem)
            RemoveHandler lbxFilesJoin.SelectedIndexChanged, AddressOf lbxFilesJoin_SelectedIndexChanged
            AddHandler lbxFilesJoin.SelectedIndexChanged, AddressOf lbxFilesJoin_SelectedIndexChanged
            If MediaTags.AudioTrackCount > 1 Then
                MediaTags.TotalDualAudioFileCount = MediaTags.TotalDualAudioFileCount - 1
            End If
            _totalFileCount = _totalFileCount - 1
        End If
    End Sub
#End Region

#Region "Clear Files Button Clicked"
    Private Sub btnClearFiles_Click(sender As Object, e As EventArgs) Handles btnClearFiles.Click
        FilesListBox.Items.Clear()
        lbxFiles.Items.Clear()
        lbBatchStreams.Items.Clear()
        ReadFilesListBox.Items.Clear()
        _totalFileCount = 0
        MediaTags.TotalDualAudioFileCount = 0
    End Sub
    Private Sub btnClearFilesJoin_Click(sender As Object, e As EventArgs) Handles btnClearFilesJoin.Click
        lbxFilesJoin.Items.Clear()
        lbJoinStreams.Items.Clear()
        ReadFilesListBox.Items.Clear()
        _totalFileCount = 0
        MediaTags.TotalDualAudioFileCount = 0
    End Sub
#End Region

#Region "Output File Button Click"
    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click
        If My.MySettings.Default.LoadLastInputFile Then
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                If My.MySettings.Default.OScustom Then
                    fbOutput.SelectedPath = My.MySettings.Default.CurrentOutputFolder
                Else
                    fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
                End If
            Else
                fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
            End If
        Else
            fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
        End If

        Dim result As DialogResult = fbOutput.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbOutput.SelectedPath
            Dim strSavePath As String
            ' print the folder name on a label
            strSavePath = foldername & "\"
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = strSavePath & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
            My.MySettings.Default.CurrentOutputFolder = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
    Private Sub btnBatchOutput_Click(sender As Object, e As EventArgs) Handles btnBatchOutput.Click
        If My.MySettings.Default.LoadLastInputFile Then
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                If My.MySettings.Default.OScustom Then
                    fbOutput.SelectedPath = My.MySettings.Default.CurrentOutputFolder
                Else
                    fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
                End If
            Else
                fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
            End If
        Else
            fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
        End If

        Dim result As DialogResult = fbOutput.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbOutput.SelectedPath
            Dim strSavePath As String
            ' print the folder name on a label
            strSavePath = foldername & "\"
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtBatchOutput.Text = strSavePath & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
            My.MySettings.Default.CurrentOutputFolder = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
    Private Sub btnJoinOutput_Click(sender As Object, e As EventArgs) Handles btnJoinOutput.Click
        If My.MySettings.Default.LoadLastInputFile Then
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentOutputFolder) Then
                If My.MySettings.Default.OScustom Then
                    fbOutput.SelectedPath = My.MySettings.Default.CurrentOutputFolder
                Else
                    fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
                End If
            Else
                fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
            End If
        Else
            fbOutput.SelectedPath = My.MySettings.Default.OutputFolder
        End If

        Dim result As DialogResult = fbOutput.ShowDialog()
        If result = DialogResult.OK Then
            ' retrieve the name of the selected folder
            Dim foldername As String = fbOutput.SelectedPath
            Dim strSavePath As String
            ' print the folder name on a label
            strSavePath = foldername & "\"
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtJoinOutput.Text = strSavePath & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
            My.MySettings.Default.CurrentOutputFolder = foldername
            My.MySettings.Default.Save()
        End If
    End Sub
#End Region

#Region "Output Folder Setting Changed"
    Private Sub rbOSdefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbOSdefault.CheckedChanged
        If rbOSdefault.Checked Then
            txtOutput.Enabled = False
            btnOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtOutput.Enabled = True
            btnOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.OutputFolder
        My.MySettings.Default.OSdefault = True
        My.MySettings.Default.OSsame = False
        My.MySettings.Default.OScustom = False
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbOSsame_CheckedChanged(sender As Object, e As EventArgs) Handles rbOSsame.CheckedChanged
        If rbOSsame.Checked Then
            txtOutput.Enabled = False
            btnOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtOutput.Enabled = True
            btnOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.CurrentInputFolder
        My.MySettings.Default.OSdefault = False
        My.MySettings.Default.OSsame = True
        My.MySettings.Default.OScustom = False
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbOScustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbOScustom.CheckedChanged
        If rbOScustom.Checked Then
            txtOutput.Enabled = True
            btnOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtOutput.Enabled = False
            btnOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.CurrentOutputFolder
        My.MySettings.Default.OSdefault = False
        My.MySettings.Default.OSsame = False
        My.MySettings.Default.OScustom = True
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbBatchOSdefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbBatchOSdefault.CheckedChanged
        If rbBatchOSdefault.Checked Then
            txtBatchOutput.Enabled = False
            btnBatchOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtBatchOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtBatchOutput.Enabled = True
            btnBatchOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.OutputFolder
        My.MySettings.Default.BatchOSdefault = True
        My.MySettings.Default.BatchOScustom = False
        My.MySettings.Default.Save()
    End Sub

    Private Sub rbBatchOScustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbBatchOScustom.CheckedChanged
        If rbBatchOScustom.Checked Then
            txtBatchOutput.Enabled = True
            btnBatchOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtBatchOutput.Enabled = False
            btnBatchOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtBatchOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.CurrentOutputFolder
        My.MySettings.Default.BatchOSdefault = False
        My.MySettings.Default.BatchOScustom = True
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbJoinOSdefault_CheckedChanged(sender As Object, e As EventArgs) Handles rbJoinOSdefault.CheckedChanged
        If rbJoinOSdefault.Checked Then
            txtJoinOutput.Enabled = False
            btnJoinOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtJoinOutput.Text = My.MySettings.Default.OutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtJoinOutput.Enabled = True
            btnJoinOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.OutputFolder
        My.MySettings.Default.JoinOSdefault = True
        My.MySettings.Default.JoinOScustom = False
        My.MySettings.Default.Save()
    End Sub
    Private Sub rbJoinOScustom_CheckedChanged(sender As Object, e As EventArgs) Handles rbJoinOScustom.CheckedChanged
        If rbJoinOScustom.Checked Then
            txtJoinOutput.Enabled = True
            btnJoinOutput.Enabled = True
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        Else
            txtJoinOutput.Enabled = False
            btnJoinOutput.Enabled = False
            If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentInputFolder) Then
                txtJoinOutput.Text = My.MySettings.Default.CurrentOutputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            End If
        End If
        My.MySettings.Default.CurrentOutputFolder = My.MySettings.Default.CurrentOutputFolder
        My.MySettings.Default.JoinOSdefault = False
        My.MySettings.Default.JoinOScustom = True
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Tab Changed"
    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl.SelectedIndexChanged
        If TabControl.SelectedTab Is TabControl.TabPages("MainTabPage") Then
            RemoveHandler btnStart.Click, AddressOf btnBatchStart_Click
            RemoveHandler btnStart.Click, AddressOf btnJoinStart_Click
            AddHandler btnStart.Click, AddressOf btnStart_Click
        ElseIf TabControl.SelectedTab Is TabControl.TabPages("BatchTabPage") Then
            RemoveHandler btnStart.Click, AddressOf btnStart_Click
            RemoveHandler btnStart.Click, AddressOf btnJoinStart_Click
            AddHandler btnStart.Click, AddressOf btnBatchStart_Click
        ElseIf TabControl.SelectedTab Is TabControl.TabPages("JoinTabPage") Then
            RemoveHandler btnStart.Click, AddressOf btnStart_Click
            RemoveHandler btnStart.Click, AddressOf btnBatchStart_Click
            AddHandler btnStart.Click, AddressOf btnJoinStart_Click
        End If
        lbxFiles.Items.Clear()
        FilesListBox.Items.Clear()
        lbxFilesJoin.Items.Clear()
        lbStreams.Items.Clear()
        lbBatchStreams.Items.Clear()
        lbJoinStreams.Items.Clear()
        _totalFileCount = 0
        MediaTags.TotalDualAudioFileCount = 0
    End Sub
#End Region

#Region "Play Completed Sound"
    Private Sub PlayCompletedSound()
        My.Computer.Audio.Play(My.MySettings.Default.CompletedSound)
    End Sub
#End Region

#Region "Play Canceled Sound"
    ' ReSharper disable once UnusedMember.Global
    ' ReSharper disable once UnusedMember.Local
    Private Sub PlayCanceledSound()
        My.Computer.Audio.Play(My.MySettings.Default.CanceledSound)
    End Sub
#End Region

#Region "Capitalize Letters"
    Private Shared Function FirstCharToUpper(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return input
        End If
        Return input.First().ToString().ToUpper() & input.Substring(1)
    End Function
    ' ReSharper disable once UnusedMember.Local
    Private Function AllCharToUpper(str As String) As String
        Return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToUpper())
    End Function
#End Region

#Region "Start Button Clicked"
    Private Sub btnStart_Click(sender As Object, e As EventArgs)
        Dim strInputFile As String
        Dim strOutputPath As String
        Dim strOutputFile As String
        Dim strNewOutputFile As String
        Dim strTime As String
        strTime = "_" & Date.Now.Hour.ToString()
        strTime += "_" & Date.Now.Minute.ToString()
        strTime += "_" & Date.Now.Second.ToString()
        strTime += "_" & Date.Now.Millisecond.ToString()

        strInputFile = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
        strOutputPath = My.MySettings.Default.CurrentOutputFolder & "\"
        strOutputFile = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        strNewOutputFile = My.MySettings.Default.CurrentFileName & strTime & My.MySettings.Default.CurrentOutputExtension
        _strSafeFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension

        If My.MySettings.Default.WriteLogFiles Then
            _newEncoder.EnableLogging = True
        Else
            _newEncoder.EnableLogging = False
        End If

        If My.MySettings.Default.CurrentLanguage = "English" Then
            _newEncoder.Language = _newEncoder.Language_English
        ElseIf My.MySettings.Default.CurrentLanguage = "Russian" Then
            _newEncoder.Language = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.CurrentLanguage = "German" Then
            _newEncoder.Language = _newEncoder.Language_German
        ElseIf My.MySettings.Default.CurrentLanguage = "Chinese" Then
            _newEncoder.Language = _newEncoder.Language_Chinese
        Else
            _newEncoder.Language = _newEncoder.Language_Czech
        End If

        If My.MySettings.Default.SubtitleLanguage = "Source" Then
            _newEncoder.SubtitleLanguage = ""
        ElseIf My.MySettings.Default.SubtitleLanguage = "English" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_English
        ElseIf My.MySettings.Default.SubtitleLanguage = "Russian" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.SubtitleLanguage = "German" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_German
        ElseIf My.MySettings.Default.SubtitleLanguage = "Chinese" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Chinese
        Else
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Czech
        End If

        _newEncoder.PriorityRealTime = My.MySettings.Default.PriorityRealTime
        _newEncoder.PriorityHigh = My.MySettings.Default.PriorityHigh
        _newEncoder.PriorityAboveNormal = My.MySettings.Default.PriorityAboveNormal
        _newEncoder.PriorityNormal = My.MySettings.Default.PriorityNormal
        _newEncoder.PriorityBelowNormal = My.MySettings.Default.PriorityBelowNormal
        _newEncoder.PriorityIdle = My.MySettings.Default.PriorityIdle

        If My.Computer.FileSystem.FileExists(strInputFile) Then
            If My.MySettings.Default.VideoCodec = "Auto" Then
                If MediaTags.VideoCodec = "h264" Then
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                    _newEncoder.VideoFrameRate = ""
                    _newEncoder.VideoBitrate = ""
                    _newEncoder.AspectRatio = ""
                    _newEncoder.VideoWidth = ""
                    _newEncoder.VideoHeight = ""
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                    If My.MySettings.Default.VideoFrameRate = "Auto" Then
                        _newEncoder.VideoFrameRate = ""
                    Else
                        _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                    End If
                    If My.MySettings.Default.VideoBitrate = "Auto" Then
                        _newEncoder.VideoBitrate = ""
                    Else
                        _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                    End If
                    If My.MySettings.Default.AspectRatio = "Auto" Then
                        _newEncoder.AspectRatio = ""
                    Else
                        _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                        _newEncoder.VideoWidth = ""
                    Else
                        _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                        _newEncoder.VideoHeight = ""
                    Else
                        _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "High" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    Else
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        _newEncoder.VideoLevel = ""
                    End If
                End If
            ElseIf My.MySettings.Default.VideoCodec = "Source" Then
                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                _newEncoder.VideoFrameRate = ""
                _newEncoder.VideoBitrate = ""
                _newEncoder.AspectRatio = ""
                _newEncoder.VideoWidth = ""
                _newEncoder.VideoHeight = ""
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                _newEncoder.VideoLevel = ""
            Else
                _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                If My.MySettings.Default.VideoFrameRate = "Auto" Then
                    _newEncoder.VideoFrameRate = ""
                Else
                    _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                End If
                If My.MySettings.Default.VideoBitrate = "Auto" Then
                    _newEncoder.VideoBitrate = ""
                Else
                    _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                End If
                If My.MySettings.Default.AspectRatio = "Auto" Then
                    _newEncoder.AspectRatio = ""
                Else
                    _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                    _newEncoder.VideoWidth = ""
                Else
                    _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                    _newEncoder.VideoHeight = ""
                Else
                    _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "High" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                Else
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                End If
            End If

            If My.MySettings.Default.AudioCodec = "Auto" Then
                If MediaTags.AudioChannels = "mono" Then
                    If My.MySettings.Default.AudioChannels = "Auto" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                    ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                    End If

                ElseIf MediaTags.AudioChannels = "stereo" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                ElseIf MediaTags.AudioChannels = "5.1(side)" Or MediaTags.AudioChannels = "5.1" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                End If
            ElseIf My.MySettings.Default.AudioCodec = "Source" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec = "AAC" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec = "AC3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec = "MP3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Mp3
            End If
            If My.MySettings.Default.AudioCodec2 = "None" Then
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec2 = "AAC" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec2 = "AC3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec2 = "MP3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Mp3
            End If

            If My.MySettings.Default.AudioChannels = "Auto" Then
                _newEncoder.AudioChannels = ""
            ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                _newEncoder.AudioChannels = "1"
            ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                _newEncoder.AudioChannels = "2"
            ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                _newEncoder.AudioChannels = "6"
            End If
            If My.MySettings.Default.AudioChannels2 = "Auto" Then
                _newEncoder.AudioChannels2 = ""
            ElseIf My.MySettings.Default.AudioChannels2 = "Mono" Then
                _newEncoder.AudioChannels2 = "1"
            ElseIf My.MySettings.Default.AudioChannels2 = "Stereo" Then
                _newEncoder.AudioChannels2 = "2"
            ElseIf My.MySettings.Default.AudioChannels2 = "5.1" Then
                _newEncoder.AudioChannels2 = "6"
            End If

            If My.MySettings.Default.AudioBitrate = "Auto" Then
                _newEncoder.AudioBitrate = ""
            Else
                _newEncoder.AudioBitrate = My.MySettings.Default.AudioBitrate
            End If
            If My.MySettings.Default.AudioBitrate2 = "Auto" Then
                _newEncoder.AudioBitrate2 = ""
            Else
                _newEncoder.AudioBitrate2 = My.MySettings.Default.AudioBitrate2
            End If

            If My.MySettings.Default.AudioSamplingRate = "Auto" Then
                _newEncoder.AudioSamplingRate = ""
            Else
                _newEncoder.AudioSamplingRate = My.MySettings.Default.AudioSamplingRate
            End If
            If My.MySettings.Default.AudioSamplingRate2 = "Auto" Then
                _newEncoder.AudioSamplingRate2 = ""
            Else
                _newEncoder.AudioSamplingRate2 = My.MySettings.Default.AudioSamplingRate2
            End If

            If _hasSubtitles Then
                If My.MySettings.Default.CopySubtitles Then
                    If My.MySettings.Default.SubtitleCodec = "Source" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                    ElseIf My.MySettings.Default.SubtitleCodec = "ass" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ass
                    ElseIf My.MySettings.Default.SubtitleCodec = "srt" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Srt
                    ElseIf My.MySettings.Default.SubtitleCodec = "ssa" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ssa
                    ElseIf My.MySettings.Default.SubtitleCodec = "mov" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_MovText
                    End If
                    _newEncoder.CopySubtitles = True
                Else
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                    _newEncoder.CopySubtitles = False
                End If
            Else
                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                _newEncoder.CopySubtitles = False
            End If

            If My.MySettings.Default.AudioCodec2 = "None" Then
                _newEncoder.DualAudio = False
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2 -map 0:3"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                End If
            Else
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1 -map 0:2"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                End If
            End If
            If My.MySettings.Default.SplitFile Then
                _newEncoder.SplitFile = True
                _newEncoder.StartTime = My.MySettings.Default.StartTime
                _newEncoder.EndTime = My.MySettings.Default.EndTime
            Else
                _newEncoder.SplitFile = False
            End If
            _newEncoder.SourceFile = strInputFile
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
            _newEncoder.Format = _newEncoder.Format_Mkv
            _newEncoder.Threads = Convert.ToString(0)

            '------START------ Run if overwrite destination file by default is on ------START------'
            If _overwrite Then
                If Not My.MySettings.Default.AudioCodec2 = "None" Then
                    If Not MediaTags.VideoTrackID() = 0 Then
                        'Store the current settings
                        EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                     _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                     _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                     _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                        _newEncoder.DualAudio = False
                        _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                        _newEncoder.VideoWidth = ""
                        _newEncoder.VideoHeight = ""
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        _newEncoder.AudioChannels = ""
                        _newEncoder.AudioBitrate = ""
                        _newEncoder.AudioSamplingRate = ""
                        If _newEncoder.CopySubtitles Then
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                        Else
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                        End If
                        _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        TempBackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Creating Temp File"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                        _processBox.lblError.Text += "fixing and creating temp file to work with"
                        _processBox.ShowDialog()
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        BackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Converting Video"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.ShowDialog()
                    End If
                Else
                    _newEncoder.OutputPath = strOutputPath
                    _newEncoder.OutputFileName = strOutputFile
                    My.MySettings.Default.CurrentNewFileName = ""
                    My.MySettings.Default.Save()
                    Text = "Analayzing " & _strSafeFileName
                    _processBox = New ProcessDialog()
                    BackgroundWorker.RunWorkerAsync()
                    _processBox.Text = "Converting Video"
                    _processBox.lblCompletedFiles.Text = ""
                    _processBox.ShowDialog()
                End If
                '------END------ Run if overwrite destination file by default is on ------END------'

                '------START------ Run if overwrite destination file by default is off ------START------'
            Else
                If My.Computer.FileSystem.FileExists(strOutputPath & strOutputFile) Then
                    Dim result As DialogResult = FileExists.ShowDialog
                    If result = Windows.Forms.DialogResult.Yes Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                TempBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                                _processBox.ShowDialog()
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                BackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Converting Video"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.ShowDialog()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    ElseIf result = Windows.Forms.DialogResult.No Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                TempBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                                _processBox.ShowDialog()
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                BackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Converting Video"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.ShowDialog()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strNewOutputFile
                            My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    Else
                        MessageBox.Show("User canceled")
                    End If

                Else
                    If Not My.MySettings.Default.AudioCodec2 = "None" Then
                        If Not MediaTags.VideoTrackID() = 0 Then
                            'Store the current settings
                            EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                         _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                         _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                         _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                            _newEncoder.DualAudio = False
                            _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                            _newEncoder.VideoWidth = ""
                            _newEncoder.VideoHeight = ""
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                            _newEncoder.AudioChannels = ""
                            _newEncoder.AudioBitrate = ""
                            _newEncoder.AudioSamplingRate = ""
                            If _newEncoder.CopySubtitles Then
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                            Else
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                            End If
                            _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            TempBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Creating Temp File"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                            _processBox.lblError.Text += "fixing and creating temp file to work with"
                            _processBox.ShowDialog()
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        BackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Converting Video"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.ShowDialog()
                    End If
                End If
            End If
            '------END------ Run if overwrite destination file by default is off ------END------'
        Else
            Dim writeErrorLogFiles As Boolean = My.MySettings.Default.WriteErrorLogFiles
            If writeErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\file_error_log.txt", True)
                    errorWriter.Write("File " & strInputFile & " not found.")
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
            MessageBox.Show("File " & strInputFile & " not found, was it deleted?")
        End If
    End Sub
    Private Sub btnBatchStart_Click(sender As Object, e As EventArgs)
        Dim strInputFile As String
        Dim strOutputPath As String
        Dim strOutputFile As String
        Dim strNewOutputFile As String
        Dim strTime As String
        strTime = "_" & Date.Now.Hour.ToString()
        strTime += "_" & Date.Now.Minute.ToString()
        strTime += "_" & Date.Now.Second.ToString()
        strTime += "_" & Date.Now.Millisecond.ToString()

        strInputFile = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
        strOutputPath = My.MySettings.Default.CurrentOutputFolder & "\"
        strOutputFile = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        strNewOutputFile = My.MySettings.Default.CurrentFileName & strTime & My.MySettings.Default.CurrentOutputExtension
        _strSafeFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension

        If My.MySettings.Default.WriteLogFiles Then
            _newEncoder.EnableLogging = True
        Else
            _newEncoder.EnableLogging = False
        End If

        If My.MySettings.Default.CurrentLanguage = "English" Then
            _newEncoder.Language = _newEncoder.Language_English
        ElseIf My.MySettings.Default.CurrentLanguage = "Russian" Then
            _newEncoder.Language = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.CurrentLanguage = "German" Then
            _newEncoder.Language = _newEncoder.Language_German
        ElseIf My.MySettings.Default.CurrentLanguage = "Chinese" Then
            _newEncoder.Language = _newEncoder.Language_Chinese
        Else
            _newEncoder.Language = _newEncoder.Language_Czech
        End If

        If My.MySettings.Default.SubtitleLanguage = "Source" Then
            _newEncoder.SubtitleLanguage = ""
        ElseIf My.MySettings.Default.SubtitleLanguage = "English" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_English
        ElseIf My.MySettings.Default.SubtitleLanguage = "Russian" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.SubtitleLanguage = "German" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_German
        ElseIf My.MySettings.Default.SubtitleLanguage = "Chinese" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Chinese
        Else
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Czech
        End If

        _newEncoder.PriorityRealTime = My.MySettings.Default.PriorityRealTime
        _newEncoder.PriorityHigh = My.MySettings.Default.PriorityHigh
        _newEncoder.PriorityAboveNormal = My.MySettings.Default.PriorityAboveNormal
        _newEncoder.PriorityNormal = My.MySettings.Default.PriorityNormal
        _newEncoder.PriorityBelowNormal = My.MySettings.Default.PriorityBelowNormal
        _newEncoder.PriorityIdle = My.MySettings.Default.PriorityIdle

        If My.Computer.FileSystem.FileExists(strInputFile) Then
            If My.MySettings.Default.VideoCodec = "Auto" Then
                If MediaTags.VideoCodec = "h264" Then
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                    _newEncoder.VideoFrameRate = ""
                    _newEncoder.VideoBitrate = ""
                    _newEncoder.AspectRatio = ""
                    _newEncoder.VideoWidth = ""
                    _newEncoder.VideoHeight = ""
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                    If My.MySettings.Default.VideoFrameRate = "Auto" Then
                        _newEncoder.VideoFrameRate = ""
                    Else
                        _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                    End If
                    If My.MySettings.Default.VideoBitrate = "Auto" Then
                        _newEncoder.VideoBitrate = ""
                    Else
                        _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                    End If
                    If My.MySettings.Default.AspectRatio = "Auto" Then
                        _newEncoder.AspectRatio = ""
                    Else
                        _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                        _newEncoder.VideoWidth = ""
                    Else
                        _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                        _newEncoder.VideoHeight = ""
                    Else
                        _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "High" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    Else
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        _newEncoder.VideoLevel = ""
                    End If
                End If
            ElseIf My.MySettings.Default.VideoCodec = "Source" Then
                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                _newEncoder.VideoFrameRate = ""
                _newEncoder.VideoBitrate = ""
                _newEncoder.AspectRatio = ""
                _newEncoder.VideoWidth = ""
                _newEncoder.VideoHeight = ""
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                _newEncoder.VideoLevel = ""
            Else
                _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                If My.MySettings.Default.VideoFrameRate = "Auto" Then
                    _newEncoder.VideoFrameRate = ""
                Else
                    _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                End If
                If My.MySettings.Default.VideoBitrate = "Auto" Then
                    _newEncoder.VideoBitrate = ""
                Else
                    _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                End If
                If My.MySettings.Default.AspectRatio = "Auto" Then
                    _newEncoder.AspectRatio = ""
                Else
                    _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                    _newEncoder.VideoWidth = ""
                Else
                    _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                    _newEncoder.VideoHeight = ""
                Else
                    _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "High" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                Else
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                End If
            End If

            If My.MySettings.Default.AudioCodec = "Auto" Then
                If MediaTags.AudioChannels = "mono" Then
                    If My.MySettings.Default.AudioChannels = "Auto" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                    ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                    End If

                ElseIf MediaTags.AudioChannels = "stereo" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                ElseIf MediaTags.AudioChannels = "5.1(side)" Or MediaTags.AudioChannels = "5.1" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                End If
            ElseIf My.MySettings.Default.AudioCodec = "Source" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec = "AAC" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec = "AC3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec = "MP3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Mp3
            End If
            If My.MySettings.Default.AudioCodec2 = "None" Then
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec2 = "AAC" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec2 = "AC3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec2 = "MP3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Mp3
            End If

            If My.MySettings.Default.AudioChannels = "Auto" Then
                _newEncoder.AudioChannels = ""
            ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                _newEncoder.AudioChannels = "1"
            ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                _newEncoder.AudioChannels = "2"
            ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                _newEncoder.AudioChannels = "6"
            End If
            If My.MySettings.Default.AudioChannels2 = "Auto" Then
                _newEncoder.AudioChannels2 = ""
            ElseIf My.MySettings.Default.AudioChannels2 = "Mono" Then
                _newEncoder.AudioChannels2 = "1"
            ElseIf My.MySettings.Default.AudioChannels2 = "Stereo" Then
                _newEncoder.AudioChannels2 = "2"
            ElseIf My.MySettings.Default.AudioChannels2 = "5.1" Then
                _newEncoder.AudioChannels2 = "6"
            End If

            If My.MySettings.Default.AudioBitrate = "Auto" Then
                _newEncoder.AudioBitrate = ""
            Else
                _newEncoder.AudioBitrate = My.MySettings.Default.AudioBitrate
            End If
            If My.MySettings.Default.AudioBitrate2 = "Auto" Then
                _newEncoder.AudioBitrate2 = ""
            Else
                _newEncoder.AudioBitrate2 = My.MySettings.Default.AudioBitrate2
            End If

            If My.MySettings.Default.AudioSamplingRate = "Auto" Then
                _newEncoder.AudioSamplingRate = ""
            Else
                _newEncoder.AudioSamplingRate = My.MySettings.Default.AudioSamplingRate
            End If
            If My.MySettings.Default.AudioSamplingRate2 = "Auto" Then
                _newEncoder.AudioSamplingRate2 = ""
            Else
                _newEncoder.AudioSamplingRate2 = My.MySettings.Default.AudioSamplingRate2
            End If

            If _hasSubtitles Then
                If My.MySettings.Default.CopySubtitles Then
                    If My.MySettings.Default.SubtitleCodec = "Source" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                    ElseIf My.MySettings.Default.SubtitleCodec = "ass" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ass
                    ElseIf My.MySettings.Default.SubtitleCodec = "srt" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Srt
                    ElseIf My.MySettings.Default.SubtitleCodec = "ssa" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ssa
                    ElseIf My.MySettings.Default.SubtitleCodec = "mov" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_MovText
                    End If
                    _newEncoder.CopySubtitles = True
                Else
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                    _newEncoder.CopySubtitles = False
                End If
            Else
                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                _newEncoder.CopySubtitles = False
            End If

            If My.MySettings.Default.AudioCodec2 = "None" Then
                _newEncoder.DualAudio = False
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2 -map 0:3"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                End If
            Else
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1 -map 0:2"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                End If
            End If
            If My.MySettings.Default.SplitFile Then
                _newEncoder.SplitFile = True
                _newEncoder.StartTime = My.MySettings.Default.StartTime
                _newEncoder.EndTime = My.MySettings.Default.EndTime
            Else
                _newEncoder.SplitFile = False
            End If
            _newEncoder.SourceFile = strInputFile
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
            _newEncoder.Format = _newEncoder.Format_Mkv
            _newEncoder.Threads = Convert.ToString(0)

            '------START------ Run if overwrite destination file by default is on ------START------'
            If _overwrite Then
                If Not My.MySettings.Default.AudioCodec2 = "None" Then
                    If Not MediaTags.VideoTrackID() = 0 Then
                        'Store the current settings
                        EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                     _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                     _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                     _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                        _newEncoder.DualAudio = False
                        _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                        _newEncoder.VideoWidth = ""
                        _newEncoder.VideoHeight = ""
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        _newEncoder.AudioChannels = ""
                        _newEncoder.AudioBitrate = ""
                        _newEncoder.AudioSamplingRate = ""
                        If _newEncoder.CopySubtitles Then
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                        Else
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                        End If
                        _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        TempBatchBackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Creating Temp File"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                        _processBox.lblError.Text += "fixing and creating temp file to work with"
                        _processBox.ShowDialog()
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        BatchBackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Converting Video"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.ShowDialog()
                    End If
                Else
                    _newEncoder.OutputPath = strOutputPath
                    _newEncoder.OutputFileName = strOutputFile
                    My.MySettings.Default.CurrentNewFileName = ""
                    My.MySettings.Default.Save()
                    Text = "Analayzing " & _strSafeFileName
                    _processBox = New ProcessDialog()
                    BatchBackgroundWorker.RunWorkerAsync()
                    _processBox.Text = "Converting Video"
                    _processBox.lblCompletedFiles.Text = ""
                    _processBox.ShowDialog()
                End If
                '------END------ Run if overwrite destination file by default is on ------END------'

                '------START------ Run if overwrite destination file by default is off ------START------'
            Else
                If My.Computer.FileSystem.FileExists(strOutputPath & strOutputFile) Then
                    Dim result As DialogResult = FileExists.ShowDialog
                    If result = Windows.Forms.DialogResult.Yes Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                TempBatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                                _processBox.ShowDialog()
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                BatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Converting Video"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.ShowDialog()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BatchBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    ElseIf result = Windows.Forms.DialogResult.No Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                TempBatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                                _processBox.ShowDialog()
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                _processBox = New ProcessDialog()
                                BatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Converting Video"
                                _processBox.lblCompletedFiles.Text = ""
                                _processBox.ShowDialog()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strNewOutputFile
                            My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BatchBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    Else
                        MessageBox.Show("User canceled")
                    End If

                Else
                    If Not My.MySettings.Default.AudioCodec2 = "None" Then
                        If Not MediaTags.VideoTrackID() = 0 Then
                            'Store the current settings
                            EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                         _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                         _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                         _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                            _newEncoder.DualAudio = False
                            _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                            _newEncoder.VideoWidth = ""
                            _newEncoder.VideoHeight = ""
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                            _newEncoder.AudioChannels = ""
                            _newEncoder.AudioBitrate = ""
                            _newEncoder.AudioSamplingRate = ""
                            If _newEncoder.CopySubtitles Then
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                            Else
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                            End If
                            _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            TempBatchBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Creating Temp File"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                            _processBox.lblError.Text += "fixing and creating temp file to work with"
                            _processBox.ShowDialog()
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            _processBox = New ProcessDialog()
                            BatchBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Converting Video"
                            _processBox.lblCompletedFiles.Text = ""
                            _processBox.ShowDialog()
                        End If
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        _processBox = New ProcessDialog()
                        BatchBackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Converting Video"
                        _processBox.lblCompletedFiles.Text = ""
                        _processBox.ShowDialog()
                    End If
                End If
            End If
            '------END------ Run if overwrite destination file by default is off ------END------'
        Else
            Dim writeErrorLogFiles As Boolean = My.MySettings.Default.WriteErrorLogFiles
            If writeErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\file_error_log.txt", True)
                    errorWriter.Write("File " & strInputFile & " not found.")
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
            MessageBox.Show("File " & strInputFile & " not found, was it deleted?")
        End If
    End Sub
    Private Sub BatchContinue()
        Dim strInputFile As String
        Dim strOutputPath As String
        Dim strOutputFile As String
        Dim strNewOutputFile As String
        Dim strTime As String
        strTime = "_" & Date.Now.Hour.ToString()
        strTime += "_" & Date.Now.Minute.ToString()
        strTime += "_" & Date.Now.Second.ToString()
        strTime += "_" & Date.Now.Millisecond.ToString()

        strInputFile = My.MySettings.Default.CurrentInputFolder & "\" & My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
        strOutputPath = My.MySettings.Default.CurrentOutputFolder & "\"
        strOutputFile = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        strNewOutputFile = My.MySettings.Default.CurrentFileName & strTime & My.MySettings.Default.CurrentOutputExtension
        _strSafeFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension

        If My.MySettings.Default.WriteLogFiles Then
            _newEncoder.EnableLogging = True
        Else
            _newEncoder.EnableLogging = False
        End If

        If My.MySettings.Default.CurrentLanguage = "English" Then
            _newEncoder.Language = _newEncoder.Language_English
        ElseIf My.MySettings.Default.CurrentLanguage = "Russian" Then
            _newEncoder.Language = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.CurrentLanguage = "German" Then
            _newEncoder.Language = _newEncoder.Language_German
        ElseIf My.MySettings.Default.CurrentLanguage = "Chinese" Then
            _newEncoder.Language = _newEncoder.Language_Chinese
        Else
            _newEncoder.Language = _newEncoder.Language_Czech
        End If

        If My.MySettings.Default.SubtitleLanguage = "Source" Then
            _newEncoder.SubtitleLanguage = ""
        ElseIf My.MySettings.Default.SubtitleLanguage = "English" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_English
        ElseIf My.MySettings.Default.SubtitleLanguage = "Russian" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.SubtitleLanguage = "German" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_German
        ElseIf My.MySettings.Default.SubtitleLanguage = "Chinese" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Chinese
        Else
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Czech
        End If

        _newEncoder.PriorityRealTime = My.MySettings.Default.PriorityRealTime
        _newEncoder.PriorityHigh = My.MySettings.Default.PriorityHigh
        _newEncoder.PriorityAboveNormal = My.MySettings.Default.PriorityAboveNormal
        _newEncoder.PriorityNormal = My.MySettings.Default.PriorityNormal
        _newEncoder.PriorityBelowNormal = My.MySettings.Default.PriorityBelowNormal
        _newEncoder.PriorityIdle = My.MySettings.Default.PriorityIdle

        If My.Computer.FileSystem.FileExists(strInputFile) Then
            If My.MySettings.Default.VideoCodec = "Auto" Then
                If MediaTags.VideoCodec = "h264" Then
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                    _newEncoder.VideoFrameRate = ""
                    _newEncoder.VideoBitrate = ""
                    _newEncoder.AspectRatio = ""
                    _newEncoder.VideoWidth = ""
                    _newEncoder.VideoHeight = ""
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                    If My.MySettings.Default.VideoFrameRate = "Auto" Then
                        _newEncoder.VideoFrameRate = ""
                    Else
                        _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                    End If
                    If My.MySettings.Default.VideoBitrate = "Auto" Then
                        _newEncoder.VideoBitrate = ""
                    Else
                        _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                    End If
                    If My.MySettings.Default.AspectRatio = "Auto" Then
                        _newEncoder.AspectRatio = ""
                    Else
                        _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                        _newEncoder.VideoWidth = ""
                    Else
                        _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                        _newEncoder.VideoHeight = ""
                    Else
                        _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                    End If
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    ElseIf My.MySettings.Default.VideoProfile = "High" Then
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                        If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                            _newEncoder.VideoLevel = ""
                        Else
                            _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                        End If
                    Else
                        _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                        _newEncoder.VideoLevel = ""
                    End If
                End If
            ElseIf My.MySettings.Default.VideoCodec = "Source" Then
                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                _newEncoder.VideoFrameRate = ""
                _newEncoder.VideoBitrate = ""
                _newEncoder.AspectRatio = ""
                _newEncoder.VideoWidth = ""
                _newEncoder.VideoHeight = ""
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                _newEncoder.VideoLevel = ""
            Else
                _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                If My.MySettings.Default.VideoFrameRate = "Auto" Then
                    _newEncoder.VideoFrameRate = ""
                Else
                    _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                End If
                If My.MySettings.Default.VideoBitrate = "Auto" Then
                    _newEncoder.VideoBitrate = ""
                Else
                    _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                End If
                If My.MySettings.Default.AspectRatio = "Auto" Then
                    _newEncoder.AspectRatio = ""
                Else
                    _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                    _newEncoder.VideoWidth = ""
                Else
                    _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                    _newEncoder.VideoHeight = ""
                Else
                    _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "High" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                Else
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                End If
            End If

            If My.MySettings.Default.AudioCodec = "Auto" Then
                If MediaTags.AudioChannels = "mono" Then
                    If My.MySettings.Default.AudioChannels = "Auto" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                        If MediaTags.AudioCodec = "aac" Then
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        Else
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                        End If
                    ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                    ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                    End If

                ElseIf MediaTags.AudioChannels = "stereo" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                ElseIf MediaTags.AudioChannels = "5.1(side)" Or MediaTags.AudioChannels = "5.1" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                End If
            ElseIf My.MySettings.Default.AudioCodec = "Source" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec = "AAC" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec = "AC3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec = "MP3" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Mp3
            End If
            If My.MySettings.Default.AudioCodec2 = "None" Then
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Copy
            ElseIf My.MySettings.Default.AudioCodec2 = "AAC" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Aac
            ElseIf My.MySettings.Default.AudioCodec2 = "AC3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Ac3
            ElseIf My.MySettings.Default.AudioCodec2 = "MP3" Then
                _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Mp3
            End If

            If My.MySettings.Default.AudioChannels = "Auto" Then
                _newEncoder.AudioChannels = ""
            ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                _newEncoder.AudioChannels = "1"
            ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                _newEncoder.AudioChannels = "2"
            ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                _newEncoder.AudioChannels = "6"
            End If
            If My.MySettings.Default.AudioChannels2 = "Auto" Then
                _newEncoder.AudioChannels2 = ""
            ElseIf My.MySettings.Default.AudioChannels2 = "Mono" Then
                _newEncoder.AudioChannels2 = "1"
            ElseIf My.MySettings.Default.AudioChannels2 = "Stereo" Then
                _newEncoder.AudioChannels2 = "2"
            ElseIf My.MySettings.Default.AudioChannels2 = "5.1" Then
                _newEncoder.AudioChannels2 = "6"
            End If

            If My.MySettings.Default.AudioBitrate = "Auto" Then
                _newEncoder.AudioBitrate = ""
            Else
                _newEncoder.AudioBitrate = My.MySettings.Default.AudioBitrate
            End If
            If My.MySettings.Default.AudioBitrate2 = "Auto" Then
                _newEncoder.AudioBitrate2 = ""
            Else
                _newEncoder.AudioBitrate2 = My.MySettings.Default.AudioBitrate2
            End If

            If My.MySettings.Default.AudioSamplingRate = "Auto" Then
                _newEncoder.AudioSamplingRate = ""
            Else
                _newEncoder.AudioSamplingRate = My.MySettings.Default.AudioSamplingRate
            End If
            If My.MySettings.Default.AudioSamplingRate2 = "Auto" Then
                _newEncoder.AudioSamplingRate2 = ""
            Else
                _newEncoder.AudioSamplingRate2 = My.MySettings.Default.AudioSamplingRate2
            End If

            If _hasSubtitles Then
                If My.MySettings.Default.CopySubtitles Then
                    If My.MySettings.Default.SubtitleCodec = "Source" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                    ElseIf My.MySettings.Default.SubtitleCodec = "ass" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ass
                    ElseIf My.MySettings.Default.SubtitleCodec = "srt" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Srt
                    ElseIf My.MySettings.Default.SubtitleCodec = "ssa" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ssa
                    ElseIf My.MySettings.Default.SubtitleCodec = "mov" Then
                        _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_MovText
                    End If
                    _newEncoder.CopySubtitles = True
                Else
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                    _newEncoder.CopySubtitles = False
                End If
            Else
                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                _newEncoder.CopySubtitles = False
            End If

            If My.MySettings.Default.AudioCodec2 = "None" Then
                _newEncoder.DualAudio = False
            ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2 -map 0:3"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                End If
            Else
                _newEncoder.DualAudio = True
                If _hasSubtitles Then
                    If My.MySettings.Default.CopySubtitles Then
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1 -map 0:2"
                    Else
                        _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                    End If
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                End If
            End If
            If My.MySettings.Default.SplitFile Then
                _newEncoder.SplitFile = True
                _newEncoder.StartTime = My.MySettings.Default.StartTime
                _newEncoder.EndTime = My.MySettings.Default.EndTime
            Else
                _newEncoder.SplitFile = False
            End If
            _newEncoder.SourceFile = strInputFile
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
            _newEncoder.Format = _newEncoder.Format_Mkv
            _newEncoder.Threads = Convert.ToString(0)

            '------START------ Run if overwrite destination file by default is on ------START------'
            If _overwrite Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                    If Not MediaTags.VideoTrackID() = 0 Then
                        'Store the current settings
                        EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                     _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                     _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                     _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                        _newEncoder.DualAudio = False
                        _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                        _newEncoder.VideoWidth = ""
                        _newEncoder.VideoHeight = ""
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                        _newEncoder.AudioChannels = ""
                        _newEncoder.AudioBitrate = ""
                        _newEncoder.AudioSamplingRate = ""
                        If _newEncoder.CopySubtitles Then
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                        Else
                            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                        End If
                        _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        TempBatchBackgroundWorker.RunWorkerAsync()
                        _processBox.Text = "Creating Temp File"
                        _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                        _processBox.lblError.Text += "fixing and creating temp file to work with"
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        BatchBackgroundWorker.RunWorkerAsync()
                    End If
                Else
                    _newEncoder.OutputPath = strOutputPath
                    _newEncoder.OutputFileName = strOutputFile
                    My.MySettings.Default.CurrentNewFileName = ""
                    My.MySettings.Default.Save()
                    Text = "Analayzing " & _strSafeFileName
                    BatchBackgroundWorker.RunWorkerAsync()
                End If
                '------END------ Run if overwrite destination file by default is on ------END------'

                '------START------ Run if overwrite destination file by default is off ------START------'
            Else
            If My.Computer.FileSystem.FileExists(strOutputPath & strOutputFile) Then
                    Dim result As DialogResult = FileExists.ShowDialog
                    If result = Windows.Forms.DialogResult.Yes Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                TempBatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strOutputFile
                                My.MySettings.Default.CurrentNewFileName = ""
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                BatchBackgroundWorker.RunWorkerAsync()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            BatchBackgroundWorker.RunWorkerAsync()
                        End If
                    ElseIf result = Windows.Forms.DialogResult.No Then
                        If Not My.MySettings.Default.AudioCodec2 = "None" Then
                            If Not MediaTags.VideoTrackID() = 0 Then
                                'Store the current settings
                                EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                             _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                             _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                             _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                                _newEncoder.DualAudio = False
                                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                                _newEncoder.VideoWidth = ""
                                _newEncoder.VideoHeight = ""
                                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                                _newEncoder.AudioChannels = ""
                                _newEncoder.AudioBitrate = ""
                                _newEncoder.AudioSamplingRate = ""
                                If _newEncoder.CopySubtitles Then
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                                Else
                                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                                End If
                                _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                TempBatchBackgroundWorker.RunWorkerAsync()
                                _processBox.Text = "Creating Temp File"
                                _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                                _processBox.lblError.Text += "fixing and creating temp file to work with"
                            Else
                                _newEncoder.OutputPath = strOutputPath
                                _newEncoder.OutputFileName = strNewOutputFile
                                My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                                My.MySettings.Default.Save()
                                Text = "Analayzing " & _strSafeFileName
                                BatchBackgroundWorker.RunWorkerAsync()
                            End If
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strNewOutputFile
                            My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            BatchBackgroundWorker.RunWorkerAsync()
                        End If
                    Else
                        MessageBox.Show("User canceled")
                    End If
                Else
                    If Not My.MySettings.Default.AudioCodec2 = "None" Then
                        If Not MediaTags.VideoTrackID() = 0 Then
                            'Store the current settings
                            EncoderSettings.SaveSettings(_appDataPath & _appName, strOutputPath, strOutputFile, _newEncoder.VideoCodec,
                                                         _newEncoder.VideoWidth, _newEncoder.VideoHeight, _newEncoder.AudioCodec, _newEncoder.AudioCodec2, _newEncoder.AudioChannels,
                                                         _newEncoder.AudioChannels2, _newEncoder.AudioBitrate, _newEncoder.AudioBitrate2, _newEncoder.AudioSamplingRate,
                                                         _newEncoder.AudioSamplingRate2, _newEncoder.SubtitleCodec, _newEncoder.DualAudio, _newEncoder.CopySubtitles, True)
                            _newEncoder.DualAudio = False
                            _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                            _newEncoder.VideoWidth = ""
                            _newEncoder.VideoHeight = ""
                            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                            _newEncoder.AudioChannels = ""
                            _newEncoder.AudioBitrate = ""
                            _newEncoder.AudioSamplingRate = ""
                            If _newEncoder.CopySubtitles Then
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                            Else
                                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                            End If
                            _newEncoder.OutputPath = EncoderSettings.TempOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            TempBatchBackgroundWorker.RunWorkerAsync()
                            _processBox.Text = "Creating Temp File"
                            _processBox.lblError.Text = "File tracks not in correct order, " & Environment.NewLine
                            _processBox.lblError.Text += "fixing and creating temp file to work with"
                        Else
                            _newEncoder.OutputPath = strOutputPath
                            _newEncoder.OutputFileName = strOutputFile
                            My.MySettings.Default.CurrentNewFileName = ""
                            My.MySettings.Default.Save()
                            Text = "Analayzing " & _strSafeFileName
                            BatchBackgroundWorker.RunWorkerAsync()
                        End If
                    Else
                        _newEncoder.OutputPath = strOutputPath
                        _newEncoder.OutputFileName = strOutputFile
                        My.MySettings.Default.CurrentNewFileName = ""
                        My.MySettings.Default.Save()
                        Text = "Analayzing " & _strSafeFileName
                        BatchBackgroundWorker.RunWorkerAsync()
                    End If
                End If
            End If
            '------END------ Run if overwrite destination file by default is off ------END------'
        Else
            Dim writeErrorLogFiles As Boolean = My.MySettings.Default.WriteErrorLogFiles
            If writeErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\file_error_log.txt", True)
                    errorWriter.Write("File " & strInputFile & " not found.")
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
            MessageBox.Show("File " & strInputFile & " not found, was it deleted?")
        End If
    End Sub
    Private Sub btnJoinStart_Click(sender As Object, e As EventArgs)
        Dim strOutputPath As String
        Dim strOutputFile As String
        Dim strNewOutputFile As String
        Dim strTime As String
        strTime = "_" & Date.Now.Hour.ToString()
        strTime += "_" & Date.Now.Minute.ToString()
        strTime += "_" & Date.Now.Second.ToString()
        strTime += "_" & Date.Now.Millisecond.ToString()

        strOutputPath = My.MySettings.Default.CurrentOutputFolder & "\"
        strOutputFile = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        strNewOutputFile = My.MySettings.Default.CurrentFileName & strTime & My.MySettings.Default.CurrentOutputExtension

        If My.MySettings.Default.WriteLogFiles Then
            _newEncoder.EnableLogging = True
        Else
            _newEncoder.EnableLogging = False
        End If

        If My.MySettings.Default.CurrentLanguage = "English" Then
            _newEncoder.Language = _newEncoder.Language_English
        ElseIf My.MySettings.Default.CurrentLanguage = "Russian" Then
            _newEncoder.Language = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.CurrentLanguage = "German" Then
            _newEncoder.Language = _newEncoder.Language_German
        ElseIf My.MySettings.Default.CurrentLanguage = "Chinese" Then
            _newEncoder.Language = _newEncoder.Language_Chinese
        Else
            _newEncoder.Language = _newEncoder.Language_Czech
        End If

        If My.MySettings.Default.SubtitleLanguage = "Source" Then
            _newEncoder.SubtitleLanguage = ""
        ElseIf My.MySettings.Default.SubtitleLanguage = "English" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_English
        ElseIf My.MySettings.Default.SubtitleLanguage = "Russian" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Russian
        ElseIf My.MySettings.Default.SubtitleLanguage = "German" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_German
        ElseIf My.MySettings.Default.SubtitleLanguage = "Chinese" Then
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Chinese
        Else
            _newEncoder.SubtitleLanguage = _newEncoder.Language_Czech
        End If

        _newEncoder.PriorityRealTime = My.MySettings.Default.PriorityRealTime
        _newEncoder.PriorityHigh = My.MySettings.Default.PriorityHigh
        _newEncoder.PriorityAboveNormal = My.MySettings.Default.PriorityAboveNormal
        _newEncoder.PriorityNormal = My.MySettings.Default.PriorityNormal
        _newEncoder.PriorityBelowNormal = My.MySettings.Default.PriorityBelowNormal
        _newEncoder.PriorityIdle = My.MySettings.Default.PriorityIdle

        If My.MySettings.Default.VideoCodec = "Auto" Then
            If MediaTags.VideoCodec = "h264" Then
                _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
                _newEncoder.VideoFrameRate = ""
                _newEncoder.VideoBitrate = ""
                _newEncoder.AspectRatio = ""
                _newEncoder.VideoWidth = ""
                _newEncoder.VideoHeight = ""
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                _newEncoder.VideoLevel = ""
            Else
                _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
                If My.MySettings.Default.VideoFrameRate = "Auto" Then
                    _newEncoder.VideoFrameRate = ""
                Else
                    _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
                End If
                If My.MySettings.Default.VideoBitrate = "Auto" Then
                    _newEncoder.VideoBitrate = ""
                Else
                    _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
                End If
                If My.MySettings.Default.AspectRatio = "Auto" Then
                    _newEncoder.AspectRatio = ""
                Else
                    _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                    _newEncoder.VideoWidth = ""
                Else
                    _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                    _newEncoder.VideoHeight = ""
                Else
                    _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
                End If
                If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                ElseIf My.MySettings.Default.VideoProfile = "High" Then
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                    If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                        _newEncoder.VideoLevel = ""
                    Else
                        _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                    End If
                Else
                    _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                    _newEncoder.VideoLevel = ""
                End If
            End If
        ElseIf My.MySettings.Default.VideoCodec = "Source" Then
            _newEncoder.VideoCodec = _newEncoder.Vcodec_Copy
            _newEncoder.VideoFrameRate = ""
            _newEncoder.VideoBitrate = ""
            _newEncoder.AspectRatio = ""
            _newEncoder.VideoWidth = ""
            _newEncoder.VideoHeight = ""
            _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
            _newEncoder.VideoLevel = ""
        Else
            _newEncoder.VideoCodec = _newEncoder.Vcodec_H264
            If My.MySettings.Default.VideoFrameRate = "Auto" Then
                _newEncoder.VideoFrameRate = ""
            Else
                _newEncoder.VideoFrameRate = My.MySettings.Default.VideoFrameRate
            End If
            If My.MySettings.Default.VideoBitrate = "Auto" Then
                _newEncoder.VideoBitrate = ""
            Else
                _newEncoder.VideoBitrate = My.MySettings.Default.VideoBitrate
            End If
            If My.MySettings.Default.AspectRatio = "Auto" Then
                _newEncoder.AspectRatio = ""
            Else
                _newEncoder.AspectRatio = My.MySettings.Default.AspectRatio
            End If
            If String.IsNullOrEmpty(My.MySettings.Default.VideoWidth) Then
                _newEncoder.VideoWidth = ""
            Else
                _newEncoder.VideoWidth = My.MySettings.Default.VideoWidth
            End If
            If String.IsNullOrEmpty(My.MySettings.Default.VideoHeight) Then
                _newEncoder.VideoHeight = ""
            Else
                _newEncoder.VideoHeight = My.MySettings.Default.VideoHeight
            End If
            If String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                End If
            ElseIf My.MySettings.Default.VideoProfile = "Baseline" Then
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_Baseline
                If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                End If
            ElseIf My.MySettings.Default.VideoProfile = "Main" Then
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_Main
                If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                End If
            ElseIf My.MySettings.Default.VideoProfile = "High" Then
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_High
                If String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
                    _newEncoder.VideoLevel = ""
                Else
                    _newEncoder.VideoLevel = My.MySettings.Default.VideoLevel
                End If
            Else
                _newEncoder.VideoProfile = _newEncoder.VideoProfile_None
                _newEncoder.VideoLevel = ""
            End If
        End If

        If My.MySettings.Default.AudioCodec = "Auto" Then
            If MediaTags.AudioChannels = "mono" Then
                If My.MySettings.Default.AudioChannels = "Auto" Then
                    If MediaTags.AudioCodec = "aac" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                    Else
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                    End If
                ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
                    If MediaTags.AudioCodec = "aac" Then
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
                    Else
                        _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                    End If
                ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
                ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
                    _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
                End If

            ElseIf MediaTags.AudioChannels = "stereo" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
            ElseIf MediaTags.AudioChannels = "5.1(side)" Or MediaTags.AudioChannels = "5.1" Then
                _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
            End If
        ElseIf My.MySettings.Default.AudioCodec = "Source" Then
            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Copy
        ElseIf My.MySettings.Default.AudioCodec = "AAC" Then
            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Aac
        ElseIf My.MySettings.Default.AudioCodec = "AC3" Then
            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Ac3
        ElseIf My.MySettings.Default.AudioCodec = "MP3" Then
            _newEncoder.AudioCodec = _newEncoder.AudioCodec_Mp3
        End If
        If My.MySettings.Default.AudioCodec2 = "None" Then
        ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
            _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Copy
        ElseIf My.MySettings.Default.AudioCodec2 = "AAC" Then
            _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Aac
        ElseIf My.MySettings.Default.AudioCodec2 = "AC3" Then
            _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Ac3
        ElseIf My.MySettings.Default.AudioCodec2 = "MP3" Then
            _newEncoder.AudioCodec2 = _newEncoder.AudioCodec_Mp3
        End If

        If My.MySettings.Default.AudioChannels = "Auto" Then
            _newEncoder.AudioChannels = ""
        ElseIf My.MySettings.Default.AudioChannels = "Mono" Then
            _newEncoder.AudioChannels = "1"
        ElseIf My.MySettings.Default.AudioChannels = "Stereo" Then
            _newEncoder.AudioChannels = "2"
        ElseIf My.MySettings.Default.AudioChannels = "5.1" Then
            _newEncoder.AudioChannels = "6"
        End If
        If My.MySettings.Default.AudioChannels2 = "Auto" Then
            _newEncoder.AudioChannels2 = ""
        ElseIf My.MySettings.Default.AudioChannels2 = "Mono" Then
            _newEncoder.AudioChannels2 = "1"
        ElseIf My.MySettings.Default.AudioChannels2 = "Stereo" Then
            _newEncoder.AudioChannels2 = "2"
        ElseIf My.MySettings.Default.AudioChannels2 = "5.1" Then
            _newEncoder.AudioChannels2 = "6"
        End If

        If My.MySettings.Default.AudioBitrate = "Auto" Then
            _newEncoder.AudioBitrate = ""
        Else
            _newEncoder.AudioBitrate = My.MySettings.Default.AudioBitrate
        End If
        If My.MySettings.Default.AudioBitrate2 = "Auto" Then
            _newEncoder.AudioBitrate2 = ""
        Else
            _newEncoder.AudioBitrate2 = My.MySettings.Default.AudioBitrate2
        End If

        If My.MySettings.Default.AudioSamplingRate = "Auto" Then
            _newEncoder.AudioSamplingRate = ""
        Else
            _newEncoder.AudioSamplingRate = My.MySettings.Default.AudioSamplingRate
        End If
        If My.MySettings.Default.AudioSamplingRate2 = "Auto" Then
            _newEncoder.AudioSamplingRate2 = ""
        Else
            _newEncoder.AudioSamplingRate2 = My.MySettings.Default.AudioSamplingRate2
        End If

        If _hasSubtitles Then
            If My.MySettings.Default.CopySubtitles Then
                If My.MySettings.Default.SubtitleCodec = "Source" Then
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Copy
                ElseIf My.MySettings.Default.SubtitleCodec = "ass" Then
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ass
                ElseIf My.MySettings.Default.SubtitleCodec = "srt" Then
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Srt
                ElseIf My.MySettings.Default.SubtitleCodec = "ssa" Then
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_Ssa
                ElseIf My.MySettings.Default.SubtitleCodec = "mov" Then
                    _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_MovText
                End If
                _newEncoder.CopySubtitles = True
            Else
                _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
                _newEncoder.CopySubtitles = False
            End If
        Else
            _newEncoder.SubtitleCodec = _newEncoder.SubtitleCodec_None
            _newEncoder.CopySubtitles = False
        End If

        If My.MySettings.Default.AudioCodec2 = "None" Then
            _newEncoder.DualAudio = False
        ElseIf My.MySettings.Default.AudioCodec2 = "Source" Then
            _newEncoder.DualAudio = True
            If _hasSubtitles Then
                If My.MySettings.Default.CopySubtitles Then
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2 -map 0:3"
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
                End If
            Else
                _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:2"
            End If
        Else
            _newEncoder.DualAudio = True
            If _hasSubtitles Then
                If My.MySettings.Default.CopySubtitles Then
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1 -map 0:2"
                Else
                    _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
                End If
            Else
                _newEncoder.CustomParams = "-map 0:0 -map 0:1 -map 0:1"
            End If
        End If

        _newEncoder.JoinSourceFile = _appDataPath & _appName & "\join_list.txt"
        _newEncoder.Format = _newEncoder.Format_Mkv
        _newEncoder.Threads = Convert.ToString(0)
        _newEncoder.OutputPath = strOutputPath

        '------START------ Run if overwrite destination file by default is on ------START------'
        If _overwrite Then
            _newEncoder.OutputFileName = strOutputFile
            My.MySettings.Default.CurrentNewFileName = ""
            My.MySettings.Default.Save()
            Text = "Analayzing " & _strSafeFileName
            _processBox = New ProcessDialog()
            JoinBackgroundWorker.RunWorkerAsync()
            _processBox.Text = "Joining Videos"
            _processBox.lblCompletedFiles.Text = ""
            _processBox.ShowDialog()
            '------END------ Run if overwrite destination file by default is on ------END------'

            '------START------ Run if overwrite destination file by default is off ------START------'
        Else
            If My.Computer.FileSystem.FileExists(strOutputPath & strOutputFile) Then
                Dim result As DialogResult = FileExists.ShowDialog
                If result = Windows.Forms.DialogResult.Yes Then
                    _newEncoder.OutputFileName = strOutputFile
                    My.MySettings.Default.CurrentNewFileName = ""
                    My.MySettings.Default.Save()
                    Text = "Analayzing " & _strSafeFileName
                    _processBox = New ProcessDialog()
                    JoinBackgroundWorker.RunWorkerAsync()
                    _processBox.Text = "Joining Videos"
                    _processBox.lblCompletedFiles.Text = ""
                    _processBox.ShowDialog()
                ElseIf result = Windows.Forms.DialogResult.No Then
                    _newEncoder.OutputFileName = strNewOutputFile
                    My.MySettings.Default.CurrentNewFileName = strNewOutputFile
                    My.MySettings.Default.Save()
                    Text = "Analayzing " & _strSafeFileName
                    _processBox = New ProcessDialog()
                    JoinBackgroundWorker.RunWorkerAsync()
                    _processBox.Text = "Joining Videos"
                    _processBox.lblCompletedFiles.Text = ""
                    _processBox.ShowDialog()
                Else
                    MessageBox.Show("User canceled")
                End If

            Else
                _newEncoder.OutputFileName = strOutputFile
                My.MySettings.Default.CurrentNewFileName = ""
                My.MySettings.Default.Save()
                Text = "Analayzing " & _strSafeFileName
                _processBox = New ProcessDialog()
                JoinBackgroundWorker.RunWorkerAsync()
                _processBox.Text = "Joining Videos"
                _processBox.lblCompletedFiles.Text = ""
                _processBox.ShowDialog()
            End If
        End If
        '------END------ Run if overwrite destination file by default is off ------END------'

    End Sub
#End Region

#Region "Background Worker Events"

#Region "Main Background Worker"
    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker.DoWork
        _newEncoder.Encode()
    End Sub
    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        Dim strLastOutputFile As String = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        Text = "Media Converter " & My.Application.Info.Version.ToString
        _processBox.Text = "Finished!"
        _processBox.Status = ""
        _processBox.ProgressValue = 100
        _processBox.lblError.Text = ""
        If String.IsNullOrEmpty(My.MySettings.Default.CurrentNewFileName) Then
            _processBox.lblCompletedFiles.Text = "Finished encoding " & strLastOutputFile & " successfully!" & Environment.NewLine
        Else
            _processBox.lblCompletedFiles.Text = "Finished encoding " & My.MySettings.Default.CurrentNewFileName & " successfully!" & Environment.NewLine
        End If
        If EncoderSettings.IsActive Then
            File.Delete(EncoderSettings.TempOutputPath & "\" & EncoderSettings.OutputFileName)
        End If
        EncoderSettings.TempOutputPath = ""
        EncoderSettings.ClearSettings()
        _processBox.btnClose.Visible = True
        If My.MySettings.Default.PlayCompletedSound Then
            PlayCompletedSound()
        End If
    End Sub
    Private Sub TempBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles TempBackgroundWorker.DoWork
        _newEncoder.Encode()
    End Sub
    Private Sub TempBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles TempBackgroundWorker.RunWorkerCompleted
        Text = "Analayzing " & _strSafeFileName
        _processBox.Text = "Converting Video"
        _processBox.lblError.Text = ""
        _newEncoder.DualAudio = EncoderSettings.DualAudio
        _newEncoder.CopySubtitles = EncoderSettings.CopySubtitles
        _newEncoder.OutputPath = EncoderSettings.OutputPath
        _newEncoder.VideoCodec = EncoderSettings.VideoCodec
        _newEncoder.VideoWidth = EncoderSettings.VideoWidth
        _newEncoder.VideoHeight = EncoderSettings.VideoHeight
        _newEncoder.AudioCodec = EncoderSettings.AudioCodec
        _newEncoder.AudioCodec2 = EncoderSettings.AudioCodec2
        _newEncoder.AudioChannels = EncoderSettings.AudioChannels
        _newEncoder.AudioChannels2 = EncoderSettings.AudioChannels2
        _newEncoder.AudioBitrate = EncoderSettings.AudioBitrate
        _newEncoder.AudioBitrate2 = EncoderSettings.AudioBitrate2
        _newEncoder.AudioSamplingRate = EncoderSettings.AudioSamplingRate
        _newEncoder.AudioSamplingRate2 = EncoderSettings.AudioSamplingRate2
        _newEncoder.SubtitleCodec = EncoderSettings.SubtitleCodec
        _newEncoder.SourceFile = EncoderSettings.TempOutputPath & "\" & EncoderSettings.OutputFileName
        If String.IsNullOrEmpty(My.MySettings.Default.CurrentNewFileName) Then
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        Else
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentNewFileName
        End If
        _newEncoder.OutputFileName = EncoderSettings.OutputFileName
        BackgroundWorker.RunWorkerAsync()
    End Sub
#End Region

#Region "Batch Background Worker"
    Private Sub BatchBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles BatchBackgroundWorker.DoWork
        _newEncoder.Encode()
    End Sub
    Private Sub BatchBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BatchBackgroundWorker.RunWorkerCompleted
        FilesListBox.Items.Remove(FilesListBox.SelectedItem)

        If FilesListBox.Items.Count > 0 Then
            Dim strLastOutputFile As String = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            _processBox.Status = ""
            _processBox.lblError.Text = ""
            If String.IsNullOrEmpty(My.MySettings.Default.CurrentNewFileName) Then
                _processBox.lblCompletedFiles.Text += "Finished encoding " & strLastOutputFile & " successfully!" & Environment.NewLine
            Else
                _processBox.lblCompletedFiles.Text += "Finished encoding " & My.MySettings.Default.CurrentNewFileName & " successfully!" & Environment.NewLine
            End If

            If EncoderSettings.IsActive Then
                File.Delete(EncoderSettings.TempOutputPath & "\" & EncoderSettings.OutputFileName)
            End If
            EncoderSettings.TempOutputPath = ""
            EncoderSettings.ClearSettings()
            FilesListBox.SelectedIndex = 0

            My.MySettings.Default.CurrentInputFolder = Path.GetDirectoryName(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.CurrentFileName = Path.GetFileNameWithoutExtension(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.CurrentInputExtension = Path.GetExtension(Convert.ToString(FilesListBox.SelectedItem))
            My.MySettings.Default.Save()
            _newFileInfo.SourceFile = Convert.ToString(FilesListBox.SelectedItem)
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentInputExtension
            _newFileInfo.GetMediaInfo()
            ReadMultipleFiles()
            BatchContinue()
        Else
            Dim strLastOutputFile As String = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
            Text = "Media Converter " & My.Application.Info.Version.ToString
            lbxFiles.Items.Clear()
            FilesListBox.Items.Clear()
            lbBatchStreams.Items.Clear()
            _processBox.Text = "Finished!"
            _processBox.Status = ""
            _processBox.ProgressValue = 100
            _totalFileCount = 0
            MediaTags.TotalDualAudioFileCount = 0
            _processBox.lblError.Text = ""
            If String.IsNullOrEmpty(My.MySettings.Default.CurrentNewFileName) Then
                _processBox.lblCompletedFiles.Text += "Finished encoding " & strLastOutputFile & " successfully!" & Environment.NewLine
            Else
                _processBox.lblCompletedFiles.Text += "Finished encoding " & My.MySettings.Default.CurrentNewFileName & " successfully!" & Environment.NewLine
            End If
            If EncoderSettings.IsActive Then
                File.Delete(EncoderSettings.TempOutputPath & "\" & EncoderSettings.OutputFileName)
            End If
            EncoderSettings.TempOutputPath = ""
            EncoderSettings.ClearSettings()
            _processBox.btnClose.Visible = True
            If My.MySettings.Default.PlayCompletedSound Then
                PlayCompletedSound()
            End If
        End If
    End Sub
    Private Sub TempBatchBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles TempBatchBackgroundWorker.DoWork
        _newEncoder.Encode()
    End Sub
    Private Sub TempBatchBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles TempBatchBackgroundWorker.RunWorkerCompleted
        Text = "Analayzing " & _strSafeFileName
        _processBox.Text = "Converting Video"
        _processBox.lblError.Text = ""
        _newEncoder.DualAudio = EncoderSettings.DualAudio
        _newEncoder.CopySubtitles = EncoderSettings.CopySubtitles
        _newEncoder.OutputPath = EncoderSettings.OutputPath
        _newEncoder.VideoCodec = EncoderSettings.VideoCodec
        _newEncoder.VideoWidth = EncoderSettings.VideoWidth
        _newEncoder.VideoHeight = EncoderSettings.VideoHeight
        _newEncoder.AudioCodec = EncoderSettings.AudioCodec
        _newEncoder.AudioCodec2 = EncoderSettings.AudioCodec2
        _newEncoder.AudioChannels = EncoderSettings.AudioChannels
        _newEncoder.AudioChannels2 = EncoderSettings.AudioChannels2
        _newEncoder.AudioBitrate = EncoderSettings.AudioBitrate
        _newEncoder.AudioBitrate2 = EncoderSettings.AudioBitrate2
        _newEncoder.AudioSamplingRate = EncoderSettings.AudioSamplingRate
        _newEncoder.AudioSamplingRate2 = EncoderSettings.AudioSamplingRate2
        _newEncoder.SubtitleCodec = EncoderSettings.SubtitleCodec
        _newEncoder.SourceFile = EncoderSettings.TempOutputPath & "\" & EncoderSettings.OutputFileName
        If String.IsNullOrEmpty(My.MySettings.Default.CurrentNewFileName) Then
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentFileName & My.MySettings.Default.CurrentOutputExtension
        Else
            _newEncoder.SourceFileName = My.MySettings.Default.CurrentNewFileName
        End If
        _newEncoder.OutputFileName = EncoderSettings.OutputFileName
        BatchBackgroundWorker.RunWorkerAsync()
    End Sub
#End Region

#Region "Join Background Worker"
    Private Sub JoinBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles JoinBackgroundWorker.DoWork
        _newEncoder.JoinVideos()
    End Sub
    Private Sub JoinBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles JoinBackgroundWorker.RunWorkerCompleted
        Text = "Media Converter " & My.Application.Info.Version.ToString
        _processBox.Text = "Finished!"
        _processBox.Status = ""
        _processBox.ProgressValue = 100
        _processBox.lblCompletedFiles.Text = "Finished joining videos"
        _processBox.btnClose.Visible = True
        _totalFileCount = 0
        MediaTags.TotalDualAudioFileCount = 0
        _processBox.lblError.Text = ""
        If My.MySettings.Default.PlayCompletedSound Then
            PlayCompletedSound()
        End If
    End Sub
#End Region

#End Region

#Region "Read File Info"
    Private Sub ReadSingleFile()
        Try
            Dim streamList As String = Nothing
            _hasSubtitles = False
            _audioTrackCount = 0
            MediaTags.AudioTrackCount = 0

            Dim xtr As New XmlTextReader(New StringReader(_newFileInfo.FFprobeStandardOutput))
            While xtr.Read()
                If xtr.NodeType = XmlNodeType.Element Then
                    Dim stream As String = Nothing
                    If xtr.Name = "stream" Then
                        Dim indexNode = xtr.GetAttribute("index")
                        Dim codecTypeNode = xtr.GetAttribute("codec_type")
                        Dim codecNameNode = xtr.GetAttribute("codec_name")
                        Dim profileNode = xtr.GetAttribute("profile")
                        Dim widthNode = xtr.GetAttribute("width")
                        Dim heightNode = xtr.GetAttribute("height")
                        Dim sampleRateNode = xtr.GetAttribute("sample_rate")
                        Dim channelsNode = xtr.GetAttribute("channel_layout")

                        Select Case codecTypeNode
                            Case "video"
                                '------START------ Run if ignore pictures is checked ------START------'
                                If _ignorePictures Then
                                    If codecNameNode = "mjpeg" Then
                                    ElseIf codecNameNode = "bmp" Then
                                    ElseIf codecNameNode = "png" Then
                                    Else
                                        MediaTags.VideoTrackID = indexNode
                                        MediaTags.VideoCodec = codecNameNode
                                        _totalTrackCount += 1
                                        stream = String.Format("|Track {0}: {1}: {2} {3} {4}x{5}", indexNode, FirstCharToUpper(codecTypeNode), codecNameNode, profileNode, widthNode, heightNode)
                                    End If
                                    '------END------ Run if ignore pictures is checked ------END------'

                                    '------START------ Run if ignore pictures is not checked ------START------'
                                Else
                                    MediaTags.VideoTrackID = indexNode
                                    MediaTags.VideoCodec = codecNameNode
                                    _totalTrackCount += 1
                                    stream = String.Format("|Track {0}: {1}: {2} {3} {4}x{5}", indexNode, FirstCharToUpper(codecTypeNode), codecNameNode, profileNode, widthNode, heightNode)
                                End If
                                '------END------ Run if ignore pictures is not checked ------END------'
                                Exit Select
                            Case "audio"
                                If _audioTrackCount = 0 Then
                                    MediaTags.AudioTrackID = indexNode
                                    MediaTags.AudioCodec = codecNameNode
                                    MediaTags.AudioChannels = channelsNode
                                    MediaTags.AudioTrackCount += 1
                                    _audioTrackCount += 1
                                    _totalTrackCount += 1
                                    stream = String.Format("|Track {0}: {1}: {2} {3} {4} Hz, {5}", indexNode, FirstCharToUpper(codecTypeNode), codecNameNode, profileNode, sampleRateNode, FirstCharToUpper(channelsNode))
                                Else
                                    MediaTags.AudioTrackCount += 1
                                    _audioTrackCount += 1
                                    _totalTrackCount += 1
                                    stream = String.Format("|Track {0}: {1}: {2} {3} {4} Hz, {5}", indexNode, FirstCharToUpper(codecTypeNode), codecNameNode, profileNode, sampleRateNode, FirstCharToUpper(channelsNode))
                                End If
                                Exit Select
                            Case "subtitle"
                                _hasSubtitles = True
                                _totalTrackCount += 1
                                stream = String.Format("|Track {0}: {1}: {2}", indexNode, FirstCharToUpper(codecTypeNode), codecNameNode)
                                Exit Select
                        End Select

                    End If
                    If xtr.Name = "tag" Then
                        Dim tagname = xtr.GetAttribute("key")
                        Dim tagvalue = xtr.GetAttribute("value")
                        Dim language As String = Nothing
                        Select Case tagname
                            Case "language"
                                'do what you want to do
                                If tagvalue = "und" Then
                                    language = "Undetermined"
                                ElseIf tagvalue = "chi" Then
                                    language = "Chinese"
                                ElseIf tagvalue = "cze" Then
                                    language = "Czech"
                                ElseIf tagvalue = "dut" Then
                                    language = "Dutch"
                                ElseIf tagvalue = "eng" Then
                                    language = "English"
                                ElseIf tagvalue = "fin" Then
                                    language = "Finnish"
                                ElseIf tagvalue = "fre" Then
                                    language = "French"
                                ElseIf tagvalue = "ger" Then
                                    language = "German"
                                ElseIf tagvalue = "ita" Then
                                    language = "Italian"
                                ElseIf tagvalue = "jpn" Then
                                    language = "Japanese"
                                ElseIf tagvalue = "nor" Then
                                    language = "Norwegian"
                                ElseIf tagvalue = "por" Then
                                    language = "Portuguese"
                                ElseIf tagvalue = "rus" Then
                                    language = "Russian"
                                ElseIf tagvalue = "spa" Then
                                    language = "Spanish"
                                ElseIf tagvalue = "swe" Then
                                    language = "Swedish"
                                Else
                                    language = tagvalue
                                End If
                                Exit Select
                            Case "LANGUAGE"
                                'do what you want to do
                                If tagvalue = "und" Then
                                    language = "Undetermined"
                                ElseIf tagvalue = "chi" Then
                                    language = "Chinese"
                                ElseIf tagvalue = "cze" Then
                                    language = "Czech"
                                ElseIf tagvalue = "dut" Then
                                    language = "Dutch"
                                ElseIf tagvalue = "eng" Then
                                    language = "English"
                                ElseIf tagvalue = "fin" Then
                                    language = "Finnish"
                                ElseIf tagvalue = "fre" Then
                                    language = "French"
                                ElseIf tagvalue = "ger" Then
                                    language = "German"
                                ElseIf tagvalue = "ita" Then
                                    language = "Italian"
                                ElseIf tagvalue = "jpn" Then
                                    language = "Japanese"
                                ElseIf tagvalue = "nor" Then
                                    language = "Norwegian"
                                ElseIf tagvalue = "por" Then
                                    language = "Portuguese"
                                ElseIf tagvalue = "rus" Then
                                    language = "Russian"
                                ElseIf tagvalue = "spa" Then
                                    language = "Spanish"
                                ElseIf tagvalue = "swe" Then
                                    language = "Swedish"
                                Else
                                    language = tagvalue
                                End If
                                Exit Select
                        End Select
                        stream = String.Format(" {0}", language)
                    End If
                    streamList += stream
                End If
            End While
            streamList = streamList.Remove(0, 1)
            If TabControl.SelectedTab Is TabControl.TabPages("MainTabPage") Then
                lbStreams.Items.AddRange(streamList.Split("|"c))
            ElseIf TabControl.SelectedTab Is TabControl.TabPages("BatchTabPage") Then
                lbBatchStreams.Items.AddRange(streamList.Split("|"c))
            ElseIf TabControl.SelectedTab Is TabControl.TabPages("JoinTabPage") Then
                lbJoinStreams.Items.AddRange(streamList.Split("|"c))
            End If
        Catch ex As Exception
            If My.MySettings.Default.WriteErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\error_log.txt", True)
                    errorWriter.WriteLine("An error has occurred while reading file tracks info in function ReadSingleFile")
                    errorWriter.WriteLine(ex.Message)
                    errorWriter.WriteLine(ex.ToString)
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
        End Try
    End Sub
    Private Sub ReadMultipleFiles()
        Try
            _hasSubtitles = False
            Dim xtr As New XmlTextReader(New StringReader(_newFileInfo.FFprobeStandardOutput))
            While xtr.Read()
                If xtr.NodeType = XmlNodeType.Element Then
                    If xtr.Name = "stream" Then
                        Dim indexNode = xtr.GetAttribute("index")
                        Dim codecTypeNode = xtr.GetAttribute("codec_type")
                        Dim codecNameNode = xtr.GetAttribute("codec_name")
                        Dim channelsNode = xtr.GetAttribute("channel_layout")

                        Select Case codecTypeNode
                            Case "video"
                                '------START------ Run if ignore pictures is checked ------START------'
                                If _ignorePictures Then
                                    If codecNameNode = "mjpeg" Then
                                    ElseIf codecNameNode = "bmp" Then
                                    ElseIf codecNameNode = "png" Then
                                    Else
                                        MediaTags.VideoTrackID = indexNode
                                        MediaTags.VideoCodec = codecNameNode
                                        _totalTrackCount += 1
                                    End If
                                    '------END------ Run if ignore pictures is checked ------END------'

                                    '------START------ Run if ignore pictures is not checked ------START------'
                                Else
                                    MediaTags.VideoTrackID = indexNode
                                    MediaTags.VideoCodec = codecNameNode
                                    _totalTrackCount += 1
                                End If
                                '------END------ Run if ignore pictures is not checked ------END------'
                                Exit Select
                            Case "audio"
                                If _audioTrackCount = 0 Then
                                    MediaTags.AudioTrackID = indexNode
                                    MediaTags.AudioCodec = codecNameNode
                                    MediaTags.AudioChannels = channelsNode
                                    MediaTags.AudioTrackCount += 1
                                    _audioTrackCount += 1
                                    _totalTrackCount += 1
                                Else
                                    MediaTags.AudioTrackCount += 1
                                    _audioTrackCount += 1
                                    _totalTrackCount += 1
                                End If
                                Exit Select
                            Case "subtitle"
                                _hasSubtitles = True
                                _totalTrackCount += 1
                                Exit Select
                        End Select
                    End If
                End If
            End While
        Catch ex As Exception
            If My.MySettings.Default.WriteErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\error_log.txt", True)
                    errorWriter.WriteLine("An error has occurred while reading file tracks info in function ReadMultipleFiles")
                    errorWriter.WriteLine(ex.Message)
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
        End Try
    End Sub

#Region "Prep Multiple Files"
    Private Sub PrepReadMultipleFiles()
        _newFileInfo.SourceFile = Convert.ToString(ReadFilesListBox.SelectedItem)
        _newFileInfo.GetMediaInfo()
        PrepMultipleFiles()
    End Sub
    Private Sub PrepContinueMultipleFiles()
        ReadFilesListBox.Items.Remove(ReadFilesListBox.SelectedItem)

        If ReadFilesListBox.Items.Count > 0 Then
            ReadFilesListBox.SelectedIndex = 0
            _newFileInfo.SourceFile = Convert.ToString(ReadFilesListBox.SelectedItem)
            _newFileInfo.GetMediaInfo()
            PrepMultipleFiles()
        End If

    End Sub
    Private Sub PrepMultipleFiles()
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(_newFileInfo.FFprobeStandardOutput)
            Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName("stream")
            MediaTags.AudioTrackCount = 0
            For Each elem As XmlNode In nodeList
                Dim codecTypeNode As XmlNode = elem.Attributes("codec_type")
                Dim trackType = ""

                If codecTypeNode IsNot Nothing Then
                    trackType = codecTypeNode.Value
                End If
                If trackType = "audio" Then
                    MediaTags.AudioTrackCount += 1
                End If
            Next elem
            If MediaTags.AudioTrackCount > 1 Then
                MediaTags.TotalDualAudioFileCount = MediaTags.TotalDualAudioFileCount + 1
            End If
            PrepContinueMultipleFiles()
        Catch ex As Exception
            If My.MySettings.Default.WriteErrorLogFiles Then
                Try
                    Dim errorWriter As New StreamWriter(My.MySettings.Default.LogPath & "\error_log.txt", True)
                    errorWriter.WriteLine("An error has occurred while reading file tracks info in function PrepMultipleFiles")
                    errorWriter.WriteLine(ex.Message)
                    errorWriter.Flush()
                    errorWriter.Close()
                    errorWriter.Dispose()
                Catch ex1 As Exception
                    MessageBox.Show(ex1.Message)
                End Try
            End If
        End Try
    End Sub
#End Region

#End Region

#Region "Output Options Button Clicked"
    Private Sub btnOutputOptions_Click(sender As Object, e As EventArgs) Handles btnOutputOptions.Click
        OutputOptions.ShowDialog()
        lbStreams.Items.Clear()
        lbBatchStreams.Items.Clear()
        lbJoinStreams.Items.Clear()
        LoadSettings()
    End Sub
#End Region

#Region "Check for updates"
    Private Sub UpdateApp()
        Updater.UpdateUpdater()
        If My.Settings.AutoCheckForUpdate Then
            If Updater.CheckForUpdate() = "Yes" Then
                ShowUpdateDialog()
            End If
        End If
    End Sub
    Private Sub ShowUpdateDialog()
        Using f As New UpdateForm()
            f.Text = String.Format(f.Text, Updater.AppVersion)
            If Updater.Document.Root IsNot Nothing Then
                f.MoreInfoLink = CStr(Updater.Document.Root.Element("info"))
                f.Info = String.Format(f.Info, Updater.NewVersion, CDate(Updater.Document.Root.Element("date")))
            End If
            If f.ShowDialog(Me) = DialogResult.OK Then
                Updater.LaunchUpdater(Updater.Document)
                Close()
            End If
        End Using
    End Sub
#End Region

#Region "Form Closing"
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Copy window location to app settings
        My.MySettings.Default.WindowLocation = Location

        ' Copy window size to app settings
        If WindowState = FormWindowState.Normal Then
            My.MySettings.Default.WindowSize = Size
        Else
            My.MySettings.Default.WindowSize = RestoreBounds.Size
        End If

        ' Save settings
        My.MySettings.Default.Save()
    End Sub
#End Region

End Class