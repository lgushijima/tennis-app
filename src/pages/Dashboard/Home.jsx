import React from "react"
import { useAuth } from "../../contexts/auth"

export default function Dashboard() {
    const { user } = useAuth()
    return (
        <div>
            <h2>Home - {user?.name}</h2>
        </div>
    )
}
