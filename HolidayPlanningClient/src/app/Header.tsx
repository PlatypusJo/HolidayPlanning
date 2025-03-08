import {Button, Image, Layout} from "antd";
import Logo from "../shared/image/logo.png";
import cl from './ui/Header.module.css'
import React from "react";
import {LoginOutlined, LogoutOutlined} from "@ant-design/icons";
import {useNotification} from "../shared/hook";
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../shared/config";
import {useFooterContext} from "../shared/ui/Footer/Footer";

export const Header = () => {
    const navigate = useNavigate()
    const {updateFloatButton } = useFooterContext()

    const onLogIn = () => {
        localStorage.setItem('userId', `${Date.now()}`)
        navigate(RoutesPaths.EVENTS)
    }

    const onLogOut = () => {
        localStorage.removeItem('userId')
        navigate(RoutesPaths.HOME)
        updateFloatButton(undefined)
    }

    return (
        <>
            <Layout.Header className={cl.header}>
                <div className={cl.headerLogo}>
                    <Image preview={false} src={Logo}/>
                    <div className={cl.headerName}>Планирование праздников</div>
                    {
                        localStorage.getItem('userId')
                            ? <Button
                                icon={<LogoutOutlined/>}
                                iconPosition={"end"}
                                className={cl.headerButton}
                                onClick={() => {
                                    onLogOut()
                                }}
                            >
                                Выйти
                            </Button>
                            : <Button
                                icon={<LoginOutlined/>}
                                iconPosition={"start"}
                                className={cl.headerButton}
                                onClick={() => {
                                    onLogIn()
                                }}
                            >
                                Войти
                            </Button>
                    }
                </div>
            </Layout.Header>
        </>
    )
}