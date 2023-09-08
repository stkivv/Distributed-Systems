import { IBaseEntity } from "./IBaseEntity";
import { IPlantInCollection } from "./IPlantInCollection";


export interface IPlantCollection extends IBaseEntity {
    collectionName: string,
    plantsInCollections: IPlantInCollection[],
    appUserId: string,
    collectionTypeId: string
}