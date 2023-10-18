import {createBrowserRouter, RouteObject} from "react-router-dom";
import App from "../App.tsx";

const routes : RouteObject[] = [{
    path: "/",
    element: <App />,
    children:[
        {
            path: "/vacancy",
            element: <div>vacancy</div>
        },
        {
            path: "/profile",
            element: <div>profile</div>
        },
        {
            path: "/login",
            element: <div>login</div>
        },
        {
            path: "/register",
            element: <div>register</div>
        },
        {
            path: "/salaries",
            element: <div>salaries</div>
        },
        {
            path: "/offers",
            element: <div>offers</div>
        }
    ]
}]

export const router = createBrowserRouter(routes);