window.jsGridWrapper = {
    init: function (elementId, settings, dotnetHelper) {
        var elem = document.getElementById(elementId);
        if (!elem) {
            throw new Error('No element with ID ' + elementId);
        }
        console.log('settings', settings);

        var $selectedLastRow = null;

        $(elem).jsGrid({
            width: settings.width,
            height: settings.height,

            inserting: settings.inserting,
            editing: settings.editing,
            sorting: settings.sorting,
            paging: settings.paging,

            data: settings.data,

            fields: settings.fields,

            // callbacks
            onItemEditing: function (args) {
                var cancel = dotnetHelper.invokeMethod('OnItemEditingSync',
                    args.item,
                    args.itemIndex,
                    args.previousItem);
                args.cancel = cancel;
            },
            onItemInserted: function () {
                console.log('onItemInserted');
            },
            onItemUpdating: function () {
                console.log('onItemUpdating');
            },
            rowClick: function (args) {
                dotnetHelper.invokeMethodAsync('RowClickAsync', args.item, args.itemIndex);
                if (null != $selectedLastRow) {
                    $selectedLastRow.removeClass('highlight-light');
                }
                $selectedLastRow = $(elem).jsGrid("rowByItem", args.item);
                $selectedLastRow.addClass('highlight-light');
            },
            rowDoubleClick: function(args) {
                dotnetHelper.invokeMethodAsync('RowDoubleClickAsync', args.item, args.itemIndex);
            }
        });
    }
}

