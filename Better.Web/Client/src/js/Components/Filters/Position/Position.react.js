var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var PositionFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'positionFilter',
    spanMin: 1,
    spanMax: 20
});

module.exports = PositionFilter;