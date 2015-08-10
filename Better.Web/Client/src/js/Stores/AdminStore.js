var smallFlux = require('small-flux'),
    Actions = require('../Actions/Actions'),
    Constants = require('../Constants/Constants'),
    Ajax = require('../Utilities/Ajax'),
    String = require('../Utilities/String'),
    C = require('../Utilities/Collection');

function toggleExists(id){
    var match = C(_matches).first(function(match){
        return match.id === id;
    });
    match.exists = !match.exists;
}

var _matches = null,
    _status = Constants.StoreStatus.notSet,
    _country = null,
    _league = null,
    _startYear = null;

var PrerequisiteStore = smallFlux.createStore({
    name: 'AdminStore',
    initialize: function () {
        this.observe(Actions.Admin.fetchMatches, this.onFetchMatches);
        this.observe(Actions.Admin.addMatch, this.onAddMatch);
        this.observe(Actions.Admin.addMany, this.onAddMany);
    },
    onFetchMatches: function (payload) {
        _country = payload.country,
        _league = payload.league,
        _startYear = payload.startYear;
        _status = Constants.StoreStatus.fetching;
        this.notify(Actions.Admin.fetchMatches);
        Ajax.get(
            String.format(Constants.Endpoints.fetchMatches, payload.baseUrl, _country, _league, _startYear),
            function (matches) {
                _matches = matches;
                _status = Constants.StoreStatus.idle;
                this.notify(Actions.Admin.fetchMatches);
            }.bind(this),
            function (xhr) {
                _status = Constants.StoreStatus.idle;
                _matches = null;
                console.log(xhr);
                this.notify(Actions.Admin.fetchMatches);
            }.bind(this),
            null,
            false
        );
    },
    onAddMatch: function (payload) {
        _status = Constants.AdminStoreStatus.addingMatch;
        this.notify(Actions.Admin.addMatch);
        Ajax.post(
            Constants.Endpoints.addMatch,
            function (match) {
                _status = Constants.StoreStatus.idle;
                toggleExists(match.Id);
                this.notify(Actions.Admin.fetchMatches);
            }.bind(this),
            function (xhr) {
                _status = Constants.StoreStatus.idle;
                console.log(xhr);
                this.notify(Actions.Admin.addMatch);
            }.bind(this),
            payload,
            false
        );
    },
    onAddMany: function (payload) {
        _status = Constants.AdminStoreStatus.addingMatch;
        this.notify(Actions.Admin.addMany);
        Ajax.post(
            Constants.Endpoints.addMany,
            function (matches) {
                matches.forEach(function (match) {
                    toggleExists(match.Id);
                });
                this.notify(Actions.Admin.fetchMatches);
            }.bind(this),
            function (xhr) {
                _status = Constants.StoreStatus.idle;
                console.log(xhr);
                this.notify(Actions.Admin.addMany);
            }.bind(this),
            payload,
            false
        );
    },
    get: function () {
        return {
            matches: _matches,
            status: _status
        };
    }
});

module.exports = PrerequisiteStore;