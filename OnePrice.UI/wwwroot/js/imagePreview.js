document.getElementById('fileInput').addEventListener('change', function () {
    handleFileChange(this, 'uploadedImage');
});

document.getElementById('fileInputCreate').addEventListener('change', function () {
    handleFileChange(this, 'uploadedImageCreate');
});

function handleFileChange(input, imageId) {
    const file = input.files[0];
    const promptText = document.querySelector('.prompt-text');
    const uploadedImage = document.getElementById(imageId);

    if (file && file.size <= 5 * 1024 * 1024 && file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = function () {
            uploadedImage.src = reader.result;
            promptText.style.display = 'none';
        };
        reader.readAsDataURL(file);
    } else {
        alert("Image size must be 5mb or less!");
    }
}
