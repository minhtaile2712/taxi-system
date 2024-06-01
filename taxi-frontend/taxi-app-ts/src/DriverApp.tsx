import { useState, useEffect } from "react";
import axios from "axios";
import Button from "@mui/material/Button";
import DialogTitle from "@mui/material/DialogTitle";
import Dialog from "@mui/material/Dialog";
import Typography from "@mui/material/Typography";

import useHub from "./useHub";

const baseUrl = "https://localhost:7283";

export default function DriverApp({ driverId }: { driverId: number }) {
  const [latLongInput, setLatLongInput] = useState(
    "10.82440934652589, 106.63035261795255"
  );

  const [currentLocation, setCurrentLocation] = useState({
    longitude: 0,
    latitude: 0,
  });

  const hub = useHub(baseUrl + "/hub");

  const sendLocation = () => {
    const latLong = latLongInput.trim();
    if (latLong) {
      const [lat, long] = latLong.split(",").map(parseFloat);
      axios.post(`${baseUrl}/api/drivers/${driverId}/location`, {
        long,
        lat,
      });
    }
  };

  const getDriverInfo = () => {
    axios
      .get(`${baseUrl}/api/drivers/${driverId}`)
      .then((res) => {
        setCurrentLocation({
          longitude: res.data.location.long,
          latitude: res.data.location.lat,
        });
      })
      .catch((err) => console.error(err));
  };

  useEffect(() => {
    axios
      .get(`${baseUrl}/api/drivers/${driverId}`)
      .then((res) => {
        setCurrentLocation({
          longitude: res.data.location?.long || 1,
          latitude: res.data.location?.lat || 1,
        });
      })
      .catch((err) => console.error(err));
  }, [driverId]);

  useEffect(() => {
    axios
      .get(`${baseUrl}/api/bookings/by-driver/${driverId}`)
      .then((res) => {
        if (res.data) {
          setBookingId(res.data.id);
          setOpen(true);
        }
      })
      .catch((err) => console.error(err));
  }, [driverId]);

  const [open, setOpen] = useState(false);

  const [bookingId, setBookingId] = useState<number | null>(null);

  useEffect(() => {
    const handlers = [
      {
        name: "BookingCreatedToDriver",
        action: (createdBookingId: number, driverIdToNotify: number) => {
          if (driverIdToNotify === driverId) {
            setBookingId(createdBookingId);
            setOpen(true);
          }
        },
      },
    ];

    hub.subscribeMany(handlers);

    return () => {
      hub.unsubscribeMany(handlers);
    };
  }, [hub, driverId]);

  const handleClose = () => {
    setOpen(false);
    setBookingId(null);
  };

  return (
    <>
      <div>
        <label>
          Lat Long
          <input
            type="text"
            value={latLongInput}
            onChange={(e) => setLatLongInput(e.target.value)}
          />
        </label>
        <div>
          <button onClick={sendLocation}>Send location</button>
        </div>
      </div>

      <div>
        <div>Current Longitude: {currentLocation.longitude}</div>
        <div>Current Latitude: {currentLocation.latitude}</div>
        <button onClick={getDriverInfo}>Get current location in system</button>
      </div>

      <BookingNotifyingDialog
        bookingId={bookingId}
        driverId={driverId}
        open={open}
        onClose={handleClose}
      />
    </>
  );
}

export interface BookingNotifyingDialogProps {
  open: boolean;
  bookingId: number | null;
  driverId: number;
  onClose: () => void;
}

function BookingNotifyingDialog(props: BookingNotifyingDialogProps) {
  const { onClose, bookingId, driverId, open } = props;

  const [isWaiting, setIsWaiting] = useState(true);

  const handleClose = () => {
    // onClose();
  };

  const handleAccept = () => {
    axios
      .post(`${baseUrl}/api/bookings/accept`, {
        bookingId,
        driverId,
      })
      .then(() => {
        setIsWaiting(false);
      });
  };

  const handleDeny = () => {
    axios
      .post(`${baseUrl}/api/bookings/deny`, {
        bookingId,
        driverId,
      })
      .then(() => {
        setIsWaiting(true);
        onClose();
      });
  };

  const hub = useHub(baseUrl + "/hub");

  useEffect(() => {
    const handlers = [
      {
        name: "BookingCompleted",
        action: (completedBookingId: number) => {
          if (completedBookingId === bookingId) {
            setIsWaiting(true);
            onClose();
          }
        },
      },
      {
        name: "BookingCancelled",
        action: (completedBookingId: number) => {
          if (completedBookingId === bookingId) {
            setIsWaiting(true);
            onClose();
          }
        },
      },
    ];

    hub.subscribeMany(handlers);

    return () => {
      hub.unsubscribeMany(handlers);
    };
  }, [hub, bookingId, onClose]);

  const handleComplete = () => {
    axios.post(`${baseUrl}/api/bookings/${bookingId}/complete`).then(() => {
      setIsWaiting(true);
      onClose();
    });
  };

  useEffect(() => {
    axios
      .get(`${baseUrl}/api/bookings/by-driver/${driverId}`)
      .then((res) => {
        if (res.data) {
          if (res.data.isAccepted) setIsWaiting(false);
        }
      })
      .catch((err) => console.error(err));
  }, [driverId]);

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>New booking</DialogTitle>
      <Typography>Booking Id: {bookingId}</Typography>

      {isWaiting ? (
        <>
          <Button onClick={handleAccept}>Accept</Button>
          <Button onClick={handleDeny}>Deny</Button>
        </>
      ) : (
        <>
          <Typography>Booking is accepted</Typography>
          <Button onClick={handleComplete}>Complete this booking</Button>
        </>
      )}
    </Dialog>
  );
}
