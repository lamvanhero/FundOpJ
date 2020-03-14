var fileManage = function () {
    var uploadFile = function (file, id, type, callback) {
        $.ajax({
            type: 'POST',
            data: file,
            contentType: false,
            processData: false,
            url: appSettings.apiUrl() + '/api/files?userId=' + id + '&type=' + type,
            beforeSend: function (xhr, data) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + $.cookie('access_token'));
            },
            success: function (data, status) {
                if (callback) callback(data);
            },
            error: function (error) {
                notificationManage.showErrorMessage(error);
            }
        });
    };

    return {
        uploadFile: uploadFile
    };
}();