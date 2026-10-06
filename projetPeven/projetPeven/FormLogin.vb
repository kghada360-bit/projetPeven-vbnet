Imports System.IO
Public Class FormLogin

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text
        If authenticateUser(username, password) Then
            MessageBox.Show("Connexion réussie!")
            Me.Hide()
            FormAccueil.Show()
        Else
            MessageBox.Show("Identifiants incorrects!")

        End If

    End Sub
    Private Function authenticateUser(username As String, password As String) As Boolean
        For Each line As String In File.ReadAllLines("users.txt")
            Dim parts() As String = line.Split("#"c)
            If parts(0) = username AndAlso parts(1) = password Then
                Return True
            End If
        Next
        Return False
    End Function

    
    Private Sub FormLogin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"c
    End Sub
End Class