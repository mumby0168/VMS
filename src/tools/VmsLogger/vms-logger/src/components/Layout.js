import React, { Component } from 'react'
import Nav from './Nav'
import {connect} from 'react-redux'
import LogsList from './LogsList'

class Layout extends Component {
    render() {
        return (
            <div class="std">
                <Nav></Nav>
                <div className="app-container">                    
                    <div className="row">                                                
                    </div>                    
                        <LogsList></LogsList>
                </div>
            </div>
        )
    }
}

export default connect()(Layout);
