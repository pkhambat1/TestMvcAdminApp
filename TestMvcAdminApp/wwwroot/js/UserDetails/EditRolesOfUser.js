$(document).ready(function () {
    editRolesOfUser;
});









var editRolesOfUser = $(".editRolesOfUser").click(function () {
    $.ajax({
        type: 'POST',
        url: '/UserDetails/EditRolesOfUser',
        data: userID = $('.editRolesByUser').attr('id'),
        success: function (d) {
            console.log(userID);

        },
        error: function (err) {
            console.log(err);
        }
    });
});