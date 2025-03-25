import React, {useState} from 'react';
import {Input} from 'antd';
import {Modal} from '../../shared/ui';
import cl from './ui/AuthorizationModal.module.css'
import Logo from '../../shared/image/modal-logo.png';
import {RoutesPaths} from "../../shared/config";
import {useNavigate} from "react-router-dom";
import {useFetching, useNotification} from "../../shared/hook";
import {auth, LoginData} from "../../shared/api";

interface AuthorizationModalProps {
    setIsAuth: (value: boolean) => void;
    visible: boolean;
    onCancel: () => void;
}

export const AuthorizationModal: React.FC<AuthorizationModalProps> =
    ({ setIsAuth, visible, onCancel }) => {
        const initialFormState: LoginData = {
            login: '',
            password: '',
        };

        const navigate = useNavigate()
        const notification = useNotification()
        const [formData, setFormData] = useState(initialFormState);
        const [fetchAuth, isLoadingAuth, isErrorAuth] = useFetching(async () => {
            try {
                const response = await auth(formData)

                if(response){
                    localStorage.setItem('userId', response.userID)
                    navigate(RoutesPaths.PROFILE)
                    setIsAuth(true);
                    notification.success(`${formData.login} приведствую в системе!`)
                }
            } catch (e) {
                notification.error(`Ошибка авторизации: ${isErrorAuth}`)
            }
        })
        const handleAuth = () => {
            fetchAuth()
            handleClose()
        };

        const handleInputChange = (field: 'login' | 'password', value: string) => {
            setFormData(prev => ({ ...prev, [field]: value }));
        };

        const handleClose = () => {
            setFormData(initialFormState);
            onCancel();
        };

        return (
            <Modal
                onCancel={handleClose}
                onOk={handleAuth}
                okButtonText="Войти"
                icon={Logo}
                loading={isLoadingAuth}
                modalTitle="Вход в личный кабинет"
                description="Введите данные для входа в систему"
                visible={visible}
                disabled={!formData.login || !formData.password}
            >
                <div className={cl.inputLogin}>
                    <div className={cl.textInput}>Логин:</div>
                    <Input
                        style={{ borderColor: '#A7CEFC80', backgroundColor: '#A7CEFC80', color: 'white' }}
                        value={formData.login}
                        onChange={(e) => handleInputChange('login', e.target.value)}
                    />
                </div>
                <div className={cl.inputPassword}>
                    <div className={cl.textInput}>Пароль:</div>
                    <Input.Password
                        style={{ borderColor: '#A7CEFC80', backgroundColor: '#A7CEFC80', color: 'white' }}
                        value={formData.password}
                        onChange={(e) => handleInputChange('password', e.target.value)}
                    />
                </div>
            </Modal>
        );
    };


