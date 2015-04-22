var dataSet = null,
        roles = null,
        idNow = null;

function loadTable() {
    $.ajax({
        type: "GET",
        async: true,
        url: "/Users/GetUsers",
        contentType: "application/json",
        datatype: "json"
    }).done(function (data) {
        dataSet = $.parseJSON(data);
        $('#usersTable').dataTable({
            "data": dataSet
        });
    });
}

function updateUser(Id) {
    $('#myModal').modal('toggle');
    document.getElementById("inputLogin").value = dataSet[Id][2];
    document.getElementById("inputEmail").value = dataSet[Id][3];
    document.getElementById("inputPassword").value = dataSet[Id][4];
    document.getElementById("inputAvatarPath").value = dataSet[Id][7];
    idNow = Id;

    var currRole = currentRole(Id);
    console.log(currRole);
    currRole.done(function (data) {
        loadRoles($.parseJSON(data));
    });
}

function createUser() {
    document.getElementById("myModalLabel").textContent = "Create user";
    $('#myModal').modal('toggle');
    document.getElementById("saveButton").onclick = saveUser;

    $.ajax({
        type: "GET",
        async: true,
        url: "/Users/GetRoles",
        contentType: "application/json",
        datatype: "json"
    }).done(function (data) {
        roles = $.parseJSON(data);
        var res = "";
        $.each(roles, function (i) {
            res += '<option value="' + roles[i].Id + '">' + roles[i].Name + "</option>";
        });
        document.getElementById("rolesSelect").innerHTML = res;
    });
}

function deleteUser(Id) {
    $.ajax({
        type: "POST",
        async: false,
        url: "/Users/DeleteUser/?Id=" + dataSet[Id][8],
        contentType: "application/json",
        datatype: "json"
    });
    location.reload();
}

function saveChanges() {
    var id = dataSet[idNow][8],
        login = document.getElementById("inputLogin").value,
        email = document.getElementById("inputEmail").value,
        password = document.getElementById("inputPassword").value,
        avatarPath = document.getElementById("inputAvatarPath").value,
        ind = document.getElementById("rolesSelect").selectedIndex.valueOf(),
        role = document.getElementById("rolesSelect").childNodes[ind].value;
    $.ajax({
        type: "POST",
        async: true,
        url: "/Users/UpdateUser/?id=" + id + "&login=" + login + "&email=" + email + "&password=" + password +
            "&avatarPath=" + avatarPath + "&role=" + role,
        contentType: "application/json",
        datatype: "json"
    });
    $('#myModal').modal('hide');
    location.reload();
}

function saveUser() {
    var login = document.getElementById("inputLogin").value,
        email = document.getElementById("inputEmail").value,
        password = document.getElementById("inputPassword").value,
        avatarPath = document.getElementById("inputAvatarPath").value,
        ind = document.getElementById("rolesSelect").selectedIndex.valueOf(),
        role = document.getElementById("rolesSelect").childNodes[ind].value;
    console.log("/Users/CreateUser/?login=" + login + "&email=" + email + "&password=" + password +
            "&avatarPath=" + avatarPath + "&role=" + role);
    $.ajax({
        type: "POST",
        async: false,
        url: "/Users/CreateUser/?login=" + login + "&email=" + email + "&password=" + password +
            "&avatarPath=" + avatarPath + "&role=" + role,
        contentType: "application/json",
        datatype: "json"
    });
    $('#myModal').modal('hide');
    location.reload();
}

function loadRoles(currRole) {
    $.ajax({
        type: "GET",
        async: true,
        url: "/Users/GetRoles",
        contentType: "application/json",
        datatype: "json"
    }).done(function (data) {
        roles = $.parseJSON(data);
        var res = "";
        $.each(roles, function (i) {
            if (roles[i].Id == currRole.RoleId) {
                res += '<option selected="selected" value="' + roles[i].Id + '">' + roles[i].Name + "</option>";
            }
            else {
                res += '<option value="' + roles[i].Id + '">' + roles[i].Name + "</option>";
            }
        });
        document.getElementById("rolesSelect").innerHTML = res;
    });
}

function currentRole(Id) {
    return $.ajax({
        type: "GET",
        async: true,
        url: "/Users/GetCurrentRole/?Id=" + dataSet[Id][8],
        contentType: "application/json",
        datatype: "json"
    });
}

$(document).ready(loadTable());
