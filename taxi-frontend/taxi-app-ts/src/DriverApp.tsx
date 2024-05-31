import { useState, useEffect } from "react";

import useHub from "./useHub";

function DriverApp() {
  const hub = useHub("https://localhost:7283/hub");

  const [phoneNumber, setPhoneNumber] = useState("");

  return (
    <div>
      <h3>Taxi App for Drivers</h3>

      <input
        type="text"
        value={phoneNumber}
        onChange={(e) => setPhoneNumber(e.target.value)}
      />
    </div>
  );
}

export default DriverApp;
