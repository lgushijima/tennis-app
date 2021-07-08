import React from "react"
import Dashboard from "../pages/Dashboard/Dashboard"
import Home from "../pages/Dashboard/Home"
import { Switch, Redirect, Route } from "react-router-dom"

const AppRoutes = () => {
    return (
        <Switch>
            <Route path="/Dashboard" exact component={Dashboard} />
            <Route path="/Home" exact component={Home} />

            <Route
                path="/"
                render={(props) => {
                    let returnUrl = ""

                    if (props.location.search) {
                        const params = new URLSearchParams(props.location.search)
                        returnUrl = params.get("returnUrl")
                        return <Redirect to={returnUrl} />
                    }

                    return <Redirect to="/Dashboard" />
                }}
            />
        </Switch>
    )
}

export default AppRoutes
