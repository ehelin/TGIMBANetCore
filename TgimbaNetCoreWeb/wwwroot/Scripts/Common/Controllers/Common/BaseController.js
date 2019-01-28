﻿var BaseController = {};

BaseController.Token = 'notSet';

BaseController.SetParameters = function(parameterNames, file) {
    var paramValues = [];

	for(var i=0; i<parameterNames.length; i++) {
		var value = BaseController.GetParameter("USER_CONTROL_" + parameterNames[i], 
												"VALUE_TYPE_" + parameterNames[i], 
												file);	
		paramValues.push(value);		
	}

	return paramValues;
}

BaseController.GetParameter = function (ctrl, type, file) {
	var value = null;

	if (!HasValue(ctrl, type, file))
    {
        return;
    }       

	ctrlObj = document.getElementById(ctrl);
	if (ctrlObj.type === 'checkbox') {
		value = ctrlObj.checked;
	} else if (ctrlObj.type === 'select-one') {
		value = ctrlObj.options[ctrlObj.selectedIndex].value;
	} else {
		value = ctrlObj.value;
	} 

	return value;
}