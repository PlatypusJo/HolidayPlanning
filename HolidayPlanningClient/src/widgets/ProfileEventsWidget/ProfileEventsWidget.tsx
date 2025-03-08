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
    const [beforeEventTime, setBeforeEventTime] = useState(events.find(ev => ev.id === selectEventId)?.startDate)

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

    useEffect(() => {
        console.log('Создние таймера')
        const interval = setInterval(() => {
            if(beforeEventTime){
                if(Date.now() >= beforeEventTime.getTime()){
                    clearInterval(interval);
                }
                setBeforeEventTime(new Date(beforeEventTime.getTime() - 1000))
            }
        }, 1000)

        return () => {
            console.log('Удаление таймера')
            clearInterval(interval)
        };
    }, [selectEventId]);

    const handleSelectEvent = (event: EventData) => {
        updateSelectedId(event.id);
        setIsDropdownOpen(false);
        setBeforeEventTime(event.startDate)
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
                                        <div key={event.id} className={cl.resumeNameEventList}>
                                            <div
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
                        <div className={cl.timerDisplay}>{getCountdown(beforeEventTime)}</div>
                    </div>
                </div>
            </div>
        </div>
    );
};