import {useEffect, useState} from "react";

AdminSecret.displayName = AdminSecret.name

export function AdminSecret() {

    const [text, setText] = useState()

    useEffect(() => {
        async function getIndex() {
            const response = await fetch("/Home/AdminSecret", {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                }
            })
            setText(response.ok ? await response.text() : response.statusText)
        }

        getIndex()
    })

    return <div>
        <h1>{AdminSecret.displayName}</h1>
        <p>Fetching text from <code>/Home/AdminSecret</code>: <span>{text ? text : "..."}</span></p>
    </div>
}
