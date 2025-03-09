import {useEffect} from "react";
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../../shared/config";

export const HomePage = () => {
    const navigate = useNavigate()

    useEffect(() => {
        localStorage.getItem('userId') && navigate(RoutesPaths.PROFILE)
    }, []);

    return (
        <div>HomePage</div>
    );
};