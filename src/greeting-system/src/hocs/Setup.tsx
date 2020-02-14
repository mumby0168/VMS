import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { Button, Card, CardHeader, CardContent, CardActions } from '@material-ui/core'
import LoginForm from "../components/setup/LoginForm";
import './Setup.css'
import { login } from "../redux/api/identity";

interface ISetupProps {
    online: boolean;
    login(code: number, email: string, password: string): void
}

const mapStateToProps = (state: IAppState) => {
    return {
        online: state.system.online
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        login: (code: number, email: string, password: string) => dispatch(login(code, email, password))
    }
}


class Setup extends React.Component<ISetupProps> {

    public render() {

        const online = this.props.online ? "Online" : "Offline";

        return (
            <div className="center background">
                <Card className="card">
                    <CardHeader title="Please login to setup account"></CardHeader>
                    <CardContent>
                        <LoginForm/>
                        <Button onClick={(e) => {
                            this.props.login(123456, 'test@test.com', 'Test123')
                        }}>Test</Button>
                    </CardContent>                    
                </Card>
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

