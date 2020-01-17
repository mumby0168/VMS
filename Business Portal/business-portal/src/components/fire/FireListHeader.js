import React from 'react';
import { Card, CardContent, Grid, Typography, MenuItem, InputLabel, Select, FormControl, makeStyles } from '@material-ui/core';


const useStyles = makeStyles(theme => ({
    root: {
        marginBottom: theme.spacing(2)
    }
}))

export default function FireListHeader(props) {

    const handleChange = (e) => {
        props.updateHandle(e.target.value);
    }

    const classes = useStyles();

    const options = props.summaries.map((summary, index) => {
        return <MenuItem key={index} value={summary.id}>{summary.name}</MenuItem>
    })    

    return (
        <Card className={classes.root} variant="outlined">
            <CardContent>
                <Grid alignItems="center" align="center" container>
                    <Grid xs={11}  item md={3}>
                        <FormControl style={{width: '100%'}}>
                            <InputLabel id="site-label">Site</InputLabel>
                            <Select
                            labelId="site-label"
                            id="demo-simple-select"
                            value={props.selected}
                            onChange={handleChange}>
                                {options}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid md={6} xs={1} item><div/>                        
                    </Grid>
                    <Grid xs={11} item md={3}>
                        <Typography variant="h5">Retrieved: {props.lastUpdated}</Typography>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    )
}