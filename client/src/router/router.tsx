import { createBrowserRouter, redirect, RouteObject } from "react-router-dom";
import App from "../App.tsx";
import { VacancyListPage } from "../pages/vacancy/vacancy.list.page.tsx";
import RegisterPage from "../pages/auth/register.page.tsx";
import LoginPage from "../pages/auth/login.page.tsx";
import ProfilePage from "../pages/profile/profile.page.tsx";
import { CandidateList } from "../pages/candidates/candidates.list.tsx";
import { CandidatePage } from "../pages/candidates/candidate.page.tsx";
import { VacancyPage } from "../pages/vacancy/vacancy.page.tsx";
import { VacancyCreatePage } from "../pages/vacancy/vacancy.create.page.tsx";

const rootLoader = async () => {
    const token = localStorage.getItem("token");
    if (!token) {
        return redirect("/login");
    }
    return null;
};

const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        loader: rootLoader,
        children: [
            {
                path: "/vacancy",
                element: <VacancyListPage />
            },
            {
                path: "/vacancy/:id",
                element: <VacancyPage />
            },
            {
                path: "/vacancy/create",
                element: <VacancyCreatePage />
            },
            {
                path: "/candidate",
                element: <CandidateList />
            },
            {
                path: "/candidate/:id",
                element: <CandidatePage />
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