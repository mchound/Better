var smallFlux = require('small-flux'),
    Actions = require('../Actions/Actions'),
    Constants = require('../Constants/Constants'),
    Ajax = require('../Utilities/Ajax');

var _teams = null,
    _matchCount = null,
    _status = Constants.StoreStatus.notSet,
    _seasonKeys = null;

var PrerequisiteStore = smallFlux.createStore({
    name: 'PrerequisiteStore',
    initialize: function () {
        this.observe(Actions.Season.confirm, this.onSeasonConfirm);
    },
    onSeasonConfirm: function (payload) {
        _seasonKeys = payload.seasonKeys;
        _status = Constants.StoreStatus.fetching;
        this.notify(Actions.Season.confirm);
        Ajax.get(
            Constants.Endpoints.getPrerequisites,
            function (prerequisites) {
                _teams = prerequisites.teams;
                _matchCount = prerequisites.matchCount;
                _status = Constants.StoreStatus.idle;
                this.notify(Actions.Season.confirm);
            }.bind(this),
            function (xhr) {
                status = Constants.StoreStatus.idle;
                _teams = null;
                console.log(xhr);
                this.notify(Actions.Season.confirm);
            }.bind(this),
            payload.seasonKeys,
            true
        );
    },
    get: function () {
        return {
            teams: _teams,
            matchCount: _matchCount,
            status: _status,
            seasonKeys: _seasonKeys
        };
    }
});

module.exports = PrerequisiteStore;