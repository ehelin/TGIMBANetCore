﻿import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/userInterface/BucketListItem'; // TODO -> add
import Button from './Button';
var utilsRef = require('../../common/Utilities');
   
class BucketListItem extends React.Component {
	parentFormAddEdit;
	state;

	constructor(props) {
		super(props);
		this.parentFormAddEdit = props.parent;
		this.state = {
			name: props.name ? props.name : '',
			dateCreated: props.dateCreated ? props.dateCreated : new Date().toLocaleDateString('en-US'),
			bucketListItemType: this.setCategory(props.bucketListItemType),
			completed: props.completed
				&& (props.completed === 'true' || props.completed === true)
					? 'on' : null,		   \
			checked: props.completed
				&& (props.completed === 'true' || props.completed === true)
				? true : false,
			latitude: props.latitude ? props.latitude : '',
			longitude: props.longitude ? props.longitude : '',
			databaseId: props.databaseId ? props.databaseId : '',
			userName: props.userName ? props.userName : ''
		};
	}

	setCategory(bucketListItemType) {
		if (bucketListItemType && bucketListItemType === '3') {
			return 'Hot';
		} else if (bucketListItemType && bucketListItemType === '2') {
			return 'Warm';
		} else {
			return 'Cool';
		}
	}

	formSubmit(name, dateCreated, bucketListItemType, completed, latitude, longitude, databaseId, userName) {
		this.parentFormAddEdit.formSubmit(name, dateCreated, bucketListItemType, completed, latitude, longitude, databaseId, userName);
	}

	formCancel() {
		this.parentFormAddEdit.formCancel();
	}

	render() {						
		let { name, dateCreated, bucketListItemType, completed, latitude,
			longitude, databaseId, userName, onPress, onChange, onCancel, checked } = this.state;
	
		var utils = Object.create(utilsRef.Utilities);
		var tableStyle = utils.GetDefaultTableStyle();		 

		return (
			<div>
				<h1>React JS - Add Edit</h1>
				<input type="hidden" id="USER_CONTROL_EDIT_DBID" value={databaseId} />
				<input type="hidden" id="USER_CONTROL_EDIT_USERNAME" value={userName} />
				<table style={tableStyle}>
					<tr>
						<td>
							<label>Item Name:</label>
							<input
								id="USER_CONTROL_ADD_ITEM_NAME"
								type="text"
								value={name}
								onChange={event => this.setState({ name: event.target.value })}
							/>
						</td>
					</tr>
					<tr>
						<td>
							<label>Date Created:</label>
							<input
								id="USER_CONTROL_ADD_DATE_CREATED"
								type="text"
								value={dateCreated}
								//onChange={event => this.setState({ dateCreated: event.target.value })}
								readonly
							/>
						</td>
					</tr>
					<tr>
						<td>
							<label>Category:</label>
							<select id="USER_CONTROL_ADD_ITEM_CATEGORY"
								value={bucketListItemType}
								onChange={event => this.setState({ bucketListItemType: event.target.value })}							>
								<option value="Hot">Hot</option>
								<option value="Warm">Warm</option>
								<option value="Cool">Cool</option>
							</select>
						</td>
					</tr>
					<tr>
						<td>
							<label>Completed:</label>
							<input type="checkbox"
								id="USER_CONTROL_ADD_COMPLETED"		  
								onChange={event => this.setState(
									{
										completed: !completed,
										checked: !checked,
									})}
								value={completed}
								checked={checked}
							/>
							
						</td>
					</tr>
					<tr>
						<td>
							<label>Latitude:</label>
							<input
								id="USER_CONTROL_ADD_LATITUDE"
								type="text"
								value={latitude}
								onChange={event => this.setState({ latitude: event.target.value })}
							/>
						</td>
					</tr>
					<tr>
						<td>
							<label>Longitude:</label>
							<input
								id="USER_CONTROL_ADD_LONGITUDE"
								type="text"
								value={longitude}
								onChange={event => this.setState({ longitude: event.target.value })}
							/>
						</td>
					</tr>
					<tr>
						<td>
							<Button onPress={() => this.formSubmit(
								name,
								dateCreated,
								bucketListItemType,
								completed,
								latitude,
								longitude,
								databaseId,
								userName,
							)} id="hvJsAddSubmitBtn">{this.props.databaseId ? 'Edit' : 'Add'}</Button>
							<Button onPress={() => this.formCancel()} id="hvJsAddCancellBtn">Cancel</Button>
						</td>
					</tr>
				</table>
			</div>
		);				   
	};
}

export default connect(
	state => state.bucketListItem,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(BucketListItem)

