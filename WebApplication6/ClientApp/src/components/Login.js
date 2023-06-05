import { useState } from "react";
import { useNavigate } from 'react-router-dom'

Login.displayName = Login.name

export function Login() {

    const navigate =  useNavigate()
    
    const [username, setUsername] = useState()
    const [password, setPassword] = useState()

    async function onLogin() {
        const formData = new FormData()
        formData.append("email", username)
        formData.append("password", password)
        const response = await fetch("/Account/Login", {
            method: "POST",
            body: formData,
        })
        if (response.ok) {
            navigate('/')
        } else {
            alert('akaskjdkasjd')
        }
    }
    
    return <div>
        <h1>{Login.displayName}</h1>
        <div>
            Username: <input onChange={(e) => setUsername(e.target.value)}/>
        </div>
        <div>
            Password: <input onChange={(e) => setPassword(e.target.value)}/>
        </div>
        <button onClick={onLogin}>Login</button>
    </div>
}
