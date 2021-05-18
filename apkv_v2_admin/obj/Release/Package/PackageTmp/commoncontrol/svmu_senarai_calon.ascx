﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_senarai_calon.ascx.vb" Inherits="apkv_v2_admin.svmu_senarai_calon" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Calon SVMU</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian
        </td>
    </tr>

    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Kod Pusat:</td>
        <td>
            <asp:DropDownList ID="ddlKodPusat" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Label ID="lblNamaPusat" runat="server" Width="500px"></asp:Label>
    </tr>

    <tr>
        <td>Tahun Peperiksaan SVMU:</td>
        <td>
            <asp:DropDownList ID="ddlTahunPeperiksaanSVMU" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    

    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
         </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Calon SVMU </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="svmu_calon_id"
                Width="100%" PageSize="40" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Angka Giliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kod Pusat">
                        <ItemTemplate>
                            <asp:Label ID="Kod" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     

                    <asp:TemplateField HeaderText="Mata Pelajaran">
                        <ItemTemplate>
                            <asp:Label ID="MataPelajaran" runat="server" Text='<%# Bind("MataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                    
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     

                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />

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
    <tr>
        <td class="fbform_sap" colspan="5">&nbsp;</td>
    </tr>

</table>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>
