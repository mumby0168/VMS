import React from 'react'
import {Route, Redirect} from 'react-router-dom'

export default function PrivateRoute({ children, valid, ...rest }) {

    console.log("Route is " + valid);

    return (
      <Route
        {...rest}
        render={({ location }) =>
          valid ? (
            children
          ) : (
            <Redirect
              to={{
                pathname: "/login",
                state: { from: location }
              }}
            />
          )
        }
      />
    );
  }


