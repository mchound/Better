var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var WinsFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'winsFilter',
    spanMin: 0,
    spanMax: 20
});

module.exports = WinsFilter;