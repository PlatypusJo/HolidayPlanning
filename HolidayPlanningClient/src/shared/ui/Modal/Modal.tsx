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
    },
}

const okButtonStyle = {
    marginRight: '42.5%',
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
    okButtonText: string
}> = ({ children, onCancel, onOk, icon, description, modalTitle, visible, disabled, okButtonText }) => {
    return (
        <ConfigProvider theme={modalTheme}>
            <AntdModal
                className={cl.modal}
                open={visible}
                okButtonProps={{
                    style: okButtonStyle,
                    disabled: disabled
                }}
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