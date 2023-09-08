import { BaseEntityService } from "./BaseEntityService";
import { IPlantCollection } from "../domain/IPlantCollection";

export class PlantCollectionService extends BaseEntityService<IPlantCollection> {
    constructor(){
        super('v1/PlantCollection');
    }

}