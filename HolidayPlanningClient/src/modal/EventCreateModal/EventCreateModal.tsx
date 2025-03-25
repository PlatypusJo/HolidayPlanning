import React, {useState} from "react";
import {Input, DatePicker, TimePicker} from "antd";
import dayjs, {Dayjs} from "dayjs";
import cl from './ui/EventCreateModal.module.css';
import Logo from '../../shared/image/modal-logo.png';
import {dateTimePickerStyle, inputStyle} from "./config/theme";
import {useFetching, useNotification} from "../../shared/hook";
import {createEvent, EventData} from "../../shared/api";
import {Modal} from "../../shared/ui";


type FormData = {
    title: string;
    budget: string;
    startDate: string;
    startTime: string;
    endDate: string;
    endTime: string;
};

export const EventCreateModal: React.FC<{
    visible: boolean;
    onCreateEvent?: (newEvent: EventData) => void;
    onCancel: () => void;
}> = ({ visible, onCreateEvent, onCancel }) => {
    const initialFormState: FormData = {
        title: '',
        budget: '',
        startDate: '',
        startTime: '',
        endDate: '',
        endTime: '',
    };

    const [formData, setFormData] = useState<FormData>(initialFormState);
    const notification = useNotification()
    const [fetchCreateEvents, isLoadingFetchCreateEvents, errorFetchCreateEvents] = useFetching(async () => {
        try {
            const response = await createEvent({
                id: `${Date.now()}`,
                title: formData.title,
                budget: Number(formData.budget),
                startDate: new Date(`${formData.startDate} ${formData.startTime}`),
                endDate: new Date(`${formData.endDate} ${formData.endTime}`),
            })
            if (response) {
                onCreateEvent && onCreateEvent(response)
                notification.success(`Мероприятие '${formData.title}' успешно создано!`)
            }
        } catch (e) {
            notification.error(`Ошибка при создании меропрития: ${errorFetchCreateEvents}`)
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
        fetchCreateEvents()
    };

    const handleClose = () => {
        setFormData(initialFormState);
        onCancel();
    };

    return (
        <Modal
            onCancel={handleClose}
            onOk={handleSubmit}
            okButtonText={"Создать"}
            icon={Logo}
            modalTitle={"Создать новое мероприятие"}
            description={"Настройте мероприятие и приступите к его планированию"}
            visible={visible}
            disabled={Object.values(formData).some(value => value === '')}
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
