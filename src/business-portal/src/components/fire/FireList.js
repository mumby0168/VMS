import React from 'react'
import {  Card, CardContent,List, ListItem, ListItemText, ListItemSecondaryAction, Avatar, Checkbox, ListItemAvatar, makeStyles } from '@material-ui/core'
import Progress from '../../common/Progress'


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
                    <ListItemText label={index} primary={user.fullName}/>
                    <ListItemSecondaryAction>
                        <Checkbox inputProps={{ 'aria-labelledby': index }}  edge="end"></Checkbox>
                    </ListItemSecondaryAction>
                </ListItem>     
              
    })

    console.log("loading: " + props.loading);

    const render = props.loading ? <Progress message="Loading fire list."/> : <List>{items}</List>;



    return (
        <Card variant="outlined">
            <CardContent className={classes.root}>                
                    {render}                
            </CardContent>            
        </Card>
    )
}