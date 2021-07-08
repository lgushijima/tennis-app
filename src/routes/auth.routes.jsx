import React from "react"
import Login from "../pages/Login/Login"
import ForgotPassword from "../pages/Login/ForgotPassword"
import { Switch, Redirect, Route } from "react-router-dom"

const AuthRoutes = () => {
    return (
        <Switch>
            <Route path="/Login" component={Login} />
            <Route path="/ForgotPassword" component={ForgotPassword} />

            <Route
                path="/"
                render={(props) => {
                    const requestedUrl = encodeURIComponent(props.location.pathname)
                    return (
                        <Redirect
                            to={{
                                pathname: "/Login",
                                search: `?returnUrl=${requestedUrl}`,
                            }}
                        />
                    )
                }}
            />
        </Switch>
    )
}

export default AuthRoutes
