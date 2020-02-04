import React from 'react'
import { Grid, Card, makeStyles, CardHeader,  CardActions, Fab } from '@material-ui/core'
import { Add } from '@material-ui/icons';
import ClearAllIcon from '@material-ui/icons/ClearAll';

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function VisitorSpecHeader(props) {    
    const classes = useStyles();        

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                <CardHeader title="Visitors Specification"/>                  
                    <CardActions>
                        <Fab size="small" onClick={(e) => props.openAdd()} color="secondary" variant="extended">
                            <Add/>
                             Add Specification
                        </Fab>
                        <Fab size="small" onClick={(e) => props.specHandle()} color="secondary" variant="extended">
                            <ClearAllIcon/>
                             Deprecated Specifications
                        </Fab>
                    </CardActions>
            </Card>
        </Grid>
    )
}
