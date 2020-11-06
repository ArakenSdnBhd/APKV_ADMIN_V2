Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class pelajar_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod ini?');")

        Try
            If Not IsPostBack Then



                lblMsg.Text = ""
                LoadPage()
                kpmkv_kaum_list()
                kpmkv_status_list()
                kpmkv_jeniscalon_list()

                strSQL = "SELECT KlusterID FROM kpmkv_kursus WHERE KodKursus='" & lblKodKursus.Text & "'"
                Dim strKlusterID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT NamaKluster FROM kpmkv_kluster WHERE KlusterID='" & strKlusterID & "'"
                lblKluster.Text = oCommon.getFieldValue(strSQL)

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub
    Private Sub kpmkv_kaum_list()
        strSQL = "SELECT Kaum FROM kpmkv_kaum ORDER BY Kaum"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKaum.DataSource = ds
            ddlKaum.DataTextField = "Kaum"
            ddlKaum.DataValueField = "Kaum"
            ddlKaum.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_status_list()
        strSQL = "SELECT StatusID, Status FROM kpmkv_status ORDER BY StatusID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Status"
            ddlStatus.DataValueField = "StatusID"
            ddlStatus.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jeniscalon_list()
        strSQL = "SELECT JenisCalonID, JenisCalon FROM kpmkv_jeniscalon ORDER BY JenisCalonID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenisCalon.DataSource = ds
            ddlJenisCalon.DataTextField = "Jeniscalon"
            ddlJenisCalon.DataValueField = "JenisCalonID"
            ddlJenisCalon.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT kpmkv_pelajar.KolejRecordID, kpmkv_pelajar.Pengajian, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, "
        strSQL += " kpmkv_jeniscalon.JenisCalonID, kpmkv_kursus.KursusID, kpmkv_kursus.KodKursus, kpmkv_kursus.NamaKursus, kpmkv_pelajar.Kaum, kpmkv_pelajar.Jantina, "
        strSQL += " kpmkv_pelajar.Agama, kpmkv_status.StatusID, kpmkv_pelajar.Email, kpmkv_pelajar.Catatan, kpmkv_kelas.NamaKelas"
        strSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        strSQL += " kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        strSQL += " LEFT OUTER JOIN kpmkv_jeniscalon ON kpmkv_pelajar.JenisCalonID = kpmkv_jeniscalon.JenisCalonID "
        strSQL += " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.PelajarID='" & Request.QueryString("PelajarID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KolejRecordID")) Then
                    lblHidKolej.Text = ds.Tables(0).Rows(0).Item("KolejRecordID")
                Else
                    lblHidKolej.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pengajian")) Then
                    lblPengajian.Text = ds.Tables(0).Rows(0).Item("Pengajian")
                Else
                    lblPengajian.Text = ""
                End If

                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    lblSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    lblSesi.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaKursus")) Then
                    lblNamaKursus.Text = ds.Tables(0).Rows(0).Item("NamaKursus")
                Else
                    lblNamaKursus.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KodKursus")) Then
                    lblKodKursus.Text = ds.Tables(0).Rows(0).Item("KodKursus")
                Else
                    lblKodKursus.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaKelas")) Then
                    lblNamaKelas.Text = ds.Tables(0).Rows(0).Item("NamaKelas")
                Else
                    lblNamaKelas.Text = ""
                End If
                'personal
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Nama")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    txtNama.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Mykad")) Then
                    txtMYKAD.Text = ds.Tables(0).Rows(0).Item("Mykad")
                    lblHidMykad.Text = ds.Tables(0).Rows(0).Item("Mykad")
                    lblMykad.Text = txtMYKAD.Text


                Else
                    txtMYKAD.Text = ""
                    lblMykad.Text = ""

                    lblHidMykad.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AngkaGiliran")) Then
                    lblAngkaGiliran.Text = ds.Tables(0).Rows(0).Item("AngkaGiliran")
                Else
                    lblAngkaGiliran.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jantina")) Then
                    chkJantina.Text = ds.Tables(0).Rows(0).Item("Jantina")
                Else
                    chkJantina.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kaum")) Then
                    ddlKaum.Text = ds.Tables(0).Rows(0).Item("Kaum")
                Else
                    ddlKaum.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Agama")) Then
                    chkAgama.Text = ds.Tables(0).Rows(0).Item("Agama")
                Else
                    chkAgama.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusID")) Then
                    ddlStatus.Text = ds.Tables(0).Rows(0).Item("StatusID")
                Else
                    ddlStatus.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JenisCalonID")) Then
                    ddlJenisCalon.Text = ds.Tables(0).Rows(0).Item("JenisCalonID")
                Else
                    ddlJenisCalon.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    txtEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    txtEmail.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Catatan")) Then
                    txtCatatan.Text = ds.Tables(0).Rows(0).Item("Catatan")
                Else
                    txtCatatan.Text = ""
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If updateData() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat pelajar."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Calon!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD Calon!"
            txtMYKAD.Focus()
            Return False
        End If

        '--changes made
        'If Not txtMYKAD.Text = lblMykad.Text Then
        '    strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        '    If oCommon.isExist(strSQL) = True Then
        '        lblMsg.Text = "MYKAD# telah digunakan. Sila masukkan MYKAD# yang baru."
        '        txtMYKAD.Focus()
        '        Return False
        '    End If
        'End If

        '--ddlAgama
        If chkAgama.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Agama!"
            chkAgama.Focus()
            Return False
        End If

        '--ddlJantina
        If chkJantina.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Jantina!"
            chkJantina.Focus()
            Return False
        End If

        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email Calon!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Calon tidak sah. (Contoh: Emel@contoh.com)"
            Return False
        End If
        Return True
    End Function

    Private Function kpmkv_pelajar_update() As Boolean
        strSQL = "UPDATE kpmkv_pelajar SET StatusID='" & ddlStatus.SelectedValue & "',JenisCalonID='" & ddlJenisCalon.SelectedValue & "',"
        strSQL += " Nama='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "', "
        strSQL += " Mykad='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "', "
        strSQL += " Agama='" & oCommon.FixSingleQuotes(chkAgama.Text) & "', "
        strSQL += " Kaum='" & oCommon.FixSingleQuotes(ddlKaum.Text) & "', "
        strSQL += " Jantina='" & oCommon.FixSingleQuotes(chkJantina.Text) & "', "
        strSQL += " Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "', "
        strSQL += " Catatan='" & oCommon.FixSingleQuotes(txtCatatan.Text.ToUpper) & "' "
        strSQL += " WHERE PelajarID='" & Request.QueryString("PelajarID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM kpmkv_pelajar  WHERE PelajarID='" & Request.QueryString("PelajarID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then

            strSQL = "DELETE FROM kpmkv_pelajar_markah WHERE PelajarID='" & Request.QueryString("PelajarID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            lblMsg.Text = "Berjaya meghapuskan rekod pelajar tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

    Private Function updateData() As Boolean

        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad = '" & lblHidMykad.Text & "'"
        strSQL += " AND KolejRecordID='" & lblHidKolej.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        Dim numrows As Integer

        numrows = MyTable.Rows.Count

        '' old data

        strSQL = " SELECT Nama, MyKad, Agama, Kaum, Jantina, Email, StatusID, JenisCalonID FROM kpmkv_pelajar WHERE PelajarID = '" & Request.QueryString("PelajarID") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        Dim ar_Pelajar As Array
        ar_Pelajar = strRet.Split("|")

        Dim strNama As String = ar_Pelajar(0)
        Dim strMyKad As String = ar_Pelajar(1)
        Dim strAgama As String = ar_Pelajar(2)
        Dim strKaum As String = ar_Pelajar(3)
        Dim strJantina As String = ar_Pelajar(4)
        Dim strEmail As String = ar_Pelajar(5)
        Dim strStatusID As String = ar_Pelajar(6)
        Dim strJenisCalonID As String = ar_Pelajar(7)

        '' old data

        Dim strPelajarID As String

        strPelajarID = ""

        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
        Dim UserID As String = oCommon.getFieldValue(strSQL)

        If numrows > 0 Then
            For i = 0 To numrows - 1

                strPelajarID = ds.Tables(0).Rows(i).Item("PelajarID")

                strSQL = "UPDATE kpmkv_pelajar SET "
                strSQL += " StatusID='" & ddlStatus.SelectedValue & "', "
                strSQL += " JenisCalonID='" & ddlJenisCalon.SelectedValue & "', "
                strSQL += " Nama='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "', "
                strSQL += " Mykad='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "', "
                strSQL += " Agama='" & oCommon.FixSingleQuotes(chkAgama.Text) & "', "
                strSQL += " Kaum='" & oCommon.FixSingleQuotes(ddlKaum.Text) & "', "
                strSQL += " Jantina='" & oCommon.FixSingleQuotes(chkJantina.Text) & "', "
                strSQL += " Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "' "
                strSQL += " WHERE PelajarID='" & strPelajarID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

            Next

            Dim strCatatan As String = ""

            If Not strNama = oCommon.FixSingleQuotes(txtNama.Text.ToUpper) Then
                strCatatan += "NAMA"
            End If
            If Not strMyKad = oCommon.FixSingleQuotes(txtMYKAD.Text) Then
                If strCatatan = "" Then
                    strCatatan += "MYKAD"
                Else
                    strCatatan += ", MYKAD"
                End If
            End If
            If Not strAgama = oCommon.FixSingleQuotes(chkAgama.Text) Then
                If strCatatan = "" Then
                    strCatatan += "AGAMA"
                Else
                    strCatatan += ", AGAMA"
                End If
            End If
            If Not strKaum = oCommon.FixSingleQuotes(ddlKaum.Text) Then
                If strCatatan = "" Then
                    strCatatan += "KAUM"
                Else
                    strCatatan += ", KAUM"
                End If
            End If
            If Not strJantina = oCommon.FixSingleQuotes(chkJantina.Text) Then
                If strCatatan = "" Then
                    strCatatan += "JANTINA"
                Else
                    strCatatan += ", JANTINA"
                End If
            End If
            If Not strEmail = oCommon.FixSingleQuotes(txtEmail.Text) Then
                If strCatatan = "" Then
                    strCatatan += "EMAIL"
                Else
                    strCatatan += ", EMAIL"
                End If
            End If
            If Not strStatusID = ddlStatus.SelectedValue Then
                If strCatatan = "" Then
                    strCatatan += "STATUS"
                Else
                    strCatatan += ", STATUS"
                End If
            End If
            If Not strJenisCalonID = ddlJenisCalon.SelectedValue Then
                If strCatatan = "" Then
                    strCatatan += "JENIS"
                Else
                    strCatatan += ", JENIS"
                End If
            End If
            If Not txtCatatan.Text = "" Then
                If strCatatan = "" Then
                    strCatatan += "CATATAN"
                Else
                    strCatatan += ", CATATAN"
                End If
            End If

            If Not strCatatan = "" Then

                strSQL = " INSERT INTO kpmkv_pelajar_changesHistory (PelajarID,KolejRecordID,Nama,Mykad,Agama,Kaum,Jantina,Email,ChangeDate, UserID, Catatan, CatatanKemaskini)"
                strSQL += " VALUES('" & Request.QueryString("PelajarID") & "','" & lblHidKolej.Text & "','" & oCommon.FixSingleQuotes(txtNama.Text) & "',"
                strSQL += " '" & txtMYKAD.Text & "','" & chkAgama.Text & "','" & ddlKaum.SelectedValue & "','" & chkJantina.Text & "',"
                strSQL += " '" & txtEmail.Text & "', GETDATE(), '" & UserID & "', '" & txtCatatan.Text & "', '" & strCatatan & "')"

                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        End If

        strRet = BindData(datRespondent)

        If strRet = True Then
            Return True
        Else
            Return False
        End If

    End Function


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

    Private Function getSQL() As String
        strSQL = "SELECT Mykad FROM kpmkv_pelajar WHERE PelajarID='" & Request.QueryString("PelajarID") & "'"
        Dim strMykad As String = oCommon.getFieldValue(strSQL)

        strSQL = "  SELECT A.ID, UPPER(B.LoginID) AS Audit, A.ChangeDate, A.Nama, A.Jantina, A.MYKAD, A.Kaum, A.Email, A.Agama, A.Catatan, A.CatatanKemaskini 
                    FROM kpmkv_pelajar_changesHistory A
                    LEFT JOIN kpmkv_users B ON B.UserID = A.UserID 
                    WHERE A.MYKAD = '" & strMykad & "'
                    GROUP BY A.ID, B.LoginID,  A.ChangeDate, A.Nama, A.Jantina, A.MYKAD, A.Kaum, A.Email, A.Agama, A.Catatan, A.CatatanKemaskini"

        getSQL = strSQL


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
        End Try
        con.Close()
        sda.Dispose()
        con.Dispose()

    End Function
End Class