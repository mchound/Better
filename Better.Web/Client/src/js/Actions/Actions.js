var SmallFlux = require('small-flux');

module.exports = {
    Season: SmallFlux.createActions(['confirm', 'selectCountry']),
    TeamFilter: SmallFlux.createActions(['select']),
    Filters: SmallFlux.createActions(['add', 'update']),
    Statistics: SmallFlux.createActions(['request']),
    Admin: SmallFlux.createActions(['fetchMatches', 'addMatch', 'addMany'])
};