import React from 'react'
import { Grid, Card, makeStyles, Table, TableHead, TableBody, TableRow, TableCell, TableContainer } from '@material-ui/core'
import EmployeeRow from './EmployeeRow';
import Progress from '../../common/Progress'

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))


const getRows = (employees) => {
    return employees.map((employee, index) => {
        return <EmployeeRow key={index} employee={employee}></EmployeeRow>
    });
}

export default function EmployeesTable(props) {


    const content = props.loading
        ? <Progress message="Loading employee data ..." /> :
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell></TableCell>
                        <TableCell>Name</TableCell>
                        <TableCell>Site</TableCell>
                        <TableCell>Status</TableCell>
                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {getRows(props.employees)}
                </TableBody>
            </Table>
        </TableContainer>


    const classes = useStyles();

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                {content}
            </Card>
        </Grid>
    )
}
