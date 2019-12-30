﻿var MainController = Object.create(BaseController);

MainController.Add_ParameterNames = [
	"ADD_ITEM_NAME",
	"ADD_DATE_CREATED",
	"ADD_ITEM_CATEGORY",
	"ADD_COMPLETED",
	"ADD_LATITUDE",
	"ADD_LONGITUDE",
];

MainController.Edit_ParameterNames = [
	"EDIT_ITEM_NAME",
	"EDIT_DATE_CREATED",
	"EDIT_ITEM_CATEGORY",
	"EDIT_COMPLETED",
	"EDIT_LATITUDE",
	"EDIT_LONGITUDE",
	"EDIT_DBID",
	"EDIT_USERNAME"
];
	 
MainController.Index = function (srchTerm) {
	var params = [];
	
	params.push(SessionGetUsername(SESSION_USERNAME));
	params.push(SessionGetToken(SESSION_TOKEN));

	if (srchTerm && srchTerm.length > 0) {
		params.push(srchTerm);
		SessionSetIsSearch(SESSION_IS_SRCH_VIEW, true);
	}
	else {
		SessionSetIsSearch(SESSION_IS_SRCH_VIEW, undefined);
	}

	HtmlVanillaJsServerCalls.GetBucketListItems(BUCKET_LIST_PROCESS_GET, params);
};

MainController.ShowMenu = function () {
	MenuController.Index();
};

MainController.SetAddViewDate = function () {
	var addViewDateInput = document.getElementById('USER_CONTROL_ADD_DATE_CREATED');
	var today = new Date();
	addViewDateInput.value = today.toLocaleDateString('en-US');
};

MainController.Search = function ()
{
	var srchTerm = GetElementValue('USER_CONTROL_SEARCH_TEXT_BOX');	// Utilities.js

	if (srchTerm) {
		return MainController.Index(srchTerm);
	}

	alert('No search term entered');
}

MainController.FormEditClick = function (itemName, dateCreated, bucketListItemType,
										completed, latitude, longitude,
										dbId, userName) {	 
	var editFormValues = [];

	editFormValues.push(itemName);
	editFormValues.push(dateCreated);
	editFormValues.push(bucketListItemType);
	editFormValues.push(completed);
	editFormValues.push(latitude);
	editFormValues.push(longitude);
	editFormValues.push(dbId);
	editFormValues.push(userName);	

	var contentDiv = Display.GetContentDiv();  

	ServerCalls.GetView(VIEW_MAIN_EDIT, contentDiv, editFormValues)
};

MainController.FormDeleteClick = function (dbId) {							   
	HtmlVanillaJsServerCalls.DeleteBucketListItem(BUCKET_LIST_PROCESS_DELETE, dbId);
};

MainController.Edit = function () {
	var params = BaseController.SetParameters(
		MainController.Edit_ParameterNames,
		"MainController.js"
	);

	HtmlVanillaJsServerCalls.EditBucketListItem(BUCKET_LIST_PROCESS_EDIT, params);
};

MainController.Add = function () {
	var params = BaseController.SetParameters(
		MainController.Add_ParameterNames,
		"MainController.js"
	);

	HtmlVanillaJsServerCalls.AddBucketListItem(BUCKET_LIST_PROCESS_ADD, params);
};