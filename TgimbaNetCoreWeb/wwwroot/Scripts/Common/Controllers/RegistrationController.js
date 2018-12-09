﻿var RegistrationController = Object.create(BaseController);

RegistrationController.ParameterNames = [
	"REGISTRATION_USERNAME",  
	"REGISTRATION_EMAIL", 
	"REGISTRATION_PASSWORD", 
	"REGISTRATION_CONFIRM_PASSWORD"
];

RegistrationController.Index = function() {
	Display.LoadView(VIEW_REGISTRATION);
}

RegistrationController.Cancel = function() {
	Display.LoadView(VIEW_LOGIN);
};

RegistrationController.Register = function() {
	var params = BaseController.SetParameters(
									RegistrationController.ParameterNames,
									"RegistrationController.js"
								);

	if (IsJQueryClient()) {				 
	    JQueryRegistration(params);
	} else {
		ServerCalls.ProcessRegistration(REGISTRATION_PROCESS_REGISTRATION, params);
	}   
};