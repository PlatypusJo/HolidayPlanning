import React, { useState } from "react";
import { Modal, Input, Image, DatePicker, TimePicker, ConfigProvider } from "antd";
import dayjs, { Dayjs } from "dayjs";
import cl from './ui/EventCreateModal.module.css';
import Logo from '../../shared/image/modal-logo.png';

type EventCreateModalProps = {
    visible: boolean;
    onCancel: () => void;
};

type FormData = {
    name: string;
    budget: string;
    date_start: string | null;
    time_start: string | null;
    date_end: string | null;
    time_end: string | null;
};

export const EventCreateModal: React.FC<EventCreateModalProps> = ({ visible, onCancel }) => {
    const initialFormState: FormData = {
        name: '',
        budget: '',
        date_start: null,
        time_start: null,
        date_end: null,
        time_end: null,
    };

    const [formData, setFormData] = useState<FormData>(initialFormState);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>, field: keyof FormData) => {
        setFormData(prev => ({
            ...prev,
            [field]: e.target.value,
        }));
    };

    const handleDateChange = (date: Dayjs | null, field: "date_start" | "date_end") => {
        setFormData(prev => ({
            ...prev,
            [field]: date ? date.format('YYYY-MM-DD') : null,
        }));
    };

    const handleTimeChange = (time: Dayjs | null, field: "time_start" | "time_end") => {
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
    };

    const handleClose = () => {
        setFormData(initialFormState);
        onCancel();
    };

    return (
        <ConfigProvider
            theme={{
                components: {
                    Modal: {
                        contentBg: '#043873',
                        headerBg: '#043873',
                        footerBg: '#043873',
                        titleColor: '#ffffff',
                        colorText: '#ffffff',
                    },
                },
            }}
        >
            <Modal
                className={cl.Modal}
                open={visible}
                okButtonProps={{ style: { marginRight: '42.5%', marginTop: '3%', background: 'linear-gradient(to right, #FE9449, #EF5282)' } }}
                okText="Создать"
                title={
                    <span className={cl.SpanTitle}>
                        <Image width={105} height={80} preview={false} src={Logo} />
                        <div>Создать новое мероприятие</div>
                        <div style={{ fontSize: "1.7vh" }}>Настройте мероприятие и приступите к его планированию</div>
                    </span>
                }
                width={"30vw"}
                onCancel={handleClose}
                centered
                cancelButtonProps={{ style: { display: 'none' } }}
                onOk={handleSubmit}
            >
                <div className={cl.Input}>
                    <Input
                        className="input-name"
                        placeholder="Название вашего мероприятия"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', color: '#ffffff' }}
                        value={formData.name}
                        onChange={(e) => handleInputChange(e, 'name')}
                    />
                </div>

                <div className={cl.InputDatetime}>
                    <DatePicker
                        className="input-date"
                        placeholder="Дата начала"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', width: '100%', color: '#ffffff' }}
                        value={formData.date_start ? dayjs(formData.date_start, 'YYYY-MM-DD') : null}
                        onChange={(date) => handleDateChange(date, 'date_start')}
                    />
                    <TimePicker
                        className="input-time"
                        placeholder="Время начала"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', width: '100%', color: '#ffffff' }}
                        value={formData.time_start ? dayjs(formData.time_start, 'HH:mm') : null}
                        onChange={(time) => handleTimeChange(time, 'time_start')}
                        format="HH:mm"
                    />
                </div>

                <div className={cl.InputDatetime}>
                    <DatePicker
                        className="input-date"
                        placeholder="Дата начала"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', width: '100%', color: '#ffffff' }}
                        value={formData.date_start ? dayjs(formData.date_start, 'YYYY-MM-DD') : null}
                        onChange={(date) => handleDateChange(date, 'date_end')}
                    />
                    <TimePicker
                        className="input-time"
                        placeholder="Время начала"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', width: '100%', color: '#ffffff' }}
                        value={formData.time_start ? dayjs(formData.time_start, 'HH:mm') : null}
                        onChange={(time) => handleTimeChange(time, 'time_end')}
                        format="HH:mm"
                    />
                </div>

                <div className={cl.Input}>
                    <Input
                        className="input-budget"
                        placeholder="Бюджет вашего мероприятия"
                        style={{ backgroundColor: 'rgba(167, 206, 252, 0.5)', color: '#ffffff' }}
                        value={formData.budget}
                        onChange={handleBudgetChange}
                        type="number"
                        min={0}
                    />
                </div>
            </Modal>
        </ConfigProvider>
    );
};
