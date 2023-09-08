import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import Index from "./Index";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { HistoryEntryTypeService } from "../../../services/HistoryEntryTypeService";
import { IHistoryEntryType } from "../../../domain/IHistoryEntryType";

const HistoryEntryType = () => {
    const historyEntryTypeService = new HistoryEntryTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [data, setData] = useState([] as IHistoryEntryType[]);

    useEffect(() => {
        async function getSizes() {
            if (jwtResponse) {
                try {
                    const response = await historyEntryTypeService.getAll(jwtResponse.jwt);
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

export default HistoryEntryType;