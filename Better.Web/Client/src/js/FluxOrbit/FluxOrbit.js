var Action = require('./FOAction');
var Store = require('./FOStore');

module.exports = {

	createAction: function(options){
		var type = Object.prototype.toString.call(options);
		var name;

		switch(type){

			case '[object String]':
				return new Action({name: options});

			case '[object Object]':
				return new Action(options);

			case '[object Array]':
				return options.map(function(o){
					return this.createAction(o);
				}.bind(this));
		}
	},

	createStore: function(options){
		var store = new Store(options);

		if(!!options.actions){
			for(var i = 0; i < options.actions.length; i++){
				var callbackName = 'on' + options.actions[i].name.charAt(0).toUpperCase() + options.actions[i].name.slice(1);
				if(!!store[callbackName]){
					store.listenTo(options.actions[i], store[callbackName]);
				}
			}
		}
		
		return store;
	}
}
