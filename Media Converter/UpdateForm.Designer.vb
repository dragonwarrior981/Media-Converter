<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateForm))
        Me.btnSkip = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.linkInfo = New System.Windows.Forms.LinkLabel()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnSkip
        '
        Me.btnSkip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSkip.Image = CType(resources.GetObject("btnSkip.Image"), System.Drawing.Image)
        Me.btnSkip.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSkip.Location = New System.Drawing.Point(215, 87)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(66, 40)
        Me.btnSkip.TabIndex = 9
        Me.btnSkip.Text = "Skip"
        Me.btnSkip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSkip.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUpdate.Location = New System.Drawing.Point(299, 86)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(66, 40)
        Me.btnUpdate.TabIndex = 8
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'linkInfo
        '
        Me.linkInfo.AutoSize = True
        Me.linkInfo.Enabled = False
        Me.linkInfo.Location = New System.Drawing.Point(24, 109)
        Me.linkInfo.Name = "linkInfo"
        Me.linkInfo.Size = New System.Drawing.Size(52, 13)
        Me.linkInfo.TabIndex = 7
        Me.linkInfo.TabStop = True
        Me.linkInfo.Text = "More Info"
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.Location = New System.Drawing.Point(24, 48)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(341, 23)
        Me.lblInfo.TabIndex = 6
        Me.lblInfo.Text = "A new version {0} was made available on {1}."
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.label1.Location = New System.Drawing.Point(23, 16)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(151, 24)
        Me.label1.TabIndex = 5
        Me.label1.Text = "Update Available"
        '
        'UpdateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 143)
        Me.Controls.Add(Me.btnSkip)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.linkInfo)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.label1)
        Me.Name = "UpdateForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Media Converter {0}"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnSkip As System.Windows.Forms.Button
    Private WithEvents btnUpdate As System.Windows.Forms.Button
    Private WithEvents linkInfo As System.Windows.Forms.LinkLabel
    Private WithEvents lblInfo As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
