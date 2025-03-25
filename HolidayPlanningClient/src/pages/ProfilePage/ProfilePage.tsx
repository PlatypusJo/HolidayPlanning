import React, {useEffect, useState} from "react";
import ImageContainerProfile from "../../shared/image/image-container-profile.png"
import {EventData, getAllEvents} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {AdviceContainer, InfoContainer, ProfileMenu} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {EventContainer, ProfileEventsWidget} from "../../widgets";
import {EventCreateModal} from "../../modal/EventCreateModal";
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../../shared/config";
import {useFooterContext} from "../../shared/ui/Footer/Footer";

export const ProfilePage = () => {
    const navigate = useNavigate()
    const notification = useNotification()
    const {updateFloatButton } = useFooterContext()
    const [events, setEvents] = useState<EventData[]>([]);
    const [isCreateEventModal, setIsCreateEventModal] = useState(false)
    const [fetchGetEvents, isLoadingFetchGetEvents, errorFetchGetEvents] = useFetching(async () => {
        try {
            const response = await getAllEvents()

            if(response){
                setEvents(response)
            }
        } catch (e) {
            notification.error(`Ошибка при получении меропритий: ${errorFetchGetEvents}`)
        }
    })

    useEffect(() => {
        updateFloatButton(undefined)
        fetchGetEvents()
    }, [])

    const updateSelectedId = (newSelectedId: string) => {
        const targetEvent = events.find(event => event.id === newSelectedId);
        if (!targetEvent) return events; // Если не найден — возвращаем исходный массив
        const remainingEvents = events.filter(event => event.id !== newSelectedId);
        setEvents([targetEvent, ...remainingEvents])
    }

    const openCreateEventModal = () => {
        setIsCreateEventModal(true);
    };

    const handleCancelCreateEventModal = () => {
        setIsCreateEventModal(false);
    };

    return (
        <>
            <InfoContainer
                title={"Давайте займемся организацией вашего мероприятия"}
                src={ImageContainerProfile}
                onBtnClick={openCreateEventModal}
                buttonSettings={{ btnIcon: <PlusOutlined/>, iconPosition: 'end', btnText: "Создать мероприятие" }}
            />
            <ProfileMenu/>
            {events.length !== 0 && <ProfileEventsWidget
                events={events}
                updateSelectedEventId={updateSelectedId}
            />}
            <AdviceContainer/>
            <EventCreateModal
                visible={isCreateEventModal}
                onCancel={handleCancelCreateEventModal}
                onCreateEvent={() => {
                    navigate(RoutesPaths.EVENTS)
                }}
            />
        </>
    )
}