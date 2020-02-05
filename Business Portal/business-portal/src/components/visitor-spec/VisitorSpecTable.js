import React from 'react'
import { Grid, Card, makeStyles, Table, TableHead, TableBody, TableRow, TableCell, TableContainer } from '@material-ui/core'
import Progress from '../../common/Progress';
import VisitorSpecRow from './VisitorSpecRow';

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function VisitorSpecTable(props) {

    console.log(props);

    const { loading, specifications } = props;
    const classes = useStyles();    

    const updateOrder = (newOrder) => {
        console.log(newOrder);
    } 

    const specs = specifications.map((spec, index) => {
        return <VisitorSpecRow maxOrder={specifications.length} updateOrder={updateOrder} spec={spec} key={index}/>
    });


    const content = loading ? <Progress message="Loading specificatons" /> :
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>                        
                        <TableCell>Order</TableCell>
                        <TableCell>Label</TableCell>
                        <TableCell>Validation Type</TableCell>
                        <TableCell>Validation Message</TableCell>
                        <TableCell></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {specs}
                </TableBody>
            </Table>
        </TableContainer>


    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                {content}
            </Card>
        </Grid>
    )
}
