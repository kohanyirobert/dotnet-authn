# Basic Auth w/o Identity

1. [No bueno for out-of-the-box solutions](https://stackoverflow.com/a/35300866/433835)
2. [Custom solution](https://dotnetthoughts.net/implementing-basic-authentication-in-minimal-webapi/) is pluggable via adding a middleware
3. However there's no default/easy way to specify which endpoints require basic authentication
4. There is no _logout_ for basic authentication
    1. There [seems to be a hack that _doesn't work_](https://stackoverflow.com/a/19258791/433835)? 🤔
5. Login method is built into browsers, there's no way to customize it (I lied, because when using React or other SPA frameworks you're doing just that)
6. This is a _stateless_ authentication format (every request contains authentication information, but contains no _authorization_ information - authorization stuff is handled on the server making that _stateful_)
7. Fun fact, [BambooHR for example uses basic authentication for its API](https://documentation.bamboohr.com/docs#authentication), although an API key needs to be sent over the wire instead of a user/password combination

## Sample Projects

1. [MVC (`mvc-basic-without-identity`)](mvc-basic-without-identity/README.md)
2. [API (`api-basic-without-identity`)](api-basic-without-identity/README.md)
3. [SPA (`spa-basic-without-identity`)](spa-basic-without-identity/README.md)
