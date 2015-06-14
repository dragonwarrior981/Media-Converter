Imports System.Text.RegularExpressions
' ReSharper disable once ClassNeverInstantiated.Global
Public Class OutputOptions

#Region "Fields"
    Private ReadOnly _outputFormats() As String = {"MKV", "MP4"}
    Private ReadOnly _languages() As String = {"English", "Russian", "German", "Chinese", "Czech"}
    Private ReadOnly _subtitleLanguages() As String = {"Source", "English", "Russian", "German", "Chinese", "Czech"}

    Private ReadOnly _videoPresets() As String = {"Auto", "SD", "720p", "1080p", "Custom"}
    Private ReadOnly _videoCodecs() As String = {"Auto", "Source", "x264"}
    '    Private ReadOnly _frameSizes() As String = {"Auto", "852x480 - hd480", "1280x720 - hd720", "1920x1080 - hd1080"}
    Private ReadOnly _videoFrameRates() As String = {"Auto", "24", "23.976", "25", "29.97", "30", "50", "59.94", "60"}
    Private ReadOnly _videoBitrates() As String = {"Auto", "1000", "2200", "2900", "3200", "6400", "12800"}
    Private ReadOnly _aspectRatios() As String = {"Auto", "4:3", "16:9", "37:20", "47:20"}
    Private ReadOnly _videoProfiles() As String = {"Auto", "Baseline", "Main", "High"}
    Private ReadOnly _videoLevels() As String = {"Auto", "3.0", "3.1", "4.0", "4.1", "4.2", "5.0", "5.1"}

    Private ReadOnly _audioCodecs() As String = {"Auto", "Source", "AAC", "AC3", "MP3"}
    Private _audioCodecs2() As String
    Private ReadOnly _audioChannels() As String = {"Auto", "Mono", "Stereo", "5.1"}
    Private ReadOnly _audioBitrates() As String = {"Auto", "32", "40", "48", "56", "64", "80", "96", "128", "160", "192", "256", "320", "384", "448", "512", "640"}
    Private ReadOnly _audioSampleRates() As String = {"Auto", "22050", "32000", "44100", "48000"}

    Private _subtitleCodecs() As String

    Private _strStartHr As String
    Private _strStartMin As String
    Private _strStartSec As String
    Private _strEndHr As String
    Private _strEndMin As String
    Private _strEndSec As String
#End Region

