import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App.tsx";
import { VacancyPage } from "../pages/vacancy/vacancy.page.tsx";
import RegisterPage from "../pages/auth/register.page.tsx";
import LoginPage from "../pages/auth/login.page.tsx";
import ProfilePage from "../pages/profile/profile.page.tsx";

const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "/vacancy",
                element: <VacancyPage />
            },
            {
                path: "/candidate",
                element: <div>candidate</div>
            },
            {
                path: "/profile",
                element: <ProfilePage />
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
    },
    {
        path: "/login",
        element: <LoginPage />
    },
    {
        path: "/register",
        element: <RegisterPage />
    },
]

export const router = createBrowserRouter(routes);