import { IHistoryEntryType } from "../domain/IHistoryEntryType";
import { BaseEntityService } from "./BaseEntityService";

export class HistoryEntryTypeService extends BaseEntityService<IHistoryEntryType> {
    constructor(){
        super('v1/HistoryEntryType');
    }

}