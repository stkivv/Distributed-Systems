import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../Root";
import RefreshToken from "../../components/RefreshToken";
import { IJWTResponse } from "../../dto/IJWTResponse";
import { TagService } from "../../services/TagService";
import Index from "./Index";
import { ITag } from "../../domain/ITag";
import { IPlantCollection } from "../../domain/IPlantCollection";
import { IReminder } from "../../domain/IReminder";
import { PlantCollectionService } from "../../services/PlantCollectionService";
import { ReminderService } from "../../services/ReminderService";

interface IProps {
    tags: ITag[],
    collections: IPlantCollection[],
    reminders: IReminder[]
}

const Manage = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [values, setValues] = useState({
        tags: [],
        collections: [],
        reminders: []
    } as IProps);

    const tagService = new TagService();
    const plantCollectionService = new PlantCollectionService();
    const reminderService = new ReminderService();

    const deleteItem = async (item: ITag | IPlantCollection | IReminder, type: string) => {
        if (!jwtResponse) {
            return;
        }
        await getRefreshToken();

        if (type === "tag"){
            await tagService.delete(jwtResponse.jwt, item.id)
        }
        else if (type === "plantCollection"){
            await plantCollectionService.delete(jwtResponse.jwt, item.id)
        }
        else if (type === "reminder"){
            await reminderService.delete(jwtResponse.jwt, item.id)
        }

        getValues();
        return;
    }

    async function getRefreshToken() {
        const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse!);
        if (newJwt && setJwtResponse) {
            setJwtResponse(newJwt)
        }   
    }

    async function getValues() {
        try {
            const responseTag = await tagService.getAll(jwtResponse!.jwt);
            console.log(responseTag);
            if (responseTag) {
                setValues(prev => ({...prev, tags: responseTag}));
            } else {
                await getRefreshToken();
                return;
            }

            const responseColl = await plantCollectionService.getAll(jwtResponse!.jwt);
            console.log(responseColl);
            if (responseColl) {
                setValues(prev => ({...prev, collections: responseColl}));
            } else {
                await getRefreshToken();
                return;
            }

            const responseReminder = await reminderService.getAll(jwtResponse!.jwt);
            console.log(responseReminder);
            if (responseReminder) {
                setValues(prev => ({...prev, reminders: responseReminder}));
            } else {
                await getRefreshToken();
                return;
            }
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        if (!jwtResponse) {
            return;
        }
        getValues();
    }, [jwtResponse]);


    return (
        <Index values={values} onDelete={deleteItem}/>
    );
}

export default Manage;