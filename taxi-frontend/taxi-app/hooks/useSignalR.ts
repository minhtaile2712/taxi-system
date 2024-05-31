"use client";
import { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';

interface Location {
  latitude: number;
  longitude: number;
}

interface UseSignalRResult {
  location: Location | null;
  error: string | null;
  sendMessage: (user: string, message: string) => void;
}

const useSignalR = (hubUrl: string): UseSignalRResult => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [location, setLocation] = useState<Location | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, [hubUrl]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log('Connected!');
          connection.on('ReceiveMessage', (user: string, message: string) => {
            console.log(`Message from ${user}: ${message}`);
          });
        })
        .catch((e: Error) => setError(`Connection failed: ${e.message}`));
    }
  }, [connection]);

  const sendMessage = (user: string, message: string) => {
    if (connection) {
      connection.send('SendMessage', user, message)
        .catch((e: Error) => setError(`Send message failed: ${e.message}`));
    }
  };

  return { location, error, sendMessage };
};

export default useSignalR;
