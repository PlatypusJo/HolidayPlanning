import {RoutesPaths} from "../../shared/config";
import {JSX} from "react";
import {HomePage} from "../../pages/HomePage";

type AppRoutes = {
    path: RoutesPaths | "*",
    element: JSX.Element
}

const NotFoundRedirect = () => {
    window.history.back()
    return (<div/>)
};

export const routes: AppRoutes[] = [
    { path: RoutesPaths.HOME, element: <HomePage/> },
    { path: "*", element: <NotFoundRedirect/> },
]
