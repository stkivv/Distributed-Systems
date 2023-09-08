import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import Index from "./Index";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { ReminderTypeService } from "../../../services/ReminderTypeService";
import { IReminderType } from "../../../domain/IReminderType";

const ReminderType = () => {
    const reminderTypeService = new ReminderTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [data, setData] = useState([] as IReminderType[]);

    useEffect(() => {
        async function getSizes() {
            if (jwtResponse) {
                try {
                    const response = await reminderTypeService.getAll(jwtResponse.jwt);
                    console.log(response);
                    if (response) {
                        setData(response);
                    } else {
                        const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
                        if (newJwt && setJwtResponse) {
                            setJwtResponse(newJwt)
                        }
                        setData([]);
                    }
                } catch (error) {
                    console.log(error)
                }
            }
        }
        getSizes();
    }, [jwtResponse]);


    return (
        <Index values={data}/>
    );
}

export default ReminderType;