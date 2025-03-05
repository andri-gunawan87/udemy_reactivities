import { Group } from "@mui/icons-material";
import { Box, AppBar, Toolbar, Typography, Button, Container, MenuItem } from "@mui/material";

type Props = {
    openForm: () => void
}

export default function Navbar({openForm}: Props) {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" sx={{
                backgroundImage: "linear-gradient(15deg, #4facfe 0%, #00f2fe 30%, #182a73 100%)"
            }}>
                <Container maxWidth="xl">
                    <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
                        <Box>
                            <MenuItem>
                                <Group fontSize="large" />
                                <Typography variant="h4" fontWeight={"bold"}>Reactivities</Typography>
                            </MenuItem>
                        </Box>
                        <Box sx={{ display: "flex" }}>
                            <MenuItem sx={{fontSize: "1.3rem", textTransform: "uppercase", fontWeight: "bold"}}>
                                Activites
                            </MenuItem>
                            <MenuItem sx={{fontSize: "1.3rem", textTransform: "uppercase", fontWeight: "bold"}}>
                                About
                            </MenuItem>
                            <MenuItem sx={{fontSize: "1.3rem", textTransform: "uppercase", fontWeight: "bold"}}>
                                Contact
                            </MenuItem>
                        </Box>
                        <Button 
                            size="large" 
                            variant="contained" 
                            color="warning"
                            onClick={openForm}
                        >
                            Create Activity
                        </Button>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}