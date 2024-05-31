"use client";

import useGeolocation from "@/hooks/useGeolocation";
import { useEffect } from "react";

export default function Home() {
  const { location, error } = useGeolocation();

  useEffect(() => {
    if (location) {
      console.log('Location:', location);
    }
  }, [location]);

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!location) {
    return <div>Getting location...</div>;
  }
  return (
    <div>
      <p>Latitude: {location.latitude}</p>
      <p>Longitude: {location.longitude}</p>
    </div>
  );
}
