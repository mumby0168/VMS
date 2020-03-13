import React from 'react'
import {ButtonGroup, Button} from '@material-ui/core'
import {postCallback, showToast} from '../../utils/httpClient'
import * as urls from '../../names/urls'
import { getPersonalAccessRecords } from '../../actions/accessRecordActions'
import {useDispatch} from "react-redux";
import {hideSiteSpinner, showSiteSpinner} from "../../actions/uiActions";

export default function InOut(props) {

    const dispatch = useDispatch();

    const inHandle = async function(e) {
        var body = {
            userId: props.userId,
            code: props.userCode,
            siteId: props.siteId
        }
        await postCallback(`${urls.gatewayBaseUrl}users/in`, body,dispatch,
            () => (dispatch) => {
                dispatch(showSiteSpinner("Signing you in ..."))
            },
            (op) => (dispatch) => {
                dispatch(hideSiteSpinner())
                dispatch(showToast("You have you singed in successfully"))
                dispatch(getPersonalAccessRecords())
            },
            () => (dispatch) => {
                dispatch(hideSiteSpinner());
                dispatch(showToast("Sign in failed", true));
            });
    }

    const outHandle = async function(e) {
        var body = {
            userId: props.userId,
            code: props.userCode,
            siteId: props.siteId
        }
        await postCallback(`${urls.gatewayBaseUrl}users/out`, body, dispatch,
            () => (dispatch) => {
            dispatch(showSiteSpinner("Signing you out ..."))
        },
        (op) => (dispatch) => {
                dispatch(hideSiteSpinner())
                dispatch(showToast("You have you singed out successfully"))
                dispatch(getPersonalAccessRecords())
            },
            (op) => (dispatch) => {
                dispatch(hideSiteSpinner());
                dispatch(showToast("Sign out failed", true));
                console.log(op);
            });

    }
  



    return (        
        <ButtonGroup style={{  
            float: 'right'                           
        }} color="secondary" aria-label="outlined secondary button group">
            <Button onClick={inHandle}>Sign in</Button>
            <Button onClick={outHandle}>Sign out</Button>            
        </ButtonGroup>        
    )
}
