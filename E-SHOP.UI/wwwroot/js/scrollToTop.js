function scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

let scrollUpButton = document.getElementById('scrollUpButton');
let isScrolling = false;

window.addEventListener('scroll', function () {
    if (!isScrolling) {
        isScrolling = true;
        scrollUpButton.style.display = 'block';

        setTimeout(function () {
            isScrolling = false;
            scrollUpButton.style.display = 'none';
        }, 2000);
    }
});

scrollUpButton.addEventListener('click', scrollToTop);

