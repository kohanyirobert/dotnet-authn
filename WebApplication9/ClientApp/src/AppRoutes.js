import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { UserSecret } from "./components/UserSecret";
import { AdminSecret } from "./components/AdminSecret";
import { Login } from "./components/Login";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/Login',
    element: <Login />
  },
  {
    path: '/UserSecret',
    element: <UserSecret />
  },
  {
    path: '/AdminSecret',
    element: <AdminSecret />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
