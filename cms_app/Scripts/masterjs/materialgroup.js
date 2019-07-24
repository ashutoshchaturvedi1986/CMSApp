$(document).ready(function () {
    if (isshowList > 0)
        showList();
    if ($('#sltCompany option').length == 2) {
        $('#sltCompany option:last').attr("selected", "selected");
        $('#sltCompany').attr("disabled", "disabled");
    }

    $("#btnSave,#btnUpdate").click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        // Get All selected values and text of orgunit from dropdown
        var action = $(this).attr('data-action');
        var materialGroupId = 0;
        if ($('#hidId').length > 0)
            materialGroupId = $('#hidId').val();

        if (action == 'INSERT') {
            var msg = '';
            $('#sltMaterialGroup option').each(function () {
                if ($(this).text().trim().toUpperCase() == name.toUpperCase()) {
                    msg = 'This group is already created.';
                }
            });
            if (msg != '') {
                alert(msg);
                return;
            }
        }

        var prmdata = {
            prmMaterialGroupId: materialGroupId,
            prmMaterialGroupName: $('#txtName').val().trim(),
            prmMaterialGroupUnder: $('#sltMaterialGroup').val(),
            prmCompanyCode: $('#sltCompany').val(),
            prmRemarks: $('#txtRemarks').val().trim(),
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