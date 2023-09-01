export interface ChatMessageModel {
    chatId: string;
    recepientId: string;
    senderId: string;
    message: string;
    image: string;
    dateTime: Date;
}