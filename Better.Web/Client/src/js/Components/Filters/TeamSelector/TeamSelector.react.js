var React = require('react'),
    FluxMixin = require('../../../Mixins/Flux.mixin'),
    PrerequisiteStore = require('../../../Stores/PrerequisiteStore'),
    FilterStore = require('../../../Stores/FilterStore');

var TeamSelector = React.createClass({
    mixins: [FluxMixin],
    stores: [PrerequisiteStore, FilterStore],
    onTeamSelect: function(){
        var team1 = this.refs.team1.getDOMNode().value;
        var team2 = this.refs.team2.getDOMNode().value;
        this.actions.Filters.update.trigger({
            filter: 'teamNameFilter',
            team1: {
                teamName: team1 === '0' ? null : team1
            },
            team2: {
                teamName: team2 === '0' ? null : team2
            }
        });
    },
    render: function () {
        if(this.state.PrerequisiteStore.status === this.constants.StoreStatus.notSet){
            return <h1>Confirm your season to select teams</h1>
        }
        else if(this.state.PrerequisiteStore.status === this.constants.StoreStatus.fetching){
            return <h1>Fetching teams for selected seasons</h1>
            }
        else{

            var team1Teams = [<option value="0" key="0">Select team 1</option>];
            var team2Teams = [<option value="0" key="0">Select team 2</option>];
            this.state.PrerequisiteStore.teams.forEach(function(team){
                var html = <option value={team} key={team}>{team}</option>
                if(this.state.FilterStore.teamNameFilter.team1.teamName === team){
                    team1Teams.push(html);
                }
                else if(this.state.FilterStore.teamNameFilter.team2.teamName === team){
                    team2Teams.push(html);
                }
                else{
                    team1Teams.push(html);
                    team2Teams.push(html);
                }
            }.bind(this));

            return (
                <div>
                    <div>
                        <label>Team 1</label>
                        <select ref="team1" defaultValue={this.state.FilterStore.teamNameFilter.team1.teamName} onChange={this.onTeamSelect}>{team1Teams}</select>
                    </div>
                    <div>
                        <label>Team 2</label>
                        <select ref="team2" defaultValue={this.state.FilterStore.teamNameFilter.team2.teamName} onChange={this.onTeamSelect}>{team2Teams}</select>
                    </div>
                </div>
            );
        }
    }
});

module.exports = TeamSelector;