window.addEventListener('DOMContentLoaded', function () {
    console.log('document loaded');

    let elements = document.getElementsByTagName('h6');
    console.log(elements.length);

    let searchedH6 = elements[0];

    searchedH6.innerText = 'Changed text';

});