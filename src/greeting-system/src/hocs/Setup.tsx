import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { Card, CardHeader, CardContent } from '@material-ui/core'
import LoginForm from "../components/setup/LoginForm";
import '../hoc-styles/Setup.css'
import { login } from "../redux/api/identity";
import { ISetupForm, loginFormUpdate, loginRejectedAction } from "../redux/actions/setupActions";
import { Loader } from "../components/common/Loader";
import { RouteComponentProps, Redirect } from "react-router";


interface ISetupProps {
    online: boolean;
    login(code: string, email: string, password: string): void,
    updateForm(data: ISetupForm): void,
    formData: ISetupForm,
    error: string,
    loading: boolean;
    updateFormError(message: string): void;
}

const mapStateToProps = (state: IAppState) => {
    return {
        online: state.system.online,
        formData: {
            email: state.setup.email,
            password: state.setup.password,
            code: state.setup.code
        },
        error: state.setup.errorMessage,
        loading: state.setup.loading,
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        login: (code: string, email: string, password: string) => dispatch(login(code, email, password)),
        updateForm: (data: ISetupForm) => dispatch(loginFormUpdate(data)),
        updateFormError: (message: string) => dispatch(loginRejectedAction({Code: 'validation', Reason: message}))
    }
}


class Setup extends React.Component<ISetupProps> {

    login(data: ISetupForm) {

        if(data.code.length !== 6) {
            this.props.updateFormError('The business code must be 6 digits.');
            return;
        }

        this.props.login(data.code, data.email, data.password);
    }

    public render() {

        if(this.props.online) {
            return <Redirect to='/main'></Redirect>
        }


        const content = this.props.loading ? <Loader message="Logging you in ..."/> : 
        <LoginForm error={this.props.error} login={this.login.bind(this)} formData={this.props.formData} updateForm={this.props.updateForm}/>

        return (
            <div className="center background">
                <Card className="card">
                    <CardHeader title="Please login to setup account"></CardHeader>
                    <CardContent>
                        {content}
                    </CardContent>                    
                </Card>
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

