Imports System.IO
Imports System.Text.RegularExpressions
Public Class FormFamille
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim famille As String = "familles.txt"
        If File.Exists(famille) Then
            Dim fs As New FileStream(famille, FileMode.Open, FileAccess.Read)
            Dim sr As New StreamReader(fs)
            Dim Ligne As String
            While sr.Peek() >= 0
                Ligne = sr.ReadLine()
                Dim donnees = Ligne.Split("#"c)
                DataGridView1.Rows.Add(donnees)
            End While
            sr.Close()
            fs.Close()
        Else
            MessageBox.Show("erreur,Fichier introuvable!!")

        End If


    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim famille As String = "familles.txt"
        Dim nom As String = TextBox1.Text
        Dim pnom As String = TextBox2.Text
        Dim dnaiss As String = TextBox3.Text
        Dim ncin As String = TextBox4.Text
        Dim adress As String = TextBox5.Text
        Dim tel As String = TextBox6.Text
        Dim his As String = TextBox7.Text
        If String.IsNullOrWhiteSpace(nom) OrElse String.IsNullOrWhiteSpace(pnom) OrElse String.IsNullOrWhiteSpace(dnaiss) OrElse String.IsNullOrWhiteSpace(ncin) OrElse String.IsNullOrWhiteSpace(adress) OrElse String.IsNullOrWhiteSpace(tel) OrElse String.IsNullOrWhiteSpace(his) Then
            MessageBox.Show("Veuillez remplir tout les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not IsNumeric(ncin) Then
            MessageBox.Show("CIN doit etre uniquement compose par des chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not IsNumeric(tel) Then
            MessageBox.Show("Le numero de telephone doit etre uniquement compose par des chiffre ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If ncin.Length <> 8 Then
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

        Dim nouvfamille As String = nom & "#" & pnom & "#" & dnaiss & "#" & ncin & "#" & adress & "#" & tel & "#" & his
        File.AppendAllText(famille, nouvfamille & vbCrLf)
        DataGridView1.Rows.Add(nom, pnom, dnaiss, ncin, adress, tel, his)
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()








    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim gerer As New Formgerer()
        Formgerer.ShowDialog()
    End Sub


End Class


