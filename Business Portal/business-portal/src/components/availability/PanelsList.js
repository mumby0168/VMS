import React from 'react';
import { AvailabilityTabPanel } from "./AvailabilityTabPanel";
import AvailabilityList from './AvailabilityList';
import Progress from '../../common/Progress';


export function PanelsList(props) {
    



    const panels = props.siteSummaries.map((summary, index) => {

        const content = props.loading ? <Progress message="Loading availability"/> : <AvailabilityList name={summary.name} availability={props.availability}/>

        return (
        <AvailabilityTabPanel key={index} index={index} value={props.value}>
            {content}
        </AvailabilityTabPanel>);
        });    


    return (
        <div>
            {panels}
        </div>
    )    
}
