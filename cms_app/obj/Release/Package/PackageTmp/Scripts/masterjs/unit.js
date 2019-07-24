$(document).ready(function () {
    if ($('#sltCompany option').length == 2) {
        $('#sltCompany option:last').attr("selected", "selected");
        $('#sltCompany').attr("disabled", "disabled");
    }
    if (isshowList > 0)
        showList();

    $("#btnSave,#btnUpdate").click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        // Get All selected values and text of orgunit from dropdown
        var code = $('#txtCode').val().trim().toUpperCase();
        var action = $(this).attr('data-action');
        var unitId = 0;
        if ($('#hidId').length > 0)
            unitId = $('#hidId').val();

        if (action == 'INSERT') {
            if (code.split(' ').length > 1) {
                alert('Space not allowed in code.');
                return;
            }

            var unitList = $('#sltCompany option:selected').attr('data-child');
            if (unitList.toUpperCase().split(',').indexOf(code) > -1) {
                alert('This unit code is already used.');
                return;
            }
        }

        var prmdata = {
            prmUnitId: unitId,
            prmCompanyCode: $('#sltCompany').val(),
            prmUnitCode: code,
            prmName: $('#txtName').val().trim(),
            prmDecimal: $('#txtDecimal').val().trim(),
            prmRemark: $('#txtRemarks').val().trim(),
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
    url_redirectForEdit(eurl, 'post', code, compcode);
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