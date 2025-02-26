import React, {useState} from "react";
import {Modal, Input, Image, DatePicker, TimePicker, ConfigProvider} from "antd";
import dayjs, {Dayjs} from "dayjs";
import cl from './ui/EventCreateModal.module.css';
import Logo from '../../shared/image/modal-logo.png';
import {cancelButtonStyle, dateTimePickerStyle, inputStyle, modalTheme, okButtonStyle} from "./config/theme";
import {useFetching, useNotification} from "../../shared/hook";
import {createEvent, EventData} from "../../shared/api";


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
    onCreateEvent: (newEvent: EventData) => void;
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
    const [contextHolder, notification] = useNotification(5)
    const [fetchCreateEvents, isLoadingFetchCreateEvents, errorFetchCreateEvents] = useFetching(async () => {
        try {
            const response = await createEvent({
                title: formData.title,
                budget: Number(formData.budget),
                startDate: new Date(`${formData.startDate} ${formData.startTime}`),
                endDate: new Date(`${formData.endDate} ${formData.endTime}`),
            })
            if (response) {
                onCreateEvent(response)
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
        console.log("Создано мероприятие:", formData);
        fetchCreateEvents()
    };

    const handleClose = () => {
        setFormData(initialFormState);
        onCancel();
    };

    return (
        <>
            {contextHolder}
            <ConfigProvider theme={modalTheme}>
                <Modal
                    className={cl.modal}
                    open={visible}
                    okButtonProps={{
                        style: okButtonStyle,
                        disabled: Object.values(formData).some(value => value === '')
                    }}
                    okText="Создать"
                    title={
                        <span className={cl.modalTitle}>
                            <Image width={105} height={80} preview={false} src={Logo}/>
                            <div>Создать новое мероприятие</div>
                            <div
                                className={cl.titleDescription}>Настройте мероприятие и приступите к его планированию</div>
                        </span>
                    }
                    width={"30vw"}
                    onCancel={handleClose}
                    centered
                    cancelButtonProps={{ style: cancelButtonStyle }}
                    onOk={handleSubmit}
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
            </ConfigProvider>
        </>
    );
};
