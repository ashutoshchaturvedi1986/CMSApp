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
        $("#sltDeptHead").empty().append('<option value="0">Select Head</option>');
        if (code != '0') {
            var URL = saveurl.replace('SaveResult','FillHierarchy');
            URL = URL + '?prmCompanyCode=' + code;

            $.ajax({
                type: "POST",
                url: sessionurl,
                dataType: "json",
                success: function (data) {
                    if (data == "Yes") {
                        $.ajax({
                            type: "POST",
                            url: URL,
                            dataType: "json",
                            success: function (data) {
                                if (data.message)
                                    window.location.href = data.message;
                                else {
                                    for (var i = 0; i < data.Table1.length; i++)
                                        $("#sltDeptHead").append('<option value="' + data.Table1[i].Id + '">' + data.Table1[i].Name + '</option>');
                                }
                            }, error: function (result) {
                                alert('There is some technical error, please contact Administrator.');
                            }
                        });// End of Ajax Call
                    } else {
                        window.location.href = data;
                    }
                }
            });// End of Session Check Call
        }
    });

    $("#btnSave,#btnUpdate").click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        // Get All selected values and text of orgunit from dropdown
        var code = $('#txtCode').val().trim().toUpperCase();
        var action = $(this).attr('data-action');
        var deptId = 0;
        if ($('#hidId').length > 0)
            deptId = $('#hidId').val();

        if (action == 'INSERT') {
            if (code.split(' ').length > 1) {
                alert('Space not allowed in code.');
                return;
            }

            var deptlist = $('#sltCompany option:selected').attr('data-child');
            if (deptlist.toUpperCase().split(',').indexOf(code) > -1) {
                alert('This department code is already used.');
                return;
            }
        }
        var prmdata = {
            prmDeptId: deptId,
            prmDeptCode: code,
            prmDepartment: $('#txtName').val().trim(),
            prmCompanyCode: $('#sltCompany').val(),
            prmDepartmentHead: $('#sltDeptHead').val(),
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