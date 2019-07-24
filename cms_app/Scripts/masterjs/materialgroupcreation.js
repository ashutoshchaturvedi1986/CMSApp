var fileExtension = ['jpeg', 'jpg', 'png'];
var photoMaterial = '';

$(document).ready(function () {
    if (isshowList > 0)
        showList();
    if ($('#sltCompany option').length == 2) {
        $('#sltCompany option:last').attr("selected", "selected");
        $('#sltCompany').attr("disabled", "disabled");
    }

    $("#materialUpload").change(function () {
        if ($(this).val() != '') {
            var fileExtension = ['jpeg', 'jpg', 'png'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(this).val('');
            }
        }

        if ($(this).val() != '') {
            $('#imgMaterialPic').attr('src', URL.createObjectURL(event.target.files[0]));
        }
    });

    $("#btnSave,#btnUpdate").click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        var materialFile = $('#materialUpload');
        var fileData = new FormData();
        var isExtension = true;

        if (materialFile.get(0).files.length > 0) {
            fileData.append(materialFile.get(0).files[0].name, materialFile.get(0).files[0]);
            photoMaterial = materialFile.get(0).files[0].name;
        }

        var materialGroupId = 0;
        if ($('#hidId').length > 0)
            materialGroupId = $('#hidId').val();
        var active = $('#chkActive').prop('checked') ? 1 : 0;

        fileData.append('hidId', materialGroupId);
        fileData.append('sltMaterialGroup', $('#sltMaterialGroup').val());
        fileData.append('txtName', $('#txtName').val().trim());
        fileData.append('sltUnit', $('#sltUnit').val());
        fileData.append('sltCompany', $('#sltCompany').val());
        fileData.append('txtReorder', $('#txtReorder').val().trim());
        fileData.append('txtOpeningStock', $('#txtOpeningStock').val().trim());
        fileData.append('txtRemarks', $('#txtRemarks').val().trim());
        fileData.append('hidChecked', active);
        fileData.append('hidAction', $(this).attr('data-action'));
        fileData.append('photoMaterial', photoMaterial);

        $.ajax({
            type: "POST",
            url: sessionurl,
            dataType: "json",
            success: function (data) {
                if (data == "Yes") {
                    saveMasterDataFormWithFile(saveurl, fileData, listurl);
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