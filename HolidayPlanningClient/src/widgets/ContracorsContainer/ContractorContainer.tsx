import cl from "./ui/ContractorsContainer.module.css";
import {SettingOutlined,} from "@ant-design/icons";
import React from "react";
import {ContractorsData, } from "../../shared/api";

export const ContractorContainer: React.FC<{
    contractor: ContractorsData,
}> = ({ contractor }) => {
    return (
        <>
            <div key={contractor.id} className={cl.blockContractorBack}>
                <div className={cl.blockContractorName}>
                    <div>{contractor.name}</div>
                    <div className={cl.contractorStatus}>{contractor.status}</div>
                    <SettingOutlined className={cl.settingsContractorIcon}/>
                </div>
                <div className={cl.description}>
                    <div>Описание: {contractor.description}</div>
                    <div>Категория: {contractor.category}</div>
                    {contractor.phoneNumber !== '' && <div>Номер телефона: {contractor.phoneNumber}</div>}
                    {contractor.email !== '' && <div>Email: {contractor.email}</div>}
                    <div>Стоимость услуг: {contractor.serviceCost} руб.</div>
                </div>
            </div>
        </>
    );
};