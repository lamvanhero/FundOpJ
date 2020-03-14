var giveManage = function () {
    var give = function () {
        notificationManage.showWarningMessage('Coming soon...')
    };

    return {
        int: function (options) {
            $('.search-input').on('keyup change', function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode === 13) {
                    let searchKey = $(this).val();
                    $(options.tableId).DataTable().search(searchKey).draw();
                }
            });
        },
        give: give
    };
}();