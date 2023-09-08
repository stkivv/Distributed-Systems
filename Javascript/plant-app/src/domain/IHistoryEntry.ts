import { IBaseEntity } from "./IBaseEntity";
import { IHistoryEntryType } from "./IHistoryEntryType";

export interface IHistoryEntry extends IBaseEntity {
    entryComment: string,
    entryTime: Date,
    historyEntryType: IHistoryEntryType | null,
    plantId: string
}
