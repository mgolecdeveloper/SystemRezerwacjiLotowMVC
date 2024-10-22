

document.addEventListener("DOMContentLoaded", function () {

    var leftNavigation = document.getElementById("leftNavigation");
    var sliderAreaWidth = 1;

    leftNavigation.addEventListener("mouseenter", function () {
        leftNavigation.style.left = "0";
    });

    leftNavigation.addEventListener("mouseleave", function () {
        leftNavigation.style.left = `-${50}px`;
    });

    document.addEventListener("mousemove", function (event) {
        var mouseX = event.clientX;

        if (mouseX >= 0 && mouseX <= sliderAreaWidth) {
            leftNavigation.style.left = "0";
        }
    });



    var navigationLinks = document.querySelectorAll('.leftNavigationUl .leftNavigationA');

    navigationLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            //event.preventDefault();

            // Usuń klasę 'active' ze wszystkich linków
            navigationLinks.forEach(function (otherLink) {
                otherLink.classList.remove('active');
            });

            // Dodaj klasę 'active' do klikniętego linku
            link.classList.add('active');

            // Zapisz informacje o klikniętym linku w localStorage
            localStorage.setItem('activeLink', link.href);
        });

        // Sprawdź, czy istnieją informacje o aktywnym linku w localStorage
        var storedLink = localStorage.getItem('activeLink');
        if (storedLink && link.href === storedLink) {
            link.classList.add('active');
        }
    });



    initializeDropdownMenu();

});

