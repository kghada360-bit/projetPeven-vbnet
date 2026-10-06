Imports System.IO
Public Class FormDons

    Private Sub FormDons_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dgvDons.Rows.Clear()
        Dim Dons As String = "dons.txt"
        If File.Exists(Dons) Then
            Dim lignes As String() = File.ReadAllLines(Dons)
            For Each ligne As String In lignes
                Dim donnees As String() = ligne.Split("#"c)
                If donnees.Length >= 5 Then
                    dgvDons.Rows.Add(donnees(0), donnees(1), donnees(2), donnees(3), donnees(4), donnees(5))
                End If
            Next
        Else
            MessageBox.Show("Le fichier 'dons.txt' n'existe pas.Veuillez le creer ou ajouter un don", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(TextBox1.Text) OrElse String.IsNullOrEmpty(ComboBox1.SelectedItem.ToString()) OrElse
         String.IsNullOrEmpty(TextBox2.Text) OrElse String.IsNullOrEmpty(DateTimePicker1.Text) OrElse String.IsNullOrEmpty(TextBox3.Text) OrElse String.IsNullOrEmpty(TextBox4.Text) Then
            MessageBox.Show("Veuillez remplir tout les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim mont As Decimal
        If Not Decimal.TryParse(TextBox2.Text, mont) Then
            MessageBox.Show("Le Montant saisie est invalide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim nom As String = TextBox1.Text
        Dim type As String = ComboBox1.SelectedItem.ToString()
        Dim description As String = TextBox3.Text
        Dim datedon As String = DateTimePicker1.Value.ToString("dd/MM/yyyy")
        Dim comm As String = TextBox4.Text

        dgvDons.Rows.Add(nom, type, mont, description, datedon, comm)
        Dim lignedon As String = nom & "#" & type & "#" & mont.ToString() & "#" & description & "#" & datedon & comm

        File.AppendAllText("dons.txt", lignedon & Environment.NewLine)
        TextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        DateTimePicker1.Value = DateTime.Now
        MessageBox.Show("Don ajoute avec succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If dgvDons.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgvDons.SelectedRows(0)

            Dim nom As String = selectedRow.Cells(0).Value.ToString()
            Dim type As String = selectedRow.Cells(1).Value.ToString()
            Dim mont As Decimal = selectedRow.Cells(2).Value.ToString()
            Dim description As String = selectedRow.Cells(3).Value.ToString()
            Dim datedon As String = selectedRow.Cells(4).Value.ToString()
            Dim comm As String = selectedRow.Cells(5).Value.ToString()

            nom = InputBox("Entrez le nouveau Nom:", "Modifier le Don", nom)
            type = InputBox("Entrez le nouveau Type:", "Modifier le Don", type)
            mont = InputBox("Entrez le nouveau Montant:", "Modifier le Don", mont)
            description = InputBox("Entrez la nouvelle Description:", "Modifier le Don", description)
            datedon = InputBox("Entrez la nouvelle Date:", "Modifier le Don", datedon)
            comm = InputBox("Entrez le nouveau Commentaire:", "Modifier le Don", comm)

            selectedRow.Cells(0).Value = nom
            selectedRow.Cells(1).Value = type
            selectedRow.Cells(2).Value = mont
            selectedRow.Cells(3).Value = description
            selectedRow.Cells(4).Value = datedon
            selectedRow.Cells(5).Value = comm
            Dim donlist As List(Of String) = File.ReadAllLines("dons.txt").ToList()
            For i As Integer = 0 To donlist.Count - 1
                Dim ligne As String = donlist(i)
                Dim donData As String() = ligne.Split("#"c)
                If donData(0) = nom And donData(4) = datedon Then
                    donlist(i) = nom & "#" & type & "#" & mont.ToString() & "#" & description & "#" & datedon & "#" & comm
                    Exit For
                End If
            Next
            File.WriteAllLines("dons.txt", donlist)
            MessageBox.Show("Le Don a ete modifie avec succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Selectionner un don a modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If dgvDons.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgvDons.SelectedRows(0)

            Dim nom As String = selectedRow.Cells(0).Value.ToString()
            Dim type As String = selectedRow.Cells(1).Value.ToString()
            Dim mont As Decimal = selectedRow.Cells(2).Value.ToString()
            Dim description As String = selectedRow.Cells(3).Value.ToString()
            Dim datedon As String = selectedRow.Cells(4).Value.ToString()
            Dim comm As String = selectedRow.Cells(5).Value.ToString()

            Dim result As DialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer ce don?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                dgvDons.Rows.Remove(selectedRow)
                Dim donlist As List(Of String) = File.ReadAllLines("dons.txt").ToList()
                For i As Integer = 0 To donlist.Count - 1
                    Dim ligne As String = donlist(i)
                    Dim dondata As String() = ligne.Split("#"c)
                    If dondata(0) = nom And dondata(4) = datedon Then
                        donlist.RemoveAt(i)
                        Exit For
                    End If

                Next
                File.WriteAllLines("dons.txt", donlist)
                MessageBox.Show("Le Don a ete supprime avec succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Selectionner un don a supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub dgvDons_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDons.CellContentClick

    End Sub
End Class