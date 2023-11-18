document.addEventListener('DOMContentLoaded', function () {
    var sortOrderInput = $('#sortOrderInput');
    var sortOrderIcon = $('#sortOrderIcon');

    
    sortOrderIcon.removeClass('fas fa-sort-up fas fa-sort-down').addClass(getSortIconClass(sortOrderInput.val()));

    document.getElementById('sortOrderToggle').addEventListener('click', function () {
        var sortOrder = sortOrderInput.val() === 'asc' ? 'desc' : 'asc';

        sortOrderInput.val(sortOrder);

        sortOrderIcon.removeClass('fas fa-sort-up fas fa-sort-down').addClass(getSortIconClass(sortOrder));
    });

    function getSortIconClass(order) {
        return order === 'asc' ? 'fas fa-sort-up' : (order === 'desc' ? 'fas fa-sort-down' : 'fas fa-sort');
    }
});