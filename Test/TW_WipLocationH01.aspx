<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TW_WipLocationH01.aspx.cs" Inherits="TaiFlexMES.TW_WIP.TW_WipLocationH01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/TF_UserControl/TF_RuleControlBar.ascx" TagName="RuleControlBar" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/DataPageStyle.css" type="text/css" rel="Stylesheet" />

    <script src="../JavaScript/UIJScript.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 342px;
            height: 25px;
            FONT-FAMILY: Verdana, Arial, 'Sans Serif', Tahoma, Helvetica, 微軟正黑體;
        }
        .style2
        {
            height: 25px;
            text-align: right;
            FONT-FAMILY: Verdana, Arial, 'Sans Serif', Tahoma, Helvetica, 微軟正黑體;
            }
        .style3
        {
            height: 25px;
            text-align: right;
            FONT-FAMILY: Verdana, Arial, 'Sans Serif', Tahoma, Helvetica, 微軟正黑體;
            width: 307px;
        }
        .style4
        {
            width: 472px;
            height: 25px;
            text-align: right;
            FONT-FAMILY: Verdana, Arial, 'Sans Serif', Tahoma, Helvetica, 微軟正黑體;
        }
        .style5
        {
            width: 72px;
            height: 25px;
            text-align: right;
            FONT-FAMILY: Verdana, Arial, 'Sans Serif', Tahoma, Helvetica, 微軟正黑體;
        }
    </style>

