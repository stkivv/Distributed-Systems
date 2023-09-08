import { useContext, useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { TagService } from '../../services/TagService';
import { JwtContext } from '../Root';
import { ITag } from '../../domain/ITag';
import { ITagColor } from '../../domain/ITagColor';
import { TagColorService } from '../../services/TagColorService';
import RefreshToken from '../../components/RefreshToken';
import { IJWTResponse } from '../../dto/IJWTResponse';
import Select from 'react-select';

interface IProps {
    id?: string,
    tagLabel?: string,
    tagColor?: ITagColor
}

const TagModal = (props: IProps) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const [show, setShow] = useState(false);

    const [validationErrors, setValidationErrors] = useState("")

    const tagColorService = new TagColorService();
    const [tagColors, setTagColors] = useState([] as ITagColor[])

    useEffect(() => {
        if (!jwtResponse) {
            console.log("not authenticated");
            handleClose();
            return;
        }

        async function getTagColors() {
            try {
                const response = await tagColorService.getAll(jwtResponse!.jwt);
                console.log(response);
                if (response) {
                    setTagColors(response);
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
        getTagColors();

        if (tagColors.length === 0){
            console.log("error: no colors available - cannot make tag");
            handleClose();
            return;
        }

    }, [jwtResponse])

    useEffect(() => {
        if (props.id && props.tagLabel && props.tagColor) {
            setValues({ ...values, tagLabel: props.tagLabel, tagColor: props.tagColor})
        }
    }, [])

    const [values, setValues] = useState({
        tagLabel: "",
        tagColor: null
    } as ITag)

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const tagService = new TagService();

    const onSubmit = async (data: ITag) => {
        console.log('onSubmit tag', );

        if(values.tagLabel.length === 0 || values.tagColor === null){
            setValidationErrors("label and color are mandatory!")
            return;
        } else {
            setValidationErrors("")
        }

        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                if (props.id) {
                    await tagService.edit(newJwt.jwt, data, props.id)
                } else {
                    await tagService.create(newJwt.jwt, data)
                }

                setValues({ ...values, tagLabel: "", tagColor: tagColors.at(0)!})
            }
            
            handleClose()
        }
    };

    const setColor = (id: string) => {
        tagColors.forEach(color => {
            if (color.id === id) {
                setValues({ ...values, tagColor: color})
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

    <button type="button" onClick={handleShow} className="btn btn-sm btn-outline-primary">{props.id ? <text>Edit</text> : <text>Add new</text>}</button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          {props.id ? <Modal.Title>Edit tag</Modal.Title> : <Modal.Title>Add a new tag</Modal.Title> }
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
                <label className="control-label">Label</label>
                <input 
                value={values.tagLabel}
                className="form-control" 
                onChange={(e) => handleChange(e.target)} 
                type="text" name="tagLabel"/>
            </div>
            <br/>
            <div className="form-group">
                <text>Choose a color</text>
                <Select<ITagColor>
                    options={tagColors}
                    getOptionLabel={option => option.colorName}
                    onChange={e => setColor(e?.id!)}
                    value={values.tagColor} inputValue={''} 
                    openMenuOnClick={true}               
                />
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

export default TagModal;