import React, { useEffect } from 'react'
import { Card, CardContent, Typography } from '@material-ui/core';



const date = () => new Date();

export const Time = () => {       

    useEffect(() => {
        const id = setInterval(() => {
            setTime(date().toLocaleTimeString())
        }, 1000)
        return () => clearInterval(id) ;
    }, [])

    const [time, setTime] = React.useState<string>(date().toLocaleTimeString());

    return (
            <div >
                <Typography variant="h4">{time}</Typography>
            </div>        
    )

}