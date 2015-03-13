var FOStore = function(options){
	
	// Private properties
	var _callbacks = {
		change: []
	};

	var _init = options.init;


	this.change = function(callback){
		_callbacks.change.push(callback);
	};

	this.addChangeListener = function(changeEvent, callback){
		if(!_callbacks[changeEvent]) _callbacks[changeEvent] = [];
		_callbacks[changeEvent].push(callback);
	};

	this.removeChangeListener = function(changeEvent, callback){
		var callbacks = _callbacks[changeEvent];
		if(!callbacks) return;
		var i = 0;
		for(i; i < callbacks.length; i++){
			if(callbacks[i] === callback) break;
		}
		if(i < callbacks.length){
			callbacks.splice(i, 1);
		}
	};

	this.emit = function(changeEvent){

		var _changeEvent = changeEvent || 'change';
		var callbacks = _callbacks[_changeEvent];

		if(!callbacks) return;

		for(var i = 0; i < callbacks.length; i++){
			callbacks[i].call(this);
		}

	};

	for(var o in options){
		this[o] = options[o];
	}

	if(!!_init) _init.call(this);

};

module.exports = FOStore;