function clearForm() {
    $('#editId').val('');
    $('#editName').val('');
    $('#fileInput').val('');
    $('#originalImgPath').val('');
    $('#uploadedImage').attr('src', '');
    $('#editModalName').text('');
}

function openEditModal(categoryId, categoryName, imgPath) {

    $('#editModal #editId').val(categoryId);
    $('#editModal #editName').val(categoryName);
    $('#editModal #originalImgPath').val(imgPath);
    $('#uploadedImage').attr('src', imgPath);
    $('#editModalName').text(categoryName);
    $('#editModal').modal('show');
}

$(document).on('click', '.btn-edit', function () {
    openEditModal(categoryId, categoryName, imgPath);
});

function submitEdit() {
    var editItemId = $('#editId').val();
    var editName = $('#editName').val();
    var editImgPath = $('#fileInput')[0].files[0];
    var originalImgPath = $('#originalImgPath').val();

    var formData = new FormData();

    formData.append('Id', editItemId);
    formData.append('Name', editName);
    formData.append('ImgPath', originalImgPath);

    if (editImgPath) {
        formData.append('Img', editImgPath);
    } else {
        formData.append('Img', null);
    }

    $.ajax({
        url: '/Category/Edit',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,

        success: function (data) {
            $('#editModal').modal('hide');
            $('#' + editItemId).html(editName);
            clearForm();
        },
        error: function (error) {
            console.error('Error during edit:', error);
        }
    });
}

$('#editModal').on('hidden.bs.modal', function () {
    clearForm();
});