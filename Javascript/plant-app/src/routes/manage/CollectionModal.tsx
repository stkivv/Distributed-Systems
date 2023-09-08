import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { JwtContext } from '../Root';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import { IPlantCollection } from '../../domain/IPlantCollection';
import { PlantCollectionService } from '../../services/PlantCollectionService';

interface IProps {
  id?: string,
  collectionName?: string,
}


const CollectionModal = (props: IProps) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const [validationErrors, setValidationErrors] = useState("")

    const [values, setValues] = useState({
        collectionName: "",
    } as IPlantCollection)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const collectionService = new PlantCollectionService();

    useEffect(() => {
      if (props.id && props.collectionName) {
        setValues({ ...values, collectionName: props.collectionName})
      }
    }, [])

    const onSubmit = async (data: IPlantCollection) => {
      if (values.collectionName.length === 0){
        setValidationErrors("Name is mandatory!")
        return;
      } else {
        setValidationErrors("")
      }
        console.log('onSubmit collection', );
        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                if (props.id) {
                  await collectionService.edit(newJwt.jwt, data, props.id)
                } else {
                    await collectionService.create(newJwt.jwt, data)
                }

                setValues({ ...values, collectionName: ""})
            }
            
            handleClose()
        }
    };

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setValues({ ...values, [target.name]: target.value});
    };


    return (
    <>
      <button type="button" onClick={handleShow} className="btn btn-sm btn-outline-primary">{props.id ? <text>Edit</text> : <text>Add new</text>}</button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          {props.id ? <Modal.Title>Edit collection</Modal.Title> : <Modal.Title>Add a new plant collection</Modal.Title> }
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
                <label className="control-label">name of the collection: </label>
                <input 
                value={values.collectionName}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="collectionName"/>
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

export default CollectionModal;