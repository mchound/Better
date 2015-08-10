var React = require('react'),
    Actions = require('../../Actions/Actions'),
    If = require('../Common/If.react');

var AdminMatch = React.createClass({
    getInitialState: function(){
        return {isAdding: false};
    },
    componentWillRecieveProps: function(nextProps){
        this.setState({isAdding: this.state.isAdding && !nextProps.match.exists});
    },
    onAddClick: function(){
        this.setState({isAdding: true});
        Actions.Admin.addMatch.trigger(this.props.match);
    },
    render: function () {
        return (
            <tr>
                <td>{this.props.match.date}</td>
                <td>{this.props.match.homeTeam}</td>
                <td>{this.props.match.awayTeam}</td>
                <td>{this.props.match.htGoals + '-' + this.props.match.atGoals}</td>
                <td>
                    <If condition={!this.props.match.exists}>
                        <button onClick={this.onAddClick} disabled={this.state.adding}>{this.state.isAdding ? 'Adding match' : 'Add'}</button>
                    </If>
                </td>
            </tr>      
        );
    }
});

module.exports = AdminMatch;