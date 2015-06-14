Imports System.IO
Imports System.Text
Public NotInheritable Class Encoder
    Implements IDisposable

#Region "Variables" 'depreciated, see Audio/video options above :)
    Private ReadOnly _strAppPath As String = My.Application.Info.DirectoryPath & "\"
    Private _logPath As String

    Private ReadOnly _ffmpeg As String = "" 'Holds the Path of the ffmpeg executable. 
    Private _threads As String = "0" 'Default, Lets FFMpeg decide the number of threads
    Private _videoCodec As String = "copy"
    Private _aCodec As String = "copy" 'Default will be to copy the audio stream.
    Private _aCodec2 As String = "copy" 'Default will be to copy the audio stream.
    Private _subtitleCodec As String = ""
    Private _language As String = ""
    Private _startTime As String = ""
    Private _endTime As String = ""
    Private _container As String
    Private _analyzed As Boolean = False
    Private _totalFrames As Integer
    Private _dualAudio As Boolean = False
    Private _splitFile As Boolean = False
    Private _copySubtitles As Boolean = False
    Private _subtitleLanguage As String = ""
    Private _videoCrop As Boolean = False
    Private _videoPad As Boolean = False
    Private _videoProfile As String
    Private _videoLevel As String

    Private ReadOnly _fFmpegProcessPropertys As New ProcessStartInfo
    ' ReSharper disable once NotAccessedField.Local
    Private _seconds As Integer
    Private _priorityRealTime As Boolean = False
    Private _priorityHigh As Boolean = False
    Private _priorityAboveNormal As Boolean = False
    Private _priorityNormal As Boolean = True
    Private _priorityBelowNormal As Boolean = False
    Private _priorityIdle As Boolean = False
    Private _enableLogging As Boolean = False
    Private ReadOnly _loggingInfo As New StringBuilder()

    'Note: Commmand line will go as follows, 
    '-i <infile> <Custom params ie. -map> <audio options> <video options> <libx264 pre (if needed)> <Thread count> <container> <output>
    Private _source As String
    Private _sourceFileName As String
    Private _joinSource As String
    Private _audioParams As String
    Private _audioParams2 As String
    Private _videoParams As String
    Private _subtitleParams As String
    Private _customParams As String '-map 0:0 -map 0:1 for example
    Private _outputDirectory As String 'ie. d:\encoded files\
    Private _outputFileName As String 'ie.. Encoded.mkv: if one isnt selected one will be created from input name
    Private _commandPass As String
#End Region

#Region "Events"
    ''' Occurs when Progress changes.
    Public Event Progress(progressPercent As Integer, timeRemaining As String)
    ''' Occurs when status changes.
    Public Event Status(status As String)
    ''' Occurs when status finished.
    ' ReSharper disable once EventNeverSubscribedTo.Global
    Public Event Finished(finished As String)
    ''' Occurs when StdError of ffmpeg changes. Usefull for debugging or to display the command line to user.
    ' ReSharper disable once EventNeverSubscribedTo.Global
    Public Event ConsoleOutput(stdOut As String)
#End Region

#Region "Video Codecs"
    Public ReadOnly Vcodec_Copy As String = "copy"
    Public ReadOnly Vcodec_H264 As String = "libx264"
#End Region

#Region "Video Profiles"
    Public ReadOnly VideoProfile_Baseline As String = "Baseline"
    Public ReadOnly VideoProfile_Main As String = "Main"
    Public ReadOnly VideoProfile_High As String = "High"
    Public ReadOnly VideoProfile_None As String = ""
#End Region

#Region "Audio Codecs"
    Public ReadOnly AudioCodec_Copy As String = "copy"
    Public ReadOnly AudioCodec_Mp3 As String = "libmp3lame"
    Public ReadOnly AudioCodec_Ac3 As String = "ac3"
    Public ReadOnly AudioCodec_Aac As String = "aac -strict experimental"
    ' ReSharper disable once UnusedMember.Global
    Public ReadOnly AudioCodec2_Copy As String = "copy"
    ' ReSharper disable once UnusedMember.Global
    Public ReadOnly AudioCodec2_Mp3 As String = "libmp3lame"
    ' ReSharper disable once UnusedMember.Global
    Public ReadOnly AudioCodec2_Ac3 As String = "ac3"
    ' ReSharper disable once UnusedMember.Global
    Public ReadOnly AudioCodec2_Aac As String = "aac -strict experimental"
#End Region

#Region "Subtitle Codecs"
    Public ReadOnly SubtitleCodec_Copy As String = "copy"
    Public ReadOnly SubtitleCodec_Ass As String = "ass"
    Public ReadOnly SubtitleCodec_Srt As String = "srt"
    Public ReadOnly SubtitleCodec_Ssa As String = "ssa"
    Public ReadOnly SubtitleCodec_MovText As String = "mov_text"
    Public ReadOnly SubtitleCodec_None As String = ""
#End Region

#Region "Languages"
    Public ReadOnly Language_English As String = "eng"
    Public ReadOnly Language_Russian As String = "rus"
    Public ReadOnly Language_German As String = "ger"
    Public ReadOnly Language_Chinese As String = "chi"
    Public ReadOnly Language_Czech As String = "cze"
