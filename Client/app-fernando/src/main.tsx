import React from "react";
import ReactDOM from "react-dom/client";
import "./App.css";
import "semantic-ui-css/semantic.min.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./router/Routes.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
