import { Outlet } from "react-router-dom";
import Footer from "../components/Footer";
import Header from "../components/Header";
import { createContext, useState } from "react";
import { IJWTResponse } from "../dto/IJWTResponse";
import { IdentityService } from "../services/IdentityService";

export const JwtContext = createContext<{
    jwtResponse: IJWTResponse | null,
    setJwtResponse: ((data: IJWTResponse | null) => void) | null
}>({ jwtResponse: null, setJwtResponse: null });


const Root = () => {
    const [jwtResponse, setJwtResponse] = useState(null as IJWTResponse | null);

    return (
        <JwtContext.Provider value={{ jwtResponse, setJwtResponse }}>
            <Header />

            <div className="container">
                <main role="main" className="pb-3">
                    <Outlet />
                </main>
            </div>
            
            <Footer />
        </JwtContext.Provider>
    )

}

export default Root;