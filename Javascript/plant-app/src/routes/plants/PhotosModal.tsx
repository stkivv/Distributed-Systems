import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { JwtContext } from '../Root';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import { IPhoto } from '../../domain/IPhoto';
import { PhotoService } from '../../services/PhotoService';

interface IProps {
    plantId: string,
    photos: IPhoto[],
    plantName: string
}

const PhotosModal = (props: IProps) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const [currentImages, setImages] = useState([] as IPhoto[])

    useEffect(() => {
        setImages(props.photos)
    }, [props.photos])

    const [values, setValues] = useState({
        imageUrl: "",
        imageDescription: "",
    } as IPhoto)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const photoService = new PhotoService();

    const onDelete = async (photoId: string) => {
        console.log('onDelete photo', );
        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                await photoService.delete(newJwt.jwt, photoId)

                var imgArray = [...currentImages]
                var indx; 
                for (let index = 0; index < imgArray.length; index++) {
                    if (photoId === imgArray[index].id) {
                        indx = index;
                        return;
                    }
                }
                if (indx !== undefined) {
                    imgArray.splice(indx, 1);
                }
                setImages(imgArray)
            }
            
            handleClose()
        }   
    }

    const onSubmit = async (data: IPhoto) => {
        console.log('onSubmit photo', );
        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                data.plantId = props.plantId
                data.imageDescription = props.plantName + " image"

                await photoService.create(newJwt.jwt, data)

                setImages(currentImages => [...currentImages, data])

                setValues({ ...values, imageUrl: "", imageDescription: ""})
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

    <button type="button" onClick={handleShow} className="btn btn-sm btn-outline-primary">Manage photos</button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Manage photos for this plant</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <h3>Current images:</h3>
            <ul>
                {currentImages.map(function(image){
                if (image.imageUrl.length > 100) {
                    return  <div>
                                <li className='photo-link'>
                                    {image.imageUrl.substring(0, 99) + "..."}
                                </li>
                                <Button variant="danger" onClick={async () => await onDelete(image.id!)}>Delete</Button>
                                <br></br>
                                <br></br>
                            </div>
                }
                return  <div>
                            <li className='photo-link'>
                                {image.imageUrl}
                            </li>
                            <Button variant="danger" onClick={async () =>  await onDelete(image.id!)}>Delete</Button>
                            <br></br>
                            <br></br>
                        </div>
                })}
            </ul>
        <hr/>
        <form>
            <div className="form-group">
                <label className="control-label">URL of new image to add</label>
                <input 
                value={values.imageUrl}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="imageUrl"/>
            </div>
        </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={async() => await onSubmit(values)}>
            Add
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default PhotosModal;