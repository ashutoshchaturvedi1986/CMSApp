var fileExtension = ['jpeg', 'jpg', 'png'];
var photoMold = '';
var photoTech = '';

$(document).ready(function () {
    if ($('#sltCompany').length > 0) {
        if ($('#sltCompany option').length == 2) {
            $('#sltCompany option:last').attr("selected", "selected");
            $('#sltCompany').attr("disabled", "disabled");
        }
    }

    $('#sltCustomer').change(function () {
        var $opt = $('#sltCustomer option:selected');
        $('#txtCustomerCode').val($opt.attr('data-code'));
        $('#txtCustomerAddress').val($opt.attr('data-address'));
        $('#txtContactPerson').val($opt.attr('data-contactperson'));
        $('#txtContactPersonMobile').val($opt.attr('data-mobile'));
    });

    $('.moldgenerate').click(function () {
        $('.moldadd,#divMoldList').addClass('hide');
        $('.moldgenerate').removeClass('btn-secondary').addClass('btn-secondary');
        $(this).removeClass('btn-secondary').addClass('btn-primary');
        $('#viewPlaceHolder').html('');
        var v = $(this).attr('data-id');
        if (v == 1)
            $('.moldadd').removeClass('hide');
        if (v > 0) {
            showMoldList(reporturl, v);
            $('#divMoldList').removeClass('hide');
        }
    });

    $("#moldUpload,#techPacUpload").change(function (event) {
        if ($(this).val() != '') {
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(this).val('');
            }
        }

        if ($(this).val() != '') {
            if ($(this).attr('id')=='moldUpload')
                $('#imgMoldPic').attr('src', URL.createObjectURL(event.target.files[0]));
            else
                $('#imgTechPic').attr('src', URL.createObjectURL(event.target.files[0]));
        }
    });

    $('#btnSave').click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        var moldFile = $('#moldUpload');
        var techFile = $('#techPacUpload');

        var fileData = new FormData();
        if (moldFile.get(0).files.length > 0) {
            fileData.append(moldFile.get(0).files[0].name, moldFile.get(0).files[0]);
            photoMold = moldFile.get(0).files[0].name;
        }
        if (techFile.get(0).files.length > 0) {
            fileData.append(techFile.get(0).files[0].name, techFile.get(0).files[0]);
            photoTech = techFile.get(0).files[0].name;
        }

        var isExtension = true;
        var size = getSelectedValue('#sltSize', ',');
        //var size = $('#sltSize').val();


        // Get All selected values and text of orgunit from dropdown
        fileData.append('hidId', 0);
        fileData.append('comp', $('#sltCompany').val());
        fileData.append('moldCode', $('#txtCode').val().trim());
        fileData.append('technology', $('#sltTechnology').val());
        fileData.append('size', size);
        fileData.append('color', $('#sltColor').val());
        fileData.append('designer', $('#sltDesigner').val());

        fileData.append('customer', $('#sltCustomer').val());
        fileData.append('customercode', $('#txtCustomerCode').val().trim());
        fileData.append('contactperson', $('#txtContactPerson').val().trim());
        fileData.append('contactpersonmobile', $('#txtContactPersonMobile').val().trim());
        fileData.append('customeraddress', $('#txtCustomerAddress').val().trim());
        fileData.append('address', $('#txtAddress').val().trim());
        fileData.append('remarks', $('#txtRemarks').val().trim());

        fileData.append('assignDate', $('#txtAssignDate').val().trim());
        fileData.append('lastSubmitionDate', $('#txtLastSubmitionDate').val().trim());
        fileData.append('photoMold', photoMold);
        fileData.append('photoTech', photoTech);
        fileData.append('hidChecked', 1);
        fileData.append('hidAction', $(this).attr('data-action'));
        //fileData.append('hidIsOrder', $('#hidIsOrder').val().trim());
        fileData.append('hidIsOrder', "1");
        
        if (isExtension != false) {
            $.ajax({
                type: "POST",
                url: sessionurl,
                dataType: "json",
                success: function (data) {
                    if (data == "Yes") {
                        saveMasterDataFormWithFile(saveurl, fileData, listurl);
                        $.unblockUI();
                    } else {
                        window.location.href = data;
                        // session out here. for redirect to login
                    }
                }, error: function (result) {
                    alert('There is some technical error, please contact Administrator.');
                }
            });
        }
    });
})

function showMoldList(url, s) {
    //$.blockUI();
    $("#viewPlaceHolder").load(url, { viewName: "_DisplayData_Mold", searchType: s },
        function (text, status, xhr) {
            if (status == 'error') {
                alert('There is some technical error, please contact Administrator.');
            }
            else {
                if (text.indexOf('There is some problem, please contact Administrator.') > -1) {
                    alert('There is some technical error, please contact Administrator.');
                }
                else if (text.indexOf('No record found') > -1) {

                } else {
                    destroyandCreateTableWithPaging($('#viewPlaceHolder table'), 0);
                }
            }
        });
}

