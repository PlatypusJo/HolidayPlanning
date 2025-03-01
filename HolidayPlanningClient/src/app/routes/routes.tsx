import {RoutesPaths} from "../../shared/config";
import {JSX} from "react";
import {EventsPage} from "../../pages/EventsPage/EventsPage";
import {EventContractorsPage} from "../../pages/EventContractorsPage/EventContractorsPage";

type AppRoutes = {
    path: RoutesPaths | "*",
    element: JSX.Element
}

const NotFoundRedirect = () => {
    window.history.back()
    return (<div/>)
};

export const routes: AppRoutes[] = [
    { path: RoutesPaths.HOME, element: <EventsPage/> },
    { path: RoutesPaths.EVENTS_CONTRACTORS, element: <EventContractorsPage/> },
    { path: "*", element: <NotFoundRedirect/> },
]
