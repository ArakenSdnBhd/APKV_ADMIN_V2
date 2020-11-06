﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kursus_search.ascx.vb" Inherits="apkv_v2_admin.kursus_search" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian Dan Penyelenggaraan >> Program</td>
    </tr>
</table>
<br />
<table class="fbform">
     <tr>
        <td style="width: 20%;">Kohort:</td>
         <td>
            <asp:DropDownList ID="ddlTahun" AutoPostBack="true" runat="server"  Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td style="width: 20%;">Nama Bidang:</td>
         <td> <asp:DropDownList ID="ddlKluster" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
         <td style="width: 20%;">Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
     <tr>
       <td style="width: 20%;">Jenis Program:</td>
        <td><asp:CheckBoxList ID="chkJenisProgram" runat="server"  Width="350px" AutoPostBack="true" RepeatDirection="Horizontal">
             <asp:ListItem>SOCIAL</asp:ListItem>
             <asp:ListItem>TEKNOLOGI</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
         <td style="width: 20%;">Kod Program:</td>
         <td><asp:DropDownList ID="ddlKodKursus" runat="server" Width="350px"></asp:DropDownList></td>
        </tr>
    <tr>
         <td style="width: 20%;">Nama Program:</td>
         <td><asp:TextBox ID="txtNamaKursus" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
   
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Program
        </td>
    </tr>
    <tr>
         <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KursusID"
                Width="100%" PageSize="25" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="KursusID" runat="server" visible="false" Text='<%# Bind("KursusID")%>'></asp:Label>
                        </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="NamaKursus" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>                   
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
                    <asp:CommandField DeleteImageUrl="~/img/warning.png" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
  <%--  <tr>
        <td>Tahun:
            <asp:DropDownList ID="ddlTahunTransfer" runat="server" Width="150px">
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnTransfer" runat="server" Text="Transfer Kluster" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>--%>
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>
