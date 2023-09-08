import { IPestType } from "../domain/IPestType";
import { BaseEntityService } from "./BaseEntityService";

export class PestTypeService extends BaseEntityService<IPestType> {
    constructor(){
        super('v1/PestType');
    }

}