import RefreshToken from "../../components/RefreshToken";
import { IOptionsForCreatePlant } from "../../domain/IOptionsForCreatePlant";
import { IPlant } from "../../domain/IPlant";
import { IPlantCollection } from "../../domain/IPlantCollection";
import { ITag } from "../../domain/ITag";
import { IJWTResponse } from "../../dto/IJWTResponse";
import { PlantService } from "../../services/PlantService";
import { JwtContext } from "../Root";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import PhotosModal from "./PhotosModal";
import { PestService } from "../../services/PestService";
import PestModal from "./PestModal";
import { HistoryEntryService } from "../../services/HistoryEntryService";
import HistoryEntryModal from "./HistoryEntryModal";
import { Carousel } from "react-bootstrap";

const Details = () => {

    let {id} = useParams();
    let navigate = useNavigate();

    const plantService = new PlantService();
    const pestService = new PestService();
    const historyEntryService = new HistoryEntryService();
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const [data, setData] = useState({
        plantName: "",
        description: "",
        plantFamily: "",
        scientificName: "",
        sizeCategory: null,
        photos: [],
        historyEntries: [],
        tags: [],
        pests: [],
        reminders: [],
        plantCollections: [],
        appUserId: ""
    } as IPlant);

    useEffect(() => {
        async function getPlant() {
            if (jwtResponse && id !== undefined) {
                try {
                    const response = await plantService.get(jwtResponse.jwt, id);
                    if (response) {
                        setData(response);
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
        getPlant();
    }, [jwtResponse]);

    const deletePlant = async () => {
        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
                await plantService.delete(newJwt.jwt, id)
                navigate("/plants");
            }
        }
    }

    const deleteChild = async (id: string, service: HistoryEntryService | PestService) => {
        if (jwtResponse) {
            const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
            if (newJwt && setJwtResponse) {
                setJwtResponse(newJwt)
                await service.delete(newJwt.jwt, id)
            }
        }
    }
    
    const editPlant = () => {
        if (jwtResponse) {
            const options = {plantCollections: data.plantCollections as IPlantCollection[], tags: data.tags as ITag[]}

            navigate("/plants/create/", {state:{
                id: id, 
                options: options as IOptionsForCreatePlant
            }});
        }
    }

    return (
        <>
        <div className="btn-group">
            <button type="button" className="btn btn-sm btn-outline-danger" onClick={deletePlant}>Delete</button>
            <button type="button" className="btn btn-sm btn-outline-primary" onClick={editPlant}>Edit</button>
            <PhotosModal plantId={id!} photos={data.photos} plantName={data.plantName}></PhotosModal>
        </div>
        <hr></hr>

        {data.photos.at(0) ?
            <Carousel>
                {data.photos.map(function(photo){
                    return(
                        <Carousel.Item className="carousel-item">
                            <img className="d-block w-100 carousel-image" src={photo.imageUrl} alt="..."></img>
                        </Carousel.Item>
                    )
                })}
            </Carousel>
            : <div></div>
        }

        <h1>{data.plantName}</h1>
        <hr></hr>
        <p>{data.description}</p>
        <hr></hr>
        <div className="card">
            <div className="card-header bg-light border-0 rounded-0">
                <button className="btn question col-12" type="button" data-bs-toggle="collapse" data-bs-target="#answer1" aria-expanded="false" aria-controls="answer1">
                    Display scientific information
                </button>
            </div>
            <div className="collapse" id="answer1">
                <div className="card card-body answer">
                    <p>Family: {data.plantFamily}</p>
                    <p>Botanical name: {data.scientificName}</p>
                </div>
            </div>
        </div>

        <hr></hr>
        <h3>Pests <PestModal plantId={id!} /></h3>
        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-3">
            {data.pests.map(function(pest) {
                return   <div className="card shadow-sm pest">
                            <div>
                                <text>{pest.pestComment}</text>
                            </div>
                            <div>
                                <text>Severity: {pest.pestSeverity!.pestSeverityName}</text>
                            </div>
                            <div>
                                <text>Type: {pest.pestType!.pestTypeName}</text>
                            </div>
                            <div>
                                <text>Discovered: {pest.pestDiscoveryTime.toString().substring(0, 10)}</text>
                            </div>
                            <br></br>
                            <div className="btn-group">
                                <PestModal plantId={id!} pestId={pest.id} pestComment={pest.pestComment} pestSeverity={pest.pestSeverity!} pestType={pest.pestType!} />
                                <button type="button" className="btn btn-sm btn-outline-danger" onClick={async() => await deleteChild(pest.id!, pestService)}>Delete</button>
                            </div>
                        </div>
            })}
                </div>
            </div>
        </div>
        <hr></hr>
        <h3>History <HistoryEntryModal plantId={id!}/> </h3>
        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-1 g-3">
                    {data.historyEntries.sort((a, b) => a.entryTime < b.entryTime ? 1 : -1).map((entry) => 
                    <div className="card shadow-sm history-entry">
                        <div>
                            Comment: {entry.entryComment}
                        </div>
                        <div>
                            Type: {entry.historyEntryType!.entryTypeName}
                        </div>
                        <div>
                            Time: {entry.entryTime.toString().substring(0, 10)}
                        </div>
                        <div className="btn-group">
                            <button type="button" className="btn btn-sm btn-outline-danger history-entry-delete" onClick={async() => await deleteChild(entry.id!, historyEntryService)}>Delete</button>
                        </div>
                        
                    </div>
                    )}
                </div>
            </div>
        </div>

        </>
    )
}

export default Details;