import React from "react";
import cl from "./Modal.module.css";
import {ConfigProvider, Image, Modal as AntdModal} from "antd";

const modalTheme = {
    components: {
        Modal: {
            contentBg: '#043873',
            headerBg: '#043873',
            footerBg: '#043873',
            titleColor: '#ffffff',
            colorText: '#ffffff',
        },
        Select: {
            colorBgContainer: "rgba(167, 206, 252, 0.5)",
            colorBorder: "rgba(217,217,217,0)",
            colorText: "rgb(255,255,255)",
            colorBgElevated: "rgb(100,122,148)",
            optionSelectedBg: "rgba(167, 206, 252, 0.5)"
        },
        Radio: {
            buttonSolidCheckedBg: "linear-gradient(to right, #FE9449, #EF5282)",
            buttonSolidCheckedHoverBg: "linear-gradient(to right, #FE9449, #EF5282)",
            colorPrimary: "#ffffff",
            colorBgContainer: "rgba(167, 206, 252, 0.5)",
            colorBorder: "rgba(217,217,217,0)"
        }
    },
}

const okButtonStyle = {
    marginRight: '4%',
    marginTop: '3%',
    background: 'linear-gradient(to right, #FE9449, #EF5282)',
    borderColor: '#FE9449',
    borderRadius: '8px'
}

const cancelButtonStyle = { display: 'none' }

export const Modal: React.FC<{
    children: React.ReactNode,
    onCancel: (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void,
    onOk: (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void
    modalTitle: string,
    description: string,
    icon: string,
    visible: boolean,
    disabled?: boolean,
    okButtonText: string,
    loading?: boolean
}> = ({ children, loading, onCancel, onOk, icon, description, modalTitle, visible, disabled, okButtonText }) => {
    return (
        <ConfigProvider theme={modalTheme}>
            <AntdModal
                className={cl.modal}
                open={visible}
                okButtonProps={{
                    style: okButtonStyle,
                    disabled: disabled
                }}
                confirmLoading={loading}
                okText={okButtonText}
                title={
                    <span className={cl.modalTitle}>
                            <Image width={105} height={80} preview={false} src={icon}/>
                            <div>{modalTitle}</div>
                            <div
                                className={cl.titleDescription}>{description}</div>
                        </span>
                }
                width={"30vw"}
                onCancel={onCancel}
                centered
                cancelButtonProps={{ style: cancelButtonStyle }}
                onOk={onOk}
            >
                {children}
            </AntdModal>
        </ConfigProvider>
    );
};