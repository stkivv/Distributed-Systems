import { IBaseEntity } from "./IBaseEntity";

export interface IReminderType extends IBaseEntity {
    reminderTypeName: string,
    eventTypeId: string
}
