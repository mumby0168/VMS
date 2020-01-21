import React, { Component } from 'react'
import { connect } from 'react-redux'

class Visitors extends Component {
    render() {
        return (
            <div>
                Visitors
            </div>
        )
    }
}


export default connect()(Visitors)
