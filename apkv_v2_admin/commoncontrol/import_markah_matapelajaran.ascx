﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="import_markah_matapelajaran.ascx.vb" Inherits="apkv_v2_admin.import_markah_matapelajaran" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik >> Import Markah Akademik >> </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td >Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pentaksiran Akademik</td>
    </tr>
     <tr>
          <td >Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td >Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
     <tr>
          <td >Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>        
        <td >Kelas:</td>
          <td><asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
   <tr>
         <td>Markah: </td>
         <td><asp:CheckBoxList ID="chkResult" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
             <asp:ListItem Value="PB">Pentaksiran Berterusan</asp:ListItem>
             <asp:ListItem Value="PA">Penilaian Akhir</asp:ListItem>
             </asp:CheckBoxList>
         </td>
    </tr>
   </table>
<br />
<div id="divImport" runat="server">
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Import Markah Akademik</td>
    </tr>
    <tr>
         <td colspan ="2">MuatNaik Format Fail Excel:
         <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="116px" /></td>
    </tr>
    <tr>
         <td>Pilih Fail Excel:
        </td>
         <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
    </div> 

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    
</div>