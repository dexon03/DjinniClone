import { HeaderComponent } from "./components/header.component.tsx";
import { Outlet } from "react-router-dom";
import { Container } from "@mui/material";
import LoginPage from "./pages/auth/login.page.tsx";
import ErrorBoundary from "./components/error.boundary.tsx";
import useToken from "./hooks/useToken.ts";


function App() {
  const { token, setToken } = useToken();

  if (!token) {
    return <LoginPage setToken={setToken} />
  }
  return (
    <ErrorBoundary>
      <HeaderComponent />
      <Container className="mt-5">
        <Outlet />
      </Container>
    </ErrorBoundary>
  )
}

export default App
