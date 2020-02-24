import React, { Component } from 'react'
import { IAppState } from '../redux/store'
import { connect } from 'react-redux'
import { IVisitorDataSpecification } from '../redux/actions/visitorFormActions'

interface IVisitorFormProps {
    specifications: IVisitorDataSpecification[],
    errors: string[],
    loading: boolean;
}

class VisitorForm extends Component<IVisitorFormProps> {    

    componentDidMount() {
        //TODO: Load form spec from API
        console.log(this.props)
    }

    render() {
        return (
            <div>
                    <h1>Visitor Form</h1>
            </div>
        )
    }
}


const mapStateToProps = (state: IAppState)  => {
    return {
        specifications: state.visitorForm.fields,
        errors: state.visitorForm.errors,
        loading: state.visitorForm.loading,
    }   
}

const mapDispatch = (dispatch: any) => {
    return {

    }
}

export default connect(mapStateToProps)(VisitorForm);
