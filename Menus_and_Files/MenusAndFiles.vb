' Author: Keith Smith
' Date: 15 October 2018

Option Explicit On
Option Strict On

Imports System.IO

Public Class MenusAndFiles
    ' Object declared but not instantiated
    Dim AccountStreamWriter As StreamWriter

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
                ' Try to open the file and overwrite
                ' AccountStreamWriter = New StreamWriter(FileNameString)
                ' Try to open the file and append
                AccountStreamWriter = New StreamWriter(FileNameString, True)

                ' Make sure textboxes are cleared
                ClearTextBoxes()

                ' Enable form elements
                AccountNumberTextBox.Enabled = True
                FirstNameTextBox.Enabled = True
                LastNameTextBox.Enabled = True
                BalanceTextBox.Enabled = True
                AddAccountButton.Enabled = True

                ' Disable close dialogue
                CloseToolStripMenuItem.Enabled = True
            Catch FEx As FormatException
                MessageBox.Show("Format Exception",
                                "Format Exception",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            Catch IOEx As IOException
                ' Permission, out of space, etc. might be reasons an IO exception could be thrown
                MessageBox.Show("IO Exception",
                                "IO Exception",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            End Try
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        CloseFile()

        ' Disable form elements
        AccountNumberTextBox.Enabled = False
        FirstNameTextBox.Enabled = False
        LastNameTextBox.Enabled = False
        BalanceTextBox.Enabled = False
        AddAccountButton.Enabled = False

        ' Disable close dialogue
        CloseToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        ' Close application
        Application.Exit()
    End Sub

    Private Sub AddAccountButton_Click(sender As Object, e As EventArgs) Handles AddAccountButton.Click
        If AccountNumberTextBox.Text <> "" And FirstNameTextBox.Text <> "" And LastNameTextBox.Text <> "" And BalanceTextBox.Text <> "" Then
            Try
                Dim RecordString As String

                ' Create the record
                RecordString = String.Format("{0},{1},{2},{3}{4}",
                                             AccountNumberTextBox.Text,
                                             FirstNameTextBox.Text,
                                             LastNameTextBox.Text,
                                             BalanceTextBox.Text,
                                             vbCrLf)

                ' Write out to file
                AccountStreamWriter.Write(RecordString)

                ' Reset the form
                ClearTextBoxes()
            Catch FEx As FormatException
                MessageBox.Show("Format Exception",
                                "Format Exception",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            Catch IOEx As IOException
                MessageBox.Show("IO Exception",
                    "IO Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
            End Try
        Else
            MessageBox.Show("Must enter data for all fields",
                            "Missing Data Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        End If
 
    End Sub

    Private Sub ClearTextBoxes()
        AccountNumberTextBox.Clear()
        FirstNameTextBox.Clear()
        LastNameTextBox.Clear()
        BalanceTextBox.Clear()
    End Sub

    Private Sub CloseFile()
        If AccountStreamWriter IsNot Nothing Then
            Try
                ' Close file
                AccountStreamWriter.Close()
            Catch IOEx As IOException
                MessageBox.Show("Error Closing File",
                                "File Close Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            End Try
        End If
    End Sub

End Class