#Region "Form Load"
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon

        'General Options
        For Each item As String In _outputFormats
            ddOuputFormat.Items.Add(item)
        Next item
        For Each item As String In _languages
            ddLanguage.Items.Add(item)
        Next item
        For Each item As String In _subtitleLanguages
            ddSubtitleLanguage.Items.Add(item)
        Next item

        'Video Options
        For Each item As String In _videoPresets
            ddVideoPreset.Items.Add(item)
        Next item
        For Each item As String In _videoCodecs
            ddVideoCodec.Items.Add(item)
        Next item
        For Each item As String In _videoFrameRates
            ddVideoFrameRate.Items.Add(item)
        Next item
        For Each item As String In _videoBitrates
            ddVideoBitrate.Items.Add(item)
        Next item
        For Each item As String In _aspectRatios
            ddAspectRatio.Items.Add(item)
        Next item
        For Each item As String In _videoProfiles
            ddVideoProfile.Items.Add(item)
        Next item
        For Each item As String In _videoLevels
            ddVideoLevel.Items.Add(item)
        Next item

        'Audio Options
        For Each item As String In _audioCodecs
            ddAudioCodec.Items.Add(item)
        Next item
        For Each item As String In _audioChannels
            ddAudioChannels.Items.Add(item)
        Next item
        For Each item As String In _audioChannels
            ddAudioChannels2.Items.Add(item)
        Next item
        For Each item As String In _audioBitrates
            ddAudioBitrate.Items.Add(item)
        Next item
        For Each item As String In _audioBitrates
            ddAudioBitrate2.Items.Add(item)
        Next item
        For Each item As String In _audioSampleRates
            ddAudioSampleRate.Items.Add(item)
        Next item
        For Each item As String In _audioSampleRates
            ddAudioSampleRate2.Items.Add(item)
        Next item

    End Sub
    Private Sub OutputOptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strStartArr() As String = My.MySettings.Default.StartTime.Split(Convert.ToChar(":"))
        Dim strEndArr() As String = My.MySettings.Default.EndTime.Split(Convert.ToChar(":"))

        If Not String.IsNullOrEmpty(My.MySettings.Default.StartTime) Then
            txtStartHr.Text = strStartArr(0)
            txtStartMin.Text = strStartArr(1)
            txtStartSec.Text = strStartArr(2)
        Else
            txtStartHr.Text = String.Format("{0}", "00")
            txtStartMin.Text = String.Format("{0}", "00")
            txtStartSec.Text = String.Format("{0}", "00")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.EndTime) Then
            txtEndHr.Text = strEndArr(0)
            txtEndMin.Text = strEndArr(1)
            txtEndSec.Text = strEndArr(2)
        Else
            txtEndHr.Text = String.Format("{0}", "00")
            txtEndMin.Text = String.Format("{0}", "00")
            txtEndSec.Text = String.Format("{0}", "00")
        End If

        If My.MySettings.Default.CurrentOutputExtension = ".mkv" Then
            ddOuputFormat.Text = String.Format("{0}", "MKV")
        Else
            ddOuputFormat.Text = String.Format("{0}", "MP4")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.CurrentLanguage) Then
            ddLanguage.Text = My.MySettings.Default.CurrentLanguage
        Else
            ddLanguage.Text = String.Format("{0}", "English")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.SubtitleLanguage) Then
            ddSubtitleLanguage.Text = My.MySettings.Default.SubtitleLanguage
        Else
            ddSubtitleLanguage.Text = String.Format("{0}", "English")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.SubtitleCodec) Then
            ddSubtitleCodec.Text = My.MySettings.Default.SubtitleCodec
        Else
            ddSubtitleCodec.Text = String.Format("{0}", "Source")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoPreset) Then
            ddVideoPreset.Text = My.MySettings.Default.VideoPreset
        Else
            ddVideoPreset.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoCodec) Then
            ddVideoCodec.Text = My.MySettings.Default.VideoCodec
        Else
            ddVideoCodec.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoFrameRate) Then
            ddVideoFrameRate.Text = My.MySettings.Default.VideoFrameRate
        Else
            ddVideoFrameRate.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoBitrate) Then
            ddVideoBitrate.Text = My.MySettings.Default.VideoBitrate
        Else
            ddVideoBitrate.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AspectRatio) Then
            ddAspectRatio.Text = My.MySettings.Default.AspectRatio
        Else
            ddAspectRatio.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoProfile) Then
            ddVideoProfile.Text = My.MySettings.Default.VideoProfile
        Else
            ddVideoProfile.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.VideoLevel) Then
            ddVideoLevel.Text = My.MySettings.Default.VideoLevel
        Else
            ddVideoLevel.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioCodec) Then
            ddAudioCodec.Text = My.MySettings.Default.AudioCodec
        Else
            ddAudioCodec.Text = String.Format("{0}", "Auto")
        End If

        If Not String.IsNullOrEmpty(MediaTags.AudioTrackCount) Then
            If MediaTags.AudioTrackCount > 1 Then
                _audioCodecs2 = {"None", "Source", "AAC", "AC3", "MP3"}
            ElseIf MediaTags.AudioTrackCount = 1 Then
                _audioCodecs2 = {"None", "AAC", "AC3", "MP3"}
            Else
                _audioCodecs2 = {"None", "AAC", "AC3", "MP3"}
            End If
        Else
            _audioCodecs2 = {"None", "AAC", "AC3", "MP3"}
        End If

        ddAudioCodec2.Items.Clear()
        For Each item As String In _audioCodecs2
            ddAudioCodec2.Items.Add(item)
        Next item

        If MediaTags.AudioTrackCount = 0 Then
            ddAudioCodec2.Text = String.Format("{0}", "AAC")
        Else
            If MediaTags.AudioTrackCount > 1 Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.AudioCodec2) Then
                    ddAudioCodec2.Text = My.MySettings.Default.AudioCodec2
                Else
                    ddAudioCodec2.Text = String.Format("{0}", "AAC")
                End If
            ElseIf MediaTags.AudioTrackCount = 1 Then
                If Not String.IsNullOrEmpty(My.MySettings.Default.AudioCodec2) Then
                    If My.MySettings.Default.AudioCodec2 = "Source" Then
                        ddAudioCodec2.Text = String.Format("{0}", "AAC")
                    Else
                        ddAudioCodec2.Text = My.MySettings.Default.AudioCodec2
                    End If
                Else
                    ddAudioCodec2.Text = String.Format("{0}", "AAC")
                End If
            Else
                If Not String.IsNullOrEmpty(My.MySettings.Default.AudioCodec2) Then
                    If My.MySettings.Default.AudioCodec2 = "Source" Then
                        ddAudioCodec2.Text = String.Format("{0}", "AAC")
                    Else
                        ddAudioCodec2.Text = My.MySettings.Default.AudioCodec2
                    End If
                Else
                    ddAudioCodec2.Text = String.Format("{0}", "AAC")
                End If
            End If
            My.MySettings.Default.AudioCodec2 = ddAudioCodec2.Text
            My.MySettings.Default.Save()
        End If


        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioChannels) Then
            ddAudioChannels.Text = My.MySettings.Default.AudioChannels
        Else
            ddAudioChannels.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioChannels2) Then
            ddAudioChannels2.Text = My.MySettings.Default.AudioChannels2
        Else
            ddAudioChannels2.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioBitrate) Then
            ddAudioBitrate.Text = My.MySettings.Default.AudioBitrate
        Else
            ddAudioBitrate.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioBitrate2) Then
            ddAudioBitrate2.Text = My.MySettings.Default.AudioBitrate2
        Else
            ddAudioBitrate2.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioSamplingRate) Then
            ddAudioSampleRate.Text = My.MySettings.Default.AudioSamplingRate
        Else
            ddAudioSampleRate.Text = String.Format("{0}", "Auto")
        End If
        If Not String.IsNullOrEmpty(My.MySettings.Default.AudioSamplingRate2) Then
            ddAudioSampleRate2.Text = My.MySettings.Default.AudioSamplingRate2
        Else
            ddAudioSampleRate2.Text = String.Format("{0}", "Auto")
        End If

        cbSubtitles.Checked = My.MySettings.Default.CopySubtitles
        cbSplit.Checked = My.MySettings.Default.SplitFile
        txtWidth.Text = My.MySettings.Default.VideoWidth
        txtHeight.Text = My.MySettings.Default.VideoHeight
        cbCrop.Checked = My.MySettings.Default.VideoCrop
        txtCropTop.Text = My.MySettings.Default.CropTop
        txtCropBottom.Text = My.MySettings.Default.CropBottom
        txtCropLeft.Text = My.MySettings.Default.CropLeft
        txtCropRight.Text = My.MySettings.Default.CropRight
        cbPad.Checked = My.MySettings.Default.VideoPad
        txtPadTop.Text = My.MySettings.Default.PadTop
        txtPadBottom.Text = My.MySettings.Default.PadBottom
        txtPadLeft.Text = My.MySettings.Default.PadLeft
        txtPadRight.Text = My.MySettings.Default.PadRight

        AddHandler ddVideoPreset.SelectedIndexChanged, AddressOf ddVideoPreset_SelectedIndexChanged

    End Sub
