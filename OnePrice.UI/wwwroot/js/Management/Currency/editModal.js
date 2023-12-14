function openEditModal(currencyId, currencyName, currencyCode, currencySymbol) {

    $('#editModal #editId').val(currencyId);
    $('#editModal #editName').val(currencyName);
    $('#editModal #editCode').val(currencyCode);
    $('#editModal #editSymbol').val(currencySymbol);

    $('#editModalName').text(currencyName);
    $('#editModal').modal('show');
}

function submitEdit() {
    var editItemId = $('#editId').val().trim();
    var editName = $('#editName').val().trim();
    var editCode = $('#editCode').val().trim();
    var editSymbol = $('#editSymbol').val().trim();

    var edited = {
        Id: editItemId,
        FullName: editName,
        Code: editCode,
        Symbol: editSymbol
    }

    $.ajax({
        url: '/Currency/Edit',
        type: 'POST',
        data: edited,
        success: function (data) {
            $('#editModal').modal('hide');
            $('#Name' + editItemId).html(editName);
            $('#Code' + editItemId).html(editCode);
            $('#Symbol' + editItemId).html(editSymbol);
        },
        error: function (error) {
            console.error('Error during edit:', error);
        }
    });
}
