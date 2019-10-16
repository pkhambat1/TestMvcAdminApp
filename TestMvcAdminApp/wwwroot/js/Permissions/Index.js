$(document).ready(function () {
    $('#deletePermission').click(deletePermission);
});

var deletePermission = function () {
    $.ajax({
        type: "GET",
        url: "/permissions/delete?permissionID=" + id,
        success: function (data) {
            if (data && data !== 0) {
                setTimeout(function () {
                    window.location.href = "/permissions";
                }, 2000);
            }
        },
        error: function (err) { }
    });
};

$(document).on('click', '.deletePermission', function () {
    $('.modal-body').empty();
    $('#deletePermission').removeAttr('disabled');
    id = this.dataset.id;
    count = this.dataset.count;
    $('#modalBody').empty();
    if (count > 0) {
        $('#confirmFooter').hide();
        $("#informFooter").show();
        $('#modalBody').append('<p>The permission you are trying to delete is currently assigned to ' + count + ' role(s). Please reassign roles to a different set of permissions before deleting.</p>');
    } else {
        $("#confirmFooter").show();
        $("#informFooter").hide();
        $('#modalBody').append('<p>Are you sure you want to delete this permission?</p>');
    }
});