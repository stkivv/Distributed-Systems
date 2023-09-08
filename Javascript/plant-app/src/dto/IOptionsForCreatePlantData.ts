import { IBaseEntity } from "../domain/IBaseEntity";
import { IPhoto } from "../domain/IPhoto";
import { IPlantCollection } from "../domain/IPlantCollection";
import { ISizeCategory } from "../domain/ISizeCategory";
import { ITag } from "../domain/ITag";


export interface IOptionsForCreatePlantData extends IBaseEntity {

    tags: ITag[],
    sizeCategories?: ISizeCategory[],
    plantCollections: IPlantCollection[],
    plantId?: string,
    photos?: IPhoto[]
}