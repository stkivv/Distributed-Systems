import { BaseEntityService } from "./BaseEntityService";
import { ITag } from "../domain/ITag";

export class TagService extends BaseEntityService<ITag> {
    constructor(){
        super('v1/Tag');
    }

}