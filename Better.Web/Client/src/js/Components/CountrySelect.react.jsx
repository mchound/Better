var React = require('react');
var FluxOrbit = require('../FluxOrbit/FluxOrbit');

module.exports = React.createClass({
	
	getInitialState: function(){
		return {
			selected: null,
			countries: FluxOrbit.stores.dashboardStore.getCountries()
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
			return (<option value={country.value}>{country.text}</option>);
		});
	},

	_onChange: function(e){
		FluxOrbit.actions.countrySelect.trigger(this.refs.select.getDOMNode().value);
	}


});