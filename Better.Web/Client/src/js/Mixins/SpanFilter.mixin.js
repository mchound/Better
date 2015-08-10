var React = require('react'),
    FluxMixin = require('../Mixins/Flux.mixin'),
    FilterStore = require('../Stores/FilterStore'),
    NumberSpan = require('../Components/Common/NumberSpan.react'),
    Constants = require('../Constants/Constants');

module.exports = {
    mixins: [FluxMixin],
    stores: [FilterStore],
    onTeam1Change: function(span){
        this.actions.Filters.update.trigger({
            filter: this.filter,
            team1: {
                min: span.lower,
                max: span.upper
            }
        });
    },
    onTeam2Change: function(span){
        this.actions.Filters.update.trigger({
            filter: this.filter,
            team2: {
                min: span.lower,
                max: span.upper
            }
        });
    },
    render: function () {
        if (!this.state[this.filter].active) return null;

        return (
              <div>
                <h3>{this.filter}</h3>
                <div>
                    <label>Team 1</label>
                    <NumberSpan min={this.spanMin} max={this.spanMax} onChange={this.onTeam1Change} />
                </div>
                <div>
                    <label>Team 2</label>
                    <NumberSpan min={this.spanMin} max={this.spanMax} onChange={this.onTeam2Change} />
                </div>
              </div>
        );
    }
};