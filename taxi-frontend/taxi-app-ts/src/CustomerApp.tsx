import { useState, useEffect } from "react";
import axios from "axios";
import Button from "@mui/material/Button";
import DialogTitle from "@mui/material/DialogTitle";
import Dialog from "@mui/material/Dialog";
import Typography from "@mui/material/Typography";
import useHub from "./useHub";

const baseUrl = "https://localhost:7283";

export default function CustomerApp({ customerId }: { customerId: number }) {
  const [latLongInput, setLatLongInput] = useState(
    "10.82440934652589, 106.63035261795255"
  );

  const [currentLocation, setCurrentLocation] = useState({
    longitude: 0,
    latitude: 0,
  });

  const sendLocation = () => {
    const latLong = latLongInput.trim();
    if (latLong) {
      const [lat, long] = latLong.split(",").map(parseFloat);
      axios.post(`${baseUrl}/api/customers/${customerId}/location`, {
        long,
        lat,
      });
    }
  };

  const newBooking = () => {
    axios
      .post(`${baseUrl}/api/bookings/create`, {
        customerId,
      })
      .then((res) => {
        if (res.data) {
          setBookingId(res.data.id);
          setOpen(true);
        }
      })
      .catch((err) => console.error(err));
  };

  const getCustomerInfo = () => {
    axios
      .get(`${baseUrl}/api/customers/${customerId}`)
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
      .get(`${baseUrl}/api/customers/${customerId}`)
      .then((res) => {
        setCurrentLocation({
          longitude: res.data.location?.long || 1,
          latitude: res.data.location?.lat || 1,
        });
      })
      .catch((err) => console.error(err));
  }, [customerId]);

  const [open, setOpen] = useState(false);
  const [bookingId, setBookingId] = useState<number | null>(null);

  useEffect(() => {
    axios
      .get(`${baseUrl}/api/bookings/by-customer/${customerId}`)
      .then((res) => {
        if (res.data) {
          setBookingId(res.data.id);
          setOpen(true);
        }
      })
      .catch((err) => console.error(err));
  }, [customerId]);

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
        <button onClick={getCustomerInfo}>
          Get current location in system
        </button>
      </div>

      <div>
        <button onClick={newBooking}>Book a ride!</button>
      </div>

      <BookingDialog
        bookingId={bookingId}
        customerId={customerId}
        open={open}
        onClose={handleClose}
      />
    </>
  );
}

export interface BookingDialogProps {
  open: boolean;
  bookingId: number | null;
  customerId: number;
  onClose: () => void;
}

function BookingDialog(props: BookingDialogProps) {
  const { onClose, bookingId, customerId, open } = props;

  const [isWaiting, setIsWaiting] = useState(true);
  const [driverPhoneNumber, setDriverPhoneNumber] = useState<string | null>(
    null
  );

  const onCleanClose = () => {
    setIsWaiting(true);
    setDriverPhoneNumber(null);
    onClose();
  };

  const handleClose = () => {
    // onClose();
  };

  const handleCancel = () => {
    axios.post(`${baseUrl}/api/bookings/${bookingId}/cancel`).then(() => {
      onCleanClose();
    });
  };

  const hub = useHub(baseUrl + "/hub");

  useEffect(() => {
    const handlers = [
      {
        name: "BookingAcceptedToCustomer",
        action: (
          acceptedBookingId: number,
          customerId: number,
          driverId: number
        ) => {
          if (acceptedBookingId === bookingId) {
            axios.get(`${baseUrl}/api/drivers/${driverId}`).then((res) => {
              setDriverPhoneNumber(res.data.phoneNumber);
            });
            setIsWaiting(false);
          }
        },
      },
      {
        name: "BookingDeniedToCustomer",
        action: (deniedBookingId: number, customerId: number) => {
          console.log("booking denied", deniedBookingId, customerId);
          if (deniedBookingId === bookingId) {
            setIsWaiting(true);
            setDriverPhoneNumber(null);
            onClose();
          }
        },
      },
      {
        name: "BookingCompleted",
        action: (completedBookingId: number) => {
          if (completedBookingId === bookingId) {
            setIsWaiting(true);
            setDriverPhoneNumber(null);
            onClose();
          }
        },
      },
      {
        name: "BookingCancelled",
        action: (completedBookingId: number) => {
          if (completedBookingId === bookingId) {
            setIsWaiting(true);
            setDriverPhoneNumber(null);
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
      onCleanClose();
    });
  };

  useEffect(() => {
    axios
      .get(`${baseUrl}/api/bookings/by-customer/${customerId}`)
      .then((res) => {
        if (res.data) {
          if (res.data.isAccepted) setIsWaiting(false);
        }
      })
      .catch((err) => console.error(err));
  }, [customerId]);

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Created booking</DialogTitle>
      <Typography>Booking Id: {bookingId}</Typography>
      {isWaiting ? (
        <>
          <Typography>Waiting for system...</Typography>
          <Button onClick={handleCancel}>Cancel booking</Button>
        </>
      ) : (
        <>
          <Typography>Booking is accepted</Typography>
          <Typography>
            Driver with phone number: {driverPhoneNumber} is on the way.
          </Typography>
          <Button onClick={handleComplete}>Complete this booking</Button>
        </>
      )}
    </Dialog>
  );
}
