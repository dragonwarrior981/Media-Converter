<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileExists
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.btnOverwrite = New System.Windows.Forms.Button()
        Me.lblFileExists = New System.Windows.Forms.Label()
        Me.pictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(196, 103)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(110, 103)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(79, 23)
        Me.btnAddNew.TabIndex = 21
        Me.btnAddNew.Text = "Add New File"
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'btnOverwrite
        '
        Me.btnOverwrite.Location = New System.Drawing.Point(19, 103)
        Me.btnOverwrite.Name = "btnOverwrite"
        Me.btnOverwrite.Size = New System.Drawing.Size(75, 23)
        Me.btnOverwrite.TabIndex = 20
        Me.btnOverwrite.Text = "Overwrite"
        Me.btnOverwrite.UseVisualStyleBackColor = True
        '
        'lblFileExists
        '
        Me.lblFileExists.AutoSize = True
        Me.lblFileExists.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileExists.Location = New System.Drawing.Point(73, 32)
        Me.lblFileExists.Name = "lblFileExists"
        Me.lblFileExists.Size = New System.Drawing.Size(228, 20)
        Me.lblFileExists.TabIndex = 19
        Me.lblFileExists.Text = "File already exists! What to do?"
        '
        'pictureBox
        '
        Me.pictureBox.Image = Global.Media_Converter.My.Resources.Resources.ErrorImage
        Me.pictureBox.Location = New System.Drawing.Point(19, 19)
        Me.pictureBox.Name = "pictureBox"
        Me.pictureBox.Size = New System.Drawing.Size(48, 48)
        Me.pictureBox.TabIndex = 18
        Me.pictureBox.TabStop = False
        '
        'FileExists
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 145)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAddNew)
        Me.Controls.Add(Me.btnOverwrite)
        Me.Controls.Add(Me.lblFileExists)
        Me.Controls.Add(Me.pictureBox)
        Me.Name = "FileExists"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FileExists"
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents btnOverwrite As System.Windows.Forms.Button
    Friend WithEvents lblFileExists As System.Windows.Forms.Label
    Friend WithEvents pictureBox As System.Windows.Forms.PictureBox
End Class
