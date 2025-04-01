import React, {useState} from "react";
import {Input, Radio, RadioChangeEvent, Select} from "antd";
import cl from './ui/MemberCreateModal.module.css'
import Logo from '../../shared/image/modal-logo.png';
import {inputStyle, selectStyle} from "./config/theme";
import {useFetching, useNotification} from "../../shared/hook";
import {ContractorStatus,
    contractorStatus, createMember, getEnumMapping, memberCategories, MemberCategory, MemberData, menuCategories, MenuCategory
} from "../../shared/api";
import {Modal} from "../../shared/ui";

const { Option } = Select;

type FormData = {
    memberCategory: string,
    memberStatus: string,
    menuCategory: string,
    fio: string,
    phoneNumber: string,
    email: string,
    comment: string,
    isChild: boolean,
    isMale: boolean,
    seat: string
};

export const MemberCreateModal: React.FC<{
    eventId: string,
    visible: boolean;
    onCreateMember: (newMember: MemberData) => void;
    onCancel: () => void;
}> = ({ eventId, visible, onCreateMember, onCancel }) => {
    const initialFormState: FormData = {
        memberCategory: memberCategories[0] as string,
        memberStatus: contractorStatus[0] as string,
        menuCategory: menuCategories[0] as string,
        fio: '',
        phoneNumber: '',
        email: '',
        comment: '',
        isChild: false,
        isMale: true,
        seat: ''
    };

    const [formData, setFormData] = useState<FormData>(initialFormState);
    const [errors, setErrors] = useState<{ phoneNumber?: string; email?: string }>({});
    const notification = useNotification()

    const [fetchCreateMember, isLoadingFetchCreateMember, errorFetchCreateMember] = useFetching(async () => {
        try {
            const newMemberId = `${Date.now()}`
            const response = await createMember({
                id: newMemberId,
                holidayId: eventId,
                memberCategoryId: `${getEnumMapping(MemberCategory, formData.memberCategory as keyof typeof MemberCategory)}`,
                memberStatusId: `${getEnumMapping(ContractorStatus, formData.memberStatus as keyof typeof ContractorStatus)}`,
                menuCategoryId: `${getEnumMapping(MenuCategory, formData.menuCategory as keyof typeof MenuCategory)}`,
                fio: formData.fio,
                phoneNumber: formData.phoneNumber,
                email: formData.email,
                comment: formData.comment,
                isChild: formData.isChild,
                isMale: formData.isMale,
                seat: formData.seat
            })
            if (response && response.status === 200) {
                onCreateMember({
                    id: newMemberId,
                    holidayId: eventId,
                    ...formData
                })
                notification.success(`Гость '${formData.fio}' успешно добавлен!`)
            }
        } catch (e) {
            notification.error(`Ошибка при добавлении гостя: ${errorFetchCreateMember}`)
        }
    })

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

    const handleRadioChange = (value: boolean, field: keyof FormData) => {
        setFormData(prev => ({ ...prev, [field]: value }));
    };

    const handleSelectChange = (value: string, field: keyof FormData) => {
        setFormData(prev => ({ ...prev, [field]: value }));
    };


    const handleSubmit = () => {
        fetchCreateMember()
        handleClose();
    };

    const handleClose = () => {
        setFormData(initialFormState);
        setErrors({});
        onCancel();
    };

    return (
        <Modal
            onCancel={handleClose}
            onOk={handleSubmit}
            okButtonText={"Добавить"}
            icon={Logo}
            loading={isLoadingFetchCreateMember}
            modalTitle={"Добавление гостя"}
            description={"Напиши информацию о госте своего меропрития"}
            visible={visible}
            disabled={Object.values(formData).some(value => value === '')}
        >
            <div className={cl.inputContainer}>
                <Input
                    placeholder="ФИО гостя"
                    style={inputStyle}
                    value={formData.fio}
                    onChange={(e) => handleInputChange(e, 'fio')}
                />
            </div>

            <div className={cl.inputBoolean}>
                <div className={cl.booleanText}>Пол:</div>
                <Radio.Group
                    buttonStyle="solid"
                    value={formData.isMale ? 'male' : 'female'}
                    onChange={(e) => handleRadioChange(e.target.value === 'male', 'isMale')}
                >
                    <Radio.Button value="male">Мужской</Radio.Button>
                    <Radio.Button value="female">Женский</Radio.Button>
                </Radio.Group>
            </div>

            <div className={cl.inputBoolean}>
                <div className={cl.booleanText}>Возрастная категория:</div>
                <Radio.Group
                    buttonStyle="solid"
                    value={formData.isChild ? 'child' : 'adult'}
                    onChange={(e) => handleRadioChange(e.target.value === 'child', 'isChild')}
                >
                    <Radio.Button value="child">Ребенок</Radio.Button>
                    <Radio.Button value="adult">Взрослый</Radio.Button>
                </Radio.Group>
            </div>

            <div className={cl.inputContainer}>
                <Select
                    placeholder="Выберите категорию гостя"
                    style={selectStyle}
                    value={formData.memberCategory}
                    onChange={(value) => handleSelectChange(value, 'memberCategory')}
                >
                    {memberCategories.map((category) => (
                        <Option key={category} value={category}>{category}</Option>
                    ))}
                </Select>
            </div>

            <div className={cl.inputContainer}>
                <Select
                    placeholder="Выберите категорию меню"
                    style={selectStyle}
                    value={formData.menuCategory}
                    onChange={(value) => handleSelectChange(value, 'menuCategory')}
                >
                    {menuCategories.map((category) => (
                        <Option key={category} value={category}>{category}</Option>
                    ))}
                </Select>
            </div>

            <div className={cl.inputContainer}>
                <Input.TextArea
                    placeholder="Комментарий"
                    style={inputStyle}
                    value={formData.comment}
                    onChange={(e) => handleInputChange(e, 'comment')}
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

            <div className={cl.inputContainerPhEmail}>
                <Input
                    placeholder="Посадка"
                    style={inputStyle}
                    value={formData.seat}
                    onChange={(e) => handleInputChange(e, 'seat')}
                    type="email"
                />
            </div>

            <div className={cl.inputContainer}>
                <Select
                    placeholder="Выберите статус"
                    style={selectStyle}
                    value={formData.memberStatus}
                    onChange={(value) => handleSelectChange(value, 'memberStatus')}
                >
                    {contractorStatus.map((status) => (
                        <Option key={status} value={status}>{status}</Option>
                    ))}
                </Select>
            </div>
        </Modal>
    );
};
