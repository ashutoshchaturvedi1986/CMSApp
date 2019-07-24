    $(document).ready(function () {
        $("#sltState").change(function () {
            var stateId = $(this).val();
            $(this).removeClass('required');
            if (stateId != '') {
                var URL = saveurl.replace('SaveResult','FillCity');
                URL += "?prmStateId=" + stateId;
                changeState(URL, "#sltCity", '');
            }
            else {
                $("#sltCity").empty().append('<option value="0">Select City</option>');
            }
        });


        $("#btnSave,#btnUpdate").click(function () {
            if (!isFormValidate('#form1')) {
                alert('Please fill required field.');
                return;
            }
            
            // Get All selected values and text of orgunit from dropdown
            var code = $('#txtCode').val().trim().toUpperCase();
            var companyAlias = $('#txtCompanyAlias').val().trim().toUpperCase();
            var action = $(this).attr('data-action');
            if (action == 'INSERT') {
                if (code.split(' ').length > 1 || companyAlias.split(' ').length > 1) {
                    alert('Space not allowed in code/companyAlias.');
                    return;
                }
            }

            var prmdata = {
                prmCompanyName: $('#txtName').val().trim(),
                prmCompanyCode: code,
                prmAlias: companyAlias,
                prmStateId: $('#sltState').val(),
                prmCityId: $('#sltCity').val(),
                prmRegAddress: $('#txtAddress').val().trim(),
                prmBankName: $('#txtBankName').val().trim(),
                prmBankAcctNo: $('#txtBankAcctNo').val().trim(),
                prmBranchName: $('#txtBranchName').val().trim(),
                prmPANNo: $('#txtPANNo').val().trim(),
                prmMICRNo: $('#txtMICRNo').val().trim(),
                prmGSTNo: $('#txtGST').val().trim(),
                prmContactEmail: $('#txtEmail').val().trim(),
                prmPhone: $('#txtPhone').val().trim(),
                prmFax: $('#txtFax').val().trim(),
                prmWebsite: $('#txtWebsite').val().trim(),
                prmContactPerson: $('#txtContactPerson').val().trim(),
                prmMobileNo: $('#txtContactPersonMob').val().trim(),
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
                        window.location.href = data; // session out here. for redirect to login
                    }
                }
            });
        });
    });