"use client";

import { useState, useEffect } from 'react';

interface Location {
  latitude: number;
  longitude: number;
}

const useGeolocation = (): { location: Location | null; error: string | null } => {
  const [location, setLocation] = useState<Location | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const updateLocation = () => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const { latitude, longitude } = position.coords;
            setLocation({ latitude, longitude });
          },
          (error) => {
            setError(error.message);
          }
        );
      } else {
        setError('Geolocation is not supported by this browser.');
      }
    };

    updateLocation(); // Call initially to get the first location
  }, []);

  return { location, error };
};

export default useGeolocation;
