﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class markah_create_PAA
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year
                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()
                'kpmkv_jeniscalon_list()
                'ddlJenisCalon.Text = "AKTIF"

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Jenis"
            ddlJenis.DataValueField = "Jenis"
            ddlJenis.DataBind()

            '--ALL
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    'Private Sub hiddencolumn()

    '    Select Case ddlSemester.Text
    '        Case "1"

    '            datRespondent.Columns.Item("5").Visible = True
    '            datRespondent.Columns.Item("6").Visible = False
    '            datRespondent.Columns.Item("7").Visible = False
    '            datRespondent.Columns.Item("8").Visible = False 'bm3
    '            datRespondent.Columns.Item("9").Visible = True
    '            datRespondent.Columns.Item("10").Visible = True
    '            datRespondent.Columns.Item("11").Visible = True
    '            datRespondent.Columns.Item("12").Visible = True
    '            datRespondent.Columns.Item("13").Visible = True
    '            datRespondent.Columns.Item("14").Visible = True
    '            datRespondent.Columns.Item("15").Visible = True
    '        Case "2"
    '            datRespondent.Columns.Item("5").Visible = True
    '            datRespondent.Columns.Item("6").Visible = False
    '            datRespondent.Columns.Item("7").Visible = False
    '            datRespondent.Columns.Item("8").Visible = False 'bm3
    '            datRespondent.Columns.Item("9").Visible = True
    '            datRespondent.Columns.Item("10").Visible = True
    '            datRespondent.Columns.Item("11").Visible = True
    '            datRespondent.Columns.Item("12").Visible = True
    '            datRespondent.Columns.Item("13").Visible = True
    '            datRespondent.Columns.Item("14").Visible = True
    '            datRespondent.Columns.Item("15").Visible = True

    '        Case "3"
    '            datRespondent.Columns.Item("5").Visible = False
    '            datRespondent.Columns.Item("6").Visible = True
    '            datRespondent.Columns.Item("7").Visible = True
    '            datRespondent.Columns.Item("8").Visible = False 'bm3
    '            datRespondent.Columns.Item("9").Visible = True
    '            datRespondent.Columns.Item("10").Visible = True
    '            datRespondent.Columns.Item("11").Visible = True
    '            datRespondent.Columns.Item("12").Visible = True
    '            datRespondent.Columns.Item("13").Visible = True
    '            datRespondent.Columns.Item("14").Visible = True
    '            datRespondent.Columns.Item("15").Visible = True

    '        Case "4"
    '            datRespondent.Columns.Item("5").Visible = False
    '            datRespondent.Columns.Item("6").Visible = False
    '            datRespondent.Columns.Item("7").Visible = False
    '            datRespondent.Columns.Item("8").Visible = True 'bm3
    '            datRespondent.Columns.Item("9").Visible = True
    '            datRespondent.Columns.Item("10").Visible = True
    '            datRespondent.Columns.Item("11").Visible = True
    '            datRespondent.Columns.Item("12").Visible = True
    '            datRespondent.Columns.Item("13").Visible = True
    '            datRespondent.Columns.Item("14").Visible = True
    '            datRespondent.Columns.Item("15").Visible = True


    '    End Select

    'End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY Semester"
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

        strSQL = "SELECT DISTINCT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' ORDER BY kpmkv_kursus.KodKursus"
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

            '--ALL
            ' ddlKodKursus.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID"
        strSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "NamaKelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            '--ALL
            'ddlNamaKelas.Items.Add(New ListItem("PILIH", "PILIH"))

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

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus, kpmkv_pelajar_markah.A_BahasaMelayu, kpmkv_pelajar_markah.A_BahasaMelayu1, kpmkv_pelajar_markah.A_BahasaMelayu2, kpmkv_pelajar_markah.A_BahasaMelayu3, "
        tmpSQL += " kpmkv_pelajar_markah.A_BahasaInggeris, kpmkv_pelajar_markah.A_Science1, kpmkv_pelajar_markah.A_Science2, kpmkv_pelajar_markah.A_Sejarah, kpmkv_pelajar_markah.A_PendidikanIslam1, "
        tmpSQL += " kpmkv_pelajar_markah.A_PendidikanIslam2, kpmkv_pelajar_markah.A_PendidikanMoral, kpmkv_pelajar_markah.A_Mathematics"
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--Kod
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--sesi
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
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

        Try
            If ValidateForm() = False Then
                lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
                Exit Sub
            End If

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim strBahasaMelayu As TextBox = datRespondent.Rows(i).FindControl("A_BahasaMelayu")
                Dim strBahasaMelayu1 As TextBox = datRespondent.Rows(i).FindControl("A_BahasaMelayu1")
                Dim strBahasaMelayu2 As TextBox = datRespondent.Rows(i).FindControl("A_BahasaMelayu2")
                Dim strBahasaMelayu3 As TextBox = datRespondent.Rows(i).FindControl("A_BahasaMelayu3")
                Dim strBahasaInggeris As TextBox = datRespondent.Rows(i).FindControl("A_BahasaInggeris")
                Dim strScience1 As TextBox = datRespondent.Rows(i).FindControl("A_Science1")
                Dim strScience2 As TextBox = datRespondent.Rows(i).FindControl("A_Science2")
                Dim strSejarah As TextBox = datRespondent.Rows(i).FindControl("A_Sejarah")
                Dim strPendidikanIslam1 As TextBox = datRespondent.Rows(i).FindControl("A_PendidikanIslam1")
                Dim strPendidikanIslam2 As TextBox = datRespondent.Rows(i).FindControl("A_PendidikanIslam2")
                Dim strPendidikanMoral As TextBox = datRespondent.Rows(i).FindControl("A_PendidikanMoral")
                Dim strMatematik As TextBox = datRespondent.Rows(i).FindControl("A_Mathematics")


                'assign value to integer
                Dim BM As Integer = strBahasaMelayu.Text
                Dim BM1 As Integer = strBahasaMelayu1.Text
                Dim BM2 As Integer = strBahasaMelayu2.Text
                Dim BM3 As Integer = strBahasaMelayu3.Text
                Dim BI As Integer = strBahasaInggeris.Text
                Dim SC1 As Integer = strScience1.Text
                Dim SC2 As Integer = strScience2.Text
                Dim SEJ As Integer = strSejarah.Text
                Dim PI1 As Integer = strPendidikanIslam1.Text
                Dim PI2 As Integer = strPendidikanIslam2.Text
                Dim PM As Integer = strPendidikanMoral.Text
                Dim Matematik As Integer = strMatematik.Text

                strSQL = "UPDATE kpmkv_pelajar_markah SET A_BahasaMelayu='" & BM & "', "
                strSQL += " A_BahasaMelayu3='" & BM3 & "', A_BahasaInggeris='" & BI & "', A_Science1='" & SC1 & "',"
                If Not ddlSemester.SelectedValue = "4" Then
                    strSQL += " A_Sejarah='" & SEJ & "',"
                End If
                strSQL += " A_PendidikanIslam1='" & PI1 & "', A_PendidikanMoral='" & PM & "', A_Science2='" & SC2 & "',"
                strSQL += " A_PendidikanIslam2='" & PI2 & "', A_Mathematics='" & Matematik & "'"
                strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsgResult.Attributes("class") = "info"
                    lblMsgResult.Text = "Berjaya!.Kemaskini markah Pentaksiran Akhir Akademik."
                Else
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Tidak Berjaya!.Kemaskini markah Pentaksir Akhiran Akademik."
                End If
            Next

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

        strRet = BindData(datRespondent)

    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strBahasaMelayu As TextBox = CType(row.FindControl("A_BahasaMelayu"), TextBox)
            Dim strBahasaMelayu1 As TextBox = CType(row.FindControl("A_BahasaMelayu1"), TextBox)
            Dim strBahasaMelayu2 As TextBox = CType(row.FindControl("A_BahasaMelayu2"), TextBox)
            Dim strBahasaMelayu3 As TextBox = CType(row.FindControl("A_BahasaMelayu3"), TextBox)
            Dim strBahasaInggeris As TextBox = CType(row.FindControl("A_BahasaInggeris"), TextBox)
            Dim strScience1 As TextBox = CType(row.FindControl("A_Science1"), TextBox)
            Dim strScience2 As TextBox = CType(row.FindControl("A_Science2"), TextBox)
            Dim strSejarah As TextBox = CType(row.FindControl("A_Sejarah"), TextBox)
            Dim strPendidikanIslam1 As TextBox = CType(row.FindControl("A_PendidikanIslam1"), TextBox)
            Dim strPendidikanIslam2 As TextBox = CType(row.FindControl("A_PendidikanIslam2"), TextBox)
            Dim strPendidikanMoral As TextBox = CType(row.FindControl("A_PendidikanMoral"), TextBox)
            Dim strMatematik As TextBox = CType(row.FindControl("A_Mathematics"), TextBox)


            '--validate NUMBER and less than 100
            '--strBahasaMelayu
            If Not strBahasaMelayu.Text.Length = 0 Then
                If oCommon.IsCurrency(strBahasaMelayu.Text) = False Then
                    Return False
                End If
                If CInt(strBahasaMelayu.Text) > 100 Then
                    Return False
                End If
            Else
                strBahasaMelayu.Text = "0"
            End If

            '--strBahasaMelayu1
            If Not strBahasaMelayu1.Text.Length = 0 Then
                If oCommon.IsCurrency(strBahasaMelayu1.Text) = False Then
                    Return False
                End If
                If CInt(strBahasaMelayu1.Text) > 100 Then
                    Return False
                End If
            Else
                strBahasaMelayu1.Text = "0"
            End If

            '--strBahasaMelayu2
            If Not strBahasaMelayu2.Text.Length = 0 Then
                If oCommon.IsCurrency(strBahasaMelayu2.Text) = False Then
                    Return False
                End If
                If CInt(strBahasaMelayu2.Text) > 100 Then
                    Return False
                End If
            Else
                strBahasaMelayu2.Text = "0"
            End If

            '--strBahasaMelayu3
            If Not strBahasaMelayu3.Text.Length = 0 Then
                If oCommon.IsCurrency(strBahasaMelayu3.Text) = False Then
                    Return False
                End If
                If CInt(strBahasaMelayu3.Text) > 100 Then
                    Return False
                End If
            Else
                strBahasaMelayu3.Text = "0"
            End If
            '--strBahasaInggeris
            If Not strBahasaInggeris.Text.Length = 0 Then
                If oCommon.IsCurrency(strBahasaInggeris.Text) = False Then
                    Return False
                End If
                If CInt(strBahasaInggeris.Text) > 100 Then
                    Return False
                End If
            Else
                strBahasaInggeris.Text = "0"
            End If

            '--strScience
            If Not strScience1.Text.Length = 0 Then
                If oCommon.IsCurrency(strScience1.Text) = False Then
                    Return False
                End If
                If CInt(strScience1.Text) > 100 Then
                    Return False
                End If
            Else
                strScience1.Text = "0"
            End If

            If Not strScience2.Text.Length = 0 Then
                If oCommon.IsCurrency(strScience2.Text) = False Then
                    Return False
                End If
                If CInt(strScience2.Text) > 100 Then
                    Return False
                End If
            Else
                strScience2.Text = "0"
            End If
            '--strSejarah
            If Not strSejarah.Text.Length = 0 Then
                If oCommon.IsCurrency(strSejarah.Text) = False Then
                    Return False
                End If
                If CInt(strSejarah.Text) > 100 Then
                    Return False
                End If
            Else
                strSejarah.Text = "0"
            End If

            '--strPendidikanIslam
            If Not strPendidikanIslam1.Text.Length = 0 Then
                If oCommon.IsCurrency(strPendidikanIslam1.Text) = False Then
                    Return False
                End If
                If CInt(strPendidikanIslam1.Text) > 100 Then
                    Return False
                End If
            Else
                strPendidikanIslam1.Text = "0"
            End If

            If Not strPendidikanIslam2.Text.Length = 0 Then
                If oCommon.IsCurrency(strPendidikanIslam2.Text) = False Then
                    Return False
                End If
                If CInt(strPendidikanIslam2.Text) > 100 Then
                    Return False
                End If
            Else
                strPendidikanIslam2.Text = "0"
            End If

            '--strPendidikanMoral
            If Not strPendidikanMoral.Text.Length = 0 Then
                If oCommon.IsCurrency(strPendidikanMoral.Text) = False Then
                    Return False
                End If
                If CInt(strPendidikanMoral.Text) > 100 Then
                    Return False
                End If
            Else
                strPendidikanMoral.Text = "0"
            End If

            'strMatematik
            If Not strMatematik.Text.Length = 0 Then
                If oCommon.IsCurrency(strMatematik.Text) = False Then
                    Return False
                End If
                If CInt(strMatematik.Text) > 100 Then
                    Return False
                End If
            Else
                strMatematik.Text = "0"
            End If
        Next

        Return True
    End Function

    Private Sub Akademik_markah()

        Dim strKodMP As Integer

        Select Case ddlSemester.Text
            Case "1"
                strKodMP = "100"
            Case "2"
                strKodMP = "200"
            Case "3"
                strKodMP = "300"
            Case "4"
                strKodMP = "400"
        End Select

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            strSQL = "SELECT TahunSem FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

            ' Dim GredBM As Integer
            Dim BerterusanBM As Integer
            Dim AkhiranBM1 As Integer
            Dim AkhiranBM2 As Integer

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanBM = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranBM2 = oCommon.getFieldValue(strSQL)

            If ddlSemester.Text = "1" Then
                Dim AM_BahasaMelayu As Integer
                Dim BM_BahasaMelayu As Integer
                Dim B_BahasaMelayu As Double
                Dim A_BahasaMelayu As Double
                Dim PointerBM As Integer

                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                B_BahasaMelayu = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayu = Math.Ceiling(B_BahasaMelayu)

                strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                A_BahasaMelayu = oCommon.getFieldValue(strSQL)
                'round up
                A_BahasaMelayu = Math.Ceiling(A_BahasaMelayu)

                'checkin Markah
                If Not (B_BahasaMelayu) = "-1" And Not (A_BahasaMelayu) = "-1" Then
                    BM_BahasaMelayu = Math.Ceiling((B_BahasaMelayu / 100) * BerterusanBM)
                    AM_BahasaMelayu = Math.Ceiling((A_BahasaMelayu / 100) * AkhiranBM1)
                    PointerBM = Math.Ceiling(BM_BahasaMelayu + AM_BahasaMelayu)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf (B_BahasaMelayu) = "-1" Or (A_BahasaMelayu) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                'Semester1

            ElseIf ddlSemester.Text = "2" Then
                Dim AM_BahasaMelayu2 As Integer
                Dim BM_BahasaMelayu2 As Integer
                Dim B_BahasaMelayu2 As Double
                Dim A_BahasaMelayu2 As Double
                Dim PointerBM2 As Integer

                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                B_BahasaMelayu2 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayu2 = Math.Ceiling(B_BahasaMelayu2)

                strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                A_BahasaMelayu2 = oCommon.getFieldValue(strSQL)
                'round up
                A_BahasaMelayu2 = Math.Ceiling(A_BahasaMelayu2)

                'checkin Markah
                If Not (B_BahasaMelayu2) = "-1" And Not (A_BahasaMelayu2) = "-1" Then
                    BM_BahasaMelayu2 = Math.Ceiling((B_BahasaMelayu2 / 100) * BerterusanBM)
                    AM_BahasaMelayu2 = Math.Ceiling((A_BahasaMelayu2 / 100) * AkhiranBM1)
                    PointerBM2 = Math.Ceiling(BM_BahasaMelayu2 + AM_BahasaMelayu2)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM2 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf (B_BahasaMelayu2) = "-1" Or (A_BahasaMelayu2) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                'semester2

            ElseIf ddlSemester.Text = "3" Then

                Dim AM_BahasaMelayu3 As Integer
                Dim BM_BahasaMelayu3 As Integer
                Dim B_BahasaMelayu3 As Double
                Dim A_BahasaMelayu3 As Double
                Dim PointerBM3 As Integer

                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                B_BahasaMelayu3 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayu3 = Math.Ceiling(B_BahasaMelayu3)

                strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                A_BahasaMelayu3 = oCommon.getFieldValue(strSQL)
                'round up
                A_BahasaMelayu3 = Math.Ceiling(A_BahasaMelayu3)

                'checkin Markah
                If Not (B_BahasaMelayu3) = "-1" And Not (A_BahasaMelayu3) = "-1" Then
                    BM_BahasaMelayu3 = Math.Ceiling((B_BahasaMelayu3 / 100) * BerterusanBM)
                    AM_BahasaMelayu3 = Math.Ceiling((A_BahasaMelayu3 / 100) * AkhiranBM1)
                    PointerBM3 = Math.Ceiling(BM_BahasaMelayu3 + AM_BahasaMelayu3)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM3 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf (B_BahasaMelayu3) = "-1" Or (A_BahasaMelayu3) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                'semester3

            ElseIf ddlSemester.Text = "4" Then
                Dim PB4 As Integer
                ' Dim PA4 As Integer
                Dim PABmSetara As Integer
                Dim PAPB4 As Integer
                ' Dim PAPB As Integer
                Dim B_BahasaMelayuSem1 As Integer
                Dim B_BahasaMelayuSem2 As Integer
                Dim B_BahasaMelayuSem3 As Integer
                Dim B_BahasaMelayuSem4 As Integer
                Dim A_BahasaMelayuSem4 As Integer
                Dim PointerBMSetara As Integer

                'get mykad
                strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='1'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                'get bm sem 1
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
                B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)
                '----------------------------------------------------------------------------

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='2'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                'get Bm sem 2
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
                B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='3'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                'get bm sem 3
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
                B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

                'get bm sem 4 PB
                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                'get bm sem 4 PA
                strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                Dim Kertas1 As Integer = 0
                Dim Kertas2 As Integer = 0

                strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                ''--get user info
                Dim ar_Kertas As Array
                ar_Kertas = strRet.Split("|")

                If (String.IsNullOrEmpty(ar_Kertas(0).ToString())) Then
                    Kertas1 = 0
                Else
                    Kertas1 = ar_Kertas(0)
                End If

                If (String.IsNullOrEmpty(ar_Kertas(1).ToString())) Then
                    Kertas2 = 0
                Else
                    Kertas2 = ar_Kertas(1)
                End If

                If Kertas1 = -1 Or Kertas2 = -1 Then

                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf Not ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then
                    PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                    'PABmSetara = Math.Ceiling(A_BahasaMelayuSem4)

                    PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                    PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM1)
                    'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                    'gred sem 4 
                    Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                        PointerBMSetara = "-1"
                    Else
                        PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))
                    End If

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then

                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            End If

            Dim BM_BahasaInggeris As Integer
            Dim AM_BahasaInggeris As Integer
            Dim BerterusanBI As Integer
            Dim AkhiranBI1 As Integer
            Dim AkhiranBI2 As Integer
            Dim B_BahasaInggeris As Double
            Dim A_BahasaInggeris As Double
            Dim PointerBI As Integer

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A02'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'BI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanBI = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'BI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranBI1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'BI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranBI2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_BahasaInggeris = oCommon.getFieldValue(strSQL)
            'round up
            B_BahasaInggeris = Math.Ceiling(B_BahasaInggeris)

            strSQL = "Select A_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_BahasaInggeris = oCommon.getFieldValue(strSQL)
            'round up
            A_BahasaInggeris = Math.Ceiling(A_BahasaInggeris)

            'checkin Markah
            If Not (B_BahasaInggeris) = "-1" And Not (A_BahasaInggeris) = "-1" Then
                BM_BahasaInggeris = Math.Ceiling((B_BahasaInggeris / 100) * BerterusanBI)
                AM_BahasaInggeris = Math.Ceiling((A_BahasaInggeris / 100) * AkhiranBI1)
                PointerBI = Math.Ceiling(BM_BahasaInggeris + AM_BahasaInggeris)
                strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='" & PointerBI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf (B_BahasaInggeris) = "-1" Or (A_BahasaInggeris) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            '------------------------------------------------------------------------------------------------------------------------
            Dim BM_Science1 As Integer
            Dim AM_Science1 As Integer
            Dim AM_Science2 As Integer
            Dim BerterusanSc As Integer
            Dim AkhiranSC1 As Integer
            Dim AkhiranSC2 As Integer
            Dim B_Science1 As Double
            Dim A_Science1 As Double
            Dim A_Science2 As Double
            Dim PointerSC1 As Integer
            Dim PointerSC2 As Integer
            Dim PointerSC As Integer
            'Dim GredSC As Integer 

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A04'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'SC' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanSc = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'SC' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranSC1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'SC' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranSC2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Science1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_Science1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Science1 = Math.Ceiling(B_Science1)

            strSQL = "Select A_Science1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_Science1 = oCommon.getFieldValue(strSQL)
            'round up
            A_Science1 = Math.Ceiling(A_Science1)

            strSQL = "Select A_Science2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_Science2 = oCommon.getFieldValue(strSQL)
            'round up
            A_Science2 = Math.Ceiling(A_Science2)

            'check sem 3 n 4 ada  kertas 1
            BM_Science1 = Math.Ceiling((B_Science1 / 100) * BerterusanSc)

            '-- PERTUKARAN PADA 10 NOV 2021 - PENGIRAAN MARKAH SAMA UNTUK SEMUA SEMESTER

            'If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

            'If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
            '        AM_Science1 = Math.Ceiling((A_Science1 / 100) * 50) '50%

            '        AM_Science2 = Math.Ceiling((A_Science2 / 100) * 20) '20% 

            '        PointerSC1 = Math.Ceiling(BM_Science1)
            '        PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
            '        PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

            '        strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            '        strRet = oCommon.ExecuteSQL(strSQL)
            '    ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
            '        strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            '        strRet = oCommon.ExecuteSQL(strSQL)
            '    End If
            'Else

            If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                AM_Science1 = Math.Ceiling((A_Science1 / 100) * AkhiranSC1) '70%
                AM_Science2 = Math.Ceiling((A_Science2 / 100) * AkhiranSC2) '70%
                PointerSC1 = Math.Ceiling(BM_Science1)
                PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            'End If

            'SC ------------------------------------------------------------------------------------------------------------

            Dim BM_Sejarah As Integer
            Dim AM_Sejarah As Integer
            Dim BerterusanSJ As Integer
            Dim AkhiranSJ1 As Integer
            Dim AkhiranSJ2 As Integer
            Dim B_Sejarah As Double
            Dim A_Sejarah As Double
            Dim PointerSJ As Integer
            'Dim GredSJ As Integer 

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A05'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'SJ' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanSJ = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'SJ' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranSJ1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'SJ' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranSJ2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_Sejarah = oCommon.getFieldValue(strSQL)
            'round up
            B_Sejarah = Math.Ceiling(B_Sejarah)

            strSQL = "Select A_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_Sejarah = oCommon.getFieldValue(strSQL)
            'round up
            A_Sejarah = Math.Ceiling(A_Sejarah)

            'checkin Markah
            If Not (B_Sejarah) = "-1" And Not (A_Sejarah) = "-1" Then
                BM_Sejarah = Math.Ceiling((B_Sejarah / 100) * BerterusanSJ)
                AM_Sejarah = Math.Ceiling((A_Sejarah / 100) * AkhiranSJ1)
                PointerSJ = Math.Ceiling(BM_Sejarah + AM_Sejarah)
                strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='" & PointerSJ & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf (B_Sejarah) = "-1" Or (A_Sejarah) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            If ddlSemester.Text = "4" Then

                Dim SJ1 As String
                Dim SJ2 As String
                Dim SJ3 As String
                Dim SJ4 As String

                Dim SJ1Int As Integer
                Dim SJ2Int As Integer
                Dim SJ3Int As Integer
                Dim SJ4Int As Integer

                Dim SJ1Double As Double
                Dim SJ2Double As Double
                Dim SJ3Double As Double
                Dim SJ4Double As Double

                Dim PointerSJSetara As Integer
                Dim PointerSJSetaraDouble As Double

                ''get MYKAD pelajar
                strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                Dim strMYKADSJ As String = oCommon.getFieldValue(strSQL)

                ''get pelajarid sem1
                strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '1'"
                Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                ''get pelajarid sem2
                strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '2'"
                Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                ''get pelajarid sem3
                strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '3'"
                Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                ''get pelajarid sem4
                strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '4'"
                Dim strPelajarID4 As String = oCommon.getFieldValue(strSQL)

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID1 & "' AND Semester = '1'"
                SJ1 = oCommon.getFieldValue(strSQL)
                SJ1Double = (10 / 100) * Double.Parse(SJ1)
                SJ1Int = Math.Ceiling((10 / 100) * Double.Parse(SJ1))

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "' AND Semester = '2'"
                SJ2 = oCommon.getFieldValue(strSQL)
                SJ2Double = (10 / 100) * Double.Parse(SJ2)
                SJ2Int = Math.Ceiling((10 / 100) * Double.Parse(SJ2))

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "' AND Semester = '3'"
                SJ3 = oCommon.getFieldValue(strSQL)
                SJ3Double = (10 / 100) * Double.Parse(SJ3)
                SJ3Int = Math.Ceiling((10 / 100) * Double.Parse(SJ3))

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID4 & "' AND Semester = '4'"
                SJ4 = oCommon.getFieldValue(strSQL)
                SJ4Double = (70 / 100) * Double.Parse(SJ4)
                SJ4Int = Math.Ceiling((70 / 100) * Double.Parse(SJ4))

                If SJ1.ToString = "T" Or SJ1 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                ElseIf SJ2.ToString = "T" Or SJ2 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                ElseIf SJ3.ToString = "T" Or SJ3 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                Else

                    PointerSJSetara = Integer.Parse(SJ1Int) + Integer.Parse(SJ2Int) + Integer.Parse(SJ3Int) + Integer.Parse(SJ4Int)
                    PointerSJSetaraDouble = SJ1Double + SJ2Double + SJ3Double + SJ4Double

                End If

                strSQL = "UPDATE kpmkv_pelajar_markah SET PointerSJSetara='" & PointerSJSetaraDouble & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------
            'strSQL = "Select Kaum from kpmkv_pelajar Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            'Dim strKaum As String = oCommon.getFieldValue(strSQL)
            'If strKaum = "BUMIPUTERA" Then

            Dim BM_PendidikanIslam1 As Integer
            Dim BerterusanPI As Integer
            Dim AkhiranPI1 As Integer
            Dim AkhiranPI2 As Integer
            Dim B_PendidikanIslam1 As Integer
            Dim A_PendidikanIslam1 As Integer
            Dim A_PendidikanIslam2 As Integer
            Dim PointerPI1 As Integer
            Dim PointerPI2 As Integer
            Dim PointerPI As Integer
            ' Dim GredPI As Integer 

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A06'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'PI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanPI = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A06'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'PI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranPI1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'PI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranPI2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
            'round up
            B_PendidikanIslam1 = Math.Ceiling(B_PendidikanIslam1)

            strSQL = "Select A_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
            'round up
            A_PendidikanIslam1 = Math.Ceiling(A_PendidikanIslam1)

            strSQL = "Select A_PendidikanIslam2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_PendidikanIslam2 = oCommon.getFieldValue(strSQL)
            'round up
            A_PendidikanIslam2 = Math.Ceiling(A_PendidikanIslam2)

            BM_PendidikanIslam1 = Math.Ceiling((B_PendidikanIslam1 / 100) * BerterusanPI)

            If Not (A_PendidikanIslam1) = "-1" And Not (A_PendidikanIslam2) = "-1" Then
                A_PendidikanIslam1 = Math.Ceiling((A_PendidikanIslam1 / 100) * AkhiranPI1)
                A_PendidikanIslam2 = Math.Ceiling((A_PendidikanIslam2 / 100) * AkhiranPI2)

                PointerPI1 = Math.Ceiling(BM_PendidikanIslam1)
                PointerPI2 = Math.Ceiling(A_PendidikanIslam1 + A_PendidikanIslam2)
                PointerPI = Math.Ceiling(PointerPI1 + PointerPI2)

                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='" & PointerPI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (A_PendidikanIslam1) = "-1" Or (A_PendidikanIslam2) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim BM_PendidikanMoral As Integer
            Dim AM_PendidikanMoral As Integer
            Dim BerterusanPM As Integer
            Dim AkhiranPM1 As Integer
            Dim AkhiranPM2 As Integer
            Dim B_PendidikanMoral As Integer
            Dim A_PendidikanMoral As Integer
            Dim PointerPM As Integer
            'Dim GredPM As Integer 

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A07'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'PM' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanPM = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A07'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'PI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranPM1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'PI' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranPM2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_PendidikanMoral = oCommon.getFieldValue(strSQL)
            'round up
            B_PendidikanMoral = Math.Ceiling(B_PendidikanMoral)

            strSQL = "Select A_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_PendidikanMoral = oCommon.getFieldValue(strSQL)
            'round up
            A_PendidikanMoral = Math.Ceiling(A_PendidikanMoral)

            'checkin Markah
            If Not (B_PendidikanMoral) = "-1" And Not (A_PendidikanMoral) = "-1" Then
                BM_PendidikanMoral = Math.Ceiling((B_PendidikanMoral / 100) * BerterusanPM)
                AM_PendidikanMoral = Math.Ceiling((A_PendidikanMoral / 100) * AkhiranPM1)
                PointerPM = Math.Ceiling(BM_PendidikanMoral + AM_PendidikanMoral)
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='" & PointerPM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (B_PendidikanMoral) = "-1" Or (A_PendidikanMoral) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim BM_Mathematics As Integer
            Dim AM_Mathematics As Integer
            Dim BerterusanMT As Integer
            Dim AkhiranMT1 As Integer
            Dim AkhiranMT2 As Integer
            Dim B_Mathematics As Integer
            Dim A_Mathematics As Integer
            Dim PointerMT As Integer
            'Dim GredMT As Integer 

            'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'MT' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            BerterusanMT = oCommon.getFieldValue(strSQL)

            'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
            'strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            '-- PERTUKARAN KE TABLE kpmkv_wajaran_a WAJARAN PADA 10 NOV 2021
            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'MT' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranMT1 = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'MT' AND Kohort = '" & ddlTahun.Text & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND Semester = '" & ddlSemester.Text & "'"
            AkhiranMT2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            B_Mathematics = oCommon.getFieldValue(strSQL)
            'round up
            B_Mathematics = Math.Ceiling(B_Mathematics)

            strSQL = "Select A_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            A_Mathematics = oCommon.getFieldValue(strSQL)
            'round up
            A_Mathematics = Math.Ceiling(A_Mathematics)

            'checkin Markah
            If Not (B_Mathematics) = "-1" And Not (A_Mathematics) = "-1" Then
                BM_Mathematics = Math.Ceiling((B_Mathematics / 100) * BerterusanMT)
                AM_Mathematics = Math.Ceiling((A_Mathematics / 100) * AkhiranMT1)
                PointerMT = Math.Ceiling(BM_Mathematics + AM_Mathematics)
                strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='" & PointerMT & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (B_Mathematics) = "-1" Or (A_Mathematics) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
        Next
    End Sub
    Private Sub Akademik_gred()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim BM As String
            Dim GredBM As String

            Dim Kertas1 As Integer = 0
            Dim Kertas2 As Integer = 0

            If ddlSemester.SelectedValue = 4 Then

                strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                ''--get user info
                Dim ar_Kertas As Array
                ar_Kertas = strRet.Split("|")

                If (String.IsNullOrEmpty(ar_Kertas(0).ToString())) Then
                    Kertas1 = 0
                Else
                    Kertas1 = ar_Kertas(0)
                End If

                If (String.IsNullOrEmpty(ar_Kertas(1).ToString())) Then
                    Kertas2 = 0
                Else
                    Kertas2 = ar_Kertas(1)
                End If

            End If

            strSQL = "SELECT BahasaMelayu as BM FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            BM = oCommon.getFieldValue(strSQL)

            If Kertas1 = -1 Or Kertas2 = -1 Then

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM = 'T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                If String.IsNullOrEmpty(BM) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredBM = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='" & GredBM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            End If


            '-----------------------------------------------------------------
            Dim BI As String
            Dim GredBI As String

            strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            BI = oCommon.getFieldValue(strSQL)

            'If BI = "0" Then
            If String.IsNullOrEmpty(BI) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredBI = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredBI='" & GredBI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------------------
            Dim SC As Integer
            Dim GredSC As String

            strSQL = "SELECT Science FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            SC = oCommon.getFieldValue(strSQL)

            'If SC = 0 Then
            If String.IsNullOrEmpty(SC) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & SC & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredSC = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredSC='" & GredSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------

            Dim SJ As String
            Dim GredSJ As String

            strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            SJ = oCommon.getFieldValue(strSQL)

            'If SJ = "0" Then
            If String.IsNullOrEmpty(SJ) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(SJ) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredSJ = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJ='" & GredSJ & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PI As String
            Dim GredPI As String

            strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            PI = oCommon.getFieldValue(strSQL)

            If PI = "0" Then
            ElseIf String.IsNullOrEmpty(PI) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredPI = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredPI='" & GredPI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PM As String
            Dim GredPM As String

            strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            PM = oCommon.getFieldValue(strSQL)

            If PM = "0" Then
            ElseIf String.IsNullOrEmpty(PM) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredPM = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredPM='" & GredPM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim MT As String
            Dim GredMT As String

            strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            MT = oCommon.getFieldValue(strSQL)

            'If MT = "0" Then
            If String.IsNullOrEmpty(MT) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(MT) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredMT = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredMT='" & GredMT & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        Next
    End Sub
    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        'check ulang
        'For j As Integer = 0 To datRespondent.Rows.Count - 1
        '    strSQL = "select PelajarID from kpmkv_pelajar_ulang where PelajarID='" & datRespondent.DataKeys(j).Value.ToString & "'"
        '    strRet = oCommon.isExist(strSQL)
        '    If strRet = True Then
        '        strSQL = "select Gred from kpmkv_pelajar_ulang where PelajarID='" & datRespondent.DataKeys(j).Value.ToString & "'"
        '        strRet = oCommon.isExist(strSQL)
        '        If strRet = True Then
        '            btnUpdate.Enabled = False
        '            btnGred.Enabled = False
        '            Exit Sub
        '        End If
        '    End If
        'Next
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()

    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        Akademik_markah()
        Akademik_gred()
        If Not strRet = "0" Then
            divMsgResult.Attributes("class") = "error"
            lblMsgResult.Text = "Tidak Berjaya mengemaskini gred Pentaksiran Akhir Akademik"
        Else
            divMsgResult.Attributes("class") = "info"
            lblMsgResult.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Akademik"
            strRet = BindData((datRespondent))
        End If

    End Sub

    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
End Class