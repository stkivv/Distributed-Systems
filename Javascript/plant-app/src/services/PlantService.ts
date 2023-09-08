import { BaseEntityService } from "./BaseEntityService";
import {IPlant} from "../domain/IPlant";

export class PlantService extends BaseEntityService<IPlant> {
    constructor(){
        super('v1/Plant');
    }

}