import { Outlet, useLocation } from "react-router-dom";
import "./App.css";
import AdvisorTable from "./components/advisors/AdvisorTable";
import { Container } from "semantic-ui-react";

function App() {
  const location = useLocation();

  return (
    <>
      {location.pathname === "/" ? (
        <AdvisorTable />
      ) : (
        <Container className="container-style">
          <Outlet />
        </Container>
      )}
    </>
  );
}

export default App;
