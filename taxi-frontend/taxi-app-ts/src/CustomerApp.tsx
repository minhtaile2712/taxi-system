import { useState, useEffect } from "react";
import axios from "axios";

import useHub from "./useHub";

const baseUrl = "https://localhost:7283";

function SmallApp({ customerId }: { customerId: number }) {
  const [longitude, setLongitude] = useState("106.64964908560823");
  const [latitude, setLatitude] = useState("10.80184768485948");

  const hub = useHub(baseUrl + "/hub");

  const sendLocation = () => {
    axios.post(`${baseUrl}/api/customers/${customerId}/location`, {
      long: Number.parseInt(longitude),
      lat: Number.parseInt(latitude),
    });
  };

  const newBooking = () => {
    axios.post(`${baseUrl}/api/bookings`, {
      customerId,
    });
  };

  return (
    <>
      <div>
        <span>Longitude:</span>
        <input
          type="text"
          value={longitude}
          onChange={(e) => setLongitude(e.target.value)}
        />
      </div>
      <div>
        <span>Latitude:</span>
        <input
          type="text"
          value={latitude}
          onChange={(e) => setLatitude(e.target.value)}
        />
      </div>
      <div>
        <button onClick={sendLocation}>Send location</button>
      </div>
      <div>
        <button onClick={newBooking}>Book a ride!</button>
      </div>
    </>
  );
}

function CustomerApp() {
  const [phoneNumberInput, setPhoneNumberInput] = useState("");
  const [customerId, setCustomerId] = useState<number | null>(null);

  const login = async () => {
    if (phoneNumberInput.trim()) {
      axios
        .get(`${baseUrl}/api/customers/by-phone/${phoneNumberInput}`)
        .then((res) => {
          if (res.data) setCustomerId(res.data.id);
        });
    }
  };

  const logout = () => {
    setCustomerId(null);
  };

  return (
    <div>
      <h3>Taxi App for Customers</h3>
      {customerId === null ? (
        <>
          <div>Phone number</div>
          <div>
            <input
              type="text"
              value={phoneNumberInput}
              onChange={(e) => setPhoneNumberInput(e.target.value)}
            />
          </div>
          <div>
            <button onClick={login}>Login</button>
          </div>
        </>
      ) : (
        <>
          <div>Logged in as customer {customerId}</div>
          <div>
            <button onClick={logout}>Logout</button>
          </div>

          <SmallApp customerId={customerId} />
        </>
      )}
    </div>
  );
}

export default CustomerApp;
