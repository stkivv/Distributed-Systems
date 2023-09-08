
import { IdentityService } from "../services/IdentityService";
import { IJWTResponse } from "../dto/IJWTResponse";


const RefreshToken = async (jwtResponse: IJWTResponse) =>  {

    if (jwtResponse) {
        const identityService = new IdentityService();
        const updatedData : IJWTResponse | undefined = await identityService.refreshToken(jwtResponse);

        return updatedData;
    }
}

export default RefreshToken;