import {RoutesPaths} from "../../shared/config";
import React, {JSX, useEffect} from "react";
import {EventsPage} from "../../pages/EventsPage/EventsPage";
import {EventContractorsPage} from "../../pages/EventContractorsPage/EventContractorsPage";
import {HomePage} from "../../pages/HomePage/HomePage";
import {useNavigate} from "react-router-dom";
import {useFooterContext} from "../../shared/ui/Footer/Footer";

type AppRoutes = {
    path: RoutesPaths | "*",
    element: JSX.Element
}

const NotFoundRedirect = () => {
    window.history.back()
    return (<div/>)
};

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const navigate = useNavigate()
    const {updateFloatButton } = useFooterContext()
    const isAuth = localStorage.getItem('userId')

    useEffect(() => {
        if(!isAuth){
            navigate(RoutesPaths.HOME)
            updateFloatButton(undefined)
        }
    }, [isAuth])

    return <>{isAuth ? children : <HomePage/>}</>;
};

export const routes: AppRoutes[] = [
    { path: RoutesPaths.HOME, element: <HomePage/> },
    //{ path: RoutesPaths.PROFILE, element: <EventsPage/> },
    { path: RoutesPaths.EVENTS, element: <ProtectedRoute><EventsPage/> </ProtectedRoute>},
    { path: RoutesPaths.EVENTS_CONTRACTORS, element: <ProtectedRoute><EventContractorsPage/></ProtectedRoute> },
    { path: "*", element: <NotFoundRedirect/> },
]
