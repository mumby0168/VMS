import React, { Component } from 'react'
import { connect } from 'react-redux'

class Employees extends Component {
    render() {
        return (
            <div>
                Employees
            </div>
        )
    }
}


export default connect()(Employees)
