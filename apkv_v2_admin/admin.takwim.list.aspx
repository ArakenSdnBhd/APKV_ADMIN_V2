<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.takwim.list.aspx.vb" Inherits="apkv_v2_admin.admin_takwim_list" %>
<%@ Register src="commoncontrol/takwim_list.ascx" tagname="takwim_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <uc1:takwim_list ID="takwim_list1" runat="server" />
</asp:Content>
