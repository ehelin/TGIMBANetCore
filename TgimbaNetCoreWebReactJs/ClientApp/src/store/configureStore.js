﻿import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as Counter from './Counter';
import * as Login from './Login';
import * as Registration from './Registration';
import * as Main from './Main';
import * as WeatherForecasts from './WeatherForecasts';	
import * as Button from './Button';

export default function configureStore(history, initialState) {
  const reducers = {
	  login: Login.reducer,
	  counter: Counter.reducer,
	  weatherForecasts: WeatherForecasts.reducer,
	  register: Registration.reducer,
	  main: Main.reducer,
	  button: Button.reducer
  };

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
