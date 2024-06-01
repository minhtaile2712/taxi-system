"use client";
import { useEffect, useRef } from 'react';
import * as signalR from '@microsoft/signalr';

const useSignalR = (hubUrl: string) => {
    const connection = useRef<signalR.HubConnection | null>(null);

    useEffect(() => {
        connection.current = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .withAutomaticReconnect()  
            .build();

        connection.current.start()
            .then(() => console.log('SignalR Connected'))
            .catch(err => console.error('SignalR Connection Error: ', err));

        return () => {
            if (connection.current) {
                connection.current.stop().then(() => console.log('SignalR Disconnected'));
            }
        };
    }, [hubUrl]);

    return connection.current;
};

export default useSignalR;
