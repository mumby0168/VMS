import React from 'react';
import IconButton from '@material-ui/core/IconButton'
import AccountCircle from '@material-ui/icons/AccountCircle';
import Menu from '@material-ui/core/Menu';
import { MenuItem, Box } from '@material-ui/core';


export default function ProfileMenu({logout, ...props}) {

    const [anchorEl, setAnchorEl] = React.useState(null);

    const open = Boolean(anchorEl);

    const handleMenu = event => {
        setAnchorEl(event.currentTarget);
    };    

    const handleClose = () => {
        setAnchorEl(null);
    };

    const anchorOrigin = {
        vertical: 'bottom',
        horizontal: 'center',
    }
    const transformOrigin = {
        vertical: 'top',
        horizontal: 'center',
    }            
      
    

    return (      
      <Box>
              <IconButton
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"                
                color="inherit"
                onClick={handleMenu}
              >
                <AccountCircle />
              </IconButton>    
              <Menu                    
                    onClose={handleClose}
                    anchorEl={anchorEl}
                    getContentAnchorEl={null}
                    anchorOrigin={anchorOrigin}
                    transformOrigin={transformOrigin}
                    open={open}
                    {...props}
                    >
                
                <MenuItem onClick={handleClose}>My account</MenuItem>
                <MenuItem onClick={logout}>Logout</MenuItem>
              </Menu>            
              </Box>                       
    )
}