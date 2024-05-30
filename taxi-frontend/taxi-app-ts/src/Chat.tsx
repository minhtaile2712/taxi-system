import { useState, useEffect } from "react";

import useHub from "./useHub";

type Message = {
  content: string;
  senderId: string;
  sentTime: Date;
};

function Chat() {
  const hub = useHub("https://localhost:7283/hub");

  const [messages, setMessages] = useState<Message[]>([]);
  const [newMessage, setNewMessage] = useState("");

  useEffect(() => {
    const handlers = [
      {
        name: "ReceiveMessage",
        action: (content: string, senderId: string, sentTime: Date) => {
          setMessages((prev) => [...prev, { content, senderId, sentTime }]);
        },
      },
    ];

    hub.subscribeMany(handlers);

    return () => {
      hub.unsubscribeMany(handlers);
    };
  }, [hub]);

  const isMyMessage = (username: string) => {
    return username === hub.connectionId;
  };

  const sendMessageToOthers = async () => {
    if (!hub.isConnected) return;
    if (newMessage.trim()) {
      await hub.connection.send("SendMessageToOthers", newMessage);
      setNewMessage("");
    }
  };

  const sendMessageToAll = async () => {
    if (!hub.isConnected) return;
    if (newMessage.trim()) {
      await hub.connection.send("SendMessageToAll", newMessage);
      setNewMessage("");
    }
  };

  return (
    <div>
      <div>
        {messages.map((msg, index) => (
          <div key={index}>
            <span>{isMyMessage(msg.senderId) ? "Me" : msg.senderId}</span>
            <span>{new Date(msg.sentTime).toLocaleString()}</span>
            <p>{msg.content}</p>
          </div>
        ))}
      </div>
      <div>
        <input
          type="text"
          value={newMessage}
          onChange={(e) => setNewMessage(e.target.value)}
        />
        <div>
          <button onClick={sendMessageToOthers}>Send to others</button>
          <button onClick={sendMessageToAll}>Send to all</button>
        </div>
      </div>
    </div>
  );
}

export default Chat;
