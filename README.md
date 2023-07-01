# ASP.NET Web AuthN

Samples and notes of different authentication techniques demonstrated with ASP.NET Core 7 sample projects.

## Prequel

### Goals

- Have examples of different authentication techniques
- Build upon builtin `dotnet` project templates
- See how things work with and without _Identity_
- Learn!

### Non-goals

- Be super-duper focused on what is _the_ most secure way of doing things (_it depends_)
- To create setups for same-site/cross-site/secure/insecure/etc. cookie scenarios in CORS/no-CORS settings

## First Things First

A quick refresher about .NET for everyone

- .NET _Framework_ is the past
- .NET _Core_ is _kind of_ the past also, but still relevant to some extent
- .NET is the new and shiny thing

About ASP.NET

- ASP.NET is old news
- ASP.NET Core is the new shiny thing

### .NET

With **_.NET_** and **_ASP.NET Core_** one can write

- "regular" server-side rendered apps with MVC (using Razor as the backend templating language)
- use Razor Pages, which is
    1. a different way of organizing MVC apps
    2. also it is referred to as being MVVM (but I didn't find any evidence of this in any Microsoft docs)
    3. it _has_ data-binding support as opposed to MVC; something similar to what WPF has, but WPF has real two-way data-binding, Razor Pages does _not_ ... so WPF is MVVM, Razor Pages are somewhat similar (but a fact is _I_ personally have no idea what MVVM is 😁)
- thirdly [Blazor](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/modern-web-applications-characteristics#blazor) which is _writing server- and/or client-side rendered webapps using .NET languages (C#) and Razor_

    1. server-side Blazor is what it sounds like, but it's leverages SignalR (which is a layer on top WebSockets or as a fallback HTTP most probably) **to only send DOM _changes_ over a SignalR connection**, the other main difference being a _component-oriented approach_ (Blazor and/or Razor components - I'm unsure, this is pretty confusing 😵)
    2. client-side Blazor is trickier as it uses WebAssembly to _run .NET code on the client_ 😲 so it's client-side C# essentially, but the goal is the same - small changes to the DOM instead of _big ass complete page reloads_, aka more responsiveness
- and lastly using a (Web) API project without any _built-in Microsoft-endorsed UI solution_ using
    1. React
    2. Angular
    3. _insert another popular SPA framework here_

[This sums it up very nicely.](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/modern-web-applications-characteristics#traditional-and-spa-behaviors-supported).

### .NET Framework

With **_.NET Framework and ASP.NET_** (watch out, no Core or anything) one could

- still write web apps in _older ways_, most notably using WebForms which uses (or used at a point in history) non-standard web "stuff" and was left behind; it still works, people know it, use it, just as with WinForms
- and probably this framework also has something akin to a Web API and/or Restful project archetype that was left behind and completely replaced with the advent of .NET Core/.NET and Web API projects

### Authentication vs Authorization

[This is what it is](https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-7.0#authentication-vs-authorization), but it is crucial to _get_ certain aspects of what's outlined further in the document.

### ASP.NET Core Identity

This is an [API shipped with/as an extension for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0) which is kind of hard to miss in the ecosystem.
It pretty much always comes up when dealing with authentication in .NET.
It supposed to _help_ things, but this isn't all that obvious.
In order to tackle how to best leverage it let's focus on it in the examples. 

## What now?

### Project types

Let's settle on the kind of project type from the above selection to focus on

- ASP.NET Core MVC with Razor (but not [Razor Pages](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/develop-asp-net-core-mvc-apps#mvc-and-razor-pages))
- ASP.NET Web API with React

Coincidentally these two are the ones _I_ personally know the most about at this point in time 🎉

### Authentication Types

I personally seen the following authentication types/formats while working in the IT industry

1. Basic authentication
2. Cookie authentication using forms
3. JWT token-based authentication using the `Bearer` scheme
4. OAuth2, OICD and _friends_

There may be several others, and even the ones I write may not be real "types" of authentication methods.
I'll focus on the first three, because OAuth2, OICD, etc. are on a whole other complexity level, maybe in a follow up project similar to this.

## Sample Projects

Based on what's above the following "matrix' can be deducted for better or worse.

1. _Not_ using ASP.NET Core Identity
   1. [Basic Auth w/o Identity](without-identity/basic/README.md)
      1. [MVC (`mvc-basic-without-identity`)](without-identity/basic/mvc-basic-without-identity/README.md)
      2. [API (`api-basic-without-identity`)](without-identity/basic/api-basic-without-identity/README.md)
      3. [SPA (`spa-basic-without-identity`)](without-identity/basic/spa-basic-without-identity/README.md)
   2. [~~Cookie~~ ~~Session~~ Stateful(?) Auth w/o Identity](without-identity/stateful/README.md)
      1. [MVC (`mvc-stateful-without-identity`)](without-identity/stateful/mvc-stateful-without-identity/README.md)
      2. [API (`api-stateful-without-identity`)](without-identity/stateful/api-stateful-without-identity/README.md)
      3. [API + React (`spa-stateful-without-identity`)](without-identity/stateful/spa-stateful-without-identity/README.md)
   3. ~~JWT~~ Stateless(?) Auth w/o Identity
      1. MVC (`mvc-stateless-without-identity`)
      2. API (`api-stateless-without-identity`)
      3. API + React (`spa-stateless-without-identity`)
2. _Using_ ASP.NET Core Identity
   1. Basic Auth /w Identity
      1. MVC (`mvc-basic-with-identity`)
      2. API (`api-basic-with-identity`)
      3. API + React (`spa-basic-with-identity`)
   2. ~~Cookie~~ ~~Session~~ Stateful(?) Auth /w Identity
      1. MVC (`mvc-stateful-with-identity`)
      2. API (`api-stateful-with-identity`)
      3. API + React (`spa-stateful-with-identity`)
   3. ~~JWT~~ Stateless(?) Auth /w Identity
      1. MVC (`mvc-stateless-with-identity`)
      2. API (`api-stateless-with-identity`)
      3. API + React (`spa-stateless-with-identity`)

This is kind of an simplification of things and won't always be super logical and straightforward, but it's okay.
Some combination of things doesn't really work, I may skip those without prior warning.

TODO: what comes below will be restructured.

## _No_ Identity

### ~~JWT?~~Stateless? Auth w/o Identity

- JWT is actually just something that travels around the wire, a different kind of "session identifier", it's not a protocol

- E.g. one could use OAuth2 with JWT or something else, _and_ JWT tokens can travel as _cookies_ between client and server

- It's important to note that using cookies with JWT makes it a _simple stateless_ _authentication_ solution

- After a client receives a JWT it can be then used to:

    - authentication (just as basic authentication)
    - authorization (the token can encapsulate authorization details, _claims_ of what the holder of the token can do in a system)
    - as it is digitally signed it can be passed around _freely_
    - this comes at a price: a token shouldn't be valid forever, it is usually valid for a specific amount of time after it needs to _refreshed_, which fact in itself present several headaches :)

- **Here we won't use OAuth2 or anything as that's a complex thing, a _framework_ for authentication (with authorization servers and resource servers and _whatnot_)**


#### WebApplication7 = MVC

- First and most important: **JWT is supported via a first-party library**

    - add it via _dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer_
    - and/or via some GUI functionality

- On a side note: NuGet dependencies are listed as follows in .NET Rider

    - ![](https://lh5.googleusercontent.com/PiSqBmHbGIhZ4FPWrP0-w0egnGFqsQt43ybmwW_-kG7hUFkzGKQ4mqHDg9bCi4Em_tEQ1ZcPyzs9Hz04aA8pd4W3o21FtMYu9Cy6YG5KatLnLK7babctRk45nisbs0jdmKkicZKyHty4BXLDI0Sfjg)


- The MVC GUI is useless here, since by default _AddJwtBearer_ configures the system in a way that the ASP.NET server expects the token via the Authorization header and this cannot be made available via forms, or clicking on links

- However, [based on this guide](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-7.0&tabs=windows) one can generate JWT tokens (for development - in a production scenario one would get such tokens from authorization server)

- One can create such tokens with commands like

    - _dotnet user-jwts create –role Admin_
    - Different roles or claims can be encapsulated into the claim and this is respected by the _\[Authorize] attribute_

- And after test the token like this

    - _curl -ivH "Authorization: Bearer $(dotnet user-jwts print -o token 9e0e57a1)" <http://localhost:5289/Home/UserSecret>_
    - Where the token's ID can be different


#### WebApplication8 = API

- This project example, without a GUI would be same as the previous _WebApplication7_ so I'll skip it for now


#### WebApplication9 = API + React

- The backend would look like as with _WebApplication8_

- However, since in React we could handle _anything_ we can build a custom login form that would _asynchronously_

    - acquire a JWT token after a successful login
    - store that in localStorage (_not secure at all_)
    - use the token stored in localStorage in subsequent API calls

- **Here the user-jwts _could_ work like instead having a working "login" we could just create a localStorage entry manually and set the token obtained from the command like, but that's not very _cool_**

- **So let's create a security token server/service or STS _embedded_ into the backend**

- Note: in the _AccountController_ the _FromBody_ attribute is used on the method parameters instead of _FromForm_

- Validation works by default _iff_ a _proper_ token received by the backend and user secrets are correctly in place


#### WebApplication9b = API + React

- Same thing as before just with using cookies to transmit token between client and server instead of the _Authorization_ header and the _Bearer_ keyword

- **No new project just a different commit (I should have done things like this since earlier)**

- This version is probably really picky when dealing with a cross-site request scenario

    - Maybe demo this by getting the JWT cookie and making call in the browser's console to a 3rd party endpoint (something like the Pokemon API)


## _With_ Identity


### Basic Auth w/ Identity


#### WebApplication10 = MVC

![](https://lh3.googleusercontent.com/7hGtiMr3dPOj_miRFHEv0muF6hI5Dr5igb0KhWrmcUw8dVnsvMq4lekl9p3biM40NozVX6sJ2HFW_-ql3RSjc8KZ6v_NaADIJyD_LG0kvZZdehlZ4mUfajj_z5eLJ0eqn4o_9NzNFDWJTZc33-MDhA)

- Even though there's no _real_ login as the credentials need to be present on the client side Identity still could be used for validating login credentials and registration

- Identity's default UI is tuned towards _form_-based login, which basic authentication is not suited for

    - [The default UI and endpoints for Identity are broken out from userland code since .NET Core 2.1 onwards](https://stackoverflow.com/questions/50802781/where-are-the-login-and-register-pages-in-an-aspnet-core-scaffolded-app)
    - To modify the defaults one could [leverage scaffolding](https://blog.jetbrains.com/dotnet/2021/03/18/scaffolding-for-asp-net-core-projects-comes-to-rider-2021-1/) and _bring_ all that code to a project (selectively)
    - To see how defaults work one could [take a look at the source code as well](https://github.com/dotnet/aspnetcore/tree/main/src/Identity/UI/src/Areas/Identity)

- As we don't want to use cookie-based authentication we don't need _AddDefaultIdentity_

    - On a side-note, there is [_AddDefaultIdentity, AddIdentity_ and _AddIdentityCore_](https://stackoverflow.com/questions/55361533/addidentity-vs-addidentitycore), here we'll need the latter with some extras

- When using _"Individual authentication"_ the _MapRazorPages_ call is added to _Program.cs_, we don't need that

- When using _AddIdentityCore_ no _SignInManager_ is configured by default

- Also need to add _AddRoles_ to be able fiddle with roles

- By default "strong" passwords are used, this is turned off in the options to _AddIdentityCore_

- _AddAuthentication needs to be configured as with WebApplication1, **the main difference is in how BasicAuthenticationHandler uses SignInManager to use the underlying database**_


#### WebApplication11 = API

- Skipping it, would work as _WebApplication11_ just without the default MVC index page, etc.


#### WebApplication12 = API + React

- Again, same as _WebApplication11_ just with React, see _WebApplication3_ for details


### ~~Cookie?Session?~~Stateful? Auth w/ Identity


#### WebApplication13 = MVC

- This is the _defacto_ default way of using Identity, defaults works like a charm


#### WebApplication14 = API


#### WebApplication15 = API + React


### ~~JWT?~~Stateless? Auth w/ Identity


#### WebApplication16 = MVC


#### WebApplication17 = API


#### WebApplication18 = API + React


## Miscellaneous stuff

- [AuthN vs AuthZ](https://www.cloudflare.com/learning/access-management/authn-vs-authz/)
- [Very good article about authentication schemes in .NET](https://matteosonoio.it/aspnet-core-authentication-schemes/), very insightful
- About [authorization flows](https://learn.microsoft.com/en-us/azure/active-directory/develop/security-tokens#authorization-flows-and-authentication-codes)

- [What user-jwts does](https://www.sobyte.net/post/2023-05/asp-dotnet--user-jwts/) in behind the scenes

- [Using secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows) in .NET projects

- [Generating JWT tokens](https://github.com/davidfowl/TodoApi/blob/0acf81c169b26747b3646c013442264664ddde91/TodoApi/Authentication/TokenService.cs#L32) similar to what user-jwts does

    - Written by a core committer for ASP.NET Core

- The actual way user-jwts create tokens

    - [Getting the signing keys](https://github.com/dotnet/aspnetcore/blob/110522a9160f910447e759e2048edc33f8eb9266/src/Tools/dotnet-user-jwts/src/Helpers/SigningKeysHandler.cs)
    - [Creating the token](https://github.com/dotnet/aspnetcore/blob/110522a9160f910447e759e2048edc33f8eb9266/src/Tools/dotnet-user-jwts/src/Helpers/JwtIssuer.cs#L60)

- Accessing a user's email address in a protected route

    - _HttpContext.User.FindFirst(ClaimTypes.Email).Value_
    - Name can be accessed more easily

- [Useful approach to debug middlewares](https://andrewlock.net/understanding-your-middleware-pipeline-in-dotnet-6-with-the-middleware-analysis-package/)/see what is happening in what order

- ASP.NET Core

    - [_Minimal API_](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio)
    - _[MInimal API AuthN](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0)_

- [Full blown _IdentityServer_](https://github.com/duendesoftware) (not related to _Identity Core (commercial)_

- [Post about the benefits and drawbacks of stateful and stateless authn](https://developer.okta.com/blog/2022/02/08/cookies-vs-tokens#when-to-use-cookies-or-tokens)

- [Long-winded GitHub issue](https://github.com/dotnet/aspnetcore/issues/42158) about why JWTs suck for SPAs and other tidbits

- [TodoApi](https://github.com/davidfowl/TodoApi), a sample project by a core ASP.NET Core committer

    - Uses the [BFF pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/backends-for-frontends)
