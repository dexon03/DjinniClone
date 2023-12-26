import { useEffect, useState } from 'react';
import { useGetChatMessagesQuery } from '../../app/features/chat/chat.api';
import useToken from '../../hooks/useToken';
import { useParams } from 'react-router-dom';
import { MessageDto } from '../../models/chat/message.dto';
import MessageComponent from '../../components/message.component';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

const ChatPage = () => {
    const { token } = useToken();
    const { id } = useParams<{ id: string }>();
    const { data: previousMessages, isLoading } = useGetChatMessagesQuery(id);

    // const [users, setUsers] = useState<any[]>([] as any[]);
    const [messages, setMessages] = useState<MessageDto[]>([] as MessageDto[]);
    // const [hubConnection, setHubConnection] = useState<any>(null);
    // const [messageInput, setMessageInput] = useState<string>('');

    useEffect(() => {
        setMessages(previousMessages);

        // const url = 'http://localhost:5245' + '/chatHub';
        // const connection = new HubConnectionBuilder()
        //     .withUrl(url)
        //     .configureLogging(LogLevel.Information)
        //     .build();

        // connection.start().then(() => {
        //     console.log('Connection started!');
        // }).catch(err => console.error('Error while establishing connection:', err));



        // setHubConnection(connection);

    }, [previousMessages]);

    // useEffect(() => {
    //     if (hubConnection) {
    //         hubConnection.on('ReceiveMessage', (message: MessageDto) => {
    //             setMessages((prevMessages: MessageDto[]) => [...prevMessages, message]);
    //         });
    //     }
    // }, [hubConnection]);

    // const sendMessage = (message: string) => {
    //     if (hubConnection) {
    //         hubConnection.invoke('SendMessage', {
    //             chatId: id,
    //             senderId: token?.userId,
    //             receiver: users.find((u) => u.id !== token?.userId),
    //             content: message,
    //         })
    //             .catch((err: any) => console.error(err));
    //     }
    // };

    if (isLoading) {
        return <div>Loading...</div>
    }

    return (
        <>
            {messages && messages.length > 0 ? messages.map((msg) => (
                <MessageComponent key={msg.id} message={msg} userId={token?.userId} />
            )) :
                <div>No messages</div>}
            {/* <div>
                <input
                    type="text"
                    placeholder="Type a message..."
                    onChange={(e) => setMessageInput(e.target.value)}
                    value={messageInput}
                />
                <button onClick={() => sendMessage(messageInput)}>Send</button>
            </div> */}
        </>
    );
}

export default ChatPage;