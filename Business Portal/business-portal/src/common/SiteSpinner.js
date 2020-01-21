import React from 'react'
import { Backdrop, Card, makeStyles } from '@material-ui/core'
import Progress from './Progress'
import { useSelector } from 'react-redux'

const useStyles = makeStyles(theme => ({
    backdrop: {
        zIndex: 255
    },
    card: {
        padding: '30px'
    }
}));

export default function SiteSpinner() {

    const classes = useStyles();

    const state = useSelector(state => {
        return {
            show: state.ui.siteSpinner,
            message: state.ui.message
        }
    })




    return (
        <Backdrop className={classes.backdrop} open={state.show}>
            <Card className={classes.card}>
                <Progress message={state.message}/>
            </Card>
        </Backdrop>
    )
}