</head>
<body class="CSBody">
    <form id="Scrap" runat="server" class="CSForm">
        <cc1:ToolkitScriptManager ID="ScriptManager" runat="server"></cc1:ToolkitScriptManager>

    <table class="CSMainTable">
        <tr>
            <td style="width: 100%; height: 41px">
                <uc1:RuleControlBar ID="cbDetail" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="CSMainDiv">
                    <table>
                        <tr>
                            <td class="style3">
                                <asp:RadioButton ID="Radio1" runat="server" Text="逐批輸入" GroupName="List" 
                                    Visible="False" />
                            </td>
                            <td class="style1">
                                <asp:RadioButton ID="Radio2" runat="server" Text="依包裝單帶出" GroupName="List" 
                                    Visible="False" />
                            </td>  
                            <td class="style4">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="CSFourCellTableContantTD">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btnApply" runat="server" Text="申請" OnClick="btnApply_Click" 
                                    Font-Size="X-Large" Width="104px" />
                                    </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <asp:Label ID="Label4" runat="server" Text="運送地點"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="Label5" runat="server" Text="起"></asp:Label>
                                <asp:DropDownList runat="server" BackColor="Yellow" 
                                    Height="23px" Width="65px" ID="Drop_FabFrom" AutoPostBack="True" 
                                    CssClass="CSMustControl">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>一廠</asp:ListItem>
                                    <asp:ListItem>二廠</asp:ListItem>
                                    <asp:ListItem>三廠</asp:ListItem>
                                    <asp:ListItem>五廠</asp:ListItem>
                                </asp:DropDownList>
                                    &nbsp;<asp:Label ID="Label6" runat="server" Text="~"></asp:Label>
                                <asp:Label ID="Label7" runat="server" Text="迄"></asp:Label>
                                <asp:DropDownList runat="server" BackColor="Yellow" Height="25px" Width="65px" 
                                    ID="Drop_FabTo" AutoPostBack="True" CssClass="CSMustControl">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>一廠</asp:ListItem>
                                    <asp:ListItem>二廠</asp:ListItem>
                                    <asp:ListItem>三廠</asp:ListItem>
                                    <asp:ListItem>五廠</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style4">
                                &nbsp;</td>
                            <td class="style5">
                                收貨單位</td>
                            <td class="CSFourCellTableContantTD">
                                <asp:DropDownList runat="server" BackColor="Yellow" 
                                    Height="27px" Width="151px" ID="Drop_Dept" AutoPostBack="True" OnTextChanged="Drop_Dept_SelectedIndexChanged"
                                    CssClass="CSMustControl">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>前製</asp:ListItem>
                                    <asp:ListItem>後製一課(三廠)</asp:ListItem>
                                    <asp:ListItem>後製二課(三廠)</asp:ListItem>
                                    <asp:ListItem>後製三課(二廠)</asp:ListItem>
                                    <asp:ListItem>包裝</asp:ListItem>
                                    <asp:ListItem>合成一課(一廠)</asp:ListItem>
                                    <asp:ListItem>合成一課(三廠)</asp:ListItem>
                                    <asp:ListItem>合成二課</asp:ListItem>
                                    <asp:ListItem>物管</asp:ListItem>
                                    <asp:ListItem>後製五課(三廠)</asp:ListItem>
                                    <asp:ListItem>後製</asp:ListItem>
                                    <asp:ListItem>C倉領料區(2廠)</asp:ListItem>
                                    <asp:ListItem>南倉領料區(3廠)</asp:ListItem>
                                    <asp:ListItem>冰庫</asp:ListItem>
                                    <asp:ListItem>合成一課危險倉</asp:ListItem>
                                    <asp:ListItem>合成二課危險倉</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td class="style1">
                                <asp:Label ID="Label_phone" runat="server" ForeColor="#0000CC"></asp:Label>
                                    </td>
                            <td class="style4">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="CSFourCellTableContantTD">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3" align="right">
                                自定群組</td>
                            <td class="style1" align="left">
                                <asp:TextBox ID="Text_Group" runat="server" BackColor="#99FF33" 
                                     Width="155px"></asp:TextBox>
                            </td>
                             <td class="style4">
                                 批號<asp:TextBox ID="Text_Lot" runat="server" BackColor="Yellow" Width="180px" AutoPostBack="True"
                                    OnTextChanged="Text_Lot_TextChanged"></asp:TextBox>
                                    </td>
                             <td class="style5">
                                 線別</td>
                            <td class="CSFourCellTableContantTD">
                                <asp:DropDownList runat="server" BackColor="Yellow" AutoPostBack="True" 
                                    Height="22px" Width="87px" ID="Drop_Machine" OnSelectedIndexChanged="Drop_Machine_SelectIndexChanged"
                                    CssClass="CSMustControl">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>T1</asp:ListItem>
                                    <asp:ListItem>T2</asp:ListItem>
                                    <asp:ListItem>T3</asp:ListItem>
                                    <asp:ListItem>T6</asp:ListItem>
                                    <asp:ListItem>T7</asp:ListItem>
                                    <asp:ListItem>T8</asp:ListItem>
                                    <asp:ListItem>T9</asp:ListItem>
                                    <asp:ListItem>T10</asp:ListItem>
                                    <asp:ListItem>T11</asp:ListItem>
                                    <asp:ListItem>T12</asp:ListItem>
                                    <asp:ListItem>T13</asp:ListItem>
                                    <asp:ListItem>C1</asp:ListItem>
                                    <asp:ListItem>C2</asp:ListItem>
                                    <asp:ListItem>C3</asp:ListItem>
                                    <asp:ListItem>C5</asp:ListItem>
                                    <asp:ListItem>C6</asp:ListItem>
                                    <asp:ListItem>C7</asp:ListItem>
                                    <asp:ListItem>L1</asp:ListItem>
                                    <asp:ListItem>L2</asp:ListItem>
                                    <asp:ListItem>L3</asp:ListItem>
                                    <asp:ListItem>L5</asp:ListItem>
                                    <asp:ListItem>L6</asp:ListItem>
                                    <asp:ListItem>L7</asp:ListItem>
                                    <asp:ListItem>L8</asp:ListItem>
                                    <asp:ListItem>L9</asp:ListItem>
                                    <asp:ListItem>L10</asp:ListItem>
                                    <asp:ListItem>L11</asp:ListItem>
                                    <asp:ListItem>L12</asp:ListItem>
                                </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label_Sum" runat="server" Text="共0筆" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                        </tr>
                        <tr>
                            <td class="style3" align="right">
                                發料單號</td>
                            <td class="style1" align="left">
                                <asp:TextBox ID="Text_ItemNo" runat="server" BackColor="#66FFFF" Width="155px" AutoPostBack="True"
                                    OnTextChanged="Text_ItemNo_TextChanged" Height="16px"></asp:TextBox>
                            </td>
                             <td class="style4">
                                 &nbsp;</td>
                             <td class="style5">
                                 &nbsp;</td>
                            <td class="CSFourCellTableContantTD">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" align="right" colspan="5">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:GridView ID="gvData" runat="server" AllowSorting="True" 
                                    AutoGenerateColumns="False" CssClass="mGrid" ForeColor="White" GridLines="None" 
                                    OnRowDeleting="gvData_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Face, Operate %>">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lbtnDelete" runat="server" CommandName="Delete" 
                                                    ImageUrl="~/Image/Toolbar-Del.png" ToolTip="<%$ Resources:Face, Delete %>" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LOT" 
                                            HeaderText="批號">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FAB_FROM" 
                                            HeaderText="起">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FAB_TO" 
                                            HeaderText="迄">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="H04_DEPT" 
                                            HeaderText="接收單位">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PACKAGE_NO" 
                                            HeaderText="包裝單號">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MACHINE" HeaderText="線別" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        </table>
                </div>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
