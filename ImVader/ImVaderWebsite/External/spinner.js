var opts = {
    lines: 17, // The number of lines to draw
    length: 22, // The length of each line
    width: 4, // The line thickness
    radius: 36, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)
    rotate: 0, // The rotation offset
    color: '#000', // #rgb or #rrggbb
    speed: 1.3, // Rounds per second
    trail: 76, // Afterglow percentage
    shadow: true, // Whether to render a shadow
    hwaccel: true, // Whether to use hardware acceleration
    className: 'spinner', // The CSS class to assign to the spinner
    zIndex: 2e9, // The z-index (defaults to 2000000000)
    top: 25, // Top position relative to parent in px
    left: 25 // Left position relative to parent in px
};
var target;
var spinner;
function initializeSpinner() {
    target = document.getElementById('spinner');
    spinner = new Spinner(opts).spin(target);
}

function startSpinner() {
    $("#loading").removeClass("hidden");
}

function stopSpinner(count) {
    setTimeout(function () {
        $("#loading").addClass("hidden");
    }, count*29);
}