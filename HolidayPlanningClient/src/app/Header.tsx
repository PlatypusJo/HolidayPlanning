import {Button, Image, Layout} from "antd";
import Logo from "../shared/image/logo.png";
import cl from './ui/Header.module.css'
import React, {useState} from "react";
import {LoginOutlined, LogoutOutlined} from "@ant-design/icons";
import {RoutesPaths} from "../shared/config";
import {useNavigate} from "react-router-dom";
import {useFooterContext} from "../shared/ui/Footer/Footer";
import {AuthorizationModal} from "../modal/AutorizationModal";
import {RegistrationModal} from "../modal/RegistrationModal";

export const Header = () => {
    const [isAuthModalVisible, setIsAuthModalVisible] = useState(false);
    const [isRegistrationModalVisible, setIsRegistrationModalVisible] = useState(false);
    const [isAuth, setIsAuth] = useState(false);
    const navigate = useNavigate()
    const {updateFloatButton } = useFooterContext()
    const showAuthModal = () => {
        setIsAuthModalVisible(true);
    };
    const handleAuthCancel = () => {
        setIsAuthModalVisible(false);
    };

    const showRegistrationModal = () => {
        setIsRegistrationModalVisible(true);
    };
    const handleRegistrationCancel = () => {
        setIsRegistrationModalVisible(false);
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
                        isAuth || localStorage.getItem('userId')
                            ? <Button
                                icon={<LogoutOutlined/>}
                                iconPosition={"end"}
                                className={`${cl.headerButton}`}
                                onClick={onLogOut}
                            >
                                Выйти
                            </Button>
                            : <div className={cl.headerButtonContainer}>
                                <Button
                                    type={"link"}
                                    icon={<LoginOutlined/>}
                                    iconPosition={"end"}
                                    className={cl.registrationButton}
                                    onClick={showRegistrationModal}
                                >
                                    Регистриация
                                </Button>
                                <Button
                                    icon={<LoginOutlined/>}
                                    iconPosition={"end"}
                                    className={cl.loginButton}
                                    onClick={showAuthModal}
                                >
                                    Войти
                                </Button>
                            </div>
                    }
                </div>
            </Layout.Header>
            <AuthorizationModal
                visible={isAuthModalVisible}
                onCancel={handleAuthCancel}
                setIsAuth={setIsAuth}
            />
            <RegistrationModal
                visible={isRegistrationModalVisible}
                onCancel={handleRegistrationCancel}
            />
        </>
    )
}