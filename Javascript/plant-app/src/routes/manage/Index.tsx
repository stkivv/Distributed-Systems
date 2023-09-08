import { Link } from "react-router-dom";
import { IPlantCollection } from "../../domain/IPlantCollection";
import { IReminder } from "../../domain/IReminder";
import { ITag } from "../../domain/ITag";
import TagModal from "./TagModal";
import CollectionModal from "./CollectionModal";
import ReminderModal from "./ReminderModal";

interface IProps {
    values: {    
        tags: ITag[],
        collections: IPlantCollection[],
        reminders: IReminder[]
    },
    onDelete: (item: ITag | IPlantCollection | IReminder, type: string) => void
}


const Index = (props : IProps) =>{
    return (
        <>
        <div>
            <h1>
                My tags &nbsp;
                <TagModal/>
            </h1>
        </div>
        <br></br>

        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-6 g-3">
            {props.values.tags.map(function(tag) {
                return   <div className="card shadow-sm manage-list-item">
                            <span>
                                <text className="tag" style={{backgroundColor: tag.tagColor!.colorHex}}>{tag.tagLabel} &nbsp;
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-tag-fill" viewBox="0 0 16 16">
                                    <path d="M2 1a1 1 0 0 0-1 1v4.586a1 1 0 0 0 .293.707l7 7a1 1 0 0 0 1.414 0l4.586-4.586a1 1 0 0 0 0-1.414l-7-7A1 1 0 0 0 6.586 1H2zm4 3.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                </svg>
                                </text>
                            </span>
                            <br></br>
                            <div className="btn-group">
                                <TagModal tagLabel={tag.tagLabel} tagColor={tag.tagColor!} id={tag.id} />
                                <button type="button" onClick={() => props.onDelete(tag, "tag")} className="btn btn-sm btn-outline-danger">Delete</button>
                            </div>
                        </div>
            })}
                </div>
            </div>
        </div>

        <hr></hr>

        <div>
            <h1>
                My collections &nbsp;
                <CollectionModal/>
            </h1>
        </div>
        <br></br>
        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-6 g-3">
            {props.values.collections.map(function(collection) {
                return   <div className="card shadow-sm manage-list-item">
                            <span>
                                <text className="collection">{collection.collectionName} &nbsp;
                                </text>
                                &nbsp;
                            </span>
                            <br></br>
                            <div className="btn-group">
                                <CollectionModal id={collection.id} collectionName={collection.collectionName} />
                                <button type="button" onClick={() => props.onDelete(collection, "plantCollection")} className="btn btn-sm btn-outline-danger">Delete</button>
                            </div>
                        </div>
                        
            })}
                </div>
            </div>
        </div>
        
        <hr></hr>

        <div>
            <h1>
                My reminders &nbsp;
                <ReminderModal/>
            </h1>
        </div>
        <br></br>
        <div className="album py-5 bg-body-tertiary">
            <div className="container">
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-6 g-3">
            {props.values.reminders.map(function(reminder) {
                return   <div className="card shadow-sm manage-list-item">
                            <span>
                                <text className="reminder">{reminder.reminderMessage} &nbsp;
                                </text>
                                &nbsp;
                            </span>
                            <br></br>
                            <div className="btn-group">
                                <button type="button" onClick={() => props.onDelete(reminder, "reminder")} className="btn btn-sm btn-outline-danger">Delete</button>
                            </div>
                        </div>
            })}
                </div>
            </div>
        </div>
        
        </>
    )
}

export default Index;
