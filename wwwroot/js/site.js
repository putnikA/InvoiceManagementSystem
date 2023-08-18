// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// bankAccountFormatter
$(document).ready(function () {
    $('#BankAccountNumber').on('input', function () {
        var inputVal = $(this).val().replace(/[^0-9]/g, ''); // Remove non-numeric characters

        // Reconstruct the formatted value with fixed hyphen positions
        var formatted = '';

        for (var i = 0; i < inputVal.length; i++) {
            if ((i === 3 || i === 16) && inputVal[i] !== '-') {
                formatted += '-';
            }
            formatted += inputVal[i];
        }

        $(this).val(formatted);
    });
});