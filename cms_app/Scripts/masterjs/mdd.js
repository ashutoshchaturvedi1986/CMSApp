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
            var active = ($('#chkActive').prop('checked') == false) ? 0 : 1;

            if (action == 'INSERT') {
                if (code.split(' ').length > 1) {
                    alert('Space not allowed in code.');
                    return;
                }

                if (window.location.pathname.indexOf('/activity') > -1) {
                    var activitylist = $('#sltProcess option:selected').attr('data-child');
                    if (activitylist.toUpperCase().split(',').indexOf(code) > -1) {
                        alert('This code is already used');
                        return;
                    }
                }
            }
            var prmdata;
            if (window.location.pathname.indexOf('/activity') > -1) {
                prmdata = {
                    prmCompanyCode: $('#sltCompany').val(),
                    prmMDDCode: code,
                    prmMDDName: $('#txtName').val().trim(),
                    prmParrentMDDCode: $('#sltProcess').val(),
                    prmRemarks: $('#txtRemarks').val().trim(),
                    prmActive: active,
                    prmAction: action
                };
            }
            else {
                prmdata = {
                    prmCompanyCode: $('#sltCompany').val(),
                    prmMDDCode: code,
                    prmMDDName: $('#txtName').val().trim(),
                    prmRemarks: $('#txtRemarks').val().trim(),
                    prmActive: active,
                    prmAction: action
                };
            }

            $.ajax({
                type: "POST",
                url: sessionurl,
                dataType: "json",
                success: function (data) {
                    if (data == "Yes") {
                        saveMasterDataForm(saveurl, prmdata, listurl);
                    } else {
                        window.location.href = data; // session out here. for redirect to login
                    }
                }
            });
        });
    });

    function EditThis(obj) {
        var $tr = $(obj).closest('tr');
        var code = $tr.find('td:eq(0)').text();
        var compcode = $tr.find('td:eq(2)').text();
        url_redirectForEditMasterData(eurl, 'post', code, compcode);
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