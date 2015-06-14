<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblCompletedFiles = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.ProgressBar = New CustomControls.CustomProgressBar()
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(14, 70)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(40, 13)
        Me.lblStatus.TabIndex = 23
        Me.lblStatus.Text = "Status:"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClose.Location = New System.Drawing.Point(129, 115)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 26)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        Me.btnClose.Visible = False
        '
        'lblCompletedFiles
        '
        Me.lblCompletedFiles.AutoSize = True
        Me.lblCompletedFiles.Location = New System.Drawing.Point(11, 10)
        Me.lblCompletedFiles.Name = "lblCompletedFiles"
        Me.lblCompletedFiles.Size = New System.Drawing.Size(0, 13)
        Me.lblCompletedFiles.TabIndex = 21
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.Location = New System.Drawing.Point(12, 23)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(0, 13)
        Me.lblError.TabIndex = 25
        '
        'ProgressBar
        '
        Me.ProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ProgressBar.CustomText = Nothing
        Me.ProgressBar.DisplayStyle = CustomControls.ProgressBarDisplayText.Percentage
        Me.ProgressBar.Location = New System.Drawing.Point(17, 86)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(335, 23)
        Me.ProgressBar.TabIndex = 26
        '
        'ProcessDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(364, 144)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblCompletedFiles)
        Me.Name = "ProcessDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ProcessDialog"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblCompletedFiles As System.Windows.Forms.Label
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents ProgressBar As CustomControls.CustomProgressBar
End Class
