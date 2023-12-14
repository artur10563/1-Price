function openCreateModal() {
    $('#createModal #createName').val('');
    $('#createModal #createCode').val('');
    $('#createModal #createSymbol').val('');
    $('#createModal').modal('show');
}

function submitCreate() {
    var createName = $('#createName').val().trim();
    var createCode = $('#createCode').val().trim();
    var createSymbol = $('#createSymbol').val().trim();

    var newCurrency = { FullName: createName, Code: createCode, Symbol: createSymbol }

    $.ajax({
        url: '/Currency/Add',
        type: 'POST',
        data: newCurrency,

        success: function (data) {
            $('#createModal').modal('hide');
            $('#changes').html('<strong>New currencies will be visible after refreshing the page.</strong>').removeAttr('hidden');
        },
        error: function (error) {
            console.error('Error during create:', error);
        }
    });
}

$('#createModal').on('hidden.bs.modal', function () {
    $('#createModal #createName').val('');
});
