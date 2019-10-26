Imports System.IO
Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication
        Dim GoogleFormURL As String = "https://docs.google.com/forms/d/e/URL"
        Dim CPUID As String
        Dim StartTime As DateTime
        'Public ViewedClips
        'Public SavedClips
        'Public LinksSelected
        'Public UpDated
        Public CurrentVersion As String
        'Application.ProductVersion
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            StartTime = My.Computer.Clock.LocalTime
            If Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Active Setup\Installed Components\{22d6f312-b0f6-11d0-94ab-0080c74c7e95}") Is Nothing Then
                MsgBox("You need to have Windows Media Player Installed to run TeslaCam Viewer" + vbCrLf + "1. Open Apps and Features" + vbCrLf + "2. Click Programs and Features on the right" + vbCrLf + "3. Click Turn Windows features on or off" + vbCrLf + "4. Click the checkbox on Media Features", MsgBoxStyle.OkCancel, "ERROR")
                'MainForm.Close()
                End
            Else
                ' Key existed
            End If
            'MainForm.ProductVersion
        End Sub
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException

            My.Application.Log.WriteException(e.Exception, TraceEventType.Critical, "Unhandled Exception.")
            SendClosingReport(e.Exception.Message.ToString.Replace("&", "-") & vbCrLf & e.Exception.StackTrace.ToString.Replace("&", "-"))
            'MessageBox.Show("Sending Crash Report:" & e.Exception.Message.ToString)
        End Sub
        Private Function SystemSerialNumber() As String
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

            Return serial_numbers
        End Function
        Private Function GetCpuId() As String
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

            Return cpu_ids
        End Function

        Public Sub SendClosingReport(CrashReport As String)
            Try
                'CrashReport.
                If My.Settings.CustomID = "" Then
                    CPUID = GetCpuId() & SystemSerialNumber()
                    CurrentVersion = Me.MainForm.ProductVersion
                Else
                    CPUID = My.Settings.CustomID
                End If

                Dim CurrentDateTime As DateTime = My.Computer.Clock.LocalTime
                Dim DateCode As String = CurrentDateTime.Year.ToString("0000") & "-" & CurrentDateTime.Month.ToString("00") & "-" & CurrentDateTime.Day.ToString("00")
                Dim elapsedTime As TimeSpan = CurrentDateTime.Subtract(StartTime)
                Dim webClient As New System.Net.WebClient

                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(GoogleFormURL & "/formResponse?usp=pp_url&entry.658672747=" &
                                                                                            Uri.EscapeUriString(CPUID) &
                                                                                            "&entry.1086997034=" & Uri.EscapeUriString(CurrentVersion) &
                                                                                            "&entry.1505263955=" & DateCode &
                                                                                            "&entry.1922677689=" & elapsedTime.TotalMinutes.ToString(".00").Replace(",", ".") &
                                                                                            "&entry.1644399542=" &
                                                                                            "&entry.1103660004=" & Uri.EscapeUriString(My.Settings.UserLanguage) &
                                                                                            "&entry.2057430665=" & Uri.EscapeUriString(CrashReport) & "&submit=Submit")
                request.Timeout = 5000
                request.GetResponse()

                If My.Settings.UserLanguage = "Dutch" Then
                    MessageBox.Show("Om de volgende reden is er een foutrapportage naar Nate verstuurd:" & vbCrLf & CrashReport.Remove(CrashReport.IndexOf(vbCrLf)), "Applicatie fout", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    MessageBox.Show("Se le ha enviado un mensaje de error a Nate, por la siguiente razón:" & vbCrLf & CrashReport.Remove(CrashReport.IndexOf(vbCrLf)), "Error de Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf My.Settings.UserLanguage = "German" Then
                    MessageBox.Show("Aus folgendem Grund wurde ein Fehlerbericht an Nate gesendet:" & vbCrLf & CrashReport.Remove(CrashReport.IndexOf(vbCrLf)), "Anwendungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("A bug report was sent to Nate for the following reason:" & vbCrLf & CrashReport.Remove(CrashReport.IndexOf(vbCrLf)), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Logging(" - Application ERROR - " & CrashReport)
            Catch ex As Exception
                Try
                    If My.Settings.UserLanguage = "Dutch" Then
                        MessageBox.Show("Er is een fout opgetreden, maar er kan geen foutrapportage mnaar Nate woirden verstuurd omdat er geen internet connectie is: " & vbCrLf & CrashReport & vbCrLf, "Applicatie fout", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf My.Settings.UserLanguage = "Español" Then
                        MessageBox.Show("Hubo un error, pero no podemos enviarle el reporte a Nate, por que no hay acceso a Internet: " & vbCrLf & CrashReport & vbCrLf, "Error de Aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf My.Settings.UserLanguage = "German" Then
                        MessageBox.Show("Es gab einen Fehler, aber es konnte kein Fehlerbericht an Nate gesendet werden (kein Internet): " & vbCrLf & CrashReport & vbCrLf, "Anwendungsfehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("There was an error but we couldn't send a bug report to Nate because there is no internet access: " & vbCrLf & CrashReport & vbCrLf, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Logging(" - Application ERROR - " & CrashReport)
                Catch exx As Exception
                    MessageBox.Show(exx.Message.ToString)
                End Try
                'MessageBox.Show("There was an error but we couldn't send a bug report to Nate because there is no internet access: " & vbCrLf & CrashReport.Remove(CrashReport.IndexOf(GetNthIndex(CrashReport, vbCrLf, 1))) & vbCrLf & vbCrLf, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub
        Public Sub Logging(ByVal Text As String)
            Try
                Dim dir As New IO.DirectoryInfo(Path.GetTempPath() & "TeslaCamViewerLogs")
                If Not dir.Exists Then
                    FileSystem.MkDir(Path.GetTempPath() & "TeslaCamViewerLogs")
                End If
                Dim objWriter As New System.IO.StreamWriter(Path.GetTempPath() & "TeslaCamViewerLogs\TCViewerCrashLog.txt", True)
                objWriter.WriteLine(Environment.NewLine & DateTime.Now & " - " & Text.ToString)
                objWriter.Close()
            Catch ex As Exception
            End Try
        End Sub
    End Class
End Namespace
