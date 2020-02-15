import { Card, CardContent, CircularProgress, Typography } from "@material-ui/core"
import React from 'react'

interface ILoaderProps {
    message: string;
}

export const Loader = (props: ILoaderProps) => {

    return (
        <Card>
            <CardContent>
                <div className="center">
                    <CircularProgress></CircularProgress>
                    <Typography>{props.message}</Typography>
                </div>
            </CardContent>
        </Card>
    )
}