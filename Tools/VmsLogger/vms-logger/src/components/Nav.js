import React, { Component } from 'react'
import {fetchLogs, purge} from '../actions/logsActions'
import { connect } from 'react-redux';
import { delay } from 'q';




class Nav extends Component {    

    componentDidMount() {   
        this.loadLogs();             
    }    

    loadLogs() {                
        this.props.dispatch(fetchLogs());                    
    }    

    render() {   
        console.log(this.props)       ;
               return (            
                <header>                
                    <nav className="navbar navbar-expand-sm navbar-light bg-light">
                        <span className="navbar-brand" >VMS Logger</span>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarNav">      
                            <button className="btn btn-default" onClick={() => this.loadLogs()}>Refresh</button>        
                            <button className="btn btn-default" onClick={() => this.props.dispatch(purge())}>Purge</button>                      
                        </div>
                    </nav>
                </header>         
        )
    }
}


export default connect()(Nav);