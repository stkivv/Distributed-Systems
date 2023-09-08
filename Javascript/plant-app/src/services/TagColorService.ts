import { BaseEntityService } from "./BaseEntityService";
import { ITagColor } from "../domain/ITagColor";

export class TagColorService extends BaseEntityService<ITagColor> {
    constructor(){
        super('v1/TagColor');
    }

}