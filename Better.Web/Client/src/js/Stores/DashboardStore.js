var FluxOrbit = require('../FluxOrbit/FluxOrbit');

var _availableLeagues = [];

FluxOrbit.createStore({
    
    name: 'dashboardStore',

    actions: ['countrySelect'],

    onCountrySelect: function(country){
        _availableLeagues = _leagues[country] || [];
        this.emit('countryChange');
    },

    getCountries: function () {
        return _countries;
    },

    getLeagues: function () {
        return _availableLeagues;
    }

});

var _countries = [
    { text: 'England', value: 'en' },
    { text: 'Scotland', value: 'sc' },
    { text: 'Germany', value: 'ge' },
    { text: 'Italy', value: 'it' },
    { text: 'Spain', value: 'sp' },
    { text: 'France', value: 'fr' },
    { text: 'ne', value: 'Netherlands' },
    { text: 'Belgium', value: 'be' },
    { text: 'Portugal', value: 'po' },
    { text: 'Turkey', value: 'tu' },
    { text: 'Greece', value: 'gr' }
];

var _leagues = {
    en: [
        { text: 'Premier League', value: 'E0' },
        { text: 'Championship', value: 'E1' },
        { text: 'League One', value: 'E2' },
        { text: 'League Two', value: 'E3' }
    ],
    sc: [
        { text: 'Premier League', value: 'SC0' },
        { text: 'Division 1', value: 'SC1' },
        { text: 'Division 2', value: 'SC2' },
        { text: 'Division 3', value: 'SC3' }
    ],
    ge: [
        { text: 'Bundesliga 1', value: 'D1' },
        { text: 'Bundesliga 2', value: 'D2' }
    ],
    it: [
        { text: 'Serie A', value: 'I1' },
        { text: 'Serie B', value: 'I2' }
    ],
    sp: [
        { text: 'La Liga Primera Division', value: 'SP1' },
        { text: 'La Liga Segunda Division', value: 'SP2' }
    ],
    fr: [
        { text: 'Le Championnat', value: 'F1' },
        { text: 'Division 2', value: 'F2' }
    ],

    ne: [
        { text: 'Eredivisie', value: 'N1' }
    ],
    be: [
        { text: 'Jupiler League', value: 'B1' }
    ],
    po: [
        { text: 'Liga 1', value: 'P1' }
    ],
    tu: [
        { text: 'Futbol Ligi 1', value: 'T1' }
    ],
    gr: [
        { text: 'Ethniki Katigoria', value: 'G1' }
    ]
}