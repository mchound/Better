var React = require('react');

module.exports = React.createClass({
	
	getInitialState: function(){
		return this._getStateFromStores();
	},

	componentDidMount: function(){
		
		if(!this.props.store) return;
		
		this.props.store.addChangeListener(this.props.storeChangeEvent, function(){
			this.setState(this._getStateFromStores());
		}.bind(this));
	},

	componentDidUnMount: function(){
	
	},

	render: function(){

		if(!this._isVisible()){
			return null;
		}

		return (
			<div>
				<label>{this.props.label}</label><br/>
				<select onChange={this._onChange} ref="select">
					{this._getDefaultOption()}
					{this._getOptions()}
				</select>
			</div>
		);

	},

	_getStateFromStores: function(){
		return {
			selected: null,
			options: this.props.getOptions()
		}
	},

	_getDefaultOption: function(){
		if(!this.props.defaultText) return null;
		return (<option value={this.props.defaultValue}>{this.props.defaultText}</option>)
	},

	_getOptions: function(){
		return this.state.options.map(function(option){
			return (<option value={option.value} key={option.value}>{option.text}</option>);
		});
	},

	_onChange: function(e){
		if(!this.props.changeAction) return;
		this.props.changeAction.trigger(this.refs.select.getDOMNode().value);
	},

	_isVisible: function(){
		return !this.props.hideIfEmpty || this.state.options.length > 0;
	}
});