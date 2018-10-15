' Author: Keith Smith
' Date: 15 October 2018

Option Explicit On
Option Strict On

Public Class MenusAndFiles
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim FileDialogResult As DialogResult ' Did they click save/cancel
        Dim FileNameString As String ' Stores name of file to work with

        ' Display dialogue to choose a file
        ' Use New because SaveFileDialog is a complete object
        Using FileSaveFileDialog As New SaveFileDialog
            ' Object is created but does not display until .ShowDialog() is called
            FileDialogResult = FileSaveFileDialog.ShowDialog()
            ' Also need name of file saved
            FileNameString = FileSaveFileDialog.FileName
        End Using

        If FileDialogResult <> Windows.Forms.DialogResult.Cancel Then
            ' Means user clicked save
            ' Try to open file for writing
            Try

            Catch FEx As FormatException
            Catch IOEx As System.IO.IOException

            End Try

            ' Enable add account button
            AddAccountButton.Enabled = True
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        ' Disable add account button
        AddAccountButton.Enabled = False
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        ' Close application
        Me.Close()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' Close application
        Application.Exit()
    End Sub


End Class
