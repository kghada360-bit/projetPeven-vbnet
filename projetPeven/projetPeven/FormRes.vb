Imports System.IO
Imports System.Text.RegularExpressions
Public Class FormRes
    Dim responsables As String = "responsables.txt"

    Private Sub FormRes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        chargerResponsables()
    End Sub
    Private Sub chargerResponsables()
        dgvResponsables.Rows.Clear()
        If File.Exists(responsables) Then
            Dim ligne = File.ReadAllLines(responsables)
            For Each lign As String In ligne
                Dim donnees = lign.Split("#"c)
                dgvResponsables.Rows.Add(donnees)
            Next
        End If
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(TextBox1.Text) OrElse String.IsNullOrEmpty(TextBox2.Text) OrElse
         String.IsNullOrEmpty(TextBox3.Text) OrElse String.IsNullOrEmpty(TextBox4.Text) OrElse
         String.IsNullOrEmpty(TextBox5.Text) OrElse String.IsNullOrEmpty(TextBox6.Text) OrElse String.IsNullOrEmpty(ComboBox1.SelectedItem.ToString()) Then
            MessageBox.Show("Veuillez remplir tout les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim nom As String = TextBox1.Text
        Dim pnom As String = TextBox2.Text
        Dim dnaiss As String = TextBox3.Text
        Dim cin As String = TextBox4.Text
        Dim adrss As String = TextBox5.Text
        Dim tel As String = TextBox6.Text
        Dim fonct As String = ComboBox1.SelectedItem.ToString()
        If Not IsNumeric(cin) Then
            MessageBox.Show("CIN doit etre uniquement compose par des chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not IsNumeric(tel) Then
            MessageBox.Show("Le numero de telephone doit etre uniquement compose par des chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cin.Length <> 8 Then
            MessageBox.Show("Le CIN doit comporter 8 chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If tel.Length <> 8 Then
            MessageBox.Show("Le Numero de Tel doit comporter 8 chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim dateRegex As New Regex("^\d{2}/\d{2}/\d{4}$")
        If Not dateRegex.IsMatch(dnaiss) Then
            MessageBox.Show("Respectez le format jj/mm/aaaa ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not nom.All(Function(c) Char.IsLetter(c)) Then
            MessageBox.Show("Nom Invalide!! ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not pnom.All(Function(c) Char.IsLetter(c)) Then
            MessageBox.Show("Prenom Invalide!! ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        dgvResponsables.Rows.Add(nom, pnom, dnaiss, cin, adrss, tel, fonct)
        Dim ligne As String = nom & "#" & pnom & "#" & dnaiss & "#" & cin & "#" & adrss & "#" & tel & "#" & fonct
        File.AppendAllText("responsables.txt", ligne & Environment.NewLine)
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        ComboBox1.SelectedIndex = -1
        MessageBox.Show("Responsable ajoute avec succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim gerer1 As New Formgeres()
        Formgeres.ShowDialog()
    End Sub
End Class