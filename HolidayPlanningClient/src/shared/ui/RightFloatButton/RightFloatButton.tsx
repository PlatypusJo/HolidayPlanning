import {Button, Tooltip} from "antd";
import cl from './RightFloatButton.module.css'
import React from "react";
import {useFooterContext} from "../Footer/Footer";

export const RightFloatButton: React.FC<{
    tooltipTitle: string
    buttonIcon: React.ReactNode,
    onClick?: React.MouseEventHandler<HTMLElement>,

}> = ({ tooltipTitle, buttonIcon, onClick }) => {
    const { showScrollTop } = useFooterContext()

    return (
        <Tooltip title={tooltipTitle}>
            <Button
                type="primary"
                shape="circle"
                icon={buttonIcon}
                size="large"
                className={`${cl.floatButton} ${showScrollTop ? cl.floatButtonSifted : cl.floatButtonDefault}`}
                onClick={onClick}
            />
        </Tooltip>
    );
};