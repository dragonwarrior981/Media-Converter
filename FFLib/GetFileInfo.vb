Public NotInheritable Class GetFileInfo
    Implements IDisposable

#Region "Variables"
    Private ReadOnly _strAppPath As String = My.Application.Info.DirectoryPath & "\"
    Private ReadOnly _strAppDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\"
    Private ReadOnly _strAppName As String = My.Application.Info.AssemblyName
    Private ReadOnly _ffprobe As String = ""

    Private _videoCodec As String
    Private _audioCodec As String
    Private _audioCodec2 As String
    Private _subtitleCodec As String

    Private ReadOnly _fFprobeProcess As New ProcessStartInfo
    Private _source As String
    Private _commandPass As String
    Private _fFprobeStandardOutput As String
    Private _fFprobeErrorOutput As String
#End Region

#Region "Private Methods"
    Private Sub SetupCommandPass()
        _commandPass = String.Format(" -v quiet -hide_banner -show_streams -print_format xml {0}", _source)
    End Sub
    Private Sub RunFFprobe(argument As String)
        Try
            _fFprobeProcess.Arguments = argument
            _fFprobeProcess.UseShellExecute = False
            _fFprobeProcess.RedirectStandardOutput = True
            _fFprobeProcess.RedirectStandardError = True
            _fFprobeProcess.CreateNoWindow = True
            Dim fFmpegProcess = Process.Start(_fFprobeProcess)
            Dim readerStdOut As IO.StreamReader = fFmpegProcess.StandardOutput
            _fFprobeStandardOutput = Nothing
            _fFprobeErrorOutput = Nothing
            While readerStdOut.EndOfStream = False
                _fFprobeStandardOutput += readerStdOut.ReadLine()
            End While
            Dim readerStdErr As IO.StreamReader = fFmpegProcess.StandardError
            While readerStdOut.EndOfStream = False
                _fFprobeErrorOutput += readerStdErr.ReadLine()
            End While
            fFmpegProcess.WaitForExit()
            fFmpegProcess.Close()
        Catch ex As Exception
            If Not IO.Directory.Exists(_strAppDataPath & _strAppName) Then
                IO.Directory.CreateDirectory(_strAppDataPath & _strAppName)
            End If
            Dim errorWriter As New IO.StreamWriter(_strAppDataPath & _strAppName & "\fflib_error_log.txt", True)
            errorWriter.WriteLine(ex.ToString)
            errorWriter.Flush()
            errorWriter.Close()
            errorWriter.Dispose()
        End Try
    End Sub
#End Region

#Region "Private Functions"
    Private Function SourceExists() As Boolean
        If My.Computer.FileSystem.FileExists(_source.Replace("""", "")) = True Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Constructors"
    ''' Initializes a new instance of the <see cref="Encoder" /> class.
    Sub New()
        _ffprobe = _strAppPath & "ffmpeg\ffprobe.exe"
        _fFprobeProcess.FileName = _ffprobe
    End Sub
    ''' Initializes a new instance of the <see cref="Encoder" /> class.
    ''' The FFmpeg executable location.
    ' ReSharper disable once UnusedMember.Global
    Sub New(fFprobe As String)
        _ffprobe = fFprobe
        _fFprobeProcess.FileName = _ffprobe
    End Sub
#End Region

#Region "Public Methods"
    Public Sub GetMediaInfo()
        If SourceExists() = True Then 'else do nothing, will error out anyways. ANd again, we will check when we analyze
            SetupCommandPass()
            RunFFprobe(_commandPass)
        End If
    End Sub
#End Region

#Region "Properties"
    Public Property SourceFile As String
        Get
            Return _source
        End Get
        Set(source As String)
            _source = """" & source & """"
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
    Public Property AudioCodec As String
        Get
            Return _audioCodec
        End Get
        Set(value As String)
            _audioCodec = value
        End Set
    End Property
    Public Property AudioCodec2 As String
        Get
            Return _audioCodec2
        End Get
        Set(value As String)
            _audioCodec2 = value
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
    Public ReadOnly Property FFprobeStandardOutput As String
        Get
            Return _fFprobeStandardOutput
        End Get
    End Property
    Public ReadOnly Property FFprobeErrorOutput As String
        Get
            Return _fFprobeErrorOutput
        End Get
    End Property
#End Region

#Region "IDisposable Support"
    Private _disposedValue As Boolean ' To detect redundant calls
    Private Sub Dispose(disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If
        End If
        _disposedValue = True
    End Sub
    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
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
