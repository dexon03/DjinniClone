import { HeaderComponent } from "./components/header.component.tsx";
import { Outlet } from "react-router-dom";
import { Container } from "@mui/material";
import ErrorBoundary from "./components/error.boundary.tsx";
function App() {
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
