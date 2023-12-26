import { ListItem, ListItemAvatar, Avatar, ListItemText } from "@mui/material";
import { MessageDto } from "../models/chat/message.dto";

const MessageComponent = ({ message, userId }: { message: MessageDto, userId: string }) => {

    // const formatTimestamp = (timestamp: Date) => {
    //     return timestamp.getTime();
    // };

    return (
        <ListItem alignItems="flex-start">
            <ListItemAvatar>
                <Avatar>{message.sender.userName}</Avatar>
            </ListItemAvatar>
            <ListItemText
                primary={message.sender.id === userId ? "You" : message.sender.userName}
                secondary={
                    <>
                        <p>{message.content}</p>
                        <p>{message.timestamp}</p>
                        {message.isRead ? <span>Read</span> : <span>Unread</span>}
                    </>
                }
            />
        </ListItem>
    );
};

export default MessageComponent;