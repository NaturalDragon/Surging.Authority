import react from 'react'

export default class PlatFormA extends react.Component {

    render() {
        // eslint-disable-next-line react/react-in-jsx-scope
        return (<a {...this.props}></a>)
    }
}