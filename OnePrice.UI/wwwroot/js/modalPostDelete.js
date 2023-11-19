$(document).ready(function () {
    $("#continueBtn").click(function () {
        var id = $("#Id").val();
        var form = $('<form>', {
            'method': 'post',
            'action': '/Post/Delete',
            'style': 'display:none;'
        }).append($('<input>', {
            'type': 'hidden',
            'name': 'id',
            'value': id
        })).appendTo('body');

        form.submit();
    });
});