import "./App.css";

import {
  Box,
  Button,
  ButtonGroup,
  Link,
  Stack,
  Typography,
} from "@mui/material";

function App() {
  return (
    <Stack
      alignItems="center"
      justifyContent="center"
      sx={{ height: "100%" }}
      spacing={2}
    >
      <Box>
        <Typography align="center" variant="h3">
          Taxi App
        </Typography>
        <Typography variant="caption">Welcome to Taxi App!</Typography>
      </Box>

      <Box>
        <Typography>Select your role</Typography>
        <ButtonGroup orientation="vertical">
          <Button component={Link} href="/customer">
            Customer
          </Button>
          <Button component={Link} href="/driver">
            Driver
          </Button>
          <Button component={Link} href="/driver">
            Center
          </Button>
        </ButtonGroup>
      </Box>
    </Stack>
  );
}

export default App;
