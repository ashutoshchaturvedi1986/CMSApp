var month = ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'];

$(document).ready(function () {
    toggelDiv($('.glyphicon-expand:eq(0)'));
    toggelDiv($('.glyphicon-expand:eq(1)'));

    $('#chkIsOk').click(function () {
        if($(this).prop('checked')){
            $('#txtAssignDate').attr('readonly', false);
            $('#txtLastSubmitionDate').attr('readonly', false);
            //$('#txtRemarks').attr('readonly', false);
        }
        else{
            $('#txtAssignDate').attr('readonly', true);
            $('#txtLastSubmitionDate').attr('readonly', true);
            resetDate();
        }
    });

    $('#btnSave').click(function () {
        //var MoldId = $('#hidMoldId').val();
        //var assignDate = $('#txtAssignDate').val().trim();
        //var lastSubmitionDate = $('#txtLastSubmitionDate').val().trim();
        //var remarks = $('#txtRemarks').val().trim();

        //alert('Mold Id:- ' + MoldId + '\n assignDate:- ' + assignDate + '\n lastSubmitionDate:- ' + lastSubmitionDate + '\n remarks:- ' + remarks);
        //return;


        if (!isFormValidate('#form1')) {
            alert('Please fill required field.');
            return false;
        }

        var chk = 1;
        if ($('#chkIsOk').prop('checked'))
            chk = 0;

        var fileData = new FormData();
        //// Get All selected values and text of orgunit from dropdown
        fileData.append('hidId', $('#hidMoldId').val());
        fileData.append('assignDate', $('#txtAssignDate').val().trim());
        fileData.append('lastSubmitionDate', $('#txtLastSubmitionDate').val().trim());
        fileData.append('remarks', $('#txtRemarks').val().trim());
        fileData.append('isApprove',chk );
        $.ajax({
            type: "POST",
            url: sessionurl,
            dataType: "json",
            success: function (data) {
                if (data == "Yes") {
                    saveMasterDataFormWithFile(saveurl, fileData, listurl);
                } else {
                    window.location.href = data; // session out here. for redirect to login
                }
            }, error: function (result) {
                alert('There is some technical error, please contact Administrator.');
            }
        });
    });
})


function setMyDate(d, obj) {
    //var dt = new Date(d).toLocaleDateString().split('/');
    //var m = dt[0];
    //var da = dt[1];
    //m = (m < 10) ? ('0' + m) : m;
    //da = (da < 10) ? ('0' + da) : da;
    var dt = d.toUpperCase().split(' ');
    var m = parseInt(month.indexOf(dt[1]))+1;
    var da = dt[0];
    m = (m < 10) ? ('0' + m) : m;
    $('#' + obj).val(dt[2] + '-' + m + '-' + da);
}

function resetDate() {
    var spanMoldAssignDate = $('#spanMoldAssignDate').text();
    var spanMoldLastSubDate = $('#spanMoldLastSubDate').text();
    setMyDate(spanMoldAssignDate, 'txtAssignDate');
    setMyDate(spanMoldLastSubDate, 'txtLastSubmitionDate');
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