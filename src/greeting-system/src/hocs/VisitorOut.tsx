import React from 'react';
import {connect} from "react-redux";
import {
    IVisitor,
    updateSelectedVisitor,
    visitorSelectionCancelled,
    visitorSelectionConfirmed
} from "../redux/actions/visitorActions";
import {IAppState} from "../redux/store";
import {List, Paper, Typography} from '@material-ui/core';
import {VisitorListItem} from "../components/visitor-out/VisitorListItem";
import {getSignedInVisitors, signOut} from "../redux/api/visitors";
import VisitorConfirmationDialog from "../components/visitor-out/VisitorConfirmationDialog";
import {operationsAggregator} from "../operations/operationsAggregator";
import {IOperation} from "../operations/operationsManager";
import {closeOverlay, IconType, openOverlay, updateOverlayAction} from "../redux/actions/overlayActions";
import {SystemViews, viewChangedAction} from "../redux/actions/systemActions";

interface IVisitorOutProps {
    visitors: IVisitor[]
    loading: boolean;
    error: string;
    siteId: string;
    selectedId: string;
    selectedName: string;
    dialogOpen: boolean;

    selectVisitor: (id: string, name: string) => void;
    confirmHandle: (id: string, name: string) => void;
    cancelHandle: () => void;
    loadVisitors: (siteId: string) => void;
}

class VisitorOut extends React.Component<IVisitorOutProps> {

    componentDidMount(): void {
        this.props.loadVisitors(this.props.siteId);
    }


    render() {

        const items = this.props.visitors.map((visitor, index) => {
            return <VisitorListItem selectVisitor={this.props.selectVisitor} visitor={visitor} key={index}/>
        });

        return (
            <React.Fragment>
                <VisitorConfirmationDialog id={this.props.selectedId} name={this.props.selectedName} open={this.props.dialogOpen} confirmHandle={this.props.confirmHandle} cancelHandle={this.props.cancelHandle}/>
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
            </div>\
        </React.Fragment>
        );
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        visitors: state.visitors.visitors,
        loading: state.visitors.loading,
        error: state.visitors.error,
        siteId: state.system.site.id,
        selectedId: state.visitors.selectedId,
        selectedName: state.visitors.selectedName,
        dialogOpen: state.visitors.dialog,
    }
};

const signOutVisitor = (dispatch: any, id: string, name: string) => {

    const removeOverlay = (): void => dispatch(updateOverlayAction(closeOverlay()));

    dispatch(updateOverlayAction(openOverlay("Signing you out", IconType.NONE, true)));
    signOut(id).then((id) => {
        operationsAggregator.listen(id,
        (op) => {
            dispatch(updateOverlayAction(openOverlay(`Goodbye, ${name} have a safe journey`, IconType.TICK)));
            setTimeout(() => {
                removeOverlay();
                dispatch(viewChangedAction(SystemViews.INIT_SIGN_IN));
            }, 1500);
        },
        (op: IOperation) => {
            updateOverlayAction(openOverlay(op.reason ?? "Oops something went wrong", IconType.ERROR, false, true));
            setTimeout(() => {
                removeOverlay();
            }, 1500);
        });
    });
};

const mapDispatchToProps = (dispatch: any) => {
    return {
        loadVisitors: (siteId: string) => dispatch(getSignedInVisitors(siteId)),
        confirmHandle: (id: string, name: string) => {
            dispatch(visitorSelectionConfirmed(true));
            signOutVisitor(dispatch,id, name);
        },
        cancelHandle: () => {
            dispatch(visitorSelectionCancelled(true))
        },
        selectVisitor: (id: string, name: string) => dispatch(updateSelectedVisitor({
            id: id,
            name: name
        }))
    }
};







export default  connect(mapStateToProps, mapDispatchToProps)(VisitorOut);