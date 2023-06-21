# ~~Cookie~~ ~~Session~~ Stateful(?) Auth w/o Identity

1. Another name for this is "form-based" auth, not sure what's the best name to use
2. Some kind of "session identifier" is sent back from the server that is included in every client request (automatically), such an identifier can be "authenticated" (after a login) or "unauthenticated" (when someone haven't logged in yet)
3. Stateful solution as compared to basic authentication (the server stores whether users having a specific session ID have already logged-in and/or what kind of claims/authorization level they have)
4. Requires a custom login form of some sorts
5. The main drawback is that _each_ backend server has to know about an authenticated session (when doing load-balancing between multiple backend servers this can be problematic, but can be solved with various techniques: sticky sessions, session information stored in a central database instead of on servers "locally", etc.)
6. Kind of most supported and well-known basic way of doing logins
7. Can be tricky when dealing with cross-site requests (as in such scenarios cookies are not automatically sent from client to server by browsers - depends on the cookie and/or CORS settings, and this also affects security)

## Sample Projects

1. [MVC (`mvc-stateful-without-identity`)](mvc-stateful-without-identity/README.md)
2. [API (`api-stateful-without-identity`)](api-stateful-without-identity/README.md)
3. [SPA (`spa-stateful-without-identity`)](spa-stateful-without-identity/README.md)
