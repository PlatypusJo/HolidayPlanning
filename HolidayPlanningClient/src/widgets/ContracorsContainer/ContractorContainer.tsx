import cl from "./ui/ContractorsContainer.module.css";
import {DeleteOutlined, EditOutlined, SettingOutlined,} from "@ant-design/icons";
import React, {useState} from "react";
import {
    ContractorsData,
    ContractorStatus,
    contractorStatus, deleteContractor
} from "../../shared/api";
import {Button, Cascader, Dropdown, MenuProps} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {ContractorChangeModal} from "../../modal/ContractorChangeModal.tsx";
import {useParams} from "react-router-dom";

export const ContractorContainer: React.FC<{
    contractor: ContractorsData,
    onChangeContractor: (contractorId: number, newContractor: ContractorsData) => void,
    onDeleteContractor: (contractorId: number) => void,
}> = ({ contractor, onChangeContractor, onDeleteContractor }) => {
    const eventId = Number(useParams().id)
    const notification = useNotification()
    const [status, setStatus] = useState(contractor.status)
    const [isChangeContractorModal, setIsChangeContractorModal] = useState(false);
    const [fetchDeleteContractor, isLoadingFetchDeleteContractor, errorFetchDeleteContractor] = useFetching(async () => {
        try {
            const response = await deleteContractor(contractor.id)
            if (response && response.status === 200) {
                onDeleteContractor(contractor.id)
                notification.success(`Подрядчик '${contractor.name}' успешно удален!`)
            }
        } catch (e) {
            notification.error(`Ошибка при удалении подрядчика: ${errorFetchDeleteContractor}`)
        }
    })

    const items: MenuProps['items'] = [
        {
            label: (
                <Button icon={<EditOutlined/>}
                        iconPosition={"start"}
                        onClick={() => {
                            openChangeContractorModal()
                        }}
                        color={"default"}
                        variant={"link"}
                >
                    Изменить подрядчика
                </Button>
            ),
            key: '0',
        }, {
            label: (
                <Button
                    icon={<DeleteOutlined/>}
                    iconPosition={"start"}
                    loading={isLoadingFetchDeleteContractor}
                    onClick={() => {
                        fetchDeleteContractor()
                    }}
                    color={"danger"}
                    variant={"link"}
                >
                    Удалить подрядчика
                </Button>
            ),
            key: '1',
        }
    ];

    const statusOptions = contractorStatus.map(val => ({
        value: val,
        label: val
    }))

    const onChangeStatus = (
        _: (string | ContractorStatus)[],
        selectedOptions: { value: string | ContractorStatus; label: string | ContractorStatus }[]
    ) => {
        setStatus(selectedOptions.map((o) => o.label).join(', '));
    };

    const openChangeContractorModal = () => {
        setIsChangeContractorModal(true);
    };


    const handleCloseChangeContractorModal = () => {
        setIsChangeContractorModal(false);
    };

    return (
        <>
            <div key={contractor.id} className={cl.blockContractorBack}>
                <div className={cl.blockContractorName}>
                    <div>{contractor.name}</div>
                    <div className={cl.contractorStatus}>
                        <Cascader
                            options={statusOptions.filter(val => val.label != status)}
                            onChange={onChangeStatus}
                            dropdownRender={(menu) => (
                                <div style={{ maxHeight: '80px', overflowY: 'hidden' }}>
                                    {menu}
                                </div>
                            )}
                        >
                            <a className={cl.statusLink}>{status}</a>
                        </Cascader>
                    </div>
                    <Dropdown menu={{ items }} trigger={['click', 'contextMenu']}>
                        <SettingOutlined className={cl.settingsContractorIcon}/>
                    </Dropdown>
                </div>
                <div className={cl.description}>
                    <div>Описание: {contractor.description}</div>
                    <div>Категория: {contractor.category}</div>
                    {contractor.phoneNumber !== '' && <div>Номер телефона: {contractor.phoneNumber}</div>}
                    {contractor.email !== '' && <div>Email: {contractor.email}</div>}
                    <div>Стоимость услуг: {contractor.serviceCost} руб.</div>
                </div>
            </div>
            <ContractorChangeModal eventId={eventId} contractor={contractor} visible={isChangeContractorModal}
                                   onChangeContractor={(contractorId: number, newContractor: ContractorsData) => {
                                       onChangeContractor(contractorId, newContractor)
                                       setStatus(newContractor.status)
                                   }} onCancel={handleCloseChangeContractorModal}/>
        </>
    );
};