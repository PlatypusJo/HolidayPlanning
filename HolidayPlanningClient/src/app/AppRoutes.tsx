import {Route, Routes} from "react-router-dom";
import React from "react";
import "./ui/App.css"
import {routes} from "./routes/routes";
import {NotificationProvider} from "../shared/hook";

export const AppRoutes = () => {
    return (
        <Routes>
            {routes.map(route =>
                <Route path={route.path} element={route.element} key={route.path}/>
            )}
        </Routes>
    );
};