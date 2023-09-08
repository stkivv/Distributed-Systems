import { IEventType } from "../domain/IEventType";
import { BaseEntityService } from "./BaseEntityService";

export class EventTypeService extends BaseEntityService<IEventType> {
    constructor(){
        super('v1/EventType');
    }

}