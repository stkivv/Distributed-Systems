import { BaseEntityService } from "./BaseEntityService";
import { ISizeCategory } from "../domain/ISizeCategory";

export class SizeCategoryService extends BaseEntityService<ISizeCategory> {
    constructor(){
        super('v1/SizeCategory');
    }

    async getOneById(jwt: string, id: string): Promise<ISizeCategory | undefined> {
        try {
            const response = await this.axios.get(id,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('create response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }
    
    async create(jwt: string, data: ISizeCategory) {
        try {
            const response = await this.axios.post('', data,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('create response', response);
            if (response.status === 201) {
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async delete(jwt: string, id: string | undefined) {
        if (id === undefined){
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
            if (response.status === 204) {
                return true;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async edit(jwt: string, data: ISizeCategory, id: string | undefined) {
        if (id === undefined){
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

            console.log('edit response', response);
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