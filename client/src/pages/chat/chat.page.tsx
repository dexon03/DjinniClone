import { useState, useEffect } from 'react';
import { LogLevel, HubConnectionBuilder } from "@microsoft/signalr";
import useToken from '../../hooks/useToken';

const ChatPage = () => {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    const { token } = useToken();
    const userId = token?.userId; // Replace with the actual user ID

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl("/chathub")
            .configureLogging(LogLevel.Information)
            .build();

        setConnection(newConnection);

        newConnection
            .start()
            .then(() => console.log("SignalR Connected"))
            .catch((error) => console.log("Error connecting to SignalR:", error));

        newConnection.on("ReceiveMessage", (senderId, message) => {
            // Handle incoming messages and update state
            setMessages([...messages, { senderId, message }]);
        });

        return () => {
            newConnection.stop();
        };
    }, [messages]);

    const sendMessage = (receiverId, message) => {
        // Send message to the back-end
        connection.invoke("SendMessage", userId, receiverId, message);
    };

    return (
        <div>
            {/* Render your chat UI here */}
        </div>
    );
};

export default ChatPage;
