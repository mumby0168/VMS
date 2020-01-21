import React from 'react'
import { Grid, Card, makeStyles, Table, TableHead, TableBody, TableRow, TableCell, TableContainer } from '@material-ui/core'
import EmployeeRow from './EmployeeRow';

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function EmployeesTable(props) {
   
    const rows =  props.employees.map((employee, index) => {
        return <EmployeeRow key={index}></EmployeeRow>
    })

    const classes = useStyles();

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>Site</TableCell>
                                <TableCell>Status</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {rows}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Card>
        </Grid>
    )
}
