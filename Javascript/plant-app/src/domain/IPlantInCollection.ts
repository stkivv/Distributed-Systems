import { IBaseEntity } from "./IBaseEntity";


export interface IPlantInCollection extends IBaseEntity {
    plantCollectionId: string,
    plantId: string
}