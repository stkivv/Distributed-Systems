import { MouseEvent, useContext, useState } from "react";
import { JwtContext } from "../../Root";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { useNavigate } from "react-router-dom";
import { TagColorService } from "../../../services/TagColorService";
import { ITagColor } from "../../../domain/ITagColor";

const Create = () => {

    const tagColorService = new TagColorService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let navigate = useNavigate();

    const [validationErrors, setValidationErrors] = useState("")

    const onSubmit = async (event: MouseEvent, data: ITagColor) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if(values.colorName.length === 0 || values.colorHex.length === 0){
            setValidationErrors("name and hex are mandatory!")
            return;
        }

        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);

            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
    
                await tagColorService.create(newJwt.jwt, data)
            }
        }

        navigate("/admin/tagcolors")
    };

    const [values, setInput] = useState({
        colorName: "",
        colorHex: ""
    } as ITagColor);

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setInput({ ...values, [target.name]: target.value});
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
        <h4>Tag color</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <form method="post">
                <div className="form-group">
                        <label className="control-label">Name</label>
                        <input 
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" id="colorName" name="colorName"/>
                    </div>
                    <div className="form-group">
                        <label className="control-label">Color hex</label>
                        <input 
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" id="colorHex" name="colorHex"/>
                    </div>
                    <div className="form-group">
                        <input
                        onClick={(e) => onSubmit(e, values)}
                        type="submit" value="Create" className="btn btn-primary"/>
                    </div>
                </form>
            </div>
        </div>
        </>
    );
}



export default Create;