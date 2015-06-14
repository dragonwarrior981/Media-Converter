Imports System.Diagnostics
Imports System.Windows.Forms
Partial Public Class UpdateForm
    Inherits Form
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Property Info As String
        Get
            Return lblInfo.Text
        End Get
        Set(value As String)
            lblInfo.Text = value
        End Set
    End Property
    Private _privateMoreInfoLink As String
    Public Property MoreInfoLink As String
        Private Get
            Return _privateMoreInfoLink
        End Get
        Set(value As String)
            _privateMoreInfoLink = value
        End Set
    End Property
    Private Sub linkInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkInfo.Click
        linkInfo.LinkVisited = True
        Process.Start(MoreInfoLink)
    End Sub
End Class
