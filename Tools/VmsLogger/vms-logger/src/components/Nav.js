import React, { Component } from 'react'
import {fetchLogs} from '../actions/logsActions'
import {connect} from 'react-redux'




class Nav extends Component {    

    componentDidMount() {   
        this.loadLogs();     
        console.log(this.props);
    }    

    loadLogs() {        
        console.log("clicked");
        this.props.dispatch(fetchLogs());                    
    }    

    render() {   
        console.log(this.props)       ;
               return (            
                <header>                
                    <nav className="container navbar navbar-expand-lg navbar-light bg-light">
                        <span className="navbar-brand" >VMS Logger</span>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarNav">      
                            <button className="btn btn-default" onClick={() => this.loadLogs()}>Refresh</button>                      
                        </div>
                    </nav>
                </header>         
        )
    }
}

const mapStateToProps = (state) => {    
    console.log(state);
    return {logs: state.logs.logs, fetching: state.logs.fetching}
    
}


export default connect(mapStateToProps)(Nav);