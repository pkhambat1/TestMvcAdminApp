// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on('click', '.clickable', function () {
    if ($(this).find('td input[type=checkbox]').prop("checked") === false) {
        if (event.target.type !== 'checkbox') {
            $(this).find('td input[type=checkbox]').prop('checked', true);
        }
    } else if ($(this).find('td input[type=checkbox]').prop("checked") === true) {
        if (event.target.type !== 'checkbox') {
            $(this).find('td input[type=checkbox]').prop('checked', false);
        }
    }
});