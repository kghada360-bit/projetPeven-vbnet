Imports System.IO
Public Class FormComptable

    Private Sub FormComptable_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        chargerdetcot()
        DateTimePicker1.Value = DateTime.Now
    End Sub
    Private Sub chargerdetcot()
        dgvcompt.Rows.Clear()
        If File.Exists("cotisations.txt") Then
            Dim ligncot As String() = File.ReadAllLines("cotisations.txt")
            For Each ligne As String In ligncot
                Dim donnee As String() = ligne.Split("#"c)
                If donnee.Length >= 4 Then
                    dgvcompt.Rows.Add("Cotisations", donnee(0), donnee(1), donnee(2), donnee(3), donnee(4))
                End If
            Next
        End If
        If File.Exists("donss.txt") Then
            Dim ligndon As String() = File.ReadAllLines("donss.txt")
            For Each ligne As String In ligndon
                Dim donnee As String() = ligne.Split("#"c)
                If donnee.Length >= 5 Then
                    dgvcompt.Rows.Add("Don", donnee(0), donnee(1), donnee(2), donnee(3), donnee(4))
                End If


            Next
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If String.IsNullOrEmpty(TextBox1.Text) OrElse
        String.IsNullOrEmpty(DateTimePicker1.Text) OrElse
        String.IsNullOrEmpty(TextBox3.Text) OrElse
            String.IsNullOrEmpty(TextBox4.Text) OrElse
            String.IsNullOrEmpty(TextBox5.Text) Then
            MessageBox.Show("Veuillez remplir tous les champs!!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim nom As String = TextBox5.Text()
        Dim mont As Decimal
        If Not Decimal.TryParse(TextBox1.Text, mont) Then
            MessageBox.Show("Le Montant saisie est invalide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim dateOP As String = DateTimePicker1.Value.ToString("dd/MM/yyyy")
        Dim typeOp As String
        Dim pnom As String = TextBox4.Text
        Dim commentaire As String = TextBox3.Text
        If RadioButton1.Checked Then
            Dim LigneCot As String = nom & "#" & pnom & "#" & mont.ToString() & "#" & dateOP & "#" & commentaire
            File.AppendAllText("cotisations.txt", LigneCot & Environment.NewLine)
            dgvcompt.Rows.Add("Cotisations", nom, pnom, mont.ToString("C"), dateOP, commentaire)
        ElseIf RadioButton2.Checked Then
            Dim Lignedon As String = nom & "#" & pnom & "#" & mont.ToString() & "#" & dateOP & "#" & commentaire
            File.AppendAllText("donss.txt", Lignedon & Environment.NewLine)
            dgvcompt.Rows.Add("Donation", nom, pnom, mont.ToString("C"), dateOP, commentaire)
        Else
            MessageBox.Show("Selectionnez un type d'operation!!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim totcotisation As Decimal = 0
        Dim totDon As Decimal = 0
        For Each row As DataGridViewRow In dgvcompt.Rows
            If Not row.IsNewRow Then
                Dim typeOp As String = row.Cells(0).Value.ToString()
                Dim mont As Decimal = Convert.ToDecimal(row.Cells(3).Value)
                If typeOp = "Cotisations" Then
                    totcotisation += mont
                ElseIf typeOp = "Don" Then
                    totDon += mont
                End If
            End If
        Next
        MessageBox.Show("Total Cotisations:" & totcotisation.ToString() & "DT" & Environment.NewLine &
                        "Total Dons:" & totDon.ToString() & "DT",
                        "Resume Comptable", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
End Class