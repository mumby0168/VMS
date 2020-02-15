
import React from 'react'
import './App.css';
import Setup from './hocs/Setup';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import { IAppState } from './redux/store';
import { useSelector } from 'react-redux';
import {PrivateRoute} from './components/routing/PrivateRoute'



const App = () => {

  const state = useSelector<IAppState, boolean>(state => state.system.online);



  return (
    
    <Router>
      <Switch>
      <PrivateRoute path='/main' online={state}>
        <div>Logged in yay</div>
      </PrivateRoute>
      <Route path='/' exact>
        <Setup/>
      </Route>
      </Switch>
    </Router>
      )
}

export default App;
