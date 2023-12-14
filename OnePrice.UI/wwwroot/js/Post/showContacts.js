$(document).ready(function () {
    $('#showContacts').click(function () {
        var phoneNumber = $(this).data('phone');

        if (phoneNumber !== undefined && phoneNumber !== null && phoneNumber.toString().length > 0) {
            $('#showContacts').html(phoneNumber);
        } else {
            $('#showContacts').html('Phone number is not available');
        }
    });
});