#End Region

#Region "Button Handlers"
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Close()
        If cbCrop.Checked Then
            If String.IsNullOrEmpty(txtCropTop.Text) Then
                My.MySettings.Default.CropTop = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtCropBottom.Text) Then
                My.MySettings.Default.CropBottom = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtCropLeft.Text) Then
                My.MySettings.Default.CropLeft = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtCropRight.Text) Then
                My.MySettings.Default.CropRight = 0
                My.MySettings.Default.Save()
            End If
        End If
        If cbPad.Checked Then
            If String.IsNullOrEmpty(txtPadTop.Text) Then
                My.MySettings.Default.PadTop = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtPadBottom.Text) Then
                My.MySettings.Default.PadBottom = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtPadLeft.Text) Then
                My.MySettings.Default.PadLeft = 0
                My.MySettings.Default.Save()
            End If
            If String.IsNullOrEmpty(txtPadRight.Text) Then
                My.MySettings.Default.PadRight = 0
                My.MySettings.Default.Save()
            End If
        End If
    End Sub
    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click
        ddOuputFormat.Text = String.Format("{0}", "MKV")
        ddLanguage.Text = String.Format("{0}", "English")
        cbSubtitles.Checked = True
        cbSplit.Checked = False
        txtStartHr.Text = String.Format("{0}", "00")
        txtStartMin.Text = String.Format("{0}", "00")
        txtStartSec.Text = String.Format("{0}", "00")
        txtEndHr.Text = String.Format("{0}", "00")
        txtEndMin.Text = String.Format("{0}", "00")
        txtEndSec.Text = String.Format("{0}", "00")
        My.MySettings.Default.StartTime = "00:00:00"
        My.MySettings.Default.EndTime = "00:00:00"
        My.MySettings.Default.Save()

        ddVideoPreset.Text = String.Format("{0}", "Auto")
        ddVideoCodec.Text = String.Format("{0}", "Auto")
        ddVideoFrameRate.Text = String.Format("{0}", "Auto")
        ddVideoBitrate.Text = String.Format("{0}", "Auto")
        ddAspectRatio.Text = String.Format("{0}", "Auto")
        txtWidth.Text = ""
        txtHeight.Text = ""

        ddAudioCodec.Text = String.Format("{0}", "Auto")
        ddAudioCodec2.Text = String.Format("{0}", "None")
        ddAudioChannels.Text = String.Format("{0}", "Auto")
        ddAudioChannels2.Text = String.Format("{0}", "Stereo")
        ddAudioBitrate.Text = String.Format("{0}", "Auto")
        ddAudioBitrate2.Text = String.Format("{0}", "Auto")
        ddAudioSampleRate.Text = String.Format("{0}", "Auto")
        ddAudioSampleRate2.Text = String.Format("{0}", "Auto")
    End Sub
