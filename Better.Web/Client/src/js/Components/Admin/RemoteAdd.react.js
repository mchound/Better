var React = require('react'),
    Seasons = require('../../Data/Seasons'),
    Actions = require('../../Actions/Actions'),
    AdminStore = require('../../Stores/AdminStore');

function filterLeagues(countryId){
    return Seasons.leagues.filter(function(league){
        return league.countryId === countryId;
    });
}

var RemoteAdd = React.createClass({
    getInitialState: function(){
        return {
            leagues: filterLeagues(Seasons.countries[0].id)
        }
    },
    onCountryChange: function(){
        this.setState({
            leagues: filterLeagues(this.refs.country.getDOMNode().value)
        });
    },
    onGetClick: function(e){
        Actions.Admin.fetchMatches.trigger({
            baseUrl: this.refs.baseUrl.getDOMNode().value,
            country: this.refs.country.getDOMNode().value,
            league: this.refs.league.getDOMNode().value,
            startYear: this.refs.season.getDOMNode().value
        });
    },
    render: function () {
        var countries = Seasons.countries.map(function(country){
            return <option key={country.id} value={country.id}>{country.name}</option>
        });
        var leagues = this.state.leagues.map(function(league){
            return <option key={league.level} value={league.fetchId === undefined ? league.level : league.fetchId}>{league.name}</option>
        });
        return (
            <div>
                <div>
                    <label>Base Url</label>
                    <input type="text" ref="baseUrl" defaultValue="mmz4281" />
                </div>
                <div>
                    <label>Country</label>
                    <select ref="country" onChange={this.onCountryChange}>
                        {countries}
                    </select>
                </div>
                <div>
                    <label>League</label>
                    <select ref="league">
                        {leagues}
                    </select>
                </div>
                <div>
                    <label>Season start year</label>
                    <input type="text" ref="season" defaultValue={new Date().getFullYear() - 1} />
                </div>
                <button onClick={this.onGetClick}>Get matches</button>
            </div>
        );
    }
});

module.exports = RemoteAdd;