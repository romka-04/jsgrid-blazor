window.jsGridWrapper = {
    init: function (elementId, settings) {
        var elem = document.getElementById(elementId);
        if (!elem) {
            throw new Error('No element with ID ' + elementId);
        }
        console.log('settings', settings);
        $(elem).jsGrid(settings);
    }
}