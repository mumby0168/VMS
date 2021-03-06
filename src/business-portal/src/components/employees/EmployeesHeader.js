import React from 'react'
import { Grid, Card, makeStyles, CardHeader, CardActions, Fab } from '@material-ui/core'
import {Add, HourglassEmpty } from '@material-ui/icons'
import { useDispatch } from 'react-redux'
import { showAddEmployee, showPending, getPendingAccounts } from '../../actions/employeeActions'

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function EmployeesHeader() {

    const dispatch = useDispatch();
    const classes = useStyles();

    const openPending = function() {
        dispatch(getPendingAccounts())
        dispatch(showPending());
    }

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                <CardHeader title="Employee's"/>                  
                    <CardActions>
                        <Fab onClick={(e) => dispatch(showAddEmployee())} size="small" color="secondary" variant="extended">
                            <Add/>
                            Add Employee
                        </Fab>
                        <Fab onClick={openPending} size="small" variant="extended" color="secondary">
                            <HourglassEmpty/>
                            Pending Accounts
                        </Fab>
                    </CardActions>                
            </Card>
        </Grid>
    )
}
