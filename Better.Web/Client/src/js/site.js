var React = require('react');
var Dashboard = require('./Components/Sections/Dashboard.react.jsx');

window.addEventListener('load', function () {
    React.render(<Dashboard />, document.querySelector('[data-am-section="dashboard"]'));
});