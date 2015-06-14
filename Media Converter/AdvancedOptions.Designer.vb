<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvancedOptions
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
        Me.cbIgnorePictures = New System.Windows.Forms.CheckBox()
        Me.cbOverwrite = New System.Windows.Forms.CheckBox()
        Me.bnClose = New System.Windows.Forms.Button()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.cbSaveSize = New System.Windows.Forms.CheckBox()
        Me.cbSaveLocation = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cbIgnorePictures
        '
        Me.cbIgnorePictures.AutoSize = True
        Me.cbIgnorePictures.Location = New System.Drawing.Point(13, 13)
        Me.cbIgnorePictures.Name = "cbIgnorePictures"
        Me.cbIgnorePictures.Size = New System.Drawing.Size(126, 17)
        Me.cbIgnorePictures.TabIndex = 0
        Me.cbIgnorePictures.Text = "Ignore picture tracks."
        Me.cbIgnorePictures.UseVisualStyleBackColor = True
        '
        'cbOverwrite
        '
        Me.cbOverwrite.AutoSize = True
        Me.cbOverwrite.Location = New System.Drawing.Point(13, 36)
        Me.cbOverwrite.Name = "cbOverwrite"
        Me.cbOverwrite.Size = New System.Drawing.Size(289, 17)
        Me.cbOverwrite.TabIndex = 1
        Me.cbOverwrite.Text = "Overwrite destination file by default. (Not recommended)"
        Me.cbOverwrite.UseVisualStyleBackColor = True
        '
        'bnClose
        '
        Me.bnClose.Location = New System.Drawing.Point(297, 104)
        Me.bnClose.Name = "bnClose"
        Me.bnClose.Size = New System.Drawing.Size(75, 23)
        Me.bnClose.TabIndex = 2
        Me.bnClose.Text = "Close"
        Me.bnClose.UseVisualStyleBackColor = True
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(197, 104)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(94, 23)
        Me.btnDefault.TabIndex = 185
        Me.btnDefault.Text = "Load Defaults"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'cbSaveSize
        '
        Me.cbSaveSize.AutoSize = True
        Me.cbSaveSize.Location = New System.Drawing.Point(13, 59)
        Me.cbSaveSize.Name = "cbSaveSize"
        Me.cbSaveSize.Size = New System.Drawing.Size(148, 17)
        Me.cbSaveSize.TabIndex = 186
        Me.cbSaveSize.Text = "Remember windows size?"
        Me.cbSaveSize.UseVisualStyleBackColor = True
        '
        'cbSaveLocation
        '
        Me.cbSaveLocation.AutoSize = True
        Me.cbSaveLocation.Location = New System.Drawing.Point(13, 82)
        Me.cbSaveLocation.Name = "cbSaveLocation"
        Me.cbSaveLocation.Size = New System.Drawing.Size(167, 17)
        Me.cbSaveLocation.TabIndex = 187
        Me.cbSaveLocation.Text = "Remember windows location?"
        Me.cbSaveLocation.UseVisualStyleBackColor = True
        '
        'AdvancedOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 137)
        Me.Controls.Add(Me.cbSaveLocation)
        Me.Controls.Add(Me.cbSaveSize)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.bnClose)
        Me.Controls.Add(Me.cbOverwrite)
        Me.Controls.Add(Me.cbIgnorePictures)
        Me.Name = "AdvancedOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Advanced Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbIgnorePictures As System.Windows.Forms.CheckBox
    Friend WithEvents cbOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents bnClose As System.Windows.Forms.Button
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents cbSaveSize As System.Windows.Forms.CheckBox
    Friend WithEvents cbSaveLocation As System.Windows.Forms.CheckBox
End Class
