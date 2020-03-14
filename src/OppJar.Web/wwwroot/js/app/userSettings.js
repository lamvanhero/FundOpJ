var userSettings = function () {
    var uploadProfileImg = function () {
        $("#select_upload").click();
    };

    var uploadBannerImg = function () {
        $("#coverImage").click();
    };

    return {
        init: function () {
            $("#coverImage").change(function (e) {
                var fileData = new FormData();
                fileData.append("file", $(this).prop('files')[0]);
                let id = $('#user-id').val();
                fileManage.uploadFile(fileData, id, 'Banner', function (url) {
                    $('#hid-profile-banner').val(url);
                    $('#edit-profile-form').submit();
                });
            });

            $("#select_upload").change(function (e) {
                var fileData = new FormData();
                fileData.append("file", $(this).prop('files')[0]);
                let id = $('#user-id').val();
                fileManage.uploadFile(fileData, id, 'Avatar', function (url) {
                    $('#hid-profile-avatar').val(url);
                    $('#edit-profile-form').submit();
                });
            });
        },
        uploadProfileImg: uploadProfileImg,
        uploadBannerImg: uploadBannerImg
    };
}();