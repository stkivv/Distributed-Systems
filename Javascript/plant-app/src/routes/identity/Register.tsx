import { MouseEvent, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IRegisterData } from "../../dto/IRegisterData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import RegisterFormView from "./RegisterFormView";

const Register = () => {
    const navigate = useNavigate();

    const [values, setInput] = useState({
        password: "",
        confirmPassword: "",
        email: "",
        firstName: "",
        lastName: "",
    } as IRegisterData);

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

        if (values.firstName.length == 0 || values.lastName.length == 0 || values.email.length == 0 || values.password.length == 0 || values.password != values.confirmPassword) {
            setValidationErrors(["Bad input values!"]);
            return;
        }
        // remove errors
        setValidationErrors([]);

        //register the user, get jwt and refresh token
        var jwtData = await identityService.register(values);

        if (jwtData == undefined) {
            setValidationErrors(["Bad input values!"]);
            return;
        } 

        if (setJwtResponse){
            setJwtResponse(jwtData);
            navigate("/plants");
       }

    }

    return (
        <RegisterFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} />
    );
}

export default Register;