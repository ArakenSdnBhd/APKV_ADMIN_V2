﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Public Class import_modul_markah1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strTahun As String = ""
    Dim strSemester As String = ""
    Dim strSesi As String = ""
    Dim strKodKursus As String = ""
    Dim strNama As String = ""
    Dim strMyKad As String = ""

    Dim strTeori1 As String = ""
    Dim strTeori2 As String = ""
    Dim strTeori3 As String = ""
    Dim strTeori4 As String = ""
    Dim strTeori5 As String = ""
    Dim strTeori6 As String = ""
    Dim strTeori7 As String = ""
    Dim strTeori8 As String = ""

    Dim strAmali1 As String = ""
    Dim strAmali2 As String = ""
    Dim strAmali3 As String = ""
    Dim strAmali4 As String = ""
    Dim strAmali5 As String = ""
    Dim strAmali6 As String = ""
    Dim strAmali7 As String = ""
    Dim strAmali8 As String = ""

    

    Dim strPelajarID As String = ""
    Dim strKursusID As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""
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
                ddlKolej.Text = "0"

                kpmkv_kodkursus_list()
                ddlKodKursus.Text = "0"

                kpmkv_kelas_list()
                ddlKelas.Text = "0"

                divImport.Visible = False

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
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
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("PILIH", "PILIH"))

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

            '--ALL
            ddlSemester.Items.Add(New ListItem("PILIH", "0"))

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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
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
            ddlKodKursus.Items.Add(New ListItem("PILIH", "0"))

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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.Text) & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
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
            ddlKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQLPA() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.Nama,kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus AS [KodProgram],"
        tmpSQL += " kpmkv_pelajar_markah.A_Amali1 AS Amali1"
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

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
        '--kelas
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        getSQLPA = tmpSQL & strWhere & strOrder
        ''--debug
        ' Response.Write(getSQL)

        Return getSQLPA

    End Function
    Private Function getSQL() As String
        Dim strModul As String = ""
        Dim strModul2 As String = ""
        Dim strModul3 As String = ""
        Dim strModul4 As String = ""
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--count no of modul
        Dim nCount As Integer = 0
        strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
        strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_modul.KursusID='" & ddlKodKursus.SelectedValue & "' AND kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "' AND kpmkv_modul.semester='" & ddlSemester.Text & "'"
        nCount = oCommon.getFieldValueInt(strSQL)

        '--loop 1 - count - PB
        For value As Integer = 1 To nCount
            strModul2 += "," & "kpmkv_pelajar_markah.B_Teori" & value.ToString & " AS Teori" & value.ToString & "," & "kpmkv_pelajar_markah.B_Amali" & value.ToString & " AS Amali" & value.ToString
        Next

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.Nama,kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus AS [KodProgram]"
        tmpSQL += strModul2
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"

        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

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
        ' Response.Write(getSQL)

        Return getSQL

    End Function
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
    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        lblMsg.Text = ""
        If chkResult.Text = "PB" Then
            ExportToCSV(getSQL)

        Else
            ExportToCSV(getSQLPA)


        End If

    End Sub
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=markah.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub
    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"

            Else

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fullFileName & ";Extended Properties=Excel 8.0;HDR=YES;")
            'Response.Write("excelConnectionString:" & excelConnectionString)

            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [markah$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    If chkResult.Text = "PB" Then
                        SaveSiteData(ds)
                    Else
                        SaveSiteDataPA(ds)
                    End If

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Markah Calon:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function
    Public Function FileIsLocked(ByVal strFullFileName As String) As Boolean
        Dim blnReturn As Boolean = False
        Dim fs As System.IO.FileStream

        Try
            fs = System.IO.File.Open(strFullFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.None)
            fs.Close()
        Catch ex As System.IO.IOException
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1

                refreshVar()
                strMsg = ""

                'bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama")) Then
                    strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                Else
                    strMsg += "Sila isi Nama|"
                End If

                'Tahun
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mykad")) Then
                    strMyKad = SiteData.Tables(0).Rows(i).Item("Mykad")
                Else
                    strMsg += "Sila isi Mykad|"
                End If

                'Kod Kursus
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KodProgram")) Then
                    strKodKursus = SiteData.Tables(0).Rows(i).Item("KodProgram")
                Else
                    strMsg += "Sila isi Kod Program|"
                End If



                If strMsg.Length = 0 Then
                    'strMsg = "Record#:" & i.ToString & "OK"
                    'strMsg += "<br/>"
                Else
                    strMsg = " & strMsg"
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)
                'disp bil

            Next

            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""
        strAmali1 = 0
        strAmali2 = 0
        strAmali3 = 0
        strAmali4 = 0
        strAmali5 = 0
        strAmali6 = 0
        strAmali7 = 0
        strAmali8 = 0

        strTeori1 = 0
        strTeori2 = 0
        strTeori3 = 0
        strTeori4 = 0
        strTeori5 = 0
        strTeori6 = 0
        strTeori7 = 0
        strTeori8 = 0


        Try

            Dim sb As StringBuilder = New StringBuilder()


            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1


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


                'strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                strKodKursus = SiteData.Tables(0).Rows(i).Item("KodProgram")
                strMyKad = SiteData.Tables(0).Rows(i).Item("MyKad")

                '--loop 1 - count
                For value As Integer = 1 To nCount

                    If value = 1 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali1")) Then
                            strAmali1 = SiteData.Tables(0).Rows(i).Item("Amali1")
                        Else
                            strAmali1 = 0
                        End If

                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori1")) Then
                            strTeori1 = SiteData.Tables(0).Rows(i).Item("Teori1")
                        Else
                            strTeori1 = 0
                        End If
                    End If

                    If value = 2 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali2")) Then
                            strAmali2 = SiteData.Tables(0).Rows(i).Item("Amali2")
                        Else
                            strAmali2 = 0
                        End If

                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori2")) Then
                            strTeori2 = SiteData.Tables(0).Rows(i).Item("Teori2")
                        Else
                            strTeori2 = 0
                        End If
                    End If

                    If value = 3 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali3")) Then
                            strAmali3 = SiteData.Tables(0).Rows(i).Item("Amali3")
                        Else
                            strAmali3 = 0
                        End If

                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori3")) Then
                            strTeori3 = SiteData.Tables(0).Rows(i).Item("Teori3")
                        Else
                            strTeori3 = 0
                        End If
                    End If

                    If value = 4 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali4")) Then
                            strAmali4 = SiteData.Tables(0).Rows(i).Item("Amali4")
                        Else
                            strAmali4 = 0
                        End If

                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori4")) Then
                            strTeori4 = SiteData.Tables(0).Rows(i).Item("Teori4")
                        Else
                            strTeori4 = 0
                        End If
                    End If

                    If value = 5 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali5")) Then
                            strAmali5 = SiteData.Tables(0).Rows(i).Item("Amali5")
                        Else
                            strAmali5 = 0
                        End If

                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori5")) Then
                            strTeori5 = SiteData.Tables(0).Rows(i).Item("Teori5")
                        Else
                            strTeori5 = 0
                        End If
                    End If

                    If value = 6 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali6")) Then
                            strAmali6 = SiteData.Tables(0).Rows(i).Item("Amali6")
                        Else
                            strAmali6 = 0
                        End If
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori6")) Then
                            strTeori6 = SiteData.Tables(0).Rows(i).Item("Teori6")
                        Else
                            strTeori6 = 0
                        End If
                    End If

                    If value = 7 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali7")) Then
                            strAmali7 = SiteData.Tables(0).Rows(i).Item("Amali7")
                        Else
                            strAmali7 = 0
                        End If
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori7")) Then
                            strTeori7 = SiteData.Tables(0).Rows(i).Item("Teori7")
                        Else
                            strTeori7 = 0
                        End If
                    End If

                    If value = 8 Then
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali8")) Then
                            strAmali8 = SiteData.Tables(0).Rows(i).Item("Amali8")
                        Else
                            strAmali8 = 0
                        End If
                        If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori8")) Then
                            strTeori8 = SiteData.Tables(0).Rows(i).Item("Teori8")
                        Else
                            strTeori8 = 0
                        End If
                    End If
                Next
                '***KursusID
                strSQL = "SELECT KursusID FROM kpmkv_kursus WHERE KodKursus='" & strKodKursus & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "'"
                Dim strKursusID As String = oCommon.getFieldValue(strSQL)

                '*****pelajarid
                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD='" & strMyKad & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "' AND Semester='" & ddlSemester.SelectedValue & "'"
                Dim strPelajarID As String = oCommon.getFieldValue(strSQL)
                If Not String.IsNullOrEmpty(strPelajarID) Then

                    strSQL = "UPDATE kpmkv_pelajar_markah Set B_Amali1='" & oCommon.FixSingleQuotes(strAmali1) & "',B_Amali2='" & oCommon.FixSingleQuotes(strAmali2) & "',B_Amali3='" & oCommon.FixSingleQuotes(strAmali3) & "',"
                    strSQL += " B_Amali4 ='" & oCommon.FixSingleQuotes(strAmali4) & "',B_Amali5='" & oCommon.FixSingleQuotes(strAmali5) & "',B_Amali6='" & oCommon.FixSingleQuotes(strAmali6) & "',B_Amali7='" & oCommon.FixSingleQuotes(strAmali7) & "' ,B_Amali8='" & oCommon.FixSingleQuotes(strAmali8) & "',"
                    strSQL += " B_Teori1='" & oCommon.FixSingleQuotes(strTeori1) & "',B_Teori2='" & oCommon.FixSingleQuotes(strTeori2) & "',B_Teori3='" & oCommon.FixSingleQuotes(strTeori3) & "',B_Teori4='" & oCommon.FixSingleQuotes(strTeori4) & "',"
                    strSQL += " B_Teori5='" & oCommon.FixSingleQuotes(strTeori5) & "',B_Teori6='" & oCommon.FixSingleQuotes(strTeori6) & "',B_Teori7='" & oCommon.FixSingleQuotes(strTeori7) & "',B_Teori8='" & oCommon.FixSingleQuotes(strTeori8) & "'"
                    strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND Sesi='" & oCommon.FixSingleQuotes(chkSesi.Text) & "' AND Semester='" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "' AND Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "' AND KursusID='" & oCommon.FixSingleQuotes(strKursusID) & "' AND PelajarID='" & oCommon.FixSingleQuotes(strPelajarID) & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    'Response.Write(strSQL)
                    If strRet = "0" Then
                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Markah berjaya dimasukkan"
                    Else
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Markah tidak berjaya dimasukkan"
                        Exit For
                    End If
                Else
                End If
            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try
        Return True

    End Function
    Private Function SaveSiteDataPA(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""
        strAmali1 = 0

        Try

            Dim sb As StringBuilder = New StringBuilder()

            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1

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


                'strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                strKodKursus = SiteData.Tables(0).Rows(i).Item("KodProgram")
                strMyKad = SiteData.Tables(0).Rows(i).Item("MyKad")

                If IsNumeric(SiteData.Tables(0).Rows(i).Item("Amali1")) Then
                    strAmali1 = SiteData.Tables(0).Rows(i).Item("Amali1")
                Else
                    strAmali1 = 0
                End If

                '***KursusID
                strSQL = "SELECT KursusID FROM kpmkv_kursus WHERE KodKursus='" & strKodKursus & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "'"
                Dim strKursusID As String = oCommon.getFieldValue(strSQL)

                '*****pelajarid
                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD='" & strMyKad & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "' AND Semester='" & ddlSemester.SelectedValue & "'"
                Dim strPelajarID As String = oCommon.getFieldValue(strSQL)
                If Not String.IsNullOrEmpty(strPelajarID) Then

                    If nCount = 2 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 3 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 4 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "', A_Amali4='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 5 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "', A_Amali4='" & strAmali1 & "'"
                        strSQL += " A_Amali5='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 6 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "', A_Amali4='" & strAmali1 & "',"
                        strSQL += " A_Amali5='" & strAmali1 & "', A_Amali6='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 7 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "', A_Amali4='" & strAmali1 & "',"
                        strSQL += " A_Amali5='" & strAmali1 & "', A_Amali6='" & strAmali1 & "',"
                        strSQL += " A_Amali7='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    ElseIf nCount = 8 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Amali1 ='" & strAmali1 & "', A_Amali2='" & strAmali1 & "',"
                        strSQL += " A_Amali3='" & strAmali1 & "', A_Amali4='" & strAmali1 & "',"
                        strSQL += " A_Amali5='" & strAmali1 & "', A_Amali6='" & strAmali1 & "',"
                        strSQL += " A_Amali7='" & strAmali1 & "', A_Amali8='" & strAmali1 & "'"
                        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND PelajarID='" & strPelajarID & "'"
                    End If
                    strRet = oCommon.ExecuteSQL(strSQL)
                    'Response.Write(strSQL)
                    If strRet = "0" Then

                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Markah berjaya dimasukkan"
                    Else

                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Markah tidak berjaya dimasukkan"
                        Exit For
                    End If
                Else
                End If
            Next


        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try
        Return True

    End Function
    Private Sub refreshVar()

        strNama = ""
        strMyKad = ""
        strKodKursus = ""

        strAmali1 = ""
        strAmali2 = ""
        strAmali3 = ""
        strAmali4 = ""
        strAmali5 = ""
        strAmali6 = ""
        strAmali7 = ""
        strAmali8 = ""

        strTeori1 = ""
        strTeori2 = ""
        strTeori3 = ""
        strTeori4 = ""
        strTeori5 = ""
        strTeori6 = ""
        strTeori7 = ""
        strTeori8 = ""
    End Sub
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub

    Private Sub chkResult_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkResult.SelectedIndexChanged
        lblMsg.Text = ""
        If Not chkResult.Text = "" Then
            divImport.Visible = True
        Else
            divImport.Visible = False
        End If
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
End Class