import {Image} from "antd";
import cl from "./NoData.module.css"
import NoDataImage from './no-data.png';
import React from "react";

export const NoData: React.FC<{title: string, text: string}> = ({title,text}) => {
    return (
        <div className={cl.noData}>
            <Image className={cl.imageNoData} preview={false} src={NoDataImage} />
            <div className={cl.titleNoData}>{title}</div>
            <div className={cl.textNoData}>{text}</div>
        </div>
    );
};