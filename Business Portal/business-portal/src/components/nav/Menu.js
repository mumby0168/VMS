import React from 'react';
import { Divider, Box, makeStyles, Toolbar, Button, Typography } from '@material-ui/core';
import ProfileMenu from './ProfileMenu';
import EmojiPeopleIcon from '@material-ui/icons/EmojiPeople';

const useStyles = makeStyles({
    center: {
        flexGrow: 1
    }
});

export default function Menu({logout, ...props}) {
    
    const getButtons = (role) => {
        if(role === "Standard") {
            return (
                <div>
                <Button onClick={(e) => props.navigate("/landing")} color="inherit">Dashboard</Button>
                <Button onClick={(e) => props.navigate("/availability")} color="inherit">Sites</Button>
                <Button color="inherit">Fire List</Button>
                </div>
            )
        }
        else if(role === "BusinessAdmin") {
            return (
                <div>
                {getButtons("Standard")}
                <Button color="inherit">Accounts</Button>
                <Button color="inherit">Visitors</Button>
                <Button color="inherit">Employees</Button>
                </div>
            )
        }
    }

    var classes = useStyles();

    var title = props.businessName === "" ? "VMS" : props.businessName;

    var profile = props.role === null ? "" : <ProfileMenu initials={props.initials} logout={logout} ></ProfileMenu>

    return (
        <Toolbar>            
            <EmojiPeopleIcon></EmojiPeopleIcon>
            <Typography variant="h6">{title}</Typography>            
            <Divider orientation="vertical"/>
            <Box align="center" className={classes.center}>
                {getButtons(props.role)}
            </Box>
            <Divider orientation="vertical"/>
                {profile}
            </Toolbar>
    )
}