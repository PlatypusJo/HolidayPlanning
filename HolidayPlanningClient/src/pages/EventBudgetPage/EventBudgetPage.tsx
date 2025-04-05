import React, {useEffect, useState} from "react";
import cl from "./ui/EventBudgetPage.module.css"
import ImageContainerContractors from "../../shared/image/budget.png"
import {BudgetData, getBudgetByEventId} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {useFooterContext} from "../../shared/ui/Footer/Footer";
import {useParams} from "react-router-dom";
import {BudgetContainer} from "../../widgets";

export const EventBudgetPage = () => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
    const [budgets, setBudgets] = useState<BudgetData[]>([]);
    const { updateFloatButton } = useFooterContext()
    const [isCreateBudgetModal, setIsCreateBudgetModal] = useState(false);
    const [fetchGetBudgets, isLoadingFetchGetBudgets, errorFetchGetBudgets] = useFetching(async () => {
        try {
            const response = await getBudgetByEventId(eventId)
            response && setBudgets(response)
        } catch (e) {
            notification.error(`Ошибка при получении расходов мероприятия: ${errorFetchGetBudgets}`)
        }
    })

    useEffect(() => {
        fetchGetBudgets()
        updateFloatButton(<RightFloatButton
            tooltipTitle={"Добавить статью"}
            buttonIcon={<PlusOutlined/>}
            onClick={
                () => {notification.info(`Создание статьи расхода в разработке!`)}
                // openCreateBudgetModal
            }
        />)
    }, [])

    const onCreateBudget = (newBudget: BudgetData) => {
        setBudgets([...budgets, newBudget])
    }

    const openCreateBudgetModal = () => {
        setIsCreateBudgetModal(true);
    };


    const handleCancelBudgetModal = () => {
        setIsCreateBudgetModal(false);
    };

    const onDeleteBudget = (budgetId: string) => {
        const updatedBudgets = budgets.filter(budget => budget.id !== budgetId);
        setBudgets(updatedBudgets);
    }

    const onChangeBudget = (budgetId: string, newBudget: BudgetData) => {
        const index = budgets.findIndex(budget => budget.id === budgetId);

        if (index !== -1) {
            const updatedBudgets = [
                ...budgets.slice(0, index),
                newBudget,
                ...budgets.slice(index + 1)
            ];

            // Обновляем состояние
            setBudgets(updatedBudgets);
        } else {
            console.warn(`Статья с id ${budgetId} не найдена`);
        }
    }


    return (
        <>
            <InfoContainer title={"Здесь располагается бюджет вашего мероприятия"} src={ImageContainerContractors}
                           onBtnClick={() => window.history.back()}/>
            <div className={cl.budgetsContainer}>
                <div className={cl.separatorUnderline}/>
                {
                    budgets.length > 0
                        ? budgets.map((budget, index) =>
                            <BudgetContainer
                                budget={budget}
                                key={index}
                                onDeleteBudget={onDeleteBudget}
                                onChangeBudget={onChangeBudget}
                            />
                        )
                        :
                        <NoData title={"Статей расходов не найдено"} text={"Нажмите +, чтобы добавить статью"}/>
                }
            </div>
        </>
    )
}