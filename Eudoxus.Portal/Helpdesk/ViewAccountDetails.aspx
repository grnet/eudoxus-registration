<%@ Page Title="" Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true"
    CodeBehind="ViewAccountDetails.aspx.cs" Inherits="Eudoxus.Portal.Helpdesk.ViewAccountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <script src="/_js/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="/_js/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <%--<script src="/_js/jquery-impromptu.1.8.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var btnEmail;
        var btnActivate;
        var btnUnlock;
        var emailError;
        var activationError;
        var unlockError;

        $.fn.extend({
            dropIn: function (speed, callback) {
                var $t = $(this);

                if ($t.css("display") == "none") {
                    eltop = $t.css('top');
                    elouterHeight = $t.outerHeight(true);

                    $t.css({ top: -elouterHeight, display: 'block' }).animate({ top: eltop }, speed, 'swing', callback);
                }
            }
        });
        $(function () {
            btnEmail = $('#btnEmail');
            btnActivate = $('#btnActivate');
            btnUnlock = $('#btnUnlock');
            emailError = $('#emailError');
            activationError = $('#activationError');
            unlockError = $('#unlockError');

            if (btnEmail.length != 0) {
                btnEmail.click(function () {
                    var onblur = "$(this).removeClass('focused');";
                    var onfocus = "$(this).addClass('focused');";
                    var txt = 'Νέο Email: <input type="text" class="tb" onfocus="' + onfocus + '" onblur="' + onblur + '" id="email" name="email" value="" />';
                    var dialog = $('<div>' + txt + '</div>')
              .dialog({
                  modal: true,
                  resizable: false,
                  draggable: true,
                  width: 380,
                  title: 'Αλλαγή Email',
                  show: 'fade',
                  hide: 'fade',
                  dialogClass: 'main-dialog-class',
                  buttons: {
                      "Αλλαγή Email": function () {
                          beginChangeEmail(dialog);
                          dialog.dialog('close');
                      },
                      "Ακύρωση": function () {
                          dialog.dialog('close');
                      }
                  },
                  open: function () {
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(1)').button({
                          icons: { primary: 'bg-accept' }
                      });
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(2)').button({
                          icons: { primary: 'bg-cancel' }
                      });
                  }
              });
                    return false;
                });
            }

            if (btnActivate.length != 0) {
                btnActivate.click(function () {
                    btnActivate.addClass('bg-loading');
                    begin(activation);
                    return false;
                });
            }

            if (btnUnlock.length != 0) {
                btnUnlock.click(function () {
                    btnUnlock.addClass('bg-loading');
                    begin(unlock);
                    return false;
                });
            }
        });

        function beginChangeEmail(m) {
            var newEmail = m.find('#email').val();
            if (!newEmail.match(/^([a-zA-Z0-9_\-])+(\.([a-zA-Z0-9_\-])+)*@((\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\]))|((([a-zA-Z0-9])+(([\-])+([a-zA-Z0-9])+)*\.)+([a-zA-Z])+(([\-])+([a-zA-Z0-9])+)*))$/)) {
                showError(emailError, 'To Email ' + newEmail + ' δεν είναι έγκυρο');
                return;
            }
            var userContext = {};
            userContext.errorArea = emailError;
            userContext.btn = btnEmail;
            userContext.keepBtnVisible = true;
            userContext.email = newEmail;
            userContext.errorMessage = 'Προκλήθηκε σφάλμα. Παρακαλούμε δοκιμάστε ξανά αργότερα.';
            userContext.successMessage = 'Η αλλαγή του e-mail ολοκληρώθηκε επιτυχώς';
            userContext.failMessage = 'Το email ' + newEmail + ' χρησιμοποιείται και δεν μπορεί να πραγματοποιηθεί η αλλαγή.';
            PageMethods.ChangeEmail(_USERNAME, newEmail, _REPORTERID, _REPORTERTYPE, pageMethodCompleted, onFailed, userContext);
        }

        function begin(fn) {
            setTimeout(fn, 1000);
        }

        function activation() {
            var userContext = {};
            userContext.btn = btnActivate;
            userContext.errorArea = activationError;
            userContext.activation = 'Ενεργοποιημένος';
            userContext.errorMessage = 'Προκλήθηκε σφάλμα. Παρακαλούμε δοκιμάστε ξανά αργότερα.';

            PageMethods.ActivateUser(_USERNAME, _REPORTERID, _REPORTERTYPE, pageMethodCompleted, onFailed, userContext)
        }


        function unlock() {
            var userContext = {};
            userContext.btn = btnUnlock;
            userContext.errorArea = unlockError;
            userContext.unlock = 'ΟΧΙ';
            userContext.errorMessage = 'Προκλήθηκε σφάλμα. Παρακαλούμε δοκιμάστε ξανά αργότερα.';

            PageMethods.UnlockUser(_USERNAME, pageMethodCompleted, onFailed, userContext)
        }


        //Helpers
        function onFailed(args, userContext) {
            userContext.btn.removeClass('bg-loading');
            showError(userContext.btn.next(), userContext.errorMessage);
        }

        function pageMethodCompleted(args, userContext) {
            if (args != null) {
                if (args) {
                    if (userContext.successMessage) {
                        showSuccess(userContext.errorArea, userContext.successMessage);
                    }
                    if (userContext.email) {
                        if ($('#txtEmail').length != 0) {
                            $('#txtEmail').val(userContext.email);
                        }
                        if ($('#ltrEmail').length != 0) {
                            $('#ltrEmail').html(userContext.email);
                        }
                    }
                    else if (userContext.activation) {
                        if ($('#ltrIsActivated').length != 0) {
                            $('#ltrIsActivated').html(userContext.activation);
                        }
                    }
                    else if (userContext.unlock) {
                        if ($('#ltrIsLockedOut').length != 0) {
                            $('#ltrIsLockedOut').html(userContext.unlock);
                        }
                    }
                    if (!userContext.keepBtnVisible)
                        userContext.btn.fadeOut();
                }
                else {
                    userContext.btn.removeClass('bg-loading');
                    showError(userContext.errorArea, userContext.failMessage);
                }
            }
            else {
                userContext.btn.removeClass('bg-loading');
                showError(userContext.errorArea, userContext.errorMessage);
            }
        }

        function showSuccess(element, message) {
            var initText = element.text();
            element.html(initText + '<span style="color:green;"> (' + $('<div/>').html(message).text() + ')</span>');
            setTimeout(function () {
                element.children().fadeOut('normal', function () {
                    element.children().remove();
                });
            }, 4000);
        }

        function showError(element, message) {
            var initText = element.text();
            element.html(initText + '<span style="color:red"> (' + $('<div/>').html(message).text() + ')</span>');
            setTimeout(function () {
                element.children().fadeOut('normal', function () {
                    element.children().remove();
                });
            }, 4000);
        }

    </script>
    <style type="text/css">
        .dv
        {
            width: 440px;
        }
        .dv tr
        {
            height: 35px;
        }
        .icon-btn
        {
            margin-left: 10px;
        }
        .img-btn
        {
            border: dashed 1px gray;
            padding: 2px;
            background-repeat: no-repeat;
            background-position: 2px 3px;
            margin-right: 15px;
            outline: none;
        }
    </style>
    <table class="dv" style="width: 100%">
        <tr>
            <th style="width: 85px">
                Username:
            </th>
            <td>
                <asp:Label runat="server" ID="ltrUsername" ForeColor="Blue" />
            </td>
        </tr>
        <tr>
            <th>
                Email:
            </th>
            <td>
                <a id="btnEmail" runat="server" href="#" title="Αλλαγή Email" class="img-btn bg-emailEdit"
                    clientidmode="Static">
                    <img alt="" src="/_img/s.gif" style="width: 16px; height: 16px" /></a>
                <asp:Label ID="ltrEmail" runat="server" ForeColor="Blue" ClientIDMode="Static" />
                <span id="emailError" />
            </td>
        </tr>
        <tr>
            <th>
                Κατάσταση:
            </th>
            <td>
                <asp:PlaceHolder runat="server" ID="phActivate"><a href="#" title="Ενεργοποίηση Χρήστη"
                    class="img-btn bg-accept" id="btnActivate">
                    <img alt="" src="/_img/s.gif" style="width: 16px; height: 16px" /></a></asp:PlaceHolder>
                <asp:Label ID="ltrIsActivated" runat="server" ForeColor="Blue" ClientIDMode="Static" />
                <span id="activationError" />
            </td>
        </tr>
        <tr>
            <th>
                Κλειδωμένος:
            </th>
            <td>
                <asp:PlaceHolder ID="phIsLocked" runat="server"><a id="btnUnlock" href="#" title="Ξεκλείδωμα Χρήστη"
                    class="img-btn bg-unlock">
                    <img alt="" src="/_img/s.gif" style="width: 16px; height: 16px" /></a> </asp:PlaceHolder>
                <asp:Label ID="ltrIsLockedOut" runat="server" ForeColor="Blue" ClientIDMode="Static" />
                <span id="unlockError" />
            </td>
        </tr>
    </table>
</asp:Content>
