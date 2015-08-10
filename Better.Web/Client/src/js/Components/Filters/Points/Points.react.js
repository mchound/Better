var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var PointsFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'pointsFilter',
    spanMin: 0,
    spanMax: 20
});

module.exports = PointsFilter;