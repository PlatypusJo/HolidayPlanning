import {Button, Image, Layout} from "antd";
import Logo from "../shared/image/logo.png";
import cl from './ui/Header.module.css'
import React from "react";
import {LoginOutlined} from "@ant-design/icons";
import {useNotification} from "../shared/hook";

export const Header = () => {
    const [contextHolder, notification] = useNotification(2)

    return (
        <>
            {contextHolder}
            <Layout.Header className={cl.header}>
                <div className={cl.headerLogo}>
                    <Image preview={false} src={Logo} />
                    <div className={cl.headerName}>Планирование праздников</div>
                    <Button
                        icon={<LoginOutlined/>}
                        iconPosition={"end"}
                        className={cl.headerButton}
                        onClick={() => {notification.info("Авторизация в разработке..")}}
                    >
                        Войти
                    </Button>
                </div>
            </Layout.Header>
        </>
    )
}