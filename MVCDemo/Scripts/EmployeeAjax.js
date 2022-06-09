
//Create view button event call
function InsertEmployee() {

    var requestData = {
        FirstName: $("#txtFirstName").val(),
        LastName: $("#txtLastName").val(),
        Email: $("#txtEmail").val(),
        Salary: $("#txtSalary").val()
    }
    $.ajax({        
        type: "POST",
        url: "/Employee/Create",
        data: requestData,
        success: function (response) {                           
             alert("You will now be redirected to index page.");
             window.location = "/Employee/Index";           
        },
        error: function (error) {
            alert("Failed while inserting the record.");
        }
    });

}


function DeleteEmployee(Id) {
    $.ajax({
        type: "POST",
        url: "/Employee/Delete",
        data: { id: Id},
        success: function (response) {
            alert("Employee Deleted successfully.");
            window.location = "/Employee/Index";
        },
        error: function (error) {
            alert("Failed while deleting the record.");
        }
    });
}


