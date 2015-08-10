var smallFlux = require('small-flux'),
    Actions = require('../Actions/Actions'),
    Constants = require('../Constants/Constants');

var _filters = {
    side: {
        active: false,
        team1: 0,
        team2: 0
    },
    teamNameFilter: {
        active: true,
        team1: {
            teamName: null
        },
        team2: {
            teamName: null
        }
    },
    positionFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    matchesFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    positionFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    winsFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    drawsFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    lossesFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    goalsMadeFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    goalsConcededFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    goalsDiffFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    },
    pointsFilter: {
        active: false,
        team1: {
            min: 0,
            max: 999
        },
        team2: {
            min: 0,
            max: 999
        }
    }
}

var FilterStore = smallFlux.createStore({
    name: 'FilterStore',
    initialize: function () {
        this.observe(Actions.Filters.add, this.onFilterAdd);
        this.observe(Actions.Filters.update, this.onFilterUpdate);
    },
    onFilterAdd: function (payload) {
        _filters[payload.filter].active = !_filters[payload.filter].active;
        this.notify(Actions.Filters.add);
    },
    onFilterUpdate: function (payload) {
        _filters[payload.filter].team1 = payload.team1 !== undefined ? payload.team1 : _filters[payload.filter].team1;
        _filters[payload.filter].team2 = payload.team2 !== undefined ? payload.team2 : _filters[payload.filter].team2;
        this.notify(Actions.Filters.update);
    },
    get: function () {
        return _filters;
    }
})

module.exports = FilterStore;