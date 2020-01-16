import React from 'react';
export function AvailabilityTabPanel(props) {
    const { children, value, index, ...other } = props;
    return (
        <div         
        component="div" role="tabpanel" 
        hidden={value !== index} 
        id={`vertical-tabpanel-${index}`} 
        aria-labelledby={`vertical-tab-${index}`} {...other}>
                {value === index ? <div>{children}</div> : "NOTHING"}
        </div>
    );
}
