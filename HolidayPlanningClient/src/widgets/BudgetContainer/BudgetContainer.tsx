import cl from "./ui/BudgetContainer.module.css";
import {DeleteOutlined, EditOutlined, SettingOutlined,} from "@ant-design/icons";
import React, {useState} from "react";
import {
    BudgetData, changePaidAmount, deleteBudget
} from "../../shared/api";
import {Button, ConfigProvider, Dropdown, MenuProps, Progress, Typography} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {useParams} from "react-router-dom";
import {calcPercent} from "../../shared/lib";
import {theme} from "./theme/theme";

const { Text } = Typography;

export const BudgetContainer: React.FC<{
    budget: BudgetData,
    onChangeBudget: (budgetId: string, newBudget: BudgetData) => void,
    onDeleteBudget: (budgetId: string) => void,
}> = ({ budget, onChangeBudget, onDeleteBudget }) => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
    const [isChangeBudgetModal, setIsChangeBudgetModal] = useState(false);
    const [fetchDeleteBudget, isLoadingFetchDeleteBudget, errorFetchDeleteBudget] = useFetching(async () => {
        try {
            const response = await deleteBudget(budget.id)
            if (response) {
                onDeleteBudget(budget.id)
                notification.success(response)
            }
        } catch (e) {
            notification.error(`Ошибка при удалении статьи бюджета: ${errorFetchDeleteBudget}`)
        }
    })
    const [fetchChangePaidAmount, isLoadingFetchChangePaidAmount, errorFetchChangePaidAmount] = useFetching(async (newPaidAmount: number) => {
        try {
            const response = await changePaidAmount(budget.id, budget.isContractor, newPaidAmount)
            if (response) {
                onChangeBudget(budget.id, {
                    ...budget,
                    paidAmount: newPaidAmount
                })
            }
        } catch (e) {
            notification.error(`Ошибка при изменении суммы оплаты статьи расхода: ${errorFetchChangePaidAmount}`)
        }
    })

    const items: MenuProps['items'] = [
        {
            label: (
                <Button icon={<EditOutlined/>}
                        iconPosition={"start"}
                        onClick={() => {
                            notification.info('Изменение статьи расхода в разработке')
                            //openChangeBudgetModal()
                        }}
                        color={"default"}
                        variant={"link"}
                >
                    Изменить статью расхода
                </Button>
            ),
            key: '0',
        }, {
            label: (
                <Button
                    icon={<DeleteOutlined/>}
                    iconPosition={"start"}
                    loading={isLoadingFetchDeleteBudget}
                    onClick={() => {
                        notification.info(`Удаление статьи расхода в разработке`)
                        //fetchDeleteBudget()
                    }}
                    color={"danger"}
                    variant={"link"}
                >
                    Удалить статью расхода
                </Button>
            ),
            key: '1',
        }
    ];

    const onChangePaidAmount = (value: string) => {
        const newPaiAmount = Number(value)
        if(isNaN(newPaiAmount)){
            notification.error(`Сумма не может модержать ничего кроме цифр`)
        } else {
            fetchChangePaidAmount(newPaiAmount)
        }
    };

    const openChangeBudgetModal = () => {
        setIsChangeBudgetModal(true);
    };


    const handleCloseChangeBudgetModal = () => {
        setIsChangeBudgetModal(false);
    };

    return (
        <ConfigProvider theme={theme}>
            <div key={budget.id} className={cl.blockContractorBack}>
                <div className={cl.blockContractorName}>
                    <div>{budget.title}</div>
                    <Dropdown menu={{ items }} trigger={['click', 'contextMenu']}>
                        <SettingOutlined className={cl.settingsContractorIcon}/>
                    </Dropdown>
                </div>
                <div className={cl.progressContainer}>
                    <Progress
                        className={cl.progressItem}
                        strokeLinecap={"butt"}
                        size={['100%', 20]}
                        percent={calcPercent(budget.paidAmount, budget.totalAmount)}
                        strokeColor={{ '0%': '#FE9449', '100%': '#EF5282' }}
                        showInfo={false}
                    />
                </div>
                <div className={cl.description}>
                    <div>
                        <Text strong>Олачено: </Text>
                        <Text strong editable={{
                            maxLength: 15,
                            text: `${budget.paidAmount}`,
                            onChange: onChangePaidAmount
                        }}>{budget.paidAmount}</Text>
                    </div>
                    <div>
                        <Text strong>Сумма: {budget.totalAmount}</Text>
                    </div>
                </div>
            </div>
        </ConfigProvider>
    );
};