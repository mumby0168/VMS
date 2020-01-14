import React from 'react';
import { Divider, Box, makeStyles, Toolbar, Button, Typography } from '@material-ui/core';
import ProfileMenu from './ProfileMenu';
import EmojiPeopleIcon from '@material-ui/icons/EmojiPeople';

const useStyles = makeStyles({
    center: {
        flexGrow: 1
    }
});

const getButtons = (role) => {
    if(role === "Standard") {
        return (
            <div>
            <Button color="inherit">Sites</Button>
            <Button color="inherit">Fire List</Button>
            </div>
        )
    }
    else if(role === "BusinessAdmin") {
        return (
            <div>
            <Button color="inherit">Sites</Button>
            <Button color="inherit">Fire List</Button>
            <Button color="inherit">Accounts</Button>
            <Button color="inherit">Visitors</Button>
            <Button color="inherit">Employees</Button>
            </div>
        )
    }
}

export default function Menu({logout, ...props}) {

    console.log(props);

    var classes = useStyles();

    var profile = props.role === null ? "" : <ProfileMenu logout={logout} ></ProfileMenu>

    return (
        <Toolbar>            
            <EmojiPeopleIcon></EmojiPeopleIcon>
            <Typography variant="h6">VMS</Typography>            
            <Divider orientation="vertical"/>
            <Box align="center" className={classes.center}>
                {getButtons(props.role)}
            </Box>
            <Divider orientation="vertical"/>
                {profile}
            </Toolbar>
    )
}