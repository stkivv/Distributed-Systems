import { IBaseEntity } from "./IBaseEntity";

export interface ITagColor extends IBaseEntity {
    colorName: string,
    colorHex: string,
}