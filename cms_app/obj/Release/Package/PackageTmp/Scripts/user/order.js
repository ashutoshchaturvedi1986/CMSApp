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

        var fileData = new FormData();

        // Get All selected values and text of orgunit from dropdown
        fileData.append('hidId', $('#hidArticleId').val());
        fileData.append('customer', $('#sltCustomer').val());
        fileData.append('salePerson', $('#sltSalePerson').val());
        fileData.append('contactperson', $('#txtContactPerson').val().trim());
        fileData.append('contactpersonmobile', $('#txtContactPersonMobile').val().trim());
        fileData.append('customeraddress', $('#txtCustomerAddress').val().trim());
        fileData.append('size', $('#sltSize').val());
        fileData.append('quantity', $('#txtQuantity').val().trim());
        fileData.append('rate', $('#txtRate').val().trim());
        fileData.append('totalQuantity', $('#txtTotalQuantity').val().trim());
        fileData.append('orderDate', $('#txtOrderDate').val().trim());
        fileData.append('orderExpDate', $('#txtOrderExpDate').val().trim());
        fileData.append('remarks', $('#txtRemarks').val().trim());
        
        fileData.append('hidAction', $(this).attr('data-action'));

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