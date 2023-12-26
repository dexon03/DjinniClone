import { MessageUserDto } from "./message.user.dto";

export interface MessageDto {
    id: string;
    content: string;
    timestamp: Date;
    sender: MessageUserDto;
    receiver: MessageUserDto;
    chatId: string;
    isRead: boolean;
}