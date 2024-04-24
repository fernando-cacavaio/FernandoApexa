import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../App";
import AdvisorForm from "../components/advisors/AdvisorForm";

export const routes: RouteObject[] = [
  {
    path: "/",
    element: <App />,
    children: [
      { path: "createAdvisor", element: <AdvisorForm key="create" /> },
      { path: "editAdvisor/:id", element: <AdvisorForm key="edit" /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
