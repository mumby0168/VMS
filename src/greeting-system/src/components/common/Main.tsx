import React from 'react'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {
    public render() {
        return (
            <div>
                {this.props.children}
            </div>
        )
    }
}