/// <reference name="MicrosoftAjax.js" />

Type.registerNamespace("Helpdesk");

Helpdesk.SchoolSearch = function () {

    var _schoolCodeHiddenFieldID = '';
    var _institutionNameLabelID = '';
    var _schoolNameLabelID = '';
    var _departmentNameLabelID = '';

    return {
        onSchoolSelected: function (selectedValues) {
            var hfSchoolCode = document.getElementById(_schoolCodeHiddenFieldID);
            var txtInstitution = document.getElementById(_institutionNameLabelID);
            var txtSchool = document.getElementById(_schoolNameLabelID);
            var txtDepartment = document.getElementById(_departmentNameLabelID);

            hfSchoolCode.value = selectedValues[0];
            txtInstitution.value = selectedValues[1];
            txtSchool.value = (selectedValues[2] == null ? '' : selectedValues[2]);
            txtDepartment.value = selectedValues[3];

            document.getElementById('lnkRemoveSchoolSelection').style.display = '';
            document.getElementById('lnkSelectSchool').style.display = 'none';
            popUp.hide();
        },

        removeSchoolSelection: function () {
            var hfSchoolCode = document.getElementById(_schoolCodeHiddenFieldID);
            var txtInstitution = document.getElementById(_institutionNameLabelID);
            var txtSchool = document.getElementById(_schoolNameLabelID);
            var txtDepartment = document.getElementById(_departmentNameLabelID);

            hfSchoolCode.value = '';
            txtInstitution.value = '';
            txtSchool.value = '';
            txtDepartment.value = '';

            document.getElementById('lnkRemoveSchoolSelection').style.display = 'none';
            document.getElementById('lnkSelectSchool').style.display = '';
        },

        init: function (hfSchoolCodeClientID, txtInstitutionNameClientID, txtSchoolNameClientID, txtDepartmentNameClientID) {
            _schoolCodeHiddenFieldID = hfSchoolCodeClientID;
            _institutionNameLabelID = txtInstitutionNameClientID;
            _schoolNameLabelID = txtSchoolNameClientID;
            _departmentNameLabelID = txtDepartmentNameClientID;

            if (document.getElementById(_schoolCodeHiddenFieldID) != null) {
                if (document.getElementById(_schoolCodeHiddenFieldID).value != '') {
                    if (document.getElementById('lnkRemoveSchoolSelection') != null) {
                        document.getElementById('lnkRemoveSchoolSelection').style.display = '';
                    }
                    if (document.getElementById('lnkSelectSchool') != null) {
                        document.getElementById('lnkSelectSchool').style.display = 'none';
                    }
                }
                else {
                    document.getElementById('lnkRemoveSchoolSelection').style.display = 'none';
                    document.getElementById('lnkSelectSchool').style.display = '';
                }
            }
        }
    };
} ();

var hd = Helpdesk.SchoolSearch;