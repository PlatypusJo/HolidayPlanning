import React, {useEffect, useState} from "react";
import cl from "./ui/EventContractorsPage.module.css"
import ImageContainerContractors from "../../shared/image/image container-contractors.png"
import {contractorCategories, ContractorsData, EventData, getEventContractors} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {useFooterContext} from "../../shared/ui/Footer/Footer";
import {ContractorContainer} from "../../widgets";
import {useParams} from "react-router-dom";
import {Checkbox} from "antd";
import {ContractorCreateModal} from "../../modal/ContractorCreateModal.tsx";

export const EventContractorsPage = () => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
    const [contractors, setContractors] = useState<ContractorsData[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
    const { updateFloatButton } = useFooterContext()
    const [isCreateContractorModal, setIsCreateContractorModal] = useState(false);
    const [fetchGetContractors, isLoadingFetchGetContractors, errorFetchGetContractors] = useFetching(async () => {
        try {
            const response = await getEventContractors(eventId)
            response && setContractors(response)
        } catch (e) {
            notification.error(`Ошибка при получении подрядчиков мероприятия: ${errorFetchGetContractors}`)
        }
    })

    useEffect(() => {
        fetchGetContractors()
        updateFloatButton(<RightFloatButton
            tooltipTitle={"Добавить подрядчика"}
            buttonIcon={<PlusOutlined/>}
            onClick={openCreateContractorModal}
        />)
    }, [])

    const onCreateContractor = (newContractor: ContractorsData) => {
        setContractors([...contractors, newContractor])
    }

    const handleCheckboxChange = (category: string) => {
        setSelectedCategories((prevSelected) =>
            prevSelected.includes(category)
                ? prevSelected.filter((item) => item !== category)
                : [...prevSelected, category]
        );
    };

    const filteredContractors = selectedCategories.length > 0
        ? contractors.filter((contractor) => selectedCategories.includes(contractor.category))
        : contractors;

    const openCreateContractorModal = () => {
        setIsCreateContractorModal(true);
    };


    const handleCancelCreateContractorModal = () => {
        setIsCreateContractorModal(false);
    };

    const onDeleteContractor = (contractorId: string) => {
        const updatedContractors = contractors.filter(contractor => contractor.id !== contractorId);
        setContractors(updatedContractors);
    }

    const onChangeContractor = (contractorId: string, newContractor: ContractorsData) => {
        const index = contractors.findIndex(contractor => contractor.id === contractorId);

        if (index !== -1) {
            const updatedContractors = [
                ...contractors.slice(0, index),
                newContractor,
                ...contractors.slice(index + 1)
            ];

            // Обновляем состояние
            setContractors(updatedContractors);
        } else {
            console.warn(`Подрядчик с id ${contractorId} не найден`);
        }
    }


    return (
        <>
            <InfoContainer title={"Здесь располагаются подрядчики вашего мероприятия"} src={ImageContainerContractors}
                           onBtnClick={() => window.history.back()}/>
            <div className={cl.contractorsContainer}>
                <div className={cl.separatorUnderline}/>
                <div className={cl.container}>
                    <div className={cl.filterContainer}>
                        <div className={cl.category}>Категории:</div>
                        <div className={cl.checkBoxContainer}>
                            {
                                contractorCategories.map(category =>
                                    <Checkbox
                                        style={{ fontSize: "1.8vh" }}
                                        checked={selectedCategories.includes(category as string)}
                                        onChange={() => handleCheckboxChange(category as string)}
                                    >
                                        {category}
                                    </Checkbox>
                                )
                            }
                        </div>
                    </div>
                    <div>
                        {
                            filteredContractors.length > 0
                                ? filteredContractors.map((contractor, index) =>
                                    <ContractorContainer
                                        contractor={contractor}
                                        key={index}
                                        onDeleteContractor={onDeleteContractor}
                                        onChangeContractor={onChangeContractor}
                                    />
                                )
                                :
                                <NoData title={"Подрядчиков не найдено"} text={"Нажмите +, чтобы добавить подрядчика"}/>
                        }
                    </div>
                </div>
            </div>
            <ContractorCreateModal eventId={eventId} visible={isCreateContractorModal}
                                   onCancel={handleCancelCreateContractorModal}
                                   onCreateContractor={onCreateContractor}/>
        </>
    )
}