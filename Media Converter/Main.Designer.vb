<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ofdInput = New System.Windows.Forms.OpenFileDialog()
        Me.BatchBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.fbOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnExitApp = New System.Windows.Forms.Button()
        Me.rbBatchOScustom = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbBatchOSdefault = New System.Windows.Forms.RadioButton()
        Me.btnViewFileErrorLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnViewErrorLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnViewLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogsMenu = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnClearLogs = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnWriteErrorLogFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnWriteLogFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsMenu = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnAdvancedOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileMenu = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriority = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityRealTime = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityHigh = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityAboveNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityBelowNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPriorityIdle = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuBarStrip = New System.Windows.Forms.ToolStrip()
        Me.HelpMenu = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnAutoUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnUpdateNow = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.gbCurrentSettings = New System.Windows.Forms.GroupBox()
        Me.gbSubtitles = New System.Windows.Forms.GroupBox()
        Me.lblSubtitles = New System.Windows.Forms.Label()
        Me.btnOutputOptions = New System.Windows.Forms.Button()
        Me.gbGeneral = New System.Windows.Forms.GroupBox()
        Me.lblGeneral = New System.Windows.Forms.Label()
        Me.gbAudio = New System.Windows.Forms.GroupBox()
        Me.lblAudio2 = New System.Windows.Forms.Label()
        Me.lblAudio = New System.Windows.Forms.Label()
        Me.gbVideo = New System.Windows.Forms.GroupBox()
        Me.lblVideo2 = New System.Windows.Forms.Label()
        Me.lblVideo1 = New System.Windows.Forms.Label()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.MainTabPage = New System.Windows.Forms.TabPage()
        Me.lbStreams = New System.Windows.Forms.ListBox()
        Me.PanelOutputFolder = New System.Windows.Forms.Panel()
        Me.rbOScustom = New System.Windows.Forms.RadioButton()
        Me.rbOSsame = New System.Windows.Forms.RadioButton()
        Me.rbOSdefault = New System.Windows.Forms.RadioButton()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.BatchTabPage = New System.Windows.Forms.TabPage()
        Me.btnClearFiles = New System.Windows.Forms.Button()
        Me.btnRemoveFile = New System.Windows.Forms.Button()
        Me.lbBatchStreams = New System.Windows.Forms.ListBox()
        Me.btnSelectFiles = New System.Windows.Forms.Button()
        Me.lbxFiles = New System.Windows.Forms.ListBox()
        Me.txtBatchOutput = New System.Windows.Forms.TextBox()
        Me.btnBatchOutput = New System.Windows.Forms.Button()
        Me.lblBatchOutput = New System.Windows.Forms.Label()
        Me.JoinTabPage = New System.Windows.Forms.TabPage()
        Me.btnClearFilesJoin = New System.Windows.Forms.Button()
        Me.btnRemoveFileJoin = New System.Windows.Forms.Button()
        Me.lbJoinStreams = New System.Windows.Forms.ListBox()
        Me.btnSelectFilesJoin = New System.Windows.Forms.Button()
        Me.lbxFilesJoin = New System.Windows.Forms.ListBox()
        Me.txtJoinOutput = New System.Windows.Forms.TextBox()
        Me.btnJoinOutput = New System.Windows.Forms.Button()
        Me.lblJoinOutput = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbJoinOScustom = New System.Windows.Forms.RadioButton()
        Me.rbJoinOSdefault = New System.Windows.Forms.RadioButton()
        Me.ToolStripContainer2 = New System.Windows.Forms.ToolStripContainer()
        Me.JoinBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.TempBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.TempBatchBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.MenuBarStrip.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.gbCurrentSettings.SuspendLayout()
        Me.gbSubtitles.SuspendLayout()
        Me.gbGeneral.SuspendLayout()
        Me.gbAudio.SuspendLayout()
        Me.gbVideo.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.MainTabPage.SuspendLayout()
        Me.PanelOutputFolder.SuspendLayout()
        Me.BatchTabPage.SuspendLayout()
        Me.JoinTabPage.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ToolStripContainer2.ContentPanel.SuspendLayout()
        Me.ToolStripContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ofdInput
        '
        Me.ofdInput.Filter = resources.GetString("ofdInput.Filter")
        Me.ofdInput.Title = "Select file to convert"
        '
        'BatchBackgroundWorker
        '
        Me.BatchBackgroundWorker.WorkerReportsProgress = True
        Me.BatchBackgroundWorker.WorkerSupportsCancellation = True
        '
        'BackgroundWorker
        '
        Me.BackgroundWorker.WorkerReportsProgress = True
        Me.BackgroundWorker.WorkerSupportsCancellation = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(764, 133)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 261
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnExitApp
        '
        Me.btnExitApp.Location = New System.Drawing.Point(845, 133)
        Me.btnExitApp.Name = "btnExitApp"
        Me.btnExitApp.Size = New System.Drawing.Size(75, 23)
        Me.btnExitApp.TabIndex = 260
        Me.btnExitApp.Text = "Exit"
        Me.btnExitApp.UseVisualStyleBackColor = True
        '
        'rbBatchOScustom
        '
        Me.rbBatchOScustom.AutoSize = True
        Me.rbBatchOScustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBatchOScustom.Location = New System.Drawing.Point(198, 3)
        Me.rbBatchOScustom.Name = "rbBatchOScustom"
        Me.rbBatchOScustom.Size = New System.Drawing.Size(205, 21)
        Me.rbBatchOScustom.TabIndex = 149
        Me.rbBatchOScustom.Text = "Use custom folder for output"
        Me.rbBatchOScustom.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbBatchOScustom)
        Me.Panel1.Controls.Add(Me.rbBatchOSdefault)
        Me.Panel1.Location = New System.Drawing.Point(27, 174)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(853, 29)
        Me.Panel1.TabIndex = 298
        '
        'rbBatchOSdefault
        '
        Me.rbBatchOSdefault.AutoSize = True
        Me.rbBatchOSdefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBatchOSdefault.Location = New System.Drawing.Point(10, 3)
        Me.rbBatchOSdefault.Name = "rbBatchOSdefault"
        Me.rbBatchOSdefault.Size = New System.Drawing.Size(182, 21)
        Me.rbBatchOSdefault.TabIndex = 147
        Me.rbBatchOSdefault.Text = "Use default output folder"
        Me.rbBatchOSdefault.UseVisualStyleBackColor = True
        '
        'btnViewFileErrorLog
        '
        Me.btnViewFileErrorLog.Name = "btnViewFileErrorLog"
        Me.btnViewFileErrorLog.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.btnViewFileErrorLog.Size = New System.Drawing.Size(190, 22)
        Me.btnViewFileErrorLog.Text = "View File Error Log"
        '
        'btnViewErrorLog
        '
        Me.btnViewErrorLog.Name = "btnViewErrorLog"
        Me.btnViewErrorLog.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.btnViewErrorLog.Size = New System.Drawing.Size(190, 22)
        Me.btnViewErrorLog.Text = "View Error Log"
        '
        'btnViewLog
        '
        Me.btnViewLog.Name = "btnViewLog"
        Me.btnViewLog.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.btnViewLog.Size = New System.Drawing.Size(190, 22)
        Me.btnViewLog.Text = "View Log"
        '
        'LogsMenu
        '
        Me.LogsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.LogsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnViewLog, Me.btnViewErrorLog, Me.btnViewFileErrorLog, Me.btnClearLogs})
        Me.LogsMenu.Image = CType(resources.GetObject("LogsMenu.Image"), System.Drawing.Image)
        Me.LogsMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LogsMenu.Name = "LogsMenu"
        Me.LogsMenu.Size = New System.Drawing.Size(45, 22)
        Me.LogsMenu.Text = "Logs"
        '
        'btnClearLogs
        '
        Me.btnClearLogs.Name = "btnClearLogs"
        Me.btnClearLogs.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.btnClearLogs.Size = New System.Drawing.Size(190, 22)
        Me.btnClearLogs.Text = "Clear Log Files"
        '
        'btnWriteErrorLogFiles
        '
        Me.btnWriteErrorLogFiles.CheckOnClick = True
        Me.btnWriteErrorLogFiles.Name = "btnWriteErrorLogFiles"
        Me.btnWriteErrorLogFiles.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.btnWriteErrorLogFiles.Size = New System.Drawing.Size(219, 22)
        Me.btnWriteErrorLogFiles.Text = "Write Error Log Files"
        '
        'btnWriteLogFiles
        '
        Me.btnWriteLogFiles.CheckOnClick = True
        Me.btnWriteLogFiles.Name = "btnWriteLogFiles"
        Me.btnWriteLogFiles.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.btnWriteLogFiles.Size = New System.Drawing.Size(219, 22)
        Me.btnWriteLogFiles.Text = "Write Log Files"
        '
        'OptionsMenu
        '
        Me.OptionsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OptionsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAdvancedOptions, Me.btnWriteLogFiles, Me.btnWriteErrorLogFiles})
        Me.OptionsMenu.Image = CType(resources.GetObject("OptionsMenu.Image"), System.Drawing.Image)
        Me.OptionsMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OptionsMenu.Name = "OptionsMenu"
        Me.OptionsMenu.Size = New System.Drawing.Size(62, 22)
        Me.OptionsMenu.Text = "Options"
        '
        'btnAdvancedOptions
        '
        Me.btnAdvancedOptions.Name = "btnAdvancedOptions"
        Me.btnAdvancedOptions.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.btnAdvancedOptions.Size = New System.Drawing.Size(219, 22)
        Me.btnAdvancedOptions.Text = "Advanced Options"
        '
        'FileMenu
        '
        Me.FileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSettings, Me.btnPriority, Me.MenuItemSeparator, Me.btnExit})
        Me.FileMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FileMenu.Name = "FileMenu"
        Me.FileMenu.Size = New System.Drawing.Size(38, 22)
        Me.FileMenu.Text = "File"
        '
        'btnSettings
        '
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.btnSettings.Size = New System.Drawing.Size(152, 22)
        Me.btnSettings.Text = "Settings"
        '
        'btnPriority
        '
        Me.btnPriority.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPriorityRealTime, Me.btnPriorityHigh, Me.btnPriorityAboveNormal, Me.btnPriorityNormal, Me.btnPriorityBelowNormal, Me.btnPriorityIdle})
        Me.btnPriority.Name = "btnPriority"
        Me.btnPriority.Size = New System.Drawing.Size(152, 22)
        Me.btnPriority.Text = "Priority"
        '
        'btnPriorityRealTime
        '
        Me.btnPriorityRealTime.CheckOnClick = True
        Me.btnPriorityRealTime.Name = "btnPriorityRealTime"
        Me.btnPriorityRealTime.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityRealTime.Text = "Real Time"
        '
        'btnPriorityHigh
        '
        Me.btnPriorityHigh.CheckOnClick = True
        Me.btnPriorityHigh.Name = "btnPriorityHigh"
        Me.btnPriorityHigh.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityHigh.Text = "High"
        '
        'btnPriorityAboveNormal
        '
        Me.btnPriorityAboveNormal.CheckOnClick = True
        Me.btnPriorityAboveNormal.Name = "btnPriorityAboveNormal"
        Me.btnPriorityAboveNormal.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityAboveNormal.Text = "Above Normal"
        '
        'btnPriorityNormal
        '
        Me.btnPriorityNormal.CheckOnClick = True
        Me.btnPriorityNormal.Name = "btnPriorityNormal"
        Me.btnPriorityNormal.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityNormal.Text = "Normal"
        '
        'btnPriorityBelowNormal
        '
        Me.btnPriorityBelowNormal.CheckOnClick = True
        Me.btnPriorityBelowNormal.Name = "btnPriorityBelowNormal"
        Me.btnPriorityBelowNormal.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityBelowNormal.Text = "Below Normal"
        '
        'btnPriorityIdle
        '
        Me.btnPriorityIdle.CheckOnClick = True
        Me.btnPriorityIdle.Name = "btnPriorityIdle"
        Me.btnPriorityIdle.Size = New System.Drawing.Size(152, 22)
        Me.btnPriorityIdle.Text = "Idle"
        '
        'MenuItemSeparator
        '
        Me.MenuItemSeparator.Name = "MenuItemSeparator"
        Me.MenuItemSeparator.Size = New System.Drawing.Size(149, 6)
        '
        'btnExit
        '
        Me.btnExit.Name = "btnExit"
        Me.btnExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.[End]), System.Windows.Forms.Keys)
        Me.btnExit.Size = New System.Drawing.Size(152, 22)
        Me.btnExit.Text = "Exit"
        '
        'MenuBarStrip
        '
        Me.MenuBarStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuBarStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.OptionsMenu, Me.LogsMenu, Me.HelpMenu})
        Me.MenuBarStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuBarStrip.Name = "MenuBarStrip"
        Me.MenuBarStrip.Size = New System.Drawing.Size(233, 25)
        Me.MenuBarStrip.TabIndex = 0
        Me.MenuBarStrip.Text = "MenuBarStrip"
        '
        'HelpMenu
        '
        Me.HelpMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAutoUpdate, Me.btnUpdateNow, Me.btnAbout})
        Me.HelpMenu.Image = CType(resources.GetObject("HelpMenu.Image"), System.Drawing.Image)
        Me.HelpMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.Size = New System.Drawing.Size(45, 22)
        Me.HelpMenu.Text = "Help"
        Me.HelpMenu.ToolTipText = "Help"
        '
        'btnAutoUpdate
        '
        Me.btnAutoUpdate.CheckOnClick = True
        Me.btnAutoUpdate.Name = "btnAutoUpdate"
        Me.btnAutoUpdate.Size = New System.Drawing.Size(229, 22)
        Me.btnAutoUpdate.Text = "Check for Updates on Startup"
        '
        'btnUpdateNow
        '
        Me.btnUpdateNow.Name = "btnUpdateNow"
        Me.btnUpdateNow.Size = New System.Drawing.Size(229, 22)
        Me.btnUpdateNow.Text = "Check for Updates Now"
        '
        'btnAbout
        '
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.ShortcutKeyDisplayString = ""
        Me.btnAbout.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.btnAbout.Size = New System.Drawing.Size(229, 22)
        Me.btnAbout.Text = "About"
        '
        'ToolStripContainer1
        '
        Me.ToolStripContainer1.BottomToolStripPanelVisible = False
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.AutoScroll = True
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.gbCurrentSettings)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.TabControl)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.ToolStripContainer2)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(950, 575)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.LeftToolStripPanelVisible = False
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.RightToolStripPanelVisible = False
        Me.ToolStripContainer1.Size = New System.Drawing.Size(950, 575)
        Me.ToolStripContainer1.TabIndex = 156
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        Me.ToolStripContainer1.TopToolStripPanelVisible = False
        '
        'gbCurrentSettings
        '
        Me.gbCurrentSettings.Controls.Add(Me.gbSubtitles)
        Me.gbCurrentSettings.Controls.Add(Me.btnOutputOptions)
        Me.gbCurrentSettings.Controls.Add(Me.gbGeneral)
        Me.gbCurrentSettings.Controls.Add(Me.gbAudio)
        Me.gbCurrentSettings.Controls.Add(Me.btnStart)
        Me.gbCurrentSettings.Controls.Add(Me.gbVideo)
        Me.gbCurrentSettings.Controls.Add(Me.btnExitApp)
        Me.gbCurrentSettings.Location = New System.Drawing.Point(12, 398)
        Me.gbCurrentSettings.Name = "gbCurrentSettings"
        Me.gbCurrentSettings.Size = New System.Drawing.Size(926, 166)
        Me.gbCurrentSettings.TabIndex = 311
        Me.gbCurrentSettings.TabStop = False
        Me.gbCurrentSettings.Text = "Current Settings"
        '
        'gbSubtitles
        '
        Me.gbSubtitles.Controls.Add(Me.lblSubtitles)
        Me.gbSubtitles.Location = New System.Drawing.Point(9, 92)
        Me.gbSubtitles.Name = "gbSubtitles"
        Me.gbSubtitles.Size = New System.Drawing.Size(259, 64)
        Me.gbSubtitles.TabIndex = 2
        Me.gbSubtitles.TabStop = False
        Me.gbSubtitles.Text = "Subtitles"
        '
        'lblSubtitles
        '
        Me.lblSubtitles.AutoSize = True
        Me.lblSubtitles.Location = New System.Drawing.Point(7, 20)
        Me.lblSubtitles.Name = "lblSubtitles"
        Me.lblSubtitles.Size = New System.Drawing.Size(0, 13)
        Me.lblSubtitles.TabIndex = 0
        '
        'btnOutputOptions
        '
        Me.btnOutputOptions.Location = New System.Drawing.Point(672, 132)
        Me.btnOutputOptions.Name = "btnOutputOptions"
        Me.btnOutputOptions.Size = New System.Drawing.Size(86, 23)
        Me.btnOutputOptions.TabIndex = 310
        Me.btnOutputOptions.Text = "Output Options"
        Me.btnOutputOptions.UseVisualStyleBackColor = True
        '
        'gbGeneral
        '
        Me.gbGeneral.Controls.Add(Me.lblGeneral)
        Me.gbGeneral.Location = New System.Drawing.Point(9, 20)
        Me.gbGeneral.Name = "gbGeneral"
        Me.gbGeneral.Size = New System.Drawing.Size(259, 66)
        Me.gbGeneral.TabIndex = 1
        Me.gbGeneral.TabStop = False
        Me.gbGeneral.Text = "General"
        '
        'lblGeneral
        '
        Me.lblGeneral.AutoSize = True
        Me.lblGeneral.Location = New System.Drawing.Point(7, 20)
        Me.lblGeneral.Name = "lblGeneral"
        Me.lblGeneral.Size = New System.Drawing.Size(0, 13)
        Me.lblGeneral.TabIndex = 0
        '
        'gbAudio
        '
        Me.gbAudio.Controls.Add(Me.lblAudio2)
        Me.gbAudio.Controls.Add(Me.lblAudio)
        Me.gbAudio.Location = New System.Drawing.Point(582, 20)
        Me.gbAudio.Name = "gbAudio"
        Me.gbAudio.Size = New System.Drawing.Size(338, 91)
        Me.gbAudio.TabIndex = 1
        Me.gbAudio.TabStop = False
        Me.gbAudio.Text = "Audio"
        '
        'lblAudio2
        '
        Me.lblAudio2.AutoSize = True
        Me.lblAudio2.Location = New System.Drawing.Point(166, 20)
        Me.lblAudio2.Name = "lblAudio2"
        Me.lblAudio2.Size = New System.Drawing.Size(0, 13)
        Me.lblAudio2.TabIndex = 1
        '
        'lblAudio
        '
        Me.lblAudio.AutoSize = True
        Me.lblAudio.Location = New System.Drawing.Point(7, 20)
        Me.lblAudio.Name = "lblAudio"
        Me.lblAudio.Size = New System.Drawing.Size(0, 13)
        Me.lblAudio.TabIndex = 0
        '
        'gbVideo
        '
        Me.gbVideo.Controls.Add(Me.lblVideo2)
        Me.gbVideo.Controls.Add(Me.lblVideo1)
        Me.gbVideo.Location = New System.Drawing.Point(274, 20)
        Me.gbVideo.Name = "gbVideo"
        Me.gbVideo.Size = New System.Drawing.Size(302, 91)
        Me.gbVideo.TabIndex = 0
        Me.gbVideo.TabStop = False
        Me.gbVideo.Text = "Video"
        '
        'lblVideo2
        '
        Me.lblVideo2.AutoSize = True
        Me.lblVideo2.Location = New System.Drawing.Point(158, 20)
        Me.lblVideo2.Name = "lblVideo2"
        Me.lblVideo2.Size = New System.Drawing.Size(0, 13)
        Me.lblVideo2.TabIndex = 1
        '
        'lblVideo1
        '
        Me.lblVideo1.AutoSize = True
        Me.lblVideo1.Location = New System.Drawing.Point(7, 20)
        Me.lblVideo1.Name = "lblVideo1"
        Me.lblVideo1.Size = New System.Drawing.Size(0, 13)
        Me.lblVideo1.TabIndex = 0
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.MainTabPage)
        Me.TabControl.Controls.Add(Me.BatchTabPage)
        Me.TabControl.Controls.Add(Me.JoinTabPage)
        Me.TabControl.Location = New System.Drawing.Point(4, 29)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(943, 362)
        Me.TabControl.TabIndex = 303
        '
        'MainTabPage
        '
        Me.MainTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.MainTabPage.Controls.Add(Me.lbStreams)
        Me.MainTabPage.Controls.Add(Me.PanelOutputFolder)
        Me.MainTabPage.Controls.Add(Me.txtOutput)
        Me.MainTabPage.Controls.Add(Me.txtInput)
        Me.MainTabPage.Controls.Add(Me.btnOutput)
        Me.MainTabPage.Controls.Add(Me.lblOutput)
        Me.MainTabPage.Controls.Add(Me.btnInput)
        Me.MainTabPage.Controls.Add(Me.lblInput)
        Me.MainTabPage.Location = New System.Drawing.Point(4, 22)
        Me.MainTabPage.Name = "MainTabPage"
        Me.MainTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage.Size = New System.Drawing.Size(935, 336)
        Me.MainTabPage.TabIndex = 0
        Me.MainTabPage.Text = "Main"
        '
        'lbStreams
        '
        Me.lbStreams.FormattingEnabled = True
        Me.lbStreams.Location = New System.Drawing.Point(13, 92)
        Me.lbStreams.Name = "lbStreams"
        Me.lbStreams.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbStreams.Size = New System.Drawing.Size(880, 121)
        Me.lbStreams.TabIndex = 298
        '
        'PanelOutputFolder
        '
        Me.PanelOutputFolder.Controls.Add(Me.rbOScustom)
        Me.PanelOutputFolder.Controls.Add(Me.rbOSsame)
        Me.PanelOutputFolder.Controls.Add(Me.rbOSdefault)
        Me.PanelOutputFolder.Location = New System.Drawing.Point(25, 66)
        Me.PanelOutputFolder.Name = "PanelOutputFolder"
        Me.PanelOutputFolder.Size = New System.Drawing.Size(853, 29)
        Me.PanelOutputFolder.TabIndex = 297
        '
        'rbOScustom
        '
        Me.rbOScustom.AutoSize = True
        Me.rbOScustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbOScustom.Location = New System.Drawing.Point(397, 3)
        Me.rbOScustom.Name = "rbOScustom"
        Me.rbOScustom.Size = New System.Drawing.Size(205, 21)
        Me.rbOScustom.TabIndex = 149
        Me.rbOScustom.Text = "Use custom folder for output"
        Me.rbOScustom.UseVisualStyleBackColor = True
        '
        'rbOSsame
        '
        Me.rbOSsame.AutoSize = True
        Me.rbOSsame.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbOSsame.Location = New System.Drawing.Point(200, 3)
        Me.rbOSsame.Name = "rbOSsame"
        Me.rbOSsame.Size = New System.Drawing.Size(191, 21)
        Me.rbOSsame.TabIndex = 148
        Me.rbOSsame.Text = "Use input folder for output"
        Me.rbOSsame.UseVisualStyleBackColor = True
        '
        'rbOSdefault
        '
        Me.rbOSdefault.AutoSize = True
        Me.rbOSdefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbOSdefault.Location = New System.Drawing.Point(12, 3)
        Me.rbOSdefault.Name = "rbOSdefault"
        Me.rbOSdefault.Size = New System.Drawing.Size(182, 21)
        Me.rbOSdefault.TabIndex = 147
        Me.rbOSdefault.Text = "Use default output folder"
        Me.rbOSdefault.UseVisualStyleBackColor = True
        '
        'txtOutput
        '
        Me.txtOutput.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtOutput.Location = New System.Drawing.Point(109, 37)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(715, 20)
        Me.txtOutput.TabIndex = 295
        '
        'txtInput
        '
        Me.txtInput.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtInput.Location = New System.Drawing.Point(109, 6)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.ReadOnly = True
        Me.txtInput.Size = New System.Drawing.Size(715, 20)
        Me.txtInput.TabIndex = 292
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(830, 37)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(75, 23)
        Me.btnOutput.TabIndex = 296
        Me.btnOutput.Text = "Browse"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutput.Location = New System.Drawing.Point(21, 39)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(91, 20)
        Me.lblOutput.TabIndex = 294
        Me.lblOutput.Text = "Output File:"
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(830, 4)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(75, 23)
        Me.btnInput.TabIndex = 293
        Me.btnInput.Text = "Browse"
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'lblInput
        '
        Me.lblInput.AutoSize = True
        Me.lblInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInput.Location = New System.Drawing.Point(33, 7)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.Size = New System.Drawing.Size(79, 20)
        Me.lblInput.TabIndex = 291
        Me.lblInput.Text = "Input File:"
        '
        'BatchTabPage
        '
        Me.BatchTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.BatchTabPage.Controls.Add(Me.btnClearFiles)
        Me.BatchTabPage.Controls.Add(Me.btnRemoveFile)
        Me.BatchTabPage.Controls.Add(Me.lbBatchStreams)
        Me.BatchTabPage.Controls.Add(Me.btnSelectFiles)
        Me.BatchTabPage.Controls.Add(Me.lbxFiles)
        Me.BatchTabPage.Controls.Add(Me.txtBatchOutput)
        Me.BatchTabPage.Controls.Add(Me.btnBatchOutput)
        Me.BatchTabPage.Controls.Add(Me.lblBatchOutput)
        Me.BatchTabPage.Controls.Add(Me.Panel1)
        Me.BatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.BatchTabPage.Name = "BatchTabPage"
        Me.BatchTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.BatchTabPage.Size = New System.Drawing.Size(935, 336)
        Me.BatchTabPage.TabIndex = 1
        Me.BatchTabPage.Text = "Batch"
        '
        'btnClearFiles
        '
        Me.btnClearFiles.Location = New System.Drawing.Point(254, 6)
        Me.btnClearFiles.Name = "btnClearFiles"
        Me.btnClearFiles.Size = New System.Drawing.Size(70, 23)
        Me.btnClearFiles.TabIndex = 306
        Me.btnClearFiles.Text = "Clear Files"
        Me.btnClearFiles.UseVisualStyleBackColor = True
        '
        'btnRemoveFile
        '
        Me.btnRemoveFile.Location = New System.Drawing.Point(120, 6)
        Me.btnRemoveFile.Name = "btnRemoveFile"
        Me.btnRemoveFile.Size = New System.Drawing.Size(128, 23)
        Me.btnRemoveFile.TabIndex = 305
        Me.btnRemoveFile.Text = "Remove Selected File"
        Me.btnRemoveFile.UseVisualStyleBackColor = True
        '
        'lbBatchStreams
        '
        Me.lbBatchStreams.FormattingEnabled = True
        Me.lbBatchStreams.Location = New System.Drawing.Point(25, 209)
        Me.lbBatchStreams.Name = "lbBatchStreams"
        Me.lbBatchStreams.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbBatchStreams.Size = New System.Drawing.Size(880, 121)
        Me.lbBatchStreams.TabIndex = 304
        '
        'btnSelectFiles
        '
        Me.btnSelectFiles.Location = New System.Drawing.Point(27, 6)
        Me.btnSelectFiles.Name = "btnSelectFiles"
        Me.btnSelectFiles.Size = New System.Drawing.Size(87, 23)
        Me.btnSelectFiles.TabIndex = 303
        Me.btnSelectFiles.Text = "Select Files"
        Me.btnSelectFiles.UseVisualStyleBackColor = True
        '
        'lbxFiles
        '
        Me.lbxFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxFiles.FormattingEnabled = True
        Me.lbxFiles.Location = New System.Drawing.Point(27, 35)
        Me.lbxFiles.Name = "lbxFiles"
        Me.lbxFiles.Size = New System.Drawing.Size(880, 108)
        Me.lbxFiles.TabIndex = 302
        '
        'txtBatchOutput
        '
        Me.txtBatchOutput.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtBatchOutput.Location = New System.Drawing.Point(111, 145)
        Me.txtBatchOutput.Name = "txtBatchOutput"
        Me.txtBatchOutput.ReadOnly = True
        Me.txtBatchOutput.Size = New System.Drawing.Size(715, 20)
        Me.txtBatchOutput.TabIndex = 300
        '
        'btnBatchOutput
        '
        Me.btnBatchOutput.Location = New System.Drawing.Point(832, 145)
        Me.btnBatchOutput.Name = "btnBatchOutput"
        Me.btnBatchOutput.Size = New System.Drawing.Size(75, 23)
        Me.btnBatchOutput.TabIndex = 301
        Me.btnBatchOutput.Text = "Browse"
        Me.btnBatchOutput.UseVisualStyleBackColor = True
        '
        'lblBatchOutput
        '
        Me.lblBatchOutput.AutoSize = True
        Me.lblBatchOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchOutput.Location = New System.Drawing.Point(23, 147)
        Me.lblBatchOutput.Name = "lblBatchOutput"
        Me.lblBatchOutput.Size = New System.Drawing.Size(91, 20)
        Me.lblBatchOutput.TabIndex = 299
        Me.lblBatchOutput.Text = "Output File:"
        '
        'JoinTabPage
        '
        Me.JoinTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.JoinTabPage.Controls.Add(Me.btnClearFilesJoin)
        Me.JoinTabPage.Controls.Add(Me.btnRemoveFileJoin)
        Me.JoinTabPage.Controls.Add(Me.lbJoinStreams)
        Me.JoinTabPage.Controls.Add(Me.btnSelectFilesJoin)
        Me.JoinTabPage.Controls.Add(Me.lbxFilesJoin)
        Me.JoinTabPage.Controls.Add(Me.txtJoinOutput)
        Me.JoinTabPage.Controls.Add(Me.btnJoinOutput)
        Me.JoinTabPage.Controls.Add(Me.lblJoinOutput)
        Me.JoinTabPage.Controls.Add(Me.Panel2)
        Me.JoinTabPage.Location = New System.Drawing.Point(4, 22)
        Me.JoinTabPage.Name = "JoinTabPage"
        Me.JoinTabPage.Size = New System.Drawing.Size(935, 336)
        Me.JoinTabPage.TabIndex = 2
        Me.JoinTabPage.Text = "Join"
        '
        'btnClearFilesJoin
        '
        Me.btnClearFilesJoin.Location = New System.Drawing.Point(254, 6)
        Me.btnClearFilesJoin.Name = "btnClearFilesJoin"
        Me.btnClearFilesJoin.Size = New System.Drawing.Size(70, 23)
        Me.btnClearFilesJoin.TabIndex = 315
        Me.btnClearFilesJoin.Text = "Clear Files"
        Me.btnClearFilesJoin.UseVisualStyleBackColor = True
        '
        'btnRemoveFileJoin
        '
        Me.btnRemoveFileJoin.Location = New System.Drawing.Point(120, 6)
        Me.btnRemoveFileJoin.Name = "btnRemoveFileJoin"
        Me.btnRemoveFileJoin.Size = New System.Drawing.Size(128, 23)
        Me.btnRemoveFileJoin.TabIndex = 314
        Me.btnRemoveFileJoin.Text = "Remove Selected File"
        Me.btnRemoveFileJoin.UseVisualStyleBackColor = True
        '
        'lbJoinStreams
        '
        Me.lbJoinStreams.FormattingEnabled = True
        Me.lbJoinStreams.Location = New System.Drawing.Point(25, 209)
        Me.lbJoinStreams.Name = "lbJoinStreams"
        Me.lbJoinStreams.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbJoinStreams.Size = New System.Drawing.Size(880, 121)
        Me.lbJoinStreams.TabIndex = 313
        '
        'btnSelectFilesJoin
        '
        Me.btnSelectFilesJoin.Location = New System.Drawing.Point(27, 6)
        Me.btnSelectFilesJoin.Name = "btnSelectFilesJoin"
        Me.btnSelectFilesJoin.Size = New System.Drawing.Size(87, 23)
        Me.btnSelectFilesJoin.TabIndex = 312
        Me.btnSelectFilesJoin.Text = "Select Files"
        Me.btnSelectFilesJoin.UseVisualStyleBackColor = True
        '
        'lbxFilesJoin
        '
        Me.lbxFilesJoin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxFilesJoin.FormattingEnabled = True
        Me.lbxFilesJoin.Location = New System.Drawing.Point(27, 35)
        Me.lbxFilesJoin.Name = "lbxFilesJoin"
        Me.lbxFilesJoin.Size = New System.Drawing.Size(880, 108)
        Me.lbxFilesJoin.TabIndex = 311
        '
        'txtJoinOutput
        '
        Me.txtJoinOutput.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtJoinOutput.Location = New System.Drawing.Point(111, 145)
        Me.txtJoinOutput.Name = "txtJoinOutput"
        Me.txtJoinOutput.ReadOnly = True
        Me.txtJoinOutput.Size = New System.Drawing.Size(715, 20)
        Me.txtJoinOutput.TabIndex = 309
        '
        'btnJoinOutput
        '
        Me.btnJoinOutput.Location = New System.Drawing.Point(832, 145)
        Me.btnJoinOutput.Name = "btnJoinOutput"
        Me.btnJoinOutput.Size = New System.Drawing.Size(75, 23)
        Me.btnJoinOutput.TabIndex = 310
        Me.btnJoinOutput.Text = "Browse"
        Me.btnJoinOutput.UseVisualStyleBackColor = True
        '
        'lblJoinOutput
        '
        Me.lblJoinOutput.AutoSize = True
        Me.lblJoinOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJoinOutput.Location = New System.Drawing.Point(23, 147)
        Me.lblJoinOutput.Name = "lblJoinOutput"
        Me.lblJoinOutput.Size = New System.Drawing.Size(91, 20)
        Me.lblJoinOutput.TabIndex = 308
        Me.lblJoinOutput.Text = "Output File:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbJoinOScustom)
        Me.Panel2.Controls.Add(Me.rbJoinOSdefault)
        Me.Panel2.Location = New System.Drawing.Point(27, 174)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(853, 29)
        Me.Panel2.TabIndex = 307
        '
        'rbJoinOScustom
        '
        Me.rbJoinOScustom.AutoSize = True
        Me.rbJoinOScustom.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbJoinOScustom.Location = New System.Drawing.Point(198, 3)
        Me.rbJoinOScustom.Name = "rbJoinOScustom"
        Me.rbJoinOScustom.Size = New System.Drawing.Size(205, 21)
        Me.rbJoinOScustom.TabIndex = 149
        Me.rbJoinOScustom.Text = "Use custom folder for output"
        Me.rbJoinOScustom.UseVisualStyleBackColor = True
        '
        'rbJoinOSdefault
        '
        Me.rbJoinOSdefault.AutoSize = True
        Me.rbJoinOSdefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbJoinOSdefault.Location = New System.Drawing.Point(10, 3)
        Me.rbJoinOSdefault.Name = "rbJoinOSdefault"
        Me.rbJoinOSdefault.Size = New System.Drawing.Size(182, 21)
        Me.rbJoinOSdefault.TabIndex = 147
        Me.rbJoinOSdefault.Text = "Use default output folder"
        Me.rbJoinOSdefault.UseVisualStyleBackColor = True
        '
        'ToolStripContainer2
        '
        Me.ToolStripContainer2.BottomToolStripPanelVisible = False
        '
        'ToolStripContainer2.ContentPanel
        '
        Me.ToolStripContainer2.ContentPanel.Controls.Add(Me.MenuBarStrip)
        Me.ToolStripContainer2.ContentPanel.Size = New System.Drawing.Size(950, 27)
        Me.ToolStripContainer2.LeftToolStripPanelVisible = False
        Me.ToolStripContainer2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer2.Name = "ToolStripContainer2"
        Me.ToolStripContainer2.RightToolStripPanelVisible = False
        Me.ToolStripContainer2.Size = New System.Drawing.Size(950, 27)
        Me.ToolStripContainer2.TabIndex = 1
        Me.ToolStripContainer2.Text = "ToolStripContainer2"
        Me.ToolStripContainer2.TopToolStripPanelVisible = False
        '
        'JoinBackgroundWorker
        '
        '
        'TempBackgroundWorker
        '
        '
        'TempBatchBackgroundWorker
        '
        '
        'Main
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 575)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Name = "Main"
        Me.Text = "Media Converter"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuBarStrip.ResumeLayout(False)
        Me.MenuBarStrip.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.gbCurrentSettings.ResumeLayout(False)
        Me.gbSubtitles.ResumeLayout(False)
        Me.gbSubtitles.PerformLayout()
        Me.gbGeneral.ResumeLayout(False)
        Me.gbGeneral.PerformLayout()
        Me.gbAudio.ResumeLayout(False)
        Me.gbAudio.PerformLayout()
        Me.gbVideo.ResumeLayout(False)
        Me.gbVideo.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.MainTabPage.ResumeLayout(False)
        Me.MainTabPage.PerformLayout()
        Me.PanelOutputFolder.ResumeLayout(False)
        Me.PanelOutputFolder.PerformLayout()
        Me.BatchTabPage.ResumeLayout(False)
        Me.BatchTabPage.PerformLayout()
        Me.JoinTabPage.ResumeLayout(False)
        Me.JoinTabPage.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ToolStripContainer2.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer2.ContentPanel.PerformLayout()
        Me.ToolStripContainer2.ResumeLayout(False)
        Me.ToolStripContainer2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ofdInput As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BatchBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents fbOutput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents BackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnExitApp As System.Windows.Forms.Button
    Friend WithEvents rbBatchOScustom As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbBatchOSdefault As System.Windows.Forms.RadioButton
    Friend WithEvents btnViewFileErrorLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnViewErrorLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnViewLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogsMenu As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnWriteErrorLogFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnWriteLogFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsMenu As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriority As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityRealTime As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityHigh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityAboveNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityBelowNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPriorityIdle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuBarStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents MainTabPage As System.Windows.Forms.TabPage
    Friend WithEvents lbStreams As System.Windows.Forms.ListBox
    Friend WithEvents PanelOutputFolder As System.Windows.Forms.Panel
    Friend WithEvents rbOScustom As System.Windows.Forms.RadioButton
    Friend WithEvents rbOSsame As System.Windows.Forms.RadioButton
    Friend WithEvents rbOSdefault As System.Windows.Forms.RadioButton
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents btnInput As System.Windows.Forms.Button
    Friend WithEvents lblInput As System.Windows.Forms.Label
    Friend WithEvents BatchTabPage As System.Windows.Forms.TabPage
    Friend WithEvents btnClearFiles As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFile As System.Windows.Forms.Button
    Friend WithEvents lbBatchStreams As System.Windows.Forms.ListBox
    Friend WithEvents btnSelectFiles As System.Windows.Forms.Button
    Friend WithEvents lbxFiles As System.Windows.Forms.ListBox
    Friend WithEvents txtBatchOutput As System.Windows.Forms.TextBox
    Friend WithEvents btnBatchOutput As System.Windows.Forms.Button
    Friend WithEvents lblBatchOutput As System.Windows.Forms.Label
    Friend WithEvents ToolStripContainer2 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents JoinTabPage As System.Windows.Forms.TabPage
    Friend WithEvents btnClearFilesJoin As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFileJoin As System.Windows.Forms.Button
    Friend WithEvents lbJoinStreams As System.Windows.Forms.ListBox
    Friend WithEvents btnSelectFilesJoin As System.Windows.Forms.Button
    Friend WithEvents lbxFilesJoin As System.Windows.Forms.ListBox
    Friend WithEvents txtJoinOutput As System.Windows.Forms.TextBox
    Friend WithEvents btnJoinOutput As System.Windows.Forms.Button
    Friend WithEvents lblJoinOutput As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbJoinOScustom As System.Windows.Forms.RadioButton
    Friend WithEvents rbJoinOSdefault As System.Windows.Forms.RadioButton
    Friend WithEvents JoinBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents TempBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents TempBatchBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnClearLogs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnOutputOptions As System.Windows.Forms.Button
    Friend WithEvents gbCurrentSettings As System.Windows.Forms.GroupBox
    Friend WithEvents gbAudio As System.Windows.Forms.GroupBox
    Friend WithEvents gbVideo As System.Windows.Forms.GroupBox
    Friend WithEvents gbGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents lblGeneral As System.Windows.Forms.Label
    Friend WithEvents lblAudio As System.Windows.Forms.Label
    Friend WithEvents lblVideo1 As System.Windows.Forms.Label
    Friend WithEvents lblAudio2 As System.Windows.Forms.Label
    Friend WithEvents btnAdvancedOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbSubtitles As System.Windows.Forms.GroupBox
    Friend WithEvents lblSubtitles As System.Windows.Forms.Label
    Friend WithEvents lblVideo2 As System.Windows.Forms.Label
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnAutoUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnUpdateNow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAbout As System.Windows.Forms.ToolStripMenuItem
End Class
