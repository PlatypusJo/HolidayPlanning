import React, {useState} from 'react';
import {Input} from 'antd';
import {Modal} from '../../shared/ui';
import cl from './ui/AuthorizationModal.module.css'
import Logo from '../../shared/image/modal-logo.png';
import {RoutesPaths} from "../../shared/config";
import {useNavigate} from "react-router-dom";

interface AuthorizationModalProps {
    setIsAuth: (value: boolean) => void;
    visible: boolean;
    onCancel: () => void;
}

export const AuthorizationModal: React.FC<AuthorizationModalProps> =
    ({ setIsAuth, visible, onCancel }) => {
        const initialFormState = {
            login: '',
            password: '',
        };

        const navigate = useNavigate()
        const [formData, setFormData] = useState(initialFormState);

        const handleAuth = () => {
            localStorage.setItem('userId', `${Date.now()}`)
            navigate(RoutesPaths.PROFILE)
            setIsAuth(true);
            onCancel();
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


