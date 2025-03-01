import cl from "./InfoContainer.module.css"
import {Button, Image} from "antd";
import {ArrowLeftOutlined} from "@ant-design/icons";
import React from "react";

export const InfoContainer: React.FC<{title: string, src: string, onRouteBack?: () => void}> = ({title, src, onRouteBack}) => {
    return (
        <div className={cl.infoContainer}>
            <div className={cl.textInfoContainer}>
                <div className={cl.titleInfoContainer}>{title}</div>
                <Button icon={<ArrowLeftOutlined />} iconPosition={"start"} className={cl.backButtonInfoContainer} onClick={() => {onRouteBack && onRouteBack()}}>
                    Назад
                </Button>
            </div>
            <Image className={cl.imageInfoContainer} preview={false} src={src} />
        </div>
    );
};