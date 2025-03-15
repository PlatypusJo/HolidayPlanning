import React, {useState} from 'react';
import {Input} from 'antd';
import {Modal} from '../../shared/ui';
import cl from './ui/RegistrationModal.module.css'
import Logo from '../../shared/image/modal-logo.png';
import {useFetching, useNotification} from "../../shared/hook";
import {registration, RegistrationData} from "../../shared/api";

export const RegistrationModal: React.FC<{
    visible: boolean;
    onCancel: () => void;
}> =
    ({ visible, onCancel }) => {
        const initialFormState: RegistrationData = {
            login: '',
            password: '',
            repeatPassword: ''
        };

        const notification = useNotification()
        const [formData, setFormData] = useState(initialFormState);
        const [isPasswordError, setIsPasswordError] = useState(false);
        const [fetchRegistration, isLoadingRegistration, isErrorRegistration] = useFetching(async () => {
            try {
                const response = await registration(formData)

                if (response) {
                    notification.success(`Вы успешно зарегистрировались в системе!`)
                }
            } catch (e) {
                notification.error(`Ошибка регистрации: ${isErrorRegistration}`)
            }
        })
        const handleRegistration = () => {
            fetchRegistration()
            handleClose()
        };

        const handleInputChange = (field: 'login' | 'password' | 'repeatPassword', value: string) => {
            setFormData(prev => ({ ...prev, [field]: value }));

            if (field === 'password' || field === 'repeatPassword') {
                setIsPasswordError(
                    field === 'password'
                        ? value !== formData.repeatPassword
                        : value !== formData.password
                )
            }
        };

        const handleClose = () => {
            setFormData(initialFormState);
            onCancel();
        };

        return (
            <Modal
                onCancel={handleClose}
                onOk={handleRegistration}
                okButtonText="Подтвердить"
                icon={Logo}
                loading={isLoadingRegistration}
                modalTitle="Регистрация"
                description="Введите данные для регистрации в системе"
                visible={visible}
                disabled={!formData.login || !formData.password || !formData.repeatPassword || isPasswordError}
            >
                <div className={cl.inputLogin}>
                    <div className={cl.textInput}>Логин:</div>
                    <Input
                        style={{ borderColor: '#A7CEFC80', backgroundColor: '#A7CEFC80', color: 'white' }}
                        value={formData.login}
                        onChange={(e) => handleInputChange('login', e.target.value)}
                    />
                </div>
                <div className={cl.inputPasswords}>
                    <div className={cl.textInput}>Пароль:</div>
                    <Input.Password
                        status={isPasswordError ? 'error' : ''}
                        style={{
                            borderColor: isPasswordError ? '#ff0000' : '#A7CEFC80',
                            backgroundColor: '#A7CEFC80',
                            color: 'white'
                        }}
                        value={formData.password}
                        onChange={(e) => handleInputChange('password', e.target.value)}
                    />
                </div>
                <div className={cl.inputPasswords}>
                    <div className={cl.textInput}>Повторите пароль:</div>
                    <Input.Password
                        status={isPasswordError ? 'error' : ''}
                        style={{
                            borderColor: isPasswordError ? '#ff0000' : '#A7CEFC80',
                            backgroundColor: '#A7CEFC80',
                            color: 'white'
                        }}
                        value={formData.repeatPassword}
                        onChange={(e) => handleInputChange('repeatPassword', e.target.value)}
                    />
                </div>
            </Modal>
        );
    };


