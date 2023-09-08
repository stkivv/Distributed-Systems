import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { JwtContext } from '../Root';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import Select from 'react-select';
import Form from "react-bootstrap/Form"
import { HistoryEntryService } from '../../services/HistoryEntryService';
import { HistoryEntryTypeService } from '../../services/HistoryEntryTypeService';
import { IHistoryEntryType } from '../../domain/IHistoryEntryType';
import { IHistoryEntry } from '../../domain/IHistoryEntry';

interface IProps{
    plantId: string
}

const HistoryEntryModal = (props: IProps) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const historyEntryService = new HistoryEntryService();
    const entryTypeService = new HistoryEntryTypeService();

    const [entryTypes, setEntryTypes] = useState([] as IHistoryEntryType[])

    useEffect(() => {
        if (!jwtResponse) {
            console.log("not authenticated");
            handleClose();
            return;
        }

        async function getEntryTypes() {
            try {
                const response = await entryTypeService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setEntryTypes(response);
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

        getEntryTypes();

        if (entryTypes.length === 0) {
            console.log("error - no entry types available")
            handleClose()
        }

    }, [jwtResponse])

    const [values, setValues] = useState({
        entryComment: "",
        entryTime: new Date(),
        historyEntryType: null
    } as IHistoryEntry)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    const onSubmit = async (data: IHistoryEntry) => {
        console.log('onSubmit history entry', );
        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                data.plantId = props.plantId;

                await historyEntryService.create(newJwt.jwt, data)

                setValues({ ...values, entryComment: ""});
            }
            
            handleClose()
        }
    };

    const setType = (id: string) => {
        entryTypes.forEach(type => {
            if (type.id === id) {
                setValues({ ...values, historyEntryType: type})
                return;
            }
        });
    }

    const setDate = (date: Date) => {
        setValues({ ...values, entryTime: date});
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
          <Modal.Title>Add a new history entry</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <form>
            <div className="form-group">
                <label className="control-label">Comment</label>
                <input 
                value={values.entryComment}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="entryComment"/>
            </div>
            <br/>
            <div className="form-group">
                <Select<IHistoryEntryType>
                    options={entryTypes}
                    getOptionLabel={option => option.entryTypeName}
                    onChange={e => setType(e?.id!)}
                    value={values.historyEntryType} inputValue={''} 
                    openMenuOnClick={true}               
                />
            </div>
            <br></br>
            <div className="form-group">
                <Form.Control type="date" onChange={(e) => setDate(new Date(e.target.value))}/>
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

export default HistoryEntryModal;