<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Custom Folder")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.Tv_Explorer = New System.Windows.Forms.TreeView()
        Me.ExplorerMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ChangeCustomFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatDriveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfirmToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReEncodeFilesMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Last3MinutesPerFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tv_ImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBoxCONTROLS = New System.Windows.Forms.GroupBox()
        Me.GBsubCONTROLS = New System.Windows.Forms.GroupBox()
        Me.GroupBoxExportSettings = New System.Windows.Forms.GroupBox()
        Me.RenderTotalTimeLabel = New System.Windows.Forms.Label()
        Me.ResolutionLabel = New System.Windows.Forms.Label()
        Me.MirrorRearEnable = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DisplaySentryIndicator = New System.Windows.Forms.CheckBox()
        Me.DurationProgressBar = New System.Windows.Forms.ProgressBar()
        Me.RenderFileProgress = New System.Windows.Forms.Label()
        Me.RenderBTN = New System.Windows.Forms.Button()
        Me.ThreadsRunningLabel = New System.Windows.Forms.Label()
        Me.FlipLREnable = New System.Windows.Forms.CheckBox()
        Me.VideoQualityLabel = New System.Windows.Forms.Label()
        Me.RenderQuality = New System.Windows.Forms.ComboBox()
        Me.MirrorLREnable = New System.Windows.Forms.CheckBox()
        Me.CloseGroupboxSettings = New System.Windows.Forms.Label()
        Me.GroupBoxControlsWindow = New System.Windows.Forms.GroupBox()
        Me.AspectRatio = New System.Windows.Forms.ComboBox()
        Me.ClipSelectUP = New System.Windows.Forms.Button()
        Me.ClipSelectDOWN = New System.Windows.Forms.Button()
        Me.SettingsBTN = New System.Windows.Forms.Button()
        Me.PlayersEnabledPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CurrentTimeList = New System.Windows.Forms.ListBox()
        Me.CurrentFPS = New System.Windows.Forms.Label()
        Me.RemoveLayoutBtn = New System.Windows.Forms.Button()
        Me.AddLayoutBtn = New System.Windows.Forms.Button()
        Me.SaveLayoutBtn = New System.Windows.Forms.Button()
        Me.AspectName = New System.Windows.Forms.ComboBox()
        Me.RenderPlayerTimecode = New System.Windows.Forms.Label()
        Me.RenderOutTimeLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.AspectRatioLabel = New System.Windows.Forms.Label()
        Me.RenderInTimeLabel = New System.Windows.Forms.Label()
        Me.UPDATELabel = New System.Windows.Forms.Label()
        Me.BtnPAUSE = New System.Windows.Forms.Button()
        Me.AppSettingsButton = New System.Windows.Forms.Button()
        Me.SettingsMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LayoutsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportLayoutsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportLayoutsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LanguageSelection = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DownloadUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewStatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.YourIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.CustomIDToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.GitHubToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TwitterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InstagramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReportABugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FeedBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DonateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreatedByNateMccombToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PCIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PCIDCPUModelMotherboardSNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Disable244BugDetectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewLogFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlsSpeed = New System.Windows.Forms.Label()
        Me.TrackBar2 = New System.Windows.Forms.TrackBar()
        Me.BtnREVERSE = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnPLAY = New System.Windows.Forms.Button()
        Me.SentryModeMarker = New System.Windows.Forms.Label()
        Me.MainPlayerMinTimecode = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MainPlayerMaxTimecode = New System.Windows.Forms.Label()
        Me.MainPlayerTimecode = New System.Windows.Forms.Label()
        Me.Donation = New System.Windows.Forms.Button()
        Me.TimeCodeBar = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBoxNewLayout = New System.Windows.Forms.GroupBox()
        Me.CloseGroupboxNewLayout = New System.Windows.Forms.Label()
        Me.SaveNewLayoutBtn = New System.Windows.Forms.Button()
        Me.NewAspectHeight = New System.Windows.Forms.TextBox()
        Me.NewAspectWidth = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NewLayoutName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBoxEXPLORER = New System.Windows.Forms.GroupBox()
        Me.CustomFolderGroupBox = New System.Windows.Forms.GroupBox()
        Me.CustomFolderCancelBTN = New System.Windows.Forms.Button()
        Me.CustomFolderSaveBTN = New System.Windows.Forms.Button()
        Me.CustomFolderURL = New System.Windows.Forms.TextBox()
        Me.CustomFolderBrowseBTN = New System.Windows.Forms.Button()
        Me.FixTeslaCamGroupBox = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.FixingNumFilesLabel = New System.Windows.Forms.Label()
        Me.FixTeslaCamFFmpegOutput = New System.Windows.Forms.RichTextBox()
        Me.FixTeslaCamBtnDone = New System.Windows.Forms.Button()
        Me.FixTeslaCamProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PREVIEWBox = New System.Windows.Forms.GroupBox()
        Me.CustomFolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.VideoRendering = New System.Windows.Forms.Label()
        Me.FFmpegOutput = New System.Windows.Forms.RichTextBox()
        Me.RenderOutTimeGraphic = New System.Windows.Forms.Label()
        Me.RenderInTimeGraphic = New System.Windows.Forms.Label()
        Me.OneSec = New System.Windows.Forms.Timer(Me.components)
        Me.Panel = New System.Windows.Forms.Panel()
        Me.FileDurations = New System.Windows.Forms.FlowLayoutPanel()
        Me.AnalyzingFilesLabel = New System.Windows.Forms.Label()
        Me.EventTimeCodeBar = New System.Windows.Forms.TrackBar()
        Me.EventSentryModeMarker = New System.Windows.Forms.Label()
        Me.SavedLayouts = New System.Windows.Forms.RichTextBox()
        Me.ImportSettingsFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ExportSettingsFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MaxDurationsList = New System.Windows.Forms.ListBox()
        Me.YouTubeTutorialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayerPreview = New AxWMPLib.AxWindowsMediaPlayer()
        Me.ExplorerMenuStrip.SuspendLayout()
        Me.GroupBoxCONTROLS.SuspendLayout()
        Me.GBsubCONTROLS.SuspendLayout()
        Me.GroupBoxExportSettings.SuspendLayout()
        Me.GroupBoxControlsWindow.SuspendLayout()
        Me.SettingsMenuStrip.SuspendLayout()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimeCodeBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxNewLayout.SuspendLayout()
        Me.GroupBoxEXPLORER.SuspendLayout()
        Me.CustomFolderGroupBox.SuspendLayout()
        Me.FixTeslaCamGroupBox.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PREVIEWBox.SuspendLayout()
        CType(Me.EventTimeCodeBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PlayerPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tv_Explorer
        '
        Me.Tv_Explorer.AllowDrop = True
        Me.Tv_Explorer.ContextMenuStrip = Me.ExplorerMenuStrip
        Me.Tv_Explorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tv_Explorer.HideSelection = False
        Me.Tv_Explorer.HotTracking = True
        Me.Tv_Explorer.Location = New System.Drawing.Point(0, 13)
        Me.Tv_Explorer.Name = "Tv_Explorer"
        TreeNode1.Name = "Node0"
        TreeNode1.Tag = "Custom"
        TreeNode1.Text = "Custom Folder"
        TreeNode1.ToolTipText = "Custom Folder"
        Me.Tv_Explorer.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.Tv_Explorer.ShowNodeToolTips = True
        Me.Tv_Explorer.Size = New System.Drawing.Size(328, 152)
        Me.Tv_Explorer.TabIndex = 1
        '
        'ExplorerMenuStrip
        '
        Me.ExplorerMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeCustomFolderToolStripMenuItem, Me.OpenToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripSeparator1, Me.RefreshToolStripMenuItem, Me.FormatDriveToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.ToolStripSeparator8, Me.ReEncodeFilesMenuItem})
        Me.ExplorerMenuStrip.Name = "ExplorerMenuStrip"
        Me.ExplorerMenuStrip.Size = New System.Drawing.Size(197, 192)
        '
        'ChangeCustomFolderToolStripMenuItem
        '
        Me.ChangeCustomFolderToolStripMenuItem.Name = "ChangeCustomFolderToolStripMenuItem"
        Me.ChangeCustomFolderToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ChangeCustomFolderToolStripMenuItem.Text = "Change Custom Folder"
        Me.ChangeCustomFolderToolStripMenuItem.Visible = False
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Enabled = False
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(193, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'FormatDriveToolStripMenuItem
        '
        Me.FormatDriveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfirmToolStripMenuItem})
        Me.FormatDriveToolStripMenuItem.Name = "FormatDriveToolStripMenuItem"
        Me.FormatDriveToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.FormatDriveToolStripMenuItem.Text = "Format Drive"
        Me.FormatDriveToolStripMenuItem.Visible = False
        '
        'ConfirmToolStripMenuItem
        '
        Me.ConfirmToolStripMenuItem.Name = "ConfirmToolStripMenuItem"
        Me.ConfirmToolStripMenuItem.Size = New System.Drawing.Size(118, 22)
        Me.ConfirmToolStripMenuItem.Text = "Confirm"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        Me.DeleteToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(193, 6)
        '
        'ReEncodeFilesMenuItem
        '
        Me.ReEncodeFilesMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllFilesToolStripMenuItem, Me.Last3MinutesPerFolderToolStripMenuItem})
        Me.ReEncodeFilesMenuItem.Name = "ReEncodeFilesMenuItem"
        Me.ReEncodeFilesMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ReEncodeFilesMenuItem.Text = "Re-Encode Files"
        Me.ReEncodeFilesMenuItem.Visible = False
        '
        'AllFilesToolStripMenuItem
        '
        Me.AllFilesToolStripMenuItem.Name = "AllFilesToolStripMenuItem"
        Me.AllFilesToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.AllFilesToolStripMenuItem.Text = "All files(3x Longer To Encode)"
        '
        'Last3MinutesPerFolderToolStripMenuItem
        '
        Me.Last3MinutesPerFolderToolStripMenuItem.Name = "Last3MinutesPerFolderToolStripMenuItem"
        Me.Last3MinutesPerFolderToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.Last3MinutesPerFolderToolStripMenuItem.Text = "Last 3 Minutes (Per Folder)"
        '
        'Tv_ImgList
        '
        Me.Tv_ImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.Tv_ImgList.ImageSize = New System.Drawing.Size(16, 16)
        Me.Tv_ImgList.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBoxCONTROLS
        '
        Me.GroupBoxCONTROLS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCONTROLS.Controls.Add(Me.GroupBoxNewLayout)
        Me.GroupBoxCONTROLS.Controls.Add(Me.GBsubCONTROLS)
        Me.GroupBoxCONTROLS.ForeColor = System.Drawing.Color.White
        Me.GroupBoxCONTROLS.Location = New System.Drawing.Point(332, 324)
        Me.GroupBoxCONTROLS.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBoxCONTROLS.Name = "GroupBoxCONTROLS"
        Me.GroupBoxCONTROLS.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxCONTROLS.Size = New System.Drawing.Size(580, 166)
        Me.GroupBoxCONTROLS.TabIndex = 5
        Me.GroupBoxCONTROLS.TabStop = False
        Me.GroupBoxCONTROLS.Text = "Controls"
        '
        'GBsubCONTROLS
        '
        Me.GBsubCONTROLS.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GBsubCONTROLS.Controls.Add(Me.GroupBoxExportSettings)
        Me.GBsubCONTROLS.Controls.Add(Me.GroupBoxControlsWindow)
        Me.GBsubCONTROLS.Controls.Add(Me.RemoveLayoutBtn)
        Me.GBsubCONTROLS.Controls.Add(Me.AddLayoutBtn)
        Me.GBsubCONTROLS.Controls.Add(Me.SaveLayoutBtn)
        Me.GBsubCONTROLS.Controls.Add(Me.AspectName)
        Me.GBsubCONTROLS.Controls.Add(Me.RenderPlayerTimecode)
        Me.GBsubCONTROLS.Controls.Add(Me.RenderOutTimeLabel)
        Me.GBsubCONTROLS.Controls.Add(Me.Label2)
        Me.GBsubCONTROLS.Controls.Add(Me.AspectRatioLabel)
        Me.GBsubCONTROLS.Controls.Add(Me.RenderInTimeLabel)
        Me.GBsubCONTROLS.Controls.Add(Me.UPDATELabel)
        Me.GBsubCONTROLS.Controls.Add(Me.BtnPAUSE)
        Me.GBsubCONTROLS.Controls.Add(Me.AppSettingsButton)
        Me.GBsubCONTROLS.Controls.Add(Me.ControlsSpeed)
        Me.GBsubCONTROLS.Controls.Add(Me.TrackBar2)
        Me.GBsubCONTROLS.Controls.Add(Me.BtnREVERSE)
        Me.GBsubCONTROLS.Controls.Add(Me.Label3)
        Me.GBsubCONTROLS.Controls.Add(Me.BtnPLAY)
        Me.GBsubCONTROLS.Controls.Add(Me.SentryModeMarker)
        Me.GBsubCONTROLS.Controls.Add(Me.MainPlayerMinTimecode)
        Me.GBsubCONTROLS.Controls.Add(Me.Label7)
        Me.GBsubCONTROLS.Controls.Add(Me.MainPlayerMaxTimecode)
        Me.GBsubCONTROLS.Controls.Add(Me.MainPlayerTimecode)
        Me.GBsubCONTROLS.Controls.Add(Me.Donation)
        Me.GBsubCONTROLS.Controls.Add(Me.TimeCodeBar)
        Me.GBsubCONTROLS.Controls.Add(Me.Label6)
        Me.GBsubCONTROLS.Controls.Add(Me.Label1)
        Me.GBsubCONTROLS.Location = New System.Drawing.Point(1, 11)
        Me.GBsubCONTROLS.Name = "GBsubCONTROLS"
        Me.GBsubCONTROLS.Size = New System.Drawing.Size(579, 154)
        Me.GBsubCONTROLS.TabIndex = 19
        Me.GBsubCONTROLS.TabStop = False
        '
        'GroupBoxExportSettings
        '
        Me.GroupBoxExportSettings.Controls.Add(Me.RenderTotalTimeLabel)
        Me.GroupBoxExportSettings.Controls.Add(Me.ResolutionLabel)
        Me.GroupBoxExportSettings.Controls.Add(Me.MirrorRearEnable)
        Me.GroupBoxExportSettings.Controls.Add(Me.Label4)
        Me.GroupBoxExportSettings.Controls.Add(Me.DisplaySentryIndicator)
        Me.GroupBoxExportSettings.Controls.Add(Me.DurationProgressBar)
        Me.GroupBoxExportSettings.Controls.Add(Me.RenderFileProgress)
        Me.GroupBoxExportSettings.Controls.Add(Me.RenderBTN)
        Me.GroupBoxExportSettings.Controls.Add(Me.ThreadsRunningLabel)
        Me.GroupBoxExportSettings.Controls.Add(Me.FlipLREnable)
        Me.GroupBoxExportSettings.Controls.Add(Me.VideoQualityLabel)
        Me.GroupBoxExportSettings.Controls.Add(Me.RenderQuality)
        Me.GroupBoxExportSettings.Controls.Add(Me.MirrorLREnable)
        Me.GroupBoxExportSettings.Controls.Add(Me.CloseGroupboxSettings)
        Me.GroupBoxExportSettings.ForeColor = System.Drawing.Color.White
        Me.GroupBoxExportSettings.Location = New System.Drawing.Point(375, 11)
        Me.GroupBoxExportSettings.Name = "GroupBoxExportSettings"
        Me.GroupBoxExportSettings.Size = New System.Drawing.Size(195, 135)
        Me.GroupBoxExportSettings.TabIndex = 26
        Me.GroupBoxExportSettings.TabStop = False
        Me.GroupBoxExportSettings.Text = "Export Settings"
        Me.GroupBoxExportSettings.Visible = False
        '
        'RenderTotalTimeLabel
        '
        Me.RenderTotalTimeLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RenderTotalTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RenderTotalTimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderTotalTimeLabel.ForeColor = System.Drawing.Color.White
        Me.RenderTotalTimeLabel.Location = New System.Drawing.Point(132, 60)
        Me.RenderTotalTimeLabel.Name = "RenderTotalTimeLabel"
        Me.RenderTotalTimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RenderTotalTimeLabel.Size = New System.Drawing.Size(53, 15)
        Me.RenderTotalTimeLabel.TabIndex = 3
        Me.RenderTotalTimeLabel.Text = "00:00.00"
        Me.RenderTotalTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ResolutionLabel
        '
        Me.ResolutionLabel.ForeColor = System.Drawing.Color.Black
        Me.ResolutionLabel.Location = New System.Drawing.Point(56, 99)
        Me.ResolutionLabel.Name = "ResolutionLabel"
        Me.ResolutionLabel.Size = New System.Drawing.Size(68, 13)
        Me.ResolutionLabel.TabIndex = 28
        Me.ResolutionLabel.Text = "1920x1080"
        Me.ResolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MirrorRearEnable
        '
        Me.MirrorRearEnable.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MirrorRearEnable.AutoSize = True
        Me.MirrorRearEnable.ForeColor = System.Drawing.Color.Black
        Me.MirrorRearEnable.Location = New System.Drawing.Point(5, 48)
        Me.MirrorRearEnable.Name = "MirrorRearEnable"
        Me.MirrorRearEnable.Size = New System.Drawing.Size(80, 17)
        Me.MirrorRearEnable.TabIndex = 19
        Me.MirrorRearEnable.TabStop = False
        Me.MirrorRearEnable.Text = "Mirror Back"
        Me.MirrorRearEnable.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(62, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Resolution"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DisplaySentryIndicator
        '
        Me.DisplaySentryIndicator.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DisplaySentryIndicator.AutoSize = True
        Me.DisplaySentryIndicator.Checked = True
        Me.DisplaySentryIndicator.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DisplaySentryIndicator.ForeColor = System.Drawing.Color.Black
        Me.DisplaySentryIndicator.Location = New System.Drawing.Point(5, 14)
        Me.DisplaySentryIndicator.Name = "DisplaySentryIndicator"
        Me.DisplaySentryIndicator.Size = New System.Drawing.Size(137, 17)
        Me.DisplaySentryIndicator.TabIndex = 18
        Me.DisplaySentryIndicator.Text = "Display Sentry Indicator"
        Me.DisplaySentryIndicator.UseVisualStyleBackColor = True
        '
        'DurationProgressBar
        '
        Me.DurationProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DurationProgressBar.BackColor = System.Drawing.Color.DimGray
        Me.DurationProgressBar.Location = New System.Drawing.Point(66, 121)
        Me.DurationProgressBar.MarqueeAnimationSpeed = 10
        Me.DurationProgressBar.Name = "DurationProgressBar"
        Me.DurationProgressBar.Size = New System.Drawing.Size(77, 10)
        Me.DurationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.DurationProgressBar.TabIndex = 4
        '
        'RenderFileProgress
        '
        Me.RenderFileProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RenderFileProgress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RenderFileProgress.Location = New System.Drawing.Point(142, 119)
        Me.RenderFileProgress.Name = "RenderFileProgress"
        Me.RenderFileProgress.Size = New System.Drawing.Size(52, 13)
        Me.RenderFileProgress.TabIndex = 9
        '
        'RenderBTN
        '
        Me.RenderBTN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RenderBTN.Enabled = False
        Me.RenderBTN.ForeColor = System.Drawing.Color.Black
        Me.RenderBTN.Location = New System.Drawing.Point(5, 87)
        Me.RenderBTN.Name = "RenderBTN"
        Me.RenderBTN.Size = New System.Drawing.Size(48, 21)
        Me.RenderBTN.TabIndex = 6
        Me.RenderBTN.Text = "Export"
        Me.RenderBTN.UseVisualStyleBackColor = True
        '
        'ThreadsRunningLabel
        '
        Me.ThreadsRunningLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ThreadsRunningLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ThreadsRunningLabel.Location = New System.Drawing.Point(1, 119)
        Me.ThreadsRunningLabel.Name = "ThreadsRunningLabel"
        Me.ThreadsRunningLabel.Size = New System.Drawing.Size(68, 13)
        Me.ThreadsRunningLabel.TabIndex = 10
        Me.ThreadsRunningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlipLREnable
        '
        Me.FlipLREnable.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FlipLREnable.AutoSize = True
        Me.FlipLREnable.ForeColor = System.Drawing.Color.Black
        Me.FlipLREnable.Location = New System.Drawing.Point(5, 65)
        Me.FlipLREnable.Name = "FlipLREnable"
        Me.FlipLREnable.Size = New System.Drawing.Size(70, 17)
        Me.FlipLREnable.TabIndex = 11
        Me.FlipLREnable.TabStop = False
        Me.FlipLREnable.Text = "Swap LR"
        Me.FlipLREnable.UseVisualStyleBackColor = True
        '
        'VideoQualityLabel
        '
        Me.VideoQualityLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.VideoQualityLabel.AutoSize = True
        Me.VideoQualityLabel.ForeColor = System.Drawing.Color.Black
        Me.VideoQualityLabel.Location = New System.Drawing.Point(125, 80)
        Me.VideoQualityLabel.Name = "VideoQualityLabel"
        Me.VideoQualityLabel.Size = New System.Drawing.Size(69, 13)
        Me.VideoQualityLabel.TabIndex = 9
        Me.VideoQualityLabel.Text = "Video Quality"
        '
        'RenderQuality
        '
        Me.RenderQuality.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RenderQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RenderQuality.ForeColor = System.Drawing.Color.Black
        Me.RenderQuality.FormattingEnabled = True
        Me.RenderQuality.Items.AddRange(New Object() {"Max", "High", "Medium", "Low"})
        Me.RenderQuality.Location = New System.Drawing.Point(127, 96)
        Me.RenderQuality.Name = "RenderQuality"
        Me.RenderQuality.Size = New System.Drawing.Size(63, 21)
        Me.RenderQuality.TabIndex = 10
        Me.RenderQuality.TabStop = False
        '
        'MirrorLREnable
        '
        Me.MirrorLREnable.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MirrorLREnable.AutoSize = True
        Me.MirrorLREnable.ForeColor = System.Drawing.Color.Black
        Me.MirrorLREnable.Location = New System.Drawing.Point(5, 31)
        Me.MirrorLREnable.Name = "MirrorLREnable"
        Me.MirrorLREnable.Size = New System.Drawing.Size(69, 17)
        Me.MirrorLREnable.TabIndex = 12
        Me.MirrorLREnable.TabStop = False
        Me.MirrorLREnable.Text = "Mirror LR"
        Me.MirrorLREnable.UseVisualStyleBackColor = True
        '
        'CloseGroupboxSettings
        '
        Me.CloseGroupboxSettings.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CloseGroupboxSettings.AutoSize = True
        Me.CloseGroupboxSettings.BackColor = System.Drawing.Color.Red
        Me.CloseGroupboxSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseGroupboxSettings.Location = New System.Drawing.Point(184, 1)
        Me.CloseGroupboxSettings.Name = "CloseGroupboxSettings"
        Me.CloseGroupboxSettings.Size = New System.Drawing.Size(11, 12)
        Me.CloseGroupboxSettings.TabIndex = 3
        Me.CloseGroupboxSettings.Text = "X"
        '
        'GroupBoxControlsWindow
        '
        Me.GroupBoxControlsWindow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GroupBoxControlsWindow.Controls.Add(Me.AspectRatio)
        Me.GroupBoxControlsWindow.Controls.Add(Me.ClipSelectUP)
        Me.GroupBoxControlsWindow.Controls.Add(Me.ClipSelectDOWN)
        Me.GroupBoxControlsWindow.Controls.Add(Me.SettingsBTN)
        Me.GroupBoxControlsWindow.Controls.Add(Me.PlayersEnabledPanel)
        Me.GroupBoxControlsWindow.Controls.Add(Me.CurrentTimeList)
        Me.GroupBoxControlsWindow.Controls.Add(Me.CurrentFPS)
        Me.GroupBoxControlsWindow.ForeColor = System.Drawing.Color.White
        Me.GroupBoxControlsWindow.Location = New System.Drawing.Point(375, 11)
        Me.GroupBoxControlsWindow.Name = "GroupBoxControlsWindow"
        Me.GroupBoxControlsWindow.Size = New System.Drawing.Size(195, 136)
        Me.GroupBoxControlsWindow.TabIndex = 17
        Me.GroupBoxControlsWindow.TabStop = False
        Me.GroupBoxControlsWindow.Text = "Selected Event"
        '
        'AspectRatio
        '
        Me.AspectRatio.FormattingEnabled = True
        Me.AspectRatio.Location = New System.Drawing.Point(85, -3)
        Me.AspectRatio.Name = "AspectRatio"
        Me.AspectRatio.Size = New System.Drawing.Size(85, 21)
        Me.AspectRatio.TabIndex = 6
        Me.AspectRatio.Visible = False
        '
        'ClipSelectUP
        '
        Me.ClipSelectUP.ForeColor = System.Drawing.Color.Black
        Me.ClipSelectUP.Location = New System.Drawing.Point(72, 38)
        Me.ClipSelectUP.Name = "ClipSelectUP"
        Me.ClipSelectUP.Size = New System.Drawing.Size(25, 26)
        Me.ClipSelectUP.TabIndex = 4
        Me.ClipSelectUP.TabStop = False
        Me.ClipSelectUP.Text = "/\"
        Me.ClipSelectUP.UseVisualStyleBackColor = True
        '
        'ClipSelectDOWN
        '
        Me.ClipSelectDOWN.ForeColor = System.Drawing.Color.Black
        Me.ClipSelectDOWN.Location = New System.Drawing.Point(72, 71)
        Me.ClipSelectDOWN.Name = "ClipSelectDOWN"
        Me.ClipSelectDOWN.Size = New System.Drawing.Size(25, 26)
        Me.ClipSelectDOWN.TabIndex = 5
        Me.ClipSelectDOWN.TabStop = False
        Me.ClipSelectDOWN.Text = "\/"
        Me.ClipSelectDOWN.UseVisualStyleBackColor = True
        '
        'SettingsBTN
        '
        Me.SettingsBTN.Enabled = False
        Me.SettingsBTN.ForeColor = System.Drawing.Color.Black
        Me.SettingsBTN.Location = New System.Drawing.Point(5, 107)
        Me.SettingsBTN.Name = "SettingsBTN"
        Me.SettingsBTN.Size = New System.Drawing.Size(92, 23)
        Me.SettingsBTN.TabIndex = 8
        Me.SettingsBTN.Text = "Export"
        Me.SettingsBTN.UseVisualStyleBackColor = True
        Me.SettingsBTN.Visible = False
        '
        'PlayersEnabledPanel
        '
        Me.PlayersEnabledPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlayersEnabledPanel.AutoScroll = True
        Me.PlayersEnabledPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.PlayersEnabledPanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PlayersEnabledPanel.Location = New System.Drawing.Point(97, 9)
        Me.PlayersEnabledPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.PlayersEnabledPanel.Name = "PlayersEnabledPanel"
        Me.PlayersEnabledPanel.Size = New System.Drawing.Size(97, 125)
        Me.PlayersEnabledPanel.TabIndex = 20
        Me.PlayersEnabledPanel.WrapContents = False
        '
        'CurrentTimeList
        '
        Me.CurrentTimeList.FormattingEnabled = True
        Me.CurrentTimeList.Location = New System.Drawing.Point(5, 33)
        Me.CurrentTimeList.Name = "CurrentTimeList"
        Me.CurrentTimeList.Size = New System.Drawing.Size(67, 69)
        Me.CurrentTimeList.TabIndex = 2
        '
        'CurrentFPS
        '
        Me.CurrentFPS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CurrentFPS.Location = New System.Drawing.Point(6, 13)
        Me.CurrentFPS.Name = "CurrentFPS"
        Me.CurrentFPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CurrentFPS.Size = New System.Drawing.Size(63, 18)
        Me.CurrentFPS.TabIndex = 25
        Me.CurrentFPS.Text = "0 FPS"
        Me.CurrentFPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RemoveLayoutBtn
        '
        Me.RemoveLayoutBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RemoveLayoutBtn.BackColor = System.Drawing.Color.DimGray
        Me.RemoveLayoutBtn.BackgroundImage = Global.TeslaCam_Viewer.My.Resources.Resources.Minus_Normal_5050
        Me.RemoveLayoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RemoveLayoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RemoveLayoutBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoveLayoutBtn.ForeColor = System.Drawing.Color.Black
        Me.RemoveLayoutBtn.Location = New System.Drawing.Point(254, 107)
        Me.RemoveLayoutBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.RemoveLayoutBtn.Name = "RemoveLayoutBtn"
        Me.RemoveLayoutBtn.Size = New System.Drawing.Size(26, 23)
        Me.RemoveLayoutBtn.TabIndex = 29
        Me.RemoveLayoutBtn.UseVisualStyleBackColor = False
        '
        'AddLayoutBtn
        '
        Me.AddLayoutBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.AddLayoutBtn.BackColor = System.Drawing.Color.DimGray
        Me.AddLayoutBtn.BackgroundImage = Global.TeslaCam_Viewer.My.Resources.Resources.Plus_Normal_5050
        Me.AddLayoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.AddLayoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddLayoutBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddLayoutBtn.ForeColor = System.Drawing.Color.Black
        Me.AddLayoutBtn.Location = New System.Drawing.Point(224, 107)
        Me.AddLayoutBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.AddLayoutBtn.Name = "AddLayoutBtn"
        Me.AddLayoutBtn.Size = New System.Drawing.Size(26, 23)
        Me.AddLayoutBtn.TabIndex = 28
        Me.AddLayoutBtn.UseVisualStyleBackColor = False
        '
        'SaveLayoutBtn
        '
        Me.SaveLayoutBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SaveLayoutBtn.BackColor = System.Drawing.Color.DimGray
        Me.SaveLayoutBtn.BackgroundImage = Global.TeslaCam_Viewer.My.Resources.Resources.Disk_Normal_5050
        Me.SaveLayoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.SaveLayoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveLayoutBtn.Location = New System.Drawing.Point(254, 82)
        Me.SaveLayoutBtn.Name = "SaveLayoutBtn"
        Me.SaveLayoutBtn.Size = New System.Drawing.Size(26, 23)
        Me.SaveLayoutBtn.TabIndex = 27
        Me.SaveLayoutBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.SaveLayoutBtn.UseVisualStyleBackColor = False
        '
        'AspectName
        '
        Me.AspectName.FormattingEnabled = True
        Me.AspectName.Location = New System.Drawing.Point(117, 83)
        Me.AspectName.Name = "AspectName"
        Me.AspectName.Size = New System.Drawing.Size(133, 21)
        Me.AspectName.TabIndex = 7
        '
        'RenderPlayerTimecode
        '
        Me.RenderPlayerTimecode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RenderPlayerTimecode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RenderPlayerTimecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderPlayerTimecode.ForeColor = System.Drawing.Color.White
        Me.RenderPlayerTimecode.Location = New System.Drawing.Point(128, 9)
        Me.RenderPlayerTimecode.Name = "RenderPlayerTimecode"
        Me.RenderPlayerTimecode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RenderPlayerTimecode.Size = New System.Drawing.Size(53, 15)
        Me.RenderPlayerTimecode.TabIndex = 4
        Me.RenderPlayerTimecode.Text = "00:00.00"
        Me.RenderPlayerTimecode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RenderOutTimeLabel
        '
        Me.RenderOutTimeLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RenderOutTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RenderOutTimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderOutTimeLabel.ForeColor = System.Drawing.Color.White
        Me.RenderOutTimeLabel.Location = New System.Drawing.Point(247, 9)
        Me.RenderOutTimeLabel.Name = "RenderOutTimeLabel"
        Me.RenderOutTimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RenderOutTimeLabel.Size = New System.Drawing.Size(53, 15)
        Me.RenderOutTimeLabel.TabIndex = 2
        Me.RenderOutTimeLabel.Text = "00:00.00"
        Me.RenderOutTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(339, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "1/10x"
        '
        'AspectRatioLabel
        '
        Me.AspectRatioLabel.AutoSize = True
        Me.AspectRatioLabel.ForeColor = System.Drawing.Color.Black
        Me.AspectRatioLabel.Location = New System.Drawing.Point(39, 87)
        Me.AspectRatioLabel.Name = "AspectRatioLabel"
        Me.AspectRatioLabel.Size = New System.Drawing.Size(81, 13)
        Me.AspectRatioLabel.TabIndex = 7
        Me.AspectRatioLabel.Text = "Camera Layout:"
        '
        'RenderInTimeLabel
        '
        Me.RenderInTimeLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RenderInTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RenderInTimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderInTimeLabel.ForeColor = System.Drawing.Color.White
        Me.RenderInTimeLabel.Location = New System.Drawing.Point(5, 9)
        Me.RenderInTimeLabel.Name = "RenderInTimeLabel"
        Me.RenderInTimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RenderInTimeLabel.Size = New System.Drawing.Size(53, 15)
        Me.RenderInTimeLabel.TabIndex = 1
        Me.RenderInTimeLabel.Text = "00:00.00"
        Me.RenderInTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UPDATELabel
        '
        Me.UPDATELabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.UPDATELabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UPDATELabel.ForeColor = System.Drawing.SystemColors.Control
        Me.UPDATELabel.Location = New System.Drawing.Point(31, 132)
        Me.UPDATELabel.Name = "UPDATELabel"
        Me.UPDATELabel.Size = New System.Drawing.Size(249, 18)
        Me.UPDATELabel.TabIndex = 19
        Me.UPDATELabel.Text = "Check for Updates"
        Me.UPDATELabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnPAUSE
        '
        Me.BtnPAUSE.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.BtnPAUSE.ForeColor = System.Drawing.Color.Black
        Me.BtnPAUSE.Location = New System.Drawing.Point(207, 57)
        Me.BtnPAUSE.Name = "BtnPAUSE"
        Me.BtnPAUSE.Size = New System.Drawing.Size(73, 23)
        Me.BtnPAUSE.TabIndex = 4
        Me.BtnPAUSE.Text = "Pause"
        Me.BtnPAUSE.UseVisualStyleBackColor = True
        '
        'AppSettingsButton
        '
        Me.AppSettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.AppSettingsButton.BackColor = System.Drawing.Color.DimGray
        Me.AppSettingsButton.BackgroundImage = Global.TeslaCam_Viewer.My.Resources.Resources.SettingsIcon
        Me.AppSettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.AppSettingsButton.ContextMenuStrip = Me.SettingsMenuStrip
        Me.AppSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AppSettingsButton.Location = New System.Drawing.Point(3, 127)
        Me.AppSettingsButton.Name = "AppSettingsButton"
        Me.AppSettingsButton.Size = New System.Drawing.Size(26, 23)
        Me.AppSettingsButton.TabIndex = 25
        Me.AppSettingsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AppSettingsButton.UseVisualStyleBackColor = False
        '
        'SettingsMenuStrip
        '
        Me.SettingsMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LayoutsToolStripMenuItem, Me.LanToolStripMenuItem, Me.ToolStripSeparator2, Me.DownloadUpdateToolStripMenuItem, Me.ViewStatsToolStripMenuItem, Me.ToolStripSeparator3, Me.GitHubToolStripMenuItem, Me.YouTubeTutorialToolStripMenuItem, Me.TwitterToolStripMenuItem, Me.InstagramToolStripMenuItem, Me.ToolStripSeparator4, Me.ReportABugToolStripMenuItem, Me.FeedBackToolStripMenuItem, Me.DonateToolStripMenuItem, Me.ToolStripSeparator5, Me.AboutToolStripMenuItem, Me.Disable244BugDetectToolStripMenuItem, Me.ViewLogFilesToolStripMenuItem})
        Me.SettingsMenuStrip.Name = "SettingsMenuStrip"
        Me.SettingsMenuStrip.Size = New System.Drawing.Size(198, 336)
        '
        'LayoutsToolStripMenuItem
        '
        Me.LayoutsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportLayoutsToolStripMenuItem, Me.ExportLayoutsToolStripMenuItem})
        Me.LayoutsToolStripMenuItem.Name = "LayoutsToolStripMenuItem"
        Me.LayoutsToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.LayoutsToolStripMenuItem.Text = "Layouts"
        '
        'ImportLayoutsToolStripMenuItem
        '
        Me.ImportLayoutsToolStripMenuItem.Name = "ImportLayoutsToolStripMenuItem"
        Me.ImportLayoutsToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ImportLayoutsToolStripMenuItem.Text = "Import Layouts"
        '
        'ExportLayoutsToolStripMenuItem
        '
        Me.ExportLayoutsToolStripMenuItem.Name = "ExportLayoutsToolStripMenuItem"
        Me.ExportLayoutsToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ExportLayoutsToolStripMenuItem.Text = "Export Layouts"
        '
        'LanToolStripMenuItem
        '
        Me.LanToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LanguageSelection})
        Me.LanToolStripMenuItem.Name = "LanToolStripMenuItem"
        Me.LanToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.LanToolStripMenuItem.Text = "Language"
        Me.LanToolStripMenuItem.Visible = False
        '
        'LanguageSelection
        '
        Me.LanguageSelection.Items.AddRange(New Object() {"English", "Español", "German", "Dutch"})
        Me.LanguageSelection.Name = "LanguageSelection"
        Me.LanguageSelection.Size = New System.Drawing.Size(121, 23)
        Me.LanguageSelection.Text = "English"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(194, 6)
        '
        'DownloadUpdateToolStripMenuItem
        '
        Me.DownloadUpdateToolStripMenuItem.Name = "DownloadUpdateToolStripMenuItem"
        Me.DownloadUpdateToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DownloadUpdateToolStripMenuItem.Text = "Download Update"
        '
        'ViewStatsToolStripMenuItem
        '
        Me.ViewStatsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.YourIDToolStripMenuItem, Me.ToolStripSeparator7, Me.CopyToolStripMenuItem1, Me.ToolStripSeparator6, Me.CustomIDToolStripTextBox})
        Me.ViewStatsToolStripMenuItem.Name = "ViewStatsToolStripMenuItem"
        Me.ViewStatsToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ViewStatsToolStripMenuItem.Text = "View Stats"
        '
        'YourIDToolStripMenuItem
        '
        Me.YourIDToolStripMenuItem.Name = "YourIDToolStripMenuItem"
        Me.YourIDToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.YourIDToolStripMenuItem.Text = "Your ID:"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(187, 6)
        '
        'CopyToolStripMenuItem1
        '
        Me.CopyToolStripMenuItem1.Name = "CopyToolStripMenuItem1"
        Me.CopyToolStripMenuItem1.Size = New System.Drawing.Size(190, 22)
        Me.CopyToolStripMenuItem1.Text = "Copy ID"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(187, 6)
        '
        'CustomIDToolStripTextBox
        '
        Me.CustomIDToolStripTextBox.Name = "CustomIDToolStripTextBox"
        Me.CustomIDToolStripTextBox.Size = New System.Drawing.Size(130, 23)
        Me.CustomIDToolStripTextBox.Text = "Choose Custom ID"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(194, 6)
        '
        'GitHubToolStripMenuItem
        '
        Me.GitHubToolStripMenuItem.Name = "GitHubToolStripMenuItem"
        Me.GitHubToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.GitHubToolStripMenuItem.Text = "GitHub"
        '
        'TwitterToolStripMenuItem
        '
        Me.TwitterToolStripMenuItem.Name = "TwitterToolStripMenuItem"
        Me.TwitterToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.TwitterToolStripMenuItem.Text = "Twitter"
        '
        'InstagramToolStripMenuItem
        '
        Me.InstagramToolStripMenuItem.Name = "InstagramToolStripMenuItem"
        Me.InstagramToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.InstagramToolStripMenuItem.Text = "Instagram"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(194, 6)
        '
        'ReportABugToolStripMenuItem
        '
        Me.ReportABugToolStripMenuItem.Name = "ReportABugToolStripMenuItem"
        Me.ReportABugToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReportABugToolStripMenuItem.Text = "Report A Bug"
        '
        'FeedBackToolStripMenuItem
        '
        Me.FeedBackToolStripMenuItem.Name = "FeedBackToolStripMenuItem"
        Me.FeedBackToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.FeedBackToolStripMenuItem.Text = "Feedback"
        '
        'DonateToolStripMenuItem
        '
        Me.DonateToolStripMenuItem.Name = "DonateToolStripMenuItem"
        Me.DonateToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DonateToolStripMenuItem.Text = "Donate"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(194, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VersionToolStripMenuItem, Me.CreatedByNateMccombToolStripMenuItem, Me.PCIDToolStripMenuItem, Me.PCIDCPUModelMotherboardSNToolStripMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'VersionToolStripMenuItem
        '
        Me.VersionToolStripMenuItem.Name = "VersionToolStripMenuItem"
        Me.VersionToolStripMenuItem.Size = New System.Drawing.Size(316, 22)
        Me.VersionToolStripMenuItem.Text = "Version"
        '
        'CreatedByNateMccombToolStripMenuItem
        '
        Me.CreatedByNateMccombToolStripMenuItem.Name = "CreatedByNateMccombToolStripMenuItem"
        Me.CreatedByNateMccombToolStripMenuItem.Size = New System.Drawing.Size(316, 22)
        Me.CreatedByNateMccombToolStripMenuItem.Text = "Created By: Nate Mccomb"
        '
        'PCIDToolStripMenuItem
        '
        Me.PCIDToolStripMenuItem.Name = "PCIDToolStripMenuItem"
        Me.PCIDToolStripMenuItem.Size = New System.Drawing.Size(316, 22)
        Me.PCIDToolStripMenuItem.Text = "PC ID: "
        '
        'PCIDCPUModelMotherboardSNToolStripMenuItem
        '
        Me.PCIDCPUModelMotherboardSNToolStripMenuItem.Name = "PCIDCPUModelMotherboardSNToolStripMenuItem"
        Me.PCIDCPUModelMotherboardSNToolStripMenuItem.Size = New System.Drawing.Size(316, 22)
        Me.PCIDCPUModelMotherboardSNToolStripMenuItem.Text = "PC ID = 'CPUID'+'Motherboard SerialNumber'"
        '
        'Disable244BugDetectToolStripMenuItem
        '
        Me.Disable244BugDetectToolStripMenuItem.Name = "Disable244BugDetectToolStripMenuItem"
        Me.Disable244BugDetectToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.Disable244BugDetectToolStripMenuItem.Text = "Disable 24.4 Bug Detect"
        '
        'ViewLogFilesToolStripMenuItem
        '
        Me.ViewLogFilesToolStripMenuItem.Name = "ViewLogFilesToolStripMenuItem"
        Me.ViewLogFilesToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ViewLogFilesToolStripMenuItem.Text = "View Log Files"
        '
        'ControlsSpeed
        '
        Me.ControlsSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ControlsSpeed.ForeColor = System.Drawing.Color.Black
        Me.ControlsSpeed.Location = New System.Drawing.Point(284, 135)
        Me.ControlsSpeed.Name = "ControlsSpeed"
        Me.ControlsSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ControlsSpeed.Size = New System.Drawing.Size(91, 13)
        Me.ControlsSpeed.TabIndex = 11
        Me.ControlsSpeed.Text = "Speed"
        Me.ControlsSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TrackBar2
        '
        Me.TrackBar2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TrackBar2.Cursor = System.Windows.Forms.Cursors.NoMoveVert
        Me.TrackBar2.LargeChange = 10
        Me.TrackBar2.Location = New System.Drawing.Point(302, 9)
        Me.TrackBar2.Maximum = 50
        Me.TrackBar2.Minimum = 1
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar2.Size = New System.Drawing.Size(45, 133)
        Me.TrackBar2.TabIndex = 7
        Me.TrackBar2.TabStop = False
        Me.TrackBar2.TickFrequency = 10
        Me.TrackBar2.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBar2.Value = 10
        '
        'BtnREVERSE
        '
        Me.BtnREVERSE.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.BtnREVERSE.ForeColor = System.Drawing.Color.Black
        Me.BtnREVERSE.Location = New System.Drawing.Point(25, 57)
        Me.BtnREVERSE.Name = "BtnREVERSE"
        Me.BtnREVERSE.Size = New System.Drawing.Size(73, 23)
        Me.BtnREVERSE.TabIndex = 5
        Me.BtnREVERSE.Text = "Reverse"
        Me.BtnREVERSE.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(347, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "5x"
        '
        'BtnPLAY
        '
        Me.BtnPLAY.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.BtnPLAY.ForeColor = System.Drawing.Color.Black
        Me.BtnPLAY.Location = New System.Drawing.Point(118, 57)
        Me.BtnPLAY.Name = "BtnPLAY"
        Me.BtnPLAY.Size = New System.Drawing.Size(73, 23)
        Me.BtnPLAY.TabIndex = 3
        Me.BtnPLAY.Text = "Play"
        Me.BtnPLAY.UseVisualStyleBackColor = True
        '
        'SentryModeMarker
        '
        Me.SentryModeMarker.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SentryModeMarker.AutoSize = True
        Me.SentryModeMarker.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SentryModeMarker.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SentryModeMarker.ForeColor = System.Drawing.Color.Red
        Me.SentryModeMarker.Location = New System.Drawing.Point(14, 31)
        Me.SentryModeMarker.Margin = New System.Windows.Forms.Padding(0)
        Me.SentryModeMarker.Name = "SentryModeMarker"
        Me.SentryModeMarker.Size = New System.Drawing.Size(7, 7)
        Me.SentryModeMarker.TabIndex = 24
        Me.SentryModeMarker.Text = "|"
        Me.SentryModeMarker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SentryModeMarker.Visible = False
        '
        'MainPlayerMinTimecode
        '
        Me.MainPlayerMinTimecode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.MainPlayerMinTimecode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MainPlayerMinTimecode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MainPlayerMinTimecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainPlayerMinTimecode.ForeColor = System.Drawing.Color.White
        Me.MainPlayerMinTimecode.Location = New System.Drawing.Point(5, 23)
        Me.MainPlayerMinTimecode.Name = "MainPlayerMinTimecode"
        Me.MainPlayerMinTimecode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MainPlayerMinTimecode.Size = New System.Drawing.Size(53, 15)
        Me.MainPlayerMinTimecode.TabIndex = 23
        Me.MainPlayerMinTimecode.Text = "00:00.00"
        Me.MainPlayerMinTimecode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MainPlayerMinTimecode.Visible = False
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(37, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Nate Mccomb"
        '
        'MainPlayerMaxTimecode
        '
        Me.MainPlayerMaxTimecode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.MainPlayerMaxTimecode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MainPlayerMaxTimecode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MainPlayerMaxTimecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainPlayerMaxTimecode.ForeColor = System.Drawing.Color.White
        Me.MainPlayerMaxTimecode.Location = New System.Drawing.Point(247, 22)
        Me.MainPlayerMaxTimecode.Name = "MainPlayerMaxTimecode"
        Me.MainPlayerMaxTimecode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MainPlayerMaxTimecode.Size = New System.Drawing.Size(53, 15)
        Me.MainPlayerMaxTimecode.TabIndex = 22
        Me.MainPlayerMaxTimecode.Text = "00:00.00"
        Me.MainPlayerMaxTimecode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MainPlayerMaxTimecode.Visible = False
        '
        'MainPlayerTimecode
        '
        Me.MainPlayerTimecode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.MainPlayerTimecode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MainPlayerTimecode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MainPlayerTimecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainPlayerTimecode.ForeColor = System.Drawing.Color.White
        Me.MainPlayerTimecode.Location = New System.Drawing.Point(128, 22)
        Me.MainPlayerTimecode.Name = "MainPlayerTimecode"
        Me.MainPlayerTimecode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MainPlayerTimecode.Size = New System.Drawing.Size(53, 15)
        Me.MainPlayerTimecode.TabIndex = 21
        Me.MainPlayerTimecode.Text = "00:00.00"
        Me.MainPlayerTimecode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MainPlayerTimecode.Visible = False
        '
        'Donation
        '
        Me.Donation.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Donation.BackColor = System.Drawing.Color.DimGray
        Me.Donation.BackgroundImage = Global.TeslaCam_Viewer.My.Resources.Resources.btn_donate_SM
        Me.Donation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Donation.Cursor = System.Windows.Forms.Cursors.Default
        Me.Donation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Donation.ForeColor = System.Drawing.Color.DimGray
        Me.Donation.Location = New System.Drawing.Point(117, 103)
        Me.Donation.Name = "Donation"
        Me.Donation.Size = New System.Drawing.Size(74, 31)
        Me.Donation.TabIndex = 18
        Me.Donation.TabStop = False
        Me.Donation.UseVisualStyleBackColor = False
        '
        'TimeCodeBar
        '
        Me.TimeCodeBar.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TimeCodeBar.BackColor = System.Drawing.Color.DimGray
        Me.TimeCodeBar.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz
        Me.TimeCodeBar.LargeChange = 50
        Me.TimeCodeBar.Location = New System.Drawing.Point(4, 26)
        Me.TimeCodeBar.Name = "TimeCodeBar"
        Me.TimeCodeBar.Size = New System.Drawing.Size(296, 45)
        Me.TimeCodeBar.SmallChange = 10
        Me.TimeCodeBar.TabIndex = 4
        Me.TimeCodeBar.TabStop = False
        Me.TimeCodeBar.TickFrequency = 10
        Me.TimeCodeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(347, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(18, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "3x"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(347, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "1x"
        '
        'GroupBoxNewLayout
        '
        Me.GroupBoxNewLayout.Controls.Add(Me.CloseGroupboxNewLayout)
        Me.GroupBoxNewLayout.Controls.Add(Me.SaveNewLayoutBtn)
        Me.GroupBoxNewLayout.Controls.Add(Me.NewAspectHeight)
        Me.GroupBoxNewLayout.Controls.Add(Me.NewAspectWidth)
        Me.GroupBoxNewLayout.Controls.Add(Me.Label11)
        Me.GroupBoxNewLayout.Controls.Add(Me.Label10)
        Me.GroupBoxNewLayout.Controls.Add(Me.Label9)
        Me.GroupBoxNewLayout.Controls.Add(Me.Label8)
        Me.GroupBoxNewLayout.Controls.Add(Me.NewLayoutName)
        Me.GroupBoxNewLayout.Controls.Add(Me.Label5)
        Me.GroupBoxNewLayout.ForeColor = System.Drawing.Color.White
        Me.GroupBoxNewLayout.Location = New System.Drawing.Point(562, 9)
        Me.GroupBoxNewLayout.Name = "GroupBoxNewLayout"
        Me.GroupBoxNewLayout.Size = New System.Drawing.Size(314, 145)
        Me.GroupBoxNewLayout.TabIndex = 30
        Me.GroupBoxNewLayout.TabStop = False
        Me.GroupBoxNewLayout.Text = "New Layout"
        Me.GroupBoxNewLayout.Visible = False
        '
        'CloseGroupboxNewLayout
        '
        Me.CloseGroupboxNewLayout.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CloseGroupboxNewLayout.AutoSize = True
        Me.CloseGroupboxNewLayout.BackColor = System.Drawing.Color.Red
        Me.CloseGroupboxNewLayout.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseGroupboxNewLayout.Location = New System.Drawing.Point(297, 0)
        Me.CloseGroupboxNewLayout.Name = "CloseGroupboxNewLayout"
        Me.CloseGroupboxNewLayout.Size = New System.Drawing.Size(11, 12)
        Me.CloseGroupboxNewLayout.TabIndex = 28
        Me.CloseGroupboxNewLayout.Text = "X"
        '
        'SaveNewLayoutBtn
        '
        Me.SaveNewLayoutBtn.Enabled = False
        Me.SaveNewLayoutBtn.ForeColor = System.Drawing.Color.Black
        Me.SaveNewLayoutBtn.Location = New System.Drawing.Point(208, 112)
        Me.SaveNewLayoutBtn.Name = "SaveNewLayoutBtn"
        Me.SaveNewLayoutBtn.Size = New System.Drawing.Size(75, 23)
        Me.SaveNewLayoutBtn.TabIndex = 27
        Me.SaveNewLayoutBtn.Text = "Save"
        Me.SaveNewLayoutBtn.UseVisualStyleBackColor = True
        '
        'NewAspectHeight
        '
        Me.NewAspectHeight.Location = New System.Drawing.Point(185, 59)
        Me.NewAspectHeight.Name = "NewAspectHeight"
        Me.NewAspectHeight.Size = New System.Drawing.Size(43, 20)
        Me.NewAspectHeight.TabIndex = 7
        '
        'NewAspectWidth
        '
        Me.NewAspectWidth.Location = New System.Drawing.Point(95, 59)
        Me.NewAspectWidth.Name = "NewAspectWidth"
        Me.NewAspectWidth.Size = New System.Drawing.Size(43, 20)
        Me.NewAspectWidth.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(14, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(267, 27)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "The proportional relationship between the layout width and layout height. 4:3 16:" &
    "9 ..."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(61, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Width"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(149, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Height"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(119, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Aspect Ratio"
        '
        'NewLayoutName
        '
        Me.NewLayoutName.Location = New System.Drawing.Point(114, 17)
        Me.NewLayoutName.Name = "NewLayoutName"
        Me.NewLayoutName.Size = New System.Drawing.Size(137, 20)
        Me.NewLayoutName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(45, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Layout Name"
        '
        'GroupBoxEXPLORER
        '
        Me.GroupBoxEXPLORER.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxEXPLORER.Controls.Add(Me.CustomFolderGroupBox)
        Me.GroupBoxEXPLORER.Controls.Add(Me.FixTeslaCamGroupBox)
        Me.GroupBoxEXPLORER.Controls.Add(Me.Tv_Explorer)
        Me.GroupBoxEXPLORER.ForeColor = System.Drawing.Color.White
        Me.GroupBoxEXPLORER.Location = New System.Drawing.Point(2, 324)
        Me.GroupBoxEXPLORER.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBoxEXPLORER.Name = "GroupBoxEXPLORER"
        Me.GroupBoxEXPLORER.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBoxEXPLORER.Size = New System.Drawing.Size(328, 165)
        Me.GroupBoxEXPLORER.TabIndex = 14
        Me.GroupBoxEXPLORER.TabStop = False
        Me.GroupBoxEXPLORER.Text = "Explorer"
        '
        'CustomFolderGroupBox
        '
        Me.CustomFolderGroupBox.Controls.Add(Me.CustomFolderCancelBTN)
        Me.CustomFolderGroupBox.Controls.Add(Me.CustomFolderSaveBTN)
        Me.CustomFolderGroupBox.Controls.Add(Me.CustomFolderURL)
        Me.CustomFolderGroupBox.Controls.Add(Me.CustomFolderBrowseBTN)
        Me.CustomFolderGroupBox.Location = New System.Drawing.Point(3, 33)
        Me.CustomFolderGroupBox.Name = "CustomFolderGroupBox"
        Me.CustomFolderGroupBox.Size = New System.Drawing.Size(322, 100)
        Me.CustomFolderGroupBox.TabIndex = 32
        Me.CustomFolderGroupBox.TabStop = False
        Me.CustomFolderGroupBox.Text = "Custom Folder"
        Me.CustomFolderGroupBox.Visible = False
        '
        'CustomFolderCancelBTN
        '
        Me.CustomFolderCancelBTN.ForeColor = System.Drawing.Color.Black
        Me.CustomFolderCancelBTN.Location = New System.Drawing.Point(200, 61)
        Me.CustomFolderCancelBTN.Name = "CustomFolderCancelBTN"
        Me.CustomFolderCancelBTN.Size = New System.Drawing.Size(75, 23)
        Me.CustomFolderCancelBTN.TabIndex = 3
        Me.CustomFolderCancelBTN.Text = "Cancel"
        Me.CustomFolderCancelBTN.UseVisualStyleBackColor = True
        '
        'CustomFolderSaveBTN
        '
        Me.CustomFolderSaveBTN.ForeColor = System.Drawing.Color.Black
        Me.CustomFolderSaveBTN.Location = New System.Drawing.Point(41, 60)
        Me.CustomFolderSaveBTN.Name = "CustomFolderSaveBTN"
        Me.CustomFolderSaveBTN.Size = New System.Drawing.Size(75, 23)
        Me.CustomFolderSaveBTN.TabIndex = 2
        Me.CustomFolderSaveBTN.Text = "Save"
        Me.CustomFolderSaveBTN.UseVisualStyleBackColor = True
        '
        'CustomFolderURL
        '
        Me.CustomFolderURL.ForeColor = System.Drawing.Color.Black
        Me.CustomFolderURL.Location = New System.Drawing.Point(8, 22)
        Me.CustomFolderURL.Name = "CustomFolderURL"
        Me.CustomFolderURL.Size = New System.Drawing.Size(227, 20)
        Me.CustomFolderURL.TabIndex = 1
        '
        'CustomFolderBrowseBTN
        '
        Me.CustomFolderBrowseBTN.ForeColor = System.Drawing.Color.Black
        Me.CustomFolderBrowseBTN.Location = New System.Drawing.Point(241, 20)
        Me.CustomFolderBrowseBTN.Name = "CustomFolderBrowseBTN"
        Me.CustomFolderBrowseBTN.Size = New System.Drawing.Size(75, 23)
        Me.CustomFolderBrowseBTN.TabIndex = 0
        Me.CustomFolderBrowseBTN.Text = "Browse"
        Me.CustomFolderBrowseBTN.UseVisualStyleBackColor = True
        '
        'FixTeslaCamGroupBox
        '
        Me.FixTeslaCamGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FixTeslaCamGroupBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.FixTeslaCamGroupBox.Controls.Add(Me.PictureBox1)
        Me.FixTeslaCamGroupBox.Controls.Add(Me.FixingNumFilesLabel)
        Me.FixTeslaCamGroupBox.Controls.Add(Me.FixTeslaCamFFmpegOutput)
        Me.FixTeslaCamGroupBox.Controls.Add(Me.FixTeslaCamBtnDone)
        Me.FixTeslaCamGroupBox.Controls.Add(Me.FixTeslaCamProgressBar)
        Me.FixTeslaCamGroupBox.ForeColor = System.Drawing.Color.White
        Me.FixTeslaCamGroupBox.Location = New System.Drawing.Point(14, 13)
        Me.FixTeslaCamGroupBox.Name = "FixTeslaCamGroupBox"
        Me.FixTeslaCamGroupBox.Size = New System.Drawing.Size(286, 149)
        Me.FixTeslaCamGroupBox.TabIndex = 2
        Me.FixTeslaCamGroupBox.TabStop = False
        Me.FixTeslaCamGroupBox.Text = "Fixing Files"
        Me.FixTeslaCamGroupBox.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PictureBox1.Image = Global.TeslaCam_Viewer.My.Resources.Resources.InstaTwitterPurpleModel3
        Me.PictureBox1.Location = New System.Drawing.Point(49, 65)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(134, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'FixingNumFilesLabel
        '
        Me.FixingNumFilesLabel.AutoSize = True
        Me.FixingNumFilesLabel.Location = New System.Drawing.Point(12, 18)
        Me.FixingNumFilesLabel.Name = "FixingNumFilesLabel"
        Me.FixingNumFilesLabel.Size = New System.Drawing.Size(118, 13)
        Me.FixingNumFilesLabel.TabIndex = 3
        Me.FixingNumFilesLabel.Text = "                                     "
        '
        'FixTeslaCamFFmpegOutput
        '
        Me.FixTeslaCamFFmpegOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FixTeslaCamFFmpegOutput.Location = New System.Drawing.Point(6, 34)
        Me.FixTeslaCamFFmpegOutput.Name = "FixTeslaCamFFmpegOutput"
        Me.FixTeslaCamFFmpegOutput.Size = New System.Drawing.Size(270, 76)
        Me.FixTeslaCamFFmpegOutput.TabIndex = 2
        Me.FixTeslaCamFFmpegOutput.TabStop = False
        Me.FixTeslaCamFFmpegOutput.Text = ""
        '
        'FixTeslaCamBtnDone
        '
        Me.FixTeslaCamBtnDone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FixTeslaCamBtnDone.ForeColor = System.Drawing.Color.Black
        Me.FixTeslaCamBtnDone.Location = New System.Drawing.Point(201, 120)
        Me.FixTeslaCamBtnDone.Name = "FixTeslaCamBtnDone"
        Me.FixTeslaCamBtnDone.Size = New System.Drawing.Size(75, 23)
        Me.FixTeslaCamBtnDone.TabIndex = 1
        Me.FixTeslaCamBtnDone.Text = "Cancel"
        Me.FixTeslaCamBtnDone.UseVisualStyleBackColor = True
        '
        'FixTeslaCamProgressBar
        '
        Me.FixTeslaCamProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FixTeslaCamProgressBar.Location = New System.Drawing.Point(6, 120)
        Me.FixTeslaCamProgressBar.Name = "FixTeslaCamProgressBar"
        Me.FixTeslaCamProgressBar.Size = New System.Drawing.Size(189, 23)
        Me.FixTeslaCamProgressBar.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'PREVIEWBox
        '
        Me.PREVIEWBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PREVIEWBox.Controls.Add(Me.PlayerPreview)
        Me.PREVIEWBox.Location = New System.Drawing.Point(246, 12)
        Me.PREVIEWBox.Name = "PREVIEWBox"
        Me.PREVIEWBox.Size = New System.Drawing.Size(400, 320)
        Me.PREVIEWBox.TabIndex = 18
        Me.PREVIEWBox.TabStop = False
        Me.PREVIEWBox.Text = "Preview Window"
        Me.PREVIEWBox.Visible = False
        '
        'VideoRendering
        '
        Me.VideoRendering.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.VideoRendering.AutoSize = True
        Me.VideoRendering.BackColor = System.Drawing.Color.Red
        Me.VideoRendering.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VideoRendering.Location = New System.Drawing.Point(-3, 134)
        Me.VideoRendering.Name = "VideoRendering"
        Me.VideoRendering.Size = New System.Drawing.Size(924, 24)
        Me.VideoRendering.TabIndex = 3
        Me.VideoRendering.Text = "Your video is being rendered and could take several minutes, Please be patient wh" &
    "ile this process completes."
        Me.VideoRendering.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VideoRendering.Visible = False
        '
        'FFmpegOutput
        '
        Me.FFmpegOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FFmpegOutput.Location = New System.Drawing.Point(729, 3)
        Me.FFmpegOutput.Name = "FFmpegOutput"
        Me.FFmpegOutput.Size = New System.Drawing.Size(183, 88)
        Me.FFmpegOutput.TabIndex = 0
        Me.FFmpegOutput.TabStop = False
        Me.FFmpegOutput.Text = ""
        Me.FFmpegOutput.Visible = False
        '
        'RenderOutTimeGraphic
        '
        Me.RenderOutTimeGraphic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RenderOutTimeGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.RenderOutTimeGraphic.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderOutTimeGraphic.ForeColor = System.Drawing.Color.Red
        Me.RenderOutTimeGraphic.Location = New System.Drawing.Point(893, 297)
        Me.RenderOutTimeGraphic.Name = "RenderOutTimeGraphic"
        Me.RenderOutTimeGraphic.Size = New System.Drawing.Size(15, 10)
        Me.RenderOutTimeGraphic.TabIndex = 13
        Me.RenderOutTimeGraphic.Text = "|"
        '
        'RenderInTimeGraphic
        '
        Me.RenderInTimeGraphic.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RenderInTimeGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.RenderInTimeGraphic.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RenderInTimeGraphic.ForeColor = System.Drawing.Color.Lime
        Me.RenderInTimeGraphic.Location = New System.Drawing.Point(8, 297)
        Me.RenderInTimeGraphic.Name = "RenderInTimeGraphic"
        Me.RenderInTimeGraphic.Size = New System.Drawing.Size(15, 10)
        Me.RenderInTimeGraphic.TabIndex = 12
        Me.RenderInTimeGraphic.Text = "|"
        '
        'OneSec
        '
        Me.OneSec.Interval = 1000
        '
        'Panel
        '
        Me.Panel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel.Location = New System.Drawing.Point(364, 3)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(205, 151)
        Me.Panel.TabIndex = 20
        '
        'FileDurations
        '
        Me.FileDurations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FileDurations.AutoScroll = True
        Me.FileDurations.Location = New System.Drawing.Point(2, 116)
        Me.FileDurations.Name = "FileDurations"
        Me.FileDurations.Size = New System.Drawing.Size(909, 100)
        Me.FileDurations.TabIndex = 22
        '
        'AnalyzingFilesLabel
        '
        Me.AnalyzingFilesLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AnalyzingFilesLabel.BackColor = System.Drawing.Color.Transparent
        Me.AnalyzingFilesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AnalyzingFilesLabel.ForeColor = System.Drawing.Color.White
        Me.AnalyzingFilesLabel.Location = New System.Drawing.Point(3, 93)
        Me.AnalyzingFilesLabel.Name = "AnalyzingFilesLabel"
        Me.AnalyzingFilesLabel.Size = New System.Drawing.Size(912, 100)
        Me.AnalyzingFilesLabel.TabIndex = 23
        Me.AnalyzingFilesLabel.Text = "Analyzing Files"
        Me.AnalyzingFilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AnalyzingFilesLabel.Visible = False
        '
        'EventTimeCodeBar
        '
        Me.EventTimeCodeBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EventTimeCodeBar.BackColor = System.Drawing.Color.DimGray
        Me.EventTimeCodeBar.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz
        Me.EventTimeCodeBar.Enabled = False
        Me.EventTimeCodeBar.LargeChange = 50
        Me.EventTimeCodeBar.Location = New System.Drawing.Point(2, 302)
        Me.EventTimeCodeBar.Name = "EventTimeCodeBar"
        Me.EventTimeCodeBar.Size = New System.Drawing.Size(912, 45)
        Me.EventTimeCodeBar.SmallChange = 10
        Me.EventTimeCodeBar.TabIndex = 24
        Me.EventTimeCodeBar.TabStop = False
        Me.EventTimeCodeBar.TickFrequency = 10
        Me.EventTimeCodeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'EventSentryModeMarker
        '
        Me.EventSentryModeMarker.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.EventSentryModeMarker.AutoSize = True
        Me.EventSentryModeMarker.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EventSentryModeMarker.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EventSentryModeMarker.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.EventSentryModeMarker.Location = New System.Drawing.Point(114, 300)
        Me.EventSentryModeMarker.Margin = New System.Windows.Forms.Padding(0)
        Me.EventSentryModeMarker.Name = "EventSentryModeMarker"
        Me.EventSentryModeMarker.Size = New System.Drawing.Size(7, 7)
        Me.EventSentryModeMarker.TabIndex = 25
        Me.EventSentryModeMarker.Text = "|"
        Me.EventSentryModeMarker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EventSentryModeMarker.Visible = False
        '
        'SavedLayouts
        '
        Me.SavedLayouts.Location = New System.Drawing.Point(2, 3)
        Me.SavedLayouts.Name = "SavedLayouts"
        Me.SavedLayouts.Size = New System.Drawing.Size(218, 96)
        Me.SavedLayouts.TabIndex = 26
        Me.SavedLayouts.Text = ""
        Me.SavedLayouts.Visible = False
        '
        'ImportSettingsFileDialog
        '
        Me.ImportSettingsFileDialog.FileName = "user.config"
        '
        'ExportSettingsFileDialog
        '
        Me.ExportSettingsFileDialog.FileName = "user.config"
        '
        'MaxDurationsList
        '
        Me.MaxDurationsList.FormattingEnabled = True
        Me.MaxDurationsList.Location = New System.Drawing.Point(1, 211)
        Me.MaxDurationsList.Name = "MaxDurationsList"
        Me.MaxDurationsList.Size = New System.Drawing.Size(65, 56)
        Me.MaxDurationsList.TabIndex = 31
        Me.MaxDurationsList.Visible = False
        '
        'YouTubeTutorialToolStripMenuItem
        '
        Me.YouTubeTutorialToolStripMenuItem.Name = "YouTubeTutorialToolStripMenuItem"
        Me.YouTubeTutorialToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.YouTubeTutorialToolStripMenuItem.Text = "YouTube Tutorial"
        '
        'PlayerPreview
        '
        Me.PlayerPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PlayerPreview.Enabled = True
        Me.PlayerPreview.Location = New System.Drawing.Point(3, 16)
        Me.PlayerPreview.Name = "PlayerPreview"
        Me.PlayerPreview.OcxState = CType(resources.GetObject("PlayerPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.PlayerPreview.Size = New System.Drawing.Size(394, 301)
        Me.PlayerPreview.TabIndex = 0
        Me.PlayerPreview.TabStop = False
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(914, 491)
        Me.Controls.Add(Me.MaxDurationsList)
        Me.Controls.Add(Me.VideoRendering)
        Me.Controls.Add(Me.AnalyzingFilesLabel)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.EventSentryModeMarker)
        Me.Controls.Add(Me.FFmpegOutput)
        Me.Controls.Add(Me.RenderOutTimeGraphic)
        Me.Controls.Add(Me.GroupBoxEXPLORER)
        Me.Controls.Add(Me.RenderInTimeGraphic)
        Me.Controls.Add(Me.GroupBoxCONTROLS)
        Me.Controls.Add(Me.EventTimeCodeBar)
        Me.Controls.Add(Me.FileDurations)
        Me.Controls.Add(Me.PREVIEWBox)
        Me.Controls.Add(Me.SavedLayouts)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(930, 530)
        Me.Name = "MainForm"
        Me.Text = "TeslaCam Viewer II "
        Me.ExplorerMenuStrip.ResumeLayout(False)
        Me.GroupBoxCONTROLS.ResumeLayout(False)
        Me.GBsubCONTROLS.ResumeLayout(False)
        Me.GBsubCONTROLS.PerformLayout()
        Me.GroupBoxExportSettings.ResumeLayout(False)
        Me.GroupBoxExportSettings.PerformLayout()
        Me.GroupBoxControlsWindow.ResumeLayout(False)
        Me.SettingsMenuStrip.ResumeLayout(False)
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimeCodeBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxNewLayout.ResumeLayout(False)
        Me.GroupBoxNewLayout.PerformLayout()
        Me.GroupBoxEXPLORER.ResumeLayout(False)
        Me.CustomFolderGroupBox.ResumeLayout(False)
        Me.CustomFolderGroupBox.PerformLayout()
        Me.FixTeslaCamGroupBox.ResumeLayout(False)
        Me.FixTeslaCamGroupBox.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PREVIEWBox.ResumeLayout(False)
        CType(Me.EventTimeCodeBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PlayerPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Tv_Explorer As TreeView
    Friend WithEvents Tv_ImgList As ImageList
    Friend WithEvents TimeCodeBar As TrackBar
    Friend WithEvents GroupBoxCONTROLS As GroupBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TrackBar2 As TrackBar
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ControlsSpeed As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBoxControlsWindow As GroupBox
    Friend WithEvents BtnPAUSE As Button
    Friend WithEvents BtnPLAY As Button
    Friend WithEvents BtnREVERSE As Button
    Friend WithEvents PREVIEWBox As GroupBox
    Friend WithEvents PlayerPreview As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents GroupBoxEXPLORER As GroupBox
    Friend WithEvents CustomFolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents Donation As Button
    Private WithEvents GBsubCONTROLS As GroupBox
    Friend WithEvents ClipSelectDOWN As Button
    Friend WithEvents ClipSelectUP As Button
    Friend WithEvents CurrentTimeList As ListBox
    Friend WithEvents RenderBTN As Button
    Friend WithEvents FFmpegOutput As RichTextBox
    Friend WithEvents VideoRendering As Label
    Friend WithEvents DurationProgressBar As ProgressBar
    Friend WithEvents SettingsBTN As Button
    Friend WithEvents CloseGroupboxSettings As Label
    Friend WithEvents RenderQuality As ComboBox
    Friend WithEvents VideoQualityLabel As Label
    Friend WithEvents RenderFileProgress As Label
    Friend WithEvents ThreadsRunningLabel As Label
    Friend WithEvents MirrorLREnable As CheckBox
    Friend WithEvents FlipLREnable As CheckBox
    Friend WithEvents RenderOutTimeLabel As Label
    Friend WithEvents RenderInTimeLabel As Label
    Friend WithEvents RenderTotalTimeLabel As Label
    Friend WithEvents RenderOutTimeGraphic As Label
    Friend WithEvents RenderInTimeGraphic As Label
    Friend WithEvents RenderPlayerTimecode As Label
    Friend WithEvents MainPlayerTimecode As Label
    Friend WithEvents MainPlayerMaxTimecode As Label
    Friend WithEvents MainPlayerMinTimecode As Label
    Friend WithEvents SentryModeMarker As Label
    Friend WithEvents ExplorerMenuStrip As ContextMenuStrip
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CurrentFPS As Label
    Friend WithEvents AppSettingsButton As Button
    Friend WithEvents SettingsMenuStrip As ContextMenuStrip
    Friend WithEvents LanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LanguageSelection As ToolStripComboBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents DownloadUpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UPDATELabel As Label
    Friend WithEvents ViewStatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents YourIDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents GitHubToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TwitterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InstagramToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ReportABugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FeedBackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DonateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VersionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreatedByNateMccombToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PCIDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PCIDCPUModelMotherboardSNToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomIDToolStripTextBox As ToolStripTextBox
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents OneSec As Timer
    Friend WithEvents DisplaySentryIndicator As CheckBox
    Friend WithEvents ReEncodeFilesMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents FixTeslaCamGroupBox As GroupBox
    Friend WithEvents FixTeslaCamBtnDone As Button
    Friend WithEvents FixTeslaCamProgressBar As ProgressBar
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FixTeslaCamFFmpegOutput As RichTextBox
    Friend WithEvents FixingNumFilesLabel As Label
    Friend WithEvents Disable244BugDetectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel As Panel
    Friend WithEvents AspectRatio As ComboBox
    Friend WithEvents FileDurations As FlowLayoutPanel
    Friend WithEvents AnalyzingFilesLabel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBoxExportSettings As GroupBox
    Friend WithEvents MirrorRearEnable As CheckBox
    Friend WithEvents AspectRatioLabel As Label
    Friend WithEvents ResolutionLabel As Label
    Friend WithEvents PlayersEnabledPanel As FlowLayoutPanel
    Friend WithEvents EventTimeCodeBar As TrackBar
    Friend WithEvents EventSentryModeMarker As Label
    Friend WithEvents SaveLayoutBtn As Button
    Friend WithEvents SavedLayouts As RichTextBox
    Friend WithEvents AspectName As ComboBox
    Friend WithEvents AddLayoutBtn As Button
    Friend WithEvents RemoveLayoutBtn As Button
    Friend WithEvents GroupBoxNewLayout As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents NewLayoutName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents NewAspectHeight As TextBox
    Friend WithEvents NewAspectWidth As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents CloseGroupboxNewLayout As Label
    Friend WithEvents SaveNewLayoutBtn As Button
    Friend WithEvents ImportSettingsFileDialog As OpenFileDialog
    Friend WithEvents ExportSettingsFileDialog As SaveFileDialog
    Friend WithEvents AllFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Last3MinutesPerFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaxDurationsList As ListBox
    Friend WithEvents CustomFolderGroupBox As GroupBox
    Friend WithEvents CustomFolderCancelBTN As Button
    Friend WithEvents CustomFolderSaveBTN As Button
    Friend WithEvents CustomFolderURL As TextBox
    Friend WithEvents CustomFolderBrowseBTN As Button
    Friend WithEvents LayoutsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportLayoutsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportLayoutsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChangeCustomFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FormatDriveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfirmToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewLogFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents YouTubeTutorialToolStripMenuItem As ToolStripMenuItem
End Class
