import { Group } from "@mui/icons-material";
import { Box, Button, Paper, Typography } from "@mui/material";
import { Link } from "react-router";

export default function HomePage() {
  return (
    <Paper
      sx={{
        color: "white",
        display: "flex",
        flexDirection: "column",
        gap: 6,
        alignItems: "center",
        alignContent: "center",
        justifyContent: "center",
        height: "100vh",
        backgroundImage:
          "linear-gradient(15deg, #4facfe 0%, #00f2fe 30%, #182a73 100%)",
      }}
    >
      <Box
        sx={{
          display: "flex",
          gap: 3,
          alignItems: "center",
          alignContent: "center",
        }}>
          <Group sx={{height: 110, widows: 110}} />
          <Typography variant="h1" fontWeight={"bold"}>
            Reactivities
          </Typography>
      </Box>
      <Typography variant="h2">
        Welcome To Reactivities
      </Typography>
      <Button
        component={Link}
        to="/activities"
        size="large"
        variant="contained"
        sx={{height: 80, borderRadius: 4, fontSize: "1.5rem"}}
      >
        Take me to the activities!
      </Button>
    </Paper>
  )
}