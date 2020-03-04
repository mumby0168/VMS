import React from 'react';
import {connect} from "react-redux";
import {IVisitor} from "../redux/actions/visitorActions";
import {IAppState} from "../redux/store";
import {List, Paper, Typography} from '@material-ui/core';
import {VisitorListItem} from "../components/visitor-out/VisitorListItem";
import {getSignedInVisitors} from "../redux/api/visitors";

interface IVisitorOutProps {
    visitors: IVisitor[]
    loading: boolean;
    error: string;
    siteId: string;

    loadVisitors: (siteId: string) => void;
}

class VisitorOut extends React.Component<IVisitorOutProps> {

    componentDidMount(): void {
        this.props.loadVisitors(this.props.siteId);
    }


    render() {

        const items = this.props.visitors.map((visitor, index) => {
            return <VisitorListItem visitor={visitor} key={index}/>
        });

        return (
            <div className='center h-100'>
            <div className='h-100' style={{width: '75%'}}>
                <div style={{height: '18%'}}>
                    <Paper className="center h-100" >
                        <Typography variant="h4">
                            Please select yourself to sign out.
                        </Typography>
                    </Paper>
                </div>
                    <Paper style={{
                        maxHeight: '500px',
                        overflowY: 'auto'
                    }}>
                    <List style={{width: '100%'}}>
                        {items}
                    </List>
                    </Paper>

            </div>
            </div>
        );
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        visitors: state.visitors.visitors,
        loading: state.visitors.loading,
        error: state.visitors.error,
        siteId: state.system.site.id
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return {
        loadVisitors: (siteId: string) => dispatch(getSignedInVisitors(siteId)),
    }
};







export default  connect(mapStateToProps, mapDispatchToProps)(VisitorOut);