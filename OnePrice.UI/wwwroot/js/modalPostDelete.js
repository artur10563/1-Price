$(document).ready(function () {
    $("#continueBtn").click(function () {
        var id = $("#Id").val();

        $.ajax({
            type: "POST",
            url: "/Post/Delete",
            data: { id: id },
            success: function (response) {
                window.location.href = "/Profile/Index";
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    });
});