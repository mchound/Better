var React = require('react');
var FluxOrbit = require('../../FluxOrbit/FluxOrbit');
var CountrySelect = require('../CountrySelect.react.jsx');
var LeagueSelect = require('../LeagueSelect.react.jsx');

module.exports = React.createClass({

	render: function(){
		
		return (
			<div>
				<CountrySelect label="Country" />
				<LeagueSelect label="League" />
			</div>
		);

	}


});