#End Region

#Region "General Settings"
    Private Sub ddOuputFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddOuputFormat.SelectedIndexChanged
        If ddOuputFormat.Text = String.Format("{0}", "MKV") Then
            My.MySettings.Default.CurrentOutputExtension = ".mkv"
            _subtitleCodecs = {"Source", "ass", "srt", "ssa"}
        Else
            My.MySettings.Default.CurrentOutputExtension = ".mp4"
            _subtitleCodecs = {"Source", "mov"}
        End If
        My.MySettings.Default.Save()

        ddSubtitleCodec.Items.Clear()
        For Each item As String In _subtitleCodecs
            ddSubtitleCodec.Items.Add(item)
        Next item

        If My.MySettings.Default.SubtitleCodec = "Source" Then
            ddSubtitleCodec.Text = String.Format("{0}", "Source")
        ElseIf My.MySettings.Default.SubtitleCodec = "ass" Then
            If My.MySettings.Default.CurrentOutputExtension = ".mkv" Then
                ddSubtitleCodec.Text = String.Format("{0}", "ass")
            Else
                ddSubtitleCodec.Text = String.Format("{0}", "Source")
            End If
        ElseIf My.MySettings.Default.SubtitleCodec = "srt" Then
            If My.MySettings.Default.CurrentOutputExtension = ".mkv" Then
                ddSubtitleCodec.Text = String.Format("{0}", "srt")
            Else
                ddSubtitleCodec.Text = String.Format("{0}", "Source")
            End If
        ElseIf My.MySettings.Default.SubtitleCodec = "ssa" Then
            If My.MySettings.Default.CurrentOutputExtension = ".mkv" Then
                ddSubtitleCodec.Text = String.Format("{0}", "ssa")
            Else
                ddSubtitleCodec.Text = String.Format("{0}", "Source")
            End If
        ElseIf My.MySettings.Default.SubtitleCodec = "mov" Then
            If My.MySettings.Default.CurrentOutputExtension = ".mp4" Then
                ddSubtitleCodec.Text = String.Format("{0}", "mov")
            Else
                ddSubtitleCodec.Text = String.Format("{0}", "Source")
            End If
        Else
            ddSubtitleCodec.Text = String.Format("{0}", "Source")
        End If

    End Sub
    Private Sub ddLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddLanguage.SelectedIndexChanged
        My.MySettings.Default.CurrentLanguage = ddLanguage.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbSubtitles_CheckedChanged(sender As Object, e As EventArgs) Handles cbSubtitles.CheckedChanged
        My.MySettings.Default.CopySubtitles = cbSubtitles.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbSplit_CheckedChanged(sender As Object, e As EventArgs) Handles cbSplit.CheckedChanged
        If cbSplit.Checked = True Then
            My.MySettings.Default.SplitFile = Convert.ToBoolean(cbSplit.CheckState)
        Else
            My.MySettings.Default.StartTime = "00:00:00"
            My.MySettings.Default.EndTime = "00:00:00"
            My.MySettings.Default.SplitFile = Convert.ToBoolean(cbSplit.CheckState)
        End If
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddSubtitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSubtitleLanguage.SelectedIndexChanged
        My.MySettings.Default.SubtitleLanguage = ddSubtitleLanguage.Text
        My.MySettings.Default.Save()
    End Sub

