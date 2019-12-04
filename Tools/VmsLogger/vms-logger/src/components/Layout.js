import React, { Component } from 'react'
import Nav from './Nav'
import {connect} from 'react-redux'

class Layout extends Component {
    render() {
        return (
            <div>
                <Nav></Nav>
                <div className="container">                    
                    <div className="row">                        
                        <h1>Logs</h1>
                    </div>
                </div>
            </div>
        )
    }
}

export default connect()(Layout);
