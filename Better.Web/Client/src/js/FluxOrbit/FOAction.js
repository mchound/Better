var FOAction = function(options){
	
	// Private properties
	var _id = 0;
	var _callbacks = [];
	var _async = options.async;

	// Private methods
	var _executeAsync = function(payload, successCallback, errorCallback){

		_async(
			
			// payload
			payload,

			// Success callback
			successCallback,

			// Error callback
			errorCallback
		);

	};

	var _invokeListeners = function(payload){
		for(var i = 0; i < _callbacks.length; i++){
			_callbacks[i].call(_callbacks[i], payload);
		}
	};

	// Public properties
	this.name = options.name;

	// Public methods

	this.listen = function(callback){
		_callbacks.push(callback)
	};

	this.trigger = function(payload){

		if(!_async){
			_invokeListeners(payload);
			return;	
		} 

		_executeAsync(

			// Payload
			payload, 

			// Success callback
			function(response){
				_invokeListeners(response);
			},

			// Error callback
			function(error){
				throw {thrower: 'FOAction: ' + _name, message: 'Async failed', error: error};
			}
		);
		
	}

};

module.exports = FOAction;