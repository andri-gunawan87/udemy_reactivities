import { Box, Container, CssBaseline } from "@mui/material";
import Navbar from "./Navbar";
import { Outlet, ScrollRestoration, useLocation } from "react-router";
import HomePage from "../../features/activities/home/HomePage";

function App() {
  const location = useLocation();

  return (
    <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
      <ScrollRestoration />
      <CssBaseline />
      {location.pathname == "/" ? (
        <HomePage />
      ) : (
        <>
          <Navbar />
          <Container maxWidth="xl" sx={{ pt: 15 }}>
            <Outlet />
          </Container>
        </>
      )}
    </Box>
  );
}

export default App;
