import { Link } from "react-router-dom";
import { IPlant } from "../../domain/IPlant";
import { useState } from "react";

interface IProps {
    values: IPlant[];
}


const Index = (props : IProps) =>{

    const [query, setQuery] = useState("")

    return (
        <>
        <h2>Plants</h2>
        <p>
            <Link to="create/">
                <button type="button" className="btn btn-sm btn-outline-primary">Create new</button>
            </Link>
        </p>

        <div>
            <input placeholder="search" className="plant-searchbar" onChange={event => setQuery(event.target.value)}></input>
        </div>

        <hr></hr>

        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                    {props.values.filter(plant => {
                            if (query === '') {
                                return plant;
                              } else if (plant.plantName.toLowerCase().includes(query.toLowerCase())
                                        || plant.description.toLowerCase().includes(query.toLowerCase())) {
                                return plant;
                              }
                        }).map((plant) => 
                        <div className="col">
                            <div className="card shadow-sm">
                                {
                                plant.photos.at(0)?.imageUrl 
                                ? <img className="plant-thumbnail" src={plant.photos.at(0)?.imageUrl} alt={plant.photos.at(0)?.imageDescription} /> 
                                : <svg className="plant-thumbnail" role="img"><rect width="100%" height="100%" fill="#55595c" /><text x="44%" y="50%" fill="#eceeef">No image</text></svg> 
                                }
                                <div className="card-body">
                                    <h2>{plant.plantName}</h2>
                                    <hr/>
                                    <div className="card">
                                        <div className="card-header bg-light border-1 rounded-1">
                                            <button className="btn question col-12" type="button" data-bs-toggle="collapse" data-bs-target={"#answer" + plant.id} aria-expanded="false" aria-controls="answer1">
                                                Show description
                                            </button>
                                        </div>
                                        <div className="collapse" id={"answer" + plant.id}>
                                            <div className="card card-body answer">
                                                <p>{(plant.description !== "") ? plant.description : "None"}</p>
                                            </div>
                                        </div>
                                    </div>
                                    <br/>
                                    <div className="card-text">
                                        <b>Size:</b> {plant.sizeCategory?.sizeName}
                                    </div>
                                    <div className="card-text">
                                        <div className="d-flex gap-0.5 justify-content-left py-0.5">
                                            <div className="col">
                                                <b>Collections:</b> 
                                                {plant.plantCollections.length !== 0 
                                                ? plant.plantCollections.map(function(collection) {
                                                    return  <span>
                                                                <text className="collection badge">{collection.collectionName}</text>
                                                            </span>
                                                })
                                                : <text>None</text>
                                                }
                                            </div>
                                        </div>
                                    </div>
                                    <br/>
                                    <div className="card-text">
                                        <div className="d-flex gap-0.5 justify-content-left py-0.5">
                                            <div className="col">
                                            {plant.tags.map(function(tag) {
                                                return  <span>
                                                            <text className="tag badge" style={{backgroundColor: tag.tagColor!.colorHex}}>{tag.tagLabel} &nbsp;
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-tag-fill" viewBox="0 0 16 16">
                                                                <path d="M2 1a1 1 0 0 0-1 1v4.586a1 1 0 0 0 .293.707l7 7a1 1 0 0 0 1.414 0l4.586-4.586a1 1 0 0 0 0-1.414l-7-7A1 1 0 0 0 6.586 1H2zm4 3.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                                            </svg>
                                                            </text>
                                                        </span>
                                            })}
                                            </div>
                                        </div>
                                    </div>
                                    <br/>
                                    <div className="d-flex justify-content-between align-items-center">
                                        <Link to={"view/" + plant.id}> 
                                            <button type="button" className="btn btn-sm btn-outline-primary">View</button> 
                                        </Link>
                                        <small className="text-body-secondary">{plant.scientificName + ", " + plant.plantFamily}</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}

                </div>
            </div>
        </div>
        </>
    )
}

export default Index;
