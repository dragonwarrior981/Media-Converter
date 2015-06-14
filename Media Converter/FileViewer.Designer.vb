<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileViewer
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
        Me.lblBody = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblBody
        '
        Me.lblBody.AutoSize = True
        Me.lblBody.ForeColor = System.Drawing.SystemColors.Window
        Me.lblBody.Location = New System.Drawing.Point(12, 9)
        Me.lblBody.Name = "lblBody"
        Me.lblBody.Size = New System.Drawing.Size(28, 13)
        Me.lblBody.TabIndex = 1
        Me.lblBody.Text = "Text"
        '
        'FileViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Blue
        Me.ClientSize = New System.Drawing.Size(735, 607)
        Me.Controls.Add(Me.lblBody)
        Me.Name = "FileViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblBody As System.Windows.Forms.Label
End Class
