import cl from "./ui/ContractorsContainer.module.css";
import {DeleteOutlined, EditOutlined, SettingOutlined,} from "@ant-design/icons";
import React, {useState} from "react";
import {
    changeContractorStatus,
    ContractorsData,
    ContractorStatus,
    contractorStatus, deleteContractor, getEnumMapping
} from "../../shared/api";
import {Button, Cascader, Dropdown, MenuProps} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {ContractorChangeModal} from "../../modal/ContractorChangeModal.tsx";
import {useParams} from "react-router-dom";

export const ContractorContainer: React.FC<{
    contractor: ContractorsData,
    onChangeContractor: (contractorId: string, newContractor: ContractorsData) => void,
    onDeleteContractor: (contractorId: string) => void,
}> = ({ contractor, onChangeContractor, onDeleteContractor }) => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
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
    const [fetchChangeStatus, isLoadingFetchChangeStatus, errorFetchChangeStatus] = useFetching(async (newStatus: string) => {
        try {
            const response = await changeContractorStatus(contractor.id, Number(getEnumMapping(ContractorStatus, newStatus as keyof typeof ContractorStatus)))
            if(response && response.status === 200){
                onChangeContractor(contractor.id, {
                    ...contractor,
                    status: newStatus
                })
            }
        } catch (e) {
            notification.error(`Ошибка при изменении статуса подрядчика '${contractor.name}': ${errorFetchChangeStatus}`)
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
        fetchChangeStatus(selectedOptions.map((o) => o.label).join(', '))
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
                            options={statusOptions.filter(val => val.label !== contractor.status)}
                            onChange={onChangeStatus}
                            dropdownRender={(menu) => (
                                <div style={{ maxHeight: '80px', overflowY: 'hidden' }}>
                                    {menu}
                                </div>
                            )}
                        >
                            <a className={cl.statusLink}>{contractor.status}</a>
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
                                   onChangeContractor={onChangeContractor} onCancel={handleCloseChangeContractorModal}/>
        </>
    );
};