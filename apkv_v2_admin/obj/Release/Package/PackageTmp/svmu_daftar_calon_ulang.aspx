<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_daftar_calon_ulang.aspx.vb" Inherits="apkv_v2_admin.svmu_daftar_calon_ulang" %>

<%@ Register Src="~/commoncontrol/svmu_daftar_calon_ulang.ascx" TagPrefix="uc1" TagName="svmu_daftar_calon_ulang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_daftar_calon_ulang runat="server" id="svmu_daftar_calon_ulang" />
</asp:Content>
