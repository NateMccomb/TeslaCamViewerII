Imports System.ComponentModel
Imports System.IO
Imports System.Security.AccessControl
Imports AxWMPLib
Imports System.Net
'Imports TeslaCam_Viewer.My.Resources

Public Class MainForm

    Dim Debug_Mode As Boolean = False

    Dim GoogleFormURL As String = "https://docs.google.com/forms/d/e/URL"
    Dim FolderLocation As String
    Dim FolderViewing As Boolean
    Dim FilePlayedOnce As Boolean


    Public CurrentVersion As String = Application.ProductVersion
    Dim FullCenterCameraName = ""

    Public SelectedNumberOfVideos As Integer = 0
    Dim MoveRenderOut As Boolean = False
    Dim MoveRenderIn As Boolean = False
    Dim RenderOutputFile As String
    Public RenderFileCount As Integer = 0
    Public RenderFileCountNotDone As Integer = 0
    Delegate Sub UpdateTextBoxDelg(text As String)
    Public myDelegate As UpdateTextBoxDelg = New UpdateTextBoxDelg(AddressOf UpdateTextBox)
    Delegate Sub FixTeslaCamUpdateTextBoxDelg(text As String)
    Public FixTeslaCammyDelegate As FixTeslaCamUpdateTextBoxDelg = New FixTeslaCamUpdateTextBoxDelg(AddressOf FixTeslaCamUpdateTextBox)
    Dim RenderFirstPlay As Boolean = False
    Dim RenderVideoLastPos As Double = 0
    Dim RenderInTime As Integer = 0
    Dim RenderOutTime As Integer = 0
    Dim FormIsClosing As Boolean = False
    Public RenderEnabled As Boolean = False
    Dim CPUID As String
    Dim OriginalCPUID As String
    Dim StartTime As DateTime
    Public ViewedClips As Integer = 0
    Dim SavedClips As Integer = 0
    Dim LinksSelected As String = ""
    Dim RenderViewsSelected As String = ""
    Dim UpDated As String = ""
    Dim curTimeZone As TimeZone = TimeZone.CurrentTimeZone
    Public MainToolTip As New Windows.Forms.ToolTip
    Dim TodaysDate As String
    Public CurrentDriveList As String = ""

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Me.Text = Me.Text & CurrentVersion
        My.Application.CurrentVersion = CurrentVersion
        StartTime = My.Computer.Clock.LocalTime
        VersionToolStripMenuItem.Text = "Version: " & CurrentVersion
        TodaysDate = StartTime.Year.ToString("0000") & "-" & StartTime.Month.ToString("00") & "-" & StartTime.Day.ToString("00")
        Logging(vbCrLf & " - TeslaCam Viewer Starting" & vbCrLf)

        RenderQuality.SelectedIndex = 0
        If My.Settings.UpgradeRequired = True Then
            My.Settings.Upgrade()
            My.Settings.UpgradeRequired = False
            If My.Settings.LatestVersion = "NewInstall" Then
                UpDated = "New Install - " & CurrentVersion
                Logging("Info - New Install - " & CurrentVersion)
                UserAgreement.Show()
            Else
                UpDated = "Updated to " & CurrentVersion & " From " & My.Settings.LatestVersion
                Logging("Info - Updated to " & CurrentVersion & " From " & My.Settings.LatestVersion)
                UserAgreement.Show()
            End If
            My.Settings.LatestVersion = CurrentVersion
            My.Settings.Save()
        End If
        Dim lastVer = My.Settings.LatestVersion
        PasteToolStripMenuItem.Enabled = False 'paste
        UpdatePCID()
        OriginalCPUID = CPUID
        If My.Computer.Keyboard.ShiftKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = False And My.Computer.Keyboard.AltKeyDown = False Then
            My.Settings.Reset()
            UpDated = UpDated & "-RESET-"
            My.Settings.LatestVersion = CurrentVersion
            My.Settings.UpgradeRequired = False
            My.Settings.Save()
            Logging("Info - Settings RESET")
        ElseIf My.Computer.Keyboard.ShiftKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.AltKeyDown = True Then
            Me.Text = Me.Text & "   ID:" & CPUID
            Debug_Mode = True
        End If

        If Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\VideoLAN\VLC") IsNot Nothing Then
            VideoPlayerType.Items.Add("VLC")
        End If
        If Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Active Setup\Installed Components\{22d6f312-b0f6-11d0-94ab-0080c74c7e95}") IsNot Nothing Then
            VideoPlayerType.Items.Add("Windows Media Player")
        End If
        If VideoPlayerType.Items.Contains(My.Settings.VideoPlayerType) = True Then
            VideoPlayerType.Text = My.Settings.VideoPlayerType
        Else
            If VideoPlayerType.Items.Count > 0 Then
                My.Settings.VideoPlayerType = VideoPlayerType.Items.Item(0)
                VideoPlayerType.Text = VideoPlayerType.Items.Item(0)
            End If
        End If


            If My.Settings.AspectRatioList Is Nothing Then
            Logging("Info - Added Default Layouts")
            My.Settings.AspectRatioList = New Specialized.StringCollection
            My.Settings.AspectNames = New Specialized.StringCollection

            My.Settings.AspectRatioList.Add("16:9")
            My.Settings.AspectNames.Add("All Cameras")
            My.Settings.AspectRatioList.Add("4:3")
            My.Settings.AspectNames.Add("Full Screen")
            My.Settings.AspectRatioList.Add("12:3")
            My.Settings.AspectNames.Add("Front & Left/Right Repeater")
            My.Settings.AspectRatioList.Add("8:3")
            My.Settings.AspectNames.Add("Front & Left Repeater")
            My.Settings.AspectRatioList.Add("8:3")
            My.Settings.AspectNames.Add("Front & Right Repeater")


            My.Settings.AspectRatioList.Add("4:3")
            My.Settings.AspectNames.Add("4:3")
            My.Settings.AspectRatioList.Add("12:3")
            My.Settings.AspectNames.Add("12:3")
            My.Settings.AspectRatioList.Add("6:3")
            My.Settings.AspectNames.Add("6:3")
            My.Settings.Save()
        End If
        'If My.Settings.AspectNames Is Nothing Then
        '    My.Settings.AspectNames = New Specialized.StringCollection
        '    My.Settings.AspectNames.Add("16:9")
        '    My.Settings.AspectNames.Add("4:3")
        '    My.Settings.AspectNames.Add("12:3")
        '    My.Settings.AspectNames.Add("6:3")
        '    My.Settings.Save()
        'End If
        MaxNumberOfThreads.Text = My.Settings.MaxThreads
        For Each RatioName As String In My.Settings.AspectNames
            AspectName.Items.Add(RatioName)
        Next
        For Each Ratios As String In My.Settings.AspectRatioList
            AspectRatio.Items.Add(Ratios)
        Next
        If My.Settings.LastAspectRatio <> "" Then
            If AspectName.Items.IndexOf(My.Settings.LastAspectRatio) <> -1 Then
                AspectName.Text = My.Settings.LastAspectRatio
            Else
                If AspectName.Items.Count > 0 Then
                    AspectName.SelectedIndex = 0
                End If
            End If
        Else
            If AspectName.Items.Count > 0 Then
                AspectName.SelectedIndex = 0
            End If
        End If
        AspectRatio.DropDownStyle = ComboBoxStyle.DropDownList
        AspectName.DropDownStyle = ComboBoxStyle.DropDownList
        If Debug_Mode = True Then
            Me.Text = Me.Text & "                                                ------------     DEBUG MODE     ------------"
            Logging("Info - DEBUG MODE - ENABLED")
            MainPlayerMaxTimecode.Visible = True
            MainPlayerMinTimecode.Visible = True
            MainPlayerTimecode.Visible = True
            SavedLayouts.Visible = True
            AnalyzingFilesLabel.Visible = True
            FFmpegOutput.Visible = True
            AspectRatio.Visible = True
            Panel.BackColor = Color.FromArgb(255, 64, 64, 64)
        Else
            Panel.BackColor = Color.Black
        End If
        Languages.FFmpegExists()




        RenderQuality.SelectedIndex = My.Settings.RenderQuality
        FlipLREnable.Checked = My.Settings.FlipLR
        MirrorLREnable.Checked = My.Settings.MirrorLR
        MirrorRearEnable.Checked = My.Settings.MirorBack

        Languages.NewVersion()
        'RESIZEgb()

        Languages.Set_Main()

        MainToolTip.Active = True

        'Me.Size = New Size(1100, 600)
        'Me.MinimumSize = Me.Size
        Tv_ImgList.ImageSize = New Size(20, 20)

        Tv_Explorer.ImageList = Tv_ImgList
        Tv_Explorer.HideSelection = False
        Try
            System.IO.Directory.Delete(Path.GetTempPath() & "TeslaCamFix\", True)
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        RefreshRootNodes()



        TimeCodeBar.Minimum = 0
        CurrentDriveList = GetDrives()
        OneSec.Enabled = True

        If My.Settings.UserAgreed = False Then
            UserAgreement.Show()
        End If

        Try
            For Each Item As String In My.Settings.UserSavedCameraLayouts
                Logging("Info - Layout List: " & "<string>" & Item.Replace("&", "&amp;") & "</string>")
            Next

        Catch ex As Exception

        End Try
        Try
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://raw.githubusercontent.com/NateMccomb/TeslaCamViewerII/master/Message")
            Dim response As System.Net.HttpWebResponse = request.GetResponse

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream)
            Dim Title As String = sr.ReadLine
            Dim Message As String = sr.ReadToEnd
            If My.Settings.Message <> Message Then
                MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.None)
                My.Settings.Message = Message
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function SystemSerialNumber() As String
        Try
            ' Get the Windows Management Instrumentation object.
            Dim wmi As Object = GetObject("WinMgmts:")

            ' Get the "base boards" (mother boards).
            Dim serial_numbers As String = ""
            Dim mother_boards As Object =
            wmi.InstancesOf("Win32_BaseBoard")
            For Each board As Object In mother_boards
                serial_numbers &= ", " & board.SerialNumber
            Next board
            If serial_numbers.Length > 0 Then serial_numbers =
            serial_numbers.Substring(2)
            Logging("Info - Motherboard SN = " & serial_numbers)
            Return serial_numbers
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            Return ""
        End Try
    End Function
    Private Function GetCpuId() As String
        Try
            Dim computer As String = "."
            Dim wmi As Object = GetObject("winmgmts:" &
            "{impersonationLevel=impersonate}!\\" &
            computer & "\root\cimv2")
            Dim processors As Object = wmi.ExecQuery("Select * from " &
            "Win32_Processor")

            Dim cpu_ids As String = ""
            For Each cpu As Object In processors
                cpu_ids = cpu_ids & ", " & cpu.ProcessorId
            Next cpu
            If cpu_ids.Length > 0 Then cpu_ids =
            cpu_ids.Substring(2)
            Logging("Info - CPU ID = " & cpu_ids)
            Return cpu_ids
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            Return "ERROR"
        End Try
    End Function
    Public Sub RefreshRootNodes()
        Tv_Explorer.Nodes.Clear()
        If My.Settings.CustomDIR <> "" Then
            UpdateCustomFolder(My.Settings.CustomDIR)
        Else
            UpdateCustomFolder("Custom Folder")
        End If

        AddSpecialAndStandardFolderImages()

        AddSpecialFolderRootNode(SpecialNodeFolders.Desktop)
        AddSpecialFolderRootNode(SpecialNodeFolders.MyDocuments)
        AddSpecialFolderRootNode(SpecialNodeFolders.MyPictures)
        AddSpecialFolderRootNode(SpecialNodeFolders.Recent)


        AddDriveRootNodes()

        Try
            Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamFix")
            If dir.Exists = True Then
                Dim SubDir As DirectoryInfo
                For Each SubDir In dir.GetDirectories().OrderByDescending(Function(p) p.Name).ToArray()
                    'Console.WriteLine(SubDir.Name)
                    AddFixedTeslaCamFolderRootNode(SubDir.FullName)
                Next
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Sub
    Private Sub TV_Explorer_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles Tv_Explorer.BeforeExpand
        Try
            Dim DrvIsReady As Boolean = (From d As DriveInfo In DriveInfo.GetDrives Where d.Name = e.Node.ImageKey Select d.IsReady).FirstOrDefault

            If (Not e.Node.ImageKey.Contains(":\")) OrElse DrvIsReady OrElse Directory.Exists(e.Node.ImageKey) Then
                e.Node.Nodes.Clear()
                AddChildNodes(e.Node, e.Node.Tag.ToString)
            Else
                e.Cancel = True
                Languages.DriveEmpty()

            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Sub

    Private Sub TV_Explorer_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles Tv_Explorer.AfterCollapse
        e.Node.Nodes.Clear()
        e.Node.Nodes.Add("Empty")
    End Sub
    Public LoadingFiles As Boolean = False
    Public FileChecked As Boolean = False
    Dim QuickStart As Boolean = False
    Private Sub LoadTimesToCurrentTimeList()
        Try
            QuickStart = False
            For Each control As Control In FileDurations.Controls
                If control.GetType Is GetType(ListBox) Then
                    Dim FileDuration As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                    FileDuration.Items.Clear()
                End If
            Next

            LoadingFiles = True
            Tv_Explorer.Enabled = False
            Dim DIRLocation As String = FullCenterCameraName

            If DIRLocation.Contains(".mp4") Then
                DIRLocation = DIRLocation.Remove(DIRLocation.LastIndexOf("\"))
            End If
            'FileList.Items.Clear()
            Dim DirInfo As New DirectoryInfo(DIRLocation)
            Dim Viewed As Boolean = False
            Dim FilesNotViewable As Boolean = False

            Dim CurrentFileTime As String = ""
            Dim CurrentTimeCount As Integer = 0
            FFmpegOutput.Text = ""
            For Each fi As FileInfo In DirInfo.GetFiles.OrderByDescending(Function(p) p.Name).ToArray()
                If Not (fi.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    If fi.Name.StartsWith("20") = True And fi.Name.Contains("_") = True And fi.Name.Contains("-") = True And fi.Name.ToLower.EndsWith(".mp4") Then
                        If FileSize(fi.FullName) > 1000 Then
                            AnalyzingFilesLabel.BringToFront()
                            AnalyzingFilesLabel.Visible = True
                            AnalyzingFilesLabel.Text = "Analyzing File" & vbCrLf & fi.Name
                            Dim fiTime As String = fi.Name.Remove(fi.Name.LastIndexOf("-"), fi.Name.Length - fi.Name.LastIndexOf("-"))
                            fiTime = fiTime.Remove(0, fiTime.IndexOf("_") + 1)

                            Dim CameraName As String = fi.Name.Remove(0, fi.Name.LastIndexOf("-") + 1).Replace(".mp4", "")

                            PlayerExist(CameraName)

                            If CurrentFileTime <> fiTime Then
                                CurrentTimeCount = CurrentTimeCount + 1
                                CurrentFileTime = fiTime
                            End If
                            For Each control As Control In FileDurations.Controls
                                If control.GetType Is GetType(ListBox) Then
                                    Dim AddFileDuration As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                                    Do Until AddFileDuration.Items.Count = CurrentTimeCount
                                        AddFileDuration.Items.Add("0")
                                    Loop
                                End If
                            Next


                            Dim p As New Process
                            p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
                            p.StartInfo.UseShellExecute = False
                            p.StartInfo.CreateNoWindow = True
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            p.StartInfo.RedirectStandardError = True
                            p.EnableRaisingEvents = True

                            p.StartInfo.RedirectStandardOutput = True



                            Dim Reader As StreamReader
                            Dim FFmpegInfo As String = Nothing

                            p.StartInfo.Arguments = "-i " & Chr(34) & (fi.FullName) & Chr(34) & " -preset veryfast -f null"
                            p.Start()
                            p.WaitForExit()
                            Reader = p.StandardError

                            Dim FileDuration As ListBox = CType(FileDurations.Controls(CameraName), ListBox)
                            Do
                                FFmpegInfo = Reader.ReadLine
                                If Debug_Mode = True Then
                                    FFmpegOutput.Text += FFmpegInfo & vbCrLf
                                    'FFmpegOutput.SelectionStart = FFmpegOutput.Text.Length
                                    'FFmpegOutput.ScrollToCaret()
                                End If
                                If FFmpegInfo.Contains("Duration:") Then
                                    Try

                                        '"  Duration: 00:01:31.20, start: 0.000000, bitrate: 3029 kb/s"
                                        Dim MaxMin As String = FFmpegInfo.Remove(FFmpegInfo.IndexOf(","), FFmpegInfo.Length - FFmpegInfo.IndexOf(",")).Remove(0, FFmpegInfo.IndexOf(":") + 5) '.Remove(3, text.Length - 3) '.Replace(":", "").Replace(".", "")
                                        MaxMin = MaxMin.Remove(MaxMin.IndexOf(":"), MaxMin.Length - MaxMin.IndexOf(":"))
                                        Dim MaxSec As Double = FromNumUS(FFmpegInfo.Remove(FFmpegInfo.IndexOf(","), FFmpegInfo.Length - FFmpegInfo.IndexOf(",")).Remove(0, FFmpegInfo.IndexOf(":") + 8))

                                        Dim MaxDuration As Double = (Int(MaxMin) * 60) + MaxSec '/ playbackSpeed 'text.Remove(text.IndexOf(","), text.Length - text.IndexOf(",")).Remove(0, text.IndexOf(":")).Replace(":", "").Replace(".", "")

                                        FileDuration.Items.Item(CurrentTimeCount - 1) = (MaxDuration)
                                        Exit Do

                                    Catch ex As Exception
                                        FileDuration.Items.Add("0")
                                        If Debug_Mode = True Then
                                            MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                        Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                                    End Try
                                End If
                            Loop While Not Reader.EndOfStream

                            p.Close()

                            Reader.Close()


                            If Not CurrentTimeList.Items.Contains(fiTime.Replace("-", ":")) Then
                                CurrentTimeList.Items.Add(fiTime.Replace("-", ":"))

                                Dim AlreadyViewed As Boolean = SearchEventList(fi.Name.Remove(fi.Name.LastIndexOf("-")))
                                If AlreadyViewed = True Then
                                    Viewed = True
                                End If
                                If CurrentTimeList.Items.Count > 2 And QuickStart = False Then
                                    LoadSentryEvent()
                                    QuickStart = True
                                    Panel.Refresh()

                                End If
                            End If
                        ElseIf FileSize(fi.FullName) = 595 Then
                            FilesNotViewable = True
                        End If
                    End If
                End If
            Next
            If FilesNotViewable = True Then
                MessageBox.Show("These files are not viewable due to your Tesla not properly starting the TeslaCam services after a software update." & vbCrLf & "Try turning off TeslaCam/Sentry mode for a few hours to allow your Tesla to go into Deep Sleep Mode.", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If Viewed = False Then
                ViewedClips += 1
            End If

            For Each control As Control In FileDurations.Controls
                If control.GetType Is GetType(ListBox) Then
                    Dim MissingFiles As Integer = 0
                    Dim FileCount As Integer = 0
                    Dim FileDuration As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                    For Each Item In FileDuration.Items
                        FileCount += 1
                        If Item = "0" Then
                            MissingFiles += 1
                        End If
                    Next
                    Dim PlayerEnabled As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                    If FileCount = MissingFiles Then
                        If FileCount <> 0 Then
                            PlayerEnabled.BackColor = Color.DarkRed
                        Else
                            PlayerEnabled.BackColor = PlayersEnabledPanel.BackColor
                        End If
                        PlayerEnabled.Checked = False
                    Else
                        PlayerEnabled.BackColor = PlayersEnabledPanel.BackColor
                        PlayerEnabled.Checked = True
                    End If
                End If
            Next


            LoadingFiles = False
            Tv_Explorer.Enabled = True
            Tv_Explorer.Focus()
            FilePlayedOnce = False

        Catch ex As Exception
            LoadingFiles = False
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Dim ExplorerRightClick As Boolean = False
    Public Sub UpdatePlayersLayout()
        Try

            Logging("Info - Updating Player Layout")
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Dim PlayerEnabledCheckBox As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim SavedPlayerEnabled As Boolean = False
                        Dim SavedPlayerSize As Double = -1
                        Dim SavedPlayerTop As Double = -1
                        Dim SavedPlayerLeft As Double = -1
                        Dim zIndex As Integer = -1

                        Dim ItemFound As Integer = -1
                        Dim CurrentItem As Integer = 0
                        SavedLayouts.Text = ""
                        For Each Item As String In My.Settings.UserSavedCameraLayouts
                            SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
                        Next

                        For Each Item As String In My.Settings.UserSavedCameraLayouts

                            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                                MyReader.TextFieldType = FileIO.FieldType.Delimited
                                MyReader.SetDelimiters("|")

                                Dim Found As Boolean = False
                                Dim currentRow As String()
                                While Not MyReader.EndOfData
                                    Try
                                        currentRow = MyReader.ReadFields()
                                    Catch ex As Exception
                                    End Try
                                End While
                                If currentRow.Count > 0 Then
                                    '0[CameraName],1[PanelAspectRatioName],2[PlayerLocationLeftPercentage],3[PlayerLocationTopPercentage],4[PlayerSizePercentage],5[Enabled?],6[zIndex]
                                    If currentRow(0) = Player.Name And currentRow(1) = AspectName.Text Then
                                        ItemFound = CurrentItem
                                        SavedPlayerEnabled = currentRow(6)
                                        SavedPlayerSize = FromNumUS(currentRow(5))
                                        SavedPlayerTop = FromNumUS(currentRow(4))
                                        SavedPlayerLeft = FromNumUS(currentRow(3))
                                        zIndex = currentRow(7)
                                        Panel.Controls.SetChildIndex(Status, zIndex - 1)
                                        Panel.Controls.SetChildIndex(Player, zIndex)

                                        Player.Top = (SavedPlayerTop / 100) * Panel.Height '((NewPlayer.Top / Panel.Height) * 100)
                                        Player.Left = (SavedPlayerLeft / 100) * Panel.Width '((NewPlayer.Left / Panel.Width) * 100)
                                        PlayerTop.Text = SavedPlayerTop
                                        PlayerLeft.Text = SavedPlayerLeft
                                        PlayerSize.Text = SavedPlayerSize
                                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))
                                        PlayerEnabledCheckBox.Checked = SavedPlayerEnabled
                                        Player.Visible = SavedPlayerEnabled
                                        Status.Location = Player.Location
                                        Exit For
                                    End If
                                End If

                            End Using
                            CurrentItem += 1
                        Next
                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Dim PlayerEnabledCheckBox As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim SavedPlayerEnabled As Boolean = False
                        Dim SavedPlayerSize As Double = -1
                        Dim SavedPlayerTop As Double = -1
                        Dim SavedPlayerLeft As Double = -1
                        Dim zIndex As Integer = -1

                        Dim ItemFound As Integer = -1
                        Dim CurrentItem As Integer = 0
                        SavedLayouts.Text = ""
                        For Each Item As String In My.Settings.UserSavedCameraLayouts
                            SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
                        Next

                        For Each Item As String In My.Settings.UserSavedCameraLayouts

                            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                                MyReader.TextFieldType = FileIO.FieldType.Delimited
                                MyReader.SetDelimiters("|")

                                Dim Found As Boolean = False
                                Dim currentRow As String()
                                While Not MyReader.EndOfData
                                    Try
                                        currentRow = MyReader.ReadFields()
                                    Catch ex As Exception
                                    End Try
                                End While
                                If currentRow.Count > 0 Then
                                    '0[CameraName],1[PanelAspectRatioName],2[PlayerLocationLeftPercentage],3[PlayerLocationTopPercentage],4[PlayerSizePercentage],5[Enabled?],6[zIndex]
                                    If currentRow(0) = Player.Name And currentRow(1) = AspectName.Text Then
                                        ItemFound = CurrentItem
                                        SavedPlayerEnabled = currentRow(6)
                                        SavedPlayerSize = FromNumUS(currentRow(5))
                                        SavedPlayerTop = FromNumUS(currentRow(4))
                                        SavedPlayerLeft = FromNumUS(currentRow(3))
                                        zIndex = currentRow(7)
                                        Panel.Controls.SetChildIndex(Status, zIndex - 1)
                                        Panel.Controls.SetChildIndex(Player, zIndex)

                                        Player.Top = (SavedPlayerTop / 100) * Panel.Height '((NewPlayer.Top / Panel.Height) * 100)
                                        Player.Left = (SavedPlayerLeft / 100) * Panel.Width '((NewPlayer.Left / Panel.Width) * 100)
                                        PlayerTop.Text = SavedPlayerTop
                                        PlayerLeft.Text = SavedPlayerLeft
                                        PlayerSize.Text = SavedPlayerSize
                                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))
                                        PlayerEnabledCheckBox.Checked = SavedPlayerEnabled
                                        Player.Visible = SavedPlayerEnabled
                                        Status.Location = Player.Location
                                        Exit For
                                    End If
                                End If

                            End Using
                            CurrentItem += 1
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub TV_Explorer_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Tv_Explorer.AfterSelect
        Try
            If ExplorerRightClick = False Then
                PlayersSTOP()
                GroupBoxCONTROLS.Enabled = False
                CurrentTimeList.Items.Clear()
                SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Normal_5050
                FullCenterCameraName = ""
                Logging("Info - Explorer Selection - " & e.Node.Tag.ToString)
                If (e.Node.ImageKey.ToString() = "Folder" And PREVIEWBox.Visible = False) Or e.Node.ImageKey.ToString().ToLower = ".mp4" Then
                    If e.Node.ImageKey.ToString().ToLower = ".mp4" Then
                        FullCenterCameraName = e.Node.Tag.ToString.Remove(e.Node.Tag.ToString.LastIndexOf("\") + 1)
                        Logging("Info - File Selected - " & e.Node.Tag.ToString)
                    Else
                        FullCenterCameraName = e.Node.Tag.ToString & "\"
                        Logging("Info - Folder Selected - " & e.Node.Tag.ToString & "\")
                    End If
                    PlayersSTOP()
                    LoadTimesToCurrentTimeList()

                    LoadSentryEvent()

                End If
                Tv_Explorer.Focus()
            Else
                If e.Node.ImageKey.ToString() = "Folder" Then
                    ReEncodeFilesMenuItem.Visible = True
                Else
                    ReEncodeFilesMenuItem.Visible = False
                End If
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub LoadSentryEvent(Optional ByVal Reload As Boolean = True)
        Try
            'FolderViewing = True



            If CurrentTimeList.Items.Count > 1 Then
                CurrentTimeList.SelectedIndex = 1
            ElseIf CurrentTimeList.Items.Count = 1 Then
                CurrentTimeList.SelectedIndex = 0
            End If
            Dim Duration As Double = 0
            MaxDurationsList.Items.Clear()

            For Each control As Control In FileDurations.Controls
                If control.GetType Is GetType(ListBox) Then
                    Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                    If PlayerTimes.Items.Count > 1 Then
                        If PlayerTimes.Items.Item(0) > Duration Then
                            Duration = PlayerTimes.Items.Item(0)
                        End If
                    End If
                End If
            Next
            Dim TotalMaxDuration As Double = 0
            Dim EventSentryModeOffset As Double = 0
            For i = 0 To CurrentTimeList.Items.Count - 1
                Dim MaxDuration As Double = 0

                For Each control As Control In FileDurations.Controls
                    If control.GetType Is GetType(ListBox) Then
                        Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                        If PlayerTimes.Items.Item(i) > MaxDuration Then
                            MaxDuration = PlayerTimes.Items.Item(i)
                        End If
                    End If
                Next
                If i > 1 Then
                    EventSentryModeOffset += MaxDuration
                End If
                MaxDurationsList.Items.Add(MaxDuration)
                TotalMaxDuration += MaxDuration
            Next
            EventTimeCodeBar.Maximum = TotalMaxDuration * 10
            If Reload = True Then
                RenderOutTime = EventTimeCodeBar.Maximum
                RenderInTime = 0
            End If
            Dim EventOffset As Double = 0
            For i = CurrentTimeList.SelectedIndex + 1 To CurrentTimeList.Items.Count - 1
                Dim MaxDuration As Double = 0
                For Each control As Control In FileDurations.Controls
                    If control.GetType Is GetType(ListBox) Then
                        Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                        If FromNumUS(PlayerTimes.Items.Item(i)) > MaxDuration Then
                            MaxDuration = FromNumUS(PlayerTimes.Items.Item(i))
                        End If
                    End If
                Next
                EventOffset += MaxDuration
            Next
            EventTimeCodeOffset = FromNumUS(EventOffset) * 10
            EventSentryTriggerTime = EventSentryModeOffset
            If QuickStart = False Then
                If My.Settings.VideoPlayerType = "VLC" Then
                    For Each control As Control In Panel.Controls
                        If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                            Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                            Player.input.time = (Duration - 2) * 1000
                        End If
                    Next
                Else
                    For Each control As Control In Panel.Controls
                        If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                            Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                            Player.Ctlcontrols.currentPosition = Duration - 2
                            Player.Visible = True
                        End If
                    Next
                End If
            End If
            SentryTriggerTime = Duration
            GroupBoxCONTROLS.Enabled = True
            UpdatePlayersLayout()
            UpdatePlayBackSpeed()
            EventSentryTriggerTime = EventSentryModeOffset + SentryTriggerTime
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub AddSpecialFolderRootNode(SpecialFolder As SpecialNodeFolders)
        Dim SpecialFolderPath As String = Environment.GetFolderPath(CType(SpecialFolder, Environment.SpecialFolder))
        Dim SpecialFolderName As String = Path.GetFileName(SpecialFolderPath)

        AddImageToImgList(SpecialFolderPath, SpecialFolderName)

        Dim DesktopNode As New TreeNode(SpecialFolderName)
        With DesktopNode
            .Tag = SpecialFolderPath
            .ImageKey = SpecialFolderName
            .SelectedImageKey = SpecialFolderName
            .Nodes.Add("Empty")
        End With

        Tv_Explorer.Nodes.Add(DesktopNode)
    End Sub

    Private Sub AddDriveRootNodes()
        For Each drv As DriveInfo In DriveInfo.GetDrives
            AddImageToImgList(drv.Name)
            Dim DriveNode As New TreeNode(drv.Name)
            Logging("Info - Drive RootNode Added - " & drv.Name)
            With DriveNode
                .Tag = drv.Name
                .ImageKey = drv.Name
                .SelectedImageKey = drv.Name
                .Nodes.Add("Empty")
            End With
            Tv_Explorer.Nodes.Add(DriveNode)
        Next
    End Sub

    Private Sub AddCustomFolderRootNode(folderpath As String)
        If Directory.Exists(folderpath) Then
            Logging("Info - Custom Folder Added - " & folderpath)
            Dim FolderName As String = New DirectoryInfo(folderpath).Name
            AddImageToImgList(folderpath)
            Dim rootNode As New TreeNode(FolderName)
            With rootNode
                .Tag = folderpath
                .ImageKey = "Folder" 'folderpath
                .SelectedImageKey = folderpath
                If Directory.GetDirectories(folderpath).Count > 0 OrElse Directory.GetFiles(folderpath).Count > 0 Then
                    .Nodes.Add("Empty")
                End If
            End With
            Tv_Explorer.Nodes.Add(rootNode) 'add this root node to the treeview
        End If
    End Sub

    Private Sub AddChildNodes(tn As TreeNode, DirPath As String)
        Dim DirInfo As New DirectoryInfo(DirPath)
        Try
            For Each di As DirectoryInfo In DirInfo.GetDirectories.OrderByDescending(Function(p) p.Name).ToArray()
                If Not (di.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    'If di.FullName.ToString.Contains("TeslaCam") Then

                    Dim FolderNode As New TreeNode(di.Name)
                    With FolderNode
                        .Tag = di.FullName
                        Logging("Info - FolderNode Added - " & di.FullName)
                        If Tv_ImgList.Images.Keys.Contains(di.FullName) Then
                            .ImageKey = di.FullName
                            .SelectedImageKey = di.FullName
                        Else
                            .ImageKey = "Folder"
                            .SelectedImageKey = "Folder"
                        End If
                        .Nodes.Add("*Empty*")
                    End With
                    tn.Nodes.Add(FolderNode)

                    'End If
                End If

            Next
            If DirInfo.FullName.Contains("\Microsoft\Windows\Recent") Then
                For Each fi As FileInfo In DirInfo.GetFiles.OrderByDescending(Function(p) p.LastAccessTime).ToArray()
                    If Not (fi.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                        Dim ImgKey As String = AddImageToImgList(fi.FullName)
                        Dim FileNode As New TreeNode(fi.Name)
                        With FileNode
                            .Tag = fi.FullName
                            .ImageKey = ImgKey
                            .SelectedImageKey = ImgKey
                        End With
                        tn.Nodes.Add(FileNode)
                    End If
                Next
            Else
                For Each fi As FileInfo In DirInfo.GetFiles.OrderByDescending(Function(p) p.Name).ToArray()
                    If Not (fi.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden And fi.Name.Contains(".mp4") Then
                        Dim ImgKey As String = AddImageToImgList(fi.FullName)
                        Dim FileNode As New TreeNode(fi.Name)
                        With FileNode
                            .Tag = fi.FullName
                            .ImageKey = ImgKey
                            .SelectedImageKey = ImgKey
                        End With
                        tn.Nodes.Add(FileNode)
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub AddSpecialAndStandardFolderImages()
        AddImageToImgList(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Folder")

        Dim SpecialFolders As New List(Of Environment.SpecialFolder)
        With SpecialFolders
            .Add(Environment.SpecialFolder.Desktop)
            .Add(Environment.SpecialFolder.MyDocuments)
            .Add(Environment.SpecialFolder.Favorites)
            .Add(Environment.SpecialFolder.Recent)
            .Add(Environment.SpecialFolder.MyMusic)
            .Add(Environment.SpecialFolder.History)
            .Add(Environment.SpecialFolder.MyPictures)
            .Add(Environment.SpecialFolder.Personal)
        End With

        For Each sf As Environment.SpecialFolder In SpecialFolders
            AddImageToImgList(Environment.GetFolderPath(sf))
        Next
    End Sub

    Public Function AddImageToImgList(FullFilePath As String, Optional SpecialImageKeyName As String = "") As String
        Dim ImgKey As String = If(SpecialImageKeyName = "", FullFilePath, SpecialImageKeyName)
        Dim LoadFromExt As Boolean = False

        If ImgKey = FullFilePath AndAlso File.Exists(FullFilePath) Then
            Dim ext As String = Path.GetExtension(FullFilePath).ToLower
            If ext <> "" AndAlso ext <> ".exe" AndAlso ext <> ".lnk" AndAlso ext <> ".url" Then
                ImgKey = Path.GetExtension(FullFilePath).ToLower
                LoadFromExt = True
            End If
        End If

        If Not Tv_ImgList.Images.Keys.Contains(ImgKey) Then
            Tv_ImgList.Images.Add(ImgKey, Iconhelper.GetIconImage(If(LoadFromExt, ImgKey, FullFilePath), IconSizes.Large32x32))
        End If

        Return ImgKey
    End Function

    Private Enum SpecialNodeFolders As Integer

        Desktop = Environment.SpecialFolder.Desktop
        Favorites = Environment.SpecialFolder.Favorites
        History = Environment.SpecialFolder.History
        MyDocuments = Environment.SpecialFolder.MyDocuments
        MyMusic = Environment.SpecialFolder.MyMusic
        MyPictures = Environment.SpecialFolder.MyPictures
        Recent = Environment.SpecialFolder.Recent
        UserProfile = Environment.SpecialFolder.Personal
    End Enum
    Private Enum InputState
        IDLE = 0
        OPENING = 1
        BUFFERING = 2
        PLAYING = 3
        PAUSED = 4
        STOPPING = 5
        ENDED = 6
        ERRORSTATE = 7
    End Enum
    Private Sub RefreshPlayers()
        If My.Settings.VideoPlayerType = "VLC" Then
            For Each control As Control In Panel.Controls
                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                    Dim state As InputState = DirectCast(Player.input.state, InputState)

                    If state = InputState.PAUSED Then
                        Player.playlist.play()
                        Player.playlist.pause()
                    End If
                End If
            Next
        Else
            For Each control As Control In Panel.Controls
                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                    If Player.playState = WMPLib.WMPPlayState.wmppsPaused Then
                        Player.Ctlcontrols.play()
                        Player.Ctlcontrols.pause()
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TimeCodeBar.Scroll
        If My.Settings.VideoPlayerType = "VLC" Then
            If Not IsNothing(Panel.Controls(CurrentTimeMaxPlayer)) Then
                Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(CurrentTimeMaxPlayer), AxAXVLC.AxVLCPlugin2)
                Player.input.time = TimeCodeBar.Value * 100
            End If
            For Each control As Control In Panel.Controls
                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                    If Player.Name <> CurrentTimeMaxPlayer Then
                        Player.input.time = TimeCodeBar.Value * 100
                    End If
                End If
            Next
        Else
            If Not IsNothing(Panel.Controls(CurrentTimeMaxPlayer)) Then
                Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(CurrentTimeMaxPlayer), AxWindowsMediaPlayer)
                Player.Ctlcontrols.currentPosition = TimeCodeBar.Value / 10
            End If
            For Each control As Control In Panel.Controls
                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                    If Player.Name <> CurrentTimeMaxPlayer Then
                        Player.Ctlcontrols.currentPosition = TimeCodeBar.Value / 10
                    End If
                End If
            Next
        End If
        RefreshPlayers()

    End Sub
    Dim FPSstopCount As Integer = 0

    Public Function ToNumUS(ByVal Number As String)
        Dim DecimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim GroupSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator

        If DecimalSeparator = "." And GroupSeparator = "," Then
            If Number.Contains(".") Then
                Dim returnVal As Double = Number
                Return returnVal
            Else
                Return Int(Number)
            End If

        Else
            If Number.Contains(DecimalSeparator) And Number.Contains(GroupSeparator) Then
                Number = Number.Replace(DecimalSeparator, ".").Replace(GroupSeparator, "")
            ElseIf Number.Contains(GroupSeparator) Then
                Number = Number.Replace(GroupSeparator, "")
            ElseIf Number.Contains(DecimalSeparator) Then
                Number = Number.Replace(DecimalSeparator, ".")
            End If
            Return Number

        End If
    End Function
    Public Function FromNumUS(ByVal Number As String)
        Dim DecimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim GroupSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator

        If DecimalSeparator = "." And GroupSeparator = "," Then
            Return Number
        Else
            If Number.Contains(".") Then
                Number = Number.Replace(".", DecimalSeparator)
            End If
            If Number.Contains(DecimalSeparator) Then
                Dim returnVal As Double = Number
                Return returnVal
            Else
                Return Int(Number)
            End If
        End If
    End Function
    Dim PlayerToFront As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try

            If LoadingFiles = True Then
                AnalyzingFilesLabel.BringToFront()
                AnalyzingFilesLabel.Visible = True
            Else
                AnalyzingFilesLabel.Visible = False
            End If
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Dim state As InputState = DirectCast(Player.input.state, InputState)
                        Dim Status As Label = CType(Panel.Controls(Player.Name & "-Status"), Label)
                        If Status.Text.Contains(state.ToString) = False Then
                            VLCPlayerChangedState(Player.Name)
                            Player.Refresh()
                        End If
                        If VLCReverse = True And Player.input.time > 500 Then
                            Player.input.time -= 500
                        End If
                    End If
                Next

            End If
            If Debug_Mode = True Then
                If OneSec.Enabled = True Then
                    GroupBoxEXPLORER.BackColor = Color.Lime
                Else
                    GroupBoxEXPLORER.BackColor = Color.Red
                End If
            End If
            'SentryModeMarker.Visible = True
            'SentryTriggerTime = TimeCodeBar.Value / 10
            If CurrentTimeList.SelectedIndex = 1 Then
                SentryModeMarker.Left = (((TimeCodeBar.Width - 30) / TimeCodeBar.Maximum) * ((SentryTriggerTime + 1) * 10)) + 10
                SentryModeMarker.Visible = True
            Else
                SentryModeMarker.Visible = False
            End If
            If CurrentTimeList.Items.Count > 0 Then
                EventSentryModeMarker.Left = (((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum) * ((EventSentryTriggerTime + 2) * 10)) + 10
                EventSentryModeMarker.Visible = True


                RenderInTimeGraphic.Left = (((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum) * RenderInTime) + 10
                RenderOutTimeGraphic.Left = (((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum) * RenderOutTime) + 10

                RenderInTimeGraphic.Visible = True
                RenderOutTimeGraphic.Visible = True

            Else
                EventSentryModeMarker.Visible = False
                RenderInTimeGraphic.Visible = False
                RenderOutTimeGraphic.Visible = False
            End If

            If MovePlayer <> "" Then
                Dim CurrentTime As DateTime = DateTime.Now
                Dim Elapsed_time As TimeSpan = CurrentTime.Subtract(PlayerClickTime)
                Panel.Cursor = Cursors.SizeAll

                If Elapsed_time.TotalMilliseconds > 200 Then
                    SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                    If My.Settings.VideoPlayerType = "VLC" Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(MovePlayer), AxAXVLC.AxVLCPlugin2)
                        If PlayerFullScreen = "" Then
                            Dim Status As Label = CType(Panel.Controls(MovePlayer & "-Status"), Label)
                            Dim Location As Point = Panel.MousePosition - Me.Location - Panel.Location
                            If Location.Y < Panel.Height And Location.Y > 20 And Location.X < Panel.Width - 20 And Location.X > 20 Then
                                Player.Location = Location - MovePlayerMousePos - New Point(14, 34) '31
                                Status.Location = New Point(Player.Left, Player.Top)

                                Dim PlayerSize As Label = CType(Panel.Controls(MovePlayer & "-Size"), Label)
                                PlayerSize.Location = New Point(Player.Left, Player.Top + 45)

                                Dim PlayerTop As Label = CType(Panel.Controls(MovePlayer & "-Top"), Label)
                                PlayerTop.Text = Decimal.Round(((Player.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)
                                PlayerTop.Location = New Point(Player.Left, Player.Top + 15)

                                Dim PlayerLeft As Label = CType(Panel.Controls(MovePlayer & "-Left"), Label)
                                PlayerLeft.Text = Decimal.Round(((Player.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)
                                PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                                If PlayerToFront = False Then
                                    PlayerToFront = True
                                    Player.BringToFront()
                                    Status.BringToFront()

                                    PlayerTop.ForeColor = Color.White
                                    PlayerLeft.ForeColor = Color.White
                                    PlayerSize.ForeColor = Color.White
                                    PlayerTop.Visible = True
                                    PlayerLeft.Visible = True
                                    PlayerSize.Visible = True
                                    PlayerTop.BringToFront()
                                    PlayerLeft.BringToFront()
                                    PlayerSize.BringToFront()
                                End If
                            End If
                        End If
                    Else
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(MovePlayer), AxWindowsMediaPlayer)
                        If PlayerFullScreen = "" Then
                            Dim Status As Label = CType(Panel.Controls(MovePlayer & "-Status"), Label)
                            Dim Location As Point = Panel.MousePosition - Me.Location - Panel.Location
                            If Location.Y < Panel.Height And Location.Y > 20 And Location.X < Panel.Width - 20 And Location.X > 20 Then
                                Player.Location = Location - MovePlayerMousePos - New Point(14, 34) '31
                                Status.Location = New Point(Player.Left, Player.Top)

                                Dim PlayerSize As Label = CType(Panel.Controls(MovePlayer & "-Size"), Label)
                                PlayerSize.Location = New Point(Player.Left, Player.Top + 45)

                                Dim PlayerTop As Label = CType(Panel.Controls(MovePlayer & "-Top"), Label)
                                PlayerTop.Text = Decimal.Round(((Player.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)
                                PlayerTop.Location = New Point(Player.Left, Player.Top + 15)

                                Dim PlayerLeft As Label = CType(Panel.Controls(MovePlayer & "-Left"), Label)
                                PlayerLeft.Text = Decimal.Round(((Player.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)
                                PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                                If PlayerToFront = False Then
                                    PlayerToFront = True
                                    Player.BringToFront()
                                    Status.BringToFront()

                                    PlayerTop.ForeColor = Color.White
                                    PlayerLeft.ForeColor = Color.White
                                    PlayerSize.ForeColor = Color.White
                                    PlayerTop.Visible = True
                                    PlayerLeft.Visible = True
                                    PlayerSize.Visible = True
                                    PlayerTop.BringToFront()
                                    PlayerLeft.BringToFront()
                                    PlayerSize.BringToFront()
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If ResizePlayer <> "" Then
                Dim CurrentTime As DateTime = DateTime.Now
                Dim Elapsed_time As TimeSpan = CurrentTime.Subtract(PlayerClickTime)
                If Elapsed_time.TotalMilliseconds > 200 Then
                    SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                    If My.Settings.VideoPlayerType = "VLC" Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(ResizePlayer), AxAXVLC.AxVLCPlugin2)
                        If PlayerFullScreen = "" Then
                            Dim Status As Label = CType(Panel.Controls(ResizePlayer & "-Status"), Label)
                            Dim MouseLocation As Point = ResizePlayerMousePos - Panel.MousePosition + Panel.Location '- Panel.Location - Me.Location

                            Dim NewSize As Point = ResizePlayerStartSize
                            Dim NewLocation As Point = ResizePlayerStartLocation

                            Select Case ResizePlayerMouseQuarter
                                Case 1
                                    'good
                                    NewSize = ResizePlayerStartSize + New Point(MouseLocation.X, MouseLocation.Y)
                                    NewLocation = New Point(ResizePlayerStartLocation.X - MouseLocation.X, ResizePlayerStartLocation.Y - MouseLocation.Y)
                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    Else
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    NewLocation = New Point(ResizePlayerStartLocation.X - (NewSize.X - ResizePlayerStartSize.X), ResizePlayerStartLocation.Y - (NewSize.Y - ResizePlayerStartSize.Y))
                                    Panel.Cursor = Cursors.PanNW
                                Case 2
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X - MouseLocation.X, ResizePlayerStartSize.Y + MouseLocation.Y)
                                    NewLocation = New Point(ResizePlayerStartLocation.X, ResizePlayerStartLocation.Y - MouseLocation.Y)

                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    If NewSize.X / 4 < NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    Panel.Cursor = Cursors.PanNE
                                Case 3
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X + MouseLocation.X, ResizePlayerStartSize.Y - MouseLocation.Y)
                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    Else
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    NewLocation = New Point(ResizePlayerStartLocation.X - (NewSize.X - ResizePlayerStartSize.X), ResizePlayerStartLocation.Y)
                                    Panel.Cursor = Cursors.PanSW
                                Case 4
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X - MouseLocation.X + Panel.Left, ResizePlayerStartSize.Y - MouseLocation.Y + Panel.Top)
                                    'NewSize = ResizePlayerStartSize - New Point(MouseLocation.X, MouseLocation.Y)

                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    If NewSize.X / 4 < NewSize.Y / 3 Then
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    Panel.Cursor = Cursors.PanSE
                            End Select

                            'NewLocation = New Point(NewLocation.X, NewLocation.Y)


                            NewSize = New Point(NewSize.X, NewSize.Y)
                            Player.Size = NewSize
                            Dim PlayerSize As Label = CType(Panel.Controls(ResizePlayer & "-Size"), Label)
                            PlayerSize.Text = Decimal.Round(((Player.Height / Panel.Height) * 100), 0, MidpointRounding.AwayFromZero)

                            Dim PlayerTop As Label = CType(Panel.Controls(ResizePlayer & "-Top"), Label)
                            PlayerTop.Text = Decimal.Round(((Player.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)

                            Dim PlayerLeft As Label = CType(Panel.Controls(ResizePlayer & "-Left"), Label)
                            PlayerLeft.Text = Decimal.Round(((Player.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)


                            Player.Location = New Point(NewLocation.X, NewLocation.Y)
                            Status.Location = New Point(Player.Left, Player.Top)
                            PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                            PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                            PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                            If PlayerToFront = False Then
                                PlayerToFront = True
                                Player.BringToFront()
                                Status.BringToFront()

                                PlayerTop.ForeColor = Color.White
                                PlayerLeft.ForeColor = Color.White
                                PlayerSize.ForeColor = Color.White
                                PlayerTop.Visible = True
                                PlayerLeft.Visible = True
                                PlayerSize.Visible = True
                                PlayerTop.BringToFront()
                                PlayerLeft.BringToFront()
                                PlayerSize.BringToFront()
                            End If
                        End If
                    Else
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(ResizePlayer), AxWindowsMediaPlayer)
                        If PlayerFullScreen = "" Then
                            Dim Status As Label = CType(Panel.Controls(ResizePlayer & "-Status"), Label)
                            Dim MouseLocation As Point = ResizePlayerMousePos - Panel.MousePosition + Panel.Location '- Panel.Location - Me.Location

                            Dim NewSize As Point = ResizePlayerStartSize
                            Dim NewLocation As Point = ResizePlayerStartLocation

                            Select Case ResizePlayerMouseQuarter
                                Case 1
                                    'good
                                    NewSize = ResizePlayerStartSize + New Point(MouseLocation.X, MouseLocation.Y)
                                    NewLocation = New Point(ResizePlayerStartLocation.X - MouseLocation.X, ResizePlayerStartLocation.Y - MouseLocation.Y)
                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    Else
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    NewLocation = New Point(ResizePlayerStartLocation.X - (NewSize.X - ResizePlayerStartSize.X), ResizePlayerStartLocation.Y - (NewSize.Y - ResizePlayerStartSize.Y))
                                    Panel.Cursor = Cursors.PanNW
                                Case 2
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X - MouseLocation.X, ResizePlayerStartSize.Y + MouseLocation.Y)
                                    NewLocation = New Point(ResizePlayerStartLocation.X, ResizePlayerStartLocation.Y - MouseLocation.Y)

                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    If NewSize.X / 4 < NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    Panel.Cursor = Cursors.PanNE
                                Case 3
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X + MouseLocation.X, ResizePlayerStartSize.Y - MouseLocation.Y)
                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    Else
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    NewLocation = New Point(ResizePlayerStartLocation.X - (NewSize.X - ResizePlayerStartSize.X), ResizePlayerStartLocation.Y)
                                    Panel.Cursor = Cursors.PanSW
                                Case 4
                                    'good
                                    NewSize = New Point(ResizePlayerStartSize.X - MouseLocation.X + Panel.Left, ResizePlayerStartSize.Y - MouseLocation.Y + Panel.Top)
                                    'NewSize = ResizePlayerStartSize - New Point(MouseLocation.X, MouseLocation.Y)

                                    If NewSize.X / 4 > NewSize.Y / 3 Then
                                        NewSize.X = (NewSize.Y / 3) * 4
                                    End If
                                    If NewSize.X / 4 < NewSize.Y / 3 Then
                                        NewSize.Y = (NewSize.X / 4) * 3
                                    End If
                                    Panel.Cursor = Cursors.PanSE
                            End Select

                            'NewLocation = New Point(NewLocation.X, NewLocation.Y)


                            NewSize = New Point(NewSize.X, NewSize.Y)
                            Player.Size = NewSize
                            Dim PlayerSize As Label = CType(Panel.Controls(ResizePlayer & "-Size"), Label)
                            PlayerSize.Text = Decimal.Round(((Player.Height / Panel.Height) * 100), 0, MidpointRounding.AwayFromZero)

                            Dim PlayerTop As Label = CType(Panel.Controls(ResizePlayer & "-Top"), Label)
                            PlayerTop.Text = Decimal.Round(((Player.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)

                            Dim PlayerLeft As Label = CType(Panel.Controls(ResizePlayer & "-Left"), Label)
                            PlayerLeft.Text = Decimal.Round(((Player.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)


                            Player.Location = New Point(NewLocation.X, NewLocation.Y)
                            Status.Location = New Point(Player.Left, Player.Top)
                            PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                            PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                            PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                            If PlayerToFront = False Then
                                PlayerToFront = True
                                Player.BringToFront()
                                Status.BringToFront()

                                PlayerTop.ForeColor = Color.White
                                PlayerLeft.ForeColor = Color.White
                                PlayerSize.ForeColor = Color.White
                                PlayerTop.Visible = True
                                PlayerLeft.Visible = True
                                PlayerSize.Visible = True
                                PlayerTop.BringToFront()
                                PlayerLeft.BringToFront()
                                PlayerSize.BringToFront()
                            End If
                        End If
                    End If
                End If
            End If


            If MovePlayer = "" And ResizePlayer = "" Then
                Panel.Cursor = Cursors.Default
                PlayerToFront = False
            End If

            Try

                Dim PlayerMin As Double = 0
                Dim playerSec As Double = 0
                If Not IsNothing(Panel.Controls(CurrentTimeMaxPlayer)) Then
                    If My.Settings.VideoPlayerType = "VLC" Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(CurrentTimeMaxPlayer), AxAXVLC.AxVLCPlugin2)

                        If Player.input.time / 1000 > 599 Then
                            Dim TempTime As Double = Player.input.time / 1000
                            For i = 1 To TempTime / 60
                                PlayerMin += 1
                                TempTime -= 60
                            Next
                            playerSec = TempTime
                        Else

                            playerSec = Player.input.time / 1000
                        End If
                        MainPlayerTimecode.Text = PlayerMin.ToString("00") & ":" & playerSec.ToString("00.00")
                        TimeCodeBar.Value = Player.input.time / 100
                        If EventTimeMove = False Then
                            EventTimeCodeBar.Value = EventTimeCodeOffset + TimeCodeBar.Value
                            'EventSentryTriggerTime = EventTimeCodeBar.Value / 10 + 2
                        End If
                    Else
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(CurrentTimeMaxPlayer), AxWindowsMediaPlayer)
                        If Player.Ctlcontrols.currentPosition * 10 > 599 Then
                            Dim TempTime As Double = Player.Ctlcontrols.currentPosition
                            For i = 1 To TempTime / 60
                                PlayerMin += 1
                                TempTime -= 60
                            Next
                            playerSec = TempTime
                        Else

                            playerSec = Player.Ctlcontrols.currentPosition
                        End If
                        MainPlayerTimecode.Text = PlayerMin.ToString("00") & ":" & playerSec.ToString("00.00")
                        TimeCodeBar.Value = Player.Ctlcontrols.currentPosition * 10
                        If EventTimeMove = False Then
                            EventTimeCodeBar.Value = EventTimeCodeOffset + TimeCodeBar.Value
                            'EventSentryTriggerTime = EventTimeCodeBar.Value / 10 + 2
                        End If
                    End If
                End If

            Catch ex As Exception
            End Try


            If CurrentTimeList.SelectedIndex > 0 Then
                ClipSelectUP.Enabled = True
            Else
                ClipSelectUP.Enabled = False
            End If
            If CurrentTimeList.SelectedIndex < CurrentTimeList.Items.Count - 1 Then
                ClipSelectDOWN.Enabled = True
            Else
                ClipSelectDOWN.Enabled = False
            End If

            Dim AvgFPS As Double = 0
            Dim PlayerCount As Integer = 0

            For Each control As Control In Panel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        'If Player.Name = CurrentTimeMaxPlayer Then
                        Dim state As InputState = DirectCast(Player.input.state, InputState)
                        If state = InputState.PLAYING Then
                            Try
                                AvgFPS = AvgFPS + Player.input.fps
                                PlayerCount += 1
                            Catch ex As Exception
                                'If Debug_Mode = True Then
                                '    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'End If
                                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                            End Try
                        End If
                        'End If
                    End If
                Else
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        If Player.Name = CurrentTimeMaxPlayer Then
                            If Player.playState = WMPLib.WMPPlayState.wmppsPlaying Or Player.playState = WMPLib.WMPPlayState.wmppsScanForward Then
                                Try
                                    AvgFPS = AvgFPS + Int(Player.network.frameRate) / 100
                                    PlayerCount += 1
                                Catch ex As Exception
                                    'If Debug_Mode = True Then
                                    '    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    'End If
                                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                                End Try
                            End If
                        End If
                    End If
                End If
            Next

            CurrentPlayedFPS = AvgFPS / PlayerCount
            If AvgFPS = 0 And PlayerCount = 0 Then
                CurrentPlayedFPS = 0
            End If
            CurrentFPS.Text = CurrentPlayedFPS.ToString("00.00") & " FPS"
            If CurrentPlayedFPS = LastPlayedFPS And CurrentPlayedFPS <> 0 And VideoPlayerType.Text <> "VLC" Then
                FPSstopCount += 1
                If FPSstopCount = 50 Then
                    CurrentFPS.BackColor = Color.Red
                    If Disable244BugDetectToolStripMenuItem.Checked = False Then
                        MessageBox.Show("These files are show signs of Tesla's 2019.24.4 bug." & vbCrLf & "Right click on the folder and select 'Re-Encode Files'", "Playback Issues ", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
                        Disable244BugDetectToolStripMenuItem.Checked = True
                    End If
                ElseIf FPSstopCount > 50 Then
                    CurrentFPS.BackColor = Color.Red
                    FPSstopCount = 50
                End If
            Else
                CurrentFPS.BackColor = GroupBoxControlsWindow.BackColor
                FPSstopCount = 0
            End If
            LastPlayedFPS = CurrentPlayedFPS


            Dim playbackSpeed As Double = 0.1 * TrackBar2.Value

            Dim StartMin As Integer = 0
            Dim StartSec As Double = 0
            Dim TotalTimeMin As Double = 0
            Dim TotalTimeSec As Double = 0
            Dim EndMin As Integer = 0
            Dim EndSec As Double = 0
            Dim TotalPlayerMin As Double = 0
            Dim TotalplayerSec As Double = 0
            Dim TempTotalTime As Double = (RenderOutTime - RenderInTime) / playbackSpeed

            If RenderInTime > 599 Then
                Dim TempTime As Double = RenderInTime
                For i = 1 To RenderInTime / 600
                    StartMin += 1
                    TempTime -= 600
                Next
                StartSec = TempTime / 10
            Else
                'StartMin = 0
                StartSec = RenderInTime / 10
            End If
            If RenderOutTime > 599 Then
                Dim TempTime As Double = RenderOutTime
                For i = 1 To RenderOutTime / 600
                    EndMin += 1
                    TempTime -= 600
                Next
                EndSec = TempTime / 10
            Else
                'EndMin = 0
                EndSec = RenderOutTime / 10
            End If
            If TempTotalTime > 599 Then
                Dim TempTime As Double = TempTotalTime
                For i = 1 To TempTotalTime / 600
                    TotalTimeMin += 1
                    TempTime -= 600
                Next
                TotalTimeSec = (TempTime / 10)
            Else
                'EndMin = 0
                TotalTimeSec = (TempTotalTime / 10)
            End If


            RenderInTimeLabel.Text = StartMin.ToString("00") & ":" & StartSec.ToString("00.00")
            RenderOutTimeLabel.Text = EndMin.ToString("00") & ":" & EndSec.ToString("00.00")
            RenderTotalTimeLabel.Text = TotalTimeMin.ToString("00") & ":" & TotalTimeSec.ToString("00.00")

            If Not IsNothing(Panel.Controls(CurrentTimeMaxPlayer)) Then
                If My.Settings.VideoPlayerType = "VLC" Then

                    Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(CurrentTimeMaxPlayer), AxAXVLC.AxVLCPlugin2)

                    If (EventTimeCodeOffset + (VLCPlayer.input.time / 100)) > 599 Then
                        Dim TempTime As Double = (EventTimeCodeOffset / 10) + (VLCPlayer.input.time / 1000)
                        For i = 1 To TempTime / 60
                            TotalPlayerMin += 1
                            TempTime -= 60
                        Next
                        TotalplayerSec = TempTime
                    Else
                        'StartMin = 0
                        TotalplayerSec = (EventTimeCodeOffset / 10) + (VLCPlayer.input.time / 1000)
                    End If
                Else
                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(CurrentTimeMaxPlayer), AxWindowsMediaPlayer)

                    If (EventTimeCodeOffset + (Player.Ctlcontrols.currentPosition * 10)) > 599 Then
                        Dim TempTime As Double = (EventTimeCodeOffset / 10) + Player.Ctlcontrols.currentPosition
                        For i = 1 To TempTime / 60
                            TotalPlayerMin += 1
                            TempTime -= 60
                        Next
                        TotalplayerSec = TempTime
                    Else
                        'StartMin = 0
                        TotalplayerSec = (EventTimeCodeOffset / 10) + Player.Ctlcontrols.currentPosition
                    End If
                End If

            End If
            RenderPlayerTimecode.Text = TotalPlayerMin.ToString("00") & ":" & TotalplayerSec.ToString("00.00")


            Try
                If FixTeslaCamGroupBox.Visible = True Then
                    Dim CurrentProcessCount As Integer = 0
                    For Each PRunning As Process In System.Diagnostics.Process.GetProcessesByName("ffmpeg")
                        'PRunning.Kill()
                        CurrentProcessCount += 1
                    Next
                    If FixTeslaCamQueue.Count <> 0 And CurrentProcessCount < My.Settings.MaxThreads Then
                        StartFixTeslaCamFiles()
                    End If

                    If FixedTeslaCamFileCount <> 0 Or CurrentProcessCount > 0 Then
                        FixTeslaCamProgressBar.Maximum = FixedTeslaCamFileCount
                        FixTeslaCamProgressBar.Value = FixedTeslaCamFileCount - FixedTeslaCamFileNotDone


                        FixingNumFilesLabel.Text = FixedTeslaCamFileCount & " Files Selected, " & CurrentProcessCount & " Being Converted, " & FixedTeslaCamFileCount - FixedTeslaCamFileNotDone & " Complete"

                        If FixedTeslaCamFileNotDone = 0 Then
                            FixedTeslaCamFileCount = 0
                        End If
                        FixTeslaCamTimePosted = False
                    Else

                        If FixTeslaCamTimePosted = False Then
                            FixTeslaCamBtnDone.Text = "Done"
                            FixTeslaCamBtnDone.Enabled = True
                            FixingNumFilesLabel.Text = "Done"
                            FixTeslaCamEnd = DateTime.Now
                            Dim Elapsed_time As TimeSpan = FixTeslaCamEnd.Subtract(FixTeslaCamStart)
                            FixTeslaCamTimePosted = True
                            Dim TotalMin As Integer = Elapsed_time.Minutes
                            If Elapsed_time.Hours > 0 Then
                                TotalMin += Elapsed_time.Hours * 60
                            End If

                            FixTeslaCamUpdateTextBox("Took " & TotalMin.ToString("00") & ":" & Elapsed_time.Seconds.ToString("00") & "." & Elapsed_time.Milliseconds.ToString("000"))
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try







        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Dim FixTeslaCamTimePosted As Boolean = True
    Dim FixTeslaCamEnd As DateTime
    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        UpdatePlayBackSpeed()

    End Sub
    Private Sub UpdatePlayBackSpeed()
        Try
            For Each control As Control In Panel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        If Player.Visible = True Then
                            Player.input.rate = 0.1 * TrackBar2.Value
                        End If
                    End If
                Else
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        If Player.Visible = True Then
                            Player.settings.rate = 0.1 * TrackBar2.Value
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try


    End Sub

    Dim SentryTriggerTime As Double = 0
    Dim EventSentryTriggerTime As Double = 0
    Public CameraCount As Integer = 0


    Public Sub PlayerExist(PlayerName As String)
        Try
            If IsNothing(Panel.Controls(PlayerName)) Then
                Logging("Info - Creating New Player: " & PlayerName)
                CameraCount += 1


                Dim SavedPlayerEnabled As Boolean = False
                Dim SavedPlayerSize As Double = -1
                Dim SavedPlayerTop As Double = -1
                Dim SavedPlayerLeft As Double = -1
                Dim zIndex As Integer = -1

                Dim ItemFound As Integer = -1
                Dim CurrentItem As Integer = 0
                For Each Item As String In My.Settings.UserSavedCameraLayouts
                    Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                        MyReader.TextFieldType = FileIO.FieldType.Delimited
                        MyReader.SetDelimiters("|")

                        Dim Found As Boolean = False
                        Dim currentRow As String()
                        While Not MyReader.EndOfData
                            Try
                                currentRow = MyReader.ReadFields()
                            Catch ex As Exception
                            End Try
                        End While
                        If currentRow.Count > 0 Then
                            '0[CameraName],1[PanelAspectRatioName],2[PlayerLocationLeftPercentage],3[PlayerLocationTopPercentage],4[PlayerSizePercentage],5[Enabled?],6[zIndex]
                            If currentRow(0) = PlayerName And currentRow(1) = AspectName.Text Then
                                ItemFound = CurrentItem
                                SavedPlayerEnabled = currentRow(6)
                                SavedPlayerSize = FromNumUS(currentRow(5))
                                SavedPlayerTop = FromNumUS(currentRow(4))
                                SavedPlayerLeft = FromNumUS(currentRow(3))
                                zIndex = currentRow(7)

                            End If
                        End If

                    End Using
                    CurrentItem += 1
                Next
                If My.Settings.VideoPlayerType = "VLC" Then
                    Dim VLCPlayer As New AxAXVLC.AxVLCPlugin2
                    VLCPlayer.Name = PlayerName
                    VLCPlayer.Location = New Point(Int(((Panel.Width - 200) * Rnd()) + 30), Int(((Panel.Height - 300) * Rnd()) + 30))


                    'NewPlayer.Left = Int(((Panel.Width) * Rnd()) + 3)

                    VLCPlayer.Size = New Size(200, 200)

                    If ItemFound <> -1 Then
                        VLCPlayer.Top = (SavedPlayerTop / 100) * Panel.Height '((NewPlayer.Top / Panel.Height) * 100)
                        VLCPlayer.Left = (SavedPlayerLeft / 100) * Panel.Width '((NewPlayer.Left / Panel.Width) * 100)
                    End If

                    Dim PlayerTop As New Label
                    PlayerTop.Name = PlayerName & "-Top"
                    PlayerTop.Text = Decimal.Round(((VLCPlayer.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)
                    PlayerTop.Location = New Point(VLCPlayer.Left, VLCPlayer.Top + 15)
                    PlayerTop.Size = New Size(40, 14)
                    PlayerTop.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerTop)

                    Dim PlayerLeft As New Label
                    PlayerLeft.Name = PlayerName & "-Left"
                    PlayerLeft.Text = Decimal.Round(((VLCPlayer.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)
                    PlayerLeft.Location = New Point(VLCPlayer.Left, VLCPlayer.Top + 30)
                    PlayerLeft.Size = New Size(40, 14)
                    PlayerLeft.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerLeft)

                    Dim PlayerSize As New Label
                    PlayerSize.Name = PlayerName & "-Size"
                    If ItemFound <> -1 Then
                        PlayerSize.Text = SavedPlayerSize
                    Else
                        PlayerSize.Text = "50"
                    End If
                    PlayerSize.Location = New Point(VLCPlayer.Left, VLCPlayer.Top + 45)
                    PlayerSize.Size = New Size(40, 14)
                    PlayerSize.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerSize)

                    AddHandler PlayerTop.MouseUp, AddressOf PlayerStatusMouseUp
                    AddHandler PlayerLeft.MouseUp, AddressOf PlayerStatusMouseUp
                    AddHandler PlayerSize.MouseUp, AddressOf PlayerStatusMouseUp

                    Panel.Controls.Add(VLCPlayer)
                    MainToolTip.SetToolTip(VLCPlayer, "Left Click-Hold To Move Camera" & vbCrLf &
                                                      "Right Click-Hold To Resize Corner Of Camera") ' & vbCrLf &
                    '"Double Right-Click To Full Screen Camera In Layout")

                    VLCPlayer.FullscreenEnabled = False
                    VLCPlayer.CtlVisible = False
                    VLCPlayer.AutoLoop = False
                    VLCPlayer.AutoPlay = False
                    VLCPlayer.Toolbar = False


                    Dim PlayerDirInfo As New ListBox
                    PlayerDirInfo.Size = New Size(50, 50)
                    PlayerDirInfo.Name = PlayerName
                    FileDurations.Controls.Add(PlayerDirInfo)
                    If Debug_Mode = False Then
                        FileDurations.Visible = False
                    Else
                        FileDurations.Visible = True
                    End If

                    VLCPlayer.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                    'AddHandler VLCPlayer.MediaPlayerEndReached, AddressOf VLCPlayerChangedState
                    'AddHandler VLCPlayer.MediaPlayerPlaying, AddressOf VLCPlayerChangedState
                    AddHandler VLCPlayer.MouseDownEvent, AddressOf VLCPlayerMouseDown
                    AddHandler VLCPlayer.MouseUpEvent, AddressOf VLCPlayerMouseUP
                    'AddHandler VLCPlayer.DblClick, AddressOf VLCPlayerDoubleClick

                    Dim StatusLabel As New Label
                    StatusLabel.Name = VLCPlayer.Name & "-Status"
                    StatusLabel.Location = New Point(VLCPlayer.Left, VLCPlayer.Top)
                    StatusLabel.Size = New Point(20, 12)
                    Panel.Controls.Add(StatusLabel)
                    StatusLabel.Text = " "
                    StatusLabel.AutoSize = True
                    StatusLabel.BackColor = Color.Black
                    StatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
                    'StatusLabel.Opacity = 0

                    AddHandler StatusLabel.MouseDown, AddressOf PlayerStatusMouseDown
                    AddHandler StatusLabel.MouseUp, AddressOf PlayerStatusMouseUp

                    Dim PlayerEnabledCheckBox As New CheckBox
                    PlayerEnabledCheckBox.Name = VLCPlayer.Name
                    PlayerEnabledCheckBox.Text = VLCPlayer.Name.Chars(0).ToString.ToUpper & VLCPlayer.Name.Replace("_", " ").Remove(0, 1)
                    PlayerEnabledCheckBox.AutoSize = False
                    PlayerEnabledCheckBox.Size = New Size(95, 16)
                    If ItemFound <> -1 Then
                        PlayerEnabledCheckBox.Checked = SavedPlayerEnabled
                        VLCPlayer.Visible = SavedPlayerEnabled
                    Else
                        PlayerEnabledCheckBox.Checked = True
                        VLCPlayer.Visible = True
                    End If

                    'PlayerEnabledCheckBox.TextAlign = ContentAlignment.TopCenter
                    PlayerEnabledCheckBox.Font = PlayersEnabledPanel.Font
                    PlayersEnabledPanel.AutoScroll = False
                    PlayersEnabledPanel.HorizontalScroll.Visible = False
                    PlayersEnabledPanel.Controls.Add(PlayerEnabledCheckBox)

                    MainToolTip.SetToolTip(PlayerEnabledCheckBox, "Enable/Disable camera in layout")

                    If ItemFound <> -1 Then
                        Panel.Controls.SetChildIndex(VLCPlayer, zIndex)
                    End If
                Else
                    Dim NewPlayer As New AxWMPLib.AxWindowsMediaPlayer
                    NewPlayer.Name = PlayerName
                    NewPlayer.Location = New Point(Int(((Panel.Width - 200) * Rnd()) + 30), Int(((Panel.Height - 300) * Rnd()) + 30))
                    'NewPlayer.Left = Int(((Panel.Width) * Rnd()) + 3)

                    NewPlayer.Size = New Size(200, 200)
                    If ItemFound <> -1 Then
                        NewPlayer.Top = (SavedPlayerTop / 100) * Panel.Height '((NewPlayer.Top / Panel.Height) * 100)
                        NewPlayer.Left = (SavedPlayerLeft / 100) * Panel.Width '((NewPlayer.Left / Panel.Width) * 100)
                    End If

                    Dim PlayerTop As New Label
                    PlayerTop.Name = PlayerName & "-Top"
                    PlayerTop.Text = Decimal.Round(((NewPlayer.Top / Panel.Height) * 100), 1, MidpointRounding.AwayFromZero)
                    PlayerTop.Location = New Point(NewPlayer.Left, NewPlayer.Top + 15)
                    PlayerTop.Size = New Size(40, 14)
                    PlayerTop.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerTop)

                    Dim PlayerLeft As New Label
                    PlayerLeft.Name = PlayerName & "-Left"
                    PlayerLeft.Text = Decimal.Round(((NewPlayer.Left / Panel.Width) * 100), 1, MidpointRounding.AwayFromZero)
                    PlayerLeft.Location = New Point(NewPlayer.Left, NewPlayer.Top + 30)
                    PlayerLeft.Size = New Size(40, 14)
                    PlayerLeft.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerLeft)

                    Dim PlayerSize As New Label
                    PlayerSize.Name = PlayerName & "-Size"
                    If ItemFound <> -1 Then
                        PlayerSize.Text = SavedPlayerSize
                    Else
                        PlayerSize.Text = "50"
                    End If
                    PlayerSize.Location = New Point(NewPlayer.Left, NewPlayer.Top + 45)
                    PlayerSize.Size = New Size(40, 14)
                    PlayerSize.Visible = Debug_Mode
                    Panel.Controls.Add(PlayerSize)

                    AddHandler PlayerTop.MouseUp, AddressOf PlayerStatusMouseUp
                    AddHandler PlayerLeft.MouseUp, AddressOf PlayerStatusMouseUp
                    AddHandler PlayerSize.MouseUp, AddressOf PlayerStatusMouseUp

                    Panel.Controls.Add(NewPlayer)
                    MainToolTip.SetToolTip(NewPlayer, "Left Click-Hold To Move Camera" & vbCrLf &
                                                      "Right Click-Hold To Resize Corner Of Camera" & vbCrLf &
                                                      "Double Right-Click To Full Screen Camera In Layout")
                    NewPlayer.uiMode = "none"
                    NewPlayer.Ctlenabled = False
                    NewPlayer.enableContextMenu = False

                    Dim PlayerDirInfo As New ListBox
                    PlayerDirInfo.Size = New Size(50, 50)
                    PlayerDirInfo.Name = PlayerName
                    FileDurations.Controls.Add(PlayerDirInfo)
                    If Debug_Mode = False Then
                        FileDurations.Visible = False
                    Else
                        FileDurations.Visible = True
                    End If

                    NewPlayer.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                    AddHandler NewPlayer.PlayStateChange, AddressOf PlayerChangedState
                    AddHandler NewPlayer.MouseDownEvent, AddressOf PlayerMouseDown
                    AddHandler NewPlayer.MouseUpEvent, AddressOf PlayerMouseUP
                    AddHandler NewPlayer.DoubleClickEvent, AddressOf PlayerDoubleClick

                    Dim StatusLabel As New Label
                    StatusLabel.Name = NewPlayer.Name & "-Status"
                    StatusLabel.Location = New Point(NewPlayer.Left, NewPlayer.Top)
                    StatusLabel.Size = New Point(20, 12)
                    Panel.Controls.Add(StatusLabel)
                    StatusLabel.Text = " "
                    StatusLabel.AutoSize = True
                    StatusLabel.BackColor = Color.Black
                    StatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
                    'StatusLabel.Opacity = 0

                    AddHandler StatusLabel.MouseDown, AddressOf PlayerStatusMouseDown
                    AddHandler StatusLabel.MouseUp, AddressOf PlayerStatusMouseUp

                    Dim PlayerEnabledCheckBox As New CheckBox
                    PlayerEnabledCheckBox.Name = NewPlayer.Name
                    PlayerEnabledCheckBox.Text = NewPlayer.Name.Chars(0).ToString.ToUpper & NewPlayer.Name.Replace("_", " ").Remove(0, 1)
                    PlayerEnabledCheckBox.AutoSize = False
                    PlayerEnabledCheckBox.Size = New Size(95, 16)
                    If ItemFound <> -1 Then
                        PlayerEnabledCheckBox.Checked = SavedPlayerEnabled
                        NewPlayer.Visible = SavedPlayerEnabled
                    Else
                        PlayerEnabledCheckBox.Checked = True
                        NewPlayer.Visible = True
                    End If

                    'PlayerEnabledCheckBox.TextAlign = ContentAlignment.TopCenter
                    PlayerEnabledCheckBox.Font = PlayersEnabledPanel.Font
                    PlayersEnabledPanel.AutoScroll = False
                    PlayersEnabledPanel.HorizontalScroll.Visible = False
                    PlayersEnabledPanel.Controls.Add(PlayerEnabledCheckBox)

                    MainToolTip.SetToolTip(PlayerEnabledCheckBox, "Enable/Disable camera in layout")

                    If ItemFound <> -1 Then
                        Panel.Controls.SetChildIndex(NewPlayer, zIndex)
                    End If

                End If





            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Dim MovePlayer As String = ""
    Dim MovePlayerMousePos As Point

    Dim ResizePlayer As String = ""
    Dim ResizePlayerMousePos As Point
    Dim ResizePlayerStartSize As Point
    Dim ResizePlayerStartLocation As Point
    Dim ResizePlayerMouseQuarter As Integer
    Dim PlayerFullScreen As String = ""

    Dim PlayerClickTime As DateTime
    Public Sub PlayerMouseDown(sender As Object, e As _WMPOCXEvents_MouseDownEvent)

        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(sender.Name), AxWindowsMediaPlayer)
        If Player.fullScreen = False Then
            Dim PlayerXY As Point = Panel.MousePosition - Player.Location - Me.Location - Panel.Location - New Point(14, 34)

            If e.nButton = 2 Then ' right
                'Resize
                MovePlayer = ""
                PlayerClickTime = DateTime.Now
                ResizePlayer = Player.Name
                ResizePlayerMousePos = Panel.MousePosition
                ResizePlayerStartSize = Player.Size
                ResizePlayerStartLocation = Player.Location

                Logging("Info - Resizing Player: " & Player.Name)

                If PlayerXY.Y < Player.Height / 2 Then
                    If PlayerXY.X < Player.Width / 2 Then
                        ResizePlayerMouseQuarter = 1
                    Else
                        ResizePlayerMouseQuarter = 2
                    End If
                Else
                    If PlayerXY.X < Player.Width / 2 Then
                        ResizePlayerMouseQuarter = 3
                    Else
                        ResizePlayerMouseQuarter = 4
                    End If
                End If
            ElseIf e.nButton = 1 Then ' left
                Logging("Info - Moving Player: " & Player.Name)
                ResizePlayer = ""
                PlayerClickTime = DateTime.Now
                MovePlayer = Player.Name
                MovePlayerMousePos = PlayerXY
            End If
        End If
    End Sub
    Public Sub VLCPlayerMouseDown(sender As Object, e As AxAXVLC.DVLCEvents_MouseDownEvent)

        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(sender.Name), AxAXVLC.AxVLCPlugin2)
        If Player.video.fullscreen = False Then
            Dim PlayerXY As Point = Panel.MousePosition - Player.Location - Me.Location - Panel.Location - New Point(14, 34)

            If e.button = 2 Then ' right
                'Resize
                MovePlayer = ""
                PlayerClickTime = DateTime.Now
                ResizePlayer = Player.Name
                ResizePlayerMousePos = Panel.MousePosition
                ResizePlayerStartSize = Player.Size
                ResizePlayerStartLocation = Player.Location

                Logging("Info - Resizing Player: " & Player.Name)

                If PlayerXY.Y < Player.Height / 2 Then
                    If PlayerXY.X < Player.Width / 2 Then
                        ResizePlayerMouseQuarter = 1
                    Else
                        ResizePlayerMouseQuarter = 2
                    End If
                Else
                    If PlayerXY.X < Player.Width / 2 Then
                        ResizePlayerMouseQuarter = 3
                    Else
                        ResizePlayerMouseQuarter = 4
                    End If
                End If
            ElseIf e.button = 1 Then ' left
                Logging("Info - Moving Player: " & Player.Name)
                ResizePlayer = ""
                PlayerClickTime = DateTime.Now
                MovePlayer = Player.Name
                MovePlayerMousePos = PlayerXY
            End If
        End If
    End Sub
    Public Sub PlayerDoubleClick(sender As Object, e As _WMPOCXEvents_DoubleClickEvent)
        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(sender.Name), AxWindowsMediaPlayer)
        Player.BringToFront()
        PlayerClickTime = DateTime.Now
        If Player.Size = Panel.Size Then
            Logging("Info - Returning Player from Full Screen: " & Player.Name)
            Dim PlayerSize As Label = CType(Panel.Controls(Player.Name & "-Size"), Label)
            Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

            Dim PlayerTop As Label = CType(Panel.Controls(Player.Name & "-Top"), Label)
            Dim PlayerLeft As Label = CType(Panel.Controls(Player.Name & "-Left"), Label)
            Player.Left = Panel.Width / (100 / PlayerLeft.Text)
            Player.Top = Panel.Height / (100 / PlayerTop.Text)
            Dim Status As Label = CType(Panel.Controls(Player.Name & "-Status"), Label)
            Status.Location = New Point(Player.Left, Player.Top)
            Status.BringToFront()
            PlayerFullScreen = ""

        Else
            Logging("Info - Full Screen Player: " & Player.Name)

            Player.Size = Panel.Size
            Player.Location = New Point(0, 0)
            Dim Status As Label = CType(Panel.Controls(Player.Name & "-Status"), Label)
            Status.Location = New Point(Player.Left, Player.Top)
            Status.BringToFront()
            PlayerFullScreen = Player.Name
        End If
    End Sub
    Public Sub VLCPlayerDoubleClick(sender As Object, e As AxAXVLC.DVLCEvents_MouseUpEvent)
        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(sender.Name), AxAXVLC.AxVLCPlugin2)
        Player.BringToFront()
        PlayerClickTime = DateTime.Now
        If Player.Size = Panel.Size Then
            Logging("Info - Returning Player from Full Screen: " & Player.Name)
            Dim PlayerSize As Label = CType(Panel.Controls(Player.Name & "-Size"), Label)
            Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

            Dim PlayerTop As Label = CType(Panel.Controls(Player.Name & "-Top"), Label)
            Dim PlayerLeft As Label = CType(Panel.Controls(Player.Name & "-Left"), Label)
            Player.Left = Panel.Width / (100 / PlayerLeft.Text)
            Player.Top = Panel.Height / (100 / PlayerTop.Text)
            Dim Status As Label = CType(Panel.Controls(Player.Name & "-Status"), Label)
            Status.Location = New Point(Player.Left, Player.Top)
            Status.BringToFront()
            PlayerFullScreen = ""

        Else
            Logging("Info - Full Screen Player: " & Player.Name)

            Player.Size = Panel.Size
            Player.Location = New Point(0, 0)
            Dim Status As Label = CType(Panel.Controls(Player.Name & "-Status"), Label)
            Status.Location = New Point(Player.Left, Player.Top)
            Status.BringToFront()
            PlayerFullScreen = Player.Name
        End If
    End Sub
    Public Sub PlayerMouseUP(sender As Object, e As _WMPOCXEvents_MouseUpEvent)
        ResizePlayer = ""
        MovePlayer = ""
        PlayerClickTime = DateTime.Now

    End Sub
    Public Sub VLCPlayerMouseUP(sender As Object, e As AxAXVLC.DVLCEvents_MouseUpEvent)
        ResizePlayer = ""
        MovePlayer = ""
        PlayerClickTime = DateTime.Now

    End Sub
    Public Sub PlayerStatusMouseUp(sender As Object, e As MouseEventArgs)
        ResizePlayer = ""
        MovePlayer = ""
        PlayerClickTime = DateTime.Now
    End Sub
    Public Sub PlayerStatusMouseDown(sender As Object, e As MouseEventArgs)
        If My.Settings.VideoPlayerType = "VLC" Then
            Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(sender.name.ToString.Replace("-Status", "")), AxAXVLC.AxVLCPlugin2)
            ResizePlayer = ""
            PlayerClickTime = DateTime.Now
            MovePlayer = Player.Name
            Dim PlayerXY As Point = Panel.MousePosition - Player.Location - Me.Location - New Point(14, 34) - Panel.Location
            MovePlayerMousePos = PlayerXY
        Else
            Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(sender.name.ToString.Replace("-Status", "")), AxWindowsMediaPlayer)
            ResizePlayer = ""
            PlayerClickTime = DateTime.Now
            MovePlayer = Player.Name
            Dim PlayerXY As Point = Panel.MousePosition - Player.Location - Me.Location - New Point(14, 34) - Panel.Location
            MovePlayerMousePos = PlayerXY
        End If



    End Sub
    Dim PlayerLastState As InputState = InputState.ERRORSTATE
    Public Sub VLCPlayerChangedState(PlayerName As String)
        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(PlayerName), AxAXVLC.AxVLCPlugin2)
        Dim state As InputState = DirectCast(Player.input.state, InputState)
        Dim Status As Label = CType(Panel.Controls(PlayerName & "-Status"), Label)
        Status.Text = Player.Name & " - " & state.ToString  'URL.ToString.Remove(0, PlayerCenter.URL.ToString.LastIndexOf("\") + 1)

        MainToolTip.SetToolTip(Status, state.ToString)
        'Status.BringToFront()
        Logging("Info - " & PlayerName & " Playstate Changed " & state.ToString)
        If state = InputState.PLAYING Then

            'CenterPlayerMissing.Visible = False
            Status.ForeColor = Color.Lime
            UpdatePlayBackSpeed()

            If CurrentTimeList.Items.Count = 1 Then
                Player.AutoLoop = True
            Else
                Player.AutoLoop = False
            End If
            If Tv_Explorer.Focused = False Then
                CurrentTimeList.Focus()
            End If
            Try
                'For Each time In CurrentTimeList.Items 'VLCPlayer.playlist
                '    If Player.mediaDescription.url.ToString.Remove(0, Player.mediaDescription.url.ToString.LastIndexOf("\")).Contains("_" & time.ToString.Replace(":", "-")) And CurrentTimeList.SelectedItem <> time.ToString.Replace("-", ":") Then
                '        If CurrentTimeList.SelectedItem <> time.ToString.Replace("-", ":") Then
                '            CurrentTimeList.SelectedIndex = -1
                '            CurrentTimeList.SelectedItem = time.ToString.Replace("-", ":")
                '        End If
                '    End If
                'Next
            Catch ex As Exception
            End Try

            If CurrentTimeList.SelectedIndex < 3 Then
                CurrentTimeList.TopIndex = 0
            Else
                CurrentTimeList.TopIndex = CurrentTimeList.SelectedIndex - 2
            End If

        ElseIf state = InputState.STOPPING Then
            Status.ForeColor = Color.Red
            If FolderViewing = True And FilePlayedOnce = True Then
                FilePlayedOnce = False
            End If

        ElseIf state = InputState.ENDED Then
            If CurrentTimeMaxPlayer = Player.Name Then
                If CurrentTimeList.Items.Count > 1 And EventTimeMove = False Then

                    'PlayersSTOP()
                    If CurrentTimeList.SelectedIndex = 0 Then
                        CurrentTimeList.SelectedIndices.Clear()
                        CurrentTimeList.SelectedIndex = CurrentTimeList.Items.Count - 1
                    Else
                        Dim LastIndex As Integer = CurrentTimeList.SelectedIndex
                        CurrentTimeList.SelectedIndices.Clear()
                        CurrentTimeList.SelectedIndex = LastIndex - 1
                    End If
                End If
            End If

        ElseIf state = InputState.PAUSED Then
            Status.ForeColor = Me.BackColor
            UpdatePlayBackSpeed()
        ElseIf state = InputState.IDLE Then
            'If Player.URL <> "" Then
            'Player.Ctlcontrols.play()
            'Else
            'CenterPlayerMissing.Visible = True
            Logging("Info - Front Camera - File is missing or corrupt")
            'End If
            Status.ForeColor = Color.LightGray
        ElseIf state = InputState.BUFFERING Then
            Status.ForeColor = Color.Gray

        Else
            Status.ForeColor = Color.DarkRed
            'Status.Text = Player.Name & " - " & Player.URL.ToString.Remove(0, Player.URL.ToString.LastIndexOf("\") + 1) & " --- ERROR ---"
            'CenterPlayerMissing.Visible = True
        End If
        Languages.FrontCamera()

    End Sub

    Public Sub PlayerChangedState(sender As Object, e As _WMPOCXEvents_PlayStateChangeEvent)
        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(sender.Name), AxWindowsMediaPlayer)
        Dim Status As Label = CType(Panel.Controls(sender.name & "-Status"), Label)
        Status.Text = Player.Name  '& " - " & Player.status 'URL.ToString.Remove(0, PlayerCenter.URL.ToString.LastIndexOf("\") + 1)
        MainToolTip.SetToolTip(Status, Player.status)
        'Status.BringToFront()
        Logging("Info - " & sender.name & " Playstate Changed " & Player.playState.ToString)

        If Player.playState = WMPLib.WMPPlayState.wmppsPlaying Then
            'CenterPlayerMissing.Visible = False
            Status.ForeColor = Color.Lime
            UpdatePlayBackSpeed()

            If CurrentTimeList.Items.Count = 1 Then
                Player.settings.setMode("loop", True)

            Else
                Player.settings.setMode("loop", False)

            End If
            If Tv_Explorer.Focused = False Then
                CurrentTimeList.Focus()
            End If
            Try
                For Each time In CurrentTimeList.Items
                    If Player.URL.Remove(0, Player.URL.LastIndexOf("\")).Contains("_" & time.ToString.Replace(":", "-")) And CurrentTimeList.SelectedItem <> time.ToString.Replace("-", ":") Then
                        If CurrentTimeList.SelectedItem <> time.ToString.Replace("-", ":") Then
                            CurrentTimeList.SelectedIndex = -1
                            CurrentTimeList.SelectedItem = time.ToString.Replace("-", ":")
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

            If CurrentTimeList.SelectedIndex < 3 Then
                CurrentTimeList.TopIndex = 0
            Else
                CurrentTimeList.TopIndex = CurrentTimeList.SelectedIndex - 2
            End If

        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsStopped Then
            Status.ForeColor = Color.Red
            If FolderViewing = True And FilePlayedOnce = True Then
                FilePlayedOnce = False
            End If

        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsMediaEnded Then
            If CurrentTimeMaxPlayer = Player.Name Then
                If CurrentTimeList.Items.Count > 1 And EventTimeMove = False Then

                    PlayersSTOP()
                    If CurrentTimeList.SelectedIndex = 0 Then
                        CurrentTimeList.SelectedIndices.Clear()
                        CurrentTimeList.SelectedIndex = CurrentTimeList.Items.Count - 1
                    Else
                        Dim LastIndex As Integer = CurrentTimeList.SelectedIndex
                        CurrentTimeList.SelectedIndices.Clear()
                        CurrentTimeList.SelectedIndex = LastIndex - 1
                    End If
                End If
            End If

        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsScanForward Then
            Status.ForeColor = Color.Blue
            UpdatePlayBackSpeed()
        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsPaused Then
            Status.ForeColor = Me.BackColor
            UpdatePlayBackSpeed()
        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsReady Then
            If Player.URL <> "" Then
                Player.Ctlcontrols.play()
            Else
                'CenterPlayerMissing.Visible = True
                Logging("Info - Front Camera - File is missing or corrupt")
            End If
            Status.ForeColor = Color.LightGray
        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsTransitioning Then
            Status.ForeColor = Color.Gray
        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsMediaEnded Then
            Status.ForeColor = Color.LightGreen
        ElseIf Player.playState = WMPLib.WMPPlayState.wmppsScanReverse Then
            Status.ForeColor = Color.BlueViolet
            'UpdatePlayBackSpeed()
        Else
            Status.ForeColor = Color.DarkRed
            Status.Text = Player.Name & " - " & Player.URL.ToString.Remove(0, Player.URL.ToString.LastIndexOf("\") + 1) & " --- ERROR ---"
            'CenterPlayerMissing.Visible = True
        End If
        Languages.FrontCamera()

    End Sub


    Private Sub MediaInfo_OpenStateChange(sender As Object, e As _WMPOCXEvents_OpenStateChangeEvent)

    End Sub

    Private Sub PlayersPAUSE()
        Try
            UpdatePlayBackSpeed()
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Player.playlist.pause()
                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Player.Ctlcontrols.pause()
                    End If
                Next
            End If
            Logging("Info - Players Paused")
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub PlayersPLAY()
        Try
            Dim AnyPlayerPlaying As Boolean = False
            Logging("Info - Players Play")
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Dim state As InputState = DirectCast(Player.input.state, InputState)
                        If state = InputState.PLAYING Then
                            AnyPlayerPlaying = True
                        End If
                        'Player.Ctlcontrols.play()
                    End If
                Next
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        If AnyPlayerPlaying = True Then

                            Player.playlist.play()
                            Player.playlist.pause()
                        Else
                            Player.playlist.play()
                        End If
                        '
                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        If Player.playState = WMPLib.WMPPlayState.wmppsPlaying Or Player.playState = WMPLib.WMPPlayState.wmppsScanForward Or Player.playState = WMPLib.WMPPlayState.wmppsScanReverse Then
                            AnyPlayerPlaying = True
                        End If
                        'Player.Ctlcontrols.play()
                    End If
                Next
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        If AnyPlayerPlaying = True Then
                            Player.Ctlcontrols.pause()
                            Player.settings.rate = 1
                            Player.Ctlcontrols.play()
                            Player.Ctlcontrols.pause()
                        Else
                            Player.Ctlcontrols.play()
                        End If
                        '
                    End If
                Next
            End If
            UpdatePlayBackSpeed()
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub PlayersSTOP()
        For Each control As Control In Panel.Controls
            If My.Settings.VideoPlayerType = "VLC" Then
                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                    Player.playlist.stop()
                End If
            Else
                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                    Player.Ctlcontrols.stop()
                End If
            End If
        Next

        Logging("Info - Players Stop")
    End Sub

    Dim FormatDriveLetter As String = ""

    Private Sub Tv_Explorer_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Tv_Explorer.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            If (File.Exists(e.Node.Tag.ToString) Or Directory.Exists(e.Node.Tag.ToString) Or e.Node.Tag.ToString = "Custom Folder") Then
                Tv_Explorer.SelectedNode = e.Node
                If e.Node.ToolTipText.Contains("Custom Folder") Then
                    ChangeCustomFolderToolStripMenuItem.Visible = True
                Else
                    ChangeCustomFolderToolStripMenuItem.Visible = False
                End If
                If e.Node.Tag.ToString.Count = 3 And e.Node.Tag.ToString.ToLower.Contains("c:\") = False Then
                    FormatDriveToolStripMenuItem.Visible = True
                    ConfirmToolStripMenuItem.Text = "Confirm - " & e.Node.Tag.ToString
                    FormatDriveLetter = e.Node.Tag.ToString
                Else
                    FormatDriveToolStripMenuItem.Visible = False
                    FormatDriveLetter = ""
                End If
            End If
        End If
    End Sub

    Private Sub BtnPLAY_Click(sender As Object, e As EventArgs) Handles BtnPLAY.Click
        PlayersPLAY()
    End Sub

    Private Sub BtnPAUSE_Click(sender As Object, e As EventArgs) Handles BtnPAUSE.Click
        Try

            PlayersPAUSE()
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Sub
    Dim VLCReverse As Boolean = False
    Private Sub BtnREVERSE_Click(sender As Object, e As EventArgs) Handles BtnREVERSE.Click
        Try
            If My.Settings.VideoPlayerType <> "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Player.Ctlcontrols.fastReverse()
                    End If
                Next
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub


    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If GroupBoxNewLayout.Visible = False And CustomFolderGroupBox.Visible = False Then
                If e.KeyData = (Keys.F5) Then
                    RefreshRootNodes()
                End If

                If e.KeyData = Keys.Space Or e.KeyData = Keys.P Then
                    CurrentTimeList.Focus()
                    If BtnPLAY.Focused = False And BtnPAUSE.Focused = False Then
                        PlayersPLAY()
                    End If
                    e.Handled = True
                End If
                If e.KeyValue = 188 Or (e.KeyData = Keys.Left And CurrentTimeList.Focus = True) Then '<
                    CurrentTimeList.Focus()
                    If TimeCodeBar.Value > TimeCodeBar.Minimum Then

                        For Each control As Control In Panel.Controls
                            If My.Settings.VideoPlayerType = "VLC" Then
                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                    If Player.input.time > 1000 Then
                                        Player.input.time -= 1000
                                    End If
                                End If
                            Else
                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                    Player.Ctlcontrols.currentPosition -= 1
                                End If
                            End If
                        Next

                        RefreshPlayers()
                    End If
                    e.Handled = True
                End If
                If e.KeyData = Keys.M Then
                    CurrentTimeList.Focus()
                    If TimeCodeBar.Value > TimeCodeBar.Minimum Then
                        For Each control As Control In Panel.Controls
                            If My.Settings.VideoPlayerType = "VLC" Then
                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                    If Player.input.time > 1000 / 36.0 Then
                                        Player.input.time -= 1000 / 36.0
                                    End If
                                End If
                            Else
                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                    Player.Ctlcontrols.currentPosition -= 1 / 36.0
                                End If
                            End If
                        Next

                        RefreshPlayers()
                    End If
                    e.Handled = True
                End If
                If e.KeyValue = 190 Or (e.KeyData = Keys.Right And CurrentTimeList.Focus = True) Then '>
                    CurrentTimeList.Focus()
                    If TimeCodeBar.Value < TimeCodeBar.Maximum Then

                        For Each control As Control In Panel.Controls
                            If My.Settings.VideoPlayerType = "VLC" Then
                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                    If Player.input.length - 1000 > Player.input.time Then
                                        Player.input.time += 1000
                                    End If
                                End If
                            Else
                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                    Player.Ctlcontrols.currentPosition += 1
                                End If
                            End If
                        Next

                        RefreshPlayers()
                    End If
                    e.Handled = True
                End If
                If e.KeyValue = 191 Then '?
                    CurrentTimeList.Focus()
                    If TimeCodeBar.Value < TimeCodeBar.Maximum Then
                        For Each control As Control In Panel.Controls
                            If My.Settings.VideoPlayerType = "VLC" Then
                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                    If Player.input.length - (1000 / 36) > Player.input.time Then
                                        Player.input.time += 1000 / 36
                                    End If
                                End If
                            Else
                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                    Player.Ctlcontrols.currentPosition += 1 / 36.0
                                End If
                            End If
                        Next

                        RefreshPlayers()
                    End If
                    e.Handled = True
                End If
                If e.KeyValue = 189 Then '-
                    CurrentTimeList.Focus()
                    If TrackBar2.Value > TrackBar2.Minimum Then
                        TrackBar2.Value -= 1
                    End If
                    UpdatePlayBackSpeed()
                    e.Handled = True
                End If
                If e.KeyValue = 187 Then '+
                    CurrentTimeList.Focus()
                    If TrackBar2.Value < TrackBar2.Maximum Then
                        TrackBar2.Value += 1
                    End If
                    UpdatePlayBackSpeed()
                    e.Handled = True
                End If
                If e.KeyData = Keys.S Then
                    CurrentTimeList.Focus()
                    For Each control As Control In Panel.Controls
                        If My.Settings.VideoPlayerType = "VLC" Then
                            If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                Player.playlist.stop()
                            End If
                        Else
                            If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                Player.Ctlcontrols.stop()
                            End If
                        End If
                    Next
                    e.Handled = True
                End If
                If e.KeyData = Keys.R Then
                    CurrentTimeList.Focus()
                    For Each control As Control In Panel.Controls
                        If My.Settings.VideoPlayerType = "VLC" Then
                            If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                If Player.input.time = 500 Then
                                    Player.input.time -= 500
                                End If
                            End If
                        Else
                            If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                Player.Ctlcontrols.fastReverse()
                            End If
                        End If
                    Next
                    e.Handled = True
                End If
                If e.KeyData = Keys.Escape Or e.KeyData = Keys.F Then
                    CurrentTimeList.Focus()

                    e.Handled = True
                End If
            End If

        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub PreviewPlayerExist(PlayerName As String)
        Try

            If IsNothing(PreviewPanel.Controls(PlayerName)) Then
                Logging("Info - Creating New Preview Player: " & PlayerName)
                If My.Settings.VideoPlayerType = "VLC" Then
                    Dim VLCPlayer As New AxAXVLC.AxVLCPlugin2
                    VLCPlayer.Name = PlayerName
                    VLCPlayer.Location = New Point(0, 0)
                    VLCPlayer.Size = New Size(200, 200)

                    VLCPlayer.Dock = DockStyle.Fill

                    PreviewPanel.Controls.Add(VLCPlayer)


                Else
                    Dim NewPlayer As New AxWMPLib.AxWindowsMediaPlayer
                    NewPlayer.Name = PlayerName

                    NewPlayer.Dock = DockStyle.Fill

                    PreviewPanel.Controls.Add(NewPlayer)
                    NewPlayer.Location = New Point(0, 0)
                    NewPlayer.Size = New Size(200, 200)
                    NewPlayer.Visible = True


                End If
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub Tv_Explorer_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles Tv_Explorer.NodeMouseHover
        Try
            PreviewPlayerExist("Preview")
            Dim Selection = e.Node.Tag.ToString
            Dim a = FileSize(Selection)
            For Each control As Control In PreviewPanel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(PreviewPanel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        If e.Node.ImageKey.ToString() = ".mp4" And FileSize(Selection) > 1000 Then
                            PREVIEWBox.BringToFront()
                            PREVIEWBox.Visible = True

                            Player.FullscreenEnabled = False
                            Player.CtlVisible = False
                            Player.Toolbar = False
                            Player.AutoLoop = True
                            Player.AutoPlay = True
                            Player.playlist.items.clear()
                            Dim URL As String = ("file:\\\" & e.Node.Tag.ToString).Replace("\", "/")
                            Player.playlist.add(URL)

                            Player.playlist.play()
                            Player.input.rate = 20
                            Player.Visible = True
                        Else
                            PREVIEWBox.Visible = False
                            Player.playlist.stop()
                        End If
                    End If
                Else
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(PreviewPanel.Controls(control.Name), AxWindowsMediaPlayer)

                        If e.Node.ImageKey.ToString() = ".mp4" And FileSize(Selection) > 1000 Then
                            PREVIEWBox.BringToFront()
                            PREVIEWBox.Visible = True
                            Player.URL = e.Node.Tag.ToString
                            Player.settings.setMode("loop", True)

                            Player.enableContextMenu = False
                            Player.settings.rate = 20
                            Player.Ctlcontrols.play()
                            Player.uiMode = "none"
                            Player.Ctlenabled = False

                        Else
                            PREVIEWBox.Visible = False
                            Player.Ctlcontrols.stop()
                        End If

                    End If
                End If
            Next





        Catch ex As Exception
            'If Debug_Mode = True Then
            '    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
            'Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            Try
                PREVIEWBox.Visible = False

            Catch eex As Exception
            End Try
        End Try

    End Sub

    Private Sub Tv_Explorer_MouseLeave(sender As Object, e As EventArgs) Handles Tv_Explorer.MouseLeave
        Try
            For Each control As Control In PreviewPanel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(PreviewPanel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Player.playlist.stop()
                    End If
                Else
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(PreviewPanel.Controls(control.Name), AxWindowsMediaPlayer)
                        Player.Ctlcontrols.stop()
                    End If
                End If
            Next
        Catch ex As Exception
            'If Debug_Mode = True Then
            '    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
            'Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Try
            PREVIEWBox.Visible = False
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub Tv_Explorer_MouseMove(sender As Object, e As MouseEventArgs) Handles Tv_Explorer.MouseMove
        Dim Location As Point = MainForm.MousePosition - Me.Location
        If Location.X + PREVIEWBox.Width + 75 > Me.Width Then
            Location.X -= PREVIEWBox.Width + 50
        Else
            Location.X += 50
        End If

        If Location.Y + PREVIEWBox.Height + 20 > Me.Height Then
            Location.Y -= Location.Y + PREVIEWBox.Height + 20 - Me.Height
            Location.Y -= 45
        Else
            Location.Y -= 45
        End If
        PREVIEWBox.Location = Location
    End Sub

    Private Sub MainForm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        UpdatePlayersLayout()
    End Sub


    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            If AspectRatio.Text = "" Then
                If AspectRatio.Items.Count > 0 Then
                    AspectRatio.SelectedItem = 0
                End If
                If AspectName.Items.Count > 0 Then
                    AspectName.SelectedItem = 0
                End If

            End If

            Dim x As Integer
            Dim y As Integer
            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(AspectRatio.Text))
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(":")
                Dim currentRow As String() = {"4", "3"}
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                    Catch ex As Exception
                    End Try
                End While
                If currentRow.Count = 2 Then
                    x = Int(currentRow(0))
                    y = Int(currentRow(1))
                End If
            End Using


            If x > 0 And y > 0 Then
                If ((Me.Height - GroupBoxEXPLORER.Height - EventTimeCodeBar.Height - 25) / y) * x > Me.Width - 35 Then
                    Panel.Width = Me.Width - 30
                    Panel.Height = (Panel.Width / x) * y
                    Panel.Left = (Me.Width - Panel.Width - 15) / 2

                Else
                    Panel.Height = Me.Height - GroupBoxEXPLORER.Height - EventTimeCodeBar.Height - 25
                    Panel.Width = (Panel.Height / y) * x
                    Panel.Left = (Me.Width - Panel.Width - 15) / 2

                End If
            End If

            For Each control As Control In Panel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)

                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Player.Left = (PlayerLeft.Text / 100) * Panel.Width 'Panel.Width / (100 / PlayerLeft.Text)
                        Player.Top = (PlayerTop.Text / 100) * Panel.Height 'Panel.Height / (100 / PlayerTop.Text)

                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Status.Location = New Point(Player.Left, Player.Top)
                        PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                        PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                        PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                        'Player.BringToFront()
                        'Status.BringToFront()
                        PlayerTop.BringToFront()
                        PlayerLeft.BringToFront()

                    End If

                Else
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)

                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Player.Left = (PlayerLeft.Text / 100) * Panel.Width 'Panel.Width / (100 / PlayerLeft.Text)
                        Player.Top = (PlayerTop.Text / 100) * Panel.Height 'Panel.Height / (100 / PlayerTop.Text)

                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Status.Location = New Point(Player.Left, Player.Top)
                        PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                        PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                        PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                        'Player.BringToFront()
                        'Status.BringToFront()
                        PlayerTop.BringToFront()
                        PlayerLeft.BringToFront()

                    End If
                End If
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub MainForm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

    End Sub

    Public Sub SendClosingReport()
        Try
            Dim CPUIDchanged As String = ""
            If OriginalCPUID <> CPUID Then
                CPUIDchanged = OriginalCPUID & "=" & CPUID
            End If
            My.Settings.Save()

            Dim CurrentDateTime As DateTime = My.Computer.Clock.LocalTime
            Dim DateCode As String = CurrentDateTime.Year.ToString("0000") & "-" & CurrentDateTime.Month.ToString("00") & "-" & CurrentDateTime.Day.ToString("00")
            'Dim elapsedTime As TimeSpan = CurrentDateTime.Subtract(StartTime)
            Logging("Info - TeslaCam Viewer Closing - " & (ActiveSeconds / 60).ToString(".00").Replace(",", "."))

            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(GoogleFormURL & "/formResponse?usp=pp_url" &
                                                                                    "&entry.658672747=" & Uri.EscapeUriString(CPUID) &
                                                                                    "&entry.568623895=" & Uri.EscapeUriString(CPUIDchanged) &
                                                                                    "&entry.1086997034=" & Uri.EscapeUriString(CurrentVersion) &
                                                                                    "&entry.1505263955=" & DateCode &
                                                                                    "&entry.1922677689=" & (ActiveSeconds / 60).ToString(".00").Replace(",", ".") &
                                                                                    "&entry.1644399542=" & ViewedClips &
                                                                                    "&entry.308391636=" & SavedClips &
                                                                                    "&entry.876718684=" & FixedFolders &
                                                                                    Uri.EscapeUriString(LinksSelected) &
                                                                                    (RenderViewsSelected) &
                                                                                    "&entry.729160587=" & Uri.EscapeUriString(My.Settings.VideoPlayerType) &
                                                                                    "&entry.1520185355=" & Uri.EscapeUriString(UpDated) &
                                                                                    "&entry.37629513=" & Uri.EscapeUriString(curTimeZone.StandardName) &
                                                                                    "&entry.1103660004=" & Uri.EscapeUriString(My.Settings.UserLanguage) &
                                                                                    "&entry.2057430665=" & "&submit=Submit") ' &entry.2057430665=(Crash+Report)
            request.Timeout = 3000
            request.GetResponse()

        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try


    End Sub
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Enabled = False

        If Debug_Mode = False And FormIsClosing = False And My.Settings.UserAgreed = True Then
            FormIsClosing = True
            SendClosingReport()
        Else
            FormIsClosing = True
        End If
        Try
            For Each control As Control In Panel.Controls
                If My.Settings.VideoPlayerType = "VLC" Then
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Player.playlist.items.clear()
                        Player.Dispose()
                    End If
                Else

                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Player.Ctlcontrols.stop()
                        Player.close()
                    End If
                End If
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

        My.Settings.VideoPlayerType = VideoPlayerType.Text
        My.Settings.LastAspectRatio = AspectName.Text
        My.Settings.Save()



        Try
            Dim a As Integer = 0
            For Each PRunning As Process In System.Diagnostics.Process.GetProcessesByName("ffmpeg")
                PRunning.Kill()
                a += 1
            Next
            'MessageBox.Show("Killed " & a & " Processes ", "Kill Process", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Try
            Dim a As String = Path.GetTempPath() & "TeslaCamViewer\"
            System.IO.Directory.Delete(Path.GetTempPath() & "TeslaCamViewer\", True)
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Try
            System.IO.Directory.Delete(Path.GetTempPath() & "TeslaCamFix\", True)
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Logging("Info - TeslaCam Viewer CLOSED - ")
    End Sub



    Private Sub Donation_Click(sender As Object, e As EventArgs) Handles Donation.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=8UKFUQCU9476N&source=url")
        LinksSelected = LinksSelected & "&entry.839831962=Donate"
        'My.Application.LinksSelected = LinksSelected
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("https://twitter.com/nate_mccomb")
        LinksSelected = LinksSelected & "&entry.839831962=Twitter"
        'My.Application.LinksSelected = LinksSelected
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Process.Start("https://twitter.com/nate_mccomb")
        LinksSelected = LinksSelected & "&entry.839831962=Twitter"
        'My.Application.LinksSelected = LinksSelected
    End Sub

    Private Sub UPDATELabel_Click(sender As Object, e As EventArgs) Handles UPDATELabel.Click

        If UPDATELabel.BackColor = Color.BlueViolet Then
            Process.Start("https://github.com/NateMccomb/TeslaCamViewerII/#install")
            LinksSelected = LinksSelected & "&entry.839831962=Update"
        Else
            Process.Start("https://github.com/NateMccomb/TeslaCamViewerII")
            LinksSelected = LinksSelected & "&entry.839831962=GitHub"
        End If
        'My.Application.LinksSelected = LinksSelected
    End Sub
    Dim CurrentTimeMaxPlayer As String = ""
    Dim EventTimeCodeOffset As Double = 0
    Private Sub CurrentTimeList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CurrentTimeList.SelectedIndexChanged
        Try
            If CurrentTimeList.SelectedItem <> Nothing Then

                Dim EventOffset As Double = 0
                For i = CurrentTimeList.SelectedIndex + 1 To CurrentTimeList.Items.Count - 1
                    Dim MaxDuration As Double = 0
                    For Each control As Control In FileDurations.Controls
                        If control.GetType Is GetType(ListBox) Then
                            Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                            If FromNumUS(PlayerTimes.Items.Item(i)) > MaxDuration Then
                                MaxDuration = FromNumUS(PlayerTimes.Items.Item(i))
                            End If
                        End If
                    Next
                    EventOffset += MaxDuration
                Next
                EventTimeCodeOffset = FromNumUS(EventOffset) * 10


                Logging("Info - Selected Time: " & CurrentTimeList.SelectedItem)
                If CurrentTimeList.SelectedItems.Count = 1 Then
                    Dim MaxDuration As Double = 0
                    For Each control As Control In FileDurations.Controls
                        If control.GetType Is GetType(ListBox) Then
                            Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                            If PlayerTimes.Items.Count > 1 Then
                                If FromNumUS(PlayerTimes.Items.Item(CurrentTimeList.SelectedIndex)) > MaxDuration Then
                                    MaxDuration = FromNumUS(PlayerTimes.Items.Item(CurrentTimeList.SelectedIndex))
                                    CurrentTimeMaxPlayer = control.Name
                                    TimeCodeBar.Maximum = MaxDuration * 10
                                    'TimeCodeBar.Value = TimeCodeBar.Minimum
                                End If
                            End If
                        End If
                    Next



                    Dim PlayerMin As Double = 0
                    Dim playerSec As Double = 0

                    If MaxDuration * 10 > 599 Then
                        Dim TempTime As Double = MaxDuration
                        For i = 1 To TempTime / 60
                            PlayerMin += 1
                            TempTime -= 60
                        Next
                        playerSec = TempTime
                    Else

                        playerSec = MaxDuration
                    End If
                    MainPlayerMaxTimecode.Text = PlayerMin.ToString("00") & ":" & playerSec.ToString("00.00")


                    If FullCenterCameraName.Remove(0, FullCenterCameraName.LastIndexOf("\")).Contains(CurrentTimeList.SelectedItem.ToString.Replace(":", "-")) = False Then
                        Dim di As New IO.DirectoryInfo(FullCenterCameraName.Remove(FullCenterCameraName.LastIndexOf("\")))
                        Dim fis = di.GetFiles().OrderByDescending(Function(p) p.Name).ToArray()
                        If fis.Length >= 0 Then
                            For Each item In fis
                                If item.Name.StartsWith("20") = True And item.Name.Contains("_") = True And item.Name.Contains("-") = True And item.Name.ToLower.EndsWith(".mp4") Then

                                    If item.Name.Contains(CurrentTimeList.SelectedItem.Replace(":", "-")) Then

                                        Dim FileGroup As String = item.FullName.Remove(item.FullName.LastIndexOf("-"))
                                        FilePlayedOnce = False
                                        PlayersSTOP()
                                        FullCenterCameraName = FileGroup 'item.FullName.Remove(item.FullName.LastIndexOf("\"))
                                        'here
                                        If My.Settings.VideoPlayerType = "VLC" Then
                                            For Each control As Control In Panel.Controls
                                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                                    Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                                    If File.Exists(FileGroup & "-" & control.Name & ".mp4") Then
                                                        'VLCPlayer.BaseURL = (FileGroup & "-" & control.Name & ".mp4")
                                                        VLCPlayer.playlist.stop()
                                                        VLCPlayer.playlist.items.clear()
                                                        VLCPlayer.AutoPlay = False

                                                        Dim URL As String = ("file:\\\" & FileGroup & "-" & control.Name & ".mp4").Replace("\", "/")
                                                        VLCPlayer.playlist.add(URL)
                                                        'AxVLCPlugin21.
                                                    Else
                                                        VLCPlayer.playlist.items.clear()
                                                        'VLCPlayer.BaseURL = "null.mp4"

                                                    End If
                                                End If
                                            Next

                                            For Each control As Control In Panel.Controls
                                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                                    Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                                    VLCPlayer.playlist.play()

                                                End If
                                            Next
                                        Else
                                            For Each control As Control In Panel.Controls
                                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                                    If File.Exists(FileGroup & "-" & control.Name & ".mp4") Then
                                                        Player.URL = (FileGroup & "-" & control.Name & ".mp4")
                                                    Else
                                                        Player.URL = "null.mp4"
                                                        Player.close()
                                                    End If
                                                End If
                                            Next

                                            For Each control As Control In Panel.Controls
                                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                                    Player.Ctlcontrols.play()
                                                End If
                                            Next
                                        End If


                                        UpdatePlayBackSpeed()
                                        If Tv_Explorer.Focused = False Then
                                            CurrentTimeList.Focus()
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                    Else
                        If My.Settings.VideoPlayerType = "VLC" Then
                            For Each control As Control In Panel.Controls
                                If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                    Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                    VLCPlayer.playlist.play()

                                End If
                            Next
                        Else
                            For Each control As Control In Panel.Controls
                                If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                    Player.Ctlcontrols.play()
                                End If
                            Next
                        End If
                        UpdatePlayBackSpeed()
                    End If
                Else
                    PlayersPAUSE()
                End If
                If RenderEnabled = True Then
                    SettingsBTN.Enabled = True
                    RenderBTN.Enabled = True
                Else
                    SettingsBTN.Enabled = False
                    RenderBTN.Enabled = False
                End If

            Else
                SettingsBTN.Enabled = False
                RenderBTN.Enabled = False
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub ClipSelectUP_Click(sender As Object, e As EventArgs) Handles ClipSelectUP.Click
        If CurrentTimeList.SelectedIndex > 0 Then
            Dim LastIndex As Integer = CurrentTimeList.SelectedIndex
            CurrentTimeList.SelectedIndices.Clear()
            CurrentTimeList.SelectedIndex = LastIndex - 1
        End If
    End Sub

    Private Sub ClipSelectDOWN_Click(sender As Object, e As EventArgs) Handles ClipSelectDOWN.Click
        If CurrentTimeList.SelectedIndex < CurrentTimeList.Items.Count - 1 Then
            Dim LastIndex As Integer = CurrentTimeList.SelectedIndex
            CurrentTimeList.SelectedIndices.Clear()
            CurrentTimeList.SelectedIndex = LastIndex + 1
        End If
    End Sub

    Private Sub CurrentTimeList_KeyDown(sender As Object, e As KeyEventArgs) Handles CurrentTimeList.KeyDown
        If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
            e.SuppressKeyPress = True
            Return
        End If
    End Sub
    Public RenderingStartTime As Date
    Dim LastPlayedFPS As Double
    Dim CurrentPlayedFPS As Double
    Dim SaveAsFileName As String = ""
    Private Sub RenderBTN_Click(sender As Object, e As EventArgs) Handles RenderBTN.Click
        Try
            Logging("Info - Render Button Pressed")
            SelectedNumberOfVideos = 0
            Dim sfd As New SaveFileDialog
            sfd.Title = "Save Video As..."
            sfd.FileName = "Untitled"
            sfd.DefaultExt = ".mp4"
            sfd.AddExtension = True
            sfd.Filter = "MP4 Files (*.mp4*)|*.mp4|AVI Image (.avi)|*.avi|Gif Image (.gif)|*.gif"
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                Logging("Info - Save As file " & sfd.FileName)
                SaveAsFileName = sfd.FileName
            Else
                Exit Sub
            End If
            GroupBoxExportSettings.Enabled = False
            VideoRendering.Visible = True
            RenderingStartTime = Now

            Dim playbackSpeed As Double = TrackBar2.Value / 10
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        VLCPlayer.playlist.pause()

                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Player.Ctlcontrols.pause()
                    End If
                Next
            End If

            RenderFileCount = 0
            RenderFileCountNotDone = 0
            RenderedVideoTotalCount = 0
            RenderFileProgress.Text = "---"
            ThreadsRunningLabel.Text = "---"

            Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamViewer")
            If Not dir.Exists Then
                FileSystem.MkDir(Path.GetTempPath() & "TeslaCamViewer")
            End If
            Try
                System.IO.File.Delete(Path.GetTempPath() & "TeslaCamViewer\join.txt")
            Catch ex As Exception
                If Debug_Mode = True Then
                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try

            Dim JoinFile As System.IO.StreamWriter
            JoinFile = My.Computer.FileSystem.OpenTextFileWriter(Path.GetTempPath() & "TeslaCamViewer\join.txt", True, System.Text.Encoding.ASCII)

            If CurrentTimeList.Items.Count > 0 Then
                Dim di As New IO.DirectoryInfo(FullCenterCameraName.Remove(FullCenterCameraName.LastIndexOf("\")))
                Dim fis = di.GetFiles().OrderByDescending(Function(p) p.Name).ToArray() '(Function(fi) fi.Name.Contains("front")).ToArray()
                Dim FilesFound As Boolean = False

                DurationProgressBar.Style = ProgressBarStyle.Marquee
                DurationProgressBar.Maximum = 1
                DurationProgressBar.Value = 0






                Dim TotalDuration As Double = 0
                For z As Integer = 0 To CurrentTimeList.Items.Count - 1
                    Dim i As Integer = CurrentTimeList.Items.Count - 1 - z
                    Dim Playerinputfiles As New List(Of String)
                    Dim PlayerNamesByZ As New List(Of String)
                    Dim FileGroup As String = ""

                    Dim StartTime As Double = -1
                    Dim EndTime As Double = 0
                    Dim CurrentDuration As Double = MaxDurationsList.Items.Item(i) * 10
                    TotalDuration += CurrentDuration
                    If RenderInTime <= TotalDuration Then
                        If RenderInTime <= TotalDuration Then
                            StartTime = RenderInTime - TotalDuration + CurrentDuration
                            If StartTime < 0 Then
                                StartTime = 0
                            End If
                        End If
                    End If

                    If RenderOutTime >= TotalDuration Then
                        EndTime = CurrentDuration - StartTime
                    Else
                        EndTime = CurrentDuration - (TotalDuration - RenderOutTime)
                    End If
                    If StartTime >= 0 And EndTime >= 0 Then
                        Dim a As String = ""
                        If EndTime > StartTime Then
                            EndTime = EndTime - StartTime
                        End If

                        StartTime = StartTime / playbackSpeed
                        EndTime = EndTime / playbackSpeed

                        Dim MaxDuration As Double = 0
                        Dim CurrentMaxPlayer As String = ""
                        For Each control As Control In FileDurations.Controls
                            If control.GetType Is GetType(ListBox) Then
                                Dim PlayerTimes As ListBox = CType(FileDurations.Controls(control.Name), ListBox)
                                If PlayerTimes.Items.Count > 0 Then
                                    If FromNumUS(PlayerTimes.Items.Item(i)) > MaxDuration Then
                                        MaxDuration = FromNumUS(PlayerTimes.Items.Item(i))
                                        CurrentMaxPlayer = control.Name
                                    End If
                                End If
                            End If
                        Next

                        Dim StartMin As Integer = 0
                        Dim StartSec As Double = 0

                        Dim EndMin As Integer = 0
                        Dim EndSec As Double = 0


                        If StartTime > 599 Then
                            Dim TempTime As Double = StartTime
                            For t = 1 To StartTime / 600
                                StartMin += 1
                                TempTime -= 600
                            Next
                            StartSec = TempTime / 10
                        Else
                            StartSec = StartTime / 10
                        End If
                        If EndTime > 599 Then
                            Dim TempTime As Double = EndTime
                            For t = 1 To EndTime / 600
                                EndMin += 1
                                TempTime -= 600
                            Next
                            EndSec = TempTime / 10
                        Else
                            'EndMin = 0
                            EndSec = EndTime / 10
                        End If

                        Dim StartTimeString As String = "-ss 00:" & StartMin.ToString("00") & ":" & StartSec.ToString("00.00").Replace(",", ".")
                        Dim EndTimeString As String = " -t 00:" & EndMin.ToString("00") & ":" & EndSec.ToString("00.00").Replace(",", ".")

                        RenderFileCountNotDone += 1

                        For Each item In fis
                            For Each PlayerEnabled As CheckBox In PlayersEnabledPanel.Controls
                                If PlayerEnabled.Checked = True And item.Name.Contains(PlayerEnabled.Name) Then

                                    If item.Name.StartsWith("20") = True And item.Name.Contains("_") = True And item.Name.Contains("-") = True And item.Name.ToLower.EndsWith(".mp4") Then
                                        If item.FullName.Remove(0, item.FullName.LastIndexOf("\")).Contains(CurrentTimeList.Items(i).Replace(":", "-")) Then
                                            FileGroup = item.FullName.Remove(item.FullName.LastIndexOf("-"))
                                            If Not Playerinputfiles.Contains(CurrentTimeList.Items(i).Replace(":", "-")) Then
                                                If File.Exists(item.FullName) And FileSize(item.FullName) > 1000 Then
                                                    Playerinputfiles.Add("-i " & Chr(34) & (item.FullName) & Chr(34) & " ")
                                                    FilesFound = True
                                                    'Else
                                                    '   Playerinputfiles.Add("-i " & Chr(34) & (Path.GetTempPath() & "Black.mp4") & Chr(34) & " ")
                                                End If
                                                'Dim playername As String = item.Name.Remove(0, item.Name.LastIndexOf("-")).Replace(".mp4", "")

                                                If My.Settings.VideoPlayerType = "VLC" Then
                                                    Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(PlayerEnabled.Name), AxAXVLC.AxVLCPlugin2)

                                                    PlayerNamesByZ.Add(Panel.Controls.GetChildIndex(Player).ToString("000") & "|" & PlayerEnabled.Name)
                                                Else
                                                    Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(PlayerEnabled.Name), AxWindowsMediaPlayer)

                                                    PlayerNamesByZ.Add(Panel.Controls.GetChildIndex(Player).ToString("000") & "|" & PlayerEnabled.Name)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next

                        If FilesFound = True Then
                            Dim FileName As String = Path.GetTempPath() & "TeslaCamViewer\RenderedFile" & i & ".mp4"
                            JoinFile.Write("file RenderedFile" & i & ".mp4" & vbCrLf)
                            Logging("Info - file RenderedFile" & i & ".mp4 - ADDED to JoinFile")
                            If IsFileInUse(FileName) And File.Exists(FileName) Then
                                MessageBox.Show("Unable to save video due to file in use" & vbCrLf & FileName, "Error With Selected File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            FFmpegOutput.Text = ""
                            Dim saveas As String = Chr(34) & FileName & Chr(34)
                            RenderOutputFile = FileName
                            Dim p As New Process
                            p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
                            p.StartInfo.UseShellExecute = False
                            p.StartInfo.CreateNoWindow = True
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            p.StartInfo.RedirectStandardError = True
                            p.EnableRaisingEvents = True
                            Application.DoEvents()

                            Dim FileDateTime As String = FileGroup.Remove(0, FileGroup.LastIndexOf("\") + 1).Replace("_", " ").Replace("-", ".")
                            If FileDateTime.Length = 16 Then
                                FileDateTime += ".00"
                            End If
                            Dim FileTime As DateTime '= DateTime.ParseExact("00000000000000", "yyyyMMddHHmmss", Nothing)
                            Dim framerate As Double = 36
                            Try
                                FileTime = DateTime.ParseExact(FileDateTime.Replace(" ", "").Replace(".", ""), "yyyyMMddHHmmss", Nothing)
                                'framerate = Int(PlayerCenter.network.frameRate) / 100
                            Catch ex As Exception
                                If Debug_Mode = True Then
                                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                            End Try

                            AddHandler p.ErrorDataReceived, AddressOf proccess_OutputDataReceived
                            AddHandler p.OutputDataReceived, AddressOf proccess_OutputDataReceived
                            AddHandler p.Exited, AddressOf proc_Exited

                            Dim dirWindowsFolder As DirectoryInfo = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System))
                            Dim FontFile As String = Path.Combine(dirWindowsFolder.FullName, "Fonts\ariblk.ttf")
                            FontFile = FontFile.Replace("\", "\\").Replace(":", "\:")


                            'ffmpeg -i input.mp4 -vf "drawtext=enable='between(t,12,3*60)':fontfile=/usr/share/fonts/truetype/freefont/FreeSerif.ttf: text='Test Text'" -acodec copy output.mp4

                            Dim Mirror As String = ""
                            PlayerNamesByZ.Sort()
                            PlayerNamesByZ.Reverse()

                            Dim InputFiles As String = ""
                            Dim VideoSize As String = ""
                            Dim VideoStartLocation As String = ""
                            Dim IndexCount As Integer = 0

                            For Each zIndex In PlayerNamesByZ
                                For Each item In Playerinputfiles
                                    Dim PlayerName As String = zIndex.Remove(0, zIndex.IndexOf("|") + 1)
                                    If item.Contains(PlayerName) Then
                                        If FlipLREnable.Checked Then
                                            If PlayerName.ToLower.Contains("left") Then
                                                PlayerName = PlayerName.ToLower.Replace("left", "right")
                                            ElseIf PlayerName.ToLower.Contains("right") Then
                                                PlayerName = PlayerName.ToLower.Replace("right", "left")
                                            End If
                                        End If
                                        InputFiles += item

                                        Dim PlayerSize As Label = CType(Panel.Controls(PlayerName & "-Size"), Label)
                                        Dim PlayerTop As Label = CType(Panel.Controls(PlayerName & "-Top"), Label)
                                        Dim PlayerLeft As Label = CType(Panel.Controls(PlayerName & "-Left"), Label)
                                        Mirror = ""
                                        If MirrorLREnable.Checked = True And (PlayerName.ToLower.Contains("left") Or PlayerName.ToLower.Contains("right")) Then
                                            Mirror = " hflip,"
                                        End If

                                        If MirrorRearEnable.Checked = True And (PlayerName.ToLower.Contains("rear") Or PlayerName.ToLower.Contains("back")) Then
                                            Mirror = " hflip,"
                                        End If

                                        '"[1:v]" & Mirror & " setpts=PTS-STARTPTS, scale=" & 640 / SelectedQuality & "x" & 480 / SelectedQuality & " [topright];"

                                        Dim Shortest As String = "overlay=shortest=0"
                                        Dim TopWaterMark As String = ""
                                        If PlayerName = CurrentMaxPlayer Then
                                            Shortest = "overlay=shortest=1"

                                        End If

                                        If PlayerNamesByZ.Item(PlayerNamesByZ.Count - 1).Contains(PlayerName) Then
                                            Dim WatermarkHightOffset As Integer = 0

                                            Dim Difference = ToNumUS(Int(OutputHeight * (PlayerSize.Text / 100)) + Int(OutputHeight * (PlayerTop.Text / 100))) - OutputHeight
                                            If Difference > 0 Then
                                                WatermarkHightOffset = ToNumUS(Int((Difference / Int(OutputHeight * (PlayerSize.Text / 100))) * 960)) + 10
                                            End If
                                            If 930 - WatermarkHightOffset > 960 Or 930 - WatermarkHightOffset < 0 Then
                                                WatermarkHightOffset = 0

                                            End If


                                            Dim WatermarkTeslaCam As String = " drawtext=text='TeslaCam Viewer II - Ver." & CurrentVersion & "':x=(375):y=(" & 930 - WatermarkHightOffset & "):fontfile='" & FontFile & "':fontsize=25:fontcolor=gray,"
                                            Dim WatermarkDateTime As String = " drawtext=text='" & FileDateTime & "':x=(5):y=(" & 925 - WatermarkHightOffset & "):fontfile='" & FontFile & "':fontsize=30:fontcolor=white,"
                                            Dim WatermarkTimecode As String = " drawtext=fontfile='" & FontFile & "':text='\ \ \ \ \ ':x=1000:y=(" & 925 - WatermarkHightOffset & "):fontsize=35:fontcolor=white:shadowcolor=black:shadowx=1:shadowy=1:timecode='" & FileTime.Hour.ToString("00") & "\:" & FileTime.Minute.ToString("00") & "\:" & FileTime.Second.ToString("00") & "\:" & (FileTime.Millisecond / 10).ToString("00") & "':timecode_rate=" & framerate & ","

                                            Dim SentryModeTrigger As String = ""
                                            If DisplaySentryIndicator.Checked = True And CurrentTimeList.Items.Count > 1 Then
                                                If CurrentTimeList.Items.Item(1) = CurrentTimeList.Items(i) Then
                                                    SentryModeTrigger = " drawtext=text='Sentry':enable='between(t," & Int(SentryTriggerTime) - 1 & "," & Int(SentryTriggerTime) + 4 & ")':x=(920):y=(" & 925 - WatermarkHightOffset & "):fontfile='" & FontFile & "':fontsize=30:fontcolor=red,"
                                                End If
                                            End If


                                            TopWaterMark = WatermarkTeslaCam & WatermarkDateTime & WatermarkTimecode & SentryModeTrigger
                                        End If
                                        'Dim playbackSpeedCode As String = " -filter:v " & Chr(34) & "setpts=" & 1 / playbackSpeed & "*PTS" & Chr(34)
                                        VideoSize += "[" & IndexCount & ":v]" & Mirror & TopWaterMark & " setpts=" & ToNumUS(1 / playbackSpeed) & "*PTS, scale=" & ToNumUS(Int(((OutputHeight * (PlayerSize.Text / 100)) / 3) * 4)) & "x" & ToNumUS(Int(OutputHeight * (PlayerSize.Text / 100))) & " [" & PlayerName & "];"

                                        '"[center] overlay=shortest=1 [tmp1];[tmp1][topright] overlay=shortest=0:x=" & 1280 / SelectedQuality & " [tmp2];[tmp2][bottomright] overlay=shortest=0:x=" & 1280 / SelectedQuality & ":y=" & 480 / SelectedQuality 
                                        Dim NextVideo As String = ""
                                        If PlayerNamesByZ.Count - 1 > IndexCount Then
                                            NextVideo = " [tmp" & IndexCount + 1 & "];[tmp" & IndexCount + 1 & "]"
                                        End If
                                        VideoStartLocation += "[" & PlayerName & "] " & Shortest & ":x=" & ToNumUS(Int((PlayerLeft.Text / 100) * OutputWidth)) & ":y=" & ToNumUS(Int((PlayerTop.Text / 100) * OutputHeight)) & NextVideo

                                        Dim PlayerLayout As String = (PlayerName & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & "True" & "|" & zIndex.Remove(zIndex.IndexOf("|")))

                                        RenderViewsSelected = RenderViewsSelected & "&entry.1291206660=" & Uri.EscapeUriString(PlayerLayout)

                                    End If
                                Next
                                IndexCount += 1
                            Next

                            Dim BaseLayout As String = "color=s=" & OutputWidth & "x" & OutputHeight & ":c=black [base];"

                            'p.StartInfo.Arguments = VideoCenter & VideoLeft & VideoRight & "-filter_complex " & Chr(34) & "color=s=" & 1920 / SelectedQuality & "x" & 960 / SelectedQuality & ":c=black [base];[0:v]" & WatermarkTeslaCam & WatermarkDateTime & WatermarkTimecode & SentryModeTrigger & " setpts=PTS-STARTPTS, scale=" & 1280 / SelectedQuality & "x" & 960 / SelectedQuality & " [center];[1:v]" & Mirror & " setpts=PTS-STARTPTS, scale=" & 640 / SelectedQuality & "x" & 480 / SelectedQuality & " [topright];[2:v]" & Mirror & " setpts=PTS-STARTPTS, scale=" & 640 / SelectedQuality & "x" & 480 / SelectedQuality & " [bottomright];[base][center] overlay=shortest=1 [tmp1];[tmp1][topright] overlay=shortest=0:x=" & 1280 / SelectedQuality & " [tmp2];[tmp2][bottomright] overlay=shortest=0:x=" & 1280 / SelectedQuality & ":y=" & 480 / SelectedQuality & Chr(34) & " -y -preset veryfast -r " & framerate & " " & saveas
                            p.StartInfo.Arguments = InputFiles & StartTimeString & EndTimeString & " -filter_complex " & Chr(34) & BaseLayout & VideoSize & "[base]" & VideoStartLocation & Chr(34) & " -y -preset veryfast -r " & framerate & " " & saveas

                            SelectedNumberOfVideos += 1
                            My.Settings.FlipLR = FlipLREnable.Checked
                            My.Settings.MirrorLR = MirrorLREnable.Checked
                            My.Settings.MirorBack = MirrorRearEnable.Checked
                            'Languages.FFmpegStarting()

                            UpdateTextBox(" ")
                            UpdateTextBox("ffmpeg " & p.StartInfo.Arguments)
                            UpdateTextBox(" ")

                            Try
                                RenderFileCount += 1
                                Call p.Start()
                            Catch ex As Exception
                                Languages.FFmpegError()
                                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                            End Try
                            Try
                                p.BeginErrorReadLine()
                            Catch ex As Exception
                                If Debug_Mode = True Then
                                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                            End Try

                        End If


                    End If
                Next
            End If
            JoinFile.Close()
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Public Sub UpdateTextBox(text As String)
        Try
            If Debug_Mode = True Then
                If text.IsNormalized Then
                    FFmpegOutput.Text += Environment.NewLine & text.ToString
                End If
                FFmpegOutput.SelectionStart = FFmpegOutput.Text.Length
                FFmpegOutput.ScrollToCaret()
            End If
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Try
            Dim objWriter As New System.IO.StreamWriter(Path.GetTempPath() & "\TeslaCamViewerLogs\TCRenderLog-" & TodaysDate & ".txt", True)
            objWriter.WriteLine(DateTime.Now.ToString.Insert(DateTime.Now.ToString.LastIndexOf(" "), "." & DateTime.Now.Millisecond.ToString("000")) & " - " & text.ToString)
            objWriter.Close()
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        If text.Contains("Duration:") Then
            Try
                Dim playbackSpeed As Double = 1

                '"  Duration: 00:01:31.20, start: 0.000000, bitrate: 3029 kb/s"
                Dim MaxMin As String = text.Remove(text.IndexOf(","), text.Length - text.IndexOf(",")).Remove(0, text.IndexOf(":") + 5) '.Remove(3, text.Length - 3) '.Replace(":", "").Replace(".", "")
                MaxMin = MaxMin.Remove(MaxMin.IndexOf(":"), MaxMin.Length - MaxMin.IndexOf(":"))
                Dim MaxSec As String = text.Remove(text.IndexOf(","), text.Length - text.IndexOf(",")).Remove(0, text.IndexOf(":") + 8)
                MaxSec = MaxSec.Remove(MaxSec.IndexOf("."), MaxSec.Length - MaxSec.IndexOf("."))
                Dim MaxDuration As Integer = (Int(MaxMin) * 60) + Int(MaxSec) / playbackSpeed 'text.Remove(text.IndexOf(","), text.Length - text.IndexOf(",")).Remove(0, text.IndexOf(":")).Replace(":", "").Replace(".", "")
                DurationProgressBar.Style = ProgressBarStyle.Continuous
                If MaxDuration > DurationProgressBar.Maximum Then
                    Try
                        DurationProgressBar.Maximum = MaxDuration
                    Catch ex As Exception
                        DurationProgressBar.Maximum = 6000
                    End Try

                End If
            Catch ex As Exception
                If Debug_Mode = True Then
                    'MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try
        End If
        If text.Contains("time=0") Then
            Try
                Dim MaxMin As String = text.Remove(text.IndexOf(" bitrate="), text.Length - text.IndexOf(" bitrate=")).Remove(0, text.IndexOf("time=0") + 8) '.Remove(3, text.Length - 3) '.Replace(":", "").Replace(".", "")
                MaxMin = MaxMin.Remove(MaxMin.IndexOf(":"), MaxMin.Length - MaxMin.IndexOf(":"))
                Dim MaxSec As String = text.Remove(text.IndexOf(" bitrate="), text.Length - text.IndexOf(" bitrate=")).Remove(0, text.IndexOf("time=0") + 11)
                MaxSec = MaxSec.Remove(MaxSec.IndexOf("."), MaxSec.Length - MaxSec.IndexOf("."))

                Dim CurrentTime As Integer = (Int(MaxMin) * 60) + Int(MaxSec) 'text.Remove(text.IndexOf(" bitrate="), text.Length - text.IndexOf(" bitrate=")).Remove(0, text.IndexOf("time=0") + 6).Replace(":", "").Replace(".", "")
                DurationProgressBar.Style = ProgressBarStyle.Continuous
                DurationProgressBar.Value = CurrentTime

                Languages.RenderStatus()

            Catch ex As Exception

                If Debug_Mode = True Then
                    'MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                Try
                    DurationProgressBar.Maximum = 60
                Catch exx As Exception
                End Try
            End Try
        End If
    End Sub

    Public Sub proccess_OutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        If Not String.IsNullOrEmpty(e.Data) Then
            Dim Process As String = ""
            Dim SentBy As String = sender.StartInfo.Arguments.ToString
            Try
                If SentBy.Contains("join.txt") = True Then
                    Process = "Joining"
                ElseIf SentBy.Contains("-ss 00:") = True Then
                    Process = "Saving"
                ElseIf SentBy.Contains("drawtext=text='TeslaCam Viewer") = True Then
                    Process = "FFmpeg-" & SentBy.Remove(0, SentBy.IndexOf("TeslaCamViewer\RenderedFile")).Replace("TeslaCamViewer\RenderedFile", "").Replace(".mp4""", "")
                Else
                    Process = ""
                    Logging("Error - Unknown Process: " & SentBy)
                End If
            Catch ex As Exception
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try

            If Me.InvokeRequired = True Then
                Try
                    Me.Invoke(myDelegate, Process & " - " & e.Data)
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            Else
                Try
                    UpdateTextBox(Process & "-" & e.Data)
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            End If
        End If
    End Sub
    Dim RenderedVideoReady As Boolean = False
    Public RenderedVideoTotalCount As Integer = 0
    Private Sub proc_Exited(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles p.Exited
        If Me.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Klaar met het converteren van video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Conversión de vídeo terminada ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Video-Konvertierung abgeschlossen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Finished converting video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Klaar met het converteren van video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Conversión de vídeo terminada ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Video-Konvertierung abgeschlossen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Finished converting video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        End If

        DurationProgressBar.Value = DurationProgressBar.Maximum
        RenderedVideoReady = False
        RenderFileCount -= 1
        RenderFileCountNotDone -= 1
        RenderedVideoTotalCount += 1
        If RenderFileCount = 0 And RenderFileCountNotDone = 0 Then
            If Me.InvokeRequired = True Then
                If My.Settings.UserLanguage = "Dutch" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Alle bestanden zijn klaar met converteren ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Todos los archivos han terminado de convertir ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "German" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Alle Dateien wurden konvertiert ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                Else
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ All files are done converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                End If

            Else
                If My.Settings.UserLanguage = "Dutch" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Alle bestanden zijn klaar met converteren ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Todos los archivos han terminado de convertir ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "German" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Alle Dateien wurden konvertiert ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                Else
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ All files are done converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                End If

            End If
            DurationProgressBar.Style = ProgressBarStyle.Continuous
            If SelectedNumberOfVideos = 1 Or RenderOutputFile = Path.GetTempPath() & "TeslaCamViewer\RenderedFile.mp4" Then
                Try
                    RenderedVideoReady = False
                    'PlayerRender.URL = RenderOutputFile
                    'PlayerRender.Ctlcontrols.play()
                    VideoRendering.Visible = False
                    'PlayerRender.settings.setMode("loop", True)
                    Dim StopTime As Date = Now
                    Dim ts As TimeSpan = StopTime - RenderingStartTime

                    UpdateTextBox(vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Rendering " & RenderedVideoTotalCount & " file(s) at " & RenderQuality.Text & " Quality took " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                    UpdateTextBox("DONE")
                    'Tohere
                    SavedClips += 1
                    File.Copy(RenderOutputFile, SaveAsFileName, True)

                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            Else
                If Me.InvokeRequired = True Then

                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Rendering all clips together now ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)

                Else

                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Rendering all clips together now ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)


                End If
                Dim VideoALL As String = "-f concat -safe 0 -i " & Chr(34) & Path.GetTempPath().Replace("\", "/") & "TeslaCamViewer/join.txt" & Chr(34)
                VideoALL += " -c copy -y -preset veryfast " & Chr(34) & Path.GetTempPath() & "TeslaCamViewer\RenderedFile.mp4" & Chr(34)

                Dim p As New Process
                p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
                p.StartInfo.UseShellExecute = False
                p.StartInfo.CreateNoWindow = True
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                p.StartInfo.RedirectStandardError = True
                p.EnableRaisingEvents = True
                Application.DoEvents()

                AddHandler p.ErrorDataReceived, AddressOf proccess_OutputDataReceived
                AddHandler p.OutputDataReceived, AddressOf proccess_OutputDataReceived
                AddHandler p.Exited, AddressOf proc_Exited

                p.StartInfo.Arguments = VideoALL
                DurationProgressBar.Maximum = 1
                DurationProgressBar.Value = 0

                VideoRendering.Visible = True

                FFmpegOutput.Text += vbCrLf & "~~~~~~~~~~~~~~ Starting FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf & Environment.NewLine

                UpdateTextBox(" ")
                UpdateTextBox("ffmpeg " & p.StartInfo.Arguments)
                UpdateTextBox(" ")
                Try
                    Call p.Start()
                    RenderFileCount = 1
                    RenderFileCountNotDone = 1
                    RenderOutputFile = Path.GetTempPath() & "TeslaCamViewer\RenderedFile.mp4"
                Catch ex As Exception
                    If My.Settings.UserLanguage = "Dutch" Then
                        FFmpegOutput.Text += vbCrLf & "~~~~~~~~~~~~~~ FOUT bij het starten van FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf & Environment.NewLine
                    ElseIf My.Settings.UserLanguage = "Español" Then
                        FFmpegOutput.Text += vbCrLf & "~~~~~~~~~~~~~~ ERROR Iniciando FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf & Environment.NewLine
                    ElseIf My.Settings.UserLanguage = "German" Then
                        FFmpegOutput.Text += vbCrLf & "~~~~~~~~~~~~~~ FEHLER beim Starten von FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf & Environment.NewLine
                    Else
                        FFmpegOutput.Text += vbCrLf & "~~~~~~~~~~~~~~ ERROR Starting FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf & Environment.NewLine
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try

                Try
                    p.BeginErrorReadLine()
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            End If
        Else
            If Me.InvokeRequired = True Then
                If My.Settings.UserLanguage = "Dutch" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Wachten op " & RenderFileCount & " bestand (en) om het converteren te beëindigen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Esperando por " & RenderFileCount & " archivo(s) para terminar la conversión ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "German" Then
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Warten bis " & RenderFileCount & " Datei(en) zuende konvertiert sind ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                Else
                    Me.Invoke(myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Waiting for " & RenderFileCount & " file(s) to finish converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                End If

            Else
                If My.Settings.UserLanguage = "Dutch" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Wachten op " & RenderFileCount & " bestand (en) om het converteren te beëindigen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Esperando por " & RenderFileCount & " archivo(s) para terminar la conversión ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                ElseIf My.Settings.UserLanguage = "German" Then
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Warten bis " & RenderFileCount & " Datei(en) zuende konvertiert sind ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                Else
                    UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Waiting for " & RenderFileCount & " file(s) to finish converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                End If

            End If
        End If
        If RenderFileCount = 0 And RenderFileCountNotDone = 0 Then
            If My.Settings.UserLanguage = "Dutch" Then
                RenderFileProgress.Text = "GEDAAN"
                ThreadsRunningLabel.Text = "" '"GEDAAN"
            ElseIf My.Settings.UserLanguage = "Español" Then
                RenderFileProgress.Text = "Hecho"
                ThreadsRunningLabel.Text = "" '"Hecho"
            ElseIf My.Settings.UserLanguage = "German" Then
                RenderFileProgress.Text = "FERTIG"
                ThreadsRunningLabel.Text = "" '"FERTIG"
            Else
                RenderFileProgress.Text = "DONE"
                ThreadsRunningLabel.Text = "" '"DONE"
                GroupBoxExportSettings.Enabled = True
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                RenderFileProgress.Text = RenderFileCount & " van " & RenderFileCountNotDone
                ThreadsRunningLabel.Text = RenderFileCount & " Running"
            ElseIf My.Settings.UserLanguage = "Español" Then
                RenderFileProgress.Text = RenderFileCount & " de " & RenderFileCountNotDone
                ThreadsRunningLabel.Text = RenderFileCount & " Reproduciendo"
            ElseIf My.Settings.UserLanguage = "German" Then
                RenderFileProgress.Text = RenderFileCount & " von " & RenderFileCountNotDone
                ThreadsRunningLabel.Text = RenderFileCount & " Arbeite"
            Else
                RenderFileProgress.Text = RenderFileCount & " of " & RenderFileCountNotDone
                ThreadsRunningLabel.Text = RenderFileCount & " Running"
                GroupBoxExportSettings.Enabled = False
            End If
        End If
    End Sub

    Public Function IsFileInUse(sFile As String) As Boolean
        Try
            Using f As New IO.FileStream(sFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            End Using
        Catch Ex As Exception
            Return True
        End Try
        Return False
    End Function

    Public Function FileSize(Location As String)
        Try
            Dim myFile As New FileInfo(Location)
            Dim sizeInBytes As Long = myFile.Length
            Return sizeInBytes
        Catch ex As Exception
            Return 0
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Function
    Private Sub SettingsBTN_Click(sender As Object, e As EventArgs) Handles SettingsBTN.Click

        Logging("Info - Settings Button Pressed")
        Try
            GroupBoxExportSettings.Location = GroupBoxControlsWindow.Location + New Point(0, GroupBoxControlsWindow.Height - GroupBoxExportSettings.Height)
            GroupBoxExportSettings.BringToFront()
            GroupBoxExportSettings.Visible = True
            PlayersPAUSE()

        Catch ex As Exception
            SettingsBTN.Enabled = False
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub CloseGroupboxSettings_Click(sender As Object, e As EventArgs) Handles CloseGroupboxSettings.Click
        GroupBoxExportSettings.Visible = False
    End Sub

    Private Sub RenderInTimeLabel_MouseClick(sender As Object, e As MouseEventArgs) Handles RenderInTimeLabel.MouseClick
        Try
            If e.Button = MouseButtons.Left Then
                If EventTimeCodeBar.Value < RenderOutTime Then
                    RenderInTime = EventTimeCodeBar.Value
                    Logging("Info - Render In Time Changed " & RenderInTime)
                End If

                'If RenderTimeline.Value >= RenderOutTime Then
                '    RenderTimeline.Value = RenderInTime
                '    PlayerRender.Ctlcontrols.currentPosition = RenderTimeline.Value / 10
                'End If
                'If RenderTimeline.Value < RenderInTime Then
                '    RenderTimeline.Value = RenderInTime
                '    PlayerRender.Ctlcontrols.currentPosition = RenderTimeline.Value / 10
                'End If
                'RenderInTimeGraphic.Left = (((GroupBoxRENDER.Width - 29 - 22) / RenderTimeline.Maximum) * RenderTimeline.Value) + 19
            ElseIf e.Button = MouseButtons.Right Then
                RenderInTime = EventTimeCodeBar.Minimum
                Logging("Info - Render In Time Changed " & RenderInTime)
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub RenderOutTimeLabel_MouseClick(sender As Object, e As MouseEventArgs) Handles RenderOutTimeLabel.MouseClick
        Try
            If e.Button = MouseButtons.Left Then
                If EventTimeCodeBar.Value > RenderInTime Then
                    RenderOutTime = EventTimeCodeBar.Value
                    Logging("Info - Render Out Time Changed " & RenderOutTime)
                End If
                'If RenderTimeline.Value > RenderOutTime Then
                '    RenderTimeline.Value = RenderOutTime
                '    '    PlayerRender.Ctlcontrols.currentPosition = RenderTimeline.Value / 10
                'End If
                'If RenderTimeline.Value < RenderInTime Then
                '    RenderTimeline.Value = RenderInTime
                '    PlayerRender.Ctlcontrols.currentPosition = RenderTimeline.Value / 10
                'End If
                'RenderOutTimeGraphic.Left = (((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum) * RenderOutTime) + 10

            ElseIf e.Button = MouseButtons.Right Then
                RenderOutTime = EventTimeCodeBar.Maximum
                Logging("Info - Render Out Time Changed " & RenderOutTime)

            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub RenderInTimeGraphic_Click(sender As Object, e As EventArgs) Handles RenderInTimeGraphic.Click
        Try
            If MoveRenderIn = True Then
                MoveRenderIn = False
                Logging("Info - Render In Time Changed " & RenderInTime)
            Else
                MoveRenderIn = True
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub RenderOutTimeGraphic_Click(sender As Object, e As EventArgs) Handles RenderOutTimeGraphic.Click
        Try
            If MoveRenderOut = True Then
                MoveRenderOut = False
                Logging("Info - Render Out Time Changed " & RenderOutTime)
            Else
                MoveRenderOut = True
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub RenderTimeline_MouseMove(sender As Object, e As MouseEventArgs)
        'Try
        '    If MoveRenderOut = True And e.X < GroupBoxRENDER.Width - 31 And e.X > 14 And e.X > RenderInTimeGraphic.Left + 4 Then
        '        RenderOutTimeGraphic.Left = e.X + 5
        '        RenderOutTime = ((e.X - 15) / ((GroupBoxRENDER.Width - 29 - 22) / RenderTimeline.Maximum))
        '    End If
        '    If MoveRenderIn = True And e.X < GroupBoxRENDER.Width - 31 And e.X > 14 And e.X < RenderOutTimeGraphic.Left - 15 Then
        '        RenderInTimeGraphic.Left = e.X + 5
        '        RenderInTime = ((e.X - 15) / ((GroupBoxRENDER.Width - 29 - 22) / RenderTimeline.Maximum))
        '    End If
        'Catch ex As Exception
        '    If Debug_Mode = True Then
        '        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        '    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        'End Try
    End Sub

    Private Sub RenderTimeline_MouseClick(sender As Object, e As MouseEventArgs)
        MoveRenderIn = False
        MoveRenderOut = False
    End Sub

    Private Sub MainForm_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Try
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            For Each path In files
                If path.Contains(".") Then
                    path = path.Remove(path.LastIndexOf("\"))
                    AddCustomFolderRootNode(path)
                    Exit For

                End If
                AddCustomFolderRootNode(path)
                Logging("Info - DragDrop added path " & path)
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub Tv_Explorer_DragEnter(sender As Object, e As DragEventArgs) Handles Tv_Explorer.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effect = DragDropEffects.Copy
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub Tv_Explorer_DragDrop(sender As Object, e As DragEventArgs) Handles Tv_Explorer.DragDrop
        Try
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            For Each path In files
                If path.Contains(".") Then
                    path = path.Remove(path.LastIndexOf("\"))
                    AddCustomFolderRootNode(path)
                    Exit For

                End If
                AddCustomFolderRootNode(path)
                Logging("Info - DragDrop added path " & path)
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Try

            Process.Start(Tv_Explorer.SelectedNode.Tag)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Languages.DeleteFiles()

    End Sub
    Dim CopyURL = ""
    Dim CopyFolder = ""
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Try
            CopyURL = Tv_Explorer.SelectedNode.Tag
            CopyFolder = Tv_Explorer.SelectedNode.ImageKey.ToString
            PasteToolStripMenuItem.Enabled = True  'paste label
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Try
            Dim f() As String = {CopyURL}
            Dim d As New DataObject(DataFormats.FileDrop, f)
            Clipboard.SetDataObject(d, True)
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If CopyFolder <> "Folder" Then
            Try
                Dim NewLocation = Tv_Explorer.SelectedNode.Tag & CopyURL.Remove(0, CopyURL.LastIndexOf("\"))
                My.Computer.FileSystem.CopyFile(CopyURL, NewLocation)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Debug_Mode = True Then
                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try
        Else
            Try
                Dim NewLocation = Tv_Explorer.SelectedNode.Tag & CopyURL.Remove(0, CopyURL.LastIndexOf("\"))
                My.Computer.FileSystem.CopyDirectory(CopyURL, NewLocation, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Debug_Mode = True Then
                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try
        End If

    End Sub

    Private Sub Tv_Explorer_MouseDown(sender As Object, e As MouseEventArgs) Handles Tv_Explorer.MouseDown
        If e.Button = MouseButtons.Right Then
            ExplorerRightClick = True
        Else
            ExplorerRightClick = False
        End If

    End Sub

    Private Sub RenderTimeline_Scroll(sender As Object, e As EventArgs)
        'Try
        '    'PlayerRender.CreateControl()
        '    If PlayerRender.playState = WMPLib.WMPPlayState.wmppsPaused And RenderTimeline.Value >= (RenderVideoLastPos - 10) * 10 And RenderTimeline.Value <= (RenderVideoLastPos + 10) * 10 Then
        '        PlayerRender.Ctlcontrols.currentPosition = RenderTimeline.Value / 10
        '        RenderVideoLastPos = PlayerRender.Ctlcontrols.currentPosition
        '        PlayerRender.Ctlcontrols.play()
        '        PlayerRender.Ctlcontrols.pause()
        '    End If
        'Catch ex As Exception
        '    If Debug_Mode = True Then
        '        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        '    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        'End Try
    End Sub

    Private Sub AppSettingsButton_MouseDown(sender As Object, e As MouseEventArgs) Handles AppSettingsButton.MouseDown

        Try
            SettingsMenuStrip.Show()
            SettingsMenuStrip.Left = MousePosition.X
            SettingsMenuStrip.Top = MousePosition.Y
            Logging("Info - App Settings Opened")
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub LanguageSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LanguageSelection.SelectedIndexChanged
        If LanguageSelection.Visible = True Then
            Logging("Info - Language changed from " & My.Settings.UserLanguage & " To " & LanguageSelection.Text)
            My.Settings.UserLanguage = LanguageSelection.Text
            My.Settings.Save()
            SettingsMenuStrip.Close()

            Languages.ChangeLanguage()
            If My.Settings.UserLanguage = "English" Then

                MessageBox.Show("Please close TeslaCam Viewer for settings to take effect")
                '    MessageBox.Show("Cierre TeslaCam Viewer para que la configuración tenga efecto.")
                'ElseIf My.Settings.Language = "German" Then
                '    MessageBox.Show("Bitte schließen Sie TeslaCam Viewer, damit die Einstellungen wirksam werden")
                'Else
            End If

        End If
    End Sub

    Private Sub LanguageSelection_Click(sender As Object, e As EventArgs) Handles LanguageSelection.Click

    End Sub

    Private Sub DownloadUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadUpdateToolStripMenuItem.Click
        Process.Start("https://github.com/NateMccomb/TeslaCamViewerII/#install")
        LinksSelected = LinksSelected & "&entry.839831962=Update"
        Logging("Info - Update Pressed")
    End Sub

    Private Sub ViewStatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewStatsToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(CPUID)
        Process.Start("https://docs.google.com/spreadsheets/d/1wLD5EuHFSwICEiqYcjo0fEnDYkKWszYaI2ZyW2qMi-U/preview#gid=575071230")
        LinksSelected = LinksSelected & "&entry.839831962=Stats"
        Logging("Info - View Stats Pressed")
    End Sub

    Private Sub YourIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YourIDToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(CPUID)
        Process.Start("https://docs.google.com/spreadsheets/d/1wLD5EuHFSwICEiqYcjo0fEnDYkKWszYaI2ZyW2qMi-U/preview#gid=575071230")
        LinksSelected = LinksSelected & "&entry.839831962=Stats"
        Logging("Info - View Stats Pressed")
    End Sub

    Private Sub CopyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem1.Click
        My.Computer.Clipboard.SetText(CPUID)
        Process.Start("https://docs.google.com/spreadsheets/d/1wLD5EuHFSwICEiqYcjo0fEnDYkKWszYaI2ZyW2qMi-U/preview#gid=575071230")
        LinksSelected = LinksSelected & "&entry.839831962=Stats"
        Logging("Info - View Stats Pressed")
    End Sub

    Private Sub GitHubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GitHubToolStripMenuItem.Click
        Process.Start("https://github.com/NateMccomb/TeslaCamViewerII")
        LinksSelected = LinksSelected & "&entry.839831962=GitHub"
        Logging("Info - GitHub Pressed")
    End Sub

    Private Sub ReportABugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportABugToolStripMenuItem.Click
        Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSdJ0CJAGLvgEIewbt9OHaU_JiJTP_AmIiecpvHEiHlWe4ZHcQ/viewform?usp=pp_url&entry.2105873733=" & Uri.EscapeUriString(CurrentVersion) & "&entry.1334079027=" & Uri.EscapeUriString(CPUID))
        LinksSelected = LinksSelected & "&entry.839831962=ReportBug"
        Logging("Info - Bug Report Pressed")
    End Sub

    Private Sub FeedBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FeedBackToolStripMenuItem.Click
        Process.Start("https://docs.google.com/forms/d/e/1FAIpQLScl0Eg_RRAbHcJf2tBZ42SrP5RkOWo1xrL4O763WsmPCgqXWA/viewform")
        LinksSelected = LinksSelected & "&entry.839831962=Feedback"
        Logging("Info - Feedback Pressed")
    End Sub

    Private Sub TwitterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TwitterToolStripMenuItem.Click
        Process.Start("https://twitter.com/TeslaCamViewer")
        LinksSelected = LinksSelected & "&entry.839831962=Twitter"
        Logging("Info - Twitter Pressed")
    End Sub

    Private Sub InstagramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InstagramToolStripMenuItem.Click
        Process.Start("https://www.instagram.com/nate.mccomb/")
        LinksSelected = LinksSelected & "&entry.839831962=Instagram"
        Logging("Info - Instagram Pressed")
    End Sub

    Private Sub DonateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DonateToolStripMenuItem.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=8UKFUQCU9476N&source=url")
        LinksSelected = LinksSelected & "&entry.839831962=Donate"
        Logging("Info - Donate Pressed")
    End Sub

    Private Sub PCIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PCIDToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(CPUID)
    End Sub
    Public Function SearchEventList(ByVal EventDateTime As String)
        Try
            If File.Exists(Path.GetTempPath() & "TCViewedEvents.txt") Then
                Dim text As String = File.ReadAllText(Path.GetTempPath() & "TCViewedEvents.txt")
                Dim index As Integer = text.IndexOf(EventDateTime)
                If index >= 0 Then
                    If Debug_Mode = True Then
                        MainPlayerMinTimecode.BackColor = Color.Lime
                    End If
                    Return True
                    Exit Function
                End If
            End If
            AddEvent(EventDateTime)
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        Return False
    End Function
    Public Sub AddEvent(ByVal EventDateTime As String)
        Try
            Dim objWriter As New System.IO.StreamWriter(Path.GetTempPath() & "TCViewedEvents.txt", True)
            objWriter.WriteLine(EventDateTime)
            objWriter.Close()
            If Debug_Mode = True Then
                MainPlayerMinTimecode.BackColor = Color.BlueViolet
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MainPlayerMinTimecode.BackColor = Color.Yellow
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Public Sub UpdatePCID()
        If My.Settings.CustomID = "" Then
            CPUID = GetCpuId() & SystemSerialNumber()
        Else
            CPUID = My.Settings.CustomID
        End If
        Logging("Info - CPUID = " & CPUID)
        YourIDToolStripMenuItem.Text = "Your ID: " & CPUID
        PCIDToolStripMenuItem.Text = "PC ID: " & CPUID
    End Sub
    Private Sub CustomIDToolStripTextBox_Click(sender As Object, e As EventArgs) Handles CustomIDToolStripTextBox.Click
        If CustomIDToolStripTextBox.Text = "Choose Custom ID" Or CustomIDToolStripTextBox.Text = "" Then
            CustomIDToolStripTextBox.Text = ""
        End If
    End Sub

    Private Sub CustomIDToolStripTextBox_LostFocus(sender As Object, e As EventArgs) Handles CustomIDToolStripTextBox.LostFocus
        If CustomIDToolStripTextBox.Text = "Choose Custom ID" Or CustomIDToolStripTextBox.Text = "" Or My.Settings.CustomID = CustomIDToolStripTextBox.Text Then
            CustomIDToolStripTextBox.Text = "Choose Custom ID"
        End If
    End Sub

    Private Sub CustomIDToolStripTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CustomIDToolStripTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim NewID As String = "-" & CustomIDToolStripTextBox.Text & "-"
            If CPUID <> NewID Then
                Dim TempCPUID As String = GetCpuId() & SystemSerialNumber()
                If CPUID = TempCPUID And My.Settings.CustomID = "" And CustomIDToolStripTextBox.Text = "" Then
                    Exit Sub
                End If
                If NewID = "Choose Custom ID" Or My.Settings.CustomID = NewID Then
                    Exit Sub
                End If
                If NewID = "" Or NewID = "Choose Custom ID" Or NewID = "--" Then
                    NewID = TempCPUID
                End If

                Dim result As Integer = MessageBox.Show("Are you sure you want to change your PC ID?" & vbCrLf & vbCrLf & "From this point forward you'll need to search for PC ID: " & NewID & vbCrLf & "Previous entries will still remain as PC ID: " & CPUID, "Change PC ID?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If result = DialogResult.Yes Then
                    If NewID = TempCPUID Then
                        My.Settings.CustomID = ""
                    Else
                        My.Settings.CustomID = NewID
                    End If
                    Logging("Info - Changed PCID from " & CPUID & " to " & NewID)

                    UpdatePCID()
                    SettingsMenuStrip.Close()

                Else
                    CustomIDToolStripTextBox.Text = ""
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Public Sub Logging(ByVal Text As String)
        Try
            Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamViewerLogs")
            If Not dir.Exists Then
                FileSystem.MkDir(Path.GetTempPath() & "TeslaCamViewerLogs")
            End If
            Dim objWriter As New System.IO.StreamWriter(Path.GetTempPath() & "TeslaCamViewerLogs\TCViewerLog-" & TodaysDate & ".txt", True)
            objWriter.WriteLine(DateTime.Now.ToString.Insert(DateTime.Now.ToString.LastIndexOf(" "), "." & DateTime.Now.Millisecond.ToString("000")) & " - " & Text.ToString)
            objWriter.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Function GetDrives()
        Dim ReturnString As String = ""
        Dim drives As String() = System.IO.Directory.GetLogicalDrives()
        For Each drive As String In drives
            ReturnString = ReturnString & drive & " "
        Next
        Return ReturnString
    End Function
    Dim ActiveSeconds As Integer = 0
    Dim DiskSaveTimeout As Integer = 100
    Dim EnterDebugMode As Integer = 0
    Private Sub OneSec_Tick(sender As Object, e As EventArgs) Handles OneSec.Tick
        If My.Computer.Keyboard.ShiftKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.AltKeyDown = True Then
            EnterDebugMode += 1
            If EnterDebugMode = 5 Then
                If Debug_Mode = False Then
                    Me.Text = "TeslaCam Viewer II " & CurrentVersion & "   ID:" & CPUID & "                                                ------------     DEBUG MODE     ------------"
                    Debug_Mode = True
                    Logging("Info - DEBUG MODE - ENABLED")
                    MainPlayerMaxTimecode.Visible = True
                    MainPlayerMinTimecode.Visible = True
                    MainPlayerTimecode.Visible = True
                    SavedLayouts.Visible = True
                    AnalyzingFilesLabel.Visible = True
                    FFmpegOutput.Visible = True
                    AspectRatio.Visible = True
                Else
                    Me.Text = "TeslaCam Viewer II " & CurrentVersion
                    Debug_Mode = False
                    Logging("Info - DEBUG MODE - DISABLED")
                    MainPlayerMaxTimecode.Visible = False
                    MainPlayerMinTimecode.Visible = False
                    MainPlayerTimecode.Visible = False
                    SavedLayouts.Visible = False
                    AnalyzingFilesLabel.Visible = False
                    FFmpegOutput.Visible = False
                    AspectRatio.Visible = False
                End If
            End If
        Else
            EnterDebugMode = 0

        End If

        If DiskSaveTimeout < 1 Then
            SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Blue_5050
            DiskSaveTimeout += 1
        ElseIf DiskSaveTimeout < 3 Then
            SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Green_5050
            DiskSaveTimeout += 1
        ElseIf DiskSaveTimeout = 3 Then
            SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Normal_5050
            DiskSaveTimeout += 1
        End If

        Try
            Dim NewDriveList As String = GetDrives()
            If CurrentDriveList <> NewDriveList Then 'And NewDriveList.Length > CurrentDriveList.Length Then
                CurrentDriveList = NewDriveList
                Logging("Info - Drives Updated - " & CurrentDriveList)
                'NewDriveFound()
                RefreshRootNodes()
            End If
            ActiveSeconds += 1
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

        Try
            Dim MaxPlayerSize As New Point(0, 0)
            Dim MinPlayerSize As New Point(Panel.Width, Panel.Height)
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                        Dim PlayerEnabledCheckBox As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        If PlayerEnabledCheckBox.Checked = True Then
                            If Player.Visible = False And LayoutChanged = False Then
                                SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                            End If
                            Player.Visible = True
                            Status.Visible = True
                        Else
                            If Player.Visible = True And LayoutChanged = False Then
                                SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                            End If
                            Player.Visible = False
                            Status.Visible = False
                        End If

                        If Player.Visible = True Then
                            If Player.Height > MaxPlayerSize.Y Then
                                MaxPlayerSize.Y = Player.Height
                                MaxPlayerSize.X = Player.Width
                            End If
                            If Player.Height < MinPlayerSize.Y Then
                                MinPlayerSize.Y = Player.Height
                                MinPlayerSize.X = Player.Width
                            End If
                            If Player.Size = Panel.Size Then
                                MaxPlayerSize.Y = Player.Height
                                MaxPlayerSize.X = Player.Width
                                MinPlayerSize.Y = Player.Height
                                MinPlayerSize.X = Player.Width
                                Exit For
                            End If
                        End If
                        If ResizePlayer = "" And MovePlayer = "" Then
                            Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                            Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                            Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                            PlayerTop.Visible = False
                            PlayerLeft.Visible = False
                            PlayerSize.Visible = False
                        End If

                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                        Dim PlayerEnabledCheckBox As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        If PlayerEnabledCheckBox.Checked = True Then
                            If Player.Visible = False And LayoutChanged = False Then
                                SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                            End If
                            Player.Visible = True
                            Status.Visible = True
                        Else
                            If Player.Visible = True And LayoutChanged = False Then
                                SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Orange_5050
                            End If
                            Player.Visible = False
                            Status.Visible = False
                        End If

                        If Player.Visible = True Then
                            If Player.Height > MaxPlayerSize.Y Then
                                MaxPlayerSize.Y = Player.Height
                                MaxPlayerSize.X = Player.Width
                            End If
                            If Player.Height < MinPlayerSize.Y Then
                                MinPlayerSize.Y = Player.Height
                                MinPlayerSize.X = Player.Width
                            End If
                            If Player.Size = Panel.Size Then
                                MaxPlayerSize.Y = Player.Height
                                MaxPlayerSize.X = Player.Width
                                MinPlayerSize.Y = Player.Height
                                MinPlayerSize.X = Player.Width
                                Exit For
                            End If
                        End If
                        If ResizePlayer = "" And MovePlayer = "" Then
                            Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                            Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                            Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                            PlayerTop.Visible = False
                            PlayerLeft.Visible = False
                            PlayerSize.Visible = False
                        End If

                    End If
                Next
            End If

            Dim SelectedQuality = 0
            Select Case RenderQuality.SelectedIndex
                Case 0
                    SelectedQuality = 1
                Case 1
                    SelectedQuality = 2
                Case 2
                    SelectedQuality = 4
                Case 3
                    SelectedQuality = 8
            End Select

            Try


                Dim x As Integer
                'Int32.TryParse(AspectRatio.Text.Remove(AspectRatio.Text.ToLower.IndexOf("x")), x)
                Dim y As Integer
                'Int32.TryParse(AspectRatio.Text.Remove(0, AspectRatio.Text.ToLower.IndexOf("x") + 1), y)
                Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(AspectRatio.Text))
                    MyReader.TextFieldType = FileIO.FieldType.Delimited
                    MyReader.SetDelimiters(":")

                    Dim currentRow As String() = {"4", "3"}
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadFields()
                        Catch ex As Exception
                        End Try
                    End While
                    If currentRow.Count = 2 Then
                        x = Int(currentRow(0))
                        y = Int(currentRow(1))
                    End If
                End Using
                If y <= x Then
                    OutputWidth = ((((Panel.Height / MinPlayerSize.Y) * TeslaCamResolution.Y) / y) * x) / SelectedQuality
                    OutputHeight = ((Panel.Height / MinPlayerSize.Y) * TeslaCamResolution.Y) / SelectedQuality
                Else
                    OutputWidth = ((Panel.Width / MinPlayerSize.X) * TeslaCamResolution.X) / SelectedQuality
                    OutputHeight = ((((Panel.Width / MinPlayerSize.X) * TeslaCamResolution.X) / x) * y) / SelectedQuality
                End If

                ResolutionLabel.Text = OutputWidth & "x" & OutputHeight
            Catch ex As Exception
                If Debug_Mode = True Then
                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try
            If CameraCount > 4 Then
                PlayersEnabledPanel.AutoScroll = True
                PlayersEnabledPanel.HorizontalScroll.Visible = False
                PlayersEnabledPanel.VerticalScroll.Visible = True
                PlayersEnabledPanel.VerticalScroll.Enabled = True

            Else
                PlayersEnabledPanel.AutoScroll = False
                ' PlayersEnabledPanel.VerticalScroll.Visible = False
            End If


        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
        LayoutChanged = False
    End Sub
    Dim LayoutChanged As Boolean = True
    Dim OutputHeight As Integer = 0
    Dim OutputWidth As Integer = 0
    Dim TeslaCamResolution As New Point(1280, 960)
    Private Sub MainForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Try
            OneSec.Enabled = False
            If FormIsClosing = False Then
                PlayersPAUSE()

            End If
            Logging("Info - - Viewer Loss of Focus - -")
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub MainForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        OneSec.Enabled = True
        Logging("Info - - Viewer now in use - -")
    End Sub
    Dim FixedTeslaCamFileCount As Integer = 0
    Dim FixedTeslaCamFileNotDone As Integer = 0
    Dim FixedTeslaCamFolder As String = ""
    Public Sub FixTeslaCamFiles(ByVal SourcePath As String, Optional ByVal SubFolders As String = "\", Optional CustomSaveLocation As String = "", Optional ClipCount As Integer = 0)

        Dim SourceDir As DirectoryInfo = New DirectoryInfo(SourcePath)
        Dim pathIndex As Integer
        pathIndex = SourcePath.LastIndexOf("\")
        Dim DestinationFolder As String
        If CustomSaveLocation = "" Then
            DestinationFolder = Path.GetTempPath() & "TeslaCamFix" & SubFolders
        Else
            DestinationFolder = CustomSaveLocation & SubFolders
        End If
        ' the source directory must exist, otherwise throw an exception
        Dim dir As New IO.DirectoryInfo(DestinationFolder & SourceDir.Name)
        If Not dir.Exists Then
            FileSystem.MkDir(DestinationFolder & SourceDir.Name)
        End If
        FixedTeslaCamFolder = DestinationFolder & SourceDir.Name

        If SourceDir.Exists Then
            Try
                Dim SubDir As DirectoryInfo
                For Each SubDir In SourceDir.GetDirectories()
                    Console.WriteLine(SubDir.Name)
                    Logging("FixFolder - " & SubFolders & SourceDir.Name)
                    FixTeslaCamFiles(SubDir.FullName, SubFolders & SourceDir.Name & "\", CustomSaveLocation, ClipCount)
                Next
            Catch ex As Exception
                If Debug_Mode = True Then
                    MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try

            Dim di As New IO.DirectoryInfo(SourceDir.ToString) '.Remove(Tv_Explorer.SelectedNode.Tag.ToString.LastIndexOf("\")))
            'Dim di As New IO.DirectoryInfo(FullCenterCameraName.Remove(FullCenterCameraName.LastIndexOf("\")))
            Dim fis = di.GetFiles().OrderByDescending(Function(p) p.Name).ToArray()
            Dim filecount As Integer = 0
            If fis.Length >= 0 Then
                FixedFolders += 1
                Dim EventCount As Integer = 0
                Dim LastEvent As String = ""
                For Each item In fis
                    If item.Name.StartsWith("20") = True And item.Name.Contains("_") = True And item.Name.Contains("-") = True And item.Name.ToLower.EndsWith(".mp4") Then
                        filecount += 1
                        If item.Name.Remove(item.Name.LastIndexOf("-")) <> LastEvent Then
                            LastEvent = item.Name.Remove(item.Name.LastIndexOf("-"))
                            EventCount += 1
                            If ClipCount < EventCount And ClipCount <> 0 Then
                                Exit For
                            End If
                        End If
                        FixedTeslaCamFileCount += 1
                        FixedTeslaCamFileNotDone += 1
                        'Console.WriteLine(item.FullName)
                        FixTeslaCamQueue.Add(item.FullName & "|" & dir.FullName & "\" & item.Name)

                        'p.WaitForExit()

                        If SubFolders <> "\" And filecount >= 12 Then
                            'Exit For
                        End If
                    End If
                Next
            End If

        Else
            Throw New DirectoryNotFoundException("Source directory does not exist: " + SourceDir.FullName)
        End If

    End Sub

    Dim FixTeslaCamQueue As New List(Of String)


    Public Sub StartFixTeslaCamFiles()
        If FixTeslaCamQueue.Count > 0 Then
            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(FixTeslaCamQueue.Item(0)))
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters("|")

                Dim Found As Boolean = False
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                    Catch ex As Exception

                    End Try
                End While
                If currentRow.Count = 2 Then
                    Dim InputFile As String = currentRow(0)
                    Dim OutputFile As String = currentRow(1)

                    Dim p As New Process
                    p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.RedirectStandardError = True
                    p.EnableRaisingEvents = True

                    'here
                    p.StartInfo.Arguments = "-i " & Chr(34) & InputFile & Chr(34) & " -c:v copy -y -preset veryfast " & Chr(34) & OutputFile & Chr(34)
                    AddHandler p.ErrorDataReceived, AddressOf FixTeslaCam_proccess_OutputDataReceived
                    AddHandler p.OutputDataReceived, AddressOf FixTeslaCam_proccess_OutputDataReceived
                    AddHandler p.Exited, AddressOf proc_FixTesla_Exited

                    Logging("FixFile - " & Chr(34) & OutputFile & Chr(34))
                    p.Start()

                    Try
                        p.BeginErrorReadLine()
                    Catch ex As Exception
                        If Debug_Mode = True Then
                            MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                    End Try

                End If

            End Using
            FixTeslaCamQueue.RemoveAt(0)

        End If

    End Sub


    Private Sub proc_FixTesla_Exited(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles p.Exited
        FixedTeslaCamFileNotDone -= 1

        Try
            Dim SentBy As String = sender.StartInfo.Arguments.ToString
            Dim Text As String = "Re-Encoded-" & SentBy.Remove(0, SentBy.LastIndexOf("\")).Replace(".mp4", "") '& " DONE"
            If Me.InvokeRequired = True Then
                Try
                    Me.Invoke(FixTeslaCammyDelegate, Text)
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            Else
                Try
                    FixTeslaCamUpdateTextBox(Text)
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try

            End If
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Sub
    Private Sub AddFixedTeslaCamFolderRootNode(folderpath As String)
        If Directory.Exists(folderpath) Then
            Logging("Info - Fixed TeslaCam Folder Added - " & folderpath)
            Dim FolderName As String = "Fixed - " & New DirectoryInfo(folderpath).Name
            AddImageToImgList(folderpath)
            Dim rootNode As New TreeNode(FolderName)
            With rootNode
                .Tag = folderpath
                .ImageKey = "Folder" 'folderpath
                .SelectedImageKey = folderpath

                If Directory.GetDirectories(folderpath).Count > 0 OrElse Directory.GetFiles(folderpath).Count > 0 Then
                    .Nodes.Add("Empty")
                End If
            End With
            Tv_Explorer.Nodes.Add(rootNode) 'add this root node to the treeview
        End If
    End Sub
    Dim FixedFolders As Integer = 0
    Dim FixTeslaCamLoggingQueue As String = ""

    Public Sub FixTeslaCam_proccess_OutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        If Not String.IsNullOrEmpty(e.Data) And Debug_Mode = True Then
            Try
                Dim SentBy As String = sender.StartInfo.Arguments.ToString
                Dim Text As String = "Fixed-FFmpegOutput-" & SentBy.Remove(0, SentBy.LastIndexOf("\")).Replace(".mp4", "") & " " & e.Data
                If Me.InvokeRequired = True Then
                    Try
                        Me.Invoke(FixTeslaCammyDelegate, Text)
                    Catch ex As Exception
                        If Debug_Mode = True Then
                            MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                    End Try
                Else
                    Try
                        FixTeslaCamUpdateTextBox(Text)
                    Catch ex As Exception
                        If Debug_Mode = True Then
                            MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                    End Try
                End If
            Catch ex As Exception
                Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
            End Try
        End If

    End Sub
    Public Sub FixTeslaCamUpdateTextBox(text As String)
        If text.StartsWith("Fixed-FFmpegOutput-") = False Then
            If text.IsNormalized Then
                FixTeslaCamFFmpegOutput.Text += text.ToString & Environment.NewLine
            End If
            FixTeslaCamFFmpegOutput.SelectionStart = FixTeslaCamFFmpegOutput.Text.Length
            FixTeslaCamFFmpegOutput.ScrollToCaret()
        End If

        Try
            Dim objWriter As New System.IO.StreamWriter(Path.GetTempPath() & "\TeslaCamViewerLogs\TCRenderLog-" & TodaysDate & ".txt", True)
            objWriter.WriteLine(DateTime.Now.ToString.Insert(DateTime.Now.ToString.LastIndexOf(" "), "." & DateTime.Now.Millisecond.ToString("000")) & " - " & text.ToString)
            objWriter.Close()
        Catch ex As Exception
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try

    End Sub

    Private Sub Tv_Explorer_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles Tv_Explorer.BeforeSelect
        If e.Node.ImageKey.ToString() = "Folder" Then
            ReEncodeFilesMenuItem.Visible = True
        Else
            ReEncodeFilesMenuItem.Visible = False
        End If
    End Sub

    Private Sub FixTeslaCamBtnDone_Click(sender As Object, e As EventArgs) Handles FixTeslaCamBtnDone.Click
        Try
            If FixTeslaCamBtnDone.Text = "Cancel" Then
                Try
                    Dim a As Integer = 0
                    For Each PRunning As Process In System.Diagnostics.Process.GetProcessesByName("ffmpeg")
                        PRunning.Kill()
                        a += 1
                    Next
                    Logging("Info - Fix TeslaCam Canceled: killed # of processes" & a)
                    'MessageBox.Show("Killed " & a & " Processes ", "Kill Process", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    If Debug_Mode = True Then
                        MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
                FixedTeslaCamFolder = ""
                Tv_Explorer.Enabled = True
                RefreshRootNodes()
                FixTeslaCamGroupBox.Visible = False
                Tv_Explorer.Focus()
                Logging("Info - TeslaCam Fix Canceled")
            Else
                Dim FolderName As String = "Fixed - " & New DirectoryInfo(FixedTeslaCamFolder).Name
                FixedTeslaCamFolder = ""
                Tv_Explorer.Enabled = True
                RefreshRootNodes()
                FixTeslaCamGroupBox.Visible = False
                Tv_Explorer.Focus()
                Logging("Info - TeslaCam Fix Done")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ExplorerMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles ExplorerMenuStrip.Opening
        If CustomFolderGroupBox.Visible = True Then
            'ExplorerMenuStrip.Close()
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshRootNodes()
    End Sub

    Private Sub Disable244BugDetectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Disable244BugDetectToolStripMenuItem.Click
        If Disable244BugDetectToolStripMenuItem.Checked = False Then
            Disable244BugDetectToolStripMenuItem.Checked = True
        Else
            Disable244BugDetectToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel.MouseDown
        ResizePlayer = ""
        MovePlayer = ""
    End Sub

    Private Sub Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel.MouseUp
        ResizePlayer = ""
        MovePlayer = ""
    End Sub

    Private Sub SaveLayoutBtn_Click(sender As Object, e As EventArgs) Handles SaveLayoutBtn.Click
        Try
            Logging("Info - Updating Selected Layout: " & AspectName.Text & " " & AspectRatio.Text)
            SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Blue_5050
            If Not My.Settings.AspectRatioList.Contains(AspectRatio.Text) Then
                'Dim Text As String = AspectRatio.Text
            End If

            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)

                        Dim PlayerEnabled As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Dim zIndex As Integer = Panel.Controls.GetChildIndex(Player)

                        Dim ItemFound As Integer = -1
                        Dim CurrentItem As Integer = 0
                        For Each Item As String In My.Settings.UserSavedCameraLayouts
                            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                                MyReader.TextFieldType = FileIO.FieldType.Delimited
                                MyReader.SetDelimiters("|")

                                Dim Found As Boolean = False
                                Dim currentRow As String()
                                While Not MyReader.EndOfData
                                    Try
                                        currentRow = MyReader.ReadFields()
                                    Catch ex As Exception

                                    End Try
                                End While
                                If currentRow.Count > 0 Then
                                    If currentRow(0) = Player.Name And currentRow(1) = AspectName.Text Then
                                        ItemFound = CurrentItem
                                        Exit For
                                    End If
                                End If

                            End Using
                            CurrentItem += 1
                        Next

                        If ItemFound = -1 Then
                            '                               [CameraName],[PanelAspectRatioName],[PlayerLocationLeftPercentage],[PlayerLocationTopPercentage],[PlayerSizePercentage],[Enabled?],[zIndex]
                            My.Settings.UserSavedCameraLayouts.Add(Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex)
                            Logging("Info - New Camera Position added to layout: " & (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex))
                        Else
                            My.Settings.UserSavedCameraLayouts(ItemFound) = (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex)
                            Logging("Info - Updated Camera Position in layout: " & (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex))
                        End If

                    End If
                Next
            Else


                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)

                        Dim PlayerEnabled As CheckBox = CType(PlayersEnabledPanel.Controls(control.Name), CheckBox)
                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Dim zIndex As Integer = Panel.Controls.GetChildIndex(Player)

                        Dim ItemFound As Integer = -1
                        Dim CurrentItem As Integer = 0
                        For Each Item As String In My.Settings.UserSavedCameraLayouts
                            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                                MyReader.TextFieldType = FileIO.FieldType.Delimited
                                MyReader.SetDelimiters("|")

                                Dim Found As Boolean = False
                                Dim currentRow As String()
                                While Not MyReader.EndOfData
                                    Try
                                        currentRow = MyReader.ReadFields()
                                    Catch ex As Exception

                                    End Try
                                End While
                                If currentRow.Count > 0 Then
                                    If currentRow(0) = Player.Name And currentRow(1) = AspectName.Text Then
                                        ItemFound = CurrentItem
                                        Exit For
                                    End If
                                End If

                            End Using
                            CurrentItem += 1
                        Next

                        If ItemFound = -1 Then
                            '                               [CameraName],[PanelAspectRatioName],[PlayerLocationLeftPercentage],[PlayerLocationTopPercentage],[PlayerSizePercentage],[Enabled?],[zIndex]
                            My.Settings.UserSavedCameraLayouts.Add(Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex)
                            Logging("Info - New Camera Position added to layout: " & (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex))
                        Else
                            My.Settings.UserSavedCameraLayouts(ItemFound) = (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex)
                            Logging("Info - Updated Camera Position in layout: " & (Player.Name & "|" & AspectName.Text & "|" & AspectRatio.Text & "|" & ToNumUS(PlayerLeft.Text) & "|" & ToNumUS(PlayerTop.Text) & "|" & ToNumUS(PlayerSize.Text) & "|" & PlayerEnabled.Checked & "|" & zIndex))
                        End If

                    End If
                Next
            End If
            My.Settings.Save()
            SavedLayouts.Text = ""
            For Each Item As String In My.Settings.UserSavedCameraLayouts
                SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
            Next
            'SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Green_5050
            DiskSaveTimeout = 0
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Private Sub SavedLayouts_MouseClick(sender As Object, e As MouseEventArgs) Handles SavedLayouts.MouseClick
        Try
            My.Computer.Clipboard.SetText(SavedLayouts.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FFmpegOutput_MouseClick(sender As Object, e As MouseEventArgs) Handles FFmpegOutput.MouseClick
        Try
            My.Computer.Clipboard.SetText(FFmpegOutput.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AspectName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AspectName.SelectedIndexChanged
        Try
            If AspectName.Text = "" Then
                If AspectRatio.Items.Count > 0 Then
                    AspectRatio.SelectedItem = 0
                End If
                If AspectName.Items.Count > 0 Then
                    AspectName.SelectedItem = 0
                End If
                'AspectRatio.Text = "16x9"
            End If
            AspectRatio.SelectedIndex = AspectName.SelectedIndex
            UpdatePlayersLayout()
            Logging("Info - Layout Selected: " & AspectName.Text & " " & AspectRatio.Text)
            Dim x As Integer
            Dim y As Integer

            Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(AspectRatio.Text))
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(":")
                Dim currentRow As String() = {"4", "3"}
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                    Catch ex As Exception
                    End Try
                End While
                If currentRow.Count = 2 Then
                    x = Int(currentRow(0))
                    y = Int(currentRow(1))
                End If
            End Using

            If x > 0 And y > 0 Then
                If ((Me.Height - GroupBoxEXPLORER.Height - EventTimeCodeBar.Height - 25) / y) * x > Me.Width - 35 Then
                    Panel.Width = Me.Width - 30
                    Panel.Height = (Panel.Width / x) * y
                    Panel.Left = (Me.Width - Panel.Width - 15) / 2
                Else
                    Panel.Height = Me.Height - GroupBoxEXPLORER.Height - EventTimeCodeBar.Height - 25
                    Panel.Width = (Panel.Height / y) * x
                    Panel.Left = (Me.Width - Panel.Width - 15) / 2
                End If
            End If
            If My.Settings.VideoPlayerType = "VLC" Then
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                        Dim Player As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)

                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Player.Left = (PlayerLeft.Text / 100) * Panel.Width 'Panel.Width / (100 / PlayerLeft.Text)
                        Player.Top = (PlayerTop.Text / 100) * Panel.Height 'Panel.Height / (100 / PlayerTop.Text)

                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Status.Location = New Point(Player.Left, Player.Top)
                        PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                        PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                        PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                        'Player.BringToFront()
                        'Status.BringToFront()
                        PlayerTop.BringToFront()
                        PlayerLeft.BringToFront()

                    End If
                Next
            Else
                For Each control As Control In Panel.Controls
                    If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                        Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)

                        Dim PlayerSize As Label = CType(Panel.Controls(control.Name & "-Size"), Label)
                        Player.Size = New Size(((Panel.Height / (100 / PlayerSize.Text)) / 3) * 4, Panel.Height / (100 / PlayerSize.Text))

                        Dim PlayerTop As Label = CType(Panel.Controls(control.Name & "-Top"), Label)
                        Dim PlayerLeft As Label = CType(Panel.Controls(control.Name & "-Left"), Label)
                        Player.Left = (PlayerLeft.Text / 100) * Panel.Width 'Panel.Width / (100 / PlayerLeft.Text)
                        Player.Top = (PlayerTop.Text / 100) * Panel.Height 'Panel.Height / (100 / PlayerTop.Text)

                        Dim Status As Label = CType(Panel.Controls(control.Name & "-Status"), Label)
                        Status.Location = New Point(Player.Left, Player.Top)
                        PlayerTop.Location = New Point(Player.Left, Player.Top + 15)
                        PlayerLeft.Location = New Point(Player.Left, Player.Top + 30)
                        PlayerSize.Location = New Point(Player.Left, Player.Top + 45)
                        'Player.BringToFront()
                        'Status.BringToFront()
                        PlayerTop.BringToFront()
                        PlayerLeft.BringToFront()

                    End If
                Next
            End If
            UpdatePlayersLayout()
            SaveLayoutBtn.BackgroundImage = My.Resources.Disk_Normal_5050
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub AddLayoutBtn_Click(sender As Object, e As EventArgs) Handles AddLayoutBtn.Click
        GroupBoxNewLayout.Location = GBsubCONTROLS.Location
        GroupBoxNewLayout.Top += 5
        GroupBoxNewLayout.Visible = True
        NewLayoutName.Text = ""
        NewLayoutName.BackColor = Color.White
        NewAspectHeight.Text = ""
        NewAspectHeight.BackColor = Color.White
        NewAspectWidth.Text = ""
        NewAspectWidth.BackColor = Color.White
    End Sub

    Private Sub NewAspectWidth_TextChanged(sender As Object, e As EventArgs) Handles NewAspectWidth.TextChanged
        Dim X As Double
        Double.TryParse(NewAspectWidth.Text, X)

        If X <> 0 Then
            NewAspectWidth.BackColor = Color.White
        Else
            NewAspectWidth.BackColor = Color.DarkRed
        End If
        EnableSaveLayoutBtn()
    End Sub

    Private Sub NewAspectHeight_TextChanged(sender As Object, e As EventArgs) Handles NewAspectHeight.TextChanged
        Dim Y As Double
        Double.TryParse(NewAspectHeight.Text, Y)

        If Y <> 0 Then
            NewAspectHeight.BackColor = Color.White
        Else
            NewAspectHeight.BackColor = Color.DarkRed
        End If
        EnableSaveLayoutBtn()
    End Sub


    Private Sub NewLayoutName_TextChanged(sender As Object, e As EventArgs) Handles NewLayoutName.TextChanged
        If NewLayoutName.Text <> "" Then
            NewLayoutName.BackColor = Color.White
        Else
            NewLayoutName.BackColor = Color.DarkRed
        End If
        For Each Item In AspectName.Items
            If Item = NewLayoutName.Text Then
                NewLayoutName.BackColor = Color.Red
            End If
        Next
        EnableSaveLayoutBtn()
    End Sub
    Private Sub EnableSaveLayoutBtn()
        Dim Y As Double
        Double.TryParse(NewAspectHeight.Text, Y)
        Dim X As Double
        Double.TryParse(NewAspectWidth.Text, X)
        If NewLayoutName.Text <> "" And Y <> 0 And X <> 0 Then
            SaveNewLayoutBtn.Enabled = True
        Else
            SaveNewLayoutBtn.Enabled = False
        End If
        For Each Item In AspectName.Items
            If Item = NewLayoutName.Text Then
                SaveNewLayoutBtn.Enabled = False
            End If
        Next
    End Sub

    Private Sub CloseGroupboxNewLayout_Click(sender As Object, e As EventArgs) Handles CloseGroupboxNewLayout.Click
        GroupBoxNewLayout.Visible = False
    End Sub

    Private Sub SaveNewLayoutBtn_Click(sender As Object, e As EventArgs) Handles SaveNewLayoutBtn.Click
        Try
            Dim Y As Double
            Double.TryParse(NewAspectHeight.Text, Y)
            Dim X As Double
            Double.TryParse(NewAspectWidth.Text, X)
            My.Settings.AspectNames.Add(NewLayoutName.Text)
            My.Settings.AspectRatioList.Add(X & ":" & Y)
            AspectName.Items.Add(NewLayoutName.Text)
            AspectRatio.Items.Add(X & ":" & Y)
            AspectName.SelectedItem = NewLayoutName.Text
            GroupBoxNewLayout.Visible = False
            Logging("Info - New Layout Created: " & NewLayoutName.Text & " " & X & ":" & Y)
            SavedLayouts.Text = ""
            For Each Item As String In My.Settings.UserSavedCameraLayouts
                SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
            Next
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub MainForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        ResizePlayer = ""
        MovePlayer = ""
    End Sub

    Private Sub RemoveLayoutBtn_Click(sender As Object, e As EventArgs) Handles RemoveLayoutBtn.Click
        Try
            Dim result As Integer = MessageBox.Show("Are you sure you want to remove " & AspectName.Text & "?", "Remove Layout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If result = DialogResult.Yes Then
                Logging("Info - Removing Layout: " & AspectName.Text & " " & AspectRatio.Text)

                Dim RemoveItemFromList As New List(Of String)()
                For Each SavedItem As String In My.Settings.UserSavedCameraLayouts
                    Using CurrentSavedReader As New FileIO.TextFieldParser(New System.IO.StringReader(SavedItem))
                        CurrentSavedReader.TextFieldType = FileIO.FieldType.Delimited
                        CurrentSavedReader.SetDelimiters("|")
                        Dim CurrentSavedRow As String()
                        While Not CurrentSavedReader.EndOfData
                            Try
                                CurrentSavedRow = CurrentSavedReader.ReadFields()
                            Catch ex As Exception
                            End Try
                        End While
                        If CurrentSavedRow(1) = AspectName.Text And CurrentSavedRow(2) = AspectRatio.Text Then
                            RemoveItemFromList.Add(SavedItem)
                        End If
                    End Using
                Next
                If RemoveItemFromList.Count > 0 Then
                    For Each ItemList As String In RemoveItemFromList
                        My.Settings.UserSavedCameraLayouts.Remove(ItemList)
                    Next
                    RemoveItemFromList.Clear()

                    My.Settings.AspectRatioList.RemoveAt(My.Settings.AspectNames.IndexOf(AspectName.Text))
                    My.Settings.AspectNames.RemoveAt(My.Settings.AspectNames.IndexOf(AspectName.Text))

                    AspectRatio.Items.RemoveAt(AspectName.Items.IndexOf(AspectName.Text))

                    AspectName.Items.RemoveAt(AspectName.Items.IndexOf(AspectName.Text))
                    If AspectName.Items.Count > 0 Then
                        AspectName.SelectedIndex = 0
                    End If
                    My.Settings.Save()
                End If

            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub
    Dim FixTeslaCamStart As DateTime

    Private Sub AllFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllFilesToolStripMenuItem.Click
        Try

            PlayersSTOP()
            FixingNumFilesLabel.Text = ""
            FixTeslaCamFFmpegOutput.Text = ""
            FixTeslaCamBtnDone.Text = "Cancel"
            FixTeslaCamBtnDone.Enabled = True
            FixTeslaCamGroupBox.Visible = True
            FixTeslaCamQueue.Clear()
            Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamFix")
            If Not dir.Exists Then
                FileSystem.MkDir(Path.GetTempPath() & "TeslaCamFix")
            End If
            FixedTeslaCamFileCount = 0
            FixedTeslaCamFileNotDone = 0
            Dim folderpath As String = ""
            Select Case MessageBox.Show("Would you like to temporary copy these files to your local OS drive?" & vbCrLf & "Select 'Yes' to temporarily copy selected files. This temporary folder will be removed when TeslaCam Viewer is closed. " & vbCrLf & vbCrLf & "Select 'No' to choose a permanent location.", "Temporarily Copy or Permanently Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Case DialogResult.Cancel
                    FixedTeslaCamFolder = ""
                    Tv_Explorer.Enabled = True
                    FixTeslaCamGroupBox.Visible = False
                    Tv_Explorer.Focus()
                    Exit Sub
                Case DialogResult.No
                    Select Case CustomFolderBrowserDialog.ShowDialog()
                        Case DialogResult.OK
                            folderpath = CustomFolderBrowserDialog.SelectedPath
                        Case DialogResult.Cancel
                            FixedTeslaCamFolder = ""
                            Tv_Explorer.Enabled = True
                            FixTeslaCamGroupBox.Visible = False
                            Tv_Explorer.Focus()
                            Exit Sub
                    End Select
            End Select
            FixTeslaCamStart = DateTime.Now
            FixTeslaCamFiles(Tv_Explorer.SelectedNode.Tag, "\", folderpath, 0)
            Tv_Explorer.Enabled = False
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub Last3MinutesPerFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Last3MinutesPerFolderToolStripMenuItem.Click
        Try
            PlayersSTOP()
            FixingNumFilesLabel.Text = ""
            FixTeslaCamFFmpegOutput.Text = ""
            FixTeslaCamBtnDone.Text = "Cancel"
            FixTeslaCamBtnDone.Enabled = True
            FixTeslaCamGroupBox.Visible = True
            FixTeslaCamQueue.Clear()
            Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamFix")
            If Not dir.Exists Then
                FileSystem.MkDir(Path.GetTempPath() & "TeslaCamFix")
            End If
            FixedTeslaCamFileCount = 0
            FixedTeslaCamFileNotDone = 0
            Dim folderpath As String = ""
            Select Case MessageBox.Show("Would you like to temporary copy these files to your local OS drive?" & vbCrLf & "Select 'Yes' to temporarily copy selected files. This temporary folder will be removed when TeslaCam Viewer is closed. " & vbCrLf & vbCrLf & "Select 'No' to choose a permanent location.", "Temporarily Copy or Permanently Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Case DialogResult.Cancel
                    FixedTeslaCamFolder = ""
                    Tv_Explorer.Enabled = True
                    FixTeslaCamGroupBox.Visible = False
                    Tv_Explorer.Focus()
                    Exit Sub
                Case DialogResult.No
                    Select Case CustomFolderBrowserDialog.ShowDialog()
                        Case DialogResult.OK
                            folderpath = CustomFolderBrowserDialog.SelectedPath
                        Case DialogResult.Cancel
                            FixedTeslaCamFolder = ""
                            Tv_Explorer.Enabled = True
                            FixTeslaCamGroupBox.Visible = False
                            Tv_Explorer.Focus()
                            Exit Sub
                    End Select
            End Select
            FixTeslaCamStart = DateTime.Now
            FixTeslaCamFiles(Tv_Explorer.SelectedNode.Tag, "\", folderpath, 3)
            Tv_Explorer.Enabled = False
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub CustomFolderCancelBTN_Click(sender As Object, e As EventArgs) Handles CustomFolderCancelBTN.Click
        CustomFolderURL.Text = ""
        CustomFolderGroupBox.Visible = False
        Tv_Explorer.Enabled = True
    End Sub

    Private Sub CustomFolderSaveBTN_Click(sender As Object, e As EventArgs) Handles CustomFolderSaveBTN.Click
        UpdateCustomFolder(CustomFolderURL.Text)
        My.Settings.CustomDIR = CustomFolderURL.Text
        My.Settings.Save()
        CustomFolderURL.Text = ""
        CustomFolderGroupBox.Visible = False
        Tv_Explorer.Enabled = True
    End Sub

    Private Sub CustomFolderBrowseBTN_Click(sender As Object, e As EventArgs) Handles CustomFolderBrowseBTN.Click
        If (CustomFolderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            Dim folderpath = CustomFolderBrowserDialog.SelectedPath
            CustomFolderURL.Text = folderpath

        End If
    End Sub

    Private Sub ExportLayoutsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportLayoutsToolStripMenuItem.Click
        Try
            Dim ExportFile As New SaveFileDialog
            ExportFile.Filter = "TeslaCam Files (*.TeslaCam*)|*.TeslaCam"
            ExportFile.Title = "Export Layout Config"
            ExportFile.FileName = "Layout Config.TeslaCam"
            ExportFile.CheckPathExists = True
            If ExportFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim OutputFile As System.IO.StreamWriter
                OutputFile = My.Computer.FileSystem.OpenTextFileWriter(ExportFile.FileName, False, System.Text.Encoding.ASCII)
                Logging("Info - Exporting Layout List ")
                SavedLayouts.Text = ""
                OutputFile.WriteLine("[CameraName]|[PanelAspectRatioName]|[PanelAspectRatio]|[PlayerLocationLeftPercentage]|[PlayerLocationTopPercentage]|[PlayerSizePercentage]|[Enabled?]|[zIndex]")

                For Each Item As String In My.Settings.UserSavedCameraLayouts
                    OutputFile.WriteLine(Item)

                    SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
                Next
                OutputFile.Close()

            End If
        Catch ex As Exception
            MessageBox.Show("ERROR:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub ImportLayoutsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportLayoutsToolStripMenuItem.Click
        Try
            Dim ImportFile As New OpenFileDialog
            ImportFile.Filter = "TeslaCam Files (*.TeslaCam*)|*.TeslaCam"
            ImportFile.Title = "Import Layout Config"
            ImportFile.FileName = "Layout Config.TeslaCam"
            ImportFile.CheckPathExists = True
            ImportFile.CheckFileExists = True
            If ImportFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim InputFile As System.IO.StreamReader
                InputFile = My.Computer.FileSystem.OpenTextFileReader(ImportFile.FileName, System.Text.Encoding.ASCII)
                Logging("Info - Importing Layouts")
                Dim FileStringLayout As String = InputFile.ReadLine()
                Do
                    Dim Item As String = InputFile.ReadLine()
                    Using MyReader As New FileIO.TextFieldParser(New System.IO.StringReader(Item))
                        MyReader.TextFieldType = FileIO.FieldType.Delimited
                        MyReader.SetDelimiters("|")

                        Dim Found As Boolean = False
                        Dim currentRow As String()
                        While Not MyReader.EndOfData
                            Try
                                currentRow = MyReader.ReadFields()
                            Catch ex As Exception
                            End Try
                        End While
                        If currentRow.Count > 6 Then
                            '0[CameraName],1[PanelAspectRatioName],2[PanelAspectRatio],3[PlayerLocationLeftPercentage],4[PlayerLocationTopPercentage],5[PlayerSizePercentage],6[Enabled?],7[zIndex]
                            Dim UpdateItemListOld As New List(Of String)()
                            Dim UpdateItemListNew As New List(Of String)()
                            For Each SavedItem As String In My.Settings.UserSavedCameraLayouts
                                Using CurrentSavedReader As New FileIO.TextFieldParser(New System.IO.StringReader(SavedItem))
                                    CurrentSavedReader.TextFieldType = FileIO.FieldType.Delimited
                                    CurrentSavedReader.SetDelimiters("|")
                                    Dim CurrentSavedRow As String()
                                    While Not CurrentSavedReader.EndOfData
                                        Try
                                            CurrentSavedRow = CurrentSavedReader.ReadFields()
                                        Catch ex As Exception
                                        End Try
                                    End While
                                    If currentRow(0) = CurrentSavedRow(0) And currentRow(1) = CurrentSavedRow(1) And currentRow(2) = CurrentSavedRow(2) Then
                                        Found = True
                                        If Item <> SavedItem Then
                                            Dim Save = MessageBox.Show("The """ & currentRow(0) & """ camera in """ & currentRow(1) & """ layout changed. Would you like to update it?" & vbCrLf & vbCrLf & "Original: """ & SavedItem & """" & vbCrLf & "      New: """ & Item & """" & vbCrLf & vbCrLf & FileStringLayout, "Importing Layout", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                            If Save = DialogResult.Yes Then
                                                UpdateItemListOld.Add(SavedItem)
                                                UpdateItemListNew.Add(Item)

                                                Logging("Info - Layout Updated: [" & SavedItem & "] to [" & Item & "]")
                                            End If
                                        End If
                                    End If
                                End Using
                            Next
                            If Found = False Then
                                My.Settings.UserSavedCameraLayouts.Add(Item)
                                Dim AlreadyAdded As Boolean = False
                                For i As Integer = 0 To AspectName.Items.Count - 1
                                    If AspectName.Items.Item(i) = currentRow(1) And AspectRatio.Items.Item(i) = currentRow(2) Then
                                        AlreadyAdded = True
                                    End If
                                Next
                                If AlreadyAdded = False Then
                                    AspectName.Items.Add(currentRow(1))
                                    My.Settings.AspectNames.Add(currentRow(1))
                                    AspectRatio.Items.Add(currentRow(2))
                                    My.Settings.AspectRatioList.Add(currentRow(2))
                                End If

                                Logging("Info - New Layout Added: [" & Item & "]")
                            Else
                                For Each ItemList As String In UpdateItemListOld
                                    My.Settings.UserSavedCameraLayouts.Item(My.Settings.UserSavedCameraLayouts.IndexOf(ItemList)) = UpdateItemListNew.Item(UpdateItemListOld.IndexOf(ItemList))
                                Next
                                UpdateItemListNew.Clear()
                                UpdateItemListOld.Clear()
                            End If
                        End If
                    End Using

                Loop While Not InputFile.EndOfStream
                My.Settings.Save()
                SavedLayouts.Text = ""
                For Each Item As String In My.Settings.UserSavedCameraLayouts
                    SavedLayouts.Text += "<string>" & Item.Replace("&", "&amp;") & "</string>" & vbCrLf
                Next
                InputFile.Close()
                MessageBox.Show("Done Importing Layout Config File", "Importing Layout", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub ChangeCustomFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeCustomFolderToolStripMenuItem.Click
        Tv_Explorer.Enabled = False
        CustomFolderURL.Text = My.Settings.CustomDIR
        CustomFolderGroupBox.BringToFront()
        CustomFolderGroupBox.Visible = True
        ExplorerMenuStrip.Close()
    End Sub

    Private Sub ConfirmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfirmToolStripMenuItem.Click
        Try
            Logging("Info - Formatting: " & FormatDriveLetter)
            Dim p As New Process
            p.StartInfo.FileName = "cmd.exe"  'System.IO.Path.Combine(Application.StartupPath, "fat32format.exe")
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = False
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal

            p.StartInfo.Verb = "runas"

            p.StartInfo.Arguments = " " + "/C" + " Echo off && CLS && Echo Please close ALL Explorer windows before formatting && """ + System.IO.Path.Combine(Application.StartupPath, "fat32format.exe") + """ " + FormatDriveLetter + " & echo. & Pause"
            p.Start()
            p.WaitForExit()
            p.Close()

            Dim AddTeslaCam = MessageBox.Show("Would you like to add TeslaCam to this drive?", "Add TeslaCam", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If AddTeslaCam = DialogResult.Yes Then
                Dim dir As New IO.DirectoryInfo(FormatDriveLetter & "TeslaCam")
                If Not dir.Exists Then
                    FileSystem.MkDir(FormatDriveLetter & "TeslaCam")
                End If
                Dim Viewer As New IO.DirectoryInfo(FormatDriveLetter & "TeslaCam Viewer")
                If Not Viewer.Exists Then
                    FileSystem.MkDir(FormatDriveLetter & "TeslaCam Viewer")
                End If
                My.Computer.FileSystem.CopyDirectory(Application.StartupPath, FormatDriveLetter & "TeslaCam Viewer")

                Logging("Info - Added TeslaCam To: " & FormatDriveLetter)
            End If
            Me.BringToFront()
            RefreshRootNodes()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub ViewLogFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewLogFilesToolStripMenuItem.Click
        Try

            Process.Start(Path.GetTempPath() & "TeslaCamViewerLogs")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub YouTubeTutorialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YouTubeTutorialToolStripMenuItem.Click
        Try
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://raw.githubusercontent.com/NateMccomb/TeslaCamViewerII/master/YouTubeTutorial")
            Dim response As System.Net.HttpWebResponse = request.GetResponse

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream)
            Dim YouTubeURL As String = sr.ReadLine


            Process.Start(YouTubeURL)
            LinksSelected = LinksSelected & "&entry.839831962=YouTube"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RenderInTimeLabel_Click(sender As Object, e As EventArgs) Handles RenderInTimeLabel.Click

    End Sub

    Private Sub RenderOutTimeLabel_Click(sender As Object, e As EventArgs) Handles RenderOutTimeLabel.Click

    End Sub
    Dim EventTimeMove As Boolean = False
    Private Sub EventTimeCodeBar_Scroll(sender As Object, e As EventArgs) Handles EventTimeCodeBar.Scroll
        Try
            EventTimeMove = True
            Dim TotalSoFar As Double
            Dim CurrentTime As Integer
            For i As Integer = 1 To MaxDurationsList.Items.Count
                Dim CurrentMax = MaxDurationsList.Items.Item(MaxDurationsList.Items.Count - i) * 10
                CurrentTime += 1
                If TotalSoFar + CurrentMax > EventTimeCodeBar.Value Then
                    CurrentTimeList.SelectedIndex = CurrentTimeList.Items.Count - CurrentTime
                    If My.Settings.VideoPlayerType = "VLC" Then
                        For Each control As Control In Panel.Controls
                            If control.GetType Is GetType(AxAXVLC.AxVLCPlugin2) Then
                                Dim VLCPlayer As AxAXVLC.AxVLCPlugin2 = CType(Panel.Controls(control.Name), AxAXVLC.AxVLCPlugin2)
                                VLCPlayer.input.time = (EventTimeCodeBar.Value - TotalSoFar) * 100
                            End If
                        Next
                    Else
                        For Each control As Control In Panel.Controls
                            If control.GetType Is GetType(AxWindowsMediaPlayer) Then
                                Dim Player As AxWindowsMediaPlayer = CType(Panel.Controls(control.Name), AxWindowsMediaPlayer)
                                Player.Ctlcontrols.currentPosition = (EventTimeCodeBar.Value - TotalSoFar) / 10
                            End If
                        Next
                    End If

                    'TimeCodeBar.Value = (EventTimeCodeBar.Value) - (TotalSoFar) '- (CurrentMax)
                    Exit For
                End If
                TotalSoFar += CurrentMax
            Next
            EventTimeMove = False
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub EventTimeCodeBar_MouseMove(sender As Object, e As MouseEventArgs) Handles EventTimeCodeBar.MouseMove
        Try
            '                               (((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum) * RenderOutTime) + 10
            If MoveRenderOut = True And e.X < EventTimeCodeBar.Width - 14 And e.X > 14 And e.X > RenderInTimeGraphic.Left + 14 Then
                RenderOutTimeGraphic.Left = e.X - 5
                RenderOutTime = ((e.X - 15) / ((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum))
            End If
            If MoveRenderIn = True And e.X < EventTimeCodeBar.Width - 14 And e.X > 14 And e.X < RenderOutTimeGraphic.Left - 4 Then
                RenderInTimeGraphic.Left = e.X - 5
                RenderInTime = ((e.X - 15) / ((EventTimeCodeBar.Width - 30) / EventTimeCodeBar.Maximum))
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub EventTimeCodeBar_MouseClick(sender As Object, e As MouseEventArgs) Handles EventTimeCodeBar.MouseClick
        MoveRenderOut = False
        MoveRenderIn = False
    End Sub

    Private Sub MaxNumberOfThreads_Click(sender As Object, e As EventArgs) Handles MaxNumberOfThreads.Click

    End Sub

    Private Sub MaxNumberOfThreads_TextChanged(sender As Object, e As EventArgs) Handles MaxNumberOfThreads.TextChanged
        Dim MaxThreads As Integer
        Double.TryParse(MaxNumberOfThreads.Text, MaxThreads)

        If MaxThreads <> 0 Then
            MaxNumberOfThreads.BackColor = Color.White
            My.Settings.MaxThreads = MaxThreads
            My.Settings.Save()
        Else
            MaxNumberOfThreads.BackColor = Color.DarkRed
        End If
    End Sub

    Private Sub MaxNumberOfThreads_KeyDown(sender As Object, e As KeyEventArgs) Handles MaxNumberOfThreads.KeyDown
        If e.KeyCode = Keys.Enter Then
            SettingsMenuStrip.Visible = False
        End If
    End Sub

    Private Sub SettingsMenuStrip_Opening(sender As Object, e As CancelEventArgs) Handles SettingsMenuStrip.Opening
        MaxNumberOfThreads.Text = My.Settings.MaxThreads
    End Sub

    Private Sub BtnREVERSE_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnREVERSE.MouseDown
        VLCReverse = True
    End Sub

    Private Sub BtnREVERSE_MouseUp(sender As Object, e As MouseEventArgs) Handles BtnREVERSE.MouseUp
        VLCReverse = False
    End Sub

    Private Sub VideoPlayerType_Click(sender As Object, e As EventArgs) Handles VideoPlayerType.Click

    End Sub

    Private Sub VideoPlayerType_TextChanged(sender As Object, e As EventArgs) Handles VideoPlayerType.TextChanged

    End Sub

    Private Sub VideoPlayerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VideoPlayerType.SelectedIndexChanged
        Try
            If My.Settings.VideoPlayerType <> VideoPlayerType.Text Then
                SettingsMenuStrip.Visible = False
                MsgBox("Please Restart TeslaCam Viewer II For Settings To Take Effect.", MsgBoxStyle.OkOnly, "Restart")
                Logging("Info - Video Player Type Changed to " & VideoPlayerType.Text)
            End If
        Catch ex As Exception
            If Debug_Mode = True Then
                MessageBox.Show("DebugMode:" & vbCrLf & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Private Sub TrackBar2_MouseClick(sender As Object, e As MouseEventArgs) Handles TrackBar2.MouseClick
        If e.Button = MouseButtons.Right Then
            TrackBar2.Value = 10
        End If
    End Sub

    Private Sub SentryBTN_Click(sender As Object, e As EventArgs) Handles SentryBTN.Click
        QuickStart = False
        LoadSentryEvent(False)
    End Sub
End Class
