var fileExtension = ['jpeg', 'jpg', 'png'];
var photoSample = '';

$(document).ready(function () {
    toggelDiv($('.glyphicon-expand:eq(0)'));

    $("#sampleUpload").change(function (event) {
        if ($(this).val() != '') {
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(this).val('');
            }
        }

        if ($(this).val() != '') {
            $('#imgSamplePic').attr('src', URL.createObjectURL(event.target.files[0]));
        }
    });

    $('#btnSave,#btnUpdate').click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return false;
        }

        var moldFile = $('#sampleUpload');
        var isExtension = true;
        var status = '', assignRemarks = '', assignDate = '', designerId = 0;
        if ($('#sltChangeStatus').length > 0) {
            status = $('#sltChangeStatus').val();
            designerId = $('#sltDesigner').val();
            assignRemarks = $('#txtAssignRemarks').val().trim();
            assignDate = $('#txtAssignDate').val();
        }
        photoSample = $('#hidSamplePicture').val();
        
        var fileData = new FormData();

        if (moldFile.get(0).files.length > 0) {
            fileData.append(moldFile.get(0).files[0].name, moldFile.get(0).files[0]);
            photoSample = moldFile.get(0).files[0].name;
        }
        
        fileData.append('hidId', $('#hidMoldId').val());
        fileData.append('courierId', $('#hidCourierId').val());
        fileData.append('Transporter', $('#sltTransporter').val());
        fileData.append('CourierNo', $('#txtCourierNo').val().trim());
        fileData.append('sample', photoSample);
        fileData.append('remarks', $('#txtRemarks').val().trim());
        fileData.append('action', $(this).attr('data-action'));
        fileData.append('status', status);
        fileData.append('designerId', designerId);
        fileData.append('assignRemarks', assignRemarks);
        fileData.append('assignDate', assignDate);

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


function showPendingCourierList(url) {
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
