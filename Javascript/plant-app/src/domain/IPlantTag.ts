import { IBaseEntity } from "./IBaseEntity";

export interface IPlantTag extends IBaseEntity {
    tagId: string,
    plantId: string
}
