import { IPhoto } from "../domain/IPhoto";
import { BaseEntityService } from "./BaseEntityService";

export class PhotoService extends BaseEntityService<IPhoto> {
    constructor(){
        super('v1/Photo');
    }

}