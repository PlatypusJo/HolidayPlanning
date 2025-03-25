import React, {useState} from "react";
import {Input, Image, DatePicker, TimePicker, ConfigProvider} from "antd";
import dayjs, {Dayjs} from "dayjs";
import cl from './ui/EventChangeModal.module.css';
import Logo from '../../shared/image/modal-logo.png';
import {dateTimePickerStyle, inputStyle} from "./config/theme";
import {useFetching, useNotification} from "../../shared/hook";
import {changeEvent, createEvent, EventData} from "../../shared/api";
import {Modal} from "../../shared/ui";


type FormData = {
    title: string;
    budget: string;
    startDate: string;
    startTime: string;
    endDate: string;
    endTime: string;
};

export const EventChangeModal: React.FC<{
    event: EventData
    visible: boolean;
    onChangeEvent: (eventId: string, newEvent: EventData) => void;
    onCancel: () => void;
}> = ({ event, visible, onChangeEvent, onCancel }) => {
    const initialFormState: FormData = {
        title: event.title,
        budget: `${event.budget}`,
        startDate: dayjs(event.startDate).format('YYYY-MM-DD'),
        startTime: dayjs(event.startDate).format('HH:mm'),
        endDate: dayjs(event.endDate).format('YYYY-MM-DD'),
        endTime: dayjs(event.endDate).format('HH:mm'),
    };

    const [formData, setFormData] = useState<FormData>(initialFormState);
    const notification = useNotification()
    const [fetchChangeEvents, isLoadingFetchChangeEvents, errorFetchChangeEvents] = useFetching(async () => {
        try {
            const eventData = {
                id: event.id,
                title: formData.title,
                budget: Number(formData.budget),
                startDate: new Date(`${formData.startDate} ${formData.startTime}`),
                endDate: new Date(`${formData.endDate} ${formData.endTime}`),
            }
            const response = await changeEvent(event.id, eventData)
            if (response) {
                onChangeEvent(event.id, eventData)
                notification.success(`Мероприятие '${formData.title}' успешно изменено!`)
            }
        } catch (e) {
            notification.error(`Ошибка при изменении меропрития: ${errorFetchChangeEvents}`)
        }
    })

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>, field: keyof FormData) => {
        setFormData(prev => ({
            ...prev,
            [field]: e.target.value,
        }));
    };

    const handleDateChange = (date: Dayjs | null, field: "startDate" | "endDate") => {
        setFormData(prev => ({
            ...prev,
            [field]: date ? date.format('YYYY-MM-DD') : null,
        }));
    };

    const handleTimeChange = (time: Dayjs | null, field: "startTime" | "endTime") => {
        setFormData(prev => ({
            ...prev,
            [field]: time ? time.format('HH:mm') : null,
        }));
    };

    const handleBudgetChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        if (/^\d*\.?\d*$/.test(value) && (value === '' || parseFloat(value) >= 0)) {
            setFormData(prev => ({ ...prev, budget: value }));
        }
    };

    const handleSubmit = () => {
        fetchChangeEvents()
        onCancel();
    };

    const handleClose = () => {
        setFormData(formData);
        onCancel();
    };

    return (
        <Modal
            onCancel={handleClose}
            onOk={handleSubmit}
            icon={Logo}
            visible={visible}
            disabled={Object.values(formData).some(value => value === '')}
            okButtonText={"Сохранить"}
            modalTitle={"Изменить мероприятие"}
            description={"Измените мероприятие и продолжайте его планирование"}
        >
            <div className={cl.inputContainer}>
                <Input
                    placeholder="Название вашего мероприятия"
                    style={inputStyle}
                    value={formData.title}
                    onChange={(e) => handleInputChange(e, 'title')}
                />
            </div>

            <div className={cl.inputDatetimeContainer}>
                <DatePicker
                    placeholder="Дата начала"
                    style={dateTimePickerStyle}
                    value={formData.startDate ? dayjs(formData.startDate, 'YYYY-MM-DD') : null}
                    onChange={(date) => handleDateChange(date, 'startDate')}
                />
                <TimePicker
                    placeholder="Время начала"
                    style={dateTimePickerStyle}
                    value={formData.startTime ? dayjs(formData.startTime, 'HH:mm') : null}
                    onChange={(time) => handleTimeChange(time, 'startTime')}
                    format="HH:mm"
                />
            </div>

            <div className={cl.inputDatetimeContainer}>
                <DatePicker
                    placeholder="Дата конца"
                    style={dateTimePickerStyle}
                    value={formData.endDate ? dayjs(formData.endDate, 'YYYY-MM-DD') : null}
                    onChange={(date) => handleDateChange(date, 'endDate')}
                />
                <TimePicker
                    placeholder="Время конца"
                    style={dateTimePickerStyle}
                    value={formData.endTime ? dayjs(formData.endTime, 'HH:mm') : null}
                    onChange={(time) => handleTimeChange(time, 'endTime')}
                    format="HH:mm"
                />
            </div>

            <div className={cl.inputContainer}>
                <Input
                    placeholder="Бюджет вашего мероприятия"
                    style={inputStyle}
                    value={formData.budget}
                    onChange={handleBudgetChange}
                    type="number"
                    min={0}
                />
            </div>
        </Modal>
    );
};
