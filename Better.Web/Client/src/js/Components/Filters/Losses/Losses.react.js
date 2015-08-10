var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var LossesFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'lossesFilter',
    spanMin: 0,
    spanMax: 20
});

module.exports = LossesFilter;