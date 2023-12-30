$(document).ready(function () {
    $('#openComplaintBtn').click(function () {
        $.ajax({
            url: '/Complaint/RenderComplaintModal',
            type: 'GET',
            success: function (data) {
                $('#complaintModalContainer').html(data);
                $('#newComplaintPopup').modal('show');
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $('#complaintModalContainer').on('click', '#continueComplaintBtn', function () {
        var content = $('#complaintContent').val();
        var typeId = $('#complaintTypeId').val();
        var postId = $('#Id').val();

        var complaintData = {
            Content: content,
            TypeId: typeId,
            PostId: postId
        };

        $.ajax({
            url: '/Complaint/Create',
            type: 'POST',
            data: complaintData,
            success: function (response) {
                $('#complaintResponse').removeClass('alert-danger').addClass('alert alert-success').text(response);
                $('#newComplaintPopup').modal('hide');
            },
            error: function (xhr) {
                $('#complaintResponse').removeClass('alert-success').addClass('alert alert-danger').text(xhr.responseText);
                $('#newComplaintPopup').modal('hide');
            }
        });
    });
});