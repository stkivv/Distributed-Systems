import { IHistoryEntry } from "../domain/IHistoryEntry";
import { BaseEntityService } from "./BaseEntityService";

export class HistoryEntryService extends BaseEntityService<IHistoryEntry> {
    constructor(){
        super('v1/HistoryEntry');
    }

}