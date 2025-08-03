
$(document).on('click', '#btnSaveFinance', function (e) {

    SaveFinance();

});

var Inquiry = 0;

function SaveFinance() {
    var mode = $('#btnSaveFinance').text().trim();


    var ajax_data = $('#Finance').serialize();
    ajax_data += '&id=' + Inquiry;
    ajax_data += '&mode=' + mode;

    $.ajax({
        url: '../finance_depart/Save_Finance',
        type: 'POST',
        dataType: 'json',
        data: ajax_data,
        success: function (data) {
            var jsonData = JSON.parse(data);
            if (jsonData.Action == true) {

                SucessNotify(jsonData.Message);

                $("#SaveFinance").text('Save');

                $("#Finance").find('input').val('');
                $("#Finance").find('textarea').val('');
                $("#Finance").find('select').val('').change();
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
        url: "../finance_depart/GetInquiryList",
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


    if ($.fn.dataTable.isDataTable('#tblFinance')) {
        tblInquiry = $('#tblFinance').DataTable().clear().destroy();
    }
    tblInquiry = $('#tblFinance').DataTable({


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
            { "data": "Id", "title": "Inquiry Id" },
            { "data": "Name", "title": "Name" },
            { "data": "Position", "title": "Position" },
            { "data": "Salary", "title": "Salary" },
            { "data": "HireDate", "title": "Hire_Date" },
            { "data": "Department", "title": "Department" },
            { "data": "Email", "title": "Email" },
            { "data": "Phone", "title": "Phone" },


            {
                "data": "Id", "title": "Action",
                "render": function (data, type, row, meta) {

                    var a = '<button type="button" style="margin-left: 30px;"  onclick="return EditInquiry(\'' + row["Id"] + '\')" class="btn btn-success btn-clean btn-icon btn-icon-md" title="View">Edit</button>';
                    var b = '<button type="button" style="margin-left: 30px;"  onclick="return DeleteInquiry(\'' + row["Id"] + '\')" class="btn btn-danger btn-clean btn-icon btn-icon-md" title="View">Delete</button>';


                    return a + b;
                }

            },
        ],
    });


}

function EditInquiry(Id) {

    var ajax_data = {

        Id: Id
    };

    $.ajax({
        url: "../finance_depart/EDITInquiry",
        data: ajax_data,
        type: 'GET',
        dataType: 'json',
        async: false,
        success: function (data) {
            var jsonData = JSON.parse(data);
            if (jsonData.length > 0) {


                $("#Name").val(jsonData[0].Name);
                $("#Position").val(jsonData[0].Position);
                $("#Salary").val(jsonData[0].Salary);
                $("#Hire_Date").val(jsonData[0].HireDate);
                $("#Department").val(jsonData[0].Department);
                $("#Email").val(jsonData[0].Email);
                $("#Phone").val(jsonData[0].Phone);
            /*    $("#Date").val(jsonData[0].Date);*/



            }


            $('#btnSaveFinance').text('Update');
            Inquiry = jsonData[0].Id;
            $('#btnCancel').show();
        },



        error: function (xhr, textStatus, errorThrown) {
            if (textStatus === 'error') {
                console.log('Error');
            }
        }
    
    });
}

        function DeleteInquiry(Id) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You want to Delete Data",
            icon: 'warning', // Updated to "icon" (as "type" is deprecated)
            showCancelButton: true,
            confirmButtonText: 'Yes, Delete it!'
        }).then(function (result) {
            if (result.isConfirmed) {
                var ajax_data = { Id: Id };

                // Send an AJAX request to the server to delete the record
                $.ajax({
                    url: "../finance_depart/DELETEInquiry",
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