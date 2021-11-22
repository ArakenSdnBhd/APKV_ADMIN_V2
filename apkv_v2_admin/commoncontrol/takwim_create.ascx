﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="takwim_create.ascx.vb" Inherits="apkv_v2_admin.takwim_create" %>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

<script type="text/javascript">
    $(function () {
        $("[id$=txtDate]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });
        $("[id$=txtDateTo]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });

        $("[id$=txtTarikhMulaNotifikasi]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });
        $("[id$=txtTarikhAkhirNotifikasi]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });
    });
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Takwim Baru</td>
    </tr>

    <!-- 11 NOV 2021 -->
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkSelectAll" runat="server" Text="PILIH SEMUA KOLEJ" AutoPostBack="true" />

        </td>
    </tr>

    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" Width="450px" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Jenis Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Width="450px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Kolej:</td>
        <td>
            <asp:CheckBoxList ID="chkBLKolej" runat="server" Width="450px" BorderColor="Black"></asp:CheckBoxList>
        </td>
    </tr>

    <!-- 11 NOV 2021 -->

    <tr>
        <td>Menu:</td>
        <td>
            <asp:DropDownList ID="ddlMenu" runat="server" Width="150px" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>SubMenu:</td>
        <td>
            <asp:DropDownList ID="ddlSubMenu" runat="server" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Sesi Pengambilan:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Tarikh Mula:</td>
        <td>
            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true"></asp:TextBox></td>

    </tr>
    <tr>
        <td>Tarikh Akhir:</td>
        <td>
            <asp:TextBox ID="txtDateTo" runat="server" ReadOnly="true"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Tajuk:</td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="450px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Catatan:</td>
        <td>
            <asp:TextBox ID="txtCatatan" runat="server" TextMode="MultiLine" Rows="10" Width="450px"></asp:TextBox>&nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Takwim</asp:LinkButton>
        </td>
    </tr>

</table>

<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Notifikasi Baru</td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td>
            <asp:DropDownList ID="ddlTahunNotifikasi" runat="server" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Tarikh Mula:</td>
        <td>
            <asp:TextBox ID="txtTarikhMulaNotifikasi" runat="server" ReadOnly="true"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Tarikh Akhir:</td>
        <td>
            <asp:TextBox ID="txtTarikhAkhirNotifikasi" runat="server" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tajuk:</td>
        <td>
            <asp:TextBox ID="txtTajukNotifikasi" runat="server" Width="450px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Catatan:</td>
        <td>
            <asp:TextBox ID="txtCatatanNotifikasi" runat="server" TextMode="MultiLine" Rows="10" Width="450px"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnTambahNotifikasi" runat="server" Text=" Tambah " CssClass="fbbutton" />
        </td>
    </tr>

</table>
