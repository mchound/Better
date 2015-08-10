var React = require('react'),
    FluxMixin = require('../../Mixins/Flux.mixin'),
    StatisticsStore = require('../../Stores/StatisticsStore');

var Result = React.createClass({
    mixins: [FluxMixin],
    stores: [StatisticsStore],
    render: function () {
        if(this.state.stats === null) return null;

        var matches = this.state.stats.matches.map(function(match){
            return (
                    <tr key={match.Date + match.HomeTeam}>
                        <td>{match.Date.substring(0, 10)}</td>
                        <td>{match.HtTableRow.Matches}</td>
                        <td>{match.HomeTeam}</td>
                        <td>{match.AwayTeam}</td>
                        <td>{match.AtTableRow.Matches}</td>
                        <td>{match.HtGoals + ' - ' + match.AtGoals}</td>
                    </tr>
                );
        });

        return (
            <div>
                <div>{'Matches: ' +  this.state.stats.matchCount}</div>
                <div>{'Team 1: ' + (this.state.stats.team1Wins * 100.0).toFixed(2) + '%'}</div>
                <div>{'Draws: ' + (this.state.stats.draws * 100.0).toFixed(2) + '%'}</div>
                <div>{'Team 2: ' + (this.state.stats.team2Wins * 100.0).toFixed(2) + '%'}</div>
                <table>
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Matches</th>
                            <th>Home</th>
                            <th>Away</th>
                            <th>Matches</th>
                            <th>Result</th>
                        </tr>
                    </thead>
                    <tbody>
                        {matches}
                    </tbody>
                </table>
            </div>
        );
    }
});

module.exports = Result;