$(document).on('click', '#btnSaveHR', function (e) {

    SaveHR();

});

var Inquiry = 0;

function SaveHR() {
    var mode = $('#btnSaveHR').text().trim();


    var ajax_data = $('#HR_form').serialize();
    ajax_data += '&id=' + Inquiry;
    ajax_data += '&mode=' + mode;

    $.ajax({
        url: '../HR_Depart/Save_HR',
        type: 'POST',
        dataType: 'json',
        data: ajax_data,
        success: function (data) {
            var jsonData = JSON.parse(data);
            if (jsonData.Action == true) {

                SucessNotify(jsonData.Message);

                $("#Save_HR").text('Save');

                $("#HR_form").find('input').val('');
                $("#HR_form").find('textarea').val('');
                $("#HR_form").find('select').val('').change();
                GetInquiryList();
            } else {

                ErrorNotify("Account Code Already Exist");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            if (textStatus === 'error') {
                $("html, body").animate({ scrollTop: 0 }, 1000);
                $('#errormsg').html('Some Error Occurred').fadeIn().delay(5000).fadeOut();
            }
        },

    });
}

function GetInquiryList() {
    var ajax_data = {

    };

    $.ajax({
        url: "../HR_Depart/GetInquiryList",
        data: ajax_data,
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            LoadCInquiryList(JSON.parse(data));

        },
        error: function (request) {
            //notifyError('Saving User Detail failed!');            

        }, beforeSend: function () {
            /* $('#loading').show();*/
        },
        complete: function () {
            /* $('#loading').hide();*/
        },
    });

}
function LoadCInquiryList(data) {

    var tblInquiry;


    if ($.fn.dataTable.isDataTable('#tblHR')) {
        tblInquiry = $('#tblHR').DataTable().clear().destroy();
    }
    tblInquiry = $('#tblHR').DataTable({

       
        "language": {
            "info": "END of TOTAL"
        },
        data: data,
        responsive: true,
        sortable: true,
        paging: true,
        dom: 'lBfrtip', // Include the paging dropdown using 'l' and specify the placement of buttons
        buttons: [
            'excelHtml5',
            'print'
        ],

        select: true,


        "order": [[0, "desc"]],
        "autoWidth": false,  // Disable automatic column width adjustment
        "scrollX": true,     // Enable horizontal scrolling if needed
        "columnDefs": [
            { "targets": -1, "width": "250px" } // Sets the last column (Action) width
        ],
        "columns": [
            { "data": "ID", "title": "ID" },
            { "data": "FirstName", "title": "FirstName" },
            { "data": "LastName", "title": "LastName" },
            { "data": "Gender", "title": "Gender" },
            { "data": "Email", "title": "Email" },
            { "data": "PhoneNumber", "title": "Phone_Number" },
            { "data": "Address", "title": "Address" },
            { "data": "HireDate", "title": "HireDate" },
            { "data": "JobTitle", "title": "JobTitle" },
            { "data": "Salary", "title": "Salary" },
            { "data": "EmploymentStatus", "title": "EmploymentStatus" },


            {
                "data": "ID", "title": "Action",
                "render": function (data, type, row, meta) {

                    var a = '<button type="button" style="margin-left: 30px;"  onclick="return EditInquiry(\'' + row["ID"] + '\')" class="btn btn-success btn-clean btn-icon btn-icon-md" title="View">Edit</button>';
                    var b = '<button type="button" style="margin-left: 30px;"  onclick="return DeleteInquiry(\'' + row["Inquiry_Id"] + '\')" class="btn btn-danger btn-clean btn-icon btn-icon-md" title="View">Delete</button>';


                    return a + b;
                }

            },
        ],
    });


}