import { IPlant } from "../../domain/IPlant";
import { PlantService } from "../../services/PlantService";
import { MouseEvent, useContext, useState, useEffect } from "react";
import { JwtContext } from "../Root";
import { useLocation, useNavigate } from "react-router-dom";
import RefreshToken from "../../components/RefreshToken";
import { IJWTResponse } from "../../dto/IJWTResponse";
import { OptionsForCreatePlantService } from "../../services/OptionsForCreatePlantService";
import { IOptionsForCreatePlant } from "../../domain/IOptionsForCreatePlant";
import { ITag } from "../../domain/ITag";
import { IOptionsForCreatePlantData } from "../../dto/IOptionsForCreatePlantData";
import { IPlantCollection } from "../../domain/IPlantCollection";

const CreatePlant = () => {

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [validationErrors, setValidationErrors] = useState("")

    const plantService = new PlantService();

    let navigate = useNavigate();

    const location = useLocation();

    const optionsService = new OptionsForCreatePlantService()

    const [options, setOptions] = useState({
        tags: [],
        sizeCategories: [],
        plantCollections: []
    } as IOptionsForCreatePlant)

    useEffect(() => {
        async function getOptions() {
            if (jwtResponse) {
                try {
                    const response = await optionsService.getAll(jwtResponse.jwt);
                    console.log(response);
                    if (response) {
                        setOptions(response);
                    } else {
                        const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
                        if (newJwt && setJwtResponse) {
                            setJwtResponse(newJwt)
                        }
                    }
                } catch (error) {
                    console.log(error)
                }
            }
        }
        getOptions();
    }, [jwtResponse]);


    useEffect(() => {
        async function initOldValues() {
            console.log("attempting to init values...")
            if (!jwtResponse) {
                return;
            }

            const options: IOptionsForCreatePlant = location.state.options;
            setTags(options.tags);
            setCollections(options.plantCollections);
    
            let plant = await plantService.get(jwtResponse.jwt, location.state.id);
            if (!plant) {
                console.log("Failed to init values")
                return;
            } else {
                setInput({...values, 
                    plantName: plant.plantName, 
                    description: plant.description,
                    plantFamily: plant.plantFamily,
                    scientificName: plant.scientificName,
                    sizeCategory: plant.sizeCategory,
                })
                return;
            }
        }

        if (location.state) {
            initOldValues();
        }
    }, []);


    const [values, setInput] = useState({
        plantName: "",
        description: "",
        plantFamily: "",
        scientificName: "",
        sizeCategory: null
    } as IPlant);

    const handleChange = (target: EventTarget & HTMLInputElement | 
        EventTarget & HTMLSelectElement | 
        EventTarget & HTMLTextAreaElement) =>{
        setInput({ ...values, [target.name]: target.value});
    };

    const [selectedTags,setTags] = useState([] as ITag[]);
    const [selectedCollections,setCollections] = useState([] as IPlantCollection[]);

    const setSize = (sizeId : string) => {
        options.sizeCategories.forEach(s => {
            if (s.id === sizeId) {
                setInput({ ...values, sizeCategory: s});
            }
        })
    }

    //https://stackoverflow.com/questions/61986464/react-checkbox-if-checked-add-value-to-array
    const tagHandleChange = (target: EventTarget & HTMLInputElement) =>{
        var element: ITag | undefined;
        options.tags.forEach(e => {
            if (e.id === target.value) {
                element = e;
            }
        });
        var selected_array = [...selectedTags];
        if (target.checked) {
            if (element) {
                selected_array = [...selectedTags, element];
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
        setTags(selected_array);
    };

    const collectionsHandleChange = (target: EventTarget & HTMLInputElement) =>{
        var element: IPlantCollection | undefined;
        options.plantCollections.forEach(e => {
            if (e.id === target.value) {
                element = e;
            }
        });
        var selected_array = [...selectedCollections];
        if (target.checked) {
            if (element) {
                selected_array = [...selectedCollections, element];
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
        setCollections(selected_array);
    };


    const onSubmit = async (event: MouseEvent, data: IPlant) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if(values.plantName.length === 0 || values.sizeCategory === null){
            setValidationErrors("name and category are mandatory!")
            return;
        }

        if (jwtResponse){
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)

                //forced to use new jwt variable because setter doesnt update right away
                var plant: IPlant;
                if (location.state) {
                    plant = await plantService.edit(newJwt.jwt, data, location.state.id)
                } else {
                    plant = await plantService.create(newJwt.jwt, data)
                }

                const selectedOptions: IOptionsForCreatePlantData = {
                    tags: selectedTags,
                    plantCollections: selectedCollections
                }

                if (plant) {
                    await optionsService.create(newJwt.jwt, selectedOptions, plant.id!)
                } else {
                    console.log("ERROR! options couldnt be created")
                }
            }
            
            navigate("/plants");
        }
    };

    return (
        <>
        {location.state ? <h4>Edit plant</h4> : <h4>New plant</h4>}
        <hr />
        {validationErrors !== "" ? 
            <div className="validation-error">
            {validationErrors}
            </div>    
            :
            <div></div>
        }

        <div className="row">
            <div className="col-md-5">
                <form method="post">

                    <div className="form-group">
                        <label className="control-label">Name</label>
                        <input 
                        value={values.plantName}
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" name="plantName"/>
                    </div>

                    <br/>

                    <div className="form-group">
                        <label className="control-label">Description</label>
                        <textarea 
                        value={values.description}
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        name="description"/>
                    </div>

                    <br/>

                    <div className="form-group">
                        <label className="control-label">Botanical name</label>
                        <input 
                        value={values.scientificName}
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" name="scientificName"/>
                    </div>

                    <br/>

                    <div className="form-group">
                        <label className="control-label">Plant family</label>
                        <input 
                        value={values.plantFamily}
                        className="form-control" 
                        onChange={(e) => handleChange(e.target)} 
                        type="text" name="plantFamily"/>
                    </div>

                    <br/>

                    <div className="form-group">
                        <label className="control-label">Choose a size: </label>
                        <select 
                        value={values.sizeCategory?.id}
                        onChange={(e) => setSize(e.target.value)} 
                        name="sizeCategoryId"
                        className="form-select"
                        >
                            <option selected>Choose...</option>
                            {options.sizeCategories.map((size) => 
                                <option value={size.id}>{size.sizeName}</option>
                            )}
                        </select> 
                    </div>

                    <br/>

                    <div className="form-group">
                        <h5 className="control-label">Choose tags to add: </h5>
                        {options.tags.map((tag) => 
                        <div>
                            <input 
                            type="checkbox"
                            onChange={(e) => tagHandleChange(e.target)} 
                            name={tag.tagLabel}
                            className="form-tag-check"
                            value={tag.id}
                            checked={selectedTags.some(s => tag.id === s.id)}
                            /> &nbsp;
                            <label className="control-label tag" style={{backgroundColor: tag.tagColor!.colorHex}} htmlFor={tag.tagLabel}> {tag.tagLabel} &nbsp;
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-tag-fill" viewBox="0 0 16 16">
                                    <path d="M2 1a1 1 0 0 0-1 1v4.586a1 1 0 0 0 .293.707l7 7a1 1 0 0 0 1.414 0l4.586-4.586a1 1 0 0 0 0-1.414l-7-7A1 1 0 0 0 6.586 1H2zm4 3.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                </svg>
                            </label>
                        </div>
                        )}
                    </div>

                    <br/>

                    <div className="form-group">
                        <h5 className="control-label">Choose collections to include in: </h5>
                        {options.plantCollections.map((collection) => 
                        <div>
                            <input 
                            type="checkbox"
                            onChange={(e) => collectionsHandleChange(e.target)} 
                            name={collection.collectionName}
                            className="form-tag-check"
                            value={collection.id}
                            checked={selectedCollections.some(s => collection.id === s.id)}
                            /> &nbsp;
                            <label className="control-label collection" htmlFor={collection.collectionName}> {collection.collectionName} </label>
                        </div>
                        )}
                    </div>
                    <br/>
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



export default CreatePlant;