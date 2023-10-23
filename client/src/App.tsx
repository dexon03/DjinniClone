import {HeaderComponent} from "./components/header.component.tsx";
import {Outlet} from "react-router-dom";
import {Container} from "@mui/material";
function App() {
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
