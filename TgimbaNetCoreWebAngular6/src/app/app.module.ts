import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ModuleWithProviders  } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';	  
import { HttpClientModule } from '@angular/common/http'	  
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { MainComponent } from './components/main/main.component';  

const routes: Routes = [
	{ path: 'login',  component: LoginComponent },
	{ path: 'registration', component: RegistrationComponent },
	{ path: 'main', component: MainComponent }
];

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		RegistrationComponent,
		MainComponent
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpClientModule, 
		RouterModule.forRoot(routes),
	],		
	providers: [],
	bootstrap: [AppComponent],
	exports: [RouterModule]
})		   
export class AppModule {}
