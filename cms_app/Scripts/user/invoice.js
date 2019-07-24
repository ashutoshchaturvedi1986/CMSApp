$(document).ready(function () {
    //$('#sltCustomer').change(function () {
    //    var $opt = $('#sltCustomer option:selected');
    //    $('#txtCustomerCode').val($opt.attr('data-code'));
    //    $('#txtCustomerAddress').val($opt.attr('data-address'));
    //    $('#txtContactPerson').val($opt.attr('data-contactperson'));
    //    $('#txtContactPersonMobile').val($opt.attr('data-mobile'));
    //});

    //$('#btnSave').click(function () {
    //    if (!isFormValidate('#form1')) {
    //        alert('Please fill required field.');
    //        return;
    //    }

    //    var fileData = new FormData();

    //    // Get All selected values and text of orgunit from dropdown
    //    fileData.append('hidId', $('#hidArticleId').val());
    //    fileData.append('customer', $('#sltCustomer').val());
    //    fileData.append('salePerson', $('#sltSalePerson').val());
    //    fileData.append('contactperson', $('#txtContactPerson').val().trim());
    //    fileData.append('contactpersonmobile', $('#txtContactPersonMobile').val().trim());
    //    fileData.append('customeraddress', $('#txtCustomerAddress').val().trim());
    //    fileData.append('size', $('#sltSize').val());
    //    fileData.append('quantity', $('#txtQuantity').val().trim());
    //    fileData.append('rate', $('#txtRate').val().trim());
    //    fileData.append('totalQuantity', $('#txtTotalQuantity').val().trim());
    //    fileData.append('orderDate', $('#txtOrderDate').val().trim());
    //    fileData.append('orderExpDate', $('#txtOrderExpDate').val().trim());
    //    fileData.append('remarks', $('#txtRemarks').val().trim());
        
    //    fileData.append('hidAction', $(this).attr('data-action'));

    //    $.ajax({
    //        type: "POST",
    //        url: sessionurl,
    //        dataType: "json",
    //        success: function (data) {
    //            if (data == "Yes") {
    //                saveMasterDataFormWithFile(saveurl, fileData, listurl);
    //                $.unblockUI();
    //            } else {
    //                window.location.href = data;
    //                // session out here. for redirect to login
    //            }
    //        }, error: function (result) {
    //            alert('There is some technical error, please contact Administrator.');
    //        }
    //    });
    //});

    $(document).on('blur', ".qnt", function (e) {
        try {
            var activeBlockID = e.currentTarget.id;
            var $tr = e.currentTarget.closest('tr');
            var qnt = parseFloat($tr.children[1].firstChild.value.trim());
            var rate = parseFloat($tr.children[2].firstChild.value.trim());

            qnt = (isNaN(qnt)) ? 0 : qnt;
            rate = (isNaN(rate)) ? 0 : rate;
            var total = (qnt==0 || rate==0) ? '' : (qnt * rate);
            $tr.children[3].firstChild.value = total;

            TotalInvoiceValue();
        } catch (exception) {
            alert(exception);
        }
    });
})


function TotalInvoiceValue() {
    var totalBeforeTax = 0;

    $('#viewPlaceHolder tbody').find('tr').each(function () {
        var qnt = 0, rate = 0;
        var $tr = $(this);
        if ($tr.find('td').length > 2) {
            qnt = parseFloat($tr.find('input:eq(0)').val().trim());
            rate = parseFloat($tr.find('input:eq(1)').val().trim());
            qnt = (isNaN(qnt)) ? 0 : qnt;
            rate = (isNaN(rate)) ? 0 : rate;

            var total = (qnt == 0 || rate == 0) ? 0 : (qnt * rate);
            totalBeforeTax += total;
        }
    });
    
    var gst = parseFloat($('#txtGST').val().trim());
    var miscExp = parseFloat($('#txtMiscExp').val().trim());
    gst = (isNaN(gst)) ? 0 : gst;
    miscExp = (isNaN(miscExp)) ? 0 : miscExp;
    var grandTotal = totalBeforeTax + gst + miscExp;

    $('#totalBeforeTax').html((totalBeforeTax > 0) ? totalBeforeTax.toString() : 'NA');
    $('#grandTotal').html((grandTotal > 0) ? grandTotal.toString() : 'NA');
}

function showCreatedList(arid, vname, url) {
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

function UpdateRawMaterialCode(code) {
    var ur = saveurl.replace('SaveResult', 'Index');
    window.location.href = ur + "?code=" + code;
}