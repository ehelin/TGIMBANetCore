﻿var utilsRef = require('../common/Utilities');
var constantsRef = require('../common/Constants');
var sessionRef = require('../common/Session');

const ACTION_TYPE_MAIN_MENU = 'MainMenu';
const ACTION_TYPE_LOAD = 'Load';
						 
//const initialState = {
//	bucketListItems: [
//		{	
//			name: null,
//			dateCreated: null,
//			bucketListItemType: null,
//			completed: null,
//			latitude: null,
//			longitude: null
//		}
//	]
//};
const initialState = {
	bucketListItems: [
		{
			name: 'test',
			dateCreated: 'date',
			bucketListItemType: 'category',
			completed: 'true',
			latitude: '1',
			longitude: '2'
		},
		{
			name: 'test 2',
			dateCreated: 'datec3',
			bucketListItemType: 'category 3',
			completed: 'true',
			latitude: '12',
			longitude: '22'
		}
	]
};

export const actionCreators = {
	main: () => async (dispatch, getState) => {
		dispatch({ type: ACTION_TYPE_MAIN_MENU });
	}, 	
	load: () => async (dispatch, getState) => {
		dispatch({ type: ACTION_TYPE_LOAD });
	}
};

export const reducer = (state, action) => {
	state = state || initialState;

	var utils = Object.create(utilsRef.Utilities);
	var host = utils.GetHost();

	if (action.type == ACTION_TYPE_MAIN_MENU) {	  
		window.location = host + '/mainmenu';
	} else if (action.type === ACTION_TYPE_LOAD) {
		var constants = Object.create(constantsRef.Constants);
		var session = Object.create(sessionRef.Session); 

		var token = session.SessionGet(constants.SESSION_TOKEN);
		var userName = session.SessionGet(constants.SESSION_USERNAME);
		// assume all calls are to load bucket list 

		const url = host + '/BucketListItem/GetBucketListItems'
				+ '?encodedUserName=' + btoa(userName)	   
				+ '&encoderedSortString=' + btoa('')
				+ '&encodedToken=' + btoa(token);
		const xhr = new XMLHttpRequest();
		xhr.open('get', url, true);
		xhr.onload = (data) => {
			if (data && data.currentTarget
				&& data.currentTarget && data.currentTarget.response
				&& data.currentTarget.response.length > 0) {
				alert('data: ' + data.currentTarget.response); 
			}
		};
		xhr.send();
	}

	return state;
};