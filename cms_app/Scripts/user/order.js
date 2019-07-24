$(document).ready(function () {
    $('#sltCustomer').change(function () {
        var $opt = $('#sltCustomer option:selected');
        $('#txtCustomerCode').val($opt.attr('data-code'));
        $('#txtCustomerAddress').val($opt.attr('data-address'));
        $('#txtContactPerson').val($opt.attr('data-contactperson'));
        $('#txtContactPersonMobile').val($opt.attr('data-mobile'));
    });

    $('#btnSave').click(function () {
        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return;
        }

        var inputxml = '';
        $('.detailsData').each(function () {
            var size = $(this).find('td input.size').val();
            var rate = Number($(this).find('td input.rate').val());
            var qty = Number($(this).find('td input.qty').val());

            if (rate != 0 || qty != 0) {
                inputxml += size + '~' + qty + '~' + rate + '^';
            }
        });
        if (inputxml.length > 0)
            inputxml = inputxml.substring(0, inputxml.length - 1);

        if (inputxml.length == 0) {
            return;
        }

        var fileData = new FormData();

        // Get All selected values and text of orgunit from dropdown
        fileData.append('hidId', $('#hidArticleId').val());
        fileData.append('orderId', $('#orderId').val());
        fileData.append('customer', $('#sltCustomer').val());
        fileData.append('salePerson', $('#sltSalePerson').val());
        fileData.append('contactperson', $('#txtContactPerson').val().trim());
        fileData.append('contactpersonmobile', $('#txtContactPersonMobile').val().trim());
        fileData.append('customeraddress', $('#txtCustomerAddress').val().trim());
        //fileData.append('size', $('#sltSize').val());
        //fileData.append('quantity', $('#txtQuantity').val().trim());
        //fileData.append('rate', $('#txtRate').val().trim());
        //fileData.append('totalQuantity', $('#txtTotalQuantity').val().trim());
        fileData.append('orderDate', $('#txtOrderDate').val().trim());
        fileData.append('orderExpDate', $('#txtOrderExpDate').val().trim());
        fileData.append('remarks', $('#txtRemarks').val().trim());
        
        fileData.append('hidAction', $(this).attr('data-action'));
        fileData.append('inputXml', inputxml);

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
    });
})


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

function SetTotalFields(changeIn, size) {
    var qtyArray = [], rateArray = [], amtArray = [];
    var totalQty = 0, totalAmt = 0, avgRate = 0;
    var qty = Number($('tr[data-size=' + size + '] .qty').val());
    var rate = Number($('tr[data-size=' + size + '] .rate').val());
    var amt = (qty == 0 || rate == 0) ? null : qty * rate;

    $('tr[data-size=' + size + '] .amt').val(amt);

    qtyArray = $('.detailsData').map(function () {
        return Number($(this).find('td input.qty').val())
    }).get();

    amtArray = $('.detailsData').map(function () {
        return Number($(this).find('td input.amt').val())
    }).get();

    totalQty = qtyArray.reduce(function (a, b) {
        return parseInt(a, 10) + parseInt(b, 10);
    });

    totalAmt = amtArray.reduce(function (a, b) {
        return parseInt(a, 10) + parseInt(b, 10);
    });

    $('#totalAmt').text((totalAmt == 0 ? null : totalAmt));

    if (changeIn == 'QUANTITY') {
        $('#totalQty').text((totalQty == 0 ? null : totalQty));
    } else if (changeIn == 'RATE') {

        avgRate = totalQty == 0 || totalAmt == 0 ? null : (totalAmt / totalQty).toFixed(2);
        $('#avgRate').text(avgRate);
    }
}