<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestCustom.ascx.cs" ClientIDMode="Static" Inherits="ExampleCriterion.CriterionBase.CustomItems.TestCustom" %>

<script type="text/javascript">
    
    function ActIOns(idHiddenField) {
        var str = '';
        $('#PanelCustom  input:checkbox:checked').each(function (i) {
            str = str + ((i == 0) ? '' : ',') + this.value;
        });
        $('#' + idHiddenField).val(str);
    }
</script>
<asp:Panel ID="PanelCustom" runat="server">
    
        <asp:CheckBoxList ID="cb" runat="server" >
        <asp:ListItem Value="1">Желтый</asp:ListItem>
        <asp:ListItem Value="2">Зеленый</asp:ListItem>
        <asp:ListItem Value="3">Крассный</asp:ListItem>
    </asp:CheckBoxList>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Panel>
