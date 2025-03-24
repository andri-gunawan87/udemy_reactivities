import { AccessTime, Place } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Chip,
  Divider,
  Typography,
} from "@mui/material";
import { Link } from "react-router";
import { formatDate } from "../../../lib/util/util";
import AvatarPopover from "../../../app/shared/component/AvatarPopover";

type Props = {
  activity: Activity;
};

export default function ActivityCard({ activity }: Props) {
  const label = activity.isHost ? "You are hosting" : "You are going";
  const color = activity.isHost
    ? "secondary"
    : activity.isGoing
    ? "warning"
    : "default";
  return (
    <Card elevation={3} sx={{ borderRadius: 3 }}>
      <Box
        display={"flex"}
        alignItems={"center"}
        justifyContent={"space-between"}
      >
        <CardHeader
          avatar={
            <Avatar
              src={activity.hostImageUrl}
              sx={{ width: 80, height: 80 }}
              alt="Image of Host"
            />
          }
          title={activity.title}
          subheader={
            <>
              Hosted by{" "}
              <Link to={`/profiles/${activity.hostId}`}>
                {activity.hostDisplayName}
              </Link>
            </>
          }
        />
        <Box display={"flex"} flexDirection={"column"} gap={2} mr={2}>
          {activity.isHost || activity.isGoing ? (
            <Chip
              variant="outlined"
              label={label}
              color={color}
              sx={{ borderRadius: 2 }}
            />
          ) : null}
          {activity.isCancelled ? (
            <Chip label={"Canceled"} color={"error"} sx={{ borderRadius: 2 }} />
          ) : null}
        </Box>
      </Box>
      <Divider sx={{ mb: 3 }} />
      <CardContent sx={{ p: 0 }}>
        <Box display={"flex"} alignItems={"center"} mb={2} px={2}>
          <Box display={"flex"} flexGrow={0} alignItems={"center"}>
            <AccessTime sx={{ mr: 1 }} />
            <Typography variant={"body2"}>
              {formatDate(activity.date)}
            </Typography>
          </Box>
          <Place sx={{ ml: 3, mr: 1 }} />
          <Typography variant={"body2"}>{activity.venue}</Typography>
        </Box>
        <Divider />
        <Box
          display={"flex"}
          sx={{ backgroundColor: "grey.200", py: 3, pl: 3 }}
          gap={2}
        >
          {activity.attendees.map((att) => (
            <AvatarPopover profile={att} key={att.id} />
          ))}
        </Box>
      </CardContent>
      <CardContent sx={{ pb: 2 }}>
        <Typography variant={"body2"}>{activity.description}</Typography>
        <Box display={"flex"} justifyContent={"end"}>
          <Button
            component={Link}
            to={`/activities/${activity.id}`}
            size="medium"
            variant="contained"
            sx={{ display: "flex", justifySelf: "self-end", borderRadius: 3 }}
          >
            View
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
}
