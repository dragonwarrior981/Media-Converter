' ReSharper disable once ClassNeverInstantiated.Global
Public Class MediaTags

#Region "Variables"
    Private Shared _videoTrackId As Integer
    Private Shared _videoCodec As String = ""
    Private Shared _audioTrackId As Integer
    Private Shared _audioCodec As String = ""
    Private Shared _audioChannels As String = ""
    Private Shared _audioTrackCount As Integer
    Private Shared _totalDualAudioFileCount As Integer
    Private Shared _subtitleCodec As String = ""
#End Region

#Region "Properties"
    Public Shared Property VideoTrackID() As Integer
        Get
            Return _videoTrackId
        End Get
        Set(value As Integer)
            _videoTrackId = value
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
    Public Shared Property AudioTrackID() As Integer
        Get
            Return _audioTrackId
        End Get
        Set(value As Integer)
            _audioTrackId = value
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
    Public Shared Property AudioChannels() As String
        Get
            Return _audioChannels
        End Get
        Set(value As String)
            _audioChannels = value
        End Set
    End Property
    Public Shared Property AudioTrackCount() As Integer
        Get
            Return _audioTrackCount
        End Get
        Set(value As Integer)
            _audioTrackCount = value
        End Set
    End Property
    Public Shared Property TotalDualAudioFileCount() As Integer
        Get
            Return _totalDualAudioFileCount
        End Get
        Set(value As Integer)
            _totalDualAudioFileCount = value
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
#End Region

End Class
