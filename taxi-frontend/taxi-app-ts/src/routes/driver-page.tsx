import { useState } from "react";
import axios from "axios";

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
    <div>
      <h3>Taxi App for Drivers</h3>
      {driverId === null ? (
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
          <div>
            Logged in as driver {driverId} ({phoneNumberInput})
          </div>
          <div>
            <button onClick={logout}>Logout</button>
          </div>

          <DriverApp driverId={driverId} />
        </>
      )}
    </div>
  );
}
