import React, { useState } from 'react';
import { Input } from 'antd';
import { Modal } from '../../shared/ui';
import cl from './ui/AuthorizationModal.module.css'
import Logo from '../../shared/image/modal-logo.png';

interface AuthorizationModalProps {
  setIsAuth: (value: boolean) => void;
  visible: boolean;
  onCancel: () => void;
}

const AuthorizationModal: React.FC<AuthorizationModalProps> = ({
  setIsAuth,
  visible,
  onCancel,
}) => {
  const [formData, setFormData] = useState({
    login: '',
    password: '',
  });

  const handleAuth = () => {
    setIsAuth(true);
    onCancel();
  };

  const handleInputChange = (field: 'login' | 'password', value: string) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  return (
    <Modal
      onCancel={onCancel}
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

export default AuthorizationModal;

