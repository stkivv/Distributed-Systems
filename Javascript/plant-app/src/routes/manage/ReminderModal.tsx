import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { JwtContext } from '../Root';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import Select from 'react-select';
import { ReminderService } from '../../services/ReminderService';
import { PlantService } from '../../services/PlantService';
import { IPlant } from '../../domain/IPlant';
import { IReminder } from '../../domain/IReminder';
import { IReminderActiveMonth } from '../../domain/IReminderActiveMonth';
import { IReminderType } from '../../domain/IReminderType';
import { ReminderTypeService } from '../../services/ReminderTypeService';
import { MonthService } from '../../services/MonthService';
import { IMonth } from '../../domain/IMonth';

const ReminderModal = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const [validationErrors, setValidationErrors] = useState("")

    const reminderService = new ReminderService();
    const plantService = new PlantService();
    const reminderTypeService = new ReminderTypeService();
    const monthService = new MonthService();

    const [plants, setPlants] = useState([] as IPlant[])
    const [reminderTypes, setReminderTypes] = useState([] as IReminderType[])
    const [months, setMonths] = useState([] as IMonth[])
    const [selectedMonths, setSelectedMonths] = useState([] as IMonth[])

    useEffect(() => {
        if (!jwtResponse) {
            console.log("not authenticated");
            handleClose();
            return;
        }

        async function getPlants() {
            try {
                const response = await plantService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setPlants(response);
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

        async function getTypes() {
            try {
                const response = await reminderTypeService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setReminderTypes(response);
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

        async function getMonths() {
            try {
                const response = await monthService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setMonths(response);
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

        getPlants();
        getTypes();
        getMonths();

        if (plants.length === 0){
            console.log("error: no plants available - cannot make reminder");
            handleClose();
            return;
        }
        if (reminderTypes.length === 0){
            console.log("error: no types available - cannot make reminder");
            handleClose();
            return;
        }
        if (months.length !== 12){
            console.log("error: invalid number of months - cannot make reminder");
            handleClose();
            return;
        }

    }, [jwtResponse])

    const [values, setValues] = useState({
        reminderMessage: "",
        plantId: "",
        reminderActiveMonths: [] as IReminderActiveMonth[],
        reminderType: null
    } as IReminder)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const monthHandleChange = (target: EventTarget & HTMLInputElement) =>{
        var element: IMonth | undefined;
        months.forEach(e => {
            if (e.id === target.value) {
                element = e;
            }
        });
        var selected_array = [...selectedMonths];
        if (target.checked) {
            if (element) {
                selected_array = [...selectedMonths, element];
            }
        } else {
            if (element) {
                var indx;
                for (let index = 0; index < selected_array.length; index++) {
                    if (element.id === selected_array[index].id){
                        indx = index;
                        break;
                    }
                }
                if (indx !== undefined) {
                    selected_array.splice(indx, 1);
                }
            }
        }
        setSelectedMonths(selected_array);
    };


    const onSubmit = async (data: IReminder) => {
        console.log('onSubmit reminder', );
        data.months = selectedMonths

        if(values.reminderMessage.length === 0 || values.reminderType === null || values.plantId.length === 0){
            setValidationErrors("message, plant and type are mandatory!")
            return;
        } else {
            setValidationErrors("")
        }

        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                await reminderService.create(newJwt.jwt, data)

                setValues({ ...values, reminderMessage: "", plantId: "", reminderActiveMonths: []})
            }
            
            handleClose()
        }
    };

    const setPlant = (id: string) => {
        setValues({ ...values, plantId: id})
    }

    const setType = (id: string) => {
        reminderTypes.forEach(type => {
            if (type.id === id) {
                setValues({ ...values, reminderType: type})
                return;
            }
        });
    }

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setValues({ ...values, [target.name]: target.value});
    };


    return (
    <>

    <button type="button" onClick={handleShow} className="btn btn-sm btn-outline-primary">Add new</button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add a new tag</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        {validationErrors !== "" ? 
            <div className="validation-error">
            {validationErrors}
            </div>    
            :
            <div></div>
        }
        <form>
            <div className="form-group">
                <label className="control-label">Reminder message:</label>
                <input 
                value={values.reminderMessage}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="reminderMessage"/>
            </div>
            <br/>
            <div className="form-group">
                <text>Choose plant</text>
                <Select<IPlant>
                    options={plants}
                    getOptionLabel={option => option.plantName}
                    onChange={e => setPlant(e?.id!)}
                    inputValue={''} 
                    openMenuOnClick={true}               
                />
            </div>
            <br></br>
            <div className="form-group">
                <text>Choose reminder type</text>
                <Select<IReminderType>
                    options={reminderTypes}
                    getOptionLabel={option => option.reminderTypeName}
                    onChange={e => setType(e?.id!)}
                    inputValue={''} 
                    openMenuOnClick={true}               
                />
            </div>
            <br></br>
            <div className="form-group">
                <text>Choose active months for reminder:</text>
                {months
                .sort((a, b) => a.monthNr > b.monthNr ? 1 : -1)
                .map((month) => 
                    <div>
                        <input 
                        type="checkbox"
                        onChange={(e) => monthHandleChange(e.target)} 
                        name={month.monthName}
                        className="form-month-check"
                        value={month.id}
                        checked={selectedMonths.some(s => month.id === s.id)}
                        /> &nbsp;
                        <label className="control-label"> {month.monthName}</label>
                    </div>
                )}
            </div>
        </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={() => onSubmit(values)}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ReminderModal;