import React from 'react';


export default function AvailabilityList(props) {   
    
    console.log(props.availability);

    const available = props.availability !== null ?  props.availability.users.map((user, index) => {
        return <h2>{user.name + " STATUS: " + user.status}</h2>
    }) : <h2>No Info</h2>

    return (
        <div>
            <h1>{props.name}</h1>
            {available}
        </div>
    )
}