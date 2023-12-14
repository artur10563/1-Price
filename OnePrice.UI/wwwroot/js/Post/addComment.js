$(document).ready(function () {
    $("#submitCommentBtn").click(function () {
        var commentText = $("#commentInput").val();
        var postId = $('#Id').val();

        var commentData = {
            PostId: postId,
            Content: commentText
        };
        $.ajax({
            url: '/Comment/AddComment',
            type: 'POST',
            data: commentData,

            success: function (result) {
                $("#commentsContainer").prepend(result);
                $("#commentInput").val('');
                $("#validationErrors").empty();
            },

            error: function (xhr) {
                if (xhr.responseJSON && xhr.responseJSON.errors) {
                    var errorsHtml = xhr.responseJSON.errors.map(function (error) {
                        return '<p>' + error + '</p>';
                    }).join('');
                    $("#validationErrors").html(errorsHtml);
                } else {
                    $("#validationErrors").html("Could not post a comment. Try again later.");
                }
            }

        });
    });
});