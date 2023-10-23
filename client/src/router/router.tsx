import {createBrowserRouter, RouteObject} from "react-router-dom";
import App from "../App.tsx";
import {VacancyPage} from "../pages/vacancy/vacancy.page.tsx";
import LoginPage from "../pages/auth/login.page.tsx";
import RegisterPage from "../pages/auth/register.page.tsx";

const routes : RouteObject[] = [{
    path: "/",
    element: <App />,
    children:[
        {
            path: "/vacancy",
            element: <VacancyPage />
        },
        {
            path: "/profile",
            element: <div>profile</div>
        },
        {
            path: "/login",
            element: <LoginPage />
        },
        {
            path: "/register",
            element: <RegisterPage />
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