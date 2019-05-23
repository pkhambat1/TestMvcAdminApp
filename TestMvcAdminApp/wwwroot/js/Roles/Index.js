$(document).ready(function () {
    $('#deleteRole').click(deleteRole);
});

var deleteRole = function () {
    $.ajax({
        type: "GET",
        url: "/roles/delete?roleID=" + id,
        success: function (data) {
            if (data && data !== 0) {
                setTimeout(function () {
                    window.location.href = "/roles";
                }, 2000);
            }
        },
        error: function (err) { }
    });
};

$(document).on('click', '.deleteRole', function () {
    $('.modal-body').empty();
    $('#deleteRole').removeAttr('disabled');
    id = this.dataset.id;
    count = this.dataset.count;
    $('#modalBody').empty();
    if (count > 0) {
        console.log("count > 0");
        $('#confirmFooter').hide();
        $('#informFooter').show();
        $('#modalBody').append('<p>The role you are trying to delete is currently assigned to ' + count + ' user(s). Please reassign users to a different set of roles before deleting.</p>');
    } else {
        console.log("count <= 0");
        $('#confirmFooter').show();
        $('#informFooter').hide();
        $('#modalBody').append('<p>Are you sure you want to delete this role?</p>');
    }
});