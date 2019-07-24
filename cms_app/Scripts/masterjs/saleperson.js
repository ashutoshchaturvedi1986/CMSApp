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
        var code = $('#txtCode').val().trim().toUpperCase();
        var action = $(this).attr('data-action');
        var salePersonId = 0;
        if ($('#hidId').length > 0)
            salePersonId = $('#hidId').val();

        if (action == 'INSERT') {
            if (code.split(' ').length > 1) {
                alert('Space not allowed in code.');
                return;
            }

            var salePersonListlist = $('#sltCompany option:selected').attr('data-child');
            if (salePersonListlist.toUpperCase().split(',').indexOf(code) > -1) {
                alert('This SalePerson code is already used.');
                return;
            }
        }
        var prmdata = {
            prmSalePersonId: salePersonId,
            prmSalePersonCode: code,
            prmSalePersonName: $('#txtName').val().trim(),
            prmCompanyCode: $('#sltCompany').val(),
            prmContactNo: $('#txtContactNo').val().trim(), 
            prmAddress: $('#txtAddress').val().trim(),
            prmDOB: $('#inputDOB').val().trim(), 
            prmPanNo: $('#txtPanNo').val().trim(), 
            prmAdharNo: $('#txtAdhar').val().trim(), 
            prmDOJ: $('#inputDOB').val().trim(),
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
