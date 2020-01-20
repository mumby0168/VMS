import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import FireListHeader from '../components/fire/FireListHeader'
import { updateTime, getSiteFireList } from '../actions/fireListActions';
import FireList from '../components/fire/FireList';

class Fire extends Component {

    componentDidMount() {
        this.updateTime();
        this.props.dispatch(getSiteFireList(this.props.preferedSite));
    }


    updateTime() {
        var d = new Date();
        var time = d.toLocaleTimeString();
        this.props.dispatch(updateTime(time));
    }

    updateSelectedSite(id) {
        this.props.dispatch(getSiteFireList(id));
    }



    render() {
        return (
            <div>
                <FireListHeader updateTime={this.updateTime.bind(this)} updateHandle={this.updateSelectedSite.bind(this)} summaries={this.props.summaries} selected={this.props.selected.id} lastUpdated={this.props.lastUpdated}/>
                <FireList loading={this.props.loading} users={this.props.selected.users}></FireList>
            </div>
        )
    }
}


const mapStateToProps = (state) => {
    return {
        preferedSite: state.user.basedSiteId,        
        summaries: state.site.summaries,
        lastUpdated: state.fire.lastUpdated,
        selected: state.fire.site,
        loading: state.fire.loading
    }
}

export default withRouter(connect(mapStateToProps)(Fire))
