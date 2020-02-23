import React, { ReactElement } from 'react'
import { TextField, Paper, InputAdornment } from '@material-ui/core'
import SearchIcon from '@material-ui/icons/Search';

interface IStaffSearchHeaderProps {
    searchText: string;
    updateSearchHandle: (text: string) => void;    
}


export default function StaffSearchHeader({ searchText, updateSearchHandle }: IStaffSearchHeaderProps): ReactElement {
    return (
        <React.Fragment>
            <div className='center'>
                <Paper className='header-card'>
                    <h2>Who are you visiting today?</h2>
                    <div className='search-field-wrapper'>
                        <TextField                        
                            value={searchText}
                            onChange={(e) => updateSearchHandle(e.target.value)}
                            fullWidth placeholder='Search Staff Members'
                            InputProps={{
                                startAdornment: (
                                    <InputAdornment position='start' >
                                        <SearchIcon />
                                    </InputAdornment>
                                )
                            }}
                        />
                    </div>
                </Paper>
            </div>
        </React.Fragment>
    )
}
