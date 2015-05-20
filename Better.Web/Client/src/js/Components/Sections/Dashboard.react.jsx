var React = require('react');
var DashboardStore = require('../../Stores/DashboardStore');
var Select = require('../Select.react.jsx');
var Actions = require('../../Actions/Actions');

module.exports = React.createClass({
	
	getInitialState: function(){
		return {};
	},

	render: function(){
		
		return (
			<div>
				<Select
					label="Country"
					defaultText="Choose Country..."
					getOptions={DashboardStore.getCountries}
					changeAction={Actions.countrySelect} />

				<Select
					label="Country"
					hideIfEmpty={true}
					getOptions={DashboardStore.getLeagues}
					store={DashboardStore}
					storeChangeEvent="countryChange" />
			</div>
		);

	}


});