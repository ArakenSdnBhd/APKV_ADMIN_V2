Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class jana_FIK_semasa1

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

                lblMsg.Text = ""

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

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
            ddlSemester.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging

        datRespondent.PageIndex = e.NewPageIndex

    End Sub

    Private Function getSQLsemasaSem123() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "  SELECT
                    kpmkv_pelajar.PelajarID,
                    kpmkv_pelajar.Nama,
                    kpmkv_pelajar.MYKAD,
                    kpmkv_pelajar.AngkaGiliran,
                    kpmkv_kursus.KodKursus,
                    kpmkv_pelajar.Semester,
                    kpmkv_negeri.Negeri,
                    kpmkv_status.Status,
                    kpmkv_pelajar.Jantina,
                    kpmkv_pelajar.Kaum,
                    kpmkv_pelajar.Agama,
                    kpmkv_kolej.Nama AS 'KolejVokasional',
                    kpmkv_kolej.Kod,
                    kpmkv_pelajar_markah.B_BahasaMelayu AS 'PB BM (100)',
                    kpmkv_pelajar_markah.B_BahasaInggeris AS 'PB BI (100)',
                    kpmkv_pelajar_markah.B_Mathematics AS 'PB MATH (100)',
                    kpmkv_pelajar_markah.B_Science1 AS 'PB SC (100)',
                    kpmkv_pelajar_markah.B_Sejarah AS 'PB SEJ (100)',
                    kpmkv_pelajar_markah.B_PendidikanIslam1 AS 'PB PI (100)',
                    kpmkv_pelajar_markah.B_PendidikanMoral AS 'PB PM (100)',
                    kpmkv_pelajar_markah.A_BahasaMelayu AS 'PA BM',
                    kpmkv_pelajar_markah.A_BahasaMelayu1 AS 'PA BM1',
                    kpmkv_pelajar_markah.A_BahasaMelayu2 AS 'PA BM2',
                    kpmkv_pelajar_markah.A_BahasaMelayu3 AS 'PA BM3',
                    kpmkv_pelajar_markah.A_BahasaInggeris AS 'PA BI (100)',
                    kpmkv_pelajar_markah.A_Science1 AS 'PA SC K1 (100)',
                    kpmkv_pelajar_markah.A_Science2 AS 'PA SC K2 (100)',
                    kpmkv_pelajar_markah.A_Science1 AS 'PA SC GABUNG (100)',
                    kpmkv_pelajar_markah.A_Mathematics AS 'PA MT (100)',
                    kpmkv_pelajar_markah.A_Sejarah AS 'PA SEJ (100)',
                    kpmkv_pelajar_markah.A_PendidikanIslam1 AS 'PA PI K1 (100)',
                    kpmkv_pelajar_markah.A_PendidikanIslam2 AS 'PA PI K2 (100)',
                    kpmkv_pelajar_markah.A_PendidikanMoral AS 'PA PM (100)',
                    kpmkv_pelajar_markah.BahasaMelayu AS 'MP BM (100)',
                    kpmkv_pelajar_markah.BahasaInggeris AS 'MP BI (100)',
                    kpmkv_pelajar_markah.Mathematics AS 'MP MATH (100)',
                    kpmkv_pelajar_markah.Science AS 'MP SC (100)',
                    kpmkv_pelajar_markah.Sejarah AS 'MP SEJ (100)',
                    kpmkv_pelajar_markah.PendidikanIslam AS 'MP PI (100)',
                    kpmkv_pelajar_markah.PendidikanMoral AS 'MP PM (100)',
                    kpmkv_pelajar_markah.GredBM AS 'GRED BM',
                    kpmkv_pelajar_markah.GredBI AS 'GRED BI',
                    kpmkv_pelajar_markah.GredMT AS 'GRED MT',
                    kpmkv_pelajar_markah.GredSC AS 'GRED SC',
                    kpmkv_pelajar_markah.GredSJ AS 'GRED SJ',
                    kpmkv_pelajar_markah.GredPI AS 'GRED PI',
                    kpmkv_pelajar_markah.GredPM AS 'GRED PM',
                    kpmkv_pelajar_markah.B_Teori1 AS 'PBT MODUL 1 (100)',
                    kpmkv_pelajar_markah.B_Amali1 AS 'PBA MODUL 1 (100)',
                    kpmkv_pelajar_markah.B_Teori2 AS 'PBT MODUL 2 (100)',
                    kpmkv_pelajar_markah.B_Amali2 AS 'PBA MODUL 2 (100)',
                    kpmkv_pelajar_markah.B_Teori3 AS 'PBT MODUL 3 (100)',
                    kpmkv_pelajar_markah.B_Amali3 AS 'PBA MODUL 3 (100)',
                    kpmkv_pelajar_markah.B_Teori4 AS 'PBT MODUL 4 (100)',
                    kpmkv_pelajar_markah.B_Amali4 AS 'PBA MODUL 4 (100)',
                    kpmkv_pelajar_markah.B_Teori5 AS 'PBT MODUL 5 (100)',
                    kpmkv_pelajar_markah.B_Amali5 AS 'PBA MODUL 5 (100)',
                    kpmkv_pelajar_markah.B_Teori6 AS 'PBT MODUL 6 (100)',
                    kpmkv_pelajar_markah.B_Amali6 AS 'PBA MODUL 6 (100)',
                    kpmkv_pelajar_markah.B_Teori7 AS 'PBT MODUL 7 (100)',
                    kpmkv_pelajar_markah.B_Amali7 AS 'PBA MODUL 7 (100)',
                    kpmkv_pelajar_markah.B_Teori8 AS 'PBT MODUL 8 (100)',
                    kpmkv_pelajar_markah.B_Amali8 AS 'PBA MODUL 8 (100)',

                    kpmkv_pelajar_markah.A_Teori1 AS 'PAT MODUL 1 (100)',
                    kpmkv_pelajar_markah.A_Amali1 AS 'PAA MODUL 1 (100)',
                    kpmkv_pelajar_markah.A_Teori2 AS 'PAT MODUL 2 (100)',
                    kpmkv_pelajar_markah.A_Amali2 AS 'PAA MODUL 2 (100)',
                    kpmkv_pelajar_markah.A_Teori3 AS 'PAT MODUL 3 (100)',
                    kpmkv_pelajar_markah.A_Amali3 AS 'PAA MODUL 3 (100)',
                    kpmkv_pelajar_markah.A_Teori4 AS 'PAT MODUL 4 (100)',
                    kpmkv_pelajar_markah.A_Amali4 AS 'PAA MODUL 4 (100)',
                    kpmkv_pelajar_markah.A_Teori5 AS 'PAT MODUL 5 (100)',
                    kpmkv_pelajar_markah.A_Amali5 AS 'PAA MODUL 5 (100)',
                    kpmkv_pelajar_markah.A_Teori6 AS 'PAT MODUL 6 (100)',
                    kpmkv_pelajar_markah.A_Amali6 AS 'PAA MODUL 6 (100)',
                    kpmkv_pelajar_markah.A_Teori7 AS 'PAT MODUL 7 (100)',
                    kpmkv_pelajar_markah.A_Amali7 AS 'PAA MODUL 7 (100)',
                    kpmkv_pelajar_markah.A_Teori8 AS 'PAT MODUL 8 (100)',
                    kpmkv_pelajar_markah.A_Amali8 AS 'PAA MODUL 8 (100)',

                    kpmkv_pelajar_markah.PBAV1 AS 'PB_MODUL1',
                    kpmkv_pelajar_markah.PBAV2 AS 'PB_MODUL2',
                    kpmkv_pelajar_markah.PBAV3 AS 'PB_MODUL3',
                    kpmkv_pelajar_markah.PBAV4 AS 'PB_MODUL4',
                    kpmkv_pelajar_markah.PBAV5 AS 'PB_MODUL5',
                    kpmkv_pelajar_markah.PBAV6 AS 'PB_MODUL6',
                    kpmkv_pelajar_markah.PBAV7 AS 'PB_MODUL7',
                    kpmkv_pelajar_markah.PBAV8 AS 'PB_MODUL8',

                    kpmkv_pelajar_markah.PAAV1 AS 'PA_MODUL1',
                    kpmkv_pelajar_markah.PAAV2 AS 'PA_MODUL2',
                    kpmkv_pelajar_markah.PAAV3 AS 'PA_MODUL3',
                    kpmkv_pelajar_markah.PAAV4 AS 'PA_MODUL4',
                    kpmkv_pelajar_markah.PAAV5 AS 'PA_MODUL5',
                    kpmkv_pelajar_markah.PAAV6 AS 'PA_MODUL6',
                    kpmkv_pelajar_markah.PAAV7 AS 'PA_MODUL7',
                    kpmkv_pelajar_markah.PAAV8 AS 'PA_MODUL8',

                    kpmkv_pelajar_markah.PBPAM1 AS 'MP MODUL 1',
                    kpmkv_pelajar_markah.PBPAM2 AS 'MP MODUL 2',
                    kpmkv_pelajar_markah.PBPAM3 AS 'MP MODUL 3',
                    kpmkv_pelajar_markah.PBPAM4 AS 'MP MODUL 4',
                    kpmkv_pelajar_markah.PBPAM5 AS 'MP MODUL 5',
                    kpmkv_pelajar_markah.PBPAM6 AS 'MP MODUL 6',
                    kpmkv_pelajar_markah.PBPAM7 AS 'MP MODUL 7',
                    kpmkv_pelajar_markah.PBPAM8 AS 'MP MODUL 8',

                    kpmkv_pelajar_markah.GredV1 AS 'GRED MODUL 1',
                    kpmkv_pelajar_markah.GredV2 AS 'GRED MODUL 2',
                    kpmkv_pelajar_markah.GredV3 AS 'GRED MODUL 3',
                    kpmkv_pelajar_markah.GredV4 AS 'GRED MODUL 4',
                    kpmkv_pelajar_markah.GredV5 AS 'GRED MODUL 5',
                    kpmkv_pelajar_markah.GredV6 AS 'GRED MODUL 6',
                    kpmkv_pelajar_markah.GredV7 AS 'GRED MODUL 7',
                    kpmkv_pelajar_markah.GredV8 AS 'GRED MODUL 8',

                    (ROUND(((CAST(kpmkv_pelajar_markah.B_Amali1 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali2 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali3 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali4 AS numeric) * 0.5) ),2)) AS 'TOTAL PB AMALI',
                    (ROUND(((CAST(kpmkv_pelajar_markah.A_Teori1 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori2 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori3 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori4 AS numeric) * 0.5) ),2)) AS 'TOTAL PB TEORI',
                    (ROUND(((CAST(kpmkv_pelajar_markah.B_Amali1 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali2 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali3 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.B_Amali4 AS numeric) * 0.5) ),2)) + (ROUND(((CAST(kpmkv_pelajar_markah.A_Teori1 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori2 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori3 AS numeric) * 0.5) + (CAST(kpmkv_pelajar_markah.A_Teori4 AS numeric) * 0.5) ),2)) AS 'TOTAL PB',
                    (ROUND((CAST(kpmkv_pelajar_markah.A_Amali1 AS numeric) * 0.1),2)) AS 'TOTAL PA AMALI',
                    (ROUND((CAST(kpmkv_pelajar_markah.A_Teori1 AS numeric) * 0.2),2)) AS 'TOTAL PA TEORI',
                    (ROUND((CAST(kpmkv_pelajar_markah.A_Amali1 AS numeric) * 0.1),2)) + (ROUND((CAST(kpmkv_pelajar_markah.A_Teori1 AS numeric) * 0.2),2)) AS 'TOTAL PA',

                    kpmkv_pelajar_markah.SMP_PB AS 'PB',
                    kpmkv_pelajar_markah.SMP_PAA AS 'PA AMALI',
                    kpmkv_pelajar_markah.SMP_PAT AS 'PA TEORI',
                    kpmkv_pelajar_markah.SMP_Total AS 'SKOR KURSUS',
                    kpmkv_pelajar_markah.SMP_Grade AS 'GRED KURSUS',

                    kpmkv_pelajar_markah.PNGBM AS 'PNG_BM',
                    kpmkv_pelajar_markah.PNGKBM AS 'PNGK_BM',
                    kpmkv_pelajar_markah.PNG_Akademik AS 'PNG_AKA',
                    kpmkv_pelajar_markah.PNGKA AS 'PNGK_AKA',
                    kpmkv_pelajar_markah.PNG_Vokasional AS 'PNG_VOK',
                    kpmkv_pelajar_markah.PNGKV AS 'PNGK_VOK',
                    kpmkv_pelajar_markah.PNGK AS 'PNGK',
                    kpmkv_pelajar_markah.PNGKK AS 'PNGKK'

                    FROM
                    kpmkv_pelajar_markah
                    LEFT JOIN kpmkv_pelajar ON kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID
                    LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_negeri ON kpmkv_negeri.Negeri = kpmkv_kolej.Negeri
                    LEFT JOIN kpmkv_status ON kpmkv_status.StatusID = kpmkv_pelajar.StatusID
                    WHERE
                    kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                    AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                    AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'
                    ORDER BY
                    kpmkv_kolej.Nama,
                    kpmkv_pelajar.Nama"

        getSQLsemasaSem123 = tmpSQL

        Return getSQLsemasaSem123

    End Function

    Private Function getSQLsemasaSem4() As String

        Dim tmpSQL As String = ""
        Dim tmpSQL2 As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        getSQLsemasaSem4 = "  SELECT DISTINCT
                        kpmkv_pelajar.PelajarID,
                        kpmkv_pelajar.Nama,
                        kpmkv_pelajar.MYKAD,
                        kpmkv_pelajar.AngkaGiliran,
                        kpmkv_kursus.KodKursus,
                        kpmkv_pelajar.Semester,
                        kpmkv_negeri.Negeri,
                        kpmkv_status.Status,
                        kpmkv_pelajar.Jantina,
                        kpmkv_pelajar.Kaum,
                        kpmkv_pelajar.Agama,
                        kpmkv_kolej.Nama AS 'KolejVokasional',
                        kpmkv_kolej.Kod,
                        kpmkv_pelajar_markah.B_BahasaMelayu AS 'PB BM (100)',
                        kpmkv_pelajar_markah.B_BahasaInggeris AS 'PB BI (100)',
                        kpmkv_pelajar_markah.B_Mathematics AS 'PB MATH (100)',
                        kpmkv_pelajar_markah.B_Science1 AS 'PB SC (100)',
                        kpmkv_pelajar_markah.B_Sejarah AS 'PB SEJ (100)',
                        kpmkv_pelajar_markah.B_PendidikanIslam1 AS 'PB PI (100)',
                        kpmkv_pelajar_markah.B_PendidikanMoral AS 'PB PM (100)',
                        kpmkv_pelajar_markah.A_BahasaMelayu AS 'PA BM',
                        kpmkv_pelajar_markah.A_BahasaMelayu1 AS 'PA BM1',
                        kpmkv_pelajar_markah.A_BahasaMelayu2 AS 'PA BM2',
                        kpmkv_pelajar_markah.A_BahasaMelayu3 AS 'PA BM3',
                        kpmkv_pelajar_markah.A_BahasaInggeris AS 'PA BI (100)',
                        kpmkv_pelajar_markah.A_Science1 AS 'PA SC K1 (100)',
                        kpmkv_pelajar_markah.A_Science2 AS 'PA SC K2 (100)',
                        kpmkv_pelajar_markah.A_Science1 AS 'Sains_Gabung',
                        kpmkv_pelajar_markah.A_Mathematics AS 'PA SC GABUNG (100)',
                        kpmkv_pelajar_markah.A_Sejarah AS 'PA SEJ (100)',
                        kpmkv_pelajar_markah.A_PendidikanIslam1 AS 'PA PI K1 (100)',
                        kpmkv_pelajar_markah.A_PendidikanIslam2 AS 'PA PI K2 (100)',
                        kpmkv_pelajar_markah.A_PendidikanMoral AS 'PA PM (100)',
                        kpmkv_pelajar_markah.BahasaMelayu AS 'MP BM (100)',
                        kpmkv_pelajar_markah.BahasaInggeris AS 'MP BI (100)',
                        kpmkv_pelajar_markah.Mathematics AS 'MP MATH (100)',
                        kpmkv_pelajar_markah.Science AS 'MP SC (100)',
                        kpmkv_pelajar_markah.Sejarah AS 'MP SEJ (100)',
                        kpmkv_pelajar_markah.PendidikanIslam AS 'MP PI (100)',
                        kpmkv_pelajar_markah.PendidikanMoral AS 'MP PM (100)',
                        kpmkv_pelajar_markah.GredBM AS 'GRED BM',
                        kpmkv_pelajar_markah.GredBI AS 'GRED BI',
                        kpmkv_pelajar_markah.GredMT AS 'GRED MT',
                        kpmkv_pelajar_markah.GredSC AS 'GRED SC',
                        kpmkv_pelajar_markah.GredSJ AS 'GRED SJ',
                        kpmkv_pelajar_markah.GredPI AS 'GRED PI',
                        kpmkv_pelajar_markah.GredPM AS 'GRED PM',
                        kpmkv_pelajar_markah.B_Teori1 AS 'PBT MODUL 1 (100)',
                        kpmkv_pelajar_markah.B_Amali1 AS 'PBA MODUL 1 (100)',
                        kpmkv_pelajar_markah.B_Teori2 AS 'PBT MODUL 2 (100)',
                        kpmkv_pelajar_markah.B_Amali2 AS 'PBA MODUL 2 (100)',
                        kpmkv_pelajar_markah.B_Teori3 AS 'PBT MODUL 3 (100)',
                        kpmkv_pelajar_markah.B_Amali3 AS 'PBA MODUL 3 (100)',
                        kpmkv_pelajar_markah.B_Teori4 AS 'PBT MODUL 4 (100)',
                        kpmkv_pelajar_markah.B_Amali4 AS 'PBA MODUL 4 (100)',
                        kpmkv_pelajar_markah.B_Teori5 AS 'PBT MODUL 5 (100)',
                        kpmkv_pelajar_markah.B_Amali5 AS 'PBA MODUL 5 (100)',
                        kpmkv_pelajar_markah.B_Teori6 AS 'PBT MODUL 6 (100)',
                        kpmkv_pelajar_markah.B_Amali6 AS 'PBA MODUL 6 (100)',
                        kpmkv_pelajar_markah.B_Teori7 AS 'PBT MODUL 7 (100)',
                        kpmkv_pelajar_markah.B_Amali7 AS 'PBA MODUL 7 (100)',
                        kpmkv_pelajar_markah.B_Teori8 AS 'PBT MODUL 8 (100)',
                        kpmkv_pelajar_markah.B_Amali8 AS 'PBA MODUL 8 (100)',
                        kpmkv_pelajar_markah.A_Teori1 AS 'PAT MODUL 1 (100)',
                        kpmkv_pelajar_markah.A_Amali1 AS 'PAA MODUL 1 (100)',
                        kpmkv_pelajar_markah.A_Teori2 AS 'PAT MODUL 2 (100)',
                        kpmkv_pelajar_markah.A_Amali2 AS 'PAA MODUL 2 (100)',
                        kpmkv_pelajar_markah.A_Teori3 AS 'PAT MODUL 3 (100)',
                        kpmkv_pelajar_markah.A_Amali3 AS 'PAA MODUL 3 (100)',
                        kpmkv_pelajar_markah.A_Teori4 AS 'PAT MODUL 4 (100)',
                        kpmkv_pelajar_markah.A_Amali4 AS 'PAA MODUL 4 (100)',
                        kpmkv_pelajar_markah.A_Teori5 AS 'PAT MODUL 5 (100)',
                        kpmkv_pelajar_markah.A_Amali5 AS 'PAA MODUL 5 (100)',
                        kpmkv_pelajar_markah.A_Teori6 AS 'PAT MODUL 6 (100)',
                        kpmkv_pelajar_markah.A_Amali6 AS 'PAA MODUL 6 (100)',
                        kpmkv_pelajar_markah.A_Teori7 AS 'PAT MODUL 7 (100)',
                        kpmkv_pelajar_markah.A_Amali7 AS 'PAA MODUL 7 (100)',
                        kpmkv_pelajar_markah.A_Teori8 AS 'PAT MODUL 8 (100)',
                        kpmkv_pelajar_markah.A_Amali8 AS 'PAA MODUL 8 (100)',
                        kpmkv_pelajar_markah.PBAV1 AS 'PB_MODUL1',
                        kpmkv_pelajar_markah.PBAV2 AS 'PB_MODUL2',
                        kpmkv_pelajar_markah.PBAV3 AS 'PB_MODUL3',
                        kpmkv_pelajar_markah.PBAV4 AS 'PB_MODUL4',
                        kpmkv_pelajar_markah.PBAV5 AS 'PB_MODUL5',
                        kpmkv_pelajar_markah.PBAV6 AS 'PB_MODUL6',
                        kpmkv_pelajar_markah.PBAV7 AS 'PB_MODUL7',
                        kpmkv_pelajar_markah.PBAV8 AS 'PB_MODUL8',
                        kpmkv_pelajar_markah.PAAV1 AS 'PA_MODUL1',
                        kpmkv_pelajar_markah.PAAV2 AS 'PA_MODUL2',
                        kpmkv_pelajar_markah.PAAV3 AS 'PA_MODUL3',
                        kpmkv_pelajar_markah.PAAV4 AS 'PA_MODUL4',
                        kpmkv_pelajar_markah.PAAV5 AS 'PA_MODUL5',
                        kpmkv_pelajar_markah.PAAV6 AS 'PA_MODUL6',
                        kpmkv_pelajar_markah.PAAV7 AS 'PA_MODUL7',
                        kpmkv_pelajar_markah.PAAV8 AS 'PA_MODUL8',
                        kpmkv_pelajar_markah.PBPAM1 AS 'MP MODUL 1',
                        kpmkv_pelajar_markah.PBPAM2 AS 'MP MODUL 2',
                        kpmkv_pelajar_markah.PBPAM3 AS 'MP MODUL 3',
                        kpmkv_pelajar_markah.PBPAM4 AS 'MP MODUL 4',
                        kpmkv_pelajar_markah.PBPAM5 AS 'MP MODUL 5',
                        kpmkv_pelajar_markah.PBPAM6 AS 'MP MODUL 6',
                        kpmkv_pelajar_markah.PBPAM7 AS 'MP MODUL 7',
                        kpmkv_pelajar_markah.PBPAM8 AS 'MP MODUL 8',
                        kpmkv_pelajar_markah.GredV1 AS 'GRED MODUL 1',
                        kpmkv_pelajar_markah.GredV2 AS 'GRED MODUL 2',
                        kpmkv_pelajar_markah.GredV3 AS 'GRED MODUL 3',
                        kpmkv_pelajar_markah.GredV4 AS 'GRED MODUL 4',
                        kpmkv_pelajar_markah.GredV5 AS 'GRED MODUL 5',
                        kpmkv_pelajar_markah.GredV6 AS 'GRED MODUL 6',
                        kpmkv_pelajar_markah.GredV7 AS 'GRED MODUL 7',
                        kpmkv_pelajar_markah.GredV8 AS 'GRED MODUL 8',
                        kpmkv_pelajar_markah.PNGBM AS 'PNG_BM',
                        kpmkv_pelajar_markah.PNGKBM AS 'PNGK_BM',
                        kpmkv_pelajar_markah.PNG_Akademik AS 'PNG_AKA',
                        kpmkv_pelajar_markah.PNGKA AS 'PNGK_AKA',
                        kpmkv_pelajar_markah.PNG_Vokasional AS 'PNG_VOK',
                        kpmkv_pelajar_markah.PNGKV AS 'PNGK_VOK',
                        kpmkv_pelajar_markah.PNGK AS 'PNGK',
                        kpmkv_pelajar_markah.PNGKK AS 'PNGKK',
                        kpmkv_pelajar_markah.GredBMSetara AS 'GREDBM',
                        (CASE WHEN kpmkv_pelajar_markah.GredBMSetara = 'G' OR kpmkv_pelajar_markah.GredBMSetara = 'T' THEN 'GAGAL' WHEN kpmkv_pelajar_markah.GredBMSetara IS NULL THEN '' ELSE 'LULUS' END) AS 'LULUS BM',
                        (CASE WHEN kpmkv_pelajar_markah.PNGKA > 2.0 THEN 'TRUE' ELSE 'FALSE' END) AS 'PNGK AKA > 2.0',
                        (CASE WHEN kpmkv_pelajar_markah.PNGK > 2.67 THEN 'TRUE' ELSE 'FALSE' END) AS 'PNGK > 2.67'
                        FROM
                        kpmkv_pelajar_markah
                        LEFT JOIN kpmkv_pelajar ON kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID
                        LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID
                        LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                        LEFT JOIN kpmkv_negeri ON kpmkv_negeri.Negeri = kpmkv_kolej.Negeri
                        LEFT JOIN kpmkv_status ON kpmkv_status.StatusID = kpmkv_pelajar.StatusID
                        WHERE
                        kpmkv_pelajar.PelajarID IS NOT NULL
                        AND kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'
                        ORDER BY
                        kpmkv_kolej.Nama,
                        kpmkv_pelajar.Nama"


        Return getSQLsemasaSem4

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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""

        updateNULL()

        If Not ddlSemester.SelectedValue = "4" Then

            ExportToCSV(getSQLsemasaSem123)

        Else

            ExportToCSV(getSQLsemasaSem4)

        End If

    End Sub

    Private Sub updateNULL()

        '' update empty field to NULL

        For i = 1 To 8

            strSQL = "  UPDATE kpmkv_pelajar_markah
                        SET B_Amali" & i & " = NULL
                        WHERE B_Amali" & i & " = ''
                        AND kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "  UPDATE kpmkv_pelajar_markah
                        SET B_Teori" & i & " = NULL
                        WHERE B_Teori" & i & " = ''
                        AND kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "  UPDATE kpmkv_pelajar_markah
                        SET A_Amali" & i & " = NULL
                        WHERE A_Amali" & i & " = ''
                        AND kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "  UPDATE kpmkv_pelajar_markah
                        SET A_Teori" & i & " = NULL
                        WHERE A_Teori" & i & " = ''
                        AND kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Semester = '" & ddlSemester.SelectedValue & "'
                        AND kpmkv_pelajar_markah.Sesi = '" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)


        Next


    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=janaFIKsemasa_Tahun" & ddlTahun.Text & "_Semester" & ddlSemester.Text & ".csv")
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

End Class