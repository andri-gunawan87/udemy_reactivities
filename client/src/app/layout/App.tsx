import { Box, Container, CssBaseline } from "@mui/material"
import axios from "axios"
import { useEffect, useState } from "react"
import Navbar from "./Navbar"
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard"

function App() {
  const [activities, setActivities] = useState<Activity[]>([])
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined)
  const [editMode, setEditMode] = useState(false)

  useEffect(() => {
    axios.get<Activity[]>("https://localhost:5001/api/activities")
      .then((response) => setActivities(response.data))
  }, [])

  const handleSelectedActivity = (id: string) => {
    setSelectedActivity(activities.find(a => a.id === id))
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

  const handleFormSubmit = (activity: Activity) => {
    if (activity.id) {
      setActivities(activities.map(a => a.id === activity.id ? activity : a))
    }
    else {
      const newActivity = { ...activity, id: activities.length.toString() }
      setActivities([...activities, newActivity])
    }
    setEditMode(false)
  }

  const handleDeleteActivity = (id: string) => {
    setActivities(activities.filter(a => a.id !== id))
  }

  return (
    <Box sx={{ bgcolor: "#eeeeee" }}>
      <CssBaseline />
      <Navbar openForm={handleFormOpen} />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        <ActivityDashboard
          activities={activities}
          selectActivity={handleSelectedActivity}
          cancelSelectActivity={handleCancelSelectedActivity}
          selectedActivity={selectedActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          submitForm={handleFormSubmit}
          deleteActivity={handleDeleteActivity}
        />
      </Container>
    </Box>
  )
}

export default App
