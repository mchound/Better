var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var GoalsDiffFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'goalsDiffFilter',
    spanMin: -999,
    spanMax: 999
});

module.exports = GoalsDiffFilter;