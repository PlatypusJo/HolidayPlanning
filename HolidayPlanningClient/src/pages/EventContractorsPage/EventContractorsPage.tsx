import React, {useEffect, useState} from "react";
import cl from "./ui/EventContractorsPage.module.css"
import ImageContainerContractors from "../../shared/image/image container-contractors.png"
import {contractorCategories, ContractorsData, getEventContractors} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {useFooterContext} from "../../shared/ui/Footer/Footer";
import {ContractorContainer} from "../../widgets";
import {useNavigate, useParams} from "react-router-dom";
import {RoutesPaths} from "../../shared/config";
import {Checkbox} from "antd";
import {ContractorCreateModal} from "../../modal/ContractorCreateModal.tsx";

export const EventContractorsPage = () => {
    const eventId = Number(useParams().id)
    const navigate = useNavigate()
    const notification = useNotification()
    const [contractors, setContractors] = useState<ContractorsData[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
    const {updateFloatButton} = useFooterContext()
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

    return (
        <>
            <InfoContainer title={"Здесь располагаются подрядчики вашего мероприятия"} src={ImageContainerContractors}
                           onRouteBack={() => navigate(RoutesPaths.EVENTS)}/>
            <div className={cl.contractorsContainer}>
                <div className={cl.separatorUnderline}/>
                <div className={cl.container}>
                    <div className={cl.filterContainer}>
                        <div className={cl.category}>Категории: </div>
                        <div className={cl.checkBoxContainer}>
                            {
                                contractorCategories.map(category =>
                                    <Checkbox
                                        style={{fontSize: "1.8vh"}}
                                        checked={selectedCategories.includes(category)}
                                        onChange={() => handleCheckboxChange(category)}
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
                                    />
                                )
                                :
                                <NoData title={"Подрядчиков не найдено"} text={"Нажмите +, чтобы добавить подрядчика"}/>
                        }
                    </div>
                </div>
            </div>
            <ContractorCreateModal visible={isCreateContractorModal} onCancel={handleCancelCreateContractorModal} onCreateContractor={onCreateContractor}/>
        </>
    )
}