#End Region

#Region "Containers"
    Public ReadOnly Format_Mkv As String = "matroska" 'for .mkv extension
    Public ReadOnly Format_Flv As String = "flv"
    Public ReadOnly Format_Mov As String = "mov"
    Public ReadOnly Format_Mp4 As String = "mp4"
    Public ReadOnly Format_Avi As String = "avi"
    Public ReadOnly Format_Mp3 As String = "mp3" 'Note: Audio only
    Public ReadOnly Format_Aac As String = "aac" 'NOTE: Audio only
#End Region

#Region "Video Options"
    Private _videoFrameRate As String
    Private _videoBitrate As String
    Private _aspectRatio As String
    Private _videoWidth As String
    Private _videoHeight As String
    Private _cropTop As String
    Private _cropBottom As String
    Private _cropLeft As String
    Private _cropRight As String
    Private _padTop As String
    Private _padBottom As String
    Private _padLeft As String
    Private _padRight As String
#End Region

#Region "Audio Options"
    Private _audioChannels As String 'sets the audio channels.. -ac 2
    Private _audioBitRate As String 'sets the audio bitrate.. -ab 128k
    Private _aFreq As String 'sets the audio sampling frequency.. -ar 48000
    Private _audioChannels2 As String 'sets the audio channels.. -ac 2
    Private _audioBitRate2 As String 'sets the audio bitrate.. -ab 128k
    Private _aFreq2 As String 'sets the audio sampling frequency.. -ar 48000
#End Region

#Region "File Types"
    ReadOnly _audioFileTypes As New ArrayList
    ' ReSharper disable once UnusedMember.Local
    Private Sub AddAudioFiles()
        _audioFileTypes.Add(".mp3")
        _audioFileTypes.Add(".wmv")
        _audioFileTypes.Add(".flac")
        _audioFileTypes.Add(".aac")
        _audioFileTypes.Add(".ac3")
        _audioFileTypes.Add(".aif")
        _audioFileTypes.Add(".at3")
        _audioFileTypes.Add(".ogg")
        _audioFileTypes.Add(".wav")
    End Sub
#End Region

