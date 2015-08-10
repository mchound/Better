var React = require('react'),
    FluxMixin = require('../../Mixins/Flux.mixin'),
    AdminStore = require('../../Stores/AdminStore'),
    AdminMatch = require('./AdminMatch.react.js');

function nonexistingFirst(a, b){
    if(a.exists && !b.exists) return 1;
    else if(!a.exists && b.exists) return -1;
    else{
        var d1 = new Date(a.date);
        var d2 = new Date(b.date);
        if(d2 > d1) return 1;
        else if(d1 > d2) return -1;
        return 0;
    }
}

var AdminMatches = React.createClass({
    mixins: [FluxMixin],
    stores: [AdminStore],
    onAddAll: function(){
        var payload = this.state.matches.filter(function(match){
            return match.exists === false;
        });
        this.actions.Admin.addMany.trigger(payload);
    },
    render: function () {
        if(this.state.status === this.constants.StoreStatus.notSet) {
            return <h1>No matches fetched</h1>
        }
        else if (this.state.status === this.constants.StoreStatus.fetching) {
            return <h1>Fetching remote matches</h1>
        }
        else if(this.state.status === this.constants.AdminStoreStatus.addingMatches){
            return <h1>Adding matches</h1>
        } 
        else if(this.state.matches === null){
            return <h1>An error occured.</h1>
        }
        else
        {
            var matches = this.state.matches.sort(nonexistingFirst).map(function(match){
                return <AdminMatch key={match.id} match={match} />
                });
            return (
                <div>
                    <button onClick={this.onAddAll}>Add all</button>
                    <table data-am-adminmatches><thead><tr><th>Date</th><th>Home</th><th>Away</th><th></th><th></th></tr></thead>
                        <tbody>
                            {matches}
                        </tbody>
                    </table>
                </div>
            );
        }
    }
});

module.exports = AdminMatches;