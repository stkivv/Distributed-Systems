import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import Index from "./Index";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { IEventType } from "../../../domain/IEventType";
import { EventTypeService } from "../../../services/EventTypeService";

const EventType = () => {
    const eventTypeService = new EventTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [data, setData] = useState([] as IEventType[]);

    useEffect(() => {
        async function getSizes() {
            if (jwtResponse) {
                try {
                    const response = await eventTypeService.getAll(jwtResponse.jwt);
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

export default EventType;