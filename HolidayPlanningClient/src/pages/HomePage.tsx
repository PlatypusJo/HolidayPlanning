import React from "react";
import cl from "./ui/HomePage.module.css"

export const HomePage = () => {
    return (
        <>
            {/*TODO: Этот блок нужно вынести в отдельный компонент, аля widget*/}
            <div className={`${cl.label} ${cl.infoContainer}`}>
                <p>HolidayPlanning</p>
            </div>
            <div className={cl.label}>
                <p>Content</p>
            </div>
        </>
    )
}