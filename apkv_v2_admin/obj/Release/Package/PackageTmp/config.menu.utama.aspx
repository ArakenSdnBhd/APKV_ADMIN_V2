﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.menu.utama.aspx.vb" Inherits="apkv_v2_admin.config_menu_utama2" %>
<%@ Register src="commoncontrol/config_menu_utama.ascx" tagname="config_menu_utama" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_menu_utama ID="config_menu_utama1" runat="server" />
</asp:Content>
