import { HeaderComponent } from "./components/header.component.tsx";
import { Outlet } from "react-router-dom";
import { Container } from "@mui/material";
import ErrorBoundary from "./components/error.boundary.tsx";
import { pdfjs } from "react-pdf";


pdfjs.GlobalWorkerOptions.workerSrc = new URL(
  'pdfjs-dist/build/pdf.worker.min.js',
  import.meta.url,
).toString();


function App() {
  return (
    <ErrorBoundary>
      <HeaderComponent />
      <Container className="mt-5 pb-5">
        <Outlet />
      </Container>
    </ErrorBoundary>
  )
}

export default App
