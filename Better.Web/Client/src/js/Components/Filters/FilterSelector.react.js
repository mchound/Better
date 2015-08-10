var React = require('react'),
    FluxMixin = require('../../Mixins/Flux.mixin'),
    FilterStore = require('../../Stores/FilterStore');

var FilterSelector = React.createClass({
    mixins: [FluxMixin],
    stores: [FilterStore],
    onFilterToggle: function(filter){
        this.actions.Filters.add.trigger({filter: filter});
    },
    render: function () {
        return (
            <div>
                <button onClick={this.onFilterToggle.bind(this, 'side')}>Home/Away</button>
                <button onClick={this.onFilterToggle.bind(this, 'positionFilter')}>Position</button>
                <button onClick={this.onFilterToggle.bind(this, 'matchesFilter')}>Matches</button>
                <button onClick={this.onFilterToggle.bind(this, 'winsFilter')}>Wins</button>
                <button onClick={this.onFilterToggle.bind(this, 'drawsFilter')}>Draws</button>
                <button onClick={this.onFilterToggle.bind(this, 'lossesFilter')}>Losses</button>
                <button onClick={this.onFilterToggle.bind(this, 'goalsMadeFilter')}>Goals made</button>
                <button onClick={this.onFilterToggle.bind(this, 'goalsConcededFilter')}>Goals conceded</button>
                <button onClick={this.onFilterToggle.bind(this, 'goalsDiffFilter')}>Goal diff</button>
                <button onClick={this.onFilterToggle.bind(this, 'pointsFilter')}>Points</button>
            </div>
        );
    }
});

module.exports = FilterSelector;