var React = require('react'),
	If = require('../Common/If.react.js');

var NumberSpan = React.createClass({
    getInitialState: function(){
        return {
            lower: this.props.lower !== null && this.props.lower !== undefined ? this.props.lower : null,
            upper: this.props.upper !== null && this.props.upper !== undefined ? this.props.upper : null
        };
    },
    onBtnLowerMinusClick: function(){
        var current = this.state.lower;
        var min = this.props.min === undefined ? 0 : this.props.min;
        var next = Math.max(min, current - 1);
        this.setState({lower: next});
        this.props.onChange({upper: this.state.upper, lower: next});
    },
    onBtnLowerPlusClick: function(){
        var current = this.state.lower;
        var max = this.props.max === undefined ? 999 : this.props.max;
        var nextLower = Math.min(max, current + 1);
        var nextUpper = nextLower > this.state.upper ? nextLower : this.state.upper;
        this.setState({lower: nextLower, upper: nextUpper});
        this.props.onChange({upper: nextUpper, lower: nextLower});
    },
    onBtnUpperMinusClick: function(){
        var current = this.state.upper;
        var min = this.props.min === undefined ? 0 : this.props.min;
        var nextUpper = Math.max(min, current - 1);
        var nextLower = nextUpper < this.state.lower ? nextUpper : this.state.lower;
        this.setState({lower: nextLower, upper: nextUpper});
        this.props.onChange({upper: nextUpper, lower: nextLower});
    },
    onBtnUpperPlusClick: function(){
        var current = this.state.upper;
        var max = this.props.max === undefined ? 999 : this.props.max;
        var next = Math.min(max, current + 1);
        this.setState({upper: next});
        this.props.onChange({upper: next, lower: this.state.lower});
    },
    onLowerChange: function(e){
        var nextLower = e.target.value === '' ? null : e.target.value;
        nextLower = parseInt(e.target.value);
        nextLower = isNaN(nextLower) ? '' : nextLower;
        this.setState({lower: nextLower});
    },
    onUpperChange: function(e){
        var nextUpper = e.target.value === '' ? null : e.target.value;
        nextUpper = parseInt(e.target.value);
        nextUpper = isNaN(nextUpper) ? '' : nextUpper;
        this.setState({upper: nextUpper});
    },
    onLowerBlur: function(){
        var nextUpper = this.state.lower > this.state.upper ? this.state.lower : this.state.upper;
        this.setState({upper: nextUpper});
        this.props.onChange({upper: nextUpper, lower: this.state.lower});
    },
    onUpperBlur: function(){
        var nextLower = this.state.upper < this.state.lower ? this.state.upper : this.state.lower;
        this.setState({lower: nextLower});
        this.props.onChange({upper: this.state.upper, lower: nextLower});
    },
    render: function(){

        return (
			<div data-am-numberspan className="clearfix">
				<div className="adjust-buttons buttons-min">
					<button onClick={this.onBtnLowerMinusClick}>-</button>
					<button onClick={this.onBtnLowerPlusClick}>+</button>
				</div>
				<input className="min" type="text" value={this.state.lower} onChange={this.onLowerChange} onBlur={this.onLowerblur} />
				<input className="separator" type="text" disabled={true} value="-" />
				<input className="max" type="text" value={this.state.upper} onChange={this.onUpperChange} onBlur={this.onUpperBlur} />
				<div className="adjust-buttons buttons-max">
					<button onClick={this.onBtnUpperMinusClick}>-</button>
					<button onClick={this.onBtnUpperPlusClick}>+</button>
				</div>
			</div>
		);

    }
});

module.exports = NumberSpan;