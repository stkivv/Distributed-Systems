import { BaseEntityService } from "./BaseEntityService";
import { IReminder } from "../domain/IReminder";

export class ReminderService extends BaseEntityService<IReminder> {
    constructor(){
        super('v1/Reminder');
    }

}