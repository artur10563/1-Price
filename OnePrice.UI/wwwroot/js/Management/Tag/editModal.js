function openEditModal(tagId, tagName) {

	$('#editModal #editId').val(tagId);
	$('#editModal #editName').val(tagName);

	$('#editModalName').text(tagName);
	$('#editModal').modal('show');
}

function submitEdit() {
	var editItemId = $('#editId').val().trim();
	var editName = $('#editName').val().trim();

	var edited = {
		Id: editItemId,
		Name: editName,
	}

	$.ajax({
		url: '/Tag/Edit',
		type: 'POST',
		data: edited,
		success: function (data) {
			$('#editModal').modal('hide');
			$('#Name' + editItemId).html(editName);
		},
		error: function (error) {
			console.error('Error during edit:', error);
		}
	});
}