#Region "Split Time Changed"
    Private Sub txtStartHour_TextChanged(sender As Object, e As EventArgs) Handles txtStartHr.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtStartHr.Text) Then
                Convert.ToInt32(txtStartHr.Text)
                _strStartHr = txtStartHr.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        If txtStartHr.Text.Length = txtStartHr.MaxLength Then
            txtStartMin.Focus()
        End If
    End Sub
    Private Sub txtStartHr_LostFocus(sender As Object, e As EventArgs) Handles txtStartHr.LostFocus
        If Not Regex.IsMatch(txtStartHr.Text, "\d{2}") And Regex.IsMatch(txtStartHr.Text, "[0-9]") Then
            _strStartHr = "0" & txtStartHr.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub txtStartMin_TextChanged(sender As Object, e As EventArgs) Handles txtStartMin.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtStartMin.Text) Then
                Convert.ToInt32(txtStartMin.Text)
                _strStartMin = txtStartMin.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        If txtStartMin.Text.Length = txtStartMin.MaxLength Then
            txtStartSec.Focus()
        End If
    End Sub
    Private Sub txtStartMin_LostFocus(sender As Object, e As EventArgs) Handles txtStartMin.LostFocus
        If Not Regex.IsMatch(txtStartMin.Text, "\d{2}") And Regex.IsMatch(txtStartMin.Text, "[0-9]") Then
            _strStartMin = "0" & txtStartMin.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub txtStartSec_TextChanged(sender As Object, e As EventArgs) Handles txtStartSec.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtStartSec.Text) Then
                Convert.ToInt32(txtStartSec.Text)
                _strStartSec = txtStartSec.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        If txtStartSec.Text.Length = txtStartSec.MaxLength Then
            txtEndHr.Focus()
        End If
    End Sub
    Private Sub txtStartSec_LostFocus(sender As Object, e As EventArgs) Handles txtStartSec.LostFocus
        If Not Regex.IsMatch(txtStartSec.Text, "\d{2}") And Regex.IsMatch(txtStartSec.Text, "[0-9]") Then
            _strStartSec = "0" & txtStartSec.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub txtEndHr_TextChanged(sender As Object, e As EventArgs) Handles txtEndHr.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtEndHr.Text) Then
                Convert.ToInt32(txtEndHr.Text)
                _strEndHr = txtEndHr.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        If txtEndHr.Text.Length = txtEndHr.MaxLength Then
            txtEndMin.Focus()
        End If
    End Sub
    Private Sub txtEndHr_LostFocus(sender As Object, e As EventArgs) Handles txtEndHr.LostFocus
        If Not Regex.IsMatch(txtEndHr.Text, "\d{2}") And Regex.IsMatch(txtEndHr.Text, "[0-9]") Then
            _strEndHr = "0" & txtEndHr.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub txtEndMin_TextChanged(sender As Object, e As EventArgs) Handles txtEndMin.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtEndMin.Text) Then
                Convert.ToInt32(txtEndMin.Text)
                _strEndMin = txtEndMin.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        If txtEndMin.Text.Length = txtEndMin.MaxLength Then
            txtEndSec.Focus()
        End If
    End Sub
    Private Sub txtEndMin_LostFocus(sender As Object, e As EventArgs) Handles txtEndMin.LostFocus
        If Not Regex.IsMatch(txtEndMin.Text, "\d{2}") And Regex.IsMatch(txtEndMin.Text, "[0-9]") Then
            _strEndMin = "0" & txtEndMin.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub txtEndSec_TextChanged(sender As Object, e As EventArgs) Handles txtEndSec.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtEndSec.Text) Then
                Convert.ToInt32(txtEndSec.Text)
                _strEndSec = txtEndSec.Text
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
    End Sub
    Private Sub txtEndSec_LostFocus(sender As Object, e As EventArgs) Handles txtEndSec.LostFocus
        If Not Regex.IsMatch(txtEndSec.Text, "\d{2}") And Regex.IsMatch(txtEndSec.Text, "[0-9]") Then
            _strEndSec = "0" & txtEndSec.Text
        End If
        SaveSplitTime()
    End Sub
    Private Sub SaveSplitTime()
        Dim startTime As String = String.Format("{0}:{1}:{2}", _strStartHr, _strStartMin, _strStartSec)
        Dim endTime As String = String.Format("{0}:{1}:{2}", _strEndHr, _strEndMin, _strEndSec)
        My.MySettings.Default.StartTime = startTime
        My.MySettings.Default.EndTime = endTime
        My.MySettings.Default.Save()
    End Sub
#End Region

#End Region

