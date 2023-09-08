import { IBaseEntity } from "./IBaseEntity";

export interface IHistoryEntryType extends IBaseEntity {
    entryTypeName: string,
    eventTypeId: string
}
