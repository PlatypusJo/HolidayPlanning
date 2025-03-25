import React, {useEffect, useState} from "react";
import {Input, Select} from "antd";
import cl from './ui/ContractorChangeModal.module.css'
import Logo from '../../shared/image/modal-logo.png';
import {inputStyle, selectStyle} from "./config/theme";
import {useFetching, useNotification} from "../../shared/hook";
import {
    changeContractor,
    contractorCategories, ContractorCategory,
    ContractorsData, ContractorStatus,
    contractorStatus, getEnumMapping
} from "../../shared/api";
import {Modal} from "../../shared/ui";

const { Option } = Select;

type FormData = {
    name: string,
    description: string,
    category: string,
    phoneNumber: string,
    email: string,
    serviceCost: string,
    status: string
};

export const ContractorChangeModal: React.FC<{
    eventId: string,
    contractor: ContractorsData
    visible: boolean;
    onChangeContractor: (contractorId: string, newContractor: ContractorsData) => void;
    onCancel: () => void;
}> = ({ eventId, contractor, visible, onChangeContractor, onCancel }) => {
    const [formData, setFormData] = useState<FormData>({
        name: contractor.name,
        description: contractor.description,
        category: contractor.category,
        phoneNumber: contractor.phoneNumber,
        email: contractor.email,
        serviceCost: `${contractor.serviceCost}`,
        status: contractor.status
    });
    const [errors, setErrors] = useState<{ phoneNumber?: string; email?: string }>({});
    const notification = useNotification()
    const [fetchChangeContractor, isLoadingFetchChangeContractor, errorFetchChangeContractor] = useFetching(async () => {
        try {
            const response = await changeContractor(contractor.id, {
                id: contractor.id,
                title: formData.name,
                description: formData.description,
                phoneNumber: formData.phoneNumber,
                email: formData.email,
                serviceCost: Number(formData.serviceCost),
                holidayId: eventId,
                statusId: `${getEnumMapping(ContractorStatus, formData.status as keyof typeof ContractorStatus)}`,
                сategoryId: `${getEnumMapping(ContractorCategory, formData.category as keyof typeof ContractorCategory)}`
            })
            if (response) {
                onChangeContractor(contractor.id, {
                    id: contractor.id,
                    name: formData.name,
                    description: formData.description,
                    category: formData.category,
                    phoneNumber: formData.phoneNumber,
                    email: formData.email,
                    status: formData.status,
                    serviceCost: Number(formData.serviceCost)
                });
                notification.success(`Подрядчик '${formData.name}' успешно изменен!`)
            }
        } catch (e) {
            notification.error(`Ошибка при изменении подрядчика: ${errorFetchChangeContractor}`)
        }
    })

    useEffect(() => {
        setFormData({...contractor, serviceCost: `${contractor.serviceCost}`})
    }, [contractor]);

    const validatePhoneNumber = (phone: string) => {
        if (!phone) return '';
        const phoneRegex = /^\+?[0-9]{10,15}$/;
        return phoneRegex.test(phone) ? '' : 'Некорректный номер телефона';
    };

    const validateEmail = (email: string) => {
        if (!email) return '';
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email) ? '' : 'Некорректный email';
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, field: keyof FormData) => {
        const value = e.target.value;
        setFormData(prev => ({ ...prev, [field]: value }));

        if (field === 'phoneNumber') {
            setErrors(prev => ({ ...prev, phoneNumber: validatePhoneNumber(value) }));
        }
        if (field === 'email') {
            setErrors(prev => ({ ...prev, email: validateEmail(value) }));
        }
    };

    const handleCostChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        if (/^\d*\.?\d*$/.test(value) && (value === '' || parseFloat(value) >= 0)) {
            setFormData(prev => ({ ...prev, serviceCost: value }));
        }
    };

    const handleSelectChange = (value: string, field: keyof FormData) => {
        setFormData(prev => ({ ...prev, [field]: value }));
    };


    const handleSubmit = () => {
        fetchChangeContractor()
        handleClose();
    };

    const handleClose = () => {
        setFormData(formData);
        setErrors({});
        onCancel();
    };

    return (
        <Modal
            onCancel={handleClose}
            onOk={handleSubmit}
            okButtonText={"Изменить"}
            icon={Logo}
            modalTitle={"Добавление подрядчика"}
            description={"Измени информацию о подрядчике для мероприятия"}
            visible={visible}
            disabled={Object.values(formData).some(value => value === '')}
        >
            <div className={cl.inputContainer}>
                <Input
                    placeholder="ФИО/Название подрядчика"
                    style={inputStyle}
                    value={formData.name}
                    onChange={(e) => handleInputChange(e, 'name')}
                />
            </div>

            <div className={cl.inputContainer}>
                <Select
                    placeholder="Выберите категорию"
                    style={selectStyle}
                    value={formData.category}
                    onChange={(value) => handleSelectChange(value, 'category')}
                >
                    {contractorCategories.map((category) => (
                        <Option key={category} value={category}>{category}</Option>
                    ))}
                </Select>
            </div>

            <div className={cl.inputContainer}>
                <Input.TextArea
                    placeholder="Описание"
                    style={inputStyle}
                    value={formData.description}
                    onChange={(e) => handleInputChange(e, 'description')}
                    rows={4}
                />
            </div>

            <div className={cl.inputContainerPhEmail}>
                <Input
                    placeholder="Телефон"
                    style={inputStyle}
                    value={formData.phoneNumber}
                    onChange={(e) => handleInputChange(e, 'phoneNumber')}
                />
                {errors.phoneNumber && <div className={cl.error}>{errors.phoneNumber}</div>}
            </div>

            <div className={cl.inputContainerPhEmail}>
                <Input
                    placeholder="Email"
                    style={inputStyle}
                    value={formData.email}
                    onChange={(e) => handleInputChange(e, 'email')}
                    type="email"
                />
                {errors.email && <div className={cl.error}>{errors.email}</div>}
            </div>

            <div className={cl.inputContainer}>
                <Input
                    placeholder="Стоимость услуги"
                    style={inputStyle}
                    value={formData.serviceCost}
                    onChange={handleCostChange}
                    type="number"
                    min={0}
                />
            </div>

            <div className={cl.inputContainer}>
                <Select
                    placeholder="Выберите статус"
                    style={selectStyle}
                    value={formData.status}
                    onChange={(value) => handleSelectChange(value, 'status')}
                >
                    {contractorStatus.map((status) => (
                        <Option key={status} value={status}>{status}</Option>
                    ))}
                </Select>
            </div>
        </Modal>
    );
};
