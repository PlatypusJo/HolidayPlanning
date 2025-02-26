import React, {useEffect, useState} from "react";
import cl from "./ui/EventsPage.module.css"
import ImageContainerEvents from "../../shared/image/image-container-events.png"
import {EventData, getAllEvents} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {EventContainer} from "../../widgets";
import {useFooterContext} from "../../shared/ui/Footer/Footer";
import {EventCreateModal} from "../../modal/EventCreateModal/EventCreateModal";

export const EventsPage = () => {
    const [contextHolder, notification] = useNotification(5)
    const [events, setEvents] = useState<EventData[]>([]);
    const { updateFloatButton } = useFooterContext()
    const [isCreateEventModal, setIsCreateEventModal] = useState(false);
    const [fetchGetEvents, isLoadingFetchGetEvents, errorFetchGetEvents] = useFetching(async () => {
        try {
            const response = await getAllEvents()
            response && setEvents(response)
        } catch (e) {
            notification.error(`Ошибка при получении меропритий: ${errorFetchGetEvents}`)
        }
    })

    useEffect(() => {
        fetchGetEvents()
        updateFloatButton(<RightFloatButton
            tooltipTitle={"Добавить мероприятие"}
            buttonIcon={<PlusOutlined/>}
            onClick={openCreateEventModal}
        />)
    }, [])

    const openCreateEventModal = () => {
        setIsCreateEventModal(true);
      };
    
      const handleCancelCreateEventModal = () => {
        setIsCreateEventModal(false);
    };

    return (
        <>
            {contextHolder}
            <InfoContainer title={"Здесь располагаются все ваши мероприятия"} src={ImageContainerEvents}/>
            <div className={cl.eventsContainer}>
                <div className={cl.separatorUnderline}/>
                {
                    events.length > 0
                        ? events.map((event, index) => <EventContainer event={event} key={index}/>)
                        :
                        <NoData title={"Мероприятий не найдено"} text={"Нажмите +, чтобы добавить новое мероприятие"}/>
                }
            </div>
            <EventCreateModal visible={isCreateEventModal} onCancel={handleCancelCreateEventModal}/>
        </>
    )
}