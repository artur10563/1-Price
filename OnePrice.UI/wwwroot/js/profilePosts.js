$(document).ready(function () {
    $("#btnActive").addClass('active');
    filterPosts(true);

    $("#btnActive").click(function () {
        setActiveButton("#btnActive");
        filterPosts(true);
    });

    $("#btnDisabled").click(function () {
        setActiveButton("#btnDisabled");
        filterPosts(false);
    });

    function setActiveButton(buttonId) {
        $(".btn-underline").removeClass('active');
        $(buttonId).addClass('active');
    }

    function filterPosts(isActive) {
        var url = '/Post/UserFilteredPosts';

        $.ajax({
            url: url,
            data: { isActive: isActive },
            type: 'GET',
            success: function (result) {
                $('#filteredPostsContainer').html('');
                $('#filteredPostsContainer').append(result);
            },
        });
    }
});
