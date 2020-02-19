import React, { ReactElement } from 'react'
import { Card, CardContent, Typography, CardActionArea } from '@material-ui/core'
import BackspaceIcon from '@material-ui/icons/Backspace';
import CheckIcon from '@material-ui/icons/Check';
import { useDispatch } from 'react-redux';
import { updateCodeAction } from '../../redux/actions/staffKeypadActions';

interface IKeypadProps {
    code: string;
}

interface IKeyPadItemProps {
    content: any;   
    onclick: () => void;
}

const KeyPadItem = ({content, onclick}: IKeyPadItemProps) => {
    return (
    <div className="keypad-item">
        <Card className="h-100 ta">
            <CardActionArea onClick={onclick} className="h-100">
                <Typography variant="h4">
                    {content}
            </Typography>
            </CardActionArea>
        </Card>
    </div >
    )
}

export default function Keypad({ code}: IKeypadProps): ReactElement {

    const dispatch = useDispatch();

    const updateCode = (append: string) => {
        const newCode = code + append;
        dispatch(updateCodeAction(newCode));
    }

    const trimCode = () => {
        const newCode = code.substring(0, code.length - 1);
        dispatch(updateCodeAction(newCode));
    }

    const submitCode = () => {
        console.log('sending code  ...' + code)
    }

    return (
        <Card className="h-100">            
                <div className="keypad-grid">
                    <KeyPadItem onclick={() => updateCode('1')} content="1"/>
                    <KeyPadItem onclick={() => updateCode('2')} content="2"/>
                    <KeyPadItem onclick={() => updateCode('3')} content="3"/>
                    <KeyPadItem onclick={() => updateCode('4')} content="4"/>
                    <KeyPadItem onclick={() => updateCode('5')} content="5"/>
                    <KeyPadItem onclick={() => updateCode('6')} content="6"/>
                    <KeyPadItem onclick={() => updateCode('7')} content="8"/>
                    <KeyPadItem onclick={() => updateCode('8')} content="9"/>
                    <KeyPadItem onclick={() => updateCode('9')} content="9"/>
                    <KeyPadItem onclick={submitCode} content={<CheckIcon/>}/>
                    <KeyPadItem onclick={() => updateCode('0')} content="0"/>
                    <KeyPadItem onclick={trimCode} content={<BackspaceIcon/>}/>
                </div>            
        </Card>
    )
}
