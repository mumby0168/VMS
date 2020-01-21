import React from 'react'
import {ButtonGroup, Button} from '@material-ui/core'
import {postCallback} from '../../utils/httpClient'
import * as urls from '../../names/urls'

export default function InOut(props) {

    const inHandle = async function(e) {
        var body = {
            userId: props.userId
        }
        await postCallback(`${urls.gatewayBaseUrl}users/in`, body, "You have succesfully signed in.", props.dispatchHandle);
    }

    const outHandle = async function(e) {
        var body = {
            userId: props.userId
        }
        await postCallback(`${urls.gatewayBaseUrl}users/out`, body, "You have succesfully signed out.", props.dispatchHandle);
    }



    return (        
        <ButtonGroup style={{
            margin: '5px',
            float: 'right'            
        }} color="secondary" aria-label="outlined secondary button group">
            <Button onClick={inHandle}>Sign in</Button>
            <Button onClick={outHandle}>Sign out</Button>            
        </ButtonGroup>        
    )
}
