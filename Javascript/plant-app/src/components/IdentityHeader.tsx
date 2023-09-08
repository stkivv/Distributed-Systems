import { useContext, useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { JwtContext } from "../routes/Root";
import { IdentityService } from "../services/IdentityService";
import jwt_decode from "jwt-decode";
import  Dropdown  from "react-bootstrap/Dropdown";
import { ReminderService } from "../services/ReminderService";
import { IReminder } from "../domain/IReminder";
import RefreshToken from "./RefreshToken";
import { IJWTResponse } from "../dto/IJWTResponse";

const IdentityHeader = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const navigate = useNavigate();
    const identityService = new IdentityService();

    const reminderService = new ReminderService();
    const [notifs, setNotifs] = useState([] as IReminder[])

    const filterNotifResponse = (data: IReminder[]) => {
        var filteredList = [] as IReminder[]
        data.forEach(r => {
            let currentTime = new Date()
            r.duration = undefined;

            r.plantHistoryEntries.forEach(e => {
                if (e.historyEntryType!.eventTypeId !== r.reminderType!.eventTypeId){
                    return;
                }

                //in days
                r.duration = Math.round(((new Date(currentTime).getTime() - new Date(e.entryTime).getTime())/(1000 * 60 * 60 * 24)))
            })
                 
            let currentMonth: number = currentTime.getMonth() + 1 //0 based indexing

            let active: boolean = false;
            r.months.forEach(a => {
                if (parseInt(a.monthNr) === currentMonth){
                    active = true;
                }
            })

            if (active) {
                filteredList.push(r)
            }
        })
        return filteredList
    }

    useEffect(() => {
        async function getNotifications() {
            if (jwtResponse) {
                try {
                    const response = await reminderService.getAll(jwtResponse.jwt);
                    console.log(response);
                    if (response) {
                        let filteredResponse: IReminder[] = filterNotifResponse(response);

                        setNotifs(filteredResponse);
                    } else {
                        const newJwt: IJWTResponse | undefined = await RefreshToken(jwtResponse);
                        if (newJwt && setJwtResponse) {
                            setJwtResponse(newJwt)
                        }
                        setNotifs([]);
                    }
                } catch (error) {
                    console.log(error)
                }
            }
        }
        getNotifications();
    }, [jwtResponse]);

    const logout = () => {
        if (jwtResponse)
            identityService.logout(jwtResponse).then(response => {
                if (setJwtResponse)
                    setJwtResponse(null);
                navigate("/");
            });
    }

    if (jwtResponse) {
        let jwtObject: any = jwt_decode(jwtResponse.jwt);

        //console.log(jwtObject);

        return (
            <>
                <li className="nav-item">
                    <Dropdown>
                        <Dropdown.Toggle variant="primary" id="dropdown">
                            Reminders &nbsp;
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="white" className="bi bi-bell-fill" viewBox="0 0 16 16">
                                <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zm.995-14.901a1 1 0 1 0-1.99 0A5.002 5.002 0 0 0 3 6c0 1.098-.5 6-2 7h14c-1.5-1-2-5.902-2-7 0-2.42-1.72-4.44-4.005-4.901z"/>
                            </svg>
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                            {(notifs.length > 0) ? notifs.map(n => {
                                return(
                                <Dropdown.Item>
                                    <b>{n.plantName}</b>
                                    <div>
                                        {n.reminderMessage} (type: {n.reminderType!.reminderTypeName})
                                    </div>
                                    <div>
                                        <b>Days since last entry: &nbsp;</b>
                                        {n.duration ? <text>{n.duration}</text> : <text>No entires of this type have been made</text>}
                                    </div>
                                    <hr></hr>
                                </Dropdown.Item>
                                )})
                                : 
                                <Dropdown.Item>
                                    <b>No active reminders</b>
                                </Dropdown.Item>
                            }
                        </Dropdown.Menu>
                    </Dropdown>
                </li>
                <li className="nav-item">
                    <Link to="/plants" className="nav-link text-light">
                        <UserInfo jwtObject={jwtObject} />
                    </Link>
                </li>
                <li className="nav-item">
                    <a onClick={(e) => {
                        e.preventDefault();
                        logout();
                    }} className="nav-link text-light" href="#">Logout</a>
                </li>
            </>
        );
    }
    return (
        <>
            <li className="nav-item">
                <Link to="register" className="nav-link text-light">Register</Link>
            </li>
            <li className="nav-item">
                <Link to="login" className="nav-link text-light">Login</Link>
            </li>
        </>
    );
}

interface IUserInfoProps {
    jwtObject: any
}

const UserInfo = (props: IUserInfoProps) => {
    return (
        <>
            {props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']}
        </>
    ); 
}

export default IdentityHeader;