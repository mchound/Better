module.exports = {
    format: function () {
        var str = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            str = str.replace('{' + i + '}', arguments[i]);
        }
        return str;
    }
}