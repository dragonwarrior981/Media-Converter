Public NotInheritable Class AboutBox
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
    End Sub
    Private Sub AboutBox_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim applicationTitle As String
        If My.Application.Info.Title <> "" Then
            applicationTitle = My.Application.Info.Title
        Else
            applicationTitle = IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Text = String.Format("About {0}", applicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' properties dialog (under the "Project" menu).
        LabelProductName.Text = My.Application.Info.ProductName
        LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        LabelCopyright.Text = My.Application.Info.Copyright
        LabelCompanyName.Text = My.Application.Info.CompanyName
        LabelEmail.Text = My.Resources.ContactEmail
        TextBoxDescription.Text = My.Application.Info.Description
    End Sub
    Private Sub LabelEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelEmail.LinkClicked
        Process.Start(String.Format("mailto:{0}?Subject={1} {2} Question", My.Resources.ContactEmail, My.Application.Info.ProductName, My.Application.Info.Version.ToString))
    End Sub
    Private Sub OKButton_Click(sender As System.Object, e As EventArgs) Handles OKButton.Click
        Close()
    End Sub

End Class
