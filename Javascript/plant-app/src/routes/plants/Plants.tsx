import { useContext, useEffect, useState } from "react";
import { PlantService } from "../../services/PlantService";
import { JwtContext } from "../Root";
import Index from "./Index";
import { IPlant } from "../../domain/IPlant";
import RefreshToken from "../../components/RefreshToken";
import { IJWTResponse } from "../../dto/IJWTResponse";

const Plants = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [data, setData] = useState([] as IPlant[]);

    useEffect(() => {
        const plantService = new PlantService();
        async function getPlants() {
            if (jwtResponse) {
                try {
                    const response = await plantService.getAll(jwtResponse.jwt);
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
        getPlants();
    }, [jwtResponse]);


    return (
        <Index values={data}/>
    );
}

export default Plants;