import React, { ReactElement } from 'react'
import { Card, Typography, CardActionArea, Paper } from '@material-ui/core'
import BackspaceIcon from '@material-ui/icons/Backspace';
import CheckIcon from '@material-ui/icons/Check';
import { useDispatch } from 'react-redux';
import { updateCodeAction } from '../../redux/actions/staffKeypadActions';
import { userInOut, UserAccess } from '../../redux/api/user'
import { operationsAggregator } from '../../operations/operationsAggregator';
import { IStaffCurrentState } from '../../redux/actions/staffActions';

interface IKeypadProps {
    code: string;
    handleSucessfulSignIn: (staffState: IStaffCurrentState | undefined) => void;
    handleSignInFailure: (reason: string) => void;
    siteId: string;
    states: IStaffCurrentState[]
}

interface IKeyPadItemProps {
    content: any;
    onclick: () => void;
}

const KeyPadItem = ({ content, onclick, }: IKeyPadItemProps) => {
    return (
        <div className="keypad-item">
            <Card variant='outlined' className="h-100 ta">
                <CardActionArea onClick={onclick} className="h-100">
                    <Typography variant="h4">
                        {content}
                    </Typography>
                </CardActionArea>
            </Card>
        </div >
    )
}

export default function Keypad({ code, handleSignInFailure, handleSucessfulSignIn, siteId, states }: IKeypadProps): ReactElement {

    const dispatch = useDispatch();

    const updateCode = (append: string) => {
        const newCode = code + append;
        dispatch(updateCodeAction(newCode));
    }

    const trimCode = () => {
        const newCode = code.substring(0, code.length - 1);
        dispatch(updateCodeAction(newCode));
    }

    //TODO: this should be pulled out to top level componet.
    const submitCode = async () => {
        const state = states.find(s => s.code === code);              
        //perform the opossite to current state
        var action; 
        if(state !== undefined) {
            action = state.action === "in" ? UserAccess.OUT : UserAccess.IN
        }
        else {
            action = UserAccess.IN;
        }
        
        var res = await userInOut(code, action, siteId);
        if (res !== "") {                          
            operationsAggregator.listen(res, () => handleSucessfulSignIn(state),
            (op) => handleSignInFailure(op.reason ?? "The reason could not be determined"));            
        }
    }

        return (
            <Paper className='numpad-wrapper'>
                <div className="keypad-grid">
                    <KeyPadItem onclick={() => updateCode('1')} content="1" />
                    <KeyPadItem onclick={() => updateCode('2')} content="2" />
                    <KeyPadItem onclick={() => updateCode('3')} content="3" />
                    <KeyPadItem onclick={() => updateCode('4')} content="4" />
                    <KeyPadItem onclick={() => updateCode('5')} content="5" />
                    <KeyPadItem onclick={() => updateCode('6')} content="6" />
                    <KeyPadItem onclick={() => updateCode('7')} content="7" />
                    <KeyPadItem onclick={() => updateCode('8')} content="8" />
                    <KeyPadItem onclick={() => updateCode('9')} content="9" />
                    <KeyPadItem onclick={submitCode} content={<CheckIcon />} />
                    <KeyPadItem onclick={() => updateCode('0')} content="0" />
                    <KeyPadItem onclick={trimCode} content={<BackspaceIcon />} />
                </div>
            </Paper>
        )
    }
