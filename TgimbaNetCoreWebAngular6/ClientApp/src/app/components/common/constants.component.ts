﻿import { Injectable, Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';					 

@Component({
	selector: 'app-root',
	templateUrl: './emptyHtml/constants.component.html',
	//styleUrls: ['./constants.component.css']
})

@Injectable()
export class ConstantsComponent {				   
	public static get SESSION_TOKEN(): string { return "SessionToken"; }
	public static get SESSION_USERNAME(): string { return "SessionUserName"; }
	public static get SESSION_SORT_ALGORITHMS(): string { return "SessionAvailableSortAlgorithms"; }
	public static get SESSION_SEARCH_ALGORITHMS(): string { return "SessionAvailableSearchAlgorithms"; }				 
}
