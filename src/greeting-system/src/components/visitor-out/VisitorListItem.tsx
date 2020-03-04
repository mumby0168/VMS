import React from 'react'
import {Typography, ListItemText, Avatar, ListItem, ListItemAvatar, Divider} from '@material-ui/core'
import PersonPinIcon from '@material-ui/icons/PersonPin';
import {IVisitor} from "../../redux/actions/visitorActions";

interface IVisitorListItemProps {
    visitor: IVisitor;
}

export function VisitorListItem({visitor}: IVisitorListItemProps) {
    return (
    <React.Fragment>
    <ListItem button alignItems="flex-start">
        <ListItemAvatar>
            <Avatar>
                <PersonPinIcon/>
            </Avatar>
        </ListItemAvatar>
        <ListItemText
            primary={visitor.name}
            secondary={
                <React.Fragment>
                    <Typography
                        component="span"
                        variant="body2"
                        color="textPrimary"
                        style={{display: 'inline'}}
                    >
                        Signed in at
                    </Typography>
                    {` : ${visitor.inAt}`}
                </React.Fragment>
            }
        />
    </ListItem>
    <Divider component='li'/>
    </React.Fragment>
    )
}