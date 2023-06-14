import { useState } from "react";
import { useNavigate } from 'react-router-dom'

Login.displayName = Login.name

export function Login() {

    const navigate =  useNavigate()
    
    const [username, setUsername] = useState()
    const [password, setPassword] = useState()

    function onLogin() {
        localStorage.setItem('username', username)
        localStorage.setItem('password', password)
        navigate('/')
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
