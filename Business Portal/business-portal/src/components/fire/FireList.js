import React from 'react'
import {  Card, CardContent,List, ListItem, ListItemText, ListItemSecondaryAction, Avatar, Checkbox, ListItemAvatar, makeStyles } from '@material-ui/core'


const useStyles = makeStyles(state => ({
    root: {
        
    }
}))

export default function FireList(props) {

    const classes = useStyles();

    const items = props.users.map((user, index) => {
        return  <ListItem key={index}>
                    <ListItemAvatar>
                        <Avatar/>
                    </ListItemAvatar>                                  
                    <ListItemText label primary={user.fullName}/>
                    <ListItemSecondaryAction>
                        <Checkbox inputProps={{ 'aria-labelledby': index }}  edge="end"></Checkbox>
                    </ListItemSecondaryAction>
                </ListItem>     
              
    })


    return (
        <Card variant="outlined">
            <CardContent className={classes.root}>
                <List>
                    {items}
                </List>                
            </CardContent>            
        </Card>
    )
}