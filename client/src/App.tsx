import {HeaderComponent} from "./components/header.component.tsx";
import {Outlet} from "react-router-dom";
import {Container} from "@mui/material";
function App() {
  return (
    <>
        <HeaderComponent />
        <Container>
            <Outlet />
        </Container>
    </>
  )
}

export default App
