module.exports = {
    create: function (country, level, startYear) {
        return {
            country: country,
            level: level,
            startYear: startYear
        };
    },

    createMany: function (country, levels, startYears) {
        var keys = [];
        for (var i = 0; i < levels.length; i++) {
            for (var q = 0; q < startYears.length; q++) {
                keys.push(this.create(country, levels[i], startYears[q]));
            }
        }
        return keys;
    }
}