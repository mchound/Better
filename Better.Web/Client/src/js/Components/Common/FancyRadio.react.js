var React = require('react');

var FancyRadio = React.createClass({
    getInitialState: function () {
        return { options: this.props.options };
    },
    onChange: function(target){
        //this.state.options.forEach(function(option){
        //    if(option.value === target.value) option.selected = true;
        //    else option.selected = false;
        //});
        if(!!this.props.onChange) this.props.onChange(target);
    },
    render: function () {

        var options = this.props.options.map(function(option){
            var className = !!option.selected ? 'selected' : null;
            return <li className={className} key={option.value} onClick={this.onChange.bind(this, option)}>{option.text}</li>
        }.bind(this));

        return (
            <ul>
                {options}
            </ul>  
        );
    }
});

module.exports = FancyRadio;