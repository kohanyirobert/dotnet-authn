import {useEffect, useState} from "react";

AdminSecret.displayName = AdminSecret.name

export function AdminSecret() {

    const [text, setText] = useState()

    useEffect(() => {
        async function getIndex() {
            try {
                const response = await fetch("/Home/AdminSecret", {
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
        <h1>{AdminSecret.displayName}</h1>
        <p>Fetching text from <code>/Home/AdminSecret</code>: <span>{text ? text : "..."}</span></p>
    </div>
}
