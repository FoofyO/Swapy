import { MessageResponseDTO } from "./message-response-dto";

export interface DetailChatResponseDTO {
    chatId: string;
    title: string;
    image: string;
    messages: MessageResponseDTO[];
}