' ReSharper disable once ClassNeverInstantiated.Global
Public Class EncoderSettings

#Region "Variables"
    Private Shared _tempOutputPath As String
    Private Shared _outputPath As String
    Private Shared _outputFileName As String
    Private Shared _videoCodec As String
    Private Shared _videoWidth As String
    Private Shared _videoHeight As String
    Private Shared _audioCodec As String
    Private Shared _audioCodec2 As String
    Private Shared _audioChannels As String
    Private Shared _audioChannels2 As String
    Private Shared _audioBitrate As String
    Private Shared _audioBitrate2 As String
    Private Shared _audioSamplingRate As String
    Private Shared _audioSamplingRate2 As String
    Private Shared _subtitleCodec As String
    Private Shared _dualAudio As Boolean
    Private Shared _copySubtitles As Boolean
    Private Shared _isActive As Boolean
#End Region

#Region "Methods"
    Public Shared Sub SaveSettings(strTempOutputPath As String, strOutputPath As String, strOutputFileName As String, strVideoCodec As String, strVideoWidth As String,
                                   strVideoHeight As String, strAudioCodec As String, strAudioCodec2 As String, strAudioChannels As String, strAudioChannels2 As String,
                                   strAudioBitrate As String, strAudioBitrate2 As String, strAudioSamplingRate As String, strAudioSamplingRate2 As String,
                                   strSubtitleCodec As String, blDualAudio As Boolean, blCopySubtitles As Boolean, blIsActive As Boolean)
        _tempOutputPath = strTempOutputPath
        _outputPath = strOutputPath
        _outputFileName = strOutputFileName
        _videoCodec = strVideoCodec
        _videoWidth = strVideoWidth
        _videoHeight = strVideoHeight
        _audioCodec = strAudioCodec
        _audioCodec2 = strAudioCodec2
        _audioChannels = strAudioChannels
        _audioChannels2 = strAudioChannels2
        _audioBitrate = strAudioBitrate
        _audioBitrate2 = strAudioBitrate2
        _audioSamplingRate = strAudioSamplingRate
        _audioSamplingRate2 = strAudioSamplingRate2
        _subtitleCodec = strSubtitleCodec
        _dualAudio = blDualAudio
        _copySubtitles = blCopySubtitles
        _isActive = blIsActive
    End Sub
    Public Shared Sub ClearSettings()
        _outputPath = ""
        _outputFileName = ""
        _videoCodec = ""
        _videoWidth = ""
        _videoHeight = ""
        _audioCodec = ""
        _audioCodec2 = ""
        _audioChannels = ""
        _audioChannels2 = ""
        _audioBitrate = ""
        _audioBitrate2 = ""
        _audioSamplingRate = ""
        _audioSamplingRate2 = ""
        _subtitleCodec = ""
        _isActive = False
    End Sub
#End Region

#Region "Properties"
    Public Shared Property TempOutputPath() As String
        Get
            Return _tempOutputPath
        End Get
        Set(value As String)
            _tempOutputPath = value
        End Set
    End Property
    Public Shared Property OutputPath() As String
        Get
            Return _outputPath
        End Get
        Set(value As String)
            _outputPath = value
        End Set
    End Property
    Public Shared Property OutputFileName() As String
        Get
            Return _outputFileName
        End Get
        Set(value As String)
            _outputFileName = value
        End Set
    End Property
    Public Shared Property VideoCodec() As String
        Get
            Return _videoCodec
        End Get
        Set(value As String)
            _videoCodec = value
        End Set
    End Property
    Public Shared Property VideoWidth() As String
        Get
            Return _videoWidth
        End Get
        Set(value As String)
            _videoWidth = value
        End Set
    End Property
    Public Shared Property VideoHeight() As String
        Get
            Return _videoHeight
        End Get
        Set(value As String)
            _videoHeight = value
        End Set
    End Property
    Public Shared Property AudioCodec() As String
        Get
            Return _audioCodec
        End Get
        Set(value As String)
            _audioCodec = value
        End Set
    End Property
    Public Shared Property AudioCodec2() As String
        Get
            Return _audioCodec2
        End Get
        Set(value As String)
            _audioCodec2 = value
        End Set
    End Property
    Public Shared Property AudioChannels() As String
        Get
            Return _audioChannels
        End Get
        Set(value As String)
            _audioChannels = value
        End Set
    End Property
    Public Shared Property AudioChannels2() As String
        Get
            Return _audioChannels2
        End Get
        Set(value As String)
            _audioChannels2 = value
        End Set
    End Property
    Public Shared Property AudioBitrate() As String
        Get
            Return _audioBitrate
        End Get
        Set(value As String)
            _audioBitrate = value
        End Set
    End Property
    Public Shared Property AudioBitrate2() As String
        Get
            Return _audioBitrate2
        End Get
        Set(value As String)
            _audioBitrate2 = value
        End Set
    End Property
    Public Shared Property AudioSamplingRate() As String
        Get
            Return _audioSamplingRate
        End Get
        Set(value As String)
            _audioSamplingRate = value
        End Set
    End Property
    Public Shared Property AudioSamplingRate2() As String
        Get
            Return _audioSamplingRate2
        End Get
        Set(value As String)
            _audioSamplingRate2 = value
        End Set
    End Property
    Public Shared Property SubtitleCodec() As String
        Get
            Return _subtitleCodec
        End Get
        Set(value As String)
            _subtitleCodec = value
        End Set
    End Property
    Public Shared Property DualAudio() As Boolean
        Get
            Return _dualAudio
        End Get
        Set(value As Boolean)
            _dualAudio = value
        End Set
    End Property
    Public Shared Property CopySubtitles() As Boolean
        Get
            Return _copySubtitles
        End Get
        Set(value As Boolean)
            _copySubtitles = value
        End Set
    End Property
    Public Shared Property IsActive() As Boolean
        Get
            Return _isActive
        End Get
        Set(value As Boolean)
            _isActive = value
        End Set
    End Property
#End Region

End Class
