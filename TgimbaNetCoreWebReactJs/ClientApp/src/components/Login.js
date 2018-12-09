import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Login';

class Login extends React.Component {
	constructor(props) {
		super(props);
		this.state = { username: null, password: null };
	}

	render() {
		let { username, password } = this.state;	

		const processLogin = _ => {									 
			this.props.login(username, password);
		}

		const navigateRegistration = _ => {			 
			// TODO - move to utility function
			var host = window.location.protocol + "//"
				+ window.location.hostname + ':' + window.location.port;
			window.location = host + '/register';
		}

		var tableStyle = {					 
			"width": "100%",							 
			"text-align": "center",
			"vertical-align":" middle"
		};

		return (		   
			<div>
				<h1>React JS - Login</h1>
				<table style={tableStyle}>
					<tr>
						<td>
							<label>Username:</label>
							<input
								id="USER_CONTROL_LOGIN_USERNAME"
								type="text"
								value={username}
								onChange={event => this.setState({ username: event.target.value })}
							/>
						</td>
					</tr>
					<tr>
						<td>
							<label>Password:</label>
							<input
								id="USER_CONTROL_LOGIN_PASSWORD"
								type="password"
								value={password}
								onChange={event => this.setState({ password: event.target.value })}
							/> 
						</td>
					</tr>
					<tr>
						<td>
							<button onClick={processLogin} id="hvJsLoginBtn">Login</button>
							<button onClick={navigateRegistration} id="hvJsRegisterPanelBtn">Register</button>
						</td>
					</tr>
				</table>
			</div>
		);
	};
}

export default connect(
	state => state.login,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);