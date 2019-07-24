$(document).ajaxStart(function () {
    $.blockUI();
});
$(document).ajaxStop(function () {
    $.unblockUI();
});

$(document).ready(function () {
    $('.glyphicon-expand').click(function () {
        toggelDiv($(this));
    });

    $('#btnCancel').click(function () {
        window.location.href = listurl;
    });
});

function toggelDiv(obj) {
    var $span = $(obj).find('span');
    var $objD = $(obj).closest('div.content-heading').next('div.mycontainer');
    var t = $span.text();

    if (t == '+')
        $span.text('-');
    else
        $span.text('+');
    $objD.slideToggle();
}
function collapseAll() {
    $('.glyphicon-expand').find('span').text('+');
    $('.mycontainer').slideToggle();
}

function isFormValidate(formId) {
    var isValid = true;
    var $frm = $(formId);
    //$frm.find('input[type="text"].required').each(function () {
    $frm.find('input.required').each(function () {
        $(this).removeClass('errorvalidate');
        if ($(this).val().trim() == '') {
            $(this).addClass('errorvalidate');
            isValid = false;
        }

    });
    $frm.find('select.required').each(function () {
        $(this).removeClass('errorvalidate');
        if ($(this).val().trim() == '' || $(this).val().trim() == '0') {
            $(this).addClass('errorvalidate');
            isValid = false;
        }
    });
    return isValid;
}
function url_redirectForEdit(url, method, code) {
    var $form = $("<form />");
    $form.attr("action", url);
    $form.attr("method", method);
    $form.append('<input type="hidden" name="code" value="' + code + '" />');
    $("body").append($form);
    $form.submit();
}

function url_redirectForEditMasterData(url, method, code, compcode) {
    var $form = $("<form />");
    $form.attr("action", url);
    $form.attr("method", method);
    $form.append('<input type="hidden" name="code" value="' + code + '" />');
    $form.append('<input type="hidden" name="compcode" value="' + compcode + '" />');
    $("body").append($form);
    $form.submit();
}


function changeState(URL, cityController,cityid) {
    $.ajax({
        type: "POST",
        url: URL,
        dataType: "json",
        success: function (data) {
            if (data.message)
                window.location.href = data.message;
            else {
                var cityDropdown = $(cityController);
                cityDropdown.empty();
                cityDropdown.append('<option value="0">Select City</option>');

                for (var i = 0; i < data.length; i++)
                    cityDropdown.append('<option value=' + data[i].CityId + '>' + data[i].Name + '</option>');
                if (cityid != '')
                    cityDropdown.val(cityid);
            }
        }, error: function (result) {
            alert('There is some technical error, please contact Administrator.');
        }
    });
}

function changeDropDown(callurl, ddlController, selectedvalue,mdhcat) {
    $.ajax({
        type: "POST",
        url: sessionurl,
        dataType: "json",
        success: function (data) {
            if (data == "Yes") {
                //$.blockUI();
                $.ajax({
                    type: "POST",
                    url: callurl,
                    dataType: "json",
                    success: function (data) {
                        if (data.message)
                            window.location.href = data.message;
                        else {
                            var ddl = $(ddlController);
                            for (var i = 0; i < data.Table.length; i++) {
                                if (data.Table[i].MDHCode == mdhcat) {
                                    ddl.append('<option value="' + data.Table[i].Id + '" data-child="' + data.Table[i].ChildCode + '">' + data.Table[i].Name + '</option>');
                                    // alert('<option value="' + data.Table[i].Id + '">' + data.Table[i].Name + '</option>');
                                }
                            }
                            if (selectedvalue != '')
                                ddl.val(selectedvalue);
                            else if (ddl.find('option').length == 2)
                                ddl.find('option:last').attr("selected", "selected");
                        }
                        //$.unblockUI();
                    }, error: function (result) {
                        alert('There is some technical error, please contact Administrator.');
                        //$.unblockUI();
                    }
                });
            } else {
                window.location.href = data;
            }
        }
    });
}


//===========================================================================================================================================
function showMasterList(url) {
    $("#viewPlaceHolder").load(url, { viewName: "_DisplayData" },
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
function destroyandCreateTableWithPaging(obj, l) {
    //if ($(obj).find('tbody').length > 0)
    //    $(obj).DataTable().fnDestroy();
    $(obj).DataTable({
        "scrollY": 367, "scrollX": true, "paging": true, "bSort": false, "lengthMenu": [10],
        autoWidth: true, columnDefs: [{ width: '10%', targets: 0 }], "fixedColumns": { leftColumns: l }
    });
}


function saveMasterDataForm(ur,prmdata,l) {
    $.ajax({
        type: "POST",
        url: ur,
        dataType: "json",
        data: prmdata,
        success: function (data) {
            if (data.message)
                window.location.href = data.message;
            else {
                alert(data);
                if (data.indexOf('already used')=='-1')
                    window.location.href = l;
            }
        }, error: function (result) {
            alert('There is some technical error, please contact Administrator.');
        }
    });
}


function saveMasterDataFormWithFile(ur, prmdata, l) {
    $.ajax({
        type: "POST",
        url: ur,
        dataType: "json",
        data: prmdata,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        success: function (data) {
            if (data.message)
                window.location.href = data.message;
            else {
                alert(data);
                window.location.href = l;
            }
        }, error: function (result) {
            alert('There is some technical error, please contact Administrator.');
        }
    });
}


function getSelectedValue(controler, htmlcontroler) {
    var Selected = [];
    $(controler + ' :selected').each(function () { Selected.push($(this).val()); });
    if (Selected.length == 0)
        $(controler + ' option').each(function () { Selected.push($(this).val()); });
    var seperator = (htmlcontroler == ',') ? ',' : ('</' + htmlcontroler + '><' + htmlcontroler + '>');
    return (htmlcontroler == ',') ? Selected.join(seperator) : ('<' + htmlcontroler + '>' + Selected.join(seperator) + '</' + htmlcontroler + '>');
}


function GetDateFormat(d) {
    var d1 = d.split('.');
    return d1[2] + '-' + d1[1] + '-' + d1[0];
}