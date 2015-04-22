var dataSet = null,
        roles = null,
        idNow = null;
function loadTable() {
    $.ajax({
        type: "GET",
        async: true,
        url: "/Roles/GetRoles",
        contentType: "application/json",
        datatype: "json"
    }).done(function (data) {
        dataSet = $.parseJSON(data);
        $('#usersTable').dataTable({
            "data": dataSet
        });
    });
}

function updateRole(id) {
    $('#myModal').modal('toggle');
    document.getElementById("inputName").value = dataSet[id][2];
    document.getElementById("inputDefinition").value = dataSet[id][3];
    idNow = id;
}

function createRole() {
    document.getElementById("myModalLabel").textContent = "Create role";
    $('#myModal').modal('toggle');
    document.getElementById("saveButton").onclick = saveRole;

}

function deleteRole(id) {
    $.ajax({
        type: "POST",
        //async: true,
        url: "/Roles/DeleteRole/?Id=" + dataSet[id][4],
        contentType: "application/json",
        datatype: "json"
    });
    location.reload();
}

function saveChanges() {
    var id = dataSet[idNow][4],
        name = document.getElementById("inputName").value,
        definition = document.getElementById("inputDefinition").value;
    $.ajax({
        type: "POST",
        //async: true,
        url:"/Roles/UpdateRole/?id="+ id +"&name=" + name + "&definition=" + definition,
        contentType: "application/json",
        datatype: "json",
        success: console.log("Role is updated"),
    });
    $('#myModal').modal('hide');
    location.reload();
}

function saveRole() {
    var name = document.getElementById("inputName").value,
        definition = document.getElementById("inputDefinition").value;
    $.ajax({
        type: "POST",
        //async: true,
        url: "/Roles/CreateRole/?name=" + name + "&definition=" + definition,
        contentType: "application/json",
        datatype: "json"
    });
    $('#myModal').modal('hide');
    location.reload();
}

$(document).ready(loadTable());
