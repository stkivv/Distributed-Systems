import { IBaseEntity } from "./IBaseEntity";

export interface IPhoto extends IBaseEntity {
    imageUrl: string,
    imageDescription: string,
    plantId: string
}
