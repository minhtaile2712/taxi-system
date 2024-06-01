import { useState } from "react";
import axios from "axios";

import CustomerApp from "../CustomerApp";

const baseUrl = "https://localhost:7283";

export default function CustomerPage() {
  const [phoneNumberInput, setPhoneNumberInput] = useState("");
  const [customerId, setCustomerId] = useState<number | null>(null);

  const login = async () => {
    const phoneNumber = phoneNumberInput.trim();
    if (phoneNumber) {
      axios
        .post(`${baseUrl}/api/customers`, {
          phoneNumber,
        })
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
          <div>
            Logged in as customer {customerId} ({phoneNumberInput})
          </div>
          <div>
            <button onClick={logout}>Logout</button>
          </div>

          <CustomerApp customerId={customerId} />
        </>
      )}
    </div>
  );
}
