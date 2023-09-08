import { MouseEvent, useContext, useState } from "react";
import { JwtContext } from "../../Root";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { useNavigate } from "react-router-dom";
import { EventTypeService } from "../../../services/EventTypeService";
import { IEventType } from "../../../domain/IEventType";

const Create = () => {

    const eventTypeService = new EventTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let navigate = useNavigate();

    const [validationErrors, setValidationErrors] = useState("")

    const onSubmit = async (event: MouseEvent, data: IEventType) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if(values.eventTypeName.length === 0){
            setValidationErrors("name is mandatory!")
            return;
        }

        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);

            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
    
                await eventTypeService.create(newJwt.jwt, data)
            }
        }

        navigate("/admin/eventtypes")
    };

    const [values, setInput] = useState({
        eventTypeName: ""
    } as IEventType);

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
        <h4>Event type</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <form method="post">
                <div className="form-group">
                        <label className="control-label">Name</label>
                        <input 
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" id="eventTypeName" name="eventTypeName"/>
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