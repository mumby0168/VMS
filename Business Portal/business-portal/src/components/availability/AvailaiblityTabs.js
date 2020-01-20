import React from 'react'
import { Tabs, Tab} from '@material-ui/core'



export default function AvailabilityTabs(props) {        
    const tabOrientation = "horizontal"

    const a11yProps = (index) =>  {
        return {
          id: `vertical-tab-${index}`,
          'aria-controls': `vertical-tabpanel-${index}`,
        };
      }

    const tabs = props.siteSummaries.map((summary, index) => {
        return <Tab key={index} label={summary.name} {...a11yProps(index)}></Tab>        
    });

    return (
        <Tabs            
            orientation={tabOrientation}
            variant="scrollable"                
            aria-label="Vertical tabs example"  
            onChange={props.handleChange}
            value={props.value}>                
                {tabs}
            </Tabs>     
    )
}