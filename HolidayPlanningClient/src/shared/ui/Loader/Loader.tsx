import {LoadingOutlined} from "@ant-design/icons";
import {Spin} from "antd";
import React from "react";

export const Loader = () => {
    return (
        <><Spin style={{color: "var(--background-color)"}} indicator={<LoadingOutlined spin />} size="large" /></>
    );
};