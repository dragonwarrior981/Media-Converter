' ReSharper disable once ClassNeverInstantiated.Global
Public Class AdvancedOptions

#Region "Form Load"
    ' ReSharper disable once MemberCanBePrivate.Global
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.Icon
    End Sub
    Private Sub AdvancedOptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        cbIgnorePictures.Checked = My.MySettings.Default.IgnorePictures
        cbOverwrite.Checked = My.MySettings.Default.Overwrite
        cbSaveSize.Checked = My.MySettings.Default.SaveWindowSize
        cbSaveLocation.Checked = My.MySettings.Default.SaveWindowLocation
    End Sub
#End Region

#Region "Ignore Pictures Checked"
    Private Sub cbIgnoreMjpeg_CheckedChanged(sender As Object, e As EventArgs) Handles cbIgnorePictures.CheckedChanged
        My.MySettings.Default.IgnorePictures = cbIgnorePictures.Checked
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Overwrite Checked"
    Private Sub cbOverwrite_CheckedChanged(sender As Object, e As EventArgs) Handles cbOverwrite.CheckedChanged
        My.MySettings.Default.Overwrite = cbOverwrite.Checked
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Save Window Size and Location"
    Private Sub cbSaveSize_CheckedChanged(sender As Object, e As EventArgs) Handles cbSaveSize.CheckedChanged
        My.MySettings.Default.SaveWindowSize = cbSaveSize.Checked
        My.MySettings.Default.Save()
    End Sub
    Private Sub cbSaveLocation_CheckedChanged(sender As Object, e As EventArgs) Handles cbSaveLocation.CheckedChanged
        My.MySettings.Default.SaveWindowLocation = cbSaveLocation.Checked
        My.MySettings.Default.Save()
    End Sub
#End Region

#Region "Close Button Clicked"
    Private Sub bnClose_Click(sender As Object, e As EventArgs) Handles bnClose.Click
        Close()
    End Sub
#End Region

#Region "Load Defaults Button Clicked"
    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click
        My.MySettings.Default.IgnorePictures = True
        My.MySettings.Default.Overwrite = False
        My.MySettings.Default.SaveWindowSize = True
        My.MySettings.Default.SaveWindowLocation = True
        My.MySettings.Default.Save()

        cbIgnorePictures.Checked = My.MySettings.Default.IgnorePictures
        cbOverwrite.Checked = My.MySettings.Default.Overwrite
    End Sub
#End Region

End Class