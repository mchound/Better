module.exports = {
    values: function (selectElement) {
        var values = [];
        for (var i = 0; i < selectElement.selectedOptions.length; i++) {
            values.push(selectElement.selectedOptions[i].value);
        }
        return values;
    }
}