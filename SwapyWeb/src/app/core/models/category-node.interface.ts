import { Specification } from "./specification";

export interface CategoryNode {
    id: string;
    value: string;
    isFinal: boolean;
    parent: Specification<string>;
}