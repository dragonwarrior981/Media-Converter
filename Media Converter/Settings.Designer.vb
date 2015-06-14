<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
        Me.ofdCompleted = New System.Windows.Forms.OpenFileDialog()
        Me.cbCanceled = New System.Windows.Forms.CheckBox()
        Me.txtCanceled = New System.Windows.Forms.RichTextBox()
        Me.btnCanaceled = New System.Windows.Forms.Button()
        Me.lblCanceled = New System.Windows.Forms.Label()
        Me.ofdCanceled = New System.Windows.Forms.OpenFileDialog()
        Me.cbCompleted = New System.Windows.Forms.CheckBox()
        Me.txtCompleted = New System.Windows.Forms.RichTextBox()
        Me.btnCompleted = New System.Windows.Forms.Button()
        Me.lblCompleted = New System.Windows.Forms.Label()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtOutputFolder = New System.Windows.Forms.RichTextBox()
        Me.txtInputFolder = New System.Windows.Forms.RichTextBox()
        Me.fbInput = New System.Windows.Forms.FolderBrowserDialog()
        Me.fbOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.cbLastInputFile = New System.Windows.Forms.CheckBox()
        Me.GroupBoxOS = New System.Windows.Forms.GroupBox()
        Me.rbCustom = New System.Windows.Forms.RadioButton()
        Me.rbSame = New System.Windows.Forms.RadioButton()
        Me.rbDefault = New System.Windows.Forms.RadioButton()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.lblOutputFolder = New System.Windows.Forms.Label()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.lblInputFolder = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtLogPath = New System.Windows.Forms.RichTextBox()
        Me.btnLogPath = New System.Windows.Forms.Button()
        Me.lblLogPath = New System.Windows.Forms.Label()
        Me.fbLogPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBoxBatchOS = New System.Windows.Forms.GroupBox()
        Me.rbBatchCustom = New System.Windows.Forms.RadioButton()
        Me.rbBatchDefault = New System.Windows.Forms.RadioButton()
        Me.GroupBoxJoinOS = New System.Windows.Forms.GroupBox()
        Me.rbJoinCustom = New System.Windows.Forms.RadioButton()
        Me.rbJoinDefault = New System.Windows.Forms.RadioButton()
        Me.GroupBoxOS.SuspendLayout()
        Me.GroupBoxBatchOS.SuspendLayout()
        Me.GroupBoxJoinOS.SuspendLayout()
        Me.SuspendLayout()
        '
        'ofdCompleted
        '
        Me.ofdCompleted.Filter = "Wav files (*.wav)|*.wav"
        '
        'cbCanceled
        '
        Me.cbCanceled.AutoSize = True
        Me.cbCanceled.Location = New System.Drawing.Point(373, 96)
        Me.cbCanceled.Name = "cbCanceled"
        Me.cbCanceled.Size = New System.Drawing.Size(197, 17)
        Me.cbCanceled.TabIndex = 192
        Me.cbCanceled.Text = "Play sound when process canceled."
        Me.cbCanceled.UseVisualStyleBackColor = True
        '
        'txtCanceled
        '
        Me.txtCanceled.BackColor = System.Drawing.SystemColors.Window
        Me.txtCanceled.Location = New System.Drawing.Point(174, 70)
        Me.txtCanceled.Multiline = False
        Me.txtCanceled.Name = "txtCanceled"
        Me.txtCanceled.ReadOnly = True
        Me.txtCanceled.Size = New System.Drawing.Size(435, 20)
        Me.txtCanceled.TabIndex = 191
        Me.txtCanceled.Text = ""
        '
        'btnCanaceled
        '
        Me.btnCanaceled.Location = New System.Drawing.Point(615, 67)
        Me.btnCanaceled.Name = "btnCanaceled"
        Me.btnCanaceled.Size = New System.Drawing.Size(75, 23)
        Me.btnCanaceled.TabIndex = 190
        Me.btnCanaceled.Text = "Browse"
        Me.btnCanaceled.UseVisualStyleBackColor = True
        '
        'lblCanceled
        '
        Me.lblCanceled.AutoSize = True
        Me.lblCanceled.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanceled.Location = New System.Drawing.Point(51, 71)
        Me.lblCanceled.Name = "lblCanceled"
        Me.lblCanceled.Size = New System.Drawing.Size(112, 17)
        Me.lblCanceled.TabIndex = 189
        Me.lblCanceled.Text = "Canceled Sound"
        '
        'ofdCanceled
        '
        Me.ofdCanceled.Filter = "Wav files (*.wav)|*.wav"
        '
        'cbCompleted
        '
        Me.cbCompleted.AutoSize = True
        Me.cbCompleted.Location = New System.Drawing.Point(174, 96)
        Me.cbCompleted.Name = "cbCompleted"
        Me.cbCompleted.Size = New System.Drawing.Size(202, 17)
        Me.cbCompleted.TabIndex = 188
        Me.cbCompleted.Text = "Play sound when process completed."
        Me.cbCompleted.UseVisualStyleBackColor = True
        '
        'txtCompleted
        '
        Me.txtCompleted.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompleted.Location = New System.Drawing.Point(174, 41)
        Me.txtCompleted.Multiline = False
        Me.txtCompleted.Name = "txtCompleted"
        Me.txtCompleted.ReadOnly = True
        Me.txtCompleted.Size = New System.Drawing.Size(435, 20)
        Me.txtCompleted.TabIndex = 187
        Me.txtCompleted.Text = ""
        '
        'btnCompleted
        '
        Me.btnCompleted.Location = New System.Drawing.Point(615, 38)
        Me.btnCompleted.Name = "btnCompleted"
        Me.btnCompleted.Size = New System.Drawing.Size(75, 23)
        Me.btnCompleted.TabIndex = 186
        Me.btnCompleted.Text = "Browse"
        Me.btnCompleted.UseVisualStyleBackColor = True
        '
        'lblCompleted
        '
        Me.lblCompleted.AutoSize = True
        Me.lblCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompleted.Location = New System.Drawing.Point(43, 42)
        Me.lblCompleted.Name = "lblCompleted"
        Me.lblCompleted.Size = New System.Drawing.Size(120, 17)
        Me.lblCompleted.TabIndex = 185
        Me.lblCompleted.Text = "Completed Sound"
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(514, 412)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(94, 23)
        Me.btnDefault.TabIndex = 184
        Me.btnDefault.Text = "Load Defaults"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(373, 412)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(85, 23)
        Me.btnClear.TabIndex = 183
        Me.btnClear.Text = "Clear File Info"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtOutputFolder
        '
        Me.txtOutputFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutputFolder.Location = New System.Drawing.Point(173, 148)
        Me.txtOutputFolder.Multiline = False
        Me.txtOutputFolder.Name = "txtOutputFolder"
        Me.txtOutputFolder.ReadOnly = True
        Me.txtOutputFolder.Size = New System.Drawing.Size(435, 20)
        Me.txtOutputFolder.TabIndex = 181
        Me.txtOutputFolder.Text = ""
        '
        'txtInputFolder
        '
        Me.txtInputFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtInputFolder.Location = New System.Drawing.Point(174, 119)
        Me.txtInputFolder.Multiline = False
        Me.txtInputFolder.Name = "txtInputFolder"
        Me.txtInputFolder.ReadOnly = True
        Me.txtInputFolder.Size = New System.Drawing.Size(435, 20)
        Me.txtInputFolder.TabIndex = 179
        Me.txtInputFolder.Text = ""
        '
        'fbInput
        '
        Me.fbInput.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'fbOutput
        '
        Me.fbOutput.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'cbLastInputFile
        '
        Me.cbLastInputFile.AutoSize = True
        Me.cbLastInputFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLastInputFile.Location = New System.Drawing.Point(37, 412)
        Me.cbLastInputFile.Name = "cbLastInputFile"
        Me.cbLastInputFile.Size = New System.Drawing.Size(329, 21)
        Me.cbLastInputFile.TabIndex = 182
        Me.cbLastInputFile.Text = "Load last used input file when application opens"
        Me.cbLastInputFile.UseVisualStyleBackColor = True
        '
        'GroupBoxOS
        '
        Me.GroupBoxOS.Controls.Add(Me.rbCustom)
        Me.GroupBoxOS.Controls.Add(Me.rbSame)
        Me.GroupBoxOS.Controls.Add(Me.rbDefault)
        Me.GroupBoxOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxOS.Location = New System.Drawing.Point(30, 188)
        Me.GroupBoxOS.Name = "GroupBoxOS"
        Me.GroupBoxOS.Size = New System.Drawing.Size(660, 62)
        Me.GroupBoxOS.TabIndex = 178
        Me.GroupBoxOS.TabStop = False
        Me.GroupBoxOS.Text = "Default Output Folder Setting"
        '
        'rbCustom
        '
        Me.rbCustom.AutoSize = True
        Me.rbCustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCustom.Location = New System.Drawing.Point(391, 25)
        Me.rbCustom.Name = "rbCustom"
        Me.rbCustom.Size = New System.Drawing.Size(205, 21)
        Me.rbCustom.TabIndex = 152
        Me.rbCustom.Text = "Use custom folder for output"
        Me.rbCustom.UseVisualStyleBackColor = True
        '
        'rbSame
        '
        Me.rbSame.AutoSize = True
        Me.rbSame.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSame.Location = New System.Drawing.Point(194, 25)
        Me.rbSame.Name = "rbSame"
        Me.rbSame.Size = New System.Drawing.Size(191, 21)
        Me.rbSame.TabIndex = 151
        Me.rbSame.Text = "Use input folder for output"
        Me.rbSame.UseVisualStyleBackColor = True
        '
        'rbDefault
        '
        Me.rbDefault.AutoSize = True
        Me.rbDefault.Checked = True
        Me.rbDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDefault.Location = New System.Drawing.Point(6, 25)
        Me.rbDefault.Name = "rbDefault"
        Me.rbDefault.Size = New System.Drawing.Size(182, 21)
        Me.rbDefault.TabIndex = 150
        Me.rbDefault.TabStop = True
        Me.rbDefault.Text = "Use default output folder"
        Me.rbDefault.UseVisualStyleBackColor = True
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(614, 145)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(75, 23)
        Me.btnOutput.TabIndex = 174
        Me.btnOutput.Text = "Browse"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'lblOutputFolder
        '
        Me.lblOutputFolder.AutoSize = True
        Me.lblOutputFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutputFolder.Location = New System.Drawing.Point(18, 151)
        Me.lblOutputFolder.Name = "lblOutputFolder"
        Me.lblOutputFolder.Size = New System.Drawing.Size(144, 17)
        Me.lblOutputFolder.TabIndex = 173
        Me.lblOutputFolder.Text = "Default Output Folder"
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(615, 116)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(75, 23)
        Me.btnInput.TabIndex = 172
        Me.btnInput.Text = "Browse"
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'lblInputFolder
        '
        Me.lblInputFolder.AutoSize = True
        Me.lblInputFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInputFolder.Location = New System.Drawing.Point(31, 122)
        Me.lblInputFolder.Name = "lblInputFolder"
        Me.lblInputFolder.Size = New System.Drawing.Size(132, 17)
        Me.lblInputFolder.TabIndex = 171
        Me.lblInputFolder.Text = "Default Input Folder"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(615, 412)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 175
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtLogPath
        '
        Me.txtLogPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtLogPath.Location = New System.Drawing.Point(174, 12)
        Me.txtLogPath.Multiline = False
        Me.txtLogPath.Name = "txtLogPath"
        Me.txtLogPath.ReadOnly = True
        Me.txtLogPath.Size = New System.Drawing.Size(435, 20)
        Me.txtLogPath.TabIndex = 201
        Me.txtLogPath.Text = ""
        '
        'btnLogPath
        '
        Me.btnLogPath.Location = New System.Drawing.Point(615, 9)
        Me.btnLogPath.Name = "btnLogPath"
        Me.btnLogPath.Size = New System.Drawing.Size(75, 23)
        Me.btnLogPath.TabIndex = 200
        Me.btnLogPath.Text = "Browse"
        Me.btnLogPath.UseVisualStyleBackColor = True
        '
        'lblLogPath
        '
        Me.lblLogPath.AutoSize = True
        Me.lblLogPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogPath.Location = New System.Drawing.Point(97, 12)
        Me.lblLogPath.Name = "lblLogPath"
        Me.lblLogPath.Size = New System.Drawing.Size(65, 17)
        Me.lblLogPath.TabIndex = 199
        Me.lblLogPath.Text = "Log Path"
        '
        'fbLogPath
        '
        Me.fbLogPath.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'GroupBoxBatchOS
        '
        Me.GroupBoxBatchOS.Controls.Add(Me.rbBatchCustom)
        Me.GroupBoxBatchOS.Controls.Add(Me.rbBatchDefault)
        Me.GroupBoxBatchOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxBatchOS.Location = New System.Drawing.Point(29, 256)
        Me.GroupBoxBatchOS.Name = "GroupBoxBatchOS"
        Me.GroupBoxBatchOS.Size = New System.Drawing.Size(660, 62)
        Me.GroupBoxBatchOS.TabIndex = 202
        Me.GroupBoxBatchOS.TabStop = False
        Me.GroupBoxBatchOS.Text = "Default Batch Output Folder Setting"
        '
        'rbBatchCustom
        '
        Me.rbBatchCustom.AutoSize = True
        Me.rbBatchCustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBatchCustom.Location = New System.Drawing.Point(195, 25)
        Me.rbBatchCustom.Name = "rbBatchCustom"
        Me.rbBatchCustom.Size = New System.Drawing.Size(205, 21)
        Me.rbBatchCustom.TabIndex = 152
        Me.rbBatchCustom.Text = "Use custom folder for output"
        Me.rbBatchCustom.UseVisualStyleBackColor = True
        '
        'rbBatchDefault
        '
        Me.rbBatchDefault.AutoSize = True
        Me.rbBatchDefault.Checked = True
        Me.rbBatchDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBatchDefault.Location = New System.Drawing.Point(6, 25)
        Me.rbBatchDefault.Name = "rbBatchDefault"
        Me.rbBatchDefault.Size = New System.Drawing.Size(182, 21)
        Me.rbBatchDefault.TabIndex = 150
        Me.rbBatchDefault.TabStop = True
        Me.rbBatchDefault.Text = "Use default output folder"
        Me.rbBatchDefault.UseVisualStyleBackColor = True
        '
        'GroupBoxJoinOS
        '
        Me.GroupBoxJoinOS.Controls.Add(Me.rbJoinCustom)
        Me.GroupBoxJoinOS.Controls.Add(Me.rbJoinDefault)
        Me.GroupBoxJoinOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxJoinOS.Location = New System.Drawing.Point(30, 334)
        Me.GroupBoxJoinOS.Name = "GroupBoxJoinOS"
        Me.GroupBoxJoinOS.Size = New System.Drawing.Size(660, 62)
        Me.GroupBoxJoinOS.TabIndex = 203
        Me.GroupBoxJoinOS.TabStop = False
        Me.GroupBoxJoinOS.Text = "Default Join Output Folder Setting"
        '
        'rbJoinCustom
        '
        Me.rbJoinCustom.AutoSize = True
        Me.rbJoinCustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbJoinCustom.Location = New System.Drawing.Point(195, 25)
        Me.rbJoinCustom.Name = "rbJoinCustom"
        Me.rbJoinCustom.Size = New System.Drawing.Size(205, 21)
        Me.rbJoinCustom.TabIndex = 152
        Me.rbJoinCustom.Text = "Use custom folder for output"
        Me.rbJoinCustom.UseVisualStyleBackColor = True
        '
        'rbJoinDefault
        '
        Me.rbJoinDefault.AutoSize = True
        Me.rbJoinDefault.Checked = True
        Me.rbJoinDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbJoinDefault.Location = New System.Drawing.Point(6, 25)
        Me.rbJoinDefault.Name = "rbJoinDefault"
        Me.rbJoinDefault.Size = New System.Drawing.Size(182, 21)
        Me.rbJoinDefault.TabIndex = 150
        Me.rbJoinDefault.TabStop = True
        Me.rbJoinDefault.Text = "Use default output folder"
        Me.rbJoinDefault.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 442)
        Me.Controls.Add(Me.GroupBoxJoinOS)
        Me.Controls.Add(Me.GroupBoxBatchOS)
        Me.Controls.Add(Me.txtLogPath)
        Me.Controls.Add(Me.btnLogPath)
        Me.Controls.Add(Me.lblLogPath)
        Me.Controls.Add(Me.cbCanceled)
        Me.Controls.Add(Me.txtCanceled)
        Me.Controls.Add(Me.btnCanaceled)
        Me.Controls.Add(Me.lblCanceled)
        Me.Controls.Add(Me.cbCompleted)
        Me.Controls.Add(Me.txtCompleted)
        Me.Controls.Add(Me.btnCompleted)
        Me.Controls.Add(Me.lblCompleted)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.txtOutputFolder)
        Me.Controls.Add(Me.txtInputFolder)
        Me.Controls.Add(Me.cbLastInputFile)
        Me.Controls.Add(Me.GroupBoxOS)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.lblOutputFolder)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.lblInputFolder)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.GroupBoxOS.ResumeLayout(False)
        Me.GroupBoxOS.PerformLayout()
        Me.GroupBoxBatchOS.ResumeLayout(False)
        Me.GroupBoxBatchOS.PerformLayout()
        Me.GroupBoxJoinOS.ResumeLayout(False)
        Me.GroupBoxJoinOS.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ofdCompleted As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbCanceled As System.Windows.Forms.CheckBox
    Friend WithEvents txtCanceled As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCanaceled As System.Windows.Forms.Button
    Friend WithEvents lblCanceled As System.Windows.Forms.Label
    Friend WithEvents ofdCanceled As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbCompleted As System.Windows.Forms.CheckBox
    Friend WithEvents txtCompleted As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCompleted As System.Windows.Forms.Button
    Friend WithEvents lblCompleted As System.Windows.Forms.Label
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtOutputFolder As System.Windows.Forms.RichTextBox
    Friend WithEvents txtInputFolder As System.Windows.Forms.RichTextBox
    Friend WithEvents fbInput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents fbOutput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cbLastInputFile As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxOS As System.Windows.Forms.GroupBox
    Friend WithEvents rbCustom As System.Windows.Forms.RadioButton
    Friend WithEvents rbSame As System.Windows.Forms.RadioButton
    Friend WithEvents rbDefault As System.Windows.Forms.RadioButton
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents lblOutputFolder As System.Windows.Forms.Label
    Friend WithEvents btnInput As System.Windows.Forms.Button
    Friend WithEvents lblInputFolder As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtLogPath As System.Windows.Forms.RichTextBox
    Friend WithEvents btnLogPath As System.Windows.Forms.Button
    Friend WithEvents lblLogPath As System.Windows.Forms.Label
    Friend WithEvents fbLogPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBoxBatchOS As System.Windows.Forms.GroupBox
    Friend WithEvents rbBatchCustom As System.Windows.Forms.RadioButton
    Friend WithEvents rbBatchDefault As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBoxJoinOS As System.Windows.Forms.GroupBox
    Friend WithEvents rbJoinCustom As System.Windows.Forms.RadioButton
    Friend WithEvents rbJoinDefault As System.Windows.Forms.RadioButton
End Class
