import {Button, Tooltip} from "antd";
import {ArrowUpOutlined} from "@ant-design/icons";
import cl from "./FloatScrollTopButton.module.css"

export const FloatScrollTopButton = () => {
    const scrollToTop = () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    };

    return (
        <>
            <Tooltip title="Наверх">
                <Button
                    type="primary"
                    shape="circle"
                    icon={<ArrowUpOutlined/>}
                    size="large"
                    className={cl.scrollTopButton}
                    onClick={scrollToTop}
                />
            </Tooltip>
        </>
    );
};