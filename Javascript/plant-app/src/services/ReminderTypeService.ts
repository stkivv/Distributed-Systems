import { BaseEntityService } from "./BaseEntityService";
import { IReminderType } from "../domain/IReminderType";

export class ReminderTypeService extends BaseEntityService<IReminderType> {
    constructor(){
        super('v1/ReminderType');
    }

}