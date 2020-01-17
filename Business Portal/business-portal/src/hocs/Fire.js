import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import FireListHeader from '../components/fire/FireListHeader'
import { updateTime, getSiteFireList } from '../actions/fireListActions';
import FireList from '../components/fire/FireList';

class Fire extends Component {

    componentDidMount() {
        var d = new Date();
        var time = d.toLocaleTimeString();
        this.props.dispatch(updateTime(time));
        this.props.dispatch(getSiteFireList(this.props.preferedSite));
    }

    updateSelectedSite(id) {
        this.props.dispatch(getSiteFireList(id));
    }



    render() {
        return (
            <div>
                <FireListHeader updateHandle={this.updateSelectedSite.bind(this)} summaries={this.props.summaries} selected={this.props.selected.id} lastUpdated={this.props.lastUpdated}/>
                <FireList users={this.props.selected.users}></FireList>
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
    }
}

export default withRouter(connect(mapStateToProps)(Fire))
