import React, { useEffect, useState, useContext, createContext } from "react"
import * as authService from "../services/auth"
import httpClient from "../services/api"

const AuthContext = createContext({})

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null)
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        async function loadStorageData() {
            const storagedUser = localStorage.getItem("@webapp:user")
            const storagedToken = localStorage.getItem("@webapp:token")

            await new Promise((resolve) => setTimeout(resolve, 2000))

            if (storagedUser && storagedToken) {
                setToken(storagedToken)
                setUser(storagedUser)
            }
            setLoading(false)
        }

        loadStorageData()
    }, [])

    async function signIn() {
        const response = await authService.SignIn()

        setToken(response.token)
        setUser(response.user)

        localStorage.setItem("@webapp:user", JSON.stringify(response.user))
        localStorage.setItem("@webapp:token", JSON.stringify(response.token))
    }

    async function signOut() {
        setUser(null)
        localStorage.removeItem("@webapp:user")
        localStorage.removeItem("@webapp:token")
    }

    function setToken(jwtToken) {
        httpClient.defaults.headers["Authorization"] = `Bearer ${jwtToken}`
    }

    return (
        <AuthContext.Provider value={{ isAuthed: !!user, user, loading, signIn, signOut }}>
            {children}
        </AuthContext.Provider>
    )
}

//-- this was the default context - but to avoid importing this context everywere, we can create a HOOK, like below
//export default AuthContext

//-- Hook
export function useAuth() {
    const context = useContext(AuthContext)
    return context
}
