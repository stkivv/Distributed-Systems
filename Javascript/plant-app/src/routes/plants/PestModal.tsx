import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { JwtContext } from '../Root';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import Select from 'react-select';
import { PestTypeService } from '../../services/PestTypeService';
import { PestSeverityService } from '../../services/PestSeverityService';
import { IPestType } from '../../domain/IPestType';
import { IPestSeverity } from '../../domain/IPestSeverity';
import { IPest } from '../../domain/IPest';
import { PestService } from '../../services/PestService';
import Form from "react-bootstrap/Form"

interface IProps{
    plantId: string,
    pestComment?: string,
    pestType?: IPestType,
    pestSeverity?: IPestSeverity,
    pestId?: string
}

const PestModal = (props: IProps) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const [validationErrors, setValidationErrors] = useState("")

    const pestTypeService = new PestTypeService();
    const pestSeverityService = new PestSeverityService();
    const pestService = new PestService();

    const [pestTypes, setPestTypes] = useState([] as IPestType[])
    const [pestSeverities, setPestSeverities] = useState([] as IPestSeverity[])

    useEffect(() => {
        if (!jwtResponse) {
            console.log("not authenticated");
            handleClose();
            return;
        }

        async function getPestTypes() {
            try {
                const response = await pestTypeService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setPestTypes(response);
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
        async function getPestSeverities() {
            try {
                const response = await pestSeverityService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setPestSeverities(response);
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
        getPestTypes();
        getPestSeverities();

    }, [jwtResponse])

    useEffect(() => {
        if (props.pestComment && props.pestSeverity && props.pestType && props.pestId) {
            setValues({ ...values, pestComment: props.pestComment, pestSeverity: props.pestSeverity, pestType: props.pestType});
        }
    }, [])

    const [values, setValues] = useState({
        pestComment: "",
        pestDiscoveryTime: new Date(),
        pestSeverity: null,
        pestType: null
    } as IPest)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    const onSubmit = async (data: IPest) => {
        console.log('onSubmit tag', );

        if(values.pestComment.length === 0 || values.pestDiscoveryTime === null || values.pestSeverity === null || values.pestType === null){
            setValidationErrors("all fields are mandatory!")
            return;
        } else {
            setValidationErrors("")
        }

        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                data.plantId = props.plantId;

                if (props.pestComment && props.pestSeverity && props.pestType) {
                    await pestService.edit(newJwt.jwt, data, props.pestId)
                } else {
                    await pestService.create(newJwt.jwt, data)
                }
                
                setValues({ ...values, pestComment: ""});
            }
            
            handleClose()
        }
    };

    const setType = (id: string) => {
        pestTypes.forEach(type => {
            if (type.id === id) {
                setValues({ ...values, pestType: type})
                return;
            }
        });
    }

    const setSeverity = (id: string) => {
        pestSeverities.forEach(severity => {
            if (severity.id === id) {
                setValues({ ...values, pestSeverity: severity})
                return;
            }
        });
    }

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setValues({ ...values, [target.name]: target.value});
    };

    const setDate = (date: Date) => {
        setValues({ ...values, pestDiscoveryTime: date});
    }

    return (
    <>

      <button type="button" onClick={handleShow} className="btn btn-sm btn-outline-primary">{props.pestId ? <text>Edit</text> : <text>Add new</text>}</button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
            {props.pestId ? <Modal.Title>Edit pest</Modal.Title> : <Modal.Title>Add a new pest</Modal.Title> }
        </Modal.Header>
        <Modal.Body>
        <form>
        {validationErrors !== "" ? 
            <div className="validation-error">
            {validationErrors}
            </div>    
            :
            <div></div>
        }
            <div className="form-group">
                <label className="control-label">Comment</label>
                <input 
                value={values.pestComment}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="pestComment"/>
            </div>
            <br/>
            <div className="form-group">
                <Select<IPestType>
                    options={pestTypes}
                    getOptionLabel={option => option.pestTypeName}
                    onChange={e => setType(e?.id!)}
                    value={values.pestType} inputValue={''} 
                    openMenuOnClick={true}               
                />
            </div>
            <br/>
            <div className="form-group">
                <Select<IPestSeverity>
                    options={pestSeverities}
                    getOptionLabel={option => option.pestSeverityName}
                    onChange={e => setSeverity(e?.id!)}
                    value={values.pestSeverity} inputValue={''} 
                    openMenuOnClick={true}               
                />
            </div>
            <br/>
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

export default PestModal;