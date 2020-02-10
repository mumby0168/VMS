import React from 'react'
import { Button, Menu, MenuItem, FormControl, InputLabel, Select } from '@material-ui/core'

export default function AvailabilityOptions() {

    const [anchorEl, setAnchorEl] = React.useState(null);

    const handleClick = event => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <div>
            <Button aria-controls="simple-menu" aria-haspopup="true" onClick={handleClick}>
                Open Menu
        </Button>
            <Menu                
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}>
                <MenuItem onClick={handleClose} style={{
                    minWidth: '100px'
                }}>
                    <FormControl>
                        <InputLabel id="results">Results</InputLabel>
                        <Select
                            labelId="results"
                            id="results"
                            >                            
                            <option>10</option>
                            <option>20</option>
                            <option>50</option>
                            <option>100</option>
                        </Select>
                    </FormControl>
                </MenuItem>
            </Menu>
        </div>
    )
}
