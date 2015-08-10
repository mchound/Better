module.exports = {
    Endpoints: {
        getPrerequisites: 'api/prerequisites',
        getStats: 'api/stats',
        fetchMatches: 'api/fetchMatches/{1}/{2}/{3}/{4}',
        addMatch: 'api/addMatch',
        addMany: 'api/addMany'
    },
    
    StoreStatus: {
        notSet: 'notset',
        idle: 'idle',
        fetching: 'fetching'
    },

    AdminStoreStatus: {
        addingMatch: 'addingmatch',
        addingMatches: 'addingmatches'
    },

    MatchSide: {
        notSet: 0,
        home: 1,
        away: 2
    }
};