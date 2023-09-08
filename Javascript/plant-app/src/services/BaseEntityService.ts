import { IBaseEntity } from "../domain/IBaseEntity";
import { BaseService } from "./BaseService";

export abstract class BaseEntityService<TEntity extends IBaseEntity> extends BaseService {
    constructor(baseUrl: string) {
        super(baseUrl);
    }

    async getAll(jwt: string): Promise<TEntity[] | undefined> {
        try {
            const response = await this.axios.get<TEntity[]>('',
                {
                    headers: {
                        'Authorization': 'Bearer ' + jwt
                    }
                }
            );

            console.log('get all response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async get(jwt: string, id: string): Promise<TEntity | undefined> {
        try {
            const response = await this.axios.get(id,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('get response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

        
    async create(jwt: string, data: TEntity) {
        try {
            const response = await this.axios.post('', data,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('create response', response);
            if (response.status === 201) {
                return response.data
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

    async edit(jwt: string, data: TEntity, id: string | undefined) {
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
            if (response.status === 201) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

}