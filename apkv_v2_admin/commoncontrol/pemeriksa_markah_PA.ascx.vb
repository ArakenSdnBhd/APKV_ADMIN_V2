﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class pemeriksa_markah_PA_
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            ''--ALL
            'ddlTahun.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY SemesterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()
        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.PelajarID,  kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_pelajar_markah.A_Teori1"
        tmpSQL += " FROM  kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' and kpmkv_pelajar.StatusID='2'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
            Exit Sub
        End If


        '--count no of modul
        Dim nCount As Integer = 0
        strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
        strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
        strSQL += " AND kpmkv_modul.KursusID='" & ddlKodKursus.SelectedValue & "'"
        nCount = oCommon.getFieldValueInt(strSQL)

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strAmali1 As TextBox = datRespondent.Rows(i).FindControl("txtTeori1")

            'assign value to integer
            Dim strAmali As String = strAmali1.Text
            strRet = oCommon.ExecuteSQL(strSQL)

            If nCount = 2 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & oCommon.FixSingleQuotes(chkSesi.Text) & "' AND Semester='" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "' AND Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"""
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 3 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 4 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 5 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 6 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori6='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 7 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori6='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori7='" & oCommon.FixSingleQuotes(strAmali) & "'"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf nCount = 8 Then
                strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori6='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori7='" & oCommon.FixSingleQuotes(strAmali) & "' ,A_Teori8='" & oCommon.FixSingleQuotes(strAmali) & "',"
                strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            If Not strRet = "0" Then
                divMsgResult.Attributes("class") = "error"
                lblMsgResult.Text = "Tidak Berjaya mengemaskini markah Pentaksiran Akhir Vokasional"
            End If
        Next

        divMsgResult.Attributes("class") = "info"
        lblMsgResult.Text = "Berjaya mengemaskini markah Pentaksiran Akhir Vokasional"
        strRet = BindData((datRespondent))
    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strAmali1 As TextBox = CType(row.FindControl("txtTeori1"), TextBox)
           
            '--validate NUMBER and less than 100
            '--amali1
            If Not strAmali1.Text.Length = 0 Then
                If oCommon.IsCurrency(strAmali1.Text) = False Then
                    Return False
                End If
                If CInt(strAmali1.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali1.Text = "0"
            End If

         
        Next

        Return True
    End Function
    Private Sub Vokasional_markah()

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%01%' "
        Dim strModul1 As String = oCommon.getFieldValue(strSQL) '1

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%02%' "
        Dim strModul2 As String = oCommon.getFieldValue(strSQL) '2

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%03%' "
        Dim strModul3 As String = oCommon.getFieldValue(strSQL) '3

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%04%' "
        Dim strModul4 As String = oCommon.getFieldValue(strSQL) '4

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%05%' "
        Dim strModul5 As String = oCommon.getFieldValue(strSQL) '5

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%06%' "
        Dim strModul6 As String = oCommon.getFieldValue(strSQL) '6


        For i As Integer = 0 To datRespondent.Rows.Count - 1


            Dim PBAmali1 As Integer
            Dim PBTeori1 As Integer
            Dim PAAmali1 As Integer
            Dim PATeori1 As Integer

            Dim B_Amali1 As Double
            Dim B_Teori1 As Double
            Dim A_Amali1 As Double
            Dim A_Teori1 As Double
            Dim PBM1 As Integer
            Dim PAM1 As Integer
            Dim PointerM1 As Integer

            'B_Amali1, B_Amali2, B_Amali3,B_Amali4, B_Amali5, B_Amali6, B_Amali7, B_Amali8, 
            'B_Teori1, B_Teori2, B_Teori3, B_Teori4, B_Teori5, B_Teori6, B_Teori7, B_Teori8, 
            'A_Amali1, A_Amali2, A_Amali3, A_Amali4, A_Amali5, A_Amali6, A_Amali7, A_Amali8,
            'A_Teori1, A_Teori2, A_Teori3, A_Teori4, A_Teori5, A_Teori6, A_Teori7, A_Teori8,
            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBAmali1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBTeori1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PAAmali1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PATeori1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Amali1 = Math.Ceiling(B_Amali1)

            strSQL = "Select B_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_Teori1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Teori1 = Math.Ceiling(B_Teori1)

            strSQL = "Select A_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            A_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            A_Amali1 = Math.Ceiling(A_Amali1)

            strSQL = "Select A_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            A_Teori1 = oCommon.getFieldValueInt(strSQL)
            'round up
            A_Teori1 = Math.Ceiling(A_Teori1)

            'convert 0 if null
            If (String.IsNullOrEmpty(B_Amali1.ToString())) Then
                B_Amali1 = 0
            End If

            If (String.IsNullOrEmpty(B_Teori1.ToString())) Then
                B_Teori1 = 0
            End If

            If (String.IsNullOrEmpty(A_Amali1.ToString())) Then
                A_Amali1 = 0
            End If

            If (String.IsNullOrEmpty(A_Teori1.ToString())) Then
                A_Teori1 = 0
            End If

            If B_Amali1 = "-1" Or B_Teori1 = "-1" Or A_Amali1 = "-1" Or A_Teori1 = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                PBM1 = Math.Ceiling(((B_Amali1 / 100) * PBAmali1) + ((B_Teori1 / 100) * PBTeori1))
                PAM1 = Math.Ceiling(((A_Amali1 / 100) * PAAmali1) + ((A_Teori1 / 100) * PATeori1))

                PointerM1 = Math.Ceiling(PBM1 + PAM1)
                strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='" & PointerM1 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            'Modu1------------------------
            If Not String.IsNullOrEmpty(strModul2) Then
                Dim PBAmali2 As Integer
                Dim PBTeori2 As Integer
                Dim PAAmali2 As Integer
                Dim PATeori2 As Integer

                Dim B_Amali2 As Double
                Dim B_Teori2 As Double
                Dim A_Amali2 As Double
                Dim A_Teori2 As Double
                Dim PBM2 As Integer
                Dim PAM2 As Integer
                Dim PointerM2 As Integer

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali2 = Math.Ceiling(B_Amali2)

                strSQL = "Select B_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Teori2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori2 = Math.Ceiling(B_Teori2)

                strSQL = "Select A_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali2 = Math.Ceiling(A_Amali2)

                strSQL = "Select A_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Teori2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori2 = Math.Ceiling(A_Teori2)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali2.ToString())) Then
                    B_Amali2 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori2.ToString())) Then
                    B_Teori2 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali2.ToString())) Then
                    A_Amali2 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori2.ToString())) Then
                    A_Teori2 = 0
                End If

                If B_Amali2 = "-1" Or B_Teori2 = "-1" Or A_Amali2 = "-1" Or A_Teori2 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM2='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Else
                    PBM2 = Math.Ceiling(((B_Amali2 / 100) * PBAmali2) + ((B_Teori2 / 100) * PBTeori2))
                    PAM2 = Math.Ceiling(((A_Amali2 / 100) * PAAmali2) + ((A_Teori2 / 100) * PATeori2))

                    PointerM2 = Math.Ceiling(PBM2 + PAM2)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM2='" & PointerM2 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
            'Modul2---------------------------------
            If Not String.IsNullOrEmpty(strModul3) Then
                Dim PBAmali3 As Integer
                Dim PBTeori3 As Integer
                Dim PAAmali3 As Integer
                Dim PATeori3 As Integer

                Dim B_Amali3 As Double
                Dim B_Teori3 As Double
                Dim A_Amali3 As Double
                Dim A_Teori3 As Double
                Dim PBM3 As Integer
                Dim PAM3 As Integer
                Dim PointerM3 As Integer

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali3 = Math.Ceiling(B_Amali3)

                strSQL = "Select B_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Teori3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori3 = Math.Ceiling(B_Teori3)

                strSQL = "Select A_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali3 = Math.Ceiling(A_Amali3)

                strSQL = "Select A_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Teori3 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori3 = Math.Ceiling(A_Teori3)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali3.ToString())) Then
                    B_Amali3 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori3.ToString())) Then
                    B_Teori3 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali3.ToString())) Then
                    A_Amali3 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori3.ToString())) Then
                    A_Teori3 = 0
                End If

                If B_Amali3 = "-1" Or B_Teori3 = "-1" Or A_Amali3 = "-1" Or A_Teori3 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM3='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else

                    PBM3 = Math.Ceiling(((B_Amali3 / 100) * PBAmali3) + ((B_Teori3 / 100) * PBTeori3))
                    PAM3 = Math.Ceiling(((A_Amali3 / 100) * PAAmali3) + ((A_Teori3 / 100) * PATeori3))

                    PointerM3 = Math.Ceiling(PBM3 + PAM3)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM3='" & PointerM3 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
            'Modul3---------------------------------
            If Not String.IsNullOrEmpty(strModul4) Then
                Dim PBAmali4 As Integer
                Dim PBTeori4 As Integer
                Dim PAAmali4 As Integer
                Dim PATeori4 As Integer

                Dim B_Amali4 As Double
                Dim B_Teori4 As Double
                Dim A_Amali4 As Double
                Dim A_Teori4 As Double
                Dim PBM4 As Integer
                Dim PAM4 As Integer
                Dim PointerM4 As Integer

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali4 = Math.Ceiling(B_Amali4)

                strSQL = "Select B_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Teori4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori4 = Math.Ceiling(B_Teori4)

                strSQL = "Select A_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali4 = Math.Ceiling(A_Amali4)

                strSQL = "Select A_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Teori4 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori4 = Math.Ceiling(A_Teori4)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali4.ToString())) Then
                    B_Amali4 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori4.ToString())) Then
                    B_Teori4 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali4.ToString())) Then
                    A_Amali4 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori4.ToString())) Then
                    A_Teori4 = 0
                End If

                If B_Amali4 = "-1" Or B_Teori4 = "-1" Or A_Amali4 = "-1" Or A_Teori4 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM4='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    PBM4 = Math.Ceiling(((B_Amali4 / 100) * PBAmali4) + ((B_Teori4 / 100) * PBTeori4))
                    PAM4 = Math.Ceiling(((A_Amali4 / 100) * PAAmali4) + ((A_Teori4 / 100) * PATeori4))

                    PointerM4 = Math.Ceiling(PBM4 + PAM4)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM4='" & PointerM4 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
            'Modul4---------------------------------
            If Not String.IsNullOrEmpty(strModul5) Then

                Dim PBAmali5 As Integer
                Dim PBTeori5 As Integer
                Dim PAAmali5 As Integer
                Dim PATeori5 As Integer

                Dim B_Amali5 As Double
                Dim B_Teori5 As Double
                Dim A_Amali5 As Double
                Dim A_Teori5 As Double
                Dim PBM5 As Integer
                Dim PAM5 As Integer
                Dim PointerM5 As Integer

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali5 = Math.Ceiling(B_Amali5)

                strSQL = "Select B_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Teori5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori5 = Math.Ceiling(B_Teori5)

                strSQL = "Select A_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali5 = Math.Ceiling(A_Amali5)

                strSQL = "Select A_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Teori5 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori5 = Math.Ceiling(A_Teori5)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali5.ToString())) Then
                    B_Amali5 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori5.ToString())) Then
                    B_Teori5 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali5.ToString())) Then
                    A_Amali5 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori5.ToString())) Then
                    A_Teori5 = 0
                End If

                If B_Amali5 = "-1" Or B_Teori5 = "-1" Or A_Amali5 = "-1" Or A_Teori5 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM5='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    PBM5 = Math.Ceiling(((B_Amali5 / 100) * PBAmali5) + ((B_Teori5 / 100) * PBTeori5))
                    PAM5 = Math.Ceiling(((A_Amali5 / 100) * PAAmali5) + ((A_Teori5 / 100) * PATeori5))

                    PointerM5 = Math.Ceiling(PBM5 + PAM5)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM5='" & PointerM5 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
            'Modul6---------------------------------
            If Not String.IsNullOrEmpty(strModul6) Then

                Dim PBAmali6 As Integer
                Dim PBTeori6 As Integer
                Dim PAAmali6 As Integer
                Dim PATeori6 As Integer

                Dim B_Amali6 As Double
                Dim B_Teori6 As Double
                Dim A_Amali6 As Double
                Dim A_Teori6 As Double
                Dim PBM6 As Integer
                Dim PAM6 As Integer
                Dim PointerM6 As Integer

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali6 = Math.Ceiling(B_Amali6)

                strSQL = "Select B_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_Teori6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori6 = Math.Ceiling(B_Teori6)

                strSQL = "Select A_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali6 = Math.Ceiling(A_Amali6)

                strSQL = "Select A_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_Teori6 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori6 = Math.Ceiling(A_Teori6)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali6.ToString())) Then
                    B_Amali6 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori6.ToString())) Then
                    B_Teori6 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali6.ToString())) Then
                    A_Amali6 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori6.ToString())) Then
                    A_Teori6 = 0
                End If

                If B_Amali6 = "-1" Or B_Teori6 = "-1" Or A_Amali6 = "-1" Or A_Teori6 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM6='" & PointerM6 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    PBM6 = Math.Ceiling(((B_Amali6 / 100) * PBAmali6) + ((B_Teori6 / 100) * PBTeori6))
                    PAM6 = Math.Ceiling(((A_Amali6 / 100) * PAAmali6) + ((A_Teori6 / 100) * PATeori6))

                    PointerM6 = Math.Ceiling(PBM6 + PAM6)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM6='" & PointerM6 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
        Next

    End Sub
    Private Sub Vokasional_gred()
        strRet = BindData(datRespondent)
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim PBPAM1 As String
            Dim GredPBPAM1 As String

            strSQL = "SELECT PBPAM1 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM1 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM1) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM1 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM1 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM1 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='" & GredPBPAM1 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            '-----------------------------------------------------------------
            Dim PBPAM2 As String
            Dim GredPBPAM2 As String

            strSQL = "SELECT PBPAM2 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM2 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM2) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM2 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM2 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM2 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='" & GredPBPAM2 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------------------
            Dim PBPAM3 As String
            Dim GredPBPAM3 As String

            strSQL = "SELECT PBPAM3 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM3 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM3) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM3 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM3 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='" & GredPBPAM3 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------

            Dim PBPAM4 As String
            Dim GredPBPAM4 As String

            strSQL = "SELECT PBPAM4 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM4 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM4) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM4 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM4 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='" & GredPBPAM4 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PBPAM5 As String
            Dim GredPBPAM5 As String

            strSQL = "SELECT PBPAM5 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM5 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM5) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM5 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM5 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='" & GredPBPAM5 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PBPAM6 As String
            Dim GredPBPAM6 As String

            strSQL = "SELECT PBPAM6 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            PBPAM6 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM6) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM6 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM6 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='" & GredPBPAM6 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

        Next
    End Sub
    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        'datRespondent.Columns.Item("5").Visible = True
        'datRespondent.Columns.Item("6").Visible = True
        'datRespondent.Columns.Item("7").Visible = True
        'datRespondent.Columns.Item("8").Visible = True
        'datRespondent.Columns.Item("9").Visible = True
        'datRespondent.Columns.Item("10").Visible = True
        'datRespondent.Columns.Item("11").Visible = True
        'datRespondent.Columns.Item("12").Visible = True
        'datRespondent.Columns.Item("13").Visible = True
        'datRespondent.Columns.Item("14").Visible = True
        'datRespondent.Columns.Item("15").Visible = True
        'datRespondent.Columns.Item("16").Visible = True
        'datRespondent.Columns.Item("17").Visible = True
        'datRespondent.Columns.Item("18").Visible = True
        'datRespondent.Columns.Item("19").Visible = True
        'datRespondent.Columns.Item("20").Visible = True
        strRet = BindData(datRespondent)
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()

    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        Vokasional_markah()
        Vokasional_gred()
        If Not strRet = "0" Then
            divMsgResult.Attributes("class") = "error"
            lblMsgResult.Text = "Tidak Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
        Else
            divMsgResult.Attributes("class") = "info"
            lblMsgResult.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
            strRet = BindData((datRespondent))
        End If
    End Sub

End Class