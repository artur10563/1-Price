$(document).ready(function () {
    function openDeleteModal(currencyId) {
        $("#modalPopup").modal('show');
        $("#modalPopup").data("currency-id", currencyId);
    }

    $(".btn-delete").click(function () {
        var currencyId = $(this).data("category-id");

        openDeleteModal(currencyId);
    });

    $("#continueBtn").click(function () {
        var currencyId = $("#modalPopup").data("currency-id");

        $.ajax({
            url: '/Currency/Delete',
            type: 'POST',
            data: { id: currencyId },
            success: function (result) {
                $("#modalPopup").modal('hide');
                $("#tr" + currencyId).remove();
            },
            error: function (error) {
                $('#deleted').html('<strong>Currency with id ' + currencyId + ' cant be deleted.</strong>').removeAttr('hidden');
            }
        });
    });
});