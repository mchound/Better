var React = require('react'),
    FluxMixin = require('../../Mixins/Flux.mixin'),
    PrerequisiteStore = require('../../Stores/PrerequisiteStore')

var MatchCounter = React.createClass({
    mixins: [FluxMixin],
    stores: [PrerequisiteStore],
    render: function () {
        var text = 'Not initialized';
        switch (this.state.status) {
            case this.constants.StoreStatus.idle:
                text = this.state.matchCount; break;
            case this.constants.StoreStatus.fetching:
                text = 'Calculating number of matches...'; break;
            default:
                text = 'Not initialized';
        };
        return (
            <h1>{text}</h1>
        );
    }
});

module.exports = MatchCounter;
