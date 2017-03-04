/// <reference name="MicrosoftAjax.js"/>

function ValidateExcelFile(sender, args) {
    var strFileName = args.Value;
    var dotIndex = strFileName.lastIndexOf('.');
    if (dotIndex < 0) {
        args.IsValid = false;
    }
    else {
        args.IsValid = strFileName.substring(dotIndex, strFileName.length).toLowerCase() == '.xls';
        if (!args.IsValid)
            alert('Only Excel files are permitted!');
    }

    if (!args.IsValid)
        document.forms[0].reset();
    return;    
}

function uploadFile() {
    var frmFileUpload = window.frames['iframeUploadFile'].document.forms[0];
    if (frmFileUpload != null) {
        frmFileUpload.elements['fileToUpload'].click();
        frmFileUpload.elements['btnSubmit'].click();
    }
    else {
        alert('Δεν βρέθηκε η φόρμα για upload του αρχείου.');
    }
    return false;
}

// Καλείται από το iframe (xFileUpload.aspx) όταν ανέβει ένα αρχείο
function onUploadingFile() {
    if (window.parent != null) {
        window.parent.blockUI();
    }
}

function blockUI() {
    var uiBlock = document.getElementById('wz-ui-blocker');
    if (uiBlock)
        uiBlock.style.display = 'block';
    var uiBlockMsg = document.getElementById('wz-inprogress');
    if (uiBlockMsg)
        uiBlockMsg.style.display = 'block';
}

function releaseUI() {
    var uiBlock = document.getElementById('wz-ui-blocker');
    if (uiBlock != null)
        uiBlock.style.display = 'none';
    var uiBlockMsg = document.getElementById('wz-inprogress');
    if (uiBlockMsg != null)
        uiBlockMsg.style.display = 'none';
}

function toggleDiv(divID) {
    var div = document.getElementById(divID);
    if (div) {
        if (div.style.display == 'block')
            div.style.display = 'none';
        else
            div.style.display = 'block';
    }
    return false;
}

function getOffsetLeft(el) {
    var ol = el.offsetLeft;
    while ((el = el.offsetParent) != null)
        ol += el.offsetLeft;
    return ol;
}

function getOffsetTop(el) {
    var ot = el.offsetTop;
    while ((el = el.offsetParent) != null)
        ot += el.offsetTop;
    return ot;
}

function onFileUpload(tbFileUploadClientID, lbFileUploadPopulateUniqueID) {
    releaseUI();
    document.getElementById(tbFileUploadID).value = 'true';
    __doPostBack(lbFileUploadPopulateUniqueID, '');
}

function confirmCancelButton(cancelButtonID) {
    var cancelBtn = $get(cancelButtonID);
    if (cancelBtn == null)
        return;
    $clearHandlers(cancelBtn);
    $addHandler(cancelBtn, 'click', function() { return confirm('Θέλετε σίγουρα να ακυρώσετε τη διαδικασία; Το αρχείο που τυχόν ανεβάσατε θα διαγραφεί.'); });    
}

function onExceptionThrown(ex) {
    document.getElementById('wz-inprogress').style.display = 'none';
    document.getElementById('wz-file-upload-error').style.display = 'block';
    document.getElementById('err-message').innerHTML = ex;
}

function hideError() {
    document.getElementById('wz-file-upload-error').style.display = 'none';
    document.getElementById('wz-ui-blocker').style.display = 'none';
}

function showDiff(fileID, voucher2) {
    popupDiff.SetContentUrl(String.format('/import/ShowDiff.aspx?id={0}&voucher2={1}', fileID, voucher2));
    popupDiff.Show();
}