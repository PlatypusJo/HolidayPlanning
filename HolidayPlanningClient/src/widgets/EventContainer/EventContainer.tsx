import cl from "./ui/EventContainer.module.css";
import {DeleteOutlined, EditOutlined, PlusCircleOutlined, SettingOutlined, SolutionOutlined} from "@ant-design/icons";
import {formatDate, formatTimeDifference} from "../../shared/lib";
import React, {useState} from "react";
import {deleteEvent, EventData, ContractorsData} from "../../shared/api";
import {Button, Dropdown, MenuProps} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {EventChangeModal} from "../../modal/EventChangeModal";
import { ContractorCreateModal } from "../../modal/ContractorCreateModal.tsx";
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../../shared/config";

export const EventContainer: React.FC<{
    event: EventData,
    onChangeEvent: (eventId: string, newEvent: EventData) => void,
    onDeleteEvent: (eventId: string) => void,
}> = ({ event, onChangeEvent, onDeleteEvent }) => {
    const navigate = useNavigate()
    const notification = useNotification()
    const [isChangeEventModal, setIsChangeEventModal] = useState(false);
    const [contractors, setContractors] = useState<ContractorsData[]>([]);
    const [isCreateContractorModal, setIsCreateContractorModal] = useState(false);
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

    const openCreateContractorModal = () => {
        setIsCreateContractorModal(true);
    };
    
    
    const handleCancelCreateContractorModal = () => {
        setIsCreateContractorModal(false);
    };

    const items: MenuProps['items'] = [
        {
            label: (
                <Button icon={<EditOutlined/>}
                        iconPosition={"start"}
                        onClick={() => {openChangeEventModal()}}
                        color={"default"}
                        variant={"link"}
                >
                    Изменить мероприятие
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
                    Удалить мероприятие
                </Button>
            ),
            key: '1',
        }, {
            type: 'divider',
        }, {
            label: (
                <Button
                    icon={<SolutionOutlined/>}
                    iconPosition={"start"}
                    onClick={() => navigate(`${RoutesPaths.EVENTS_CONTRACTORS}`.replace(":id", `${event.id}`))}
                    color={"default"}
                    variant={"link"}
                >
                    Подрядчики
                </Button>
            ),
            key: '2',
        }, {
            label: (
                <Button
                    icon={<><PlusCircleOutlined/> <SolutionOutlined/></>}
                    iconPosition={"start"}
                    onClick={openCreateContractorModal}
                    color={"default"}
                    variant={"link"}
                >
                    Добавить подрядчика
                </Button>
            ),
            key: '3',
        }
    ];

    const openChangeEventModal = () => {
        setIsChangeEventModal(true);
    };


    const handleCloseChangeEventModal = () => {
        setIsChangeEventModal(false);
    };

    const onCreateContractor = (newContractor: ContractorsData) => {
                setContractors([...contractors, newContractor])
    }

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
            <ContractorCreateModal eventId={event.id} visible={isCreateContractorModal} onCancel={handleCancelCreateContractorModal} onCreateContractor={onCreateContractor}/>
        </>
    );
};