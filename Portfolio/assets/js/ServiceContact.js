




$(document).on('click', '#btnSave', function (e) {

    Service();

});

var Inquiry = 0;

function Service() {
    var mode = $('#btnSave').text().trim();


    var ajax_data = $('#myForm').serialize();
    ajax_data += '&id=' + Inquiry;
    ajax_data += '&mode=' + mode;

    $.ajax({
        url: '../ServiceContact/Save_Inquiry',
        type: 'POST',
        dataType: 'json',
        data: ajax_data,
        success: function (data) {
            var jsonData = JSON.parse(data);
            if (jsonData.Action == true) {

                SucessNotify(jsonData.Message);

                $("#btnSave").text('Save');

                $("#myForm").find('input').val('');
                $("#myForm").find('textarea').val('');
                $("#myForm").find('select').val('').change();
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
        url: "../ServiceContact/GetInquiryList",
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


    if ($.fn.dataTable.isDataTable('#tblInquiry')) {
        tblInquiry = $('#tblInquiry').DataTable().clear().destroy();
    }
    tblInquiry = $('#tblInquiry').DataTable({


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
        "columns": [
            { "data": "ID", "title": "Inquiry Id" },
            { "data": "Name", "title": "Name" },
            { "data": "F_Name", "title": "F_Name" },
            { "data": "Age", "title": "Age" },
            { "data": "Contact", "title": "Contact" },
            { "data": "Address", "title": "Address" },
            { "data": "Date", "title": "Date" },


            {
                "data": "ID", "title": "Action",
                "render": function (data, type, row, meta) {

                    var a = '<button type="button" style="margin-left: 30px;"  onclick="return EditInquiry(\'' + row["ID"] + '\')" class="btn btn-success btn-clean btn-icon btn-icon-md" title="View">Edit</button>';
                    var b = '<button type="button" style="margin-left: 30px;"  onclick="return DeleteInquiry(\'' + row["ID"] + '\')" class="btn btn-danger btn-clean btn-icon btn-icon-md" title="View">Delete</button>';


                    return a + b;
                }

            },
        ],
    });


}

function EditInquiry(ID) {

    var ajax_data = {

        ID: ID
    };

    $.ajax({
        url: "../ServiceContact/EDITInquiry",
        data: ajax_data,
        type: 'GET',
        dataType: 'json',
        async: false,
        success: function (data) {
            var jsonData = JSON.parse(data);
            if (jsonData.length > 0) {


                $("#Name").val(jsonData[0].Name);
                $("#F_Name").val(jsonData[0].F_Name);
                $("#Age").val(jsonData[0].Age);
                $("#Contact").val(jsonData[0].Contact);
                $("#Address").val(jsonData[0].Address);
                $("#Date").val(jsonData[0].Date);



            }


            $('#btnSave').text('Update');
            Inquiry = jsonData[0].ID;
            $('#btnCancel').show();
        },



        error: function (xhr, textStatus, errorThrown) {
            if (textStatus === 'error') {
                console.log('Error');
            }
        }
    });
}

function DeleteInquiry(ID) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to Delete Data",
        icon: 'warning', // Updated to "icon" (as "type" is deprecated)
        showCancelButton: true,
        confirmButtonText: 'Yes, Delete it!'
    }).then(function (result) {
        if (result.isConfirmed) {
            var ajax_data = { ID: ID };

            // Send an AJAX request to the server to delete the record
            $.ajax({
                url: "../ServiceContact/DELETEInquiry",
                type: "POST",
                data: ajax_data,
                success: function (data) {
                    if (data.Action == true) {
                        GetInquiryList();
                    } else {
                        GetInquiryList();
                    }
                },
                error: function () {
                    alert("An error occurred while deleting the record.");
                }
            });
        }
    });
}