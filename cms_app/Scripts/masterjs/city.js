    $(document).ready(function () {
        $("#btnSave,#btnUpdate").click(function () {
            if (!isFormValidate('#form1')) {
                alert('Please fill required field.');
                return;
            }
            
            // Get All selected values and text of orgunit from dropdown
            var code = $('#txtCode').val().trim().toUpperCase();
            var action = $(this).attr('data-action');
            
            if (action == 'INSERT') {
                if (code.split(' ').length > 1) {
                    alert('Space not allowed in code.');
                    return;
                }

                var cityList = $('#sltState option:selected').attr('data-cities');
                if (cityList.toUpperCase().split(',').indexOf(code) > -1) {
                    alert('City Code already used.');
                    return;
                }
            }
            
            var prmdata = {
                prmCityId: code,
                prmStateId: $('#sltState').val(),
                prmName: $('#txtName').val().trim(),
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
                        window.location.href = data; // session out here. for redirect to login
                    }
                }
            });
        });
    });