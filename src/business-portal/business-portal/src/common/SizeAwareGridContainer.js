import React from 'react'
import { useMediaQuery, Grid } from "@material-ui/core"

export default function SizeAwareGrid(props) {    
    const matches = useMediaQuery('(min-width:600px)');

    const direction = matches ? "row" : "column";


    return (
        <Grid container direction={direction}>
            {props.children}
        </Grid>
    )
}