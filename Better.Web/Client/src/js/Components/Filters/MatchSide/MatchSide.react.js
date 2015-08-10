var React = require('react'),
    FluxMixin = require('../../../Mixins/Flux.mixin'),
    FilterStore = require('../../../Stores/FilterStore'),
    FancyRadio = require('../../Common/FancyRadio.react'),
    Constants = require('../../../Constants/Constants');

var MatchSideFilter = React.createClass({
    mixins: [FluxMixin],
    stores: [FilterStore],
    onTeam1Change: function(option){
        var payload = {
            filter: 'side',
            team1: option.value
        };
        if(option.value === Constants.MatchSide.home){
            payload.team2 = Constants.MatchSide.away;
        }
        else if(option.value === Constants.MatchSide.away){
            payload.team2 = Constants.MatchSide.home;
        }
        else{
            payload.team2 = Constants.MatchSide.notSet;
        }
        this.actions.Filters.update.trigger(payload);
    },
    onTeam2Change: function(option){
        var payload = {
            filter: 'matchSide',
            team2: option.value
        };
        if(option.value === Constants.MatchSide.home){
            payload.team1 = Constants.MatchSide.away;
        }
        else if(option.value === Constants.MatchSide.away){
            payload.team1 = Constants.MatchSide.home;
        }
        else{
            payload.team1 = Constants.MatchSide.notSet;
        }
        this.actions.Filters.update.trigger(payload);
    },
    render: function () {
        if (!this.state.side.active) return null;

        var team1Options = [
            {value: Constants.MatchSide.home, text: 'Home', selected: this.state.side.team1 === Constants.MatchSide.home}, 
            {value: Constants.MatchSide.notSet, text: 'Net set', selected: this.state.side.team1 === Constants.MatchSide.notSet}, 
            {value: Constants.MatchSide.away, text: 'Away', selected: this.state.side.team1 === Constants.MatchSide.away}
        ];

        var team2Options = [
            {value: Constants.MatchSide.home, text: 'Home', selected: this.state.side.team2 === Constants.MatchSide.home}, 
            {value: Constants.MatchSide.notSet, text: 'Net set', selected: this.state.side.team2 === Constants.MatchSide.notSet}, 
            {value: Constants.MatchSide.away, text: 'Away', selected: this.state.side.team2 === Constants.MatchSide.away}
        ];

        return (
              <div>
                <div>
                    <label>Team 1</label>
                    <FancyRadio options={team1Options} onChange={this.onTeam1Change} />
                </div>
                <div>
                    <label>Team 2</label>
                    <FancyRadio options={team2Options} onChange={this.onTeam2Change} />
                </div>
              </div>
        );
    }
});

module.exports = MatchSideFilter;