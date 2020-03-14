var eventManage = function () {
    var file = null;

    var updateEvent = function () {
        let postData = {};
        var formData = $('#eventForm').serializeObject();
        $.each(formData, function (item, index) {
            postData[camelize(item.split('.')[1])] = formData[item];
        });
        ajaxWrapper.update('/api/event/' + postData.id, postData, function (resp, stat) {
            notificationManage.showSuccessMessage('Saved!');
            setTimeout(function () {
                window.location.reload();
            }, 500);
        });
    };

    var saveChange = function () {
        if (file) {
            var fileData = new FormData();
            fileData.append("file", file);
            fileManage.uploadFile(fileData, $("#user-id").val(), "Event", function (url) {
                $('#url-event-photo').val(url);
                updateEvent();
            });
        }
        else {
            updateEvent();
        }
    };

    var donate = function () {
        let postData = {};
        var formData = $('#eventForm').serializeObject();
        $.each(formData, function (item, index) {
            postData[camelize(item.split('.')[1])] = formData[item];
        });

        stripeManage.checkout({
            name: postData.senderName,
            email: postData.email,
            amount: postData.amount
        });

        pubsub.subscribe('PaymentSuccessfully', function (arg) {
            $.ajax({
                type: "POST",
                url: "/account/give",
                data: postData,
                success: function (data, status) {
                    if (data.success) {
                        notificationManage.showSuccessMessage("Donate succeed.");
                    }
                    else {
                        notificationManage.showErrorMessage("Donation failed, please try again.");
                    }
                    setTimeout(function () {
                        window.location.reload();
                    }, 500);
                },
                error: function (error) {
                    notificationManage.showErrorMessage(error);
                }
            });
        });
    };

    return {
        init: function () {
            $("#file-upload-event").change(function (e) {
                file = $(this).prop("files")[0];
                let imageSrc = URL.createObjectURL(file);
                $("#event-image").attr("src", imageSrc);
            });
        },
        saveChange: saveChange,
        donate: donate
    };
}();