import {useEffect, useState} from "react";

UserSecret.displayName = UserSecret.name

export function UserSecret() {

    const [text, setText] = useState()

    useEffect(() => {
        async function getIndex() {
            try {
                const response = await fetch("/Home/UserSecret", {
                    redirect: 'error',
                })
                setText(response.ok ? await response.text() : response.statusText)
            } catch (e) {
                setText(e.message)
            }
        }

        getIndex()
    })

    return <div>
        <h1>{UserSecret.displayName}</h1>
        <p>Fetching text from <code>/Home/UserSecret</code>: <span>{text ? text : "..."}</span></p>
    </div>
}
