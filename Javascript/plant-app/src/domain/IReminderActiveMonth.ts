import { IBaseEntity } from "./IBaseEntity";

export interface IReminderActiveMonth extends IBaseEntity {
    reminderId: string,
    monthId: string
}
