import React from "react"
import { useAuth } from "../contexts/auth"

import AppRoutes from "./app.routes"
import AuthRoutes from "./auth.routes"

const Routes = () => {
    const { isAuthed, loading } = useAuth()

    if (loading) {
        return <div>Loading</div>
    }

    return isAuthed ? <AppRoutes /> : <AuthRoutes />
}
export default Routes
