<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="privilege.aspx.cs" Inherits="WebSite.admin.Privilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="/css/Global.css" />
    <link rel="stylesheet" href="/themes/default/easyui.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="/themes/icon.css" />
    <script src="/js/jquery.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script src="/js/easyui-lang-zh_CN.js"></script>
    <script src="/js/cnvp.js"></script>
    <script src="/js/jQuery.query.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="privi-list">
                    <input type="checkbox" id="check_all1<%#Eval("ID") %>" value="<%#Eval("ID") %>" <%#Checked(Eval("ID").ToString(), Privilist) %> name="checkall" />
                    <label for="check_all1<%#Eval("ID") %>"><%#Eval("Module") %></label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    
    </form>
</body>
</html>
