import { MouseEvent, useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import { useNavigate, useParams } from "react-router-dom";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import RefreshToken from "../../../components/RefreshToken";
import { IEventType } from "../../../domain/IEventType";
import { ReminderTypeService } from "../../../services/ReminderTypeService";
import { IReminderType } from "../../../domain/IReminderType";
import { EventTypeService } from "../../../services/EventTypeService";
import Select from 'react-select';

const Edit = () =>{
    const reminderTypeService = new ReminderTypeService();
    const eventTypeService = new EventTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let navigate = useNavigate();

    let {id} = useParams();

    const [validationErrors, setValidationErrors] = useState("")

    const [eventTypes, setEventTypes] = useState([] as IEventType[])

    useEffect(() => {
        if (!jwtResponse) {
            return;
        }

        async function getEventTypes() {
            try {
                const response = await eventTypeService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setEventTypes(response);
                } else {
                    const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse!);
                    if (newJwt && setJwtResponse) {
                        setJwtResponse(newJwt)
                    }
                }
            } catch (error) {
                console.log(error)
            }
        }

        getEventTypes();

    }, [jwtResponse])


    const onSubmit = async (event: MouseEvent, data: IReminderType) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if(values.reminderTypeName.length === 0 || values.eventTypeId === ""){
            setValidationErrors("name and eventtype are mandatory!")
            return;
        }

        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);

            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
    
                await reminderTypeService.edit(newJwt.jwt, data, id)
            }
        }

        navigate("/admin/remindertypes")
    };

    const [values, setInput] = useState({
        reminderTypeName: "",
        eventTypeId: ""
    } as IReminderType);

    const setType = (id: string) => {
        eventTypes.forEach(type => {
            if (type.id === id) {
                setInput({ ...values, eventTypeId: id})
                return;
            }
        });
    }


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
        <h4>Reminder type</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <form>
                    <div className="form-group">
                        <label className="control-label">Name</label>
                        <input 
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" id="reminderTypeName" name="reminderTypeName"/>
                    </div>
                    <div className="form-group">
                        <Select<IEventType>
                            options={eventTypes}
                            getOptionLabel={option => option.eventTypeName}
                            onChange={e => setType(e?.id!)}
                            inputValue={''} 
                            openMenuOnClick={true}               
                        />
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



export default Edit;
