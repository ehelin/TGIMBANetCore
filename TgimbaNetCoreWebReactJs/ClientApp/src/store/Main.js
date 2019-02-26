﻿var utilsRef = require('../common/Utilities');
var constantsRef = require('../common/Constants');
var sessionRef = require('../common/Session');

const ACTION_TYPE_MAIN_MENU = 'MainMenu';
const ACTION_TYPE_LOAD = 'Load';
const ACTION_TYPE_DELETE = 'Delete';
const ACTION_TYPE_EDIT = 'Edit';
						 
const initialState = {
	bucketListItems: null
};

export const actionCreators = {
	main: () => async (dispatch, getState) => {		 
		dispatch({ type: ACTION_TYPE_MAIN_MENU });
	},
	delete: (id) => async (dispatch, getState) => {
		// TODO - call redux handler, delete selected item and redisplay main form
		alert('main redux formDelete: ' + id);
		dispatch({ type: ACTION_TYPE_DELETE });
	}, 	
	edit: (name, dateCreated, bucketListItemType, completed,
		latitude, longitude, databaseId, userName) => async (dispatch, getState) =>
		{
		// TODO - call redux handler, delete selected item and redisplay main form
		//alert('main redux formDelete: ' + id);
		dispatch({
			type: ACTION_TYPE_EDIT, name, dateCreated,
			bucketListItemType, completed, latitude, longitude, databaseId, userName
		});
	}, 	
	load: () => async (dispatch, getState) => {
		var constants = Object.create(constantsRef.Constants);
		var session = Object.create(sessionRef.Session);

		var token = session.SessionGet(constants.SESSION_TOKEN);
		var userName = session.SessionGet(constants.SESSION_USERNAME);	   

		const url = 'BucketListItem/GetBucketListItems'
			+ '?encodedUserName=' + btoa(userName)
			+ '&encoderedSortString=' + btoa('')
			+ '&encodedToken=' + btoa(token);
		const response = await fetch(url);
		const bucketListItems = await response.json();

		for (let i = 0; i < bucketListItems.length; i++) {
			bucketListItems[i].number = i + 1;
		}

		dispatch({ type: ACTION_TYPE_LOAD, bucketListItems });
	}
};

export const reducer = (state, action) => {
	state = state || initialState;		

	var utils = Object.create(utilsRef.Utilities);
	var host = utils.GetHost();

	if (action.type === ACTION_TYPE_LOAD) {
		return {
			...state,
			bucketListItems: action.bucketListItems
		};
	}
	else if (action.type == ACTION_TYPE_MAIN_MENU) {
		window.location = host + '/mainmenu';
	}
	else if (action.type === ACTION_TYPE_EDIT) {
		var queryString = '?name=' + action.name
			+ '&dateCreated=' + action.dateCreated
			+ '&bucketListItemType=' + action.bucketListItemType
			+ '&completed=' + action.completed
			+ '&latitude=' + action.latitude
			+ '&longitude=' + action.longitude
			+ '&databaseId=' + action.databaseId
			+ '&userName=' + action.userName;
					   
		window.location = host + '/edit' + queryString;
	}											  

	return state;
};