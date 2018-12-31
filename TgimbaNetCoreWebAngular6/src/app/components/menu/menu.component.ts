﻿import { Injectable, Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { SessionComponent } from '../common/session.component';	 

@Component({
	selector: 'app-root',
	templateUrl: './menu.component.html',
	styleUrls: ['./menu.component.css']
})

@Injectable()
export class MenuComponent {		
	//private baseUrl: string;	 

	constructor(
		private http: HttpClient,
		private router: Router
	) {								   											   
		//this.baseUrl = window.location.protocol + "//"
		//				+ window.location.hostname + ':' + window.location.port; 
	}

	public AddBucketListItem() {
		alert('AddBucketListItem() clicked');
	};

	public SortBucketListItem() {
		alert('SortBucketListItem() clicked');
	};

	public RunAlgorithm() {
		alert('RunAlgorithm() clicked');
	};

	public LogOut() {
		SessionComponent.SessionClearStorage();
		this.router.navigate(['/login']);
	};

	public Cancel() {
		if (LoginComponent.IsLoggedIn() === true) {
			this.router.navigate(['/main']);
		}
		else {
			this.router.navigate(['/login']);
		}	
	};
}
