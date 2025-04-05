import React, {useEffect, useState} from "react";
import cl from "./ui/EventMembersPage.module.css"
import ImageContainerGuests from "../../shared/image/image container-guests.png"
import {
    ContractorsData,
    getMembersByEventId, memberCategories,
    MemberData, menuCategories
} from "../../shared/api";
import {useFetching, useNotification} from "../../shared/hook";
import {InfoContainer, NoData, RightFloatButton} from "../../shared/ui";
import {PlusOutlined} from "@ant-design/icons";
import {useFooterContext} from "../../shared/ui/Footer/Footer";
import {MemberContainer} from "../../widgets";
import {useParams} from "react-router-dom";
import {Checkbox} from "antd";
import {MemberCreateModal} from "../../modal/MemberCreateModal.tsx";
import {MemberChangeModal} from "../../modal/MemberChangeModal.tsx";

export const EventMembersPage = () => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
    const [members, setMembers] = useState<MemberData[]>([]);
    const [selectedMemberCategories, setSelectedMemberCategories] = useState<string[]>([]);
    const [selectedMenuCategories, setSelectedMenuCategories] = useState<string[]>([]);
    const [isCreateMemberModal, setIsCreateMemberModal] = useState(false);
    const { updateFloatButton } = useFooterContext()
    const [fetchGetMembers, isLoadingFetchGetMembers, errorFetchGetMembers] = useFetching(async () => {
        try {
            const response = await getMembersByEventId(eventId)
            response && setMembers(response)
        } catch (e) {
            notification.error(`Ошибка при получении гостей мероприятия: ${errorFetchGetMembers}`)
        }
    })

    useEffect(() => {
        fetchGetMembers()
        updateFloatButton(<RightFloatButton
            tooltipTitle={"Добавить гостя"}
            buttonIcon={<PlusOutlined/>}
            onClick={() => {openCreateMemberModal()}}
        />)
    }, [])

    const openCreateMemberModal = () => {
        setIsCreateMemberModal(true);
    };


    const handleCancelCreateMemberModal = () => {
        setIsCreateMemberModal(false);
    };

    const handleMemberCategoryChange = (category: string) => {
        setSelectedMemberCategories(prev =>
            prev.includes(category)
                ? prev.filter(c => c !== category)
                : [...prev, category]
        );
    };

    const handleMenuCategoryChange = (category: string) => {
        setSelectedMenuCategories(prev =>
            prev.includes(category)
                ? prev.filter(c => c !== category)
                : [...prev, category]
        );
    };

    const filteredMembers = members.filter(member => {
        const matchesMember = selectedMemberCategories.length === 0
            || selectedMemberCategories.includes(member.memberCategory);
        const matchesMenu = selectedMenuCategories.length === 0
            || selectedMenuCategories.includes(member.menuCategory);
        return matchesMember && matchesMenu;
    });

    const onCreateMember = (newMember: MemberData) => {
        console.log(newMember)
        setMembers([...members, newMember])
    }

    const onDeleteMember = (memberId: string) => {
        const updatedMembers = members.filter(member => member.id !== memberId);
        setMembers(updatedMembers);
    }

    const onChangeMember = (memberId: string, newMember: MemberData) => {
        const index = members.findIndex(member => member.id === memberId);

        if (index !== -1) {
            const updatedMembers = [
                ...members.slice(0, index),
                newMember,
                ...members.slice(index + 1)
            ];

            // Обновляем состояние
            setMembers(updatedMembers);
        } else {
            console.warn(`Гость с id ${memberId} не найден`);
        }
    }

    return (
        <>
            <InfoContainer title={"Здесь располагаются гости вашего мероприятия"} src={ImageContainerGuests}
                           onBtnClick={() => window.history.back()}/>
            <div className={cl.memberContainer}>
                <div className={cl.separatorUnderline}/>
                <div className={cl.container}>
                    <div className={cl.filterContainer}>
                        <div className={cl.category}>Группы:</div>
                        <div className={cl.checkBoxContainer}>
                            {
                                memberCategories.map(category =>
                                    <Checkbox
                                        key={category}
                                        style={{ fontSize: "1.8vh" }}
                                        checked={selectedMemberCategories.includes(category as string)}
                                        onChange={() => handleMemberCategoryChange(category as string)}
                                    >
                                        {category}
                                    </Checkbox>
                                )
                            }
                        </div>
                        <div className={cl.category}>Тип меню:</div>
                        <div className={cl.checkBoxContainer}>
                            {menuCategories.map(category => // Убедитесь, что menuCategories импортирован
                                <Checkbox
                                    key={category}
                                    style={{ fontSize: "1.8vh" }}
                                    checked={selectedMenuCategories.includes(category as string)}
                                    onChange={() => handleMenuCategoryChange(category as string)}
                                >
                                    {category}
                                </Checkbox>
                            )}
                        </div>
                    </div>
                    <div>
                        {
                            filteredMembers.length > 0
                                ? filteredMembers.map((member, index) =>
                                    <MemberContainer
                                        member={member}
                                        key={index}
                                        onDeleteMember={onDeleteMember}
                                        onChangeMember={onChangeMember}
                                    />
                                )
                                :
                                <NoData title={"Гостей не найдено"} text={"Нажмите +, чтобы добавить гостя"}/>
                        }
                    </div>
                </div>
            </div>
            <MemberCreateModal eventId={eventId} visible={isCreateMemberModal} onCreateMember={onCreateMember} onCancel={handleCancelCreateMemberModal}/>
        </>
    )
}