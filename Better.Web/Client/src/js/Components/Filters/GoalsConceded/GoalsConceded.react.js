var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var GoalsConcededFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'goalsConcededFilter',
    spanMin: 0,
    spanMax: 999
});

module.exports = GoalsConcededFilter;