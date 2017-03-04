<%@ Page Language="C#" MasterPageFile="~/Browse/Browse.Master" AutoEventWireup="true" CodeBehind="ContactForm.aspx.cs" Inherits="Eudoxus.Portal.Browse.ContactForm" Title="Υποβολή Ερωτήματος" %>

<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:MultiView ID="mvContact" runat="server" ActiveViewIndex="0">
        <asp:View ID="vSend" runat="server">
            <asp:ScriptManagerProxy runat="server" ID="sm">
                <CompositeScript>
                    <Scripts>
                        <asp:ScriptReference Path="~/_js/popUp1.js" />
                        <asp:ScriptReference Path="~/_js/SchoolSearch.js" />
                    </Scripts>
                </CompositeScript>
            </asp:ScriptManagerProxy>
            <dxpc:ASPxPopupControl ID="dxpcPopup" runat="server" ClientInstanceName="devExPopup" Width="500" Height="580" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="true"
                CloseAction="CloseButton">
            </dxpc:ASPxPopupControl>
            <table width="100%" class="dv">
                <tr>
                    <th style="width: 100px">
                        Ονοματεπώνυμο:
                    </th>
                    <td>
                        <asp:TextBox ID="txtReporterName" runat="server" Width="88%" onkeyup="Imis.Lib.ToUpper"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvReporterName" runat="server" ControlToValidate="txtReporterName" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        Τηλέφωνο:
                    </th>
                    <td>
                        <asp:TextBox ID="txtReporterPhone" runat="server" MaxLength="10" Width="88%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvReporterPhone" runat="server" ControlToValidate="txtReporterPhone" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revReporterPhone" runat="server" ControlToValidate="txtReporterPhone" Display="Static" ValidationGroup="Contact" ValidationExpression="^(2[0-9]{9})|(69[0-9]{8})$" ErrorMessage="Πρέπει να αποτελείται από ακριβώς 10 ψηφία και να ξεκινάει από 2 αν πρόκειται για σταθερό ή από 69 αν πρόκειται για κινητό."><img src="/_img/error.gif" title="Μη έγκυρος αριθμός τηλεφώνου" /></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        E-mail:
                    </th>
                    <td>
                        <asp:TextBox ID="txtReporterEmail" runat="server" Width="88%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvReporterEmail" runat="server" ControlToValidate="txtReporterEmail" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revReporterEmail" runat="server" ControlToValidate="txtReporterEmail" Display="Dynamic" ValidationGroup="Contact" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Το E-mail δεν είναι έγκυρο" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        Είδος Χρήστη:
                    </th>
                    <td align="left">
                        <asp:DropDownList ID="ddlReporterType" runat="server" OnInit="ddlReporterType_Init" Width="88%" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ID="rfvReporterType" runat="server" ControlToValidate="ddlReporterType" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            
            <script type="text/javascript">
                $(function () {
                    $('#ddlReporterType').change(function () {
                        if (($(this).val() == '<%= Eudoxus.BusinessModel.enReporterType.Student.ToString("D") %>') ||
                            ($(this).val() == '<%= Eudoxus.BusinessModel.enReporterType.Secretary.ToString("D") %>')) {
                            $('#tbSchool').show();
                        }
                        else
                            $('#tbSchool').hide();
                    });
                        if (($(this).val() == '<%= Eudoxus.BusinessModel.enReporterType.Student.ToString("D") %>') ||
                            ($(this).val() == '<%= Eudoxus.BusinessModel.enReporterType.Secretary.ToString("D") %>')) {
                        $('#tbSchool').show();
                    }
                    else
                        $('#tbSchool').hide();
                });

                function validateSchool(s, args) {
                    if (($('#ddlReporterType').val() == '<%= Eudoxus.BusinessModel.enReporterType.Student.ToString("D") %>') ||
                        ($('#ddlReporterType').val() == '<%= Eudoxus.BusinessModel.enReporterType.Secretary.ToString("D") %>'))
                        args.IsValid = $('#txtInstitutionName').val() != '';
                    else
                        args.IsValid = true;
                }
            </script>
            <table class="dv" width="100%" id="tbSchool" style="display: none;border:none">
                <tr>
                    <th style="width: 100px">
                        Ίδρυμα:
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtInstitutionName" runat="server" Width="88%" ClientIDMode="Static" />
                        <asp:CustomValidator ID="cvValidateSchool" ClientValidationFunction="validateSchool" ErrorMessage="Tο πεδίο 'Ίδρυμα' είναι υποχρεωτικό" runat="server" Display="Static" ValidationGroup="Contact"
                        OnServerValidate="cvValidateSchool_ServerValidate">
                            <img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" />
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        Σχολή:
                    </th>
                    <td>
                        <asp:TextBox ID="txtSchoolName" runat="server" Width="88%" />
                        <a href="javascript:void(0);" id="lnkSelectSchool" onclick="popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');">
                            <img id="Img1" runat="server" align="absmiddle" src="~/_img/iconSelectSchool.png" alt="Επιλογή Σχολής" title="Επιλογή Σχολής" /></a> <a href="javascript:void(0);" id="lnkRemoveSchoolSelection" onclick="return hd.removeSchoolSelection();"
                                style="display: none;">
                                <img id="Img2" runat="server" align="absmiddle" src="~/_img/iconRemoveSchool.png" alt="Αφαίρεση Σχολής" title="Αφαίρεση Σχολής" /></a>
                        <asp:HiddenField ID="hfSchoolCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        Τμήμα:
                    </th>
                    <td>
                        <asp:TextBox ID="txtDepartmentName" runat="server" Width="88%" />
                    </td>
                </tr>
            </table>
            <table class="dv" width="100%">
                <tr>
                    <th style="width: 100px">
                        Είδος Αναφοράς:
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlIncidentType" runat="server" Width="88%" />
                        <ajaxToolkit:CascadingDropDown ID="cddIncidentType" runat="server" TargetControlID="ddlIncidentType" ParentControlID="ddlReporterType" Category="IncidentTypes" PromptText="-- επιλέξτε πηγή αναφοράς --"
                            ServicePath="~/PortalServices/Services.asmx" ServiceMethod="GetIncidentTypes" LoadingText="Παρακαλω περιμένετε">
                        </ajaxToolkit:CascadingDropDown>
                        <asp:RequiredFieldValidator ID="rfvIncidentType" runat="server" ControlToValidate="ddlIncidentType" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px">
                        Κείμενο:
                    </th>
                    <td>
                        <asp:TextBox ID="txtReportText" runat="server" TextMode="MultiLine" Height="100px" Width="88%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvfReportText" runat="server" ControlToValidate="txtReportText" Display="Static" ValidationGroup="Contact"><img src="/_img/error.gif" title="Το πεδίο είναι υποχρεωτικό" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:LinkButton ID="lnkSend" runat="server" Text="Αποστολή" CssClass="icon-btn bg-email" OnClick="lnkSend_Click" ValidationGroup="Contact" />
            </div>
            <div align="center">
                <asp:Literal runat="server" ID="ltlError" Visible="false" Text="Παρουσιάστηκε ένα σφάλμα κατα την αποστόλή του E-mail. Παρακαλώ ξαναπροσπαθήστε."></asp:Literal>
            </div>
            <div style="background: #DDDDDD none repeat scroll 0 0; border: 1px solid #636F21; display: block; font-style: italic; margin-top: 20px; margin-bottom: 8px; padding: 5px; text-align: justify;">
                Εναλλακτικά μπορείτε να επικοινωνήσετε με το Γραφείο Αρωγής της δράσης στο τηλέφωνο <b>215 215 7850</b>. Ώρες λειτουργίας Δευτέρα με Παρασκευή <b>09:00 πμ - 17:00 μμ</b>
            </div>
        </asp:View>
        <asp:View ID="vComplete" runat="server">
            <b>Ευχαριστούμε για την επικοινωνία σας.<br />
                <br />
                Θα λάβετε σύντομα την απάντηση στο e-mail σας.</b>
        </asp:View>
        <asp:View ID="vOnlineReportsNotAllowed" runat="server">
            <div style="background: #DDDDDD none repeat scroll 0 0; border: 1px solid #636F21; display: block; font-weight: bold; margin-top: 20px; margin-bottom: 8px; padding: 5px; text-align: center;">
                Για πληροφορίες μπορείτε να απευθύνεστε στο τηλέφωνο 215 215 7850
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
