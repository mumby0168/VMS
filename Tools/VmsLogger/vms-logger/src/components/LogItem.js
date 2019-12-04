import React, { Component } from 'react'

export default class LogItem extends Component {

    getBadgeColour = () => {
        switch(this.props.log.type.toLowerCase()) {
            case "info":
                return "badge-success";
            case "warning":
                return "badge-warnining";
            case "error":
                return "badge-danger";
            case "trace":
                return "bagde-info"
            default:
                return "";
        }
    }
    render() {        

        var className = "badge".concat(" ", this.getBadgeColour())


        return (
            <li className="list-group-item log-item">
                <div className="row">
                    <div className="col-md-1">
                        <p>{this.props.log.date} {this.props.log.time}</p>                                                
                    </div>
                    <div className="col-md-1">
                        <span className={className}>{this.props.log.type}</span>
                    </div>
                    <div className="col-md-2">
                        <p>{this.props.log.service}</p>
                    </div>
                    <div className="col-md-1">
                        <p>{this.props.log.category}</p>
                    </div>
                    <div className="col-md-5">
                        <p>{this.props.log.message}</p>
                    </div>

                </div>
            </li>
        )
    }
}
