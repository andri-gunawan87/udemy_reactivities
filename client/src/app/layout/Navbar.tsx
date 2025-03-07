import { Group } from "@mui/icons-material";
import { Box, AppBar, Toolbar, Typography, Container, MenuItem } from "@mui/material";
import { NavLink } from "react-router";
import MenuItemLink from "../shared/component/MenuItemLink";


export default function Navbar() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" sx={{
                backgroundImage: "linear-gradient(15deg, #4facfe 0%, #00f2fe 30%, #182a73 100%)"
            }}>
                <Container maxWidth="xl">
                    <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
                        <Box>
                            <MenuItem component={NavLink} to="/" sx={{ display: "flex", gap: 2 }}>
                                <Group fontSize="large" />
                                <Typography variant="h4" fontWeight={"bold"}>Reactivities</Typography>
                            </MenuItem>
                        </Box>
                        <Box sx={{ display: "flex" }}>
                            <MenuItemLink to="/activities" >
                                Activites
                            </MenuItemLink>
                            <MenuItemLink to="/createActivity" >
                                Create Activity
                            </MenuItemLink>
                        </Box>
                        <MenuItem>
                            User Menu
                        </MenuItem>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}