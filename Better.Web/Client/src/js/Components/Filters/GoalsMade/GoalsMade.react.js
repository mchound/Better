var React = require('react'),
    SpanFilterMixin = require('../../../Mixins/SpanFilter.mixin');

var GaolsMadeFilter = React.createClass({
    mixins: [SpanFilterMixin],
    filter: 'goalsMadeFilter',
    spanMin: 0,
    spanMax: 999
});

module.exports = GaolsMadeFilter;