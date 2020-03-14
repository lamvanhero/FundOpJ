var memoriesManage = function () {
    var openFile = function () {
        $("#feed-upload").click();
    };

    var reset = function () {
        file = null;
        feedType = "Text";
        $("#feed-upload").val("");
        $("#post-text-area").val("");
        $("#clear-button").hide();
        $(".lnk-preview").html('');
        $(".lnk-preview").hide();
        resetPreview();
    };

    var resetPreview = function () {
        $("#img-preview").attr("src", "#");
        $("#img-preview").hide();
        $("#video-preview").attr("src", "#");
        $("#video-preview").hide();
        $("#audio-preview").attr("src", "#");
        $("#audio-preview").hide();
    };

    var createPost = function (data) {
        console.log('data: ',data);
        ajaxWrapper.post('/api/Memory', data, function (response) {
            notificationManage.showSuccessMessage("Posted!");
            setTimeout(function () {
                window.location.reload();
            }, 500);
        },
            function (error) {
                notificationManage.showErrorMessage(error);
            });
    };

    var deletePost = function (feedId) {
        ajaxWrapper.delete('/api/Memory/' + feedId, function (response) {
            notificationManage.showSuccessMessage("Deleted!");
            setTimeout(function () {
                window.location.reload();
            }, 500);
        });
    };

    var shareFacebook = function (data) {
        FB.ui(
            {
                method: 'share',
                href: window.location.origin + data.url
            },
            function (response) {
                if (response) {
                    ajaxWrapper.update('/api/Memory/' + data.feedId + '/change-privacy/Public', null, function (response) {
                        notificationManage.showSuccessMessage("Shared!");
                    });
                }
            }
        );
    };

    var hideLoadMore = function () {
        $("#feed-load-more").hide();
    };

    var buildPreview = function (response) {
        return '<div class="col-md-2 col-sm-2">' +
            '<img class="img-responsive" src="' + response.image + '"></div>' +
            '<div class="col-md-10 col-sm-10">' +
            '<a style="font-size:18px;" href="' + response.url + '" target="_blank">' + response.title + '</a>' +
            '<p>' + response.description + '</p></div>';
    };

    return {
        init: function () {
            var file = null;
            var feedType = "Text";
            var page = 1;

            reset();

            if (parseInt($("#total-page").val()) <= page) hideLoadMore();

            $(document).on('click', '.btn-comment-post', function () {
                let feedId = $(this).data("feed-id");

                let comment = {
                    Content: $("#comment-text-area-" + feedId).val(),
                    ReferId: feedId
                };

                $.ajax({
                    type: "POST",
                    data: comment,
                    url: '/Memory/PostComment',
                    beforeSend: function (xhr, data) {
                        $('#preloader').show();
                        xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
                    },
                    success: function (data, status) {
                        $('#preloader').hide();
                        notificationManage.showSuccessMessage("Posted!");
                        $("#comment-area-" + feedId).prepend(data);
                        $("#comment-text-area-" + feedId).val("");
                        $("#feed-comment-count-" + feedId).html(parseInt($("#feed-comment-count-" + feedId).html()) + 1);
                    },
                    error: function (error) {
                        $('#preloader').hide();
                        notificationManage.showErrorMessage(error);
                    }
                });
            });

            $(document).on('click', '.feed-like', function () {
                let feedId = $(this).data("feed-id");
                ajaxWrapper.post('/api/Memory/' + feedId + '/like', null, function (response) {
                    notificationManage.showSuccessMessage("Liked!");
                    $("#feed-nbr-like-" + feedId).html(parseInt($("#feed-nbr-like-" + feedId).html()) + 1);
                });
            });

            $("#post-text-area").keyup(function () {
                let value = $(this).val();
                if (value !== "" || file !== null) {
                    $("#clear-button").show();
                }
                else {
                    $("#clear-button").hide();
                }

                var matches = value.match(/\bhttps?:\/\/\S+/gi);

                if (matches) {
                    $.ajax({
                        url: "https://api.linkpreview.net",
                        dataType: 'jsonp',
                        data: { q: matches[0], key: appSettings.linkPreviewKey() },
                        success: function (response) {
                            $("#hid-preview").val(JSON.stringify(response));
                            $(".lnk-preview").html(buildPreview(response));
                            $(".lnk-preview").show();
                        }
                    });
                }
            });

            $("#feed-upload").change(function () {
                resetPreview();
                $("#clear-button").show();
                file = $(this).prop("files")[0];
                if (file.type.match("image")) {
                    feedType = "Image";
                    $("#img-preview").attr("src", URL.createObjectURL(file));
                    $("#img-preview").show();
                }

                if (file.type.match("video")) {
                    feedType = "Video";
                    $("#video-preview").attr("src", URL.createObjectURL(file));
                    $("#video-preview").show();
                }

                if (file.type.match("audio")) {
                    feedType = "Audio";
                    $("#audio-preview").attr("src", URL.createObjectURL(file));
                    $("#audio-preview").show();
                }

                $(".lnk-preview").html('');
                $(".lnk-preview").hide();
            });

            $("#post-button").click(function () {
                let postedData = {
                    userSlug: $("#user-slug").val(),
                    content: $("#post-text-area").val(),
                    feedType: feedType,
                    preview: $("#hid-preview").val(),
                    fileUrl: ""
                };
                if (file) {
                    var fileData = new FormData();
                    fileData.append("file", file);
                    fileManage.uploadFile(fileData, $("#user-id").val(), "Feed", function (url) {
                        postedData.fileUrl = url;
                        createPost(postedData);
                    });
                }
                else {
                    createPost(postedData);
                }
            });

            $("#btn-feed-load-more").click(function () {
                $.ajax({
                    type: "GET",
                    url: "/user/" + $("#user-slug").val() + "/feeds/" + (page + 1),
                    beforeSend: function (xhr, data) {
                        xhr.setRequestHeader("Authorization", "Bearer " + $.cookie("access_token"));
                    },
                    success: function (data, status) {
                        page += 1;
                        if (parseInt($("#total-page").val()) === page) hideLoadMore();
                        $("#feed").append(data);
                    },
                    error: function (error) {
                        notificationManage.showErrorMessage(error);
                    }
                });
            });

            window.fbAsyncInit = function () {
                FB.init({
                    appId: '109011126433538',
                    cookie: true,
                    xfbml: true,
                    version: 'v6.0'
                });
            };

            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = "https://connect.facebook.net/en_US/sdk.js";
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));
        },
        openFile: openFile,
        reset: reset,
        createPost: createPost,
        deletePost: deletePost,
        shareFacebook: shareFacebook
    };
}();