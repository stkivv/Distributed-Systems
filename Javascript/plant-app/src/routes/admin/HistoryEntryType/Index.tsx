import { Link } from "react-router-dom";
import { IHistoryEntryType } from "../../../domain/IHistoryEntryType";

interface IProps {
    values: IHistoryEntryType[];

}


const Index = (props : IProps) =>{

    return (
        <>
        <p>
            <Link to="create/">Create new</Link>
        </p>
        <table className="table">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {props.values.map((i) => 
                    <tr key={i.id}>
                    <td>
                        {i.entryTypeName}
                    </td>
                    <td>
                        <Link to={"delete/" + i.id}>Delete</Link> | 
                        <Link to={"edit/" + i.id}>Edit</Link> | 
                    </td>
                    </tr>
                )}
            </tbody>
        </table>
        </>
    )
}

export default Index;
