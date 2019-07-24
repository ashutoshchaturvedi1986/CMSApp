$(document).ready(function () {
    $('[id*=rblLanguage]').on("change", function () {
        var selected = $(this).val();
        var textBox = $('[id*=Description]');
        if (selected == "Hindi") {
            $(textBox).attr('class', 'form-control required hindiFont');
        }
        else {
            $(textBox).attr('class', 'form-control required');
        }
    });

    $('#btnSave').click(function () {
        var size = '';
        var subProcessXml = '';
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }
        
        $('#tbProcessPriority tr').filter(function () {
            var ProcessCode = $(this).find('td:eq(0)').data('code');
            var subProcessCode = $(this).find('td:eq(1)').data('code');
            var subProcessPriority = $(this).find('select').val();

            subProcessXml += ProcessCode + '~' + subProcessCode + '~' + subProcessPriority + '^';
        })
        if (subProcessXml.length > 0)
            subProcessXml = subProcessXml.substring(0, subProcessXml.length - 1);

        if (subProcessXml.length == 0) {
            alert('Please fill required field.');
            return;
        }

        var lang = $('[id*=rblLanguage]:checked').val();
        var isBeforeProcess = $('[name*=rad1]:checked').val();
        var isAfterProcess = $('[name*=rad2]:checked').val();
        var size = getSelectedValue('#sltSize', ',');


        var fileData = new FormData();
        // Get All selected values and text of orgunit from dropdown
        fileData.append('hidId', $('#hidMoldId').val());
        fileData.append('brand', $('#sltBrand').val());
        fileData.append('color', $('#sltColor').val());
        fileData.append('size', size);
        fileData.append('articleDate', $('#txtArticleDate').val().trim());
        fileData.append('isBeforeProcess', isBeforeProcess);
        fileData.append('isAfterProcess', isAfterProcess);
        fileData.append('lang', lang);
        fileData.append('description', $('#Description').val().trim());
        fileData.append('hidAction', $(this).attr('data-action'));
        fileData.append('subProcessInputs', subProcessXml);

        $.ajax({
            type: "POST",
            url: sessionurl,
            dataType: "json",
            success: function (data) {
                if (data == "Yes") {
                    saveMasterDataFormWithFile(saveurl, fileData, listurl);
                    //$.unblockUI();
                } else {
                    window.location.href = data;
                    // session out here. for redirect to login
                }
            }, error: function (result) {
                alert('There is some technical error, please contact Administrator.');
            }
        });
    });

    $('#btnSaveRawInfo').click(function () {
        var subProcessXml = '';
        $('#tbProcessPriority tr').filter(function () {
            var RawMaterialId = $(this).find('td:eq(0)').data('code');
            //var RawMaterial = $(this).find('td:eq(1)').find('input[type="text"]').val().trim();
            var RawMaterial = $(this).find('td:eq(1)').find('select').val();
            var StandardSize = $(this).find('td:eq(2)').find('select').val();
            var Norms = $(this).find('td:eq(3)').find('input[type="text"]').val().trim();
            var StandardVariance = $(this).find('td:eq(4)').find('input[type="text"]').val().trim();
            var PerValue = $(this).find('td:eq(5)').find('input[type="text"]').val().trim();
            var OtherSize = $(this).find('td:eq(6)').find('select').val();

            subProcessXml += RawMaterialId + '~' + RawMaterial + '~' + StandardSize + '~' + Norms + '~' + StandardVariance + '~' + PerValue + '~' + OtherSize + '^';
        })
        if (subProcessXml.length > 0)
            subProcessXml = subProcessXml.substring(0, subProcessXml.length - 1);

        if (subProcessXml.length == 0) {
            //window.location.href = listurl;
            return;
        }

        var fileData = new FormData();
        // Get All selected values and text of orgunit from dropdown
        fileData.append('subProcessInputs', subProcessXml);

        $.ajax({
            type: "POST",
            url: sessionurl,
            dataType: "json",
            success: function (data) {
                if (data == "Yes") {
                    saveMasterDataFormWithFile(saveurl.replace('SaveResult', 'SaveRawMaterialResult'), fileData, listurl);
                } else {
                    window.location.href = data;
                }
            }, error: function (result) {
                alert('There is some technical error, please contact Administrator.');
            }
        });
    });
})

function showMoldListForArticleCreation(arid, vname, url) {
    $("#viewPlaceHolder").load(url, { action: arid, viewName: vname },
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
function CreateSubProcess() {
    var oLength = $('#sltActivity option:selected').length;
    if (oLength > 0)
        $('#tbProcessPriority').html('');

    $('#sltActivity option:selected').each(function (x) {
        var $t = $(this);
        var $tr = $('<tr/>');
        var $sl = $('<select/>', { 'class': 'formPriority', 'id': 'sltPriority' + x });
        $sl.attr('onchange', 'SetPriority(this)');
        var $tdsl = $('<td/>');
        $sl.append($("<option/>").val('0').text('Select'));
        for (j = 1; j <= oLength; j++)
            $sl.append($("<option/>").val(j).text(j));
        $tdsl.append($sl);

        $tr.append('<td data-code="' + $t.attr('data-process') + '">' + getProcessName($t.attr('data-process')) + '</td>');
        $tr.append('<td data-code="' + $t.val() + '">' + $t.text() + '</td>');
        $tr.append($tdsl);

        $('#tbProcessPriority').append($tr);
    });
}
function getProcessName(v) {
    var processName = '';
    $('#sltProcess option:selected').each(function (x) {
        if (v == $(this).val())
            processName = $(this).text();
    });
    return processName;
}
function SetPriority(obj) {
    var vl = $(obj).val();
    var $tr = $(obj).closest('tr');
    var pr = $tr.find('td:eq(0)').text();
    var alreadyExist = 0;
    $('#tbProcessPriority tr').not($tr).each(function () {
        if (alreadyExist == 0) {
            var pr1 = $(this).find('td:eq(0)').text();
            var vl1 = $(this).find('select').val();
            if (pr == pr1 && vl == vl1)
                alreadyExist = 1;
        }
    });

    if (alreadyExist > 0) {
        alert('This priority is already set.');
        $(obj).val('0');
    }
}
function hideSubProcess(p) {
    var subProcess = p.split(',');
    var dropDown = $('#sltActivity');
    dropDown.multiselect('destroy');
    dropDown.empty();

    var arrmr = p.split(',');
    for (j = 0; j < arrmr.length; j++) {
        $.each(subProcessList, function (x) {
            if (subProcessList[x].ProcessCode.toUpperCase() == arrmr[j].toUpperCase()) {
                dropDown.append($("<option/>").val(subProcessList[x].SubProcessCode).text(subProcessList[x].SubProcessName).attr('data-process', subProcessList[x].ProcessCode));
            }
        });
    }
    dropDown.multiselect({ selectedList: 1, noneSelectedText: "Select Sub Process", selectedText: '# Sub Process(s) Selected' });
}