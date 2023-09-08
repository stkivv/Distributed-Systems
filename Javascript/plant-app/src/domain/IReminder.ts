import { IBaseEntity } from "./IBaseEntity";
import { IHistoryEntry } from "./IHistoryEntry";
import { IMonth } from "./IMonth";
import { IReminderActiveMonth } from "./IReminderActiveMonth";
import { IReminderType } from "./IReminderType";

export interface IReminder extends IBaseEntity {
    reminderFrequency: Date,
    reminderMessage: string,
    reminderActiveMonths: IReminderActiveMonth[]
    plantId: string,
    plantName: string
    reminderType: IReminderType | null,
    appUserId: string,
    months: IMonth[],
    duration: number | undefined,
    plantHistoryEntries: IHistoryEntry[]
}
