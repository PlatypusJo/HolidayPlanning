import React, {useEffect, useState} from "react";
import cl from "./ui/EventsPage.module.css"
import ImageContainerEvents from "../../shared/image/image-container-events.png"
import {EventData, getAllEvents} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {EventContainer} from "../../widgets";

export const EventsPage = () => {
    const [contextHolder, notification] = useNotification(5)
    const [events, setEvents] = useState<EventData[]>([]);
    const [fetchGetEvents, errorFetchGetEvents] = useFetching(async () => {
        try {
            const response = await getAllEvents()
            console.log(response)
            setEvents(response)
        } catch (e) {
            notification.error(`Ошибка при получении меропритий: ${errorFetchGetEvents}`)
        }
    })

    useEffect(() => {
        fetchGetEvents()
    }, [])

    return (
        <>
            {contextHolder}
            <InfoContainer title={"Здесь располагаются все ваши мероприятия"} src={ImageContainerEvents}/>
            <div className={cl.eventsContainer}>
                <div className={cl.separatorUnderline}/>
                {
                    events.length > 0
                        ? events.map(event => <EventContainer event={event}/>)
                        :
                        <NoData title={"Мероприятий не найдено"} text={"Нажмите +, чтобы добавить новое мероприятие"}/>
                }
            </div>
            <RightFloatButton tooltipTitle={"Добавить мероприятие"} buttonIcon={<PlusOutlined/>}
                              onClick={() => notification.info("Добавление мероприятия в раработке..")}/>
        </>
    )
}