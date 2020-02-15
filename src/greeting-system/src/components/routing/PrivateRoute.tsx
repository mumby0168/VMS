import React, { FunctionComponent } from 'react'
import { Route, Redirect } from 'react-router'

interface IPrivateRouteProps {
    online: boolean;
    path: string;
}

export const PrivateRoute : FunctionComponent<IPrivateRouteProps> = ({online, path, children})  => 
online ? <Route path={path}>
    {children}
</Route> : <Redirect to='/'/>
