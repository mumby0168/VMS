import React from 'react'
import { Grid, Card, makeStyles, Table, TableHead, TableBody, TableRow, TableCell, TableContainer, Button } from '@material-ui/core'
import EmployeeRow from './EmployeeRow';
import Progress from '../../common/Progress'
import CardDialog from '../../common/CardDialog'
import { hideRemoveConfirmation, showRemoveConfirmation, removeAccount } from '../../actions/employeeActions';

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))


const getRows = (employees, confirmHandle) => {
    return employees.map((employee, index) => {
        return <EmployeeRow showConfirm={confirmHandle} key={index} employee={employee}></EmployeeRow>
    });
}

export default function EmployeesTable(props) {    

    const {isOpen, id} = props.confirm;

    const removeEmployee = (id) => {
        props.dispatch(removeAccount(id));
    }

    const showConfirm = (id) => {
        props.dispatch(showRemoveConfirmation(id));
    }

    const handleClose = () => { 
        props.dispatch(hideRemoveConfirmation());
    }

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
                    {getRows(props.employees, showConfirm)}
                </TableBody>
            </Table>
        </TableContainer>


    const classes = useStyles();

    return (            
        <Grid className={classes.root} item xs={12} md={12}>
            <CardDialog open={isOpen} title="Please confirm removing employee"
            text="This will remove access for the employee and all of there access records." showCancel
            handleClose={handleClose} actions={
                <Button variant="contained" onClick={(e) => removeEmployee(id)} color="secondary">Remove</Button>
            }>
                
            </CardDialog>
            <Card className={classes.card}>
                {content}
            </Card>
        </Grid>
    )
}
