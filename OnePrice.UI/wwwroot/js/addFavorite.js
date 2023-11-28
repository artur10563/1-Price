$(document).ready(function () {
    $(document).on("click", ".fa-heart", function () {
        var icon = $(this);
        var id = icon.data("id");
        var url = '/Post/AddFavorite';

        $.ajax({
            url: url,
            method: "POST",
            data: { postId: id },
            success: function () {
                icon.toggleClass("far fas");
            },
            error: function (response) {
                alert("Log in to perform this action");
            }
        });
    });
});