import {Card, CardContent} from "@material-ui/core";
import React from "react";
import List from "@material-ui/core/List";
import Avatar from "@material-ui/core/Avatar";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import ListItem from "@material-ui/core/ListItem";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import IconButton from "@material-ui/core/IconButton";
import MoreVertIcon from '@material-ui/icons/MoreVert';
import PersonPinCircleIcon from '@material-ui/icons/PersonPinCircle';


export default function VisitorList(props) {


    const items = props.visitors.map((visitor, index) => {
        return (
            <ListItem key={index}>
                <ListItemAvatar>
                    <Avatar>
                        <PersonPinCircleIcon/>
                    </Avatar>
                </ListItemAvatar>
                <ListItemText primary={visitor.name} secondary={`${visitor.siteName}: ${visitor.inTime}, ${visitor.outTime}`} />
                <ListItemSecondaryAction>
                    <IconButton>
                        <MoreVertIcon/>
                    </IconButton>
                </ListItemSecondaryAction>
            </ListItem>
        )
    })


    return (
        <Card>
            <CardContent>
                <List>
                    {items}
                </List>
            </CardContent>
        </Card>
    )
}