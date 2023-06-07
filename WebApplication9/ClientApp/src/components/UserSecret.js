import {useEffect, useState} from "react";

UserSecret.displayName = UserSecret.name

export function UserSecret() {

    const [text, setText] = useState()

    useEffect(() => {
        async function getIndex() {
            const response = await fetch("/Home/UserSecret", {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                }
            })
            setText(response.ok ? await response.text() : response.statusText)
        }

        getIndex()
    })

    return <div>
        <h1>{UserSecret.displayName}</h1>
        <p>Fetching text from <code>/Home/UserSecret</code>: <span>{text ? text : "..."}</span></p>
    </div>
}