#Region "Private Methods"
    Private Sub SetupVideo()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine("Setting video track settings:")
        If _videoCodec = "copy" Then
            _videoParams = String.Format("-c:v copy -metadata:s:v language={0}", _language)
        ElseIf _videoCodec = "none" Then
            _videoParams = "-vn"
        Else
            _videoParams = String.Format("-c:v {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} -metadata:s:v language={14}", _videoCodec, FVideoFrameRate, FVideoBitrate, FAspectRatio, FVideoResolution, FCropTop, FCropBottom, FCropLeft, FCropRight, FPadTop, FPadBottom, FPadLeft, FPadRight, FVideoProfile, _language)
        End If
        _loggingInfo.AppendLine(_videoParams)
    End Sub
    Private Sub SetupAudio()
        If _dualAudio Then
            _loggingInfo.AppendLine()
            _loggingInfo.AppendLine("Setting audio track 1 settings:")
            If _aCodec = "copy" Then
                _audioParams = String.Format("-c:a:0 copy -metadata:s:a:0 language={0}", _language)
            Else
                _audioParams = String.Format("{0} {1} {2} {3} -metadata:s:a:0 language={4}", FAudiocodec, FAudioChannels, FAudioBitrate, FAudioFrequency, _language)
            End If
        Else
            _loggingInfo.AppendLine()
            _loggingInfo.AppendLine("Setting audio track settings:")
            If _aCodec = "copy" Then
                _audioParams = String.Format("-c:a copy -metadata:s:a language={0}", _language)
            Else
                _audioParams = String.Format("{0} {1} {2} {3} -metadata:s:a language={4}", FAudiocodec, FAudioChannels, FAudioBitrate, FAudioFrequency, _language)
            End If
        End If
        _loggingInfo.AppendLine(_audioParams)
    End Sub
    Private Sub SetupAudio2()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine("Setting audio track 2 settings:")
        If _aCodec2 = "copy" Then
            _audioParams2 = String.Format("-c:a:1 copy -metadata:s:a:1 language={0}", _language)
        Else
            _audioParams2 = String.Format("{0} {1} {2} {3} -metadata:s:a:1 language={4}", FAudiocodec2, FAudioBitrate2, FAudioFrequency2, FAudioChannels2, _language)
        End If
        _loggingInfo.AppendLine(_audioParams2)
    End Sub
    Private Sub SetupSubtitle()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine("Setting subtitle track settings:")
        If _copySubtitles Then
            If _subtitleCodec = "copy" Then
                If _subtitleLanguage = "Source" Then
                    _subtitleParams = "-c:s copy"
                Else
                    _subtitleParams = String.Format("-c:s copy -metadata:s:s language={0}", _subtitleLanguage)
                End If
            ElseIf _subtitleCodec = "none" Then
                _subtitleParams = "-sn"
            Else
                If _subtitleLanguage = "Source" Then
                    _subtitleParams = String.Format("-c:s {0}", _subtitleCodec)
                Else
                    _subtitleParams = String.Format("-c:s {0} -metadata:s:s language={1}", _subtitleCodec, _subtitleLanguage)
                End If
            End If
        Else
            _subtitleParams = "-sn"
        End If
        _loggingInfo.AppendLine(_subtitleParams)
    End Sub
    Private Sub SetupCommandPass()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine("Setting up ffmpeg command line:")
        If _splitFile Then
            If _dualAudio Then
                _commandPass = String.Format("-y -i {0} {1} {2} {3} {4} {5} {6} -ss {7} -to {8} {9}{10}", _source, _customParams, _videoParams, _audioParams, _audioParams2, _subtitleParams, "-threads " & _threads, _startTime, _endTime, """" & _outputDirectory, _outputFileName & """")
            Else
                _commandPass = String.Format("-y -i {0} {1} {2} {3} {4} -ss {5} -to {6} {7}{8}", _source, _videoParams, _audioParams, _subtitleParams, "-threads " & _threads, _startTime, _endTime, """" & _outputDirectory, _outputFileName & """")
            End If
        Else
            If _dualAudio Then
                _commandPass = String.Format("-y -i {0} {1} {2} {3} {4} {5} {6} {7}{8}", _source, _customParams, _videoParams, _audioParams, _audioParams2, _subtitleParams, "-threads " & _threads, """" & _outputDirectory, _outputFileName & """")
            Else
                _commandPass = String.Format("-y -i {0} {1} {2} {3} {4} {5}{6}", _source, _videoParams, _audioParams, _subtitleParams, "-threads " & _threads, """" & _outputDirectory, _outputFileName & """")
            End If
        End If
        _loggingInfo.AppendLine(_commandPass)
    End Sub
    Private Sub SetupJoinCommandPass()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine("Setting up ffmpeg command line:")
        _commandPass = String.Format("-y -f concat -i {0} {1} {2} {3}{4}", _joinSource, _videoParams, _audioParams, """" & _outputDirectory, _outputFileName & """")
        _loggingInfo.AppendLine(_commandPass)
    End Sub
    Private Sub RunEncode(argument As String)
        Try
            _fFmpegProcessPropertys.Arguments = argument
            _fFmpegProcessPropertys.UseShellExecute = False
            _fFmpegProcessPropertys.RedirectStandardOutput = True
            _fFmpegProcessPropertys.RedirectStandardError = True
            _fFmpegProcessPropertys.CreateNoWindow = True
            RaiseEvent Finished("No")
            Dim fFmpegProcess = Process.Start(_fFmpegProcessPropertys)

            If _priorityRealTime Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.RealTime
            ElseIf _priorityHigh Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.High
            ElseIf _priorityAboveNormal Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.AboveNormal
            ElseIf _priorityNormal Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.Normal
            ElseIf _priorityBelowNormal Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.BelowNormal
            ElseIf _priorityIdle Then
                fFmpegProcess.PriorityClass = ProcessPriorityClass.Idle
            Else
                fFmpegProcess.PriorityClass = ProcessPriorityClass.Normal
            End If

            Dim readerStdOut As StreamReader = fFmpegProcess.StandardError
            Dim sOutput As String
            While readerStdOut.EndOfStream = False
                sOutput = readerStdOut.ReadLine()
                _loggingInfo.AppendLine(".." & sOutput & "..")
                RaiseEvent ConsoleOutput(sOutput)
                Dim percent As Integer = GetPercent(sOutput)
                RaiseEvent Progress(percent, Timeleft(sOutput))
            End While
            fFmpegProcess.WaitForExit()
            If fFmpegProcess.HasExited = True Then
                RaiseEvent Finished("Yes")
            End If
            fFmpegProcess.Close()
        Catch ex As Exception
            Dim errorWriter As New StreamWriter(String.Format("{0}\fflib_error_log.txt", _logPath), True)
            errorWriter.WriteLine(ex.ToString)
            errorWriter.Flush()
            errorWriter.Close()
            errorWriter.Dispose()
        End Try
    End Sub
#End Region

#Region "Private Functions"
    Private Function FVideoFrameRate() As String
        If _videoFrameRate = Nothing Then
            Return ""
        Else
            Return String.Format("-r {0}", _videoFrameRate)
        End If
    End Function
    Private Function FVideoBitrate() As String
        If _videoBitrate = Nothing Then
            Return ""
        Else
            Return String.Format("-b {0}000", _videoBitrate)
        End If
    End Function
    Private Function FAspectRatio() As String
        If _aspectRatio = Nothing Then
            Return ""
        Else
            Return String.Format("-aspect {0}", _aspectRatio)
        End If
    End Function
    Private Function FCropTop() As String
        If _videoCrop Then
            If _cropTop = Nothing Then
                Return ""
            Else
                Return String.Format("-croptop {0}", _cropTop)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FCropBottom() As String
        If _videoCrop Then
            If _cropBottom = Nothing Then
                Return ""
            Else
                Return String.Format("-cropbottom {0}", _cropBottom)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FCropLeft() As String
        If _videoCrop Then
            If _cropLeft = Nothing Then
                Return ""
            Else
                Return String.Format("-cropleft {0}", _cropLeft)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FCropRight() As String
        If _videoCrop Then
            If _cropRight = Nothing Then
                Return ""
            Else
                Return String.Format("-cropright {0}", _cropRight)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FPadTop() As String
        If _videoPad Then
            If _padTop = Nothing Then
                Return ""
            Else
                Return String.Format("-padtop {0}", _padTop)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FPadBottom() As String
        If _videoPad Then
            If _padBottom = Nothing Then
                Return ""
            Else
                Return String.Format("-padbottom {0}", _padBottom)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FPadLeft() As String
        If _videoPad Then
            If _padLeft = Nothing Then
                Return ""
            Else
                Return String.Format("-padleft {0}", _padLeft)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FPadRight() As String
        If _videoPad Then
            If _padRight = Nothing Then
                Return ""
            Else
                Return String.Format("-padright {0}", _padRight)
            End If
        Else
            Return ""
        End If
    End Function
    Private Function FVideoResolution() As String
        If _videoWidth = Nothing Or _videoHeight = Nothing Then
            Return ""
        Else
            Return String.Format("-vf scale={0}:{1}", _videoWidth, _videoHeight)
        End If
    End Function
    Private Function FVideoProfile() As String
        If _videoProfile = Nothing Then
            Return ""
        Else
            Return String.Format("-profile:v {0} -level {1}", _videoProfile, _videoLevel)
        End If
    End Function
    Private Function FAudiocodec() As String
        If _dualAudio Then
            Return String.Format("-c:a:0 {0}", _aCodec)
        Else
            Return String.Format("-c:a {0}", _aCodec)
        End If
    End Function
    Private Function FAudioChannels() As String
        If _audioChannels = Nothing Then
            Return ""
        Else
            If _dualAudio Then
                Return String.Format("-ac:a:0 {0}", _audioChannels)
            Else
                Return String.Format("-ac {0}", _audioChannels)
            End If
        End If
    End Function
    Private Function FAudioBitrate() As String
        If _audioBitRate = Nothing Then
            Return ""
        Else
            If _dualAudio Then
                Return String.Format("-ab:a:0 {0}", _audioBitRate)
            Else
                Return String.Format("-ab {0}", _audioBitRate)
            End If
        End If
    End Function
    Private Function FAudioFrequency() As String
        If _aFreq = Nothing Then
            Return ""
        Else
            If _dualAudio Then
                Return String.Format("-ar:a:0 {0}", _aFreq)
            Else
                Return String.Format("-ar {0}", _aFreq)
            End If
        End If
    End Function
    Private Function FAudiocodec2() As String
        Return String.Format("-c:a:1 {0}", _aCodec2)
    End Function
    Private Function FAudioChannels2() As String
        If _audioChannels2 = Nothing Then
            Return ""
        Else
            Return String.Format("-ac:a:1 {0}", _audioChannels2)
        End If
    End Function
    Private Function FAudioBitrate2() As String
        If _audioBitRate2 = Nothing Then
            Return ""
        Else
            Return String.Format("-ab:a:1 {0}", _audioBitRate2)
        End If
    End Function
    Private Function FAudioFrequency2() As String
        If _aFreq2 = Nothing Then
            Return ""
        Else
            Return String.Format("-ar:a:1 {0}", _aFreq2)
        End If
    End Function
    Private Function QuickAnalyze() As Boolean
        Dim infoprocess As New ProcessStartInfo
        infoprocess.Arguments = String.Format("-i {0}", _source) '-an -vcodec copy -f rawvideo -y null"
        infoprocess.UseShellExecute = False
        infoprocess.RedirectStandardError = True
        infoprocess.CreateNoWindow = True
        infoprocess.FileName = _ffmpeg
        Dim fFmpegProcess = Process.Start(infoprocess)
        Dim readerStdOut As StreamReader = fFmpegProcess.StandardError
        Dim sOutput As String
        Dim hours As Integer
        Dim minutes As Integer
        Dim seconds As Integer
        While readerStdOut.EndOfStream = False
            sOutput = readerStdOut.ReadLine
            If sOutput.Contains("max_analyze_duration") = True Then
                fFmpegProcess.Close()
                Return False 'ie, we werent able to perform a quick info analisys.
            Else
                If sOutput.Contains("Duration:") Then 'We want time to determain progress.
                    Dim split1 As String() = sOutput.Split(New [Char]() {" ", ","})
                    Dim string1 As String = split1(3)
                    Dim times As String() = string1.Split(New Char() {":"c})
                    Try
                        hours = times(0)
                        minutes = times(1)
                        seconds = times(2)
                        hours = hours * 60
                        minutes = minutes + hours
                        minutes = minutes * 60
                        _seconds = seconds + minutes
                    Catch ex As Exception
                        fFmpegProcess.Close()
                        Return False
                    End Try
                End If
                Dim fps As String
                If sOutput.Contains(" fps,") Then 'We want fps, so we can determain total frames so we can get ETA of completion. 
                    Try
                        Dim split1 As String() = sOutput.Split(New [Char]() {" ", ","})
                        Dim s1 As String = split1(20)
                        fps = s1
                    Catch ex As Exception
                        fFmpegProcess.Close()
                        Return False
                    End Try
                Else
                    fFmpegProcess.Close()
                    Return False
                End If
                'Encase in a try/catch, just in case FPS pulled from the console is something besides a number.. :)
                Try
                    _totalFrames = seconds * fps
                Catch ex As Exception
                    fFmpegProcess.Close()
                    Return False
                End Try
            End If
        End While
        fFmpegProcess.WaitForExit()
        fFmpegProcess.Close()
        Return True
    End Function
    Private Function SlowAnalyze() As Boolean
        Dim infoprocess As New ProcessStartInfo
        infoprocess.Arguments = String.Format("-i {0} -an -c:v copy -f rawvideo -y nul", _source)
        infoprocess.UseShellExecute = False
        infoprocess.RedirectStandardError = True
        infoprocess.CreateNoWindow = True
        infoprocess.FileName = _ffmpeg
        Dim fFmpegProcess = Process.Start(infoprocess)
        Dim readerStdOut As StreamReader = fFmpegProcess.StandardError
        Dim sOutput As String
        Dim frame = ""
        Dim time = ""
        While readerStdOut.EndOfStream = False
            sOutput = readerStdOut.ReadLine()
            RaiseEvent ConsoleOutput(sOutput)
            If sOutput.Contains("frame=") Then
                Try
                    Dim outLine As String() = sOutput.Split(New Char() {"="})
                    Dim chunk1 As String() = outLine(1).Split(New Char() {" "}) 'Frame
                    ' ReSharper disable once UnusedVariable
                    Dim chunk2 As String() = outLine(2).Split(New Char() {" "}) 'fps (speed of encoding, NOT the actual frame rate)
                    ' ReSharper disable once UnusedVariable
                    Dim chunk3 As String() = outLine(3).Split(New Char() {" "}) 'qfactor
                    ' ReSharper disable once UnusedVariable
                    Dim chunk4 As String() = outLine(4).Split(New Char() {" "}) 'size
                    Dim chunk5 As String() = outLine(5).Split(New Char() {" "}) 'time
                    ' ReSharper disable once UnusedVariable
                    Dim chunk6 As String() = outLine(6).Split(New Char() {" "}) 'bitrate
                    Dim timeHit = False
                    Dim frameHit = False
                    For index = 0 To chunk1.Count - 1 'should account for enuff spaces!
                        If chunk1(index) <> "" And frameHit = False Then
                            frame = chunk1(index)
                            frameHit = True
                        End If
                    Next
                    For index = 0 To chunk5.Count - 1 'should account for enuff spaces!
                        If chunk5(index) <> "" And timeHit = False Then
                            time = chunk5(index)
                            timeHit = True
                        End If
                    Next
                Catch ex As Exception
                    'do nothing, prolly an audio file. Besides, all a failure here means is no progress, lol. 
                    'basicly, the progress will have to be intermanate. 
                    fFmpegProcess.Kill()
                    fFmpegProcess.Close()
                    Return False
                End Try
            End If
        End While
        If IsNumeric(time) = True Then
            _seconds = time
        End If
        If IsNumeric(frame) = True Then
            _totalFrames = frame
        End If

        fFmpegProcess.WaitForExit()
        fFmpegProcess.Close()
        Return True
    End Function
    Private Function SourceExists() As Boolean
        If My.Computer.FileSystem.FileExists(_source.Replace("""", "")) = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function IsVideo() As Boolean
        'first, parse the file extension from the input file, check if its on the audio list..
        Dim fileExt As String = My.Computer.FileSystem.GetFileInfo(_source.Trim("""", "")).Extension.ToLower
        If _audioFileTypes.Contains(fileExt) = True Then
            Return False
        Else : Return True
        End If
    End Function
    Private Function GetFileName() As String
        'NOTE: Even tho ffmpeg does this for you, we will do it ourselfs, we may remove it later. who knows.
        'This will run and check if user entered in a custom filename. ie, encoded.mkv
        'if not, then we will create on based on the container(if set) OR the codec
        Dim sourceExt As String = My.Computer.FileSystem.GetFileInfo(_source.Trim("""")).Extension
        Dim sourceName As String = My.Computer.FileSystem.GetFileInfo(_source.Trim("""")).Name.Replace(sourceExt, "")
        'We now have JUST the name of the source file, with No extension, now we need to add an extension based on
        'the codec, or preferably the container if it was set..
        If _outputFileName Is Nothing Then 'User didnt specify a custom filename, lets create one.
            'BTW, we are gonna do somethin i HATE doing, which is alotta nested directions. so lets get focused.
            If _container Is Nothing Then
                'generate file based on codec!
                'Note, we must check for audio only! 
                If IsVideo() = True Then
                    'check the video containers.
                    Select Case _videoCodec
                        Case Vcodec_Copy
                            'We shoulda checked this before. But the in and outfile can stay the same
                            'I hope the user CHANGED the output directory to something different! haha
                            Return sourceName & sourceExt
                        Case Vcodec_H264
                            Return String.Format("{0}.mkv", sourceName)
                            'add them. otherwise, just do a CASE ELSE, and add a default like avi or mp4. 
                    End Select
                Else
                    'check the audio containers.
                    Select Case _aCodec
                        Case AudioCodec_Aac
                            Return String.Format("{0}.aac", sourceName)
                        Case AudioCodec_Copy
                            Return String.Format("{0}{1}", sourceName, sourceExt)
                        Case AudioCodec_Ac3
                            Return String.Format("{0}.ac3", sourceName)
                        Case AudioCodec_Mp3
                            Return String.Format("{0}.mp3", sourceName)
                    End Select
                End If
            Else
                'generate file based on container(which is preferable)
                Select Case _container
                    Case Format_Aac
                        Return String.Format("{0}.aac", sourceName)
                    Case Format_Avi
                        Return String.Format("{0}.avi", sourceName)
                    Case Format_Mkv
                        Return String.Format("{0}.mkv", sourceName)
                    Case Format_Flv
                        Return String.Format("{0}.flv", sourceName)
                    Case Format_Mov
                        Return String.Format("{0}.mov", sourceName)
                    Case Format_Mp3
                        Return String.Format("{0}.mp3", sourceName)
                    Case Format_Mp4
                        Return String.Format("{0}.mp4", sourceName)
                End Select
            End If
            Return sourceName
        Else 'then return what the user entered in for the filename. 
            Return _outputFileName
        End If
    End Function
    Private Function GetPercent(cout As String) As Integer
        Dim frame = 0
        If cout.Contains("frame=") Then
            Try
                Dim outLine As String() = cout.Split(New Char() {"="})
                Dim chunk1 As String() = outLine(1).Split(New Char() {" "}) 'Frame
                ' ReSharper disable once UnusedVariable
                Dim chunk2 As String() = outLine(2).Split(New Char() {" "}) 'fps (speed of encoding, NOT the actual frame rate)
                ' ReSharper disable once UnusedVariable
                Dim chunk3 As String() = outLine(3).Split(New Char() {" "}) 'qfactor
                ' ReSharper disable once UnusedVariable
                Dim chunk4 As String() = outLine(4).Split(New Char() {" "}) 'size
                ' ReSharper disable once UnusedVariable
                Dim chunk5 As String() = outLine(5).Split(New Char() {" "}) 'time
                ' ReSharper disable once UnusedVariable
                Dim chunk6 As String() = outLine(6).Split(New Char() {" "}) 'bitrate
                'the above chunks had to be split. 
                'the ones we care about are FRAME AND TIME: Now, because on short files
                'there may be many spaces, ie frames=     32 is different than frames=  3343 <--note less spaces.
                Dim frameHit = False

                For index = 0 To chunk1.Count - 1 'should account for enuff spaces!
                    If chunk1(index) <> "" And frameHit = False Then
                        frame = chunk1(index)
                        frameHit = True
                    End If
                Next
                Return frame / _totalFrames * 100
            Catch ex As Exception
            End Try
        End If
        Return Nothing
    End Function
    ' ReSharper disable once UnusedParameter.Local
    Private Function Timeleft(cout As String) As String
        Return 0
    End Function
#End Region

#Region "Constructors"
    Sub New()
        _ffmpeg = String.Format("{0}ffmpeg\ffmpeg.exe", _strAppPath)
        _fFmpegProcessPropertys.FileName = _ffmpeg
    End Sub
    ' ReSharper disable once UnusedMember.Global
    Sub New(fFmpeg As String)
        _ffmpeg = fFmpeg
        _fFmpegProcessPropertys.FileName = _ffmpeg
    End Sub
#End Region

#Region "Public Methods"
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub AnalyzeFile()
        'this may be called explicitly, or it should run automaticly before the encoder is ran
        'It can be be called so if the user wanted to analyze files as added ect ect..
        'Also, we need to check to see if its a video file, because we wont analyze audio files. no point.
        If IsVideo() = True Then
            If _analyzed = False Then
                RaiseEvent Status("Analayzing ")
                If QuickAnalyze() = False Then 'Meaning Couldn't get the info by doing a quick analyze
                    If SlowAnalyze() = False Then
                    End If
                End If
                _analyzed = True
                If _enableLogging Then
                    _loggingInfo.AppendLine(String.Format("Analayzing {0}.................................", _sourceFileName))
                End If
            End If
        End If

    End Sub
    Public Sub Encode()
        If SourceExists() = True Then 'else do nothing, will error out anyways and again, we will check when we analyze
            RaiseEvent Status("Setting up encoder")
            _loggingInfo.Clear()
            _loggingInfo.AppendLine(".................................Setting up encoder.....................................")
            AnalyzeFile()
            If String.IsNullOrEmpty(_outputFileName) Then
                _outputFileName = GetFileName()
            End If
            SetupVideo()
            SetupAudio()
            If _dualAudio Then
                SetupAudio2()
            End If
            SetupSubtitle()
            RaiseEvent Status("Encoding ")
            SetupCommandPass()
            _loggingInfo.AppendLine()
            _loggingInfo.AppendLine(String.Format("Encoding {0}.................................", _sourceFileName))
            _loggingInfo.AppendLine()
            _loggingInfo.AppendLine(".................................Data Output Start.....................................")
            RunEncode(_commandPass)
        End If
        _loggingInfo.AppendLine(".................................Data Output End.....................................")
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine(String.Format("Finished Encoding {0}.................................", _sourceFileName))
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine()
        If _enableLogging Then
            Try
                Dim writer As New StreamWriter(String.Format("{0}\log.txt", _logPath), True)
                writer.WriteLine(_loggingInfo.ToString)
                writer.Flush()
                writer.Close()
                writer.Dispose()
            Catch ex As Exception
            End Try
        End If
        RaiseEvent Status("Finished Encoding ")
    End Sub
    Public Sub JoinVideos()
        RaiseEvent Status("Preparing to join files")
        _loggingInfo.Clear()
        _loggingInfo.AppendLine(".................................Preparing to join files.....................................")
        If String.IsNullOrEmpty(_outputFileName) Then
            _outputFileName = GetFileName()
        End If
        SetupVideo()
        SetupAudio()
        If _dualAudio Then
            SetupAudio2()
        End If
        SetupSubtitle()
        RaiseEvent Status("Joining ")
        SetupJoinCommandPass()
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine(String.Format("Joining {0}.................................", _sourceFileName))
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine(".................................Data Output Start.....................................")
        RunEncode(_commandPass)
        _loggingInfo.AppendLine(".................................Data Output End.....................................")
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine(String.Format("Finished Joining {0}.................................", _outputFileName))
        _loggingInfo.AppendLine()
        _loggingInfo.AppendLine()
        If _enableLogging Then
            Try
                Dim writer As New StreamWriter(String.Format("{0}\log.txt", _logPath), True)
                writer.WriteLine(_loggingInfo.ToString)
                writer.Flush()
                writer.Close()
                writer.Dispose()
            Catch ex As Exception
            End Try
        End If
        RaiseEvent Status("Finished Joining ")
    End Sub
#End Region

#Region "Properties"
    Public Property SourceFile As String
        Get
            Return _source
        End Get
        Set(value As String)
            _source = String.Format("""{0}""", value)
            _analyzed = False
            _outputFileName = Nothing
        End Set
    End Property
    Public Property SourceFileName As String
        Get
            Return _sourceFileName
        End Get
        Set(value As String)
            _sourceFileName = value
        End Set
    End Property
    Public Property JoinSourceFile As String
        Get
            Return _joinSource
        End Get
        Set(value As String)
            _joinSource = String.Format("""{0}""", value)
        End Set
    End Property
    Public Property Format As String
        Get
            Return _container
        End Get
        Set(value As String)
            _container = value
        End Set
    End Property
    Public Property VideoCodec As String
        Get
            Return _videoCodec
        End Get
        Set(value As String)
            _videoCodec = value
        End Set
    End Property
    Public Property VideoFrameRate As String
        Get
            Return _videoFrameRate
        End Get
        Set(value As String)
            _videoFrameRate = value
        End Set
    End Property
    Public Property VideoBitrate As String
        Get
            Return _videoBitrate
        End Get
        Set(value As String)
            _videoBitrate = value
        End Set
    End Property
    Public Property AspectRatio As String
        Get
            Return _aspectRatio
        End Get
        Set(value As String)
            _aspectRatio = value
        End Set
    End Property
    Public Property VideoWidth As String
        Get
            Return _videoWidth
        End Get
        Set(value As String)
            _videoWidth = value
        End Set
    End Property
    Public Property VideoHeight As String
        Get
            Return _videoHeight
        End Get
        Set(value As String)
            _videoHeight = value
        End Set
    End Property
    Public Property CropTop As String
        Get
            Return _cropTop
        End Get
        Set(value As String)
            _cropTop = value
        End Set
    End Property
    Public Property CropBottom As String
        Get
            Return _cropBottom
        End Get
        Set(value As String)
            _cropBottom = value
        End Set
    End Property
    Public Property CropLeft As String
        Get
            Return _cropLeft
        End Get
        Set(value As String)
            _cropLeft = value
        End Set
    End Property
    Public Property CropRight As String
        Get
            Return _cropRight
        End Get
        Set(value As String)
            _cropRight = value
        End Set
    End Property
    Public Property PadTop As String
        Get
            Return _padTop
        End Get
        Set(value As String)
            _padTop = value
        End Set
    End Property
    Public Property PadBottom As String
        Get
            Return _padBottom
        End Get
        Set(value As String)
            _padBottom = value
        End Set
    End Property
    Public Property PadLeft As String
        Get
            Return _padLeft
        End Get
        Set(value As String)
            _padLeft = value
        End Set
    End Property
    Public Property PadRight As String
        Get
            Return _padRight
        End Get
        Set(value As String)
            _padRight = value
        End Set
    End Property
    Public Property AudioCodec As String
        Get
            Return _aCodec
        End Get
        Set(value As String)
            _aCodec = value
        End Set
    End Property
    Public Property AudioChannels As String
        Get
            Return _audioChannels
        End Get
        Set(value As String)
            _audioChannels = value
        End Set
    End Property
    Public Property AudioBitrate As String
        Get
            Return _audioBitRate
        End Get
        Set(value As String)
            _audioBitRate = value
        End Set
    End Property
    Public Property AudioSamplingRate As String
        Get
            Return _aFreq
        End Get
        Set(value As String)
            _aFreq = value
        End Set
    End Property
    Public Property AudioCodec2 As String
        Get
            Return _aCodec2
        End Get
        Set(value As String)
            _aCodec2 = value
        End Set
    End Property
    Public Property AudioChannels2 As String
        Get
            Return _audioChannels2
        End Get
        Set(value As String)
            _audioChannels2 = value
        End Set
    End Property
    Public Property AudioBitrate2 As String
        Get
            Return _audioBitRate2
        End Get
        Set(value As String)
            _audioBitRate2 = value
        End Set
    End Property
    Public Property AudioSamplingRate2 As String
        Get
            Return _aFreq2
        End Get
        Set(value As String)
            _aFreq2 = value
        End Set
    End Property
    Public Property SubtitleCodec As String
        Get
            Return _subtitleCodec
        End Get
        Set(value As String)
            _subtitleCodec = value
        End Set
    End Property
    Public Property Threads As String
        Get
            Return _threads
        End Get
        Set(value As String)
            _threads = value
        End Set
    End Property
    Public Property OutputPath As String
        Get
            Return _outputDirectory
        End Get
        Set(value As String)
            If value.EndsWith("\") Then
                If My.Computer.FileSystem.DirectoryExists(value) = False Then
                    My.Computer.FileSystem.CreateDirectory(value)
                End If
                _outputDirectory = value
            Else
                If My.Computer.FileSystem.DirectoryExists(String.Format("{0}\", value)) = False Then
                    My.Computer.FileSystem.CreateDirectory(String.Format("{0}\", value))
                End If
                _outputDirectory = String.Format("{0}\", value)
            End If

        End Set
    End Property
    Public Property OutputFileName As String
        Get
            Return _outputFileName
        End Get
        Set(value As String)
            _outputFileName = value
        End Set
    End Property
    Public Property CustomParams As String
        Get
            Return _customParams
        End Get
        Set(value As String)
            _customParams = value
        End Set
    End Property
    Public Property Language As String
        Get
            Return _language
        End Get
        Set(value As String)
            _language = value
        End Set
    End Property
    Public Property DualAudio As Boolean
        Get
            Return _dualAudio
        End Get
        Set(value As Boolean)
            _dualAudio = value
        End Set
    End Property
    Public Property SplitFile As Boolean
        Get
            Return _splitFile
        End Get
        Set(value As Boolean)
            _splitFile = value
        End Set
    End Property
    Public Property CopySubtitles As Boolean
        Get
            Return _copySubtitles
        End Get
        Set(value As Boolean)
            _copySubtitles = value
        End Set
    End Property
    Public Property SubtitleLanguage As String
        Get
            Return _subtitleLanguage
        End Get
        Set(value As String)
            _subtitleLanguage = value
        End Set
    End Property
    Public Property VideoCrop As Boolean
        Get
            Return _videoCrop
        End Get
        Set(value As Boolean)
            _videoCrop = value
        End Set
    End Property
    Public Property VideoPad As Boolean
        Get
            Return _videoPad
        End Get
        Set(value As Boolean)
            _videoPad = value
        End Set
    End Property
    Public Property VideoProfile As String
        Get
            Return _videoProfile
        End Get
        Set(value As String)
            _videoProfile = value
        End Set
    End Property
    Public Property VideoLevel As String
        Get
            Return _videoLevel
        End Get
        Set(value As String)
            _videoLevel = value
        End Set
    End Property
    Public Property StartTime As String
        Get
            Return _startTime
        End Get
        Set(value As String)
            _startTime = value
        End Set
    End Property
    Public Property EndTime As String
        Get
            Return _endTime
        End Get
        Set(value As String)
            _endTime = value
        End Set
    End Property
    Public Property PriorityRealTime As Boolean
        Get
            Return _priorityRealTime
        End Get
        Set(value As Boolean)
            _priorityRealTime = value
        End Set
    End Property
    Public Property PriorityHigh As Boolean
        Get
            Return _priorityHigh
        End Get
        Set(value As Boolean)
            _priorityHigh = value
        End Set
    End Property
    Public Property PriorityAboveNormal As Boolean
        Get
            Return _priorityAboveNormal
        End Get
        Set(value As Boolean)
            _priorityAboveNormal = value
        End Set
    End Property
    Public Property PriorityNormal As Boolean
        Get
            Return _priorityNormal
        End Get
        Set(value As Boolean)
            _priorityNormal = value
        End Set
    End Property
    Public Property PriorityBelowNormal As Boolean
        Get
            Return _priorityBelowNormal
        End Get
        Set(value As Boolean)
            _priorityBelowNormal = value
        End Set
    End Property
    Public Property PriorityIdle As Boolean
        Get
            Return _priorityIdle
        End Get
        Set(value As Boolean)
            _priorityIdle = value
        End Set
    End Property
    Public Property EnableLogging As Boolean
        Get
            Return _enableLogging
        End Get
        Set(value As Boolean)
            _enableLogging = value
        End Set
    End Property
    Public Property LogPath As String
        Get
            Return _logPath
        End Get
        Set(value As String)
            If My.Computer.FileSystem.DirectoryExists(value) = False Then
                My.Computer.FileSystem.CreateDirectory(value)
            End If
            _logPath = value
        End Set
    End Property
#End Region

#Region "IDisposable Support"
    Private _disposedValue As Boolean ' To detect redundant calls
    Private Sub Dispose(disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
            End If
        End If
        _disposedValue = True
    End Sub
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
