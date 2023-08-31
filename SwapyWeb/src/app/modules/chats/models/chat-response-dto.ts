export interface ChatResponseDTO {
    chatId: string;
    title: string;
    logo: string;
    image: string;
    lastMessage: string;
    lastMessageDateTime: Date | null;
}