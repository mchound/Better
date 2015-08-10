var smallFlux = require('small-flux'),
    Seasons = require('../Data/Seasons'),
    Actions = require('../Actions/Actions'),
    C = require('../Utilities/Collection');

function filterLeagues(countryId) {
    return Seasons.leagues.filter(function(league){
        return league.countryId === countryId;
    });
}

function getCountryById(id) {
    return C(Seasons.countries).first(function (country) {
        return country.id === id;
    });
}

var _seasons = {
    countries: Seasons.countries,
    leagues: filterLeagues(Seasons.countries[0].id),
    seasons: Seasons.seasons
};

var _selected = {
    country: null,
    leagues: null,
    seasons: null
};

module.exports = smallFlux.createStore({
    name: 'SeasonStore',
    initialize: function () {
        this.observe(Actions.Season.selectCountry, this.onSelectCountry);
        this.observe(Actions.Season.confirm, this.onConfirm);
    },
    onSelectCountry: function (payload) {
        _seasons.leagues = filterLeagues(payload.id);
        this.notify(Actions.Season.selectCountry);
    },
    onConfirm: function(payload){
        _selected.country = payload.country;
        _selected.seasons = payload.seasons;
        _selected.leagues = payload.leagues;
        this.notify(Actions.Season.confirm);
    },
    get: function(){
        return {
            seasons: _seasons,
            selected: _selected
        };
    },
    getSeasons: function () {
        return _seasons;
    },
    getSelected: function () {
        return _selected;
    },
    getCountryById: function (id) {
        return getCountryById(id);
    }
});