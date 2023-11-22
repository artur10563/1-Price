
function initializeImagePreview(elementId) {
    const elementsWithStyle = $('#' + elementId);

    elementsWithStyle.on('change', function () {
        const file = this.files[0];
        const promptText = $(this).siblings('.prompt-text');
        const uploadedImage = $(this);

        if (file && file.size <= 5 * 1024 * 1024 && file.type.startsWith('image/')) {
            const reader = new FileReader();
            reader.onload = function () {
                uploadedImage.attr('src', reader.result);
                promptText.hide();
            };
            reader.readAsDataURL(file);
        } else {
            alert("Image size must be 5mb or less!");
        }
    });
}

initializeImagePreview('uploadedImageCreate');
initializeImagePreview('uploadedImage');
initializeImagePreview('uploadedImg');
initializeImagePreview('uploadedImageEdit');
