import React from "react"
import { useAuth } from "../../contexts/auth"

import style from "../../assets/css/pages/_login.scss"

export default function Login() {
    const { signIn } = useAuth()

    function handleSignIn() {
        signIn()
    }

    return (
        <div>
            <h2 className={style.bazinga}>Login</h2>
            <button onClick={handleSignIn}>SignIn</button>
        </div>
    )
}
