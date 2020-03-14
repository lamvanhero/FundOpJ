var appSettings = function () {
    var _options = {};

    return {
        init: function (options) {
            _options = options;
        },
        apiUrl: function () {
            return _options.apiUrl;
        },
        linkPreviewKey: function () {
            return _options.linkPreviewKey;
        }
    };
}();

var notificationManage = function () {
    var showErrorMessage = function (error) {
        let errorMsg;

        if (error.status) {
            switch (error.status) {
                case 403: errorMsg = "You don't have permission to do this action!"; break;
                case 404: errorMsg = "Not found!"; break;
                case 400: {
                    if (error.responseJSON.errors) {
                        errorMsg = error.responseJSON.errors.split(";").join("</br>");
                    }
                    else {
                        errorMsg = error.responseJSON.error;
                    }
                    break;
                }
                default: errorMsg = "Somethings went wrong!"; break;
            }
        }
        else {
            if (error.responseJSON) {
                errorMsg = error.responseJSON.error;
            }
            else if (error.statusText) {
                errorMsg = error.statusText;
            }
            else {
                errorMsg = error;
            }
        }

        $.toast({
            title: errorMsg,
            type: 'error',
            delay: 3000
        });
    };

    var showSuccessMessage = function (message) {
        $.toast({
            title: message,
            type: 'success',
            delay: 3000
        });
    };

    var showWarningMessage = function (message) {
        $.toast({
            title: message,
            type: 'warning',
            delay: 3000
        });
    };

    return {
        showErrorMessage: showErrorMessage,
        showSuccessMessage: showSuccessMessage,
        showWarningMessage: showWarningMessage
    };
}();

function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(id).attr("src", e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

function dataURItoBlob(dataURI) {
    var byteString = atob(dataURI.split(',')[1]);

    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }

    return new Blob([ab], { type: mimeString });
}

function _calculateAge(birthday) {
    var ageDifMs = Date.now() - birthday.getTime();
    var ageDate = new Date(ageDifMs);
    return Math.abs(ageDate.getUTCFullYear() - 1970);
}

function camelize(str) {
    try {
        return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function (word, index) {
            return index === 0 ? word.toLowerCase() : word.toUpperCase();
        }).replace(/\s+/g, '');
    }
    catch{
        return '';
    }
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};