"use client";

import React from "react";
import { useEffect } from "react";
import useGeolocation from "../hooks/useGeolocation";
import { useRouter,useSearchParams } from "next/navigation";
import { updateLocation } from "../services/customers";

export default function Home() {
  const { location, error } = useGeolocation();
  const router = useRouter();
  const searchParams = useSearchParams();
  const id = searchParams.get('id');
  if(!id){
    router.push('/login');
  }
  useEffect(() => {
    const intervalId = setInterval(async () => {
      if (location && id) {
        const data = {
          long: location.longitude,
          lat: location.latitude,
        };
        try {
          const result = await updateLocation(id, data);
          console.log('Location updated successfully:', result);
        } catch (error) {
          console.error('Failed to update location:', error);
        }
      }
    }, 5000); 

    return () => clearInterval(intervalId); 
  }, [location,id]);

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
