var React = require('react');
var DashboardStore = require('../Stores/DashboardStore');

module.exports = React.createClass({
	
	getInitialState: function(){
		return {
			selected: null,
			leagues: DashboardStore.getLeagues()
		}
	},

	componentDidMount: function(){
		DashboardStore.addChangeListener('countryChange', function(){
			this.setState({leagues: DashboardStore.getLeagues()});
		}.bind(this));
	},

	componentDidUnMount: function(){
	
	},

	render: function(){
		
		return (
			<div style={{display: this.state.leagues.length <= 0 ? 'none' : ''}}>
				<label>{this.props.label}</label><br/>
				<select onChange={this._onChange} ref="select">
					<option>Select league</option>
					{this._options()}
				</select>
			</div>
		);

	},

	_options: function(){
		return this.state.leagues.map(function(league){
			return (<option value={league.value} key={league.value}>{league.text}</option>);
		});
	},

	_onChange: function(e){
		
	}


});