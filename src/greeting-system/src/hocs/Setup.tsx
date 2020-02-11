import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { compose } from "redux";

interface ISetupProps {
    online: boolean;
}

const mapStateToProps = (state: IAppState) => {
    return {
        online: state.system.online
    }
}

class Setup extends React.Component<ISetupProps> {
    
    constructor(props: ISetupProps) {
        super(props);        
    }
    
    componentDidMount() {

    }


    public render() {        

        const online = this.props.online ? "Online" : "Offline";

        return (
        <h1>Setup Page: Online: {online}</h1>
        )
    }
}

export default connect(mapStateToProps, {})(Setup)

