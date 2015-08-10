var Actions = require('../Actions/Actions'),
    Constants = require('../Constants/Constants');

function storeUpdateCallback(component, store, singleStore) {
    if (singleStore) {
        return function () {
            component.setState(store.get());
        }
    }
    else {
        return function () {
            var storeState = {};
            storeState[store.name] = store.get();
            component.setState(storeState);
        }
    }
}

module.exports = {
    getInitialState: function () {
        var state = {};
        if (this.stores.length > 1) {
            this.stores.forEach(function (store) {
                state[store.name] = store.get();
            }.bind(this));
            return state;
        }

        return this.stores[0].get();
    },
    componentWillMount: function(){
        this.actions = Actions;
        this.constants = Constants;
    },
    componentDidMount: function () {
        this.stores.forEach(function (store) {
            var callback = storeUpdateCallback(this, store, this.stores.length === 1);
            this['on' + store.name + 'update'] = callback;
            store.attach(callback);
        }.bind(this));
    },
    componentWillUnmount: function () {
        this.stores.forEach(function (store) {
            store.detach(this['on' + store.name + 'update']);
        }.bind(this));
    }
}