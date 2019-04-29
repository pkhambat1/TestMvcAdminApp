// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on('click', '.clickable', function () {
    // If unchecked row is clicked
    if ($(this).find('td input[type=checkbox]').prop("checked") == false) {
        // Row click
        if (event.target.type !== 'checkbox') {
            $(this).find('td input[type=checkbox]').prop('checked', true);
            $(this).css('background', '#b8daff');
            // Checkbox click
        } else {
            $(this).css('background', 'white');
        }
        // If checked row is clicked
    } else if ($(this).find('td input[type=checkbox]').prop("checked") == true) {
        // Row click
        if (event.target.type !== 'checkbox') {
            $(this).find('td input[type=checkbox]').prop('checked', false);
            $(this).css('background', 'white');
            // Checkbox click
        } else {
            $(this).css('background', '#b8daff');
        }
    }
});