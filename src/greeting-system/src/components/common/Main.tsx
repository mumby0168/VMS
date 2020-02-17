import React from 'react'
import { Paper } from '@material-ui/core'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {
    public render() {
        return (
            <Paper className="full-height" >
                {this.props.children}
            </Paper>
        )
    }
}