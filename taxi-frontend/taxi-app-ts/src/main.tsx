import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import CssBaseline from "@mui/material/CssBaseline";
import Container from "@mui/material/Container";
import Box from "@mui/material/Box";

import "./index.css";
import App from "./App.tsx";
import ErrorPage from "./routes/error-page.tsx";
import CustomerPage from "./routes/customer-page.tsx";
import DriverPage from "./routes/driver-page.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
  },
  {
    path: "customer",
    element: <CustomerPage />,
  },
  {
    path: "driver",
    element: <DriverPage />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Box sx={{ bgcolor: "#cfe8fc" }}>
      <CssBaseline />
      <Container
        disableGutters
        fixed
        maxWidth="xs"
        sx={{ bgcolor: "white", height: "100vh" }}
      >
        <RouterProvider router={router} />
      </Container>
    </Box>
  </React.StrictMode>
);
