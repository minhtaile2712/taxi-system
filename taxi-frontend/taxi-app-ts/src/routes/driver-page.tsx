import { useState } from "react";
import axios from "axios";

import { Box, Button, Stack, TextField, Typography } from "@mui/material";
import LogoutIcon from "@mui/icons-material/Logout";

import DriverApp from "../DriverApp";

const baseUrl = "https://localhost:7283";

export default function DriverPage() {
  const [phoneNumberInput, setPhoneNumberInput] = useState("");
  const [driverId, setDriverId] = useState<number | null>(null);

  const login = async () => {
    const phoneNumber = phoneNumberInput.trim();
    if (phoneNumber) {
      axios
        .post(`${baseUrl}/api/drivers`, {
          phoneNumber,
        })
        .then((res) => {
          if (res.data) setDriverId(res.data.id);
        });
    }
  };

  const logout = () => {
    setDriverId(null);
  };

  return (
    <Stack
      alignItems="center"
      justifyContent="center"
      sx={{ height: "100%" }}
      spacing={2}
      padding={2}
    >
      <Typography align="center" variant="h4">
        Taxi App
      </Typography>
      <Typography variant="caption">Welcome to Taxi App for driver!</Typography>
      {driverId === null ? (
        <>
          <Typography>Enter your credential</Typography>
          <div>
            <TextField
              label="Phone number"
              variant="outlined"
              value={phoneNumberInput}
              onChange={(e) => setPhoneNumberInput(e.target.value)}
            />
          </div>
          <div>
            <Button variant="contained" onClick={login}>
              Continue
            </Button>
          </div>
        </>
      ) : (
        <>
          <Stack direction="row" alignItems="center" spacing={2} width="100%">
            <Box flexGrow={1}>
              <Stack direction="row" justifyContent="space-between">
                <Typography>Driver ID:</Typography>
                <Typography>{driverId}</Typography>
              </Stack>
              <Stack direction="row" justifyContent="space-between">
                <Typography>Phone number:</Typography>
                <Typography>{phoneNumberInput}</Typography>
              </Stack>
            </Box>

            <Button
              variant="contained"
              startIcon={<LogoutIcon />}
              onClick={logout}
            >
              Logout
            </Button>
          </Stack>

          <DriverApp driverId={driverId} />
        </>
      )}
    </Stack>
  );
}
