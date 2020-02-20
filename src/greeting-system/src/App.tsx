
import React from 'react'
import './App.css';
import Setup from './hocs/Setup';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import { IAppState } from './redux/store';
import { useSelector } from 'react-redux';
import {PrivateRoute} from './components/routing/PrivateRoute'
import Main from './components/common/Main'



const App = () => {

  const state = useSelector<IAppState, boolean>(state => state.system.online);

  

  return (
    <React.Fragment>      
    <Router>
      <Switch>
      <PrivateRoute path='/main' online={state}>
          <Main>            
          </Main>
      </PrivateRoute>
      <Route path='/' exact>
        <Setup/>
      </Route>
      <Route>
        <div>404 not found  </div> 
      </Route>
      </Switch>
    </Router>
    </React.Fragment>
      )
}

export default App;
