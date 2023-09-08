import { IOptionsForCreatePlant } from "../domain/IOptionsForCreatePlant";
import { IOptionsForCreatePlantData } from "../dto/IOptionsForCreatePlantData";
import { BaseService } from "./BaseService";

export class OptionsForCreatePlantService extends BaseService {
    constructor(){
        super('v1/OptionsForCreatePlant');
    }

    async getAll(jwt: string): Promise<IOptionsForCreatePlant | undefined> {
        try {
            const response = await this.axios.get<IOptionsForCreatePlant>('',
                {
                    headers: {
                        'Authorization': 'Bearer ' + jwt
                    }
                }
            );

            console.log('get options response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;
        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

    async create(jwt: string, data: IOptionsForCreatePlantData, plantId: string) {
        if (plantId === undefined){
            return undefined;
        }
        try {
            data.plantId = plantId;
            const response = await this.axios.post('', data,
            {
                headers: {
                    'Authorization': 'Bearer ' + jwt
                }
            });

            console.log('create options response', response);
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