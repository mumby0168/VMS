import React from 'react';
import { CircularProgress } from "@material-ui/core";

export default function Progress(props) {
    return (
        <div align="center">
            <CircularProgress/>
            <h4>{props.message}</h4>
        </div>
    )
}