var smallFlux = require('small-flux'),
    Actions = require('../Actions/Actions'),
    Constants = require('../Constants/Constants'),
    Ajax = require('../Utilities/Ajax');

var _stats = null,
    _status = Constants.StoreStatus.notSet;

var StatisticsStore = smallFlux.createStore({
    name: 'StatisticsStore',
    initialize: function () {
        this.observe(Actions.Statistics.request, this.onRequest);
    },
    onRequest: function (payload) {
        _status = Constants.StoreStatus.fetching;
        this.notify(Actions.Statistics.request);
        Ajax.get(
            Constants.Endpoints.getStats,
            function (stats) {
                _stats = stats;
                _status = Constants.StoreStatus.idle;
                this.notify(Actions.Statistics.request);
            }.bind(this),
            function (xhr) {
                status = Constants.StoreStatus.idle;
                _stats = null;
                console.log(xhr);
                this.notify(Actions.Statistics.request);
            }.bind(this),
            {matchFilter: payload.matchFilter, seasonKeys: payload.seasonKeys},
            true
        );
    },
    get: function () {
        return {
            stats: _stats,
            status: _status
        };
    }
});

module.exports = StatisticsStore;