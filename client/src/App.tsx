import {HeaderComponent} from "./components/header.component.tsx";
import {Outlet} from "react-router-dom";
import {Container} from "@mui/material";
import LoginPage from "./pages/auth/login.page.tsx";
import {useState} from "react";
function App() {
  const [token, setToken] = useState();

  if(!token) {
      return <LoginPage setToken={setToken} />
  }
  return (
    <>
        <HeaderComponent />
        <Container className="mt-5">
            <Outlet />
        </Container>
    </>
  )
}

export default App
