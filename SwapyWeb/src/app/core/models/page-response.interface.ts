export interface PageResponse<T> {
    items: T[];
    count: number;
    allPages: number;
}