import React from 'react'
import { useSelector } from 'react-redux'
import { createMuiTheme, ThemeProvider } from '@material-ui/core'
import { blue } from '@material-ui/core/colors'


export default function ThemeWrapper(props) {

    const isdarkMode = useSelector(state => state.ui.isDark)

    const theme = createMuiTheme({
        palette: {
            primary: blue,
            type: isdarkMode ? "dark" : "light"
        }
    })

    return (
        <ThemeProvider theme={theme}>
            {props.children}
        </ThemeProvider>
    )
}
