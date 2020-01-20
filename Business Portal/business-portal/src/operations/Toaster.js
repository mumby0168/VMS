import React, { Component } from 'react'
import { connect } from 'react-redux'

class Toaster extends Component {

    


    render() {

        console.log("TOASTER");

        const messages = this.props.messages.map((message) => {
            console.log(message);
            return <h1>{message.message}</h1>
        })
        return (
            <div>
                {messages}
            </div>
        )
    }
}

const mapStateToProps = (state => {
    return {
        messages: state.toast.messages
    }
});

export default connect(mapStateToProps)(Toaster);
