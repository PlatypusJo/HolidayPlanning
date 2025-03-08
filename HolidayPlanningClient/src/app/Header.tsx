import {Button, Image, Layout} from "antd";
import Logo from "../shared/image/logo.png";
import cl from './ui/Header.module.css'
import React, {useState} from "react";
import {LoginOutlined, LogoutOutlined} from "@ant-design/icons";
import {RoutesPaths} from "../shared/config";
import {useNavigate} from "react-router-dom";
import {useFooterContext} from "../shared/ui/Footer/Footer";
import {AuthorizationModal} from "../modal/AutorizationModal";

export const Header = () => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [isAuth, setIsAuth] = useState(false);
    const navigate = useNavigate()
    const {updateFloatButton } = useFooterContext()

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    const onLogOut = () => {
        setIsAuth(false)
        localStorage.removeItem('userId')
        navigate(RoutesPaths.HOME)
        updateFloatButton(undefined)
    }

    return (
        <>
            <Layout.Header className={cl.header}>
                <div className={cl.headerLogo}>
                    <Image preview={false} src={Logo} />
                    <div className={cl.headerName}>Планирование праздников</div>
                    {
                        isAuth
                            ? <Button
                                icon={<LogoutOutlined/>}
                                iconPosition={"end"}
                                className={cl.headerButton}
                                onClick={onLogOut}
                            >
                                Выйти
                            </Button>
                            : <Button
                                icon={<LoginOutlined/>}
                                iconPosition={"end"}
                                className={cl.headerButton}
                                onClick={showModal}
                            >
                                Войти
                            </Button>
                    }
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