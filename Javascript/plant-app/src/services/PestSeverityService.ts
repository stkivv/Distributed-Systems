import { IPestSeverity } from "../domain/IPestSeverity";
import { BaseEntityService } from "./BaseEntityService";

export class PestSeverityService extends BaseEntityService<IPestSeverity> {
    constructor(){
        super('v1/PestSeverity');
    }

}