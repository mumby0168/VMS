
import React from 'react'
import { Select, MenuItem, Button, makeStyles, Theme, createStyles } from '@material-ui/core'
import { IKeyValuePair } from '../../redux/common/types'


interface ISiteSelectProps {
    sites: IKeyValuePair[]
    selected: IKeyValuePair;
    selectionChangedHandle(chosen: IKeyValuePair): void;
    confirmHandle(): void;
}

const useStyles = makeStyles((theme: Theme) => createStyles({
    textFieldSpacing: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1)
    }
}))

export const SiteSelect: React.FunctionComponent<ISiteSelectProps> = (props: ISiteSelectProps) => {


    const classes = useStyles();

    const items = props.sites.map((site, index) => {
        return <MenuItem key={index} value={site.key}>{site.value}</MenuItem>
    })

    const change = (e: any) =>{
        const id = e.targert.value;
        const selected = props.sites.find(s => s.key === id);
        if(selected) {
            props.selectionChangedHandle(selected);
        }
    }

    return (
        <React.Fragment>            

            <Select 
            className={classes.textFieldSpacing}
                fullWidth
                value={props.selected}
                onChange={change}
            >
                {items}
            </Select>            

            <Button className={classes.textFieldSpacing} onClick={props.confirmHandle} variant="contained" color="secondary">Confirm</Button>

        </React.Fragment>
    )
}