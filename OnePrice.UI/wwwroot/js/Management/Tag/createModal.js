function openCreateModal() {
    $('#createModal #createName').val('');
    $('#createModal').modal('show');
}

function submitCreate() {
    var createName = $('#createName').val();
    var newTag = { Name: createName }

    $.ajax({
        url: '/Tag/Add',
        type: 'POST',
        data: newTag,

        success: function (data) {
            $('#createModal').modal('hide');
            $('#changes').html('<strong>New tags will be visible after refreshing the page.</strong>').removeAttr('hidden');
        },
        error: function (error) {
            console.error('Error during create:', error);
        }
    });
}

$('#createModal').on('hidden.bs.modal', function () {
    $('#createModal #createName').val('');
});