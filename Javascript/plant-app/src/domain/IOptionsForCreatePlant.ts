import { IBaseEntity } from "./IBaseEntity";
import { IPlantCollection } from "./IPlantCollection";
import { ISizeCategory } from "./ISizeCategory";
import { ITag } from "./ITag";


export interface IOptionsForCreatePlant extends IBaseEntity {

    tags: ITag[],
    sizeCategories: ISizeCategory[],
    plantCollections: IPlantCollection[]

}