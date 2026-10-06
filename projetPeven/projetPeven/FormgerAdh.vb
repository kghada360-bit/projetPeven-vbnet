Imports System.IO
Public Class FormgerAdh

    Dim adherent As String = "adherent.txt"
    Private Sub FormgerAdh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        chargerAdherent()
    End Sub
    Private Sub chargerAdherent()
        dgvAdh.Rows.Clear()
        If File.Exists(adherent) Then
            Dim ligne = File.ReadAllLines(adherent)
            For Each lign As String In ligne
                Dim donnees = lign.Split("#"c)
                dgvAdh.Rows.Add(donnees)
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If dgvAdh.SelectedRows.Count > 0 Then
            Dim ligneselect As DataGridViewRow = dgvAdh.SelectedRows(0)
            Dim cin As String = ligneselect.Cells(3).Value.ToString()
            If MessageBox.Show("Etes_vous sur de supprimer ce Adherent ?", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning) Then
                dgvAdh.Rows.Remove(ligneselect)
                Supprimer(cin)
            End If
        Else
            MessageBox.Show("Selectionner l'Adherent vous choisissez supprimer ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub
    Private Sub Supprimer(cin As String)
        Dim ligne As List(Of String) = File.ReadAllLines(adherent).ToList()
        For Each lign As String In ligne
            Dim donnees = lign.Split("#"c)
            If donnees(3).ToLower() = cin.ToLower() Then
                ligne.Remove(lign)
                Exit For
            End If
        Next
        File.WriteAllLines(adherent, ligne)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim recherche As String = TextBox1.Text.ToLower()
        dgvAdh.Rows.Clear()
        If File.Exists(adherent) Then
            Dim ligne = File.ReadAllLines(adherent)
            For Each lign As String In ligne
                Dim donnees = lign.Split("#"c)
                If donnees(3).ToLower().Contains(recherche) Then
                    dgvAdh.Rows.Add(donnees)
                End If
            Next
        End If
        If dgvAdh.Rows.Count = 0 Then
            MessageBox.Show("Aucun Adherent trouvée avec ce NCIN!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If dgvAdh.SelectedRows.Count > 0 Then
            Dim ligne As DataGridViewRow = dgvAdh.SelectedRows(0)
            Dim cin As String = ligne.Cells(3).Value.ToString()

            Dim nouveauNom As String = InputBox("Entrez le nouveau Nom:", "Modifier Adherent", ligne.Cells(0).Value.ToString())
            Dim nouveauPnom As String = InputBox("Entrez le nouveau Prenom:", "Modifier Adherent", ligne.Cells(1).Value.ToString())
            Dim nouvelleDnaiss As String = InputBox("Entrez la nouvelle Date de naissance:", "Modifier Adherent", ligne.Cells(2).Value.ToString())
            Dim nouveauTel As String = InputBox("Entrez le nouveau Tel:", "Modifier Adherent", ligne.Cells(5).Value.ToString())
            Dim nouveauAdresse As String = InputBox("Entrez la nouvelle Adresse:", "Modifier Adherent", ligne.Cells(4).Value.ToString())
            Dim nouvelFontion As String = InputBox("Entrez la nouvel Fonction :", "Modifier Adherent", ligne.Cells(6).Value.ToString())

            If String.IsNullOrEmpty(nouveauNom) OrElse String.IsNullOrEmpty(nouveauPnom) OrElse String.IsNullOrEmpty(nouvelleDnaiss) OrElse
             String.IsNullOrEmpty(nouveauTel) OrElse String.IsNullOrEmpty(nouveauAdresse) OrElse String.IsNullOrEmpty(nouvelFontion) Then
                MessageBox.Show("Tous les champs doivent étre remplis.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ligne.Cells(0).Value = nouveauNom
            ligne.Cells(1).Value = nouveauPnom
            ligne.Cells(2).Value = nouvelleDnaiss
            ligne.Cells(4).Value = nouveauAdresse
            ligne.Cells(5).Value = nouveauTel
            ligne.Cells(6).Value = nouvelFontion

            MettreAjourFichier(adherent)

            MessageBox.Show("Les informations ont ete modifiees.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Selectionner l'Adherent a modifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)



        End If
    End Sub
    Private Sub MettreAjourFichier(fichier As String)
        Dim lignes As List(Of String) = File.ReadAllLines(fichier).ToList()
        Dim cin As String = dgvAdh.SelectedRows(0).Cells(3).Value.ToString()
        For i As Integer = 0 To lignes.Count - 1
            Dim donnees = lignes(i).Split("#"c)
            If donnees(3) = cin Then
                lignes(i) = String.Join("#", dgvAdh.SelectedRows(0).Cells.Cast(Of DataGridViewCell).Select(Function(c) c.Value.ToString()).ToArray())


            End If

        Next
    End Sub


End Class