import React from 'react'
import { Grid, Card, makeStyles, CardHeader,  CardActions, Fab } from '@material-ui/core'
import DataUsageIcon from '@material-ui/icons/DataUsage';

const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: '1rem'
    },
    card: {
        padding: theme.spacing(1)
    }
}))

export default function VisitorsHeader(props) {    
    const classes = useStyles();        

    return (
        <Grid className={classes.root} item xs={12} md={12}>
            <Card className={classes.card}>
                <CardHeader title="Visitors"/>                  
                    <CardActions>
                        <Fab size="small" onClick={(e) => props.specHandle()} color="secondary" variant="extended">
                            <DataUsageIcon/>
                             Manage Specification
                        </Fab>
                    </CardActions>
            </Card>
        </Grid>
    )
}
