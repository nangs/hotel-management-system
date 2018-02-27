﻿Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmDeductionEntryRecord
    Private Const STR_SelectWorkingdateAsEntryDateEmployeeRegistration As String = "select (workingdate) as [Entry Date], (EmployeeRegistration.EmployeeID) as [Employee ID],(EmployeeName) as [EmployeeName],(deduction) as [Deduction] from Advanceentry,employeeRegistration where EmployeeRegistration.EmployeeID=AdvanceEntry.EmployeeID and  deduction > 0 and  Employeename = '"









    


    

   


    Private Sub DeductionEntryRecord_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        frmMainMenu.Show()
    End Sub
    Sub fillcombo()

        Try
           
            Dim CN As New oledbConnection(cs)

            CN.Open()
            adp = New oledbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (employeename) FROM Advanceentry,employeeRegistration where EmployeeRegistration.EmployeeID=AdvanceEntry.EmployeeID", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            EmployeeName.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                EmployeeName.Items.Add(drow(0).ToString())
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeductionEntryRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
    End Sub

    Private Sub EmployeeName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeName.SelectedIndexChanged
        Try

             con = New oledbConnection(cs)
            con.Open()
            cmd = New OleDbCommand(STR_SelectWorkingdateAsEntryDateEmployeeRegistration & EmployeeName.Text & "'order by workingdate", con)
            Dim myDA As oledbDataAdapter = New oledbDataAdapter(cmd)
            Dim myDataSet As DataSet = New DataSet()
            myDA.Fill(myDataSet, "AdvanceEntry")
            myDA.Fill(myDataSet, "EmployeeRegistration")
            DataGridView1.DataSource = myDataSet.Tables("AdvanceEntry").DefaultView
            DataGridView1.DataSource = myDataSet.Tables("EmployeeRegistration").DefaultView
            Dim sum As Double = 0
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(3).Value
            Next
            TotalDeduction.Text = sum

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    
    

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Close()
    End Sub
End Class