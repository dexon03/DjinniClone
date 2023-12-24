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
import { RecruiterVacanciesList } from "../pages/vacancy/vacancy.recruiter.list.tsx";
import { VacancyUpdatePage } from "../pages/vacancy/vacancy.update.page.tsx";
import { UserList } from "../pages/users/user.list.tsx";
import { UserEdit } from "../pages/users/user.edit.tsx";
import { StatisticPage } from "../pages/statistics/statistic.page.tsx";
import ChatList from "../pages/chat/chat.list.page.tsx";

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
                path: "/vacancy/myVacancies",
                element: <RecruiterVacanciesList />
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
                path: "/vacancy/edit/:id",
                element: <VacancyUpdatePage />
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
                element: <StatisticPage />
            },
            {
                path: "/offers",
                element: <ChatList />
            },
            {
                path: "/applications",
                element: <ChatList />
            },
            {
                path: "/chat/:id",
                element: <div>Chat</div>
            },
            {
                path: "/users",
                element: <UserList />
            },
            {
                path: "/users/:id",
                element: <UserEdit />
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