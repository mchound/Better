var React = require('react'),
    MatchSideFilter = require('./MatchSide/MatchSide.react'),
    PositionFilter = require('./Position/Position.react'),
    DrawsFilter = require('./Draws/Draws.react'),
    GoalsConcededFilter = require('./GoalsConceded/GoalsConceded.react'),
    GoalsDiffFilter = require('./GoalsDiff/GoalsDiff.react'),
    GoalsMadeFilter = require('./GoalsMade/GoalsMade.react'),
    LossesFilter = require('./Losses/Losses.react'),
    MatchesFilter = require('./Matches/Matches.react'),
    PointsFilter = require('./Points/Points.react'),
    WinsFilter = require('./Wins/Wins.react');

var FilterList = React.createClass({
    render: function () {
        return (
            <ul>
                <MatchSideFilter />
                <PositionFilter />
                <DrawsFilter />
                <GoalsConcededFilter />
                <GoalsDiffFilter />
                <GoalsMadeFilter />
                <LossesFilter />
                <MatchesFilter />
                <PointsFilter />
                <WinsFilter />
            </ul>  
        );
    }
});

module.exports = FilterList;