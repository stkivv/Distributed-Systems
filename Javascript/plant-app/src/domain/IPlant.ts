import { IBaseEntity } from "./IBaseEntity";
import { IPhoto } from "./IPhoto";
import { IHistoryEntry } from "./IHistoryEntry";
import { IPest } from "./IPest";
import { IReminder } from "./IReminder";
import { ISizeCategory } from "./ISizeCategory";
import { IPlantCollection } from "./IPlantCollection";
import { ITag } from "./ITag";

export interface IPlant extends IBaseEntity {
    plantName: string,
    description: string,
    plantFamily: string,
    scientificName: string,
    sizeCategory: ISizeCategory | null,
    photos: IPhoto[],
    historyEntries: IHistoryEntry[],
    pests: IPest[],
    reminders: IReminder[],
    appUserId: string,
    plantCollections: IPlantCollection[],
    tags: ITag[]
}
