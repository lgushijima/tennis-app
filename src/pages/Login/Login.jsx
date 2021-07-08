import React from "react"
import { useAuth } from "../../contexts/auth"

export default function Login() {
    const { signIn } = useAuth()

    function handleSignIn() {
        signIn()
    }

    return (
        <div>
            <h2>Login</h2>
            <button onClick={handleSignIn}>SignIn</button>
        </div>
    )
}
