import cl from "./ui/EventContainer.module.css";
import {EditOutlined} from "@ant-design/icons";
import {formatDate, formatTimeDifference} from "../../shared/lib";
import React from "react";
import {EventData} from "../../shared/api";

export const EventContainer: React.FC<{ event: EventData }> = ({ event }) => {
    return (
        <div key={event.id} className={cl.blockEventBack}>
            <div className={cl.blockEventName}>
                <div>{event.title}</div>
                <EditOutlined className={cl.changeEventIcon}
                              onClick={() => console.info("Изменение мероприятия в разработке..")}/>
            </div>
            <div className={cl.blockEventDate}>
                <div>{`${formatDate(event.startDate).replace(",", " в ")}, ${formatTimeDifference(event.startDate, event.endDate)}`}</div>
                <div>Бюджет: {event.budget} руб.</div>
            </div>
        </div>
    );
};