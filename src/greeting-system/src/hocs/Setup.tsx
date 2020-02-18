import { IAppState } from "../redux/store";
import React from "react";
import { connect } from "react-redux";
import { Card, CardHeader, CardContent, Grid } from '@material-ui/core'
import LoginForm from "../components/setup/LoginForm";
import '../hoc-styles/Setup.css'
import { login } from "../redux/api/identity";
import { ISetupForm, loginFormUpdate, loginRejectedAction, siteSelectionConfirmed, siteSelectionChangedAction } from "../redux/actions/setupActions";
import { Loader } from "../components/common/Loader";
import { IKeyValuePair } from "../redux/common/types";
import { SiteSelect } from "../components/setup/SiteSelect";
import { Redirect } from "react-router";
import { getSites, getSiteInfo } from "../redux/api/sites";


interface ISetupProps {
    online: boolean;
    login(code: string, email: string, password: string): void,
    updateForm(data: ISetupForm): void,
    formData: ISetupForm,
    error: string,
    loading: boolean;
    updateFormError(message: string): void;
    sites: IKeyValuePair[];
    selectedSite: IKeyValuePair;
    siteConfirmed: boolean;
    confirmHandle(): void;
    selectionChangedHandle(choice: IKeyValuePair): void;
    loadSites(businessId: string): void;
    businessId: string;
    fetchSite(siteId: string): void;
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
        sites: state.setup.sites,
        selectedSite: state.setup.selectedSite,
        siteConfirmed: state.setup.siteConfirmed,
        businessId: state.system.token.businessId
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        login: (code: string, email: string, password: string) => dispatch(login(code, email, password)),
        updateForm: (data: ISetupForm) => dispatch(loginFormUpdate(data)),
        updateFormError: (message: string) => dispatch(loginRejectedAction({Code: 'validation', Reason: message})),
        confirmHandle: () => dispatch(siteSelectionConfirmed(true)),
        selectionChangedHandle: (c: IKeyValuePair) => dispatch(siteSelectionChangedAction(c)),
        loadSites: (bid: string) => dispatch(getSites(bid)),
        fetchSite: (siteId: string) => dispatch(getSiteInfo(siteId)),
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

        if(this.props.siteConfirmed) {
            this.props.fetchSite(this.props.selectedSite.key);
            return <Redirect to='/main'></Redirect>
        }       

        var title = "Please login to setup account"


        var content = this.props.loading ? <Loader message="Logging you in ..."/> : 
        <LoginForm error={this.props.error} login={this.login.bind(this)} formData={this.props.formData} updateForm={this.props.updateForm}/>

        if(this.props.online) {
            if(this.props.sites.length === 0) {
                console.log('load sites.')
                this.props.loadSites(this.props.businessId);                
            }

            content =  <SiteSelect confirmHandle={this.props.confirmHandle} selectionChangedHandle={this.props.selectionChangedHandle} sites={this.props.sites} selected={this.props.selectedSite} />
            title = "Please select a site"
        }

        return (
            <Grid style={{height: '100%'}} alignItems="center" container className="center background full-height">
                <Grid item md={4}></Grid>
                <Grid item md={4}>
                <Card className="card">
                    <CardHeader title={title}></CardHeader>
                    <CardContent>
                        {content}
                    </CardContent>                    
                </Card>
                </Grid>
                <Grid item
                 md={4}></Grid>
            </Grid>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Setup)

