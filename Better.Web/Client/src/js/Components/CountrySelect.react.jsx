var React = require('react');
var DashboardStore = require('../Stores/DashboardStore');
var Actions = require('../Actions/Actions');

module.exports = React.createClass({
	
	getInitialState: function(){
		return {
			selected: null,
			countries: DashboardStore.getCountries()
		}
	},

	componentDidMount: function(){
	
	},

	componentDidUnMount: function(){
	
	},

	render: function(){
		
		return (
			<div>
				<label>{this.props.label}</label><br/>
				<select onChange={this._onChange} ref="select">
					<option>Select country</option>
					{this._options()}
				</select>
			</div>
		);

	},

	_options: function(){
		return this.state.countries.map(function(country){
			return (<option value={country.value} key={country.value}>{country.text}</option>);
		});
	},

	_onChange: function(e){
		Actions.countrySelect.trigger(this.refs.select.getDOMNode().value);
	}


});