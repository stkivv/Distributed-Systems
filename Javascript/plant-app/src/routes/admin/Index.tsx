import { useContext } from "react";
import { Link } from "react-router-dom";
import { JwtContext } from "../Root";
import jwt_decode from "jwt-decode";


const Index = () =>{

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let userRole: string | null = null;

    //https://stackoverflow.com/questions/57353664/how-can-get-claims-parameter-from-a-decoed-jwt
    if (jwtResponse) {
        let jwtObject: any = jwt_decode(jwtResponse.jwt);
        Object.keys(jwtObject).forEach(function (key) {
            let res = key.split("/")
            if (res.length > 1) {
                if (res[res.length - 1] === 'role') {
                    userRole = jwtObject[key]
                }
            }
        })
    }

    return (
        userRole === "Admin" 
        ? 
        <>
        <h2>Admin options</h2>
        <hr></hr>
        <h3>Size categories</h3>
        <Link to="sizecategories/" >View</Link>
        <hr></hr>
        <h3>Tag colors</h3>
        <Link to="tagcolors/" >View</Link>
        <hr></hr>
        <h3>Pest types</h3>
        <Link to="pesttype/" >View</Link>
        <hr></hr>
        <h3>Pest severities</h3>
        <Link to="pestseverities/" >View</Link>
        <hr></hr>
        <h3>Event types</h3>
        <Link to="eventtypes/" >View</Link>
        <hr></hr>
        <h3>Reminder types</h3>
        <Link to="remindertypes/" >View</Link>
        <hr></hr>
        <h3>History entry types</h3>
        <Link to="historyentrytypes/" >View</Link>
        </>
        :
        <>
        <h2>You are not authorized to access this content!</h2>
        </>
        
    )
}

export default Index;
