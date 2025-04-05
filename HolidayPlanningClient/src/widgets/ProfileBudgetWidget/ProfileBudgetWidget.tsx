import React, {useEffect, useState} from "react";
import {WalletOutlined} from "@ant-design/icons";
import cl from './ui/ProfileBudgetWidget.module.css'
import {useFetching} from "../../shared/hook";
import {BudgetMetrics, getAllEvents, getBudgetMetricsByEventId} from "../../shared/api";

export const ProfileBudgetWidget: React.FC<{eventId: string}> = ({ eventId }) => {
    const [budgetMetrics, setBudgetMetrics] = useState({
        generalBudget: 0,
        paid: 0,
        waitingPayment: 0,
        restBudget: 0
    } as BudgetMetrics)

    const [fetchGetBudgetMetrics, isLoadingFetchBudgetMetrics, errorFetchBudgetMetrics] = useFetching(async () => {
        try {
            const response = await getBudgetMetricsByEventId(eventId)

            if(response){
                setBudgetMetrics(response)
            }
        } catch (e) {
            console.error(`Ошибка при получении метрик бюджета: ${errorFetchBudgetMetrics}`)
        }
    })

    useEffect(() => {
        fetchGetBudgetMetrics()
    }, []);

    return (
        <>
            <div className={cl.resumeBudget}>
                <div className={cl.resumeBudgetHeader}>
                    <WalletOutlined className={cl.resumeEventBudgetButton}/>
                    <div className={cl.resumeBudgetHeaderText}>Бюджет</div>
                </div>
                <div className={cl.resumeUnderlineHeader}></div>
                <div className={cl.tooltipBudget}>
                    <div className={cl.budgetRow}>
                        <div className={cl.budgetItem}>Бюджет</div>
                        <div className={cl.budgetValue}>
                            <div className={`${cl.tooltipBudgetIconBox} ${cl.tooltipBudgetIcon1}`}/>
                            {budgetMetrics.generalBudget} рублей
                        </div>
                    </div>
                    <div className={cl.budgetRow}>
                        <div className={cl.budgetItem}>Оплачено</div>
                        <div className={cl.budgetValue}>
                            <div className={`${cl.tooltipBudgetIconBox} ${cl.tooltipBudgetIcon2}`}/>
                            {budgetMetrics.paid} рублей
                        </div>
                    </div>
                    <div className={cl.budgetRow}>
                        <div className={cl.budgetItem}>В ожидании</div>
                        <div className={cl.budgetValue}>
                            <div className={`${cl.tooltipBudgetIconBox} ${cl.tooltipBudgetIcon3}`}/>
                            {budgetMetrics.waitingPayment} рублей
                        </div>
                    </div>
                </div>
                <div className={cl.resumeUnderlineHeader}></div>
                <div className={cl.tooltipBudget}>
                    <div className={cl.budgetRow}>
                        <div className={cl.budgetItemOst}>Остаток</div>
                        <div className={cl.budgetValueOst}>
                            <div className={`${cl.tooltipBudgetIconBox} ${cl.tooltipBudgetIcon4}`}/>
                            {budgetMetrics.restBudget} рублей
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};