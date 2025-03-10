import cl from "./ContentBlock.module.css";
import { Button, Image } from "antd";
import { ArrowRightOutlined } from "@ant-design/icons";
import React from "react";

export const ContentBlock: React.FC<{
    title: string,
    src: string,
    onBtnClick?: () => void,
    buttonSettings?: {
        btnIcon?: React.ReactNode,
        iconPosition?: "start" | "end",
        btnText: string
    },
    description?: string,
    isTextFirst?: boolean, 
    variant: "white-bg-left-text-right-image" | "white-bg-left-image-right-text" | "blue-bg-left-text-right-image" | "blue-left-image-right-text"
}> = ({ title, src, onBtnClick, buttonSettings, description, isTextFirst, variant}) => {
    return (
        <div className={`${cl.contentBlock} ${cl[variant]}`}>
            {isTextFirst ? (
                <>
                    <div className={cl.textInfoContainer}>
                        <div className={cl.titleInfoContainer}>{title}</div>
                        {description && <div className={cl.descrInfoContainer}>{description}</div>}
                        {buttonSettings && (
                            <Button
                                icon={buttonSettings?.btnIcon ?? <ArrowRightOutlined />}
                                iconPosition={buttonSettings?.iconPosition ?? "start"}
                                className={cl.backButtonInfoContainer}
                                onClick={() => { onBtnClick && onBtnClick(); }}
                            >
                                {buttonSettings?.btnText ?? 'Начать'}
                            </Button>
                        )}
                    </div>
                    <Image className={cl.imageInfoContainer} preview={false} src={src} />
                </>
            ) : (
                <>
                    <Image className={cl.imageInfoContainer} preview={false} src={src} />
                    <div className={cl.textInfoContainer}>
                        <div className={cl.titleInfoContainer}>{title}</div>
                        {description && <div className={cl.descrInfoContainer}>{description}</div>}
                        <Button
                            icon={buttonSettings?.btnIcon ?? <ArrowRightOutlined />}
                            iconPosition={buttonSettings?.iconPosition ?? "start"}
                            className={cl.backButtonInfoContainer}
                            onClick={() => { onBtnClick && onBtnClick(); }}
                        >
                            {buttonSettings?.btnText ?? 'Подробнее'}
                        </Button>
                    </div>
                </>
            )}
        </div>
    );
};
