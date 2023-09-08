import { MouseEvent, useContext, useState } from "react";
import { ILoginData } from "../../dto/ILoginData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import LoginFormView from "./LoginFormView";
import { useNavigate } from "react-router-dom";
import jwt_decode from "jwt-decode";

const Login = () => {
    const navigate = useNavigate();

    const [values, setInput] = useState({
        email: "",
        password: "",
    } as ILoginData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        // debugger;
        // console.log(target.name, target.value, target.type)

        setInput({ ...values, [target.name]: target.value });
    }

    const {jwtResponse, setJwtResponse} = useContext(JwtContext);

    const identityService = new IdentityService();

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if (values.email.length == 0 || values.password.length == 0) {
            setValidationErrors(["Bad input values!"]);
            return;
        }
        // remove errors
        setValidationErrors([]);

        var jwtData = await identityService.login(values);

        if (jwtData == undefined) {
            setValidationErrors(["invalid inputs"]);
            return;
        } 

        if (setJwtResponse){
             setJwtResponse(jwtData);

             let userRole: string | null = null;
             let jwtObject: any = jwt_decode(jwtData.jwt);
             Object.keys(jwtObject).forEach(function (key) {
                 let res = key.split("/")
                 if (res.length > 1) {
                     if (res[res.length - 1] === 'role') {
                         userRole = jwtObject[key]
                     }
                 }
             })
             if (userRole === "Admin") {
                navigate("/admin");
                return;
             }
             navigate("/plants");
        }
    }

    return (
        <LoginFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} />
    );
}

export default Login;