var Action = require('./FOAction');
var Store = require('./FOStore');

module.exports = {

	actions: {},

	stores: {},

	createAction: function(options){
		var type = Object.prototype.toString.call(options);
		var name;

		switch(type){

			case '[object String]':
				this.actions[options] = new Action({name: options});
				return this.actions[options];

			case '[object Object]':
				this.actions[options.name] = new Action(options);
				return this.actions[options.name];

			case '[object Array]':
				return options.map(function(o){
					return this.createAction(o);
				}.bind(this));
		}
	},

	createStore: function(options){
		var store = this.stores[options.name] = new Store(options);

		if(!!options.actions){
			for(var i = 0; i < options.actions.length; i++){
				var callbackName = 'on' + options.actions[i].charAt(0).toUpperCase() + options.actions[i].slice(1);
				if(!!store[callbackName]){
					this.actions[options.actions[i]].listen(store[callbackName].bind(store));
				}
			}
		}
		
		return store;
	}
}

//window.FluxOrbit = require('./FluxOrbit');
//
//var actions = FluxOrbit.createAction(['myAction', 'myAction2']);
//
//var myStore = FluxOrbit.createStore({
//
//	name: 'MyTestStore',
//
//	actions: ['myAction', 'myAction2'],
//
//	init: function(){
//		
//		FluxOrbit.actions.myAction.listen(function(payload){
//			this.val = payload;
//			this.emit();
//		}.bind(this));
//
//	},
//
//	onMyAction2: function(payload){
//		console.log(payload);
//		this.emit();
//	},
//
//	val: null
//
//});
//
//myStore.change(function(){
//	console.log(this.val);
//});
//
//actions[1].trigger('This payload will be passed to the store and set to store.val');
