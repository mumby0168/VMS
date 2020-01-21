import React from 'react'
import { Grid, Card, makeStyles, CardHeader } from '@material-ui/core'

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function EmployeesHeader(props) {

    const classes = useStyles();

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                <CardHeader title="Employee's"/>                                    
            </Card>
        </Grid>
    )
}
