var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var DrawsFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'drawsFilter',
    spanMin: 0,
    spanMax: 20
});

module.exports = DrawsFilter;