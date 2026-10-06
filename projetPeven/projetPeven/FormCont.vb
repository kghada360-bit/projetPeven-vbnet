Imports System.IO
Public Class FormCont
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(TextBox2.Text) OrElse String.IsNullOrEmpty(TextBox3.Text) OrElse
           String.IsNullOrEmpty(TextBox1.Text) OrElse String.IsNullOrEmpty(TextBox6.Text) Then
            MessageBox.Show("Veuillez remplir tous les champs Obligatoire!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim mont As Decimal
        If Not Decimal.TryParse(TextBox4.Text, mont) Then
            MessageBox.Show("Le Montant saisie est invalide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not TextBox1.Text.Contains("@") OrElse Not TextBox1.Text.Contains(".") Then
            MessageBox.Show("Adresse email invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim nom As String = TextBox2.Text
        Dim mail As String = TextBox1.Text
        Dim tel As String = TextBox6.Text
        Dim respon As String = TextBox3.Text
        Dim detail As String = TextBox5.Text
        Dim type As String
        If RadioButton1.Checked Then
            type = "Personne"
            Dim LignePersonne As String = nom & "#" & respon & "#" & mail & "#" & tel & "#" & mont.ToString() & "#" & detail
            File.AppendAllText("donateur.txt", LignePersonne & Environment.NewLine)
            dgvcont.Rows.Add("Personne", nom, respon, mail, tel, mont.ToString("C"), detail)
        ElseIf RadioButton2.Checked Then
            If String.IsNullOrEmpty(TextBox3.Text) OrElse String.IsNullOrEmpty(TextBox5.Text) Then
                MessageBox.Show("Le Montant saisie est invalide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            type = "Campagne"
            Dim ligneCampagne As String = nom & "#" & mail & "#" & tel & "#" & mont.ToString() & "#" & respon & "#" & detail
            File.AppendAllText("campagne.txt", ligneCampagne & Environment.NewLine)
            dgvcont.Rows.Add("Campagne", nom, respon, mail, tel, mont.ToString("C"), detail)
        End If
        EffacerChamp()
        MessageBox.Show("Contact ajoute avec succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub EffacerChamp()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        RadioButton1.Checked = True
    End Sub

    Private Sub FormCont_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        chargerDonnees()
    End Sub
    Private Sub chargerDonnees()
        dgvcont.Rows.Clear()
        If File.Exists("personne.txt") Then
            Dim lignPer As String() = File.ReadAllLines("parsonne.txt")
            For Each ligne As String In lignPer
                Dim donnee As String() = ligne.Split("#"c)
                dgvcont.Rows.Add("Personne", donnee(0), donnee(1), donnee(2), donnee(3), donnee(4), donnee(5))

            Next
        End If
        If File.Exists("campagne.txt") Then
            Dim ligncom As String() = File.ReadAllLines("campagne.txt")
            For Each ligne As String In ligncom
                Dim donnee As String() = ligne.Split("#"c)
                dgvcont.Rows.Add("compagne", donnee(0), donnee(1), donnee(2), donnee(3), donnee(4), donnee(5))

            Next
        End If
    End Sub
End Class