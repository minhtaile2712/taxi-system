import { useState, useEffect } from "react";
import {
  HubConnectionBuilder,
  HubConnection,
  HubConnectionState,
} from "@microsoft/signalr";

type Handler = {
  name: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  action: (...args: any[]) => any;
};

type useHubReturns = {
  connection: HubConnection;
  connectionId: string | null;
  isConnected: boolean;
  subscribe(handler: Handler): void;
  unsubscribe(handler: Handler): void;
  subscribeMany(handlers: Handler[]): void;
  unsubscribeMany(handles: Handler[]): void;
};

function useHub(url: string): useHubReturns {
  const [hubConnection] = useState<HubConnection>(
    new HubConnectionBuilder().withUrl(url).build()
  );

  useEffect(() => {
    if (hubConnection.state === HubConnectionState.Disconnected) {
      hubConnection
        .start()
        .then(() => {
          console.log("Hub started.");
        })
        .catch((err) => {
          console.error("Error while starting hub:", err);
        });
    }

    return () => {
      if (hubConnection.state === HubConnectionState.Connected) {
        hubConnection.stop().then(() => {
          console.log("Hub stopped.");
        });
      }
    };
  }, [url, hubConnection]);

  const subscribe = (handler: Handler) =>
    hubConnection.on(handler.name, handler.action);

  const unsubscribe = (handler: Handler) =>
    hubConnection.off(handler.name, handler.action);

  const subscribeMany = (handlers: Handler[]) =>
    handlers.forEach((handler) =>
      hubConnection.on(handler.name, handler.action)
    );

  const unsubscribeMany = (handlers: Handler[]) =>
    handlers.forEach((handler) =>
      hubConnection.off(handler.name, handler.action)
    );

  return {
    connection: hubConnection,
    connectionId: hubConnection.connectionId,
    isConnected: hubConnection.state === HubConnectionState.Connected,
    subscribe,
    unsubscribe,
    subscribeMany,
    unsubscribeMany,
  };
}

export default useHub;
