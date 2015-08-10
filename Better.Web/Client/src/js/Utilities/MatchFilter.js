module.exports = {
    create: function (filters) {
        var filter = {
            team1: {},
            team2: {}
        };

        for (var filterType in filters) {
            if (filters[filterType].active) {
                filter.team1[filterType] = filters[filterType].team1;
                filter.team2[filterType] = filters[filterType].team2;
            }
        }

        return filter;
    }
}