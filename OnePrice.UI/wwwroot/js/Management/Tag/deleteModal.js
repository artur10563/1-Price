$(document).ready(function () {
    function openDeleteModal(tagId) {
        $("#modalPopup").modal('show');
        $("#modalPopup").data("tag-id", tagId);
    }

    $(".btn-delete").click(function () {
        var tagId = $(this).data("tag-id");

        openDeleteModal(tagId);
    });

    $("#continueBtn").click(function () {
        var tagId = $("#modalPopup").data("tag-id");

        $.ajax({
            url: '/Tag/Delete',
            type: 'POST',
            data: { id: tagId },
            success: function (result) {
                $("#modalPopup").modal('hide');
                $("#tr" + tagId).remove();
            },
            error: function (error) {
                $('#deleted').html('<strong>Tag with id ' + tagId + ' cant be deleted.</strong>').removeAttr('hidden');
            }
        });
    });
});