#Region "Video Settings"
    Private Sub ddVideoPreset_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddVideoPreset.Text = String.Format("{0}", "Auto") Then
            ddVideoCodec.Text = String.Format("{0}", "Auto")
            ddVideoFrameRate.Text = String.Format("{0}", "Auto")
            ddVideoBitrate.Text = String.Format("{0}", "Auto")
            ddAspectRatio.Text = String.Format("{0}", "Auto")
            txtWidth.Text = ""
            txtHeight.Text = ""
            ddVideoCodec.Enabled = True
            ddVideoFrameRate.Enabled = True
            ddVideoBitrate.Enabled = True
            ddAspectRatio.Enabled = True
            txtWidth.Enabled = True
            txtHeight.Enabled = True
            My.MySettings.Default.VideoPreset = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoCodec = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoFrameRate = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoBitrate = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoWidth = ""
            My.MySettings.Default.VideoHeight = ""
        ElseIf ddVideoPreset.Text = String.Format("{0}", "SD") Then
            ddVideoCodec.Text = String.Format("{0}", "x264")
            ddVideoFrameRate.Text = String.Format("{0}", "Auto")
            ddVideoBitrate.Text = String.Format("{0}", "1000")
            ddAspectRatio.Text = String.Format("{0}", "Auto")
            txtWidth.Text = String.Format("{0}", "852")
            txtHeight.Text = String.Format("{0}", "480")
            ddVideoCodec.Enabled = False
            ddVideoFrameRate.Enabled = False
            ddVideoBitrate.Enabled = False
            ddAspectRatio.Enabled = False
            txtWidth.Enabled = False
            txtHeight.Enabled = False
            My.MySettings.Default.VideoPreset = "SD"
            My.MySettings.Default.VideoCodec = "x264"
            My.MySettings.Default.VideoFrameRate = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoBitrate = "1000"
            My.MySettings.Default.VideoWidth = "852"
            My.MySettings.Default.VideoHeight = "480"
        ElseIf ddVideoPreset.Text = String.Format("{0}", "720p") Then
            ddVideoCodec.Text = String.Format("{0}", "x264")
            ddVideoFrameRate.Text = String.Format("{0}", "Auto")
            ddVideoBitrate.Text = String.Format("{0}", "2200")
            ddAspectRatio.Text = String.Format("{0}", "Auto")
            txtWidth.Text = String.Format("{0}", "1280")
            txtHeight.Text = String.Format("{0}", "720")
            ddVideoCodec.Enabled = False
            ddVideoFrameRate.Enabled = False
            ddVideoBitrate.Enabled = False
            ddAspectRatio.Enabled = False
            txtWidth.Enabled = False
            txtHeight.Enabled = False
            My.MySettings.Default.VideoPreset = "720p"
            My.MySettings.Default.VideoCodec = "x264"
            My.MySettings.Default.VideoFrameRate = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoBitrate = "2200"
            My.MySettings.Default.VideoWidth = "1280"
            My.MySettings.Default.VideoHeight = "720"
        ElseIf ddVideoPreset.Text = String.Format("{0}", "1080p") Then
            ddVideoCodec.Text = String.Format("{0}", "x264")
            ddVideoFrameRate.Text = String.Format("{0}", "Auto")
            ddVideoBitrate.Text = String.Format("{0}", "2900")
            ddAspectRatio.Text = String.Format("{0}", "Auto")
            txtWidth.Text = String.Format("{0}", "1920")
            txtHeight.Text = String.Format("{0}", "1080")
            ddVideoCodec.Enabled = False
            ddVideoFrameRate.Enabled = False
            ddVideoBitrate.Enabled = False
            ddAspectRatio.Enabled = False
            txtWidth.Enabled = False
            txtHeight.Enabled = False
            My.MySettings.Default.VideoPreset = "1080p"
            My.MySettings.Default.VideoCodec = "x264"
            My.MySettings.Default.VideoFrameRate = String.Format("{0}", "Auto")
            My.MySettings.Default.VideoBitrate = "2900"
            My.MySettings.Default.VideoWidth = "1920"
            My.MySettings.Default.VideoHeight = "1080"
        ElseIf ddVideoPreset.Text = String.Format("{0}", "Custom") Then
            ddVideoCodec.Text = String.Format("{0}", "x264")
            ddVideoCodec.Enabled = True
            ddVideoFrameRate.Enabled = True
            ddVideoBitrate.Enabled = True
            ddAspectRatio.Enabled = True
            txtWidth.Enabled = True
            txtHeight.Enabled = True
            My.MySettings.Default.VideoPreset = "Custom"
            My.MySettings.Default.VideoCodec = "x264"
        End If
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddVideoCodec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddVideoCodec.SelectedIndexChanged
        If ddVideoCodec.Text = String.Format("{0}", "Source") Then
            lblVideoFrameRate.Visible = False
            ddVideoFrameRate.Visible = False
            lblVideoBitrate.Visible = False
            ddVideoBitrate.Visible = False
            lblAspectRatio.Visible = False
            ddAspectRatio.Visible = False
            lblVideoProfile.Visible = False
            ddVideoProfile.Visible = False
            lblVideoLevel.Visible = False
            ddVideoLevel.Visible = False
            lblResolution.Visible = False
            lblX.Visible = False
            txtWidth.Visible = False
            txtHeight.Visible = False
            txtWidth.Text = ""
            txtHeight.Text = ""
            cbCrop.Checked = False
            cbPad.Checked = False
            plCropPad.Visible = False
            My.MySettings.Default.VideoCodec = ddVideoCodec.Text
            My.MySettings.Default.VideoWidth = ""
            My.MySettings.Default.VideoHeight = ""
        Else
            lblVideoFrameRate.Visible = True
            ddVideoFrameRate.Visible = True
            lblVideoBitrate.Visible = True
            ddVideoBitrate.Visible = True
            lblAspectRatio.Visible = True
            ddAspectRatio.Visible = True
            lblVideoProfile.Visible = True
            ddVideoProfile.Visible = True
            lblVideoLevel.Visible = True
            ddVideoLevel.Visible = True
            lblResolution.Visible = True
            lblX.Visible = True
            txtWidth.Visible = True
            txtHeight.Visible = True
            plCropPad.Visible = True
            My.MySettings.Default.VideoCodec = ddVideoCodec.Text
        End If
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddVideoFrameRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddVideoFrameRate.SelectedIndexChanged
        My.MySettings.Default.VideoFrameRate = ddVideoFrameRate.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddVideoBitrate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddVideoBitrate.SelectedIndexChanged
        My.MySettings.Default.VideoBitrate = ddVideoBitrate.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAspectRatio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAspectRatio.SelectedIndexChanged
        My.MySettings.Default.AspectRatio = ddAspectRatio.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbCrop_CheckedChanged(sender As Object, e As EventArgs) Handles cbCrop.CheckedChanged
        If cbCrop.Checked Then
            txtCropTop.Enabled = True
            txtCropBottom.Enabled = True
            txtCropLeft.Enabled = True
            txtCropRight.Enabled = True
        Else
            txtCropTop.Enabled = False
            txtCropBottom.Enabled = False
            txtCropLeft.Enabled = False
            txtCropRight.Enabled = False
        End If
        My.MySettings.Default.VideoCrop = cbCrop.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtCropTop_TextChanged(sender As Object, e As EventArgs) Handles txtCropTop.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtCropTop.Text) Then
                Convert.ToInt32(txtCropTop.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.CropTop = txtCropTop.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtCropBottom_TextChanged(sender As Object, e As EventArgs) Handles txtCropBottom.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtCropBottom.Text) Then
                Convert.ToInt32(txtCropBottom.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.CropBottom = txtCropBottom.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtCropLeft_TextChanged(sender As Object, e As EventArgs) Handles txtCropLeft.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtCropLeft.Text) Then
                Convert.ToInt32(txtCropLeft.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.CropLeft = txtCropLeft.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtCropRight_TextChanged(sender As Object, e As EventArgs) Handles txtCropRight.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtCropRight.Text) Then
                Convert.ToInt32(txtCropRight.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.CropRight = txtCropRight.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbPad_CheckedChanged(sender As Object, e As EventArgs) Handles cbPad.CheckedChanged
        If cbPad.Checked Then
            txtPadTop.Enabled = True
            txtPadBottom.Enabled = True
            txtPadLeft.Enabled = True
            txtPadRight.Enabled = True
        Else
            txtPadTop.Enabled = False
            txtPadBottom.Enabled = False
            txtPadLeft.Enabled = False
            txtPadRight.Enabled = False
        End If
        My.MySettings.Default.VideoPad = cbPad.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtPadTop_TextChanged(sender As Object, e As EventArgs) Handles txtPadTop.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtPadTop.Text) Then
                Convert.ToInt32(txtPadTop.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.PadTop = txtPadTop.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtPadBottom_TextChanged(sender As Object, e As EventArgs) Handles txtPadBottom.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtPadBottom.Text) Then
                Convert.ToInt32(txtPadBottom.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.PadBottom = txtPadBottom.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtPadLeft_TextChanged(sender As Object, e As EventArgs) Handles txtPadLeft.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtPadLeft.Text) Then
                Convert.ToInt32(txtPadLeft.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.PadLeft = txtPadLeft.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtPadRight_TextChanged(sender As Object, e As EventArgs) Handles txtPadRight.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtPadRight.Text) Then
                Convert.ToInt32(txtPadRight.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.PadRight = txtPadRight.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtWidth.Text) Then
                Convert.ToInt32(txtWidth.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.VideoWidth = txtWidth.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub txtHeight_TextChanged(sender As Object, e As EventArgs) Handles txtHeight.TextChanged
        Try
            ' ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            If Not String.IsNullOrEmpty(txtHeight.Text) Then
                Convert.ToInt32(txtHeight.Text)
            End If
        Catch ex As FormatException
            MessageBox.Show(String.Format("{0}", "Not a number"))
        End Try
        My.MySettings.Default.VideoHeight = txtHeight.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddVideoProfile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddVideoProfile.SelectedIndexChanged
        If ddVideoProfile.Text = String.Format("{0}", "Auto") Then
            lblVideoLevel.Visible = False
            ddVideoLevel.Visible = False
        ElseIf ddVideoProfile.Text = String.Format("{0}", "Baseline") Then
            lblVideoLevel.Visible = True
            ddVideoLevel.Visible = True
        ElseIf ddVideoProfile.Text = String.Format("{0}", "Main") Then
            lblVideoLevel.Visible = True
            ddVideoLevel.Visible = True
        ElseIf ddVideoProfile.Text = String.Format("{0}", "High") Then
            lblVideoLevel.Visible = True
            ddVideoLevel.Visible = True
        End If
        My.MySettings.Default.VideoProfile = ddVideoProfile.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddVideoLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddVideoLevel.SelectedIndexChanged
        My.MySettings.Default.VideoLevel = ddVideoLevel.Text
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Audio Settings"
    Private Sub ddAudioCodec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioCodec.SelectedIndexChanged
        If ddAudioCodec.Text = String.Format("{0}", "Source") Then
            lblAudioChannels.Visible = False
            ddAudioChannels.Visible = False
            lblAudioBitrate.Visible = False
            ddAudioBitrate.Visible = False
            lblAudioSampleRate.Visible = False
            ddAudioSampleRate.Visible = False
            My.MySettings.Default.AudioCodec = ddAudioCodec.Text
        Else
            lblAudioChannels.Visible = True
            ddAudioChannels.Visible = True
            lblAudioBitrate.Visible = True
            ddAudioBitrate.Visible = True
            lblAudioSampleRate.Visible = True
            ddAudioSampleRate.Visible = True
            My.MySettings.Default.AudioCodec = ddAudioCodec.Text
        End If
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioCodec2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioCodec2.SelectedIndexChanged
        If ddAudioCodec2.Text = String.Format("{0}", "None") Then
            lblAudioChannels2.Visible = False
            ddAudioChannels2.Visible = False
            lblAudioBitrate2.Visible = False
            ddAudioBitrate2.Visible = False
            lblAudioSampleRate2.Visible = False
            ddAudioSampleRate2.Visible = False
            My.MySettings.Default.AudioCodec2 = ddAudioCodec2.Text
        ElseIf ddAudioCodec2.Text = String.Format("{0}", "Source") Then
            lblAudioChannels2.Visible = False
            ddAudioChannels2.Visible = False
            lblAudioBitrate2.Visible = False
            ddAudioBitrate2.Visible = False
            lblAudioSampleRate2.Visible = False
            ddAudioSampleRate2.Visible = False
            My.MySettings.Default.AudioCodec2 = ddAudioCodec2.Text
        Else
            lblAudioChannels2.Visible = True
            ddAudioChannels2.Visible = True
            lblAudioBitrate2.Visible = True
            ddAudioBitrate2.Visible = True
            lblAudioSampleRate2.Visible = True
            ddAudioSampleRate2.Visible = True
            My.MySettings.Default.AudioCodec2 = ddAudioCodec2.Text
        End If
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioChannels_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioChannels.SelectedIndexChanged
        My.MySettings.Default.AudioChannels = ddAudioChannels.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioChannels2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioChannels2.SelectedIndexChanged
        My.MySettings.Default.AudioChannels2 = ddAudioChannels2.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioBitrate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioBitrate.SelectedIndexChanged
        My.MySettings.Default.AudioBitrate = ddAudioBitrate.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioBitrate2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioBitrate2.SelectedIndexChanged
        My.MySettings.Default.AudioBitrate2 = ddAudioBitrate2.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioSampleRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioSampleRate.SelectedIndexChanged
        My.MySettings.Default.AudioSamplingRate = ddAudioSampleRate.Text
        My.MySettings.Default.Save()
    End Sub
    Private Sub ddAudioSampleRate2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddAudioSampleRate2.SelectedIndexChanged
        My.MySettings.Default.AudioSamplingRate2 = ddAudioSampleRate2.Text
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Subtitle Settings"
    Private Sub ddSubtitleCodec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSubtitleCodec.SelectedIndexChanged
        My.MySettings.Default.SubtitleCodec = ddSubtitleCodec.Text
        My.MySettings.Default.Save()
    End Sub
#End Region

End Class