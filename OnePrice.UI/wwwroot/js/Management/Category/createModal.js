function openCreateModal() {
	$('#createModal #createName').val('');
	$('#createModal #uploadedImageCreate').attr('src', '');
	$('#createModal .prompt-text').text('Click to choose an image (max 5MB)');
	$('#createModal').modal('show');
}

function submitCreate() {
	var createName = $('#createName').val();
	var createImgPath = $('#fileInputCreate')[0].files[0];

	var formData = new FormData();

	formData.append('Name', createName);

	if (createImgPath) {
		formData.append('Img', createImgPath);
	} else {
		formData.append('Img', null);
	}

	$.ajax({
		url: '/Category/Add',
		type: 'POST',
		processData: false,
		contentType: false,
		data: formData,

		success: function (data) {
			$('#createModal').modal('hide');
			$('#changes').html('<strong>New categories will be visible after refreshing the page.</strong>').removeAttr('hidden');
			clearForm();
		},
		error: function (error) {
			console.error('Error during create:', error);
		}
	});
}

$('#createModal').on('hidden.bs.modal', function () {
	clearForm();
});