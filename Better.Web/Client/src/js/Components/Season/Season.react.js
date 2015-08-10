var React = require('react'),
    Actions = require('../../Actions/Actions'),
    SeasonStore = require('../../Stores/SeasonStore'),
    HtmlSelectHelper = require('../../Utilities/HtmlSelectHelper'),
    SeasonKey = require('../../Utilities/SeasonKey');

function confirmIsEnabled(){
    return  !!this.refs.league &&
            this.refs.league.getDOMNode().value !== '' &&
            !!this.refs.season &&
            this.refs.season.getDOMNode().value !== '';
}

var Season = React.createClass({
    getInitialState: function(){
        var state = SeasonStore.getSeasons();
        state.canConfirm = false;
        return state;
    },
    componentDidMount: function(){
        SeasonStore.attach(this.onSeasonsUpdate);
    },
    componentWillUnmount: function(){
        SeasonStore.detach(this.onSeasonsUpdate);
    },
    onSeasonsUpdate: function(action){
        if(action === Actions.Season.selectCountry){
            this.setState(SeasonStore.getSeasons());
        }
    },
    onCountryChange: function(e){
        this.setState({canConfirm: false});
        Actions.Season.selectCountry.trigger({id: e.target.value});
    },
    onChange: function(){
        this.setState({canConfirm: confirmIsEnabled.call(this)});
    },
    onConfirmClick: function(){
        Actions.Season.confirm.trigger({
            seasonKeys: SeasonKey.createMany(
                SeasonStore.getCountryById(this.refs.country.getDOMNode().value).name,
                HtmlSelectHelper.values(this.refs.league.getDOMNode()),
                HtmlSelectHelper.values(this.refs.season.getDOMNode())
            )
        });
    },
    render: function () {
        var countries = this.state.countries.map(function(country){
            return <option value={country.id} key={country.id}>{country.name}</option>
        });
        var leagues = this.state.leagues.map(function(league){
            return <option value={league.level} key={league.countryId + league.level}>{league.name}</option>
        });
        var seasons = this.state.seasons.map(function(season){
            return <option value={season.startYear} key={season.startYear}>{season.name}</option>
        });
        return (
            <div>
                <select ref="country" onChange={this.onCountryChange}>{countries}</select>
                <select ref="league" onChange={this.onChange} multiple>{leagues}</select>
                <select ref="season" onChange={this.onChange} multiple>{seasons}</select>
                <button onClick={this.onConfirmClick} disabled={!this.state.canConfirm}>Confirm</button>
            </div>
        );
    }
});

module.exports = Season;