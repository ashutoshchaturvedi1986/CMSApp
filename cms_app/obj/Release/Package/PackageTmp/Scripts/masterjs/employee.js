$(document).ready(function () {
    if (isshowList > 0)
        showList();

    if ($('#sltCompany option').length == 2) {
        $('#sltCompany option:last').attr("selected", "selected");
        $('#sltCompany').attr("disabled", "disabled");
        //$("#sltCompany").change();
    }

    $("#sltCompany").change(function () {
        var code = $(this).val();
        $("#sltRole").empty().append('<option value="0">Select Role</option>');
        if (code != '0') {
            var URL = saveurl.replace('SaveResult','FillHierarchy');
            URL = URL + '?prmCompanyCode=' + code;
            changeDropDown(URL, "#sltRole", '', 'ROLE');
        }
    });

    toggelDiv($('.glyphicon-expand:eq(0)'));

    $("#btnSave,#btnUpdate").click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        // Get All selected values and text of orgunit from dropdown
        var code = $('#txtEmployeeCode').val().trim().toUpperCase();
        var action = $(this).attr('data-action');
        var empId = 0;
        if ($('#hidId').length > 0)
            empId = $('#hidId').val();

        if (action == 'INSERT') {
            if (code.split(' ').length > 1) {
                alert('Space not allowed in code.');
                return;
            }
            var emplist = $('#sltCompany option:selected').attr('data-child');
            if (emplist.toUpperCase().split(',').indexOf(code) > -1) {
                alert('This employee code is already used.');
                return;
            }
        }

        var prmdata = {
            prmEmpId: empId,
            prmCompanyCode: $('#sltCompany').val(),
            prmRole: $('#sltRole').val(),
            prmEmpCode: code,
            prmEmpPass: $('#txtEmployeePassword').val().trim(),
            prmEmailId: $('#inputEmail').val().trim(),
            prmPhone: $('#inputPhone').val().trim(),
            prmDOJ: $('#inputDOJ').val().trim(),
            prmDept: $('#inputDept').val().trim(),
            prmEmpName: $('#inputEmployeeName').val().trim(),
            prmFatherName: $('#inputFatherName').val().trim(),
            prmMaritalStatus: $('#sltMaritalStatus').val(),
            prmBlood: $('#sltBloodGroup').val(),
            prmAddress: $('#inputAddress').val().trim(),
            prmPANNo: $('#inputPANNo').val().trim(),
            prmAadharNo: $('#inputAadharNo').val().trim(),
            prmDOB: $('#inputDOB').val().trim(),
            prmRemark: $('#inputRemark').val().trim(),
            prmActive: $('#chkActive').prop('checked'),
            prmAction: action
        };

        $.ajax({
            type: "POST",
            url: sessionurl,
            dataType: "json",
            success: function (data) {
                if (data == "Yes") {
                    saveMasterDataForm(saveurl, prmdata, listurl);
                } else {
                    window.location.href = data;
                }
            }
        });
    });
});

function EditThis(obj) {
    var $tr = $(obj).closest('tr');
    var code = $tr.find('td:eq(0)').text();
    url_redirectForEdit(eurl, 'post', code);
}

function showList() {
    $.ajax({
        type: "POST",
        url: sessionurl,
        dataType: "json",
        success: function (data) {
            if (data == "Yes") {
                showMasterList(reporturl);
            } else {
                window.location.href = data;
            }
        }
    });
}