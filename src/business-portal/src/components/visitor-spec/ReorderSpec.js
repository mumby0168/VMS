import React from 'react'
import { Card, CardContent, Button, makeStyles, TextField } from '@material-ui/core';


const useStyles = makeStyles(theme => ({
    root: {
        padding: theme.spacing(2)
    }
}))

export default function ReorderSpec(props) {

    const { updateOrder, max, current, id } = props;

    console.log(id);

    const [count, setCount] = React.useState(current)

    const classes = useStyles();    

    return (
        <Card className={classes.root}>
            <CardContent align="center">                
                    <TextField
                    label="Order"
                    type="number"    
                    value={count}                
                    onChange={e => setCount(e.target.value)}
                    style={{
                        width: "100px",
                        marginBottom: "1rem"
                    }}                    
                    InputProps={{ inputProps: { min: 1, max: max } }}
                    />     
                    <br/>           
                <Button variant="contained" color="secondary" onClick={() => updateOrder(count, id)}>Update</Button>
            </CardContent>
        </Card>
    )
}
