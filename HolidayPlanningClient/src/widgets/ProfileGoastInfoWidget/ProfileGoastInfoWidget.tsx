import React, {useEffect, useState} from "react";
import cl from './ui/ProfileGoastInfoWidget.module.css'
import {SolutionOutlined, TeamOutlined} from "@ant-design/icons";
import {ConfigProvider, Progress, Tooltip} from "antd";
import {theme} from "./theme/theme";
import {useFetching} from "../../shared/hook";
import {deleteMember, getMembersContractorsByEventId, getMembersMetricsByEventId, Metrics} from "../../shared/api";

export const ProfileGoastInfoWidget: React.FC<{eventId: string}> = ({ eventId }) => {
    const defaultMetrics: Metrics = {
        anticipation: {
            count: 0,
            percent: 0
        },
        confirmed: {
            count: 0,
            percent: 0
        },
        rejected: {
            count: 0,
            percent: 0
        }
    }
    const [memberMetrics, setMemberMetrics] = useState(defaultMetrics)
    const [contractorMetrics, setContractorMetrics] = useState(defaultMetrics)

    const [fetchGetMemberMetrics, isLoadingFetchGetMemberMetrics, errorFetchGetMemberMetrics] = useFetching(async () => {
        try {
            const response = await getMembersMetricsByEventId(eventId)
            if (response) {
                setMemberMetrics(response)
            }
        } catch (e) {
            console.error(`Ошибка при получении метрик гостей: ${errorFetchGetMemberMetrics}`)
        }
    })
    const [fetchGetContractorMetrics, isLoadingFetchGetContractorMetrics, errorFetchGetContractorMetrics] = useFetching(async () => {
        try {
            const response = await getMembersContractorsByEventId(eventId)
            if (response) {
                setContractorMetrics(response)
            }
        } catch (e) {
            console.error(`Ошибка при получении метрик подрядчиков: ${errorFetchGetContractorMetrics}`)
        }
    })

    useEffect(() => {
        fetchGetMemberMetrics()
        fetchGetContractorMetrics()
    }, []);

    return (
        <ConfigProvider theme={theme}>
            <div className={cl.resumeTwoBlock}>
                <div className={cl.resumeTwoGoast}>
                    <div className={cl.resumeGoastHeader}>
                        <TeamOutlined className={cl.resumeGostHeaderButton}/>
                        <div className={cl.resumeGostHeaderText}>Гости</div>
                    </div>
                    <div className={cl.resumeGostUnderlineHeader}></div>
                    <Tooltip title={
                        <>
                            <div>{memberMetrics.confirmed.count} подтверждено ({memberMetrics.confirmed.percent}%)</div>
                            <div>{memberMetrics.anticipation.count} в ожидании ({memberMetrics.anticipation.percent}%)</div>
                            <div>{memberMetrics.rejected.count} отклонено ({memberMetrics.rejected.percent}%)</div>
                        </>
                    }>
                        <div style={{ position: 'relative', marginTop: '2%', marginBottom: '2%'}}>
                            <Progress
                                className={cl.progressGost}
                                type="circle"
                                strokeLinecap="butt"
                                size={['100%', 300]}
                                strokeWidth={20}
                                showInfo={false}
                                success={{percent: memberMetrics.confirmed.percent, strokeColor: '#F77567'}}
                                percent={memberMetrics.anticipation.percent + memberMetrics.confirmed.percent}
                                strokeColor={'#FEC04C'}
                            />
                        </div>
                    </Tooltip>
                    <div className={cl.tooltipGost}>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon1}`}> </div>
                        <div className={cl.textTooltipGost}>{memberMetrics.confirmed.count} подтверждено</div>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon2}`}> </div>
                        <div className={cl.textTooltipGost}>{memberMetrics.anticipation.count} в ожидании</div>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon3}`}> </div>
                        <div className={cl.textTooltipGost}>{memberMetrics.rejected.count} отклонено</div>
                    </div>
                </div>
                <div className={cl.resumeTwoGoast}>
                    <div className={cl.resumeGoastHeader}>
                        <SolutionOutlined className={cl.resumeGostHeaderButton}/>
                        <div className={cl.resumeGostHeaderText}>Подрядчики</div>
                    </div>
                    <div className={cl.resumeGostUnderlineHeader}></div>
                    <Tooltip title={
                        <>
                            <div>{contractorMetrics.confirmed.count} забронировано ({contractorMetrics.confirmed.percent}%)</div>
                            <div>{contractorMetrics.anticipation.count} в ожидании ({contractorMetrics.anticipation.percent}%)</div>
                            <div>{contractorMetrics.rejected.count} отклонено ({contractorMetrics.rejected.percent}%)</div>
                        </>
                    }>
                        <div style={{ position: 'relative', marginTop: '2%', marginBottom: '2%'}}>
                            <Progress
                                className={cl.progressGost}
                                type="circle"
                                strokeLinecap="butt"
                                size={['100%', 300]}
                                strokeWidth={20}
                                showInfo={false}
                                success={{percent: contractorMetrics.confirmed.percent, strokeColor: '#F77567'}}
                                percent={contractorMetrics.anticipation.percent + contractorMetrics.confirmed.percent}
                                strokeColor={'#FEC04C'}
                            />
                        </div>
                    </Tooltip>
                    <div className={cl.tooltipGost}>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon1}`}> </div>
                        <div className={cl.textTooltipGost}>{contractorMetrics.confirmed.count} забронировано</div>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon2}`}> </div>
                        <div className={cl.textTooltipGost}>{contractorMetrics.anticipation.count} в ожидании</div>
                        <div className={`${cl.tooltipGostIconBox} ${cl.tooltipGostIcon3}`}> </div>
                        <div className={cl.textTooltipGost}>{contractorMetrics.rejected.count} отклонено</div>
                    </div>
                </div>
            </div>
        </ConfigProvider>
    );
};