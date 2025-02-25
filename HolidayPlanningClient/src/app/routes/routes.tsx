import {RoutesPaths} from "../../shared/config";
import {JSX} from "react";
import {EventsPage} from "../../pages/EventsPage/EventsPage";

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
    { path: "*", element: <NotFoundRedirect/> },
]
