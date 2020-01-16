import React from 'react';
import { AvailabilityTabPanel } from "./AvailabilityTabPanel";
import AvailabilityList from './AvailabilityList';


export function PanelsList(props) {
    
    const paneles = props.siteSummaries.map((summary, index) => {
        return (
        <AvailabilityTabPanel key={index} index={index} value={props.value}>
            <AvailabilityList name={summary.name} availability={props.availability}/>
        </AvailabilityTabPanel>);
        });    


    return (
        <div>
            {paneles}
        </div>
    )    
}
