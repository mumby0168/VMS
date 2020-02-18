import React, { useEffect } from 'react'
import { Typography } from '@material-ui/core';



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
                <Typography variant="h5">{time}</Typography>
            </div>        
    )

}