var doRequest = function (type, url, data, onSuccess, onError, complexParameter) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                onSuccess(JSON.parse(xhr.responseText));
            }
            else {
                onError(xhr);
            }
        }
    };
    var jsonString = JSON.stringify(data);
    var _url = type === 'GET' && !!complexParameter ? url + '?data=' + jsonString : url;
    xhr.open(type, _url, true);
    xhr.timeout = 10000;
    xhr.ontimeout = onError;
    xhr.setRequestHeader('Accept', 'application/json');
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(jsonString);
}

var Ajax = {
    get: function (url, onSuccess, onError, data, complexParameter) {
        doRequest('GET', url, data, onSuccess, onError, complexParameter);
    },
    post: function (url, onSucces, onError, data) {
        doRequest('POST', url, data, onSucces, onError, false);
    }
};

module.exports = Ajax;