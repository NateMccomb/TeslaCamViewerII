Imports System.ComponentModel

Public Class UserAgreement
    Dim CloseTeslaCamViewer As Boolean = True
    Private Sub AgreeBTN_Click(sender As Object, e As EventArgs) Handles AgreeBTN.Click
        MainForm.Enabled = True
        CloseTeslaCamViewer = False
        My.Settings.UserAgreed = True
        Me.Close()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.BringToFront()
    End Sub

    Private Sub UserAgreement_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If CloseTeslaCamViewer = True Then
            End
        End If
    End Sub

    Private Sub UserAgreement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Left = (MainForm.Left + MainForm.Width / 2) - (Me.Width / 2)
        Me.Top = (MainForm.Top + MainForm.Height / 2) - (Me.Height / 2)
        MainForm.Enabled = False
    End Sub

    Private Sub RichTextBox1_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox1.LinkClicked
        Process.Start(e.LinkText)
    End Sub
End Class