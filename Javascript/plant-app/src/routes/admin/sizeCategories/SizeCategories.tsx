import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import Index from "./Index";
import { SizeCategoryService } from "../../../services/SizeCategoryService";
import { ISizeCategory } from "../../../domain/ISizeCategory";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";

const SizeCategories = () => {
    const sizeCategoryService = new SizeCategoryService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [data, setData] = useState([] as ISizeCategory[]);

    useEffect(() => {
        async function getSizes() {
            if (jwtResponse) {
                try {
                    const response = await sizeCategoryService.getAll(jwtResponse.jwt);
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

export default SizeCategories;