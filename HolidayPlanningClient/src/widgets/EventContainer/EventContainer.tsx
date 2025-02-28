import cl from "./ui/EventContainer.module.css";
import {DeleteOutlined, EditOutlined, SettingOutlined, SolutionOutlined} from "@ant-design/icons";
import {formatDate, formatTimeDifference} from "../../shared/lib";
import React, {useState} from "react";
import {deleteEvent, EventData} from "../../shared/api";
import {Button, Dropdown, MenuProps} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {EventChangeModal} from "../../modal/EventChangeModal";

export const EventContainer: React.FC<{
    event: EventData,
    onChangeEvent: (eventId: number, newEvent: EventData) => void,
    onDeleteEvent: (eventId: number) => void,
}> = ({ event, onChangeEvent, onDeleteEvent }) => {
    const { notification } = useNotification()
    const [isChangeEventModal, setIsChangeEventModal] = useState(false);
    const [fetchDeleteEvents, isLoadingFetchDeleteEvents, errorFetchDeleteEvents] = useFetching(async () => {
        try {
            const response = await deleteEvent(event.id)
            if (response && response.status === 200) {
                onDeleteEvent(event.id)
                notification.success(`Мероприятие '${event.title}' успешно удалено!`)
            }
        } catch (e) {
            notification.error(`Ошибка при удалении меропрития: ${errorFetchDeleteEvents}`)
        }
    })

    const items: MenuProps['items'] = [
        {
            label: (
                <Button icon={<EditOutlined/>}
                        iconPosition={"start"}
                        onClick={() => {openChangeEventModal()}}
                        color={"default"}
                        variant={"link"}
                >
                    Изменить
                </Button>
            ),
            key: '0',
        }, {
            label: (
                <Button
                    icon={<DeleteOutlined/>}
                    iconPosition={"start"}
                    loading={isLoadingFetchDeleteEvents}
                    onClick={() => {fetchDeleteEvents()}}
                    color={"danger"}
                    variant={"link"}
                >
                    Удалить
                </Button>
            ),
            key: '1',
        }, {
            label: (
                <Button
                    icon={<SolutionOutlined/>}
                    iconPosition={"start"}
                    onClick={() => notification.info("Подрядчики")}
                    color={"default"}
                    variant={"link"}
                >
                    Подрядчики
                </Button>
            ),
            key: '2',
        }
    ];

    const openChangeEventModal = () => {
        setIsChangeEventModal(true);
    };


    const handleCloseChangeEventModal = () => {
        setIsChangeEventModal(false);
    };

    return (
        <>
            <div key={event.id} className={cl.blockEventBack}>
                <div className={cl.blockEventName}>
                    <div>{event.title}</div>
                    <Dropdown menu={{ items }} trigger={['click', 'contextMenu']}>
                        <SettingOutlined className={cl.settingsEventIcon}/>
                    </Dropdown>
                </div>
                <div className={cl.blockEventDate}>
                    <div>{`${formatDate(event.startDate).replace(",", " в ")}, ${formatTimeDifference(event.startDate, event.endDate)}`}</div>
                    <div>Бюджет: {event.budget} руб.</div>
                </div>
            </div>
            <EventChangeModal event={event} visible={isChangeEventModal} onCancel={handleCloseChangeEventModal} onChangeEvent={onChangeEvent}/>
        </>
    );
};