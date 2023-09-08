import { ICollectionType } from "../domain/ICollectionType";
import { ICollectionTypeData } from "../dto/ICollectionTypeData";
import { BaseEntityService } from "./BaseEntityService";

export class CollectionTypeService extends BaseEntityService<ICollectionType> {
    constructor(){
        super('v1/CollectionType');
    }

    
    override async create(jwt: string, data: ICollectionTypeData) {
        try {
            const response = await this.axios.post('', data,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('create response', response);
            if (response.status === 200) {
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    override async delete(jwt: string, id: string | undefined) {
        if (id == undefined){
            return undefined;
        }
        try {
            const response = await this.axios.delete(id,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('delete response', response);
            if (response.status === 200) {
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    override async edit(jwt: string, data: ICollectionTypeData, id: string | undefined) {
        if (id == undefined){
            return undefined;
        }
        try {
            data.id = id;
            const response = await this.axios.put(id, data,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('delete response', response);
            if (response.status === 200) {
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
}