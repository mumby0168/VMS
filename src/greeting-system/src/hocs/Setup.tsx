import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { loginSuccesful } from "../redux/actions/systemActions";
import { Button, Card, CardHeader, CardContent, CardActions } from '@material-ui/core'
import LoginForm from "../components/setup/LoginForm";
import './Setup.css'

interface ISetupProps {
    online: boolean;
    login(): void
}

const mapStateToProps = (state: IAppState) => {
    return {
        online: state.system.online
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        login: () => dispatch(loginSuccesful("hellooooooo"))
    }
}


class Setup extends React.Component<ISetupProps> {

    public render() {

        const online = this.props.online ? "Online" : "Offline";

        return (
            <div className="center">
                <Card>
                    <CardHeader title="Please login to setup account"></CardHeader>
                    <CardContent>
                        <LoginForm/>
                    </CardContent>
                    <CardActions>
                        <Button variant="contained" color="primary">Login</Button>
                    </CardActions>
                </Card>
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

