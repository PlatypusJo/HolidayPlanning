import {Button, Image, Layout} from "antd";
import Logo from "../shared/image/logo.png";
import cl from './ui/Header.module.css'
import React, {useState} from "react";
import {LoginOutlined} from "@ant-design/icons";
import AuthorizationModal from "../modal/AutorizationModal/AuthorizationModal";

export const Header = () => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [isAuth, setIsAuth] = useState(false);

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    return (
        <>
            <Layout.Header className={cl.header}>
                <div className={cl.headerLogo}>
                    <Image preview={false} src={Logo} />
                    <div className={cl.headerName}>Планирование праздников</div>
                    <Button
                        icon={<LoginOutlined/>}
                        iconPosition={"end"}
                        className={cl.headerButton}
                        onClick={showModal}
                    >
                        {isAuth ? 'Личный кабинет' : 'Войти'}
                    </Button>
                </div>
            </Layout.Header>

            <AuthorizationModal
                visible={isModalVisible}
                onCancel={handleCancel}
                setIsAuth={setIsAuth}
            />
        </>
    )
}