var ajaxWrapper = function () {
    var options = {};

    var get = function (url, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: options.baseUrl + url,
            beforeSend: function (xhr, data) {
                $('#preloader').show();
                xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
            },
            success: function (data, status) {
                $('#preloader').hide();
                if (successCallback) successCallback(data);
            },
            error: function (error) {
                $('#preloader').hide();
                if (errorCallback)
                    errorCallback(error);
                else
                    notificationManage.showErrorMessage(error);
            }
        });
    };

    var post = function (url, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json",
            url: options.baseUrl + url,
            beforeSend: function (xhr, data) {
                $('#preloader').show();
                xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
            },
            success: function (data, status) {
                $('#preloader').hide();
                if (successCallback) successCallback(data);
            },
            error: function (error) {
                $('#preloader').hide();
                if (errorCallback)
                    errorCallback(error);
                else
                    notificationManage.showErrorMessage(error);
            }
        });
    };

    var remove = function (url, successCallback, errorCallback) {
        $.ajax({
            type: "DELETE",
            url: options.baseUrl + url,
            beforeSend: function (xhr, data) {
                $('#preloader').show();
                xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
            },
            success: function (data, status) {
                $('#preloader').hide();
                if (successCallback) successCallback(data);
            },
            error: function (error) {
                $('#preloader').hide();
                if (errorCallback)
                    errorCallback(error);
                else
                    notificationManage.showErrorMessage(error);
            }
        });
    };

    var update = function (url, data, successCallback, errorCallback) {
        var configs = {
            type: "PUT",
            url: options.baseUrl + url,
            beforeSend: function (xhr, data) {
                $('#preloader').show();
                xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
            },
            success: function (data, status) {
                $('#preloader').hide();
                if (successCallback) successCallback(data);
            },
            error: function (error) {
                $('#preloader').hide();
                if (errorCallback)
                    errorCallback(error);
                else
                    notificationManage.showErrorMessage(error);
            }
        };

        if (data) {
            $.extend(configs, {
                data: JSON.stringify(data),
                contentType: "application/json",
            });
        }

        $.ajax(configs);
    };

    return {
        init: function (_options) {
            options = _options;
        },
        get: get,
        post: post,
        delete: remove,
        update: update
    };
}();