function showMoldListForOrder(url) {
    $("#viewPlaceHolder").load(url, { viewName: "_DisplayData_Mold" },
        function (text, status, xhr) {
            if (status == 'error') {
                alert('There is some technical error, please contact Administrator.');
            }
            else {
                if (text.indexOf('There is some problem, please contact Administrator.') > -1) {
                    alert('There is some technical error, please contact Administrator.');
                }
                else if (text.indexOf('No record found') > -1) {

                } else {
                    destroyandCreateTableWithPaging($('#viewPlaceHolder table'), 0);
                }
            }
        });
}

function showPendingSampleList(url) {
    $("#viewPlaceHolder").load(url, { viewName: "_DisplayDataArticle" },
        function (text, status, xhr) {
            if (status == 'error') {
                alert('There is some technical error, please contact Administrator.');
            }
            else {
                if (text.indexOf('There is some problem, please contact Administrator.') > -1) {
                    alert('There is some technical error, please contact Administrator.');
                }
                else if (text.indexOf('No record found') > -1) {

                } else {
                    destroyandCreateTableWithPaging($('#viewPlaceHolder table'), 0);
                }
            }
        });
}

function CreateNewDeveloperCode(code) {
    var ur = saveurl.replace('SaveResult', 'Index');
    window.location.href = ur + "?prmId=" + code;
}

//function CreateNewDeveloperCode(code) {
//    if (code.length > 0) {
//        var ur = saveurl.replace('SaveResult', 'GetMasterListForEdit');
//        $.ajax({
//            type: "POST",
//            url: ur,
//            dataType: "json",
//            data: { 'prmId': code },
//            success: function (result) {
//                if (result != null) {
//                    var obj = result[0];
//                    debugger;
//                    $('#txtCode').val(obj.MoldName);
//                    $('#sltCustomer').val(obj.CustomerId);
//                    $('#sltTechnology').val(obj.Technology);
//                    //$('#sltSize').val(obj.Size);
//                    if (obj.Size != null) {
//                        for (var i = 0; i < obj.Size.split(',').length ; i++) {
//                            $('#sltSize option').each(function () {
//                                if ($(this).val() == obj.Size.split(',')[i])
//                                    $(this).attr('selected', true);
//                            });
//                        }
//                    }
//                    $('#sltColor').val(obj.Colour);
//                    $('#sltDesigner').val(obj.DesignerId);

//                    $('#txtAddress').val(obj.CourierAddress);
//                    $('#txtRemarks').val(obj.Remarks);

//                    var $opt = $('#sltCustomer option:selected');
//                    var code = $opt.attr('data-code');
//                    $('#txtCustomerCode').val(code);
//                    $('#txtContactPerson').val(obj.ContactPerson);
//                    $('#txtContactPersonMobile').val(obj.ContactNo);
//                    $('#txtCustomerAddress').val(obj.Address);

//                    if (obj.MoldPicture.trim().length > 0) {
//                        $('#imgMoldPic').attr('src', '/images/mold/' + obj.MoldPicture);
//                        photoMold = obj.MoldPicture;
//                    }
//                    if (obj.TechPacDetail.trim().length > 0) {
//                        $('#imgTechPic').attr('src', '/images/mold/' + obj.TechPacDetail);
//                        photoTech = obj.TechPacDetail;
//                    }

//                    $('#txtAssignDate').val(GetDateFormat(obj.AssignDate));
//                    $('#txtLastSubmitionDate').val(GetDateFormat(obj.LastSubmitionDate));
//                }
//            }, error: function (result) {
//                alert('There is some technical error, please contact Administrator.');
//            }
//        });
//    }
//}

function ResetMoldData() {
    var size = $('#hidSize').val().trim();
    var $opt = $('#sltCustomer option:selected');
    var ContactPerson = $('#txtContactPerson').val().trim();
    var ContactNo = $('#txtContactPersonMobile').val().trim();
    var Address = $('#txtCustomerAddress').val().trim();

    $('#txtCustomerCode').val($opt.attr('data-code'));
    if (ContactPerson.length == 0)
        $('#txtContactPerson').val($opt.attr('data-contactperson'));
    if (ContactNo.length == 0)
        $('#txtContactPersonMobile').val($opt.attr('data-mobile'));
    if (Address.length == 0)
        $('#txtCustomerAddress').val($opt.attr('data-address'));

    if (size.length > 0) {
        for (var i = 0; i < size.split(',').length ; i++) {
            $('#sltSize option').each(function () {
                if ($(this).val() == size.split(',')[i])
                    $(this).attr('selected', true);
            });
        }
    }
}



