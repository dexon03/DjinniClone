import { ListItem, ListItemAvatar, Avatar, ListItemText } from "@mui/material";
import { MessageDto } from "../models/chat/message.dto";

const MessageComponent = ({ message, userId }: { message: MessageDto, userId: string }) => {

    // const formatTimestamp = (timestamp: Date) => {
    //     return timestamp.getTime();
    // };
    const time = new Date(message.timestamp).toLocaleTimeString()

    return (
        message &&
        <ListItem alignItems="flex-start">
            <ListItemAvatar>
                <Avatar>{message.sender.userName.charAt(0)}</Avatar>
            </ListItemAvatar>
            <ListItemText
                primary={message.sender.id === userId ? "You" : message.sender.userName}
                secondary={
                    <>
                        <h5>{message.content}</h5>
                        <p>{time}</p>
                        {message.isRead ? <span>Read</span> : <span>Unread</span>}
                    </>
                }
            />
        </ListItem>
    );
};

export default MessageComponent;