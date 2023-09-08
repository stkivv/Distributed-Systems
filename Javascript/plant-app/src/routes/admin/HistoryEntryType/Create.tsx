import { MouseEvent, useContext, useEffect, useState } from "react";
import { JwtContext } from "../../Root";
import RefreshToken from "../../../components/RefreshToken";
import { IJWTResponse } from "../../../dto/IJWTResponse";
import { useNavigate } from "react-router-dom";
import { EventTypeService } from "../../../services/EventTypeService";
import { IEventType } from "../../../domain/IEventType";
import Select from 'react-select';
import { HistoryEntryTypeService } from "../../../services/HistoryEntryTypeService";
import { IHistoryEntryType } from "../../../domain/IHistoryEntryType";

const Create = () => {

    const eventTypeService = new EventTypeService();
    const historyEntryTypeService = new HistoryEntryTypeService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let navigate = useNavigate();

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

    const onSubmit = async (event: MouseEvent, data: IHistoryEntryType) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if(values.entryTypeName.length === 0 || values.eventTypeId === ""){
            setValidationErrors("name and eventtype are mandatory!")
            return;
        }

        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);

            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
    
                await historyEntryTypeService.create(newJwt.jwt, data)
            }
        }

        navigate("/admin/historyentrytypes")
    };

    const [values, setInput] = useState({
        entryTypeName: "",
        eventTypeId: ""
    } as IHistoryEntryType);

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setInput({ ...values, [target.name]: target.value});
    };

    const setType = (id: string) => {
        eventTypes.forEach(type => {
            if (type.id === id) {
                setInput({ ...values, eventTypeId: id})
                return;
            }
        });
    }

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
                        type="text" id="entryTypeName" name="entryTypeName"/>
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



export default Create;