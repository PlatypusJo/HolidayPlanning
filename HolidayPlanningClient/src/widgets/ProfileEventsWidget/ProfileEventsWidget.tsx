import React, {useEffect, useState} from "react";
import {EventData} from "../../shared/api";
import {useNotification} from "../../shared/hook";
import {CheckOutlined, GiftOutlined, MenuOutlined} from "@ant-design/icons";
import {formatDate, getCountdown} from "../../shared/lib";
import cl from './ProfileEventsWidget.module.css'

export const ProfileEventsWidget: React.FC<{
    events: EventData[],
    updateSelectedEventId: (newSelectedId: number) => void,
}> = ({events,  updateSelectedEventId}) => {
    const notification = useNotification()
    const [selectEventId, setSelectEventId] = useState<number | undefined>(Number(localStorage.getItem('selectEventId')));
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    useEffect(() => {
        selectEventId && localStorage.setItem('selectEventId', `${selectEventId}`);

        if (selectEventId) {
            localStorage.setItem('selectEventId', `${selectEventId}`);
            updateSelectedId(selectEventId);
        } else {
            const closestEventId = findClosestEvent(events);
            updateSelectedId(closestEventId);
        }
    }, [selectEventId]);

    const handleSelectEvent = (event: EventData) => {
        updateSelectedId(event.id);
        setIsDropdownOpen(false);
        notification.success(`Выбрано мероприятие: ${event.title}`);
    };

    const updateSelectedId = (newSelectedId: number) => {
        setSelectEventId(newSelectedId)
        updateSelectedEventId(newSelectedId)
    }

    const findClosestEvent = (events: EventData[]) => {
        const currentDateTime = new Date();

        const futureEvents = events.filter(event => event.startDate >= currentDateTime);

        let closestEvent: EventData;

        if (futureEvents.length > 0) {
            closestEvent = futureEvents.reduce((closest, event) =>
                event.startDate < closest.startDate ? event : closest
            );
        } else {
            const pastEvents = events.filter(event => event.startDate < currentDateTime);
            closestEvent = pastEvents.reduce((closest, event) =>
                event.startDate > closest.startDate ? event : closest
            );
        }

        return closestEvent.id;
    };

    return (
        <div className={cl.contentResume}>
            <div className={cl.textResume}>
                <div className={cl.resumeBlock}>Сводка</div>
                <div className={cl.resumeUnderline}></div>
                <div className={cl.resumeEvent}>
                    <div className={cl.resumeEventHeader}>
                        <GiftOutlined className={cl.resumeEventHeaderButton}/>
                        <div className={cl.resumeEventHeaderText}>Мероприятия</div>
                    </div>
                    <div className={cl.resumeUnderlineHeader}></div>
                    <div className={cl.resumeBlockEventName}>
                        <div>
                            {isDropdownOpen ? (
                                <>
                                    {events.map((event) => (
                                        <div className={cl.resumeNameEventList}>
                                            <div
                                                key={event.id}
                                                className={cl.resumeNameEventList}
                                                onClick={() => handleSelectEvent(event)}
                                            >
                                                {event.title}
                                                {events[0].title === event.title ? (
                                                    <CheckOutlined className={cl.checkIcon}/>) : ("")}
                                            </div>
                                            <div
                                                className={cl.resumeDateEvent}>{formatDate(events[0].startDate)}</div>
                                        </div>
                                    ))}
                                </>
                            ) : (
                                <>
                                    <div className={cl.resumeNameEvent}>{events[0].title}</div>
                                    <div className={cl.resumeDateEvent}>{formatDate(events[0].startDate)}</div>
                                </>
                            )}
                        </div>
                        <MenuOutlined className={cl.iconDropdown}
                                      onClick={() => setIsDropdownOpen(prev => !prev)}/>
                    </div>
                    <div className={cl.countdownTimer}>
                        <div className={cl.timerDisplay}>{getCountdown(events[0].startDate)}</div>
                    </div>
                </div>
            </div>
        </div>
    );
};