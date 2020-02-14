import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { Button, Card, CardHeader, CardContent, CardActions } from '@material-ui/core'
import LoginForm from "../components/setup/LoginForm";
import './Setup.css'
import { login } from "../redux/api/identity";
import { ISetupForm, loginFormUpdate } from "../redux/actions/setupActions";


interface ISetupProps {
    online: boolean;
    login(code: string, email: string, password: string): void,
    updateForm(data: ISetupForm): void,
    formData: ISetupForm,
    error: string
}

const mapStateToProps = (state: IAppState) => {
    return {
        online: state.system.online,
        formData: {
            email: state.setup.email,
            password: state.setup.password,
            code: state.setup.code
        },
        error: state.setup.errorMessage
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        login: (code: string, email: string, password: string) => dispatch(login(code, email, password)),
        updateForm: (data: ISetupForm) => dispatch(loginFormUpdate(data)),
    }
}


class Setup extends React.Component<ISetupProps> {


    login(data: ISetupForm) {
        this.props.login(data.code, data.email, data.password);
    }

    public render() {

        const online = this.props.online ? "Online" : "Offline";

        

        return (
            <div className="center background">
                <Card className="card">
                    <CardHeader title="Please login to setup account"></CardHeader>
                    <CardContent>
                        <LoginForm error={this.props.error} login={this.login.bind(this)} formData={this.props.formData} updateForm={this.props.updateForm}/>
                    </CardContent>                    
                </Card>
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

