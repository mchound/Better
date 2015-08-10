var React = require('react'),
    Season = require('./Components/Season/Season.react'),
    MatchCounter = require('./Components/MatchCounter/MatchCounter.react'),
    TeamSelector = require('./Components/Filters/TeamSelector/TeamSelector.react'),
    FilterList = require('./Components/Filters/FilterList.react'),
    FilterSelector = require('./Components/Filters/FilterSelector.react'),
    Result = require('./Components/Statistics/Result.react'),
    RemoteAdd = require('./Components/Admin/RemoteAdd.react'),
    AdminMatches = require('./Components/Admin/AdminMatches.react'),
    FilterStore = require('./Stores/FilterStore'),
    PrerequisiteStore = require('./Stores/PrerequisiteStore'),
    Actions = require('./Actions/Actions'),
    MatchFilter = require('./Utilities/MatchFilter');

function render(selector, component){
    var node = document.querySelector(selector);
    if(!!node) React.render(component, node);
}

function click(selector, callback){
    var node = document.querySelector(selector);
    if(!!node) node.addEventListener('click', callback);
}

window.onload = function () {

    render('#season', <Season />);
    render('#match-counter', <MatchCounter />);
    render('#team-selector', <TeamSelector />);
    render('#filter-selector', <FilterSelector />);
    render('#filter-list', <FilterList />);
    render('#stat-results', <Result />);
    render('#admin', <RemoteAdd />);
    render('#admin-matches', <AdminMatches />);

    click('#btn-stats', function(){
        Actions.Statistics.request.trigger({
            matchFilter: MatchFilter.create(FilterStore.get()),
            seasonKeys: PrerequisiteStore.get().seasonKeys
        });
    });

}