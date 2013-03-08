<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ExampleCriterion.Index" %>
<%@ Register TagPrefix="cc1" Namespace="Criterion" Assembly="Criterion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Criterion</title>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css"/>
    <link href="CriterionBase/ContentCriterion/Criterion.css" rel="stylesheet" />
    <script src="CriterionBase/ScriptCriterion/Criterion.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div id="contnt">
        
        
        
        
        <div id="L1">
              <asp:Button ID="btBody" runat="server" Text="Body" Width="154px" OnClick="BtBodyClick"  />
                    <asp:Button ID="btTelephones" runat="server" Text="Telephones" Width="109px" OnClick="BtTelephonesClick" />
                   <a href="/Index.aspx?typename=Telephones">Telephones</a>
        </div>
         <div id="L2">
               <asp:Label ID="SqlText" runat="server" ></asp:Label>
                    <br/>
                    <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label>
         </div>
         <div id="L3">
              <cc1:CriterionForms ID="CriterionForms1"  runat="server"   /> 
         </div>
         <div id="L4">
             <input id="Submit1" type="submit" value="submit as Post" />
             <asp:Button ID="Button3" runat="server" Text="WebFormsSubmit" OnClick="Button3Click" style="height: 25px" />
         </div>
      
       
    </div>
    </form>
</body>
</html>
