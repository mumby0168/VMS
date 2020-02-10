import React from 'react';
import { Divider, Box, makeStyles, Toolbar, Button, Typography } from '@material-ui/core';
import ProfileMenu from './ProfileMenu';
import EmojiPeopleIcon from '@material-ui/icons/EmojiPeople';

const useStyles = makeStyles({
    center: {
        flexGrow: 1,
        paddingLeft: '1rem',
        paddingRight: '1rem'
    }
});

export default function Menu({ logout, ...props }) {

    const getButtons = (role) => {
        if (role === "Standard") {
            return (
                <React.Fragment>
                    <Button onClick={(e) => props.navigate("/landing")} color="inherit">Dashboard</Button>
                    <Button onClick={(e) => props.navigate("/availability")} color="inherit">Sites</Button>
                    <Button onClick={(e) => props.navigate("/firelist")} color="inherit">Fire List</Button>
                </React.Fragment>
            )
        }
        else if (role === "BusinessAdmin") {
            return (
                <React.Fragment>
                    {getButtons("Standard")}
                    <Button onClick={(e) => props.navigate("/visitors")} color="inherit">Visitors</Button>
                    <Button onClick={(e) => props.navigate("/employees")} color="inherit">Employees</Button>
                </React.Fragment>
            )
        }
    }

    var classes = useStyles();

    var title = props.businessName === "" ? "VMS" : props.businessName;

    var profile = props.role === null ? "" : <ProfileMenu handleThemeChange={props.handleThemeChange} initials={props.initials} logout={logout} ></ProfileMenu>

    return (
        <Toolbar>
            <EmojiPeopleIcon></EmojiPeopleIcon>
            <Typography variant="h6">{title}</Typography>
            <Divider orientation="vertical" />
            <Box className={classes.center}>
                {getButtons(props.role)}
            </Box>
            <Divider orientation="vertical" />
            {profile}
        </Toolbar>
    )
}