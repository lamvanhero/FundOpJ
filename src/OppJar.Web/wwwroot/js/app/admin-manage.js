var adminManage = function () {
    var changeUserStatus = function (status, id) {
        $.ajax({
            type: "POST",
            url: "/admins/users/" + id + "/" + status,
            success: function (data, status) {
                notificationManage.showSuccessMessage("Success!");
                setTimeout(function () {
                    location.reload();
                }, 500);
            },
            error: function (error) {
                notificationManage.showErrorMessage(error);
            }
        });
    };

    var saveChange = function () {
        let userId = $('#modal-profile #user-id').val();
        let successMsg = userId === "" ? "Created!" : "Updated!";
        $.ajax({
            type: "POST",
            url: "/admins/users/" + userId,
            data: $('#frm-profile').serializeObject(),
            success: function (data, status) {
                if (data.success) {
                    notificationManage.showSuccessMessage(successMsg);
                    setTimeout(function () {
                        location.reload();
                    }, 500);
                }
                else {
                    $('#frm-profile').html(data);
                }
            },
            error: function (error) {
                notificationManage.showErrorMessage(error);
            }
        });
    };

    var getProfile = function (id, callback) {
        $.ajax({
            type: 'GET',
            url: "/admins/users/" + id,
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
        init: function () {
            $('#modal-profile').on('show.bs.modal', function (event) {
                let id = $(event.relatedTarget).data('id');
                if (id === "") {
                    $(this).find('.modal-title').html("Add Admin");
                    $(this).find('#user-id').val("");
                }
                else {
                    $(this).find('.modal-title').html("Edit Admin");
                    $(this).find('#user-id').val(id);
                }

                getProfile(id, function (data) {
                    $('#modal-profile #frm-profile').html(data);
                });
            });
        },
        saveChange: saveChange,
        changeUserStatus: changeUserStatus
    };
}();