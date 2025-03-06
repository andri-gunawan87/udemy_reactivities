import { Box, Container, CssBaseline, Typography } from "@mui/material"
import { useState } from "react"
import Navbar from "./Navbar"
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard"
import { useActivities } from "../../lib/hooks/useActivities"

function App() {
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined)
  const [editMode, setEditMode] = useState(false)
  const { activities, isPending } = useActivities()


  const handleSelectedActivity = (id: string) => {
    setSelectedActivity(activities!.find(a => a.id === id))
  }

  const handleCancelSelectedActivity = () => {
    setSelectedActivity(undefined)
  }

  const handleFormOpen = (id?: string) => {
    if (id) handleSelectedActivity(id)
    else handleCancelSelectedActivity()
    setEditMode(true)
  }

  const handleFormClose = () => {
    setEditMode(false)
  }

  return (
    <Box sx={{ bgcolor: "#eeeeee", minHeight: "100vh" }}>
      <CssBaseline />
      <Navbar openForm={handleFormOpen} />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        {!activities || isPending ? (<Typography>Loading...</Typography>) :
          (
            <ActivityDashboard
              activities={activities}
              selectActivity={handleSelectedActivity}
              cancelSelectActivity={handleCancelSelectedActivity}
              selectedActivity={selectedActivity}
              editMode={editMode}
              openForm={handleFormOpen}
              closeForm={handleFormClose}
            />
          )}
      </Container>
    </Box>
  )
}

export default App
