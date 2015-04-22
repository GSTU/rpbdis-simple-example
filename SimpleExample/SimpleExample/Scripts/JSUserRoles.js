function loadTable() {
    var dataSet = null;
    var usersJson = null;
    $.ajax({
        type: "POST",
        async: true,
        //type: "GET",
        url: "/UserRoles/GetUserRoles",///?firstIndex=" + frstIndex,
        contentType: "application/json",
        datatype: "json",
        success: function (data) {
            usersJson = $.parseJSON(data);
            $('#usersTable').dataTable({
                "data": usersJson
            });
        }
    });
}

$(document).ready(loadTable());