import cl from "./InfoContainer.module.css"
import {Button, Image} from "antd";
import {ArrowLeftOutlined} from "@ant-design/icons";
import React from "react";

export const InfoContainer: React.FC<{
    title: string,
    src: string,
    onBtnClick?: () => void,
    buttonSettings?: {
        btnIcon?: React.ReactNode
        iconPosition?: "start" | "end"
        btnText: string
    }
    description?: string
}> = ({title, src, onBtnClick, buttonSettings, description}) => {
    return (
        <div className={cl.infoContainer}>
            <div className={cl.textInfoContainer}>
                <div className={cl.titleInfoContainer}>{title}</div>
                {description && <div className={cl.descrInfoContainer}>{description}</div>}
                <Button icon={buttonSettings?.btnIcon ?? <ArrowLeftOutlined />} iconPosition={buttonSettings?.iconPosition ?? "start"} className={cl.backButtonInfoContainer} onClick={() => {onBtnClick && onBtnClick()}}>
                    {buttonSettings?.btnText ?? 'Назад'}
                </Button>
            </div>
            <Image className={cl.imageInfoContainer} preview={false} src={src} />
        </div>
    );
};