Public Class ProcessDialog

#Region "Properties"
    Public WriteOnly Property Status() As String
        Set(ByVal value As String)
            lblStatus.Text = value
        End Set
    End Property
    Public WriteOnly Property ProgressValue() As Integer
        Set(ByVal value As Integer)
            ProgressBar.Value = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
#End Region

End Class