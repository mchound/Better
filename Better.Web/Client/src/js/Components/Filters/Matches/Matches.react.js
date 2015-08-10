var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var MatchesFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'matchesFilter',
    spanMin: 1,
    spanMax: 20
});

module.exports = MatchesFilter;