var React = require('react');

var If = React.createClass({
    render: function(){

        if(!this.props.condition) return null;

        return (
			<div>
				{this.props.children}
			</div>
		);
    }
});

module.exports = If;