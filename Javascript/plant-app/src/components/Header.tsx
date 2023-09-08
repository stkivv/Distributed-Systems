import { useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import { JwtContext } from "../routes/Root";
import IdentityHeader from "./IdentityHeader";
import jwt_decode from "jwt-decode";

const Header = () => {
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
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
                <div className="container">
                    <Link className="navbar-brand" to={jwtResponse ? "/plants" : "/"}>PlantApp
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-tree-fill" viewBox="0 0 16 16">
                            <path d="M8.416.223a.5.5 0 0 0-.832 0l-3 4.5A.5.5 0 0 0 5 5.5h.098L3.076 8.735A.5.5 0 0 0 3.5 9.5h.191l-1.638 3.276a.5.5 0 0 0 .447.724H7V16h2v-2.5h4.5a.5.5 0 0 0 .447-.724L12.31 9.5h.191a.5.5 0 0 0 .424-.765L10.902 5.5H11a.5.5 0 0 0 .416-.777l-3-4.5z"/>
                        </svg>
                    </Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item" style={{ 'display': jwtResponse && userRole !== "Admin" ? '' : 'none' }}>
                                <Link to="manage" className="nav-link text-light">Manage</Link>
                            </li>

                            <li className="nav-item" style={{ 'display': jwtResponse && userRole !== "Admin" ? '' : 'none' }}>
                                <Link to="plants" className="nav-link text-light">Plants</Link>
                            </li>

                            <li className="nav-item" style={{ 'display': jwtResponse && userRole === "Admin" ? '' : 'none' }}>
                                <Link to="admin" className="nav-link text-light">Admin options</Link>
                            </li>

                        </ul>

                        <ul className="navbar-nav">
                            <IdentityHeader/>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}


export default Header;