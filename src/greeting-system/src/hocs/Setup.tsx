import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { loginSuccesful } from "../redux/actions/systemActions";
import {Button} from '@material-ui/core'

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
            <div>
                <h1>Setup Page: Online: {online}</h1>
                <Button onClick={this.props.login}>Click me to login</Button>
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

