' ReSharper disable once ClassNeverInstantiated.Global
Public Class FileExists
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
    End Sub
    Private Sub btnOverwrite_Click(sender As Object, e As EventArgs) Handles btnOverwrite.Click
        DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        DialogResult = Windows.Forms.DialogResult.No
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

End Class