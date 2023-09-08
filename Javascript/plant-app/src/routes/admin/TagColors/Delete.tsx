import { Link, useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../Root";
import { MouseEvent, useContext, useState } from "react";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import RefreshToken from "../../../components/RefreshToken";
import { TagColorService } from "../../../services/TagColorService";

const Delete = () =>{

    let {id} = useParams();

    const [validationErrors, setValidationErrors] = useState("")

    const tagColorService = new TagColorService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let navigate = useNavigate();
    

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit', event);
        event.preventDefault();


        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);

            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
    
                var result: undefined | true = await tagColorService.delete(newJwt.jwt, id)

                if (result === undefined) {
                    setValidationErrors("Failed to delete object (possibly violating FK constraint? Cascade delete is OFF)")
                    return;
                }
            }
        }

        navigate("/admin/tagcolors")
    };

    return (
        <>
        {validationErrors !== "" ? 
            <div className="validation-error">
            {validationErrors}
            </div>    
            :
            <div></div>
        }
        <h3>Are you sure you want to delete?</h3>
        <div>
            
            <form method="post">
                <input
                onClick={(e) => onSubmit(e)}
                type="submit" value="Delete" className="btn btn-danger" /> |
                <Link to="/admin/tagcolors">Back</Link>
            </form>
        </div>
        </>
    )
}

export default Delete;
