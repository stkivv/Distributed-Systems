import { IMonth } from "../domain/IMonth";
import { BaseEntityService } from "./BaseEntityService";

export class MonthService extends BaseEntityService<IMonth> {
    constructor(){
        super('v1/Month');
    }

}