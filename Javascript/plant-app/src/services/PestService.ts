import { IPest } from "../domain/IPest";
import { BaseEntityService } from "./BaseEntityService";

export class PestService extends BaseEntityService<IPest> {
    constructor(){
        super('v1/Pest');
    }

}