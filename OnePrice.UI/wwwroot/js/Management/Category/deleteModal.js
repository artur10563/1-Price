$(document).ready(function () {
	function openDeleteModal(categoryId) {
		$("#modalPopup .modal-title").text("Confirm Delete");
		$("#modalPopup .modal-body pre").text("Are you sure you want to delete this category?");

		$("#modalPopup").modal('show');

		$("#modalPopup").data("category-id", categoryId);
	}
	$(".btn-delete").click(function () {
		var categoryId = $(this).data("category-id");

		openDeleteModal(categoryId);
	});

	$("#continueBtn").click(function () {
		var categoryId = $("#modalPopup").data("category-id");

		$.ajax({
			url: '/Category/Delete',
			type: 'POST',
			data: { id: categoryId },
			success: function (result) {
				$("#modalPopup").modal('hide');
				$("#tr" + categoryId).remove();
			},
			error: function (error) {
				$('#deleted').html('<strong>Category with id ' + categoryId + ' cant be deleted.</strong>').removeAttr('hidden');
			}
		});
	});
});