Imports System.IO

Module Languages
    Public Sub ChangeLanguage()
        MainForm.MainToolTip.RemoveAll()
        FFmpegExists()
        NewVersion()
        Set_Main()

    End Sub
    Public Sub FFmpegExists()
        If File.Exists(System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")) = True Then
            MainForm.RenderEnabled = True
            MainForm.SettingsBTN.Visible = True
            MainForm.Logging("Info - FFmpeg - FOUND")

            MainForm.MainToolTip.SetToolTip(MainForm.SettingsBTN, "Export all selected files from the selected time window into one video file")

            If File.Exists(Path.GetTempPath() & "Black.mp4") = False Then
                Try
                    MainForm.Logging("Info - Black.mp4 not found")
                    Dim p As New Process
                    p.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.RedirectStandardError = True
                    p.EnableRaisingEvents = True
                    p.StartInfo.Arguments = "-f lavfi -i color=c=black:s=1280x960:d=3 -r 36 -y " & Path.GetTempPath() & "Black.mp4"
                    p.Start()
                    MainForm.Logging("Info - Creating Black.mp4")
                Catch ex As Exception
                    MainForm.Logging("Error - " & ex.Message & " IN:" & ex.TargetSite.Name & vbCrLf & ex.StackTrace)
                End Try
            Else
                MainForm.Logging("Info - Black.mp4 was found")
            End If
        Else
            MainForm.RenderEnabled = False
            MainForm.SettingsBTN.Visible = False
            MainForm.Logging("Info - FFmpeg - NOT FOUND")


        End If

    End Sub

    Public Sub NewVersion()
        Try
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://raw.githubusercontent.com/NateMccomb/TeslaCamViewerII/master/Version")
            Dim response As System.Net.HttpWebResponse = request.GetResponse

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream)
            Dim NewestVersion As String = sr.ReadLine
            Dim VersionNotes As String = sr.ReadToEnd


            If NewestVersion.Contains(MainForm.CurrentVersion) Or NewestVersion.Contains("2019.44.0.0") Then
                If My.Settings.UserLanguage = "Dutch" Then
                    MainForm.UPDATELabel.Text = "Je hebt de laatste versie"
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Je beschikt over de laatste versie: " & MainForm.CurrentVersion & vbCrLf & VersionNotes)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    MainForm.UPDATELabel.Text = "Estas actualizado"
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Estas actualizado con la version: " & MainForm.CurrentVersion & vbCrLf & VersionNotes)
                ElseIf My.Settings.UserLanguage = "German" Then
                    MainForm.UPDATELabel.Text = "Du bist auf aktuellem Stand"
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Du bist auf aktuellem Stand mit Version: " & MainForm.CurrentVersion & vbCrLf & VersionNotes)
                Else
                    MainForm.UPDATELabel.Text = "You are up to date"
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "You are up to date with Version: " & MainForm.CurrentVersion & vbCrLf & VersionNotes)
                End If
                MainForm.DownloadUpdateToolStripMenuItem.Visible = False
                MainForm.Logging("Info - TeslaCam Viewer Up to date")
            Else
                If My.Settings.UserLanguage = "Dutch" Then
                    MainForm.UPDATELabel.Text = "Er is een nieuwe versie beschikbaar: " & NewestVersion
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Klik hier om de nieuwste versie te downloaden: " & NewestVersion & vbCrLf & VersionNotes)
                ElseIf My.Settings.UserLanguage = "Español" Then
                    MainForm.UPDATELabel.Text = "Hay una nnueva versión disponible: " & NewestVersion
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Haz clic para bajar la nueva versión: " & NewestVersion & vbCrLf & VersionNotes)
                ElseIf My.Settings.UserLanguage = "German" Then
                    MainForm.UPDATELabel.Text = "Eine neue Version ist verfügbar: " & NewestVersion
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Klicke hier um die neueste Version herunterzuladen: " & NewestVersion & vbCrLf & VersionNotes)
                Else
                    MainForm.UPDATELabel.Text = "There's a new version available: " & NewestVersion
                    MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Click here to download the newest version: " & NewestVersion & vbCrLf & VersionNotes)
                End If
                MainForm.DownloadUpdateToolStripMenuItem.Visible = True
                MainForm.UPDATELabel.BackColor = Color.BlueViolet
                MainForm.Logging("Info - New Version of TeslaCam Viewer is available - " & vbCrLf & VersionNotes)
            End If
        Catch ex As Exception
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UPDATELabel.Text = MainForm.UPDATELabel.Text & " --Geen internet connectie-- "
                MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Kan niet controleren op nieuwe updates.")
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UPDATELabel.Text = MainForm.UPDATELabel.Text & " --No hay acceso a Internet-- "
                MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "No se pueden buscar nuevas actualizaciones.")
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UPDATELabel.Text = MainForm.UPDATELabel.Text & " --Keine Internetverbindung-- "
                MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Es kann nicht nach neuen Updates gesucht werden.")
            Else
                MainForm.UPDATELabel.Text = MainForm.UPDATELabel.Text & " --No Internet Access-- "
                MainForm.MainToolTip.SetToolTip(MainForm.UPDATELabel, "Unable to check for new updates.")
            End If
            MainForm.Logging("Info - Unable to check for new updates")
        End Try
    End Sub

    Public Sub Set_Main()
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        MainForm.Logging("Info - Language = " & My.Settings.UserLanguage)
        If My.Settings.UserLanguage = "Dutch" Then

            MainForm.LanToolStripMenuItem.Text = "Taal"
            MainForm.LanguageSelection.Text = "Dutch"
            MainForm.MainToolTip.SetToolTip(MainForm.Tv_Explorer, "F5-vernieuwen, klik met de rechtermuisknop - om het bestand / de map te openen of de aangepaste map te wijzigen")

            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxEXPLORER, "Klik om naar een nieuwe locatie te verplaatsen")
            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxCONTROLS, "Klik om naar een nieuwe locatie te verplaatsen")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxLEFTREPEATER, "Klik om naar een nieuwe locatie te verplaatsen")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxFRONT, "Klik om naar een nieuwe locatie te verplaatsen")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxRIGHTREPEATER, "Klik om naar een nieuwe locatie te verplaatsen")

            MainForm.MainToolTip.SetToolTip(MainForm.Donation, "Bedankt!")

            MainForm.MainToolTip.SetToolTip(MainForm.SentryModeMarker, "Sentry Mode Trigger - indien Sentry Mode was ingeschakeld")
            MainForm.MainToolTip.SetToolTip(MainForm.TimeCodeBar, "Spring terug / vooruit '<' / '>', Frame terug / vooruit 'M' '/'")
            MainForm.MainToolTip.SetToolTip(MainForm.TrackBar2, "Snelheid - '+' / '-'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPLAY, "Play / Pause - 'Spatiebalk' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPAUSE, "Play / Pause - 'Spatiebalk' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnREVERSE, "Achteruit - 'R' & stop - 'S'")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerCenter, "Klik voor volledig scherm")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerLeft, "Klik voor volledig scherm")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerRight, "Klik voor volledig scherm")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderFileProgress, "Verwerking aantal bestanden")
            MainForm.MainToolTip.SetToolTip(MainForm.ThreadsRunningLabel, "Aantal lopende threads")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderPlayerTimecode, "Huidige tijdcode")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeLabel, "Opslaan als startpunt" & vbCrLf & "Klik met de rechtermuisknop om de huidige tijdcode in te stellen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeLabel, "Opslaan als eindpunt" & vbCrLf & "Klik met de rechtermuisknop om de huidige tijdcode in te stellen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderTotalTimeLabel, "Opslaan als bestandslengte")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeGraphic, "In puntmarkering" & vbCrLf & "Klik met de rechtermuisknop om de positie van de tijdcode te verplaatsen / in te stellen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeGraphic, "Uit puntmarkering" & vbCrLf & "Klik met de rechtermuisknop om de positie van de tijdcode te verplaatsen / in te stellen")
            MainForm.MainToolTip.SetToolTip(MainForm.FFmpegOutput, "FFmpeg Output Console Window")
            MainForm.MainToolTip.SetToolTip(MainForm.CurrentTimeList, "Houd 'CTRL' of 'SHIFT' ingedrukt om meerdere keren te selecteren")
            MainForm.MainToolTip.SetToolTip(MainForm.AppSettingsButton, "Applicatie instellingen")


            MainForm.VideoRendering.Text = "Je video wordt gemaakt en kan enkele minuten duren. Even geduld terwijl dit proces is voltooid."
            MainForm.RenderBTN.Text = "Render"


            MainForm.VideoQualityLabel.Text = "Video kwaliteit"

            MainForm.RenderQuality.Items.Clear()
            MainForm.RenderQuality.Items.Add("Max")
            MainForm.RenderQuality.Items.Add("Hoog")
            MainForm.RenderQuality.Items.Add("Medium")
            MainForm.RenderQuality.Items.Add("Laag")
            MainForm.RenderQuality.SelectedIndex = My.Settings.RenderQuality
            'MainForm.GroupBoxFRONT.Text = "Front"
            'MainForm.GroupBoxLEFTREPEATER.Text = "Linker Repeater"
            'MainForm.GroupBoxRIGHTREPEATER.Text = "Rechter Repeater"

            MainForm.FlipLREnable.Text = "Swap LR"
            MainForm.MirrorLREnable.Text = "Spiegel LR"
            MainForm.SettingsBTN.Text = "Instellingen"
            MainForm.BtnREVERSE.Text = "Reverse"
            MainForm.BtnPLAY.Text = "Play"
            MainForm.BtnPAUSE.Text = "Pause"

            MainForm.ControlsSpeed.Text = "Snelheid"

            MainForm.DownloadUpdateToolStripMenuItem.Text = "Klik hier om de nieuwste versie te downloaden:"

        ElseIf My.Settings.UserLanguage = "Español" Then

            MainForm.LanToolStripMenuItem.Text = "Idioma"
            MainForm.LanguageSelection.Text = "Español"
            MainForm.MainToolTip.SetToolTip(MainForm.Tv_Explorer, "F5-Refrescar, Click de la derecha - Para abrir el archivo cambiar de carpeta")

            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxEXPLORER, "Clic para mover a nueva localización")
            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxCONTROLS, "Clic para mover a nueva localización")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxLEFTREPEATER, "Clic para mover a nueva localización")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxFRONT, "Clic para mover a nueva localización")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxRIGHTREPEATER, "Clic para mover a nueva localización")

            MainForm.MainToolTip.SetToolTip(MainForm.Donation, "¡Gracias!")

            MainForm.MainToolTip.SetToolTip(MainForm.SentryModeMarker, "Activar el modeo sentinela - Si el modo sentineta ha sido activado")
            MainForm.MainToolTip.SetToolTip(MainForm.TimeCodeBar, "Brincar Atrás/Adelante '<' / '>', Cuadro Atrás/Adelante 'M' '/'")
            MainForm.MainToolTip.SetToolTip(MainForm.TrackBar2, "Velocidad - '+' / '-'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPLAY, "Reproducir/Pausa - 'Barra de espacio' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPAUSE, "Reproducir/Pausa - 'Barra de espacio' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnREVERSE, "Marcha atrás - 'R' & Detener - 'S'")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerCenter, "Clic para pantalla completa")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerLeft, "Clic para pantalla completa")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerRight, "Clic para pantalla completa")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderFileProgress, "Procesando número de archivos")
            MainForm.MainToolTip.SetToolTip(MainForm.ThreadsRunningLabel, "Número de procesor corriendo")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderPlayerTimecode, "Código de tiempo actual")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeLabel, "Grabar como punto de comienzo" & vbCrLf & "Clic derecho para configurar en el código de tiempo actual")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeLabel, "Grabar como punto final" & vbCrLf & "Clic derecho para configurar en el código de tiempo actual")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderTotalTimeLabel, "Grabar como duración")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeGraphic, "Marcador" & vbCrLf & "Clic de la derecha para mover / indicar posición de tiempo")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeGraphic, "Marcador de salida" & vbCrLf & "Clic de la derecha para mover / indicar posición de tiempo")
            MainForm.MainToolTip.SetToolTip(MainForm.FFmpegOutput, "Consola de salida FFmpeg")
            MainForm.MainToolTip.SetToolTip(MainForm.CurrentTimeList, "Apretar 'CTRL' o 'SHIFT' para seleccionar múltiples archivos")
            MainForm.MainToolTip.SetToolTip(MainForm.AppSettingsButton, "Application settings")


            MainForm.VideoRendering.Text = "Su video se está procesando y podría tomar varios minutos. Tenga paciencia mientras se completa este proceso."
            MainForm.RenderBTN.Text = "Procesar"

            MainForm.VideoQualityLabel.Text = "Calidad de vídeo"

            MainForm.RenderQuality.Items.Clear()
            MainForm.RenderQuality.Items.Add("Max")
            MainForm.RenderQuality.Items.Add("Alta")
            MainForm.RenderQuality.Items.Add("Mediana")
            MainForm.RenderQuality.Items.Add("Baja")
            MainForm.RenderQuality.SelectedIndex = My.Settings.RenderQuality
            'MainForm.GroupBoxFRONT.Text = "Delantero"
            'MainForm.GroupBoxLEFTREPEATER.Text = "Repetidor de la izquierda"
            'MainForm.GroupBoxRIGHTREPEATER.Text = "Repetidor de la derecha"

            MainForm.FlipLREnable.Text = "Voltear LR"
            MainForm.MirrorLREnable.Text = "Espejo LR"
            MainForm.SettingsBTN.Text = "Exportar"
            MainForm.BtnREVERSE.Text = "Marcha Atrás"
            MainForm.BtnPLAY.Text = "Reproducir"
            MainForm.BtnPAUSE.Text = "Pausa"

            MainForm.ControlsSpeed.Text = "Velocidad"

            MainForm.DownloadUpdateToolStripMenuItem.Text = "Haz clic para bajar la nueva versión"

        ElseIf My.Settings.UserLanguage = "German" Then
            MainForm.LanToolStripMenuItem.Text = "Sprache"
            MainForm.LanguageSelection.Text = "German"
            MainForm.MainToolTip.SetToolTip(MainForm.Tv_Explorer, "F5-Aktualisieren, Rechtsklick - Datei/Ordner öffnen oder benutzerdefinierten Ordner ändern")

            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxEXPLORER, "Klicken um an neuen Ort zu verschieben")
            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxCONTROLS, "Klicken um an neuen Ort zu verschieben")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxLEFTREPEATER, "Klicken um an neuen Ort zu verschieben")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxFRONT, "Klicken um an neuen Ort zu verschieben")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxRIGHTREPEATER, "Klicken um an neuen Ort zu verschieben")

            MainForm.MainToolTip.SetToolTip(MainForm.Donation, "Danke!")

            MainForm.MainToolTip.SetToolTip(MainForm.SentryModeMarker, "Wächter-Modus Aktivierung - Wenn Wächter Modus aktiviert wurde")
            MainForm.MainToolTip.SetToolTip(MainForm.TimeCodeBar, "Springe zurück/vorwärts '<' / '>', Frame zurück/vorwärts 'M' '/'")
            MainForm.MainToolTip.SetToolTip(MainForm.TrackBar2, "Geschwindigkeit - '+' / '-'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPLAY, "Play/Pause - 'Leertaste' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPAUSE, "Play/Pause - 'Leertaste' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnREVERSE, "Rückwärts - 'R' & Stop - 'S'")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerCenter, "Klicke für Vollbild")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerLeft, "Klicke für Vollbild")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerRight, "Klicke für Vollbild")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderFileProgress, "Prozessiere Anzahl an Dateien")
            MainForm.MainToolTip.SetToolTip(MainForm.ThreadsRunningLabel, "Anzahl an laufenden Prozessen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderPlayerTimecode, "Aktueller Zeitstempel")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeLabel, "Speichere als Startpunkt" & vbCrLf & "Rechtsklick um aktuellen Zeitstempel zu setzen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeLabel, "Speichere als Endpunkt" & vbCrLf & "Rechtsklick um aktuellen Zeitstempel zu setzen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderTotalTimeLabel, "Speichere als Dateilänge")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeGraphic, "In-Point Markierung" & vbCrLf & "Rechtsklick um das Ändern/Setzen des Zeitstempels zu aktivieren")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeGraphic, "Out-Point Markierung" & vbCrLf & "Rechtsklick um das Ändern/Setzen des Zeitstempels zu aktivieren")
            MainForm.MainToolTip.SetToolTip(MainForm.FFmpegOutput, "FFmpeg Ausgabe-Konsole")
            MainForm.MainToolTip.SetToolTip(MainForm.CurrentTimeList, "Halte 'STRG' or 'SHIFT' um mehrere Dateien auszuwählen")
            MainForm.MainToolTip.SetToolTip(MainForm.AppSettingsButton, "Application settings")

            MainForm.VideoRendering.Text = "Dein Video wird erstellt. Dies kann einige Minuten dauern. Bitte hab ein wenig Geduld."
            MainForm.RenderBTN.Text = "Render"

            'ControlsSpeed.Text =
            MainForm.VideoQualityLabel.Text = "Video Qualität"

            MainForm.RenderQuality.Items.Clear()
            MainForm.RenderQuality.Items.Add("Max")
            MainForm.RenderQuality.Items.Add("Hoch")
            MainForm.RenderQuality.Items.Add("Mittel")
            MainForm.RenderQuality.Items.Add("Niedrig")
            MainForm.RenderQuality.SelectedIndex = My.Settings.RenderQuality
            'MainForm.GroupBoxFRONT.Text = "Vorne"
            'MainForm.GroupBoxLEFTREPEATER.Text = "Linker Repeater"
            'MainForm.GroupBoxRIGHTREPEATER.Text = "Rechter Repeater"

            MainForm.FlipLREnable.Text = "LR tauschen"
            MainForm.MirrorLREnable.Text = "LR spiegeln"
            MainForm.SettingsBTN.Text = "Einstellungen"
            MainForm.BtnREVERSE.Text = "Rückwärts"
            MainForm.BtnPLAY.Text = "Play"
            MainForm.BtnPAUSE.Text = "Pause"

            MainForm.ControlsSpeed.Text = "Geschwindigkeit"

            MainForm.DownloadUpdateToolStripMenuItem.Text = "Klicke hier um die neueste Version herunterzuladen:"

        Else
            MainForm.MainToolTip.SetToolTip(MainForm.SaveLayoutBtn, "Save current layout")
            MainForm.MainToolTip.SetToolTip(MainForm.AddLayoutBtn, "Add a new camera layout")
            MainForm.MainToolTip.SetToolTip(MainForm.RemoveLayoutBtn, "Remove selected camera layout")
            MainForm.LanToolStripMenuItem.Text = "Language"
            MainForm.LanguageSelection.Text = "English"
            MainForm.MainToolTip.SetToolTip(MainForm.Tv_Explorer, "F5-Refresh, Right click - To open file/folder or change Custom Folder")

            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxEXPLORER, "Click to move to new location")
            MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxCONTROLS, "Click to move to new location")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxLEFTREPEATER, "Click to move to new location")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxFRONT, "Click to move to new location")
            'MainForm.MainToolTip.SetToolTip(MainForm.GroupBoxRIGHTREPEATER, "Click to move to new location")

            MainForm.MainToolTip.SetToolTip(MainForm.Donation, "Thanks!")
            MainForm.MainToolTip.SetToolTip(MainForm.EventSentryModeMarker, "Sentry Mode Trigger - If Sentry Mode was enabled")
            MainForm.MainToolTip.SetToolTip(MainForm.SentryModeMarker, "Sentry Mode Trigger - If Sentry Mode was enabled")
            MainForm.MainToolTip.SetToolTip(MainForm.TimeCodeBar, "Jump Back/Forward '<' / '>', Frame Back/Forward 'M' '/'")
            MainForm.MainToolTip.SetToolTip(MainForm.TrackBar2, "Speed - '+' / '-'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPLAY, "Play/Pause - 'Spacebar' / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnPAUSE, "Play/Pause - Spacebar / 'P'")
            MainForm.MainToolTip.SetToolTip(MainForm.BtnREVERSE, "Reverse - 'R' & Stop - 'S'")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerCenter, "Click for Fullscreen")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerLeft, "Click for Fullscreen")
            'MainForm.MainToolTip.SetToolTip(MainForm.PlayerRight, "Click for Fullscreen")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderFileProgress, "Processing number of files")
            MainForm.MainToolTip.SetToolTip(MainForm.ThreadsRunningLabel, "Number of threads running")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderPlayerTimecode, "Current Timecode")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeLabel, "Save As Start Point" & vbCrLf & "Left click to set at current timecode" & vbCrLf & "Right click to reset")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeLabel, "Save As End Point" & vbCrLf & "Left click to set at current timecode" & vbCrLf & "Right click to reset")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderTotalTimeLabel, "Save As File Length")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderInTimeGraphic, "In Point Marker")
            MainForm.MainToolTip.SetToolTip(MainForm.RenderOutTimeGraphic, "Out Point Marker")
            MainForm.MainToolTip.SetToolTip(MainForm.FFmpegOutput, "FFmpeg Output Console Window")
            'MainForm.MainToolTip.SetToolTip(MainForm.CurrentTimeList, "Hold 'CTRL' or 'SHIFT' to select multiple times")
            MainForm.MainToolTip.SetToolTip(MainForm.AppSettingsButton, "Application settings")

        End If

    End Sub

    Public Sub DriveEmpty()
        If My.Settings.UserLanguage = "Dutch" Then
            MessageBox.Show("Het CD- of DVD-station is leeg.", "Drive Info...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf My.Settings.UserLanguage = "Español" Then
            MessageBox.Show("El CD o DVD está vacío", "Drive Info...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf My.Settings.UserLanguage = "German" Then
            MessageBox.Show("Das CD oder DVD Laufwerk ist leer.", "Drive Info...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("The CD or DVD drive is empty.", "Drive Info...", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Public Sub LeftRepeater()
        If My.Settings.UserLanguage = "Dutch" Then
            'MainForm.GroupBoxLEFTREPEATER.Text = MainForm.GroupBoxLEFTREPEATER.Text.Replace("Left Repeater - ", "Linker Repeater - ")
            'MainForm.GroupBoxLEFTREPEATER.Text = MainForm.GroupBoxLEFTREPEATER.Text.Replace("ERROR", "Fout")
        ElseIf My.Settings.UserLanguage = "Español" Then
            'MainForm.GroupBoxLEFTREPEATER.Text = MainForm.GroupBoxLEFTREPEATER.Text.Replace("Left Repeater - ", "Repetidor de la izquierda - ")
        ElseIf My.Settings.UserLanguage = "German" Then
            'MainForm.GroupBoxLEFTREPEATER.Text = MainForm.GroupBoxLEFTREPEATER.Text.Replace("Left Repeater - ", "Linker Repeater - ")
            'MainForm.GroupBoxLEFTREPEATER.Text = MainForm.GroupBoxLEFTREPEATER.Text.Replace("ERROR", "FEHLER")
        End If
    End Sub
    Public Sub RightRepeater()
        If My.Settings.UserLanguage = "Dutch" Then
            'MainForm.GroupBoxRIGHTREPEATER.Text = MainForm.GroupBoxRIGHTREPEATER.Text.Replace("Right Repeater - ", "Rechter Repeater - ")
            'MainForm.GroupBoxRIGHTREPEATER.Text = MainForm.GroupBoxRIGHTREPEATER.Text.Replace("ERROR", "Fout")
        ElseIf My.Settings.UserLanguage = "Español" Then
            'MainForm.GroupBoxRIGHTREPEATER.Text = MainForm.GroupBoxRIGHTREPEATER.Text.Replace("Right Repeater - ", "Repetidor de la derecha - ")
        ElseIf My.Settings.UserLanguage = "German" Then
            'MainForm.GroupBoxRIGHTREPEATER.Text = MainForm.GroupBoxRIGHTREPEATER.Text.Replace("Right Repeater - ", "Rechter Repeater - ")
            'MainForm.GroupBoxRIGHTREPEATER.Text = MainForm.GroupBoxRIGHTREPEATER.Text.Replace("ERROR", "FEHLER")
        End If
    End Sub
    Public Sub FrontCamera()
        If My.Settings.UserLanguage = "Dutch" Then
            'MainForm.GroupBoxFRONT.Text = MainForm.GroupBoxFRONT.Text.Replace("Front - ", "Front - ")
            'MainForm.GroupBoxFRONT.Text = MainForm.GroupBoxFRONT.Text.Replace("ERROR", "Fout")
        ElseIf My.Settings.UserLanguage = "Español" Then
            'MainForm.GroupBoxFRONT.Text = MainForm.GroupBoxFRONT.Text.Replace("Front - ", "Delantero - ")
        ElseIf My.Settings.UserLanguage = "German" Then
            'MainForm.GroupBoxFRONT.Text = MainForm.GroupBoxFRONT.Text.Replace("Front - ", "Vorne - ")
            'MainForm.GroupBoxFRONT.Text = MainForm.GroupBoxFRONT.Text.Replace("ERROR", "FEHLER")
        End If
    End Sub
    Public Sub Preview()
        If My.Settings.UserLanguage = "Dutch" Then
            MainForm.PREVIEWBox.Text = MainForm.PREVIEWBox.Text.Replace("Preview Window - ", "Voorbeeldvenster - ")
            MainForm.PREVIEWBox.Text = MainForm.PREVIEWBox.Text.Replace("ERROR", "Fout")
        ElseIf My.Settings.UserLanguage = "Español" Then
            MainForm.PREVIEWBox.Text = MainForm.PREVIEWBox.Text.Replace("Preview Window - ", "Ventana de adelanto - ")
        ElseIf My.Settings.UserLanguage = "German" Then
            MainForm.PREVIEWBox.Text = MainForm.PREVIEWBox.Text.Replace("Preview Window - ", "Vorschaufenster - ")
            MainForm.PREVIEWBox.Text = MainForm.PREVIEWBox.Text.Replace("ERROR", "FEHLER")
        End If
    End Sub

    Public Sub UpdateCustomFolder(folderpath As String)
        If Directory.Exists(folderpath) Or folderpath = "Custom Folder" Then
            Dim FolderName As String = New DirectoryInfo(folderpath).Name
            If folderpath <> "Custom Folder" Then
                MainForm.AddImageToImgList(folderpath)
            End If
            Dim rootNode As New TreeNode("- " & FolderName & " -")
            If My.Settings.UserLanguage = "Dutch" Then
                rootNode.Name = "Aangepaste Map"
            ElseIf My.Settings.UserLanguage = "Español" Then
                rootNode.Name = "Carpeta personalizada"
            ElseIf My.Settings.UserLanguage = "German" Then
                rootNode.Name = "Benutzerdefinierter Ordner"
            End If

            Try
                With rootNode
                    If My.Settings.UserLanguage = "Dutch" Then
                        .ToolTipText = "Aangepaste Map - " & folderpath & " - Custom Folder"
                    ElseIf My.Settings.UserLanguage = "Español" Then
                        .ToolTipText = "Carpeta personalizada - " & folderpath & " - Custom Folder"
                    ElseIf My.Settings.UserLanguage = "German" Then
                        .ToolTipText = "Benutzerdefinierter Ordner - " & folderpath & " - Custom Folder"
                    Else
                        .ToolTipText = "Custom Folder - " & folderpath
                    End If
                    '.ToolTipText = "Custom Folder - " & folderpath
                    .Tag = folderpath
                    .ImageKey = folderpath
                    .SelectedImageKey = folderpath
                    If Directory.GetDirectories(folderpath).Count > 0 OrElse Directory.GetFiles(folderpath).Count > 0 Then
                        .Nodes.Add("Empty")
                    End If
                End With
            Catch ex As Exception
            End Try

            Try
                MainForm.Tv_Explorer.Nodes.Item(0).Remove()
            Catch ex As Exception
            End Try
            MainForm.Logging("Info - Custom Folder Added - " & folderpath)
            MainForm.Tv_Explorer.Nodes.Insert(0, rootNode)
        End If
    End Sub

    Public Sub CameraNotFound()
        If My.Settings.UserLanguage = "Dutch" Then
            MessageBox.Show("De camera die is geselecteerd, bestaat niet bij alle geselecteerde tijden", "Fout...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf My.Settings.UserLanguage = "Español" Then
            MessageBox.Show("La cámara seleccionada no existe entre los tiempos seleccionados", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf My.Settings.UserLanguage = "German" Then
            MessageBox.Show("Die ausgewählte Kamera ist nicht überall im ausgewählten Zeitraum verfügbar", "Fehler...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("The camera that was selected does not exist between all times that were selected", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        MainForm.Logging("Info - The camera that was selected does not exist between all times that were selected")
    End Sub

    Public Sub FFmpegStarting()
        If My.Settings.UserLanguage = "Dutch" Then
            MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ FFmpeg starten ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
        ElseIf My.Settings.UserLanguage = "Español" Then
            MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Iniciando FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
        ElseIf My.Settings.UserLanguage = "German" Then
            MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Starte FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
        Else
            MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Starting FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
        End If
    End Sub
    Public Sub FFmpegError()
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ FOUT bij het starten van FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ ERROR Iniciando FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ FEHLER beim Starten von FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ ERROR Starting FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ FOUT bij het starten van FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ ERROR Iniciando FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ FEHLER beim Starten von FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ ERROR Starting FFmpeg ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If
        End If
    End Sub
    Public Sub FFmpegFinished()
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Klaar met het converteren van video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Conversión de vídeo terminada ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Video-Konvertierung abgeschlossen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Finished converting video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Klaar met het converteren van video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Conversión de vídeo terminada ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Video-Konvertierung abgeschlossen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Finished converting video ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If
        End If

    End Sub
    Public Sub FFmpegAllDone()
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Alle bestanden zijn klaar met converteren ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Todos los archivos han terminado de convertir ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Alle Dateien wurden konvertiert ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ All files are done converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Alle bestanden zijn klaar met converteren ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Todos los archivos han terminado de convertir ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Alle Dateien wurden konvertiert ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ All files are done converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If
        End If

    End Sub
    Public Sub FFmpegRenderingAll()
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Alle clips nu aan het samen voegen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Procesando todos los clips ahora ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Rendern aller Clips zusammen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Rendering all clips together now ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Alle clips nu aan het samen voegen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Procesando todos los clips ahora ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Rendern aller Clips zusammen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Rendering all clips together now ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If
        End If
    End Sub
    Public Sub FFmpegWaiting()
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Wachten op " & MainForm.RenderFileCount & " bestand (en) om het converteren te beëindigen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Esperando por " & MainForm.RenderFileCount & " archivo(s) para terminar la conversión ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Warten bis " & MainForm.RenderFileCount & " Datei(en) zuende konvertiert sind ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & "~~~~~~~~~~~~~~ Waiting for " & MainForm.RenderFileCount & " file(s) to finish converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        Else

            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Wachten op " & MainForm.RenderFileCount & " bestand (en) om het converteren te beëindigen ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Esperando por " & MainForm.RenderFileCount & " archivo(s) para terminar la conversión ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Warten bis " & MainForm.RenderFileCount & " Datei(en) zuende konvertiert sind ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            Else
                MainForm.UpdateTextBox(vbCrLf & "~~~~~~~~~~~~~~ Waiting for " & MainForm.RenderFileCount & " file(s) to finish converting ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
            End If

        End If
    End Sub



    Public Sub RenderStatus()

        If MainForm.RenderFileCount = 0 And MainForm.RenderFileCountNotDone = 0 Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.RenderFileProgress.Text = "GEDAAN"
                MainForm.ThreadsRunningLabel.Text = "GEDAAN"
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.RenderFileProgress.Text = "Hecho"
                MainForm.ThreadsRunningLabel.Text = "Hecho"
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.RenderFileProgress.Text = "FERTIG"
                MainForm.ThreadsRunningLabel.Text = "FERTIG"
            Else
                MainForm.RenderFileProgress.Text = "DONE"
                MainForm.ThreadsRunningLabel.Text = "DONE"
            End If

            MainForm.DurationProgressBar.Value = MainForm.DurationProgressBar.Maximum
        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.RenderFileProgress.Text = MainForm.RenderFileCount & " van " & MainForm.RenderFileCountNotDone
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.RenderFileProgress.Text = MainForm.RenderFileCount & " de " & MainForm.RenderFileCountNotDone
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.RenderFileProgress.Text = MainForm.RenderFileCount & " von " & MainForm.RenderFileCountNotDone
            Else
                MainForm.RenderFileProgress.Text = MainForm.RenderFileCount & " of " & MainForm.RenderFileCountNotDone
            End If

        End If
        If My.Settings.UserLanguage = "Dutch" Then
            MainForm.ThreadsRunningLabel.Text = MainForm.RenderFileCount & " Running"
        ElseIf My.Settings.UserLanguage = "Español" Then
            MainForm.ThreadsRunningLabel.Text = MainForm.RenderFileCount & " Reproduciendo"
        ElseIf My.Settings.UserLanguage = "German" Then
            MainForm.ThreadsRunningLabel.Text = MainForm.RenderFileCount & " Arbeite"
        Else
            MainForm.ThreadsRunningLabel.Text = MainForm.RenderFileCount & " Running"
        End If



    End Sub
    Public Sub RenderDone()
        Dim StopTime As Date = Now
        Dim ts As TimeSpan = StopTime - MainForm.RenderingStartTime
        If MainForm.InvokeRequired = True Then
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Het " & MainForm.RenderedVideoTotalCount & " bestand (en) weergeven bij " & MainForm.RenderQuality.Text & " Kwaliteit duurde " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.Invoke(MainForm.myDelegate, "DONE")
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Procesando " & MainForm.RenderedVideoTotalCount & " archivo(s) en " & MainForm.RenderQuality.Text & " Calidad tomó " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.Invoke(MainForm.myDelegate, "Hecho")
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Rendern von " & MainForm.RenderedVideoTotalCount & " Datei(en) bei " & MainForm.RenderQuality.Text & " Qualität benötigte " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.Invoke(MainForm.myDelegate, "FERTIG")
            Else
                MainForm.Invoke(MainForm.myDelegate, vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Rendering " & MainForm.RenderedVideoTotalCount & " file(s) at " & MainForm.RenderQuality.Text & " Quality took " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.Invoke(MainForm.myDelegate, "DONE")
            End If

        Else
            If My.Settings.UserLanguage = "Dutch" Then
                MainForm.UpdateTextBox(vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Het " & MainForm.RenderedVideoTotalCount & " bestand (en) weergeven bij " & MainForm.RenderQuality.Text & " Kwaliteit duurde " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.UpdateTextBox("DONE")
            ElseIf My.Settings.UserLanguage = "Español" Then
                MainForm.UpdateTextBox(vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Procesando " & MainForm.RenderedVideoTotalCount & " archivo(s) en " & MainForm.RenderQuality.Text & " Calidad tomó " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.UpdateTextBox("Hecho")
            ElseIf My.Settings.UserLanguage = "German" Then
                MainForm.UpdateTextBox(vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Rendern von " & MainForm.RenderedVideoTotalCount & " Datei(en) bei " & MainForm.RenderQuality.Text & " Qualität benötigte " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.UpdateTextBox("FERTIG")
            Else
                MainForm.UpdateTextBox(vbCrLf & vbCrLf & "~~~~~~~~~~~~~~ Rendering " & MainForm.RenderedVideoTotalCount & " file(s) at " & MainForm.RenderQuality.Text & " Quality took " & ts.Hours.ToString.PadLeft(2, "0"c) & ":" & ts.Minutes.ToString.PadLeft(2, "0"c) & ":" & ts.Seconds.ToString.PadLeft(2, "0"c) & " ~~~~~~~~~~~~~~" & vbCrLf & vbCrLf)
                MainForm.UpdateTextBox("DONE")
            End If
        End If

    End Sub

    Public Sub UnableToDelete(ex)
        If My.Settings.UserLanguage = "Dutch" Then
            MessageBox.Show("Kan Temp Map niet verwijderen: " & vbCrLf & ex.Message, "Fout ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf My.Settings.UserLanguage = "Español" Then
            MessageBox.Show("No se pudo borrar la carpeta temporera: " & vbCrLf & ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf My.Settings.UserLanguage = "German" Then
            MessageBox.Show("Temp Ordner konnte nicht gelöscht werden: " & vbCrLf & ex.Message, "Fehler ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("Unable to delete temp folder: " & vbCrLf & ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        MainForm.Logging("Info - Unable to delete temp folder: " & vbCrLf & ex.Message)
    End Sub




    Public Sub NewDriveFound()
        Dim result As Integer
        result = MessageBox.Show("A new drive was detected, would you like to refresh the 'Explorer' window? ", "New drive detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If result = DialogResult.No Then
            MainForm.Logging("Info - Update Drives - No was selected")
        ElseIf result = DialogResult.Yes Then
            MainForm.Logging("Info - Update Drives - Yes was selected. Updateing Explorer window")
            MainForm.RefreshRootNodes()

        End If

    End Sub
    Public Sub DeleteFiles()
        Try

            Dim result As Integer


            If My.Settings.UserLanguage = "Dutch" Then
                result = MessageBox.Show("Weet je zeker dat je wilt verwijderen:" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Verwijder", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            ElseIf My.Settings.UserLanguage = "Español" Then
                result = MessageBox.Show("Estás seguro que quieres borrar:" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            ElseIf My.Settings.UserLanguage = "German" Then
                result = MessageBox.Show("Bist du sicher, dass du diese Datei löschen möchtest:" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            Else
                result = MessageBox.Show("Are you sure you want to delete:" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            End If

            If result = DialogResult.No Then
                'MessageBox.Show("No pressed")

            ElseIf result = DialogResult.Yes Then
                MainForm.Logging("Info - Delete - Yes was selected")
                Dim FileLocation = MainForm.Tv_Explorer.SelectedNode.Tag

                If FileLocation.ToString.ToLower = "c:\" Or FileLocation.ToString.ToLower.Contains("windows") = True Or FileLocation.ToString.ToLower.EndsWith(":\") = True Or
                    FileLocation.ToString.ToLower.EndsWith("\desktop") = True Or FileLocation.ToString.ToLower.EndsWith("\documents") = True Or
                    FileLocation.ToString.ToLower.EndsWith("\recent") = True Or FileLocation.ToString.ToLower.EndsWith("\pictures") Then
                    If My.Settings.UserLanguage = "Dutch" Then
                        MessageBox.Show("Kan bestand / map niet verwijderen: " & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "FOUT ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf My.Settings.UserLanguage = "Español" Then
                        MessageBox.Show("No se puede borrar Archivo/Carpeta: " & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf My.Settings.UserLanguage = "German" Then
                        MessageBox.Show("Datei/Ordner konnte nicht gelöscht werden: " & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Fehler ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("Unable to delete File/Folder: " & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    MainForm.Logging("Info - Unable to delete File/Folder: " & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag)

                Else
                    Dim Positive As Integer
                    If My.Settings.UserLanguage = "Dutch" Then
                        Positive = MessageBox.Show("Deze bestanden worden PERMANENT VERWIJDERD!" & vbCrLf & "Weet je zeker dat je ze wilt verwijderen?" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Verwijder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
                    ElseIf My.Settings.UserLanguage = "Español" Then
                        Positive = MessageBox.Show("¡Estos archivo se BORRARÁN PERMANENTEMENTE!" & vbCrLf & "¿Estás seguro que lo quieres borrar?" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
                    ElseIf My.Settings.UserLanguage = "German" Then
                        Positive = MessageBox.Show("Diese Dateien werden DAUERHAFT GELÖSCHT!" & vbCrLf & "Bist du sicher, dass du sie löschen möchtest?" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
                    Else
                        Positive = MessageBox.Show("These files will be PERMANENTLY DELETED!" & vbCrLf & "Are you sure you want to delete them?" & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Tag, "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
                    End If
                    If Positive = DialogResult.No Then
                        MainForm.Logging("Info - Delete2 - No was selected")
                    ElseIf Positive = DialogResult.Yes Then
                        Try
                            MainForm.Logging("Info - Delete - MainForm.Tv_Explorer.SelectedNode.Tag")
                            System.IO.Directory.Delete(MainForm.Tv_Explorer.SelectedNode.Tag, True)
                        Catch ex As Exception

                            Try
                                MainForm.Logging("Info - Delete - " & MainForm.Tv_Explorer.SelectedNode.Tag)

                                System.IO.File.Delete(MainForm.Tv_Explorer.SelectedNode.Tag)
                            Catch exx As Exception
                                If My.Settings.UserLanguage = "Dutch" Then
                                    MessageBox.Show("Kan bestand / map niet verwijderen: " & vbCrLf & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Text & vbCrLf & vbCrLf & exx.Message.ToString, "Fout ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ElseIf My.Settings.UserLanguage = "Español" Then
                                    MessageBox.Show("No se puede borrar Archivo/Carpeta: " & vbCrLf & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Text & vbCrLf & vbCrLf & exx.Message.ToString, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ElseIf My.Settings.UserLanguage = "German" Then
                                    MessageBox.Show("Datei/Ordner konnte nicht gelöscht werden: " & vbCrLf & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Text & vbCrLf & vbCrLf & exx.Message.ToString, "Fehler ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Else
                                    MessageBox.Show("Unable to delete File/Folder: " & vbCrLf & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Text & vbCrLf & vbCrLf & exx.Message.ToString, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                MainForm.Logging("Info - Unable to delete File/Folder: " & vbCrLf & vbCrLf & MainForm.Tv_Explorer.SelectedNode.Text & vbCrLf & vbCrLf & exx.Message.ToString)
                            End Try
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("Unable to delete temp folder: " & vbCrLf & ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub SaveAS(sfd)
        If My.Settings.UserLanguage = "Dutch" Then
            sfd.Title = "Video opslaan als ..."
            sfd.FileName = "Untitled"
        ElseIf My.Settings.UserLanguage = "Español" Then
            sfd.Title = "Grabar vídeo como..."
            sfd.FileName = "Sin Título"
        ElseIf My.Settings.UserLanguage = "German" Then
            sfd.Title = "Speichere Video als..."
            sfd.FileName = "Unbenannt"
        Else
            sfd.Title = "Save Video As..."
            sfd.FileName = "Untitled"
        End If
    End Sub




End Module
