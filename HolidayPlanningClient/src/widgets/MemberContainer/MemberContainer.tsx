import cl from "./ui/MemberContainer.module.css";
import {DeleteOutlined, EditOutlined, SettingOutlined,} from "@ant-design/icons";
import React, {useState} from "react";
import {
    changeMemberStatus,
    ContractorStatus,
    contractorStatus, deleteMember, getEnumMapping, MemberData
} from "../../shared/api";
import {Button, Cascader, Dropdown, MenuProps} from "antd";
import {useFetching, useNotification} from "../../shared/hook";
import {useParams} from "react-router-dom";
import {MemberChangeModal} from "../../modal/MemberChangeModal.tsx";

export const MemberContainer: React.FC<{
    member: MemberData,
    onChangeMember: (memberId: string, newMember: MemberData) => void,
    onDeleteMember: (memberId: string) => void,
}> = ({ member, onChangeMember, onDeleteMember }) => {
    const eventId = `${useParams().id}`
    const notification = useNotification()
    const [isChangeMemberModal, setIsChangeMemberModal] = useState(false);
    const [fetchDeleteMember, isLoadingFetchDeleteMember, errorFetchDeleteMember] = useFetching(async () => {
        try {
            const response = await deleteMember(member.id)
            if (response && response.status === 200) {
                onDeleteMember(member.id)
                notification.success(`Гость '${member.fio}' успешно удален!`)
            }
        } catch (e) {
            notification.error(`Ошибка при удалении гостя: ${errorFetchDeleteMember}`)
        }
    })
    const [fetchChangeStatus, isLoadingFetchChangeStatus, errorFetchChangeStatus] = useFetching(async (newStatus: string) => {
        try {
            const response = await changeMemberStatus(member.id, Number(getEnumMapping(ContractorStatus, newStatus as keyof typeof ContractorStatus)))
            if(response && response.status === 200){
                onChangeMember(member.id, {
                    ...member,
                    memberStatus: newStatus
                })
            }
        } catch (e) {
            notification.error(`Ошибка при изменении статуса гостя '${member.fio}': ${errorFetchChangeStatus}`)
        }
    })

    const items: MenuProps['items'] = [
        {
            label: (
                <Button icon={<EditOutlined/>}
                        iconPosition={"start"}
                        onClick={() => {openChangeMemberModal()}}
                        color={"default"}
                        variant={"link"}
                >
                    Изменить гостя
                </Button>
            ),
            key: '0',
        }, {
            label: (
                <Button
                    icon={<DeleteOutlined/>}
                    iconPosition={"start"}
                    loading={isLoadingFetchDeleteMember}
                    onClick={() => {
                        fetchDeleteMember()
                    }}
                    color={"danger"}
                    variant={"link"}
                >
                    Удалить гостя
                </Button>
            ),
            key: '1',
        }
    ];

    const statusOptions = contractorStatus.map(val => ({
        value: val,
        label: val
    }))

    const openChangeMemberModal = () => {
        setIsChangeMemberModal(true);
    };


    const handleChangeCreateMemberModal = () => {
        setIsChangeMemberModal(false);
    };

    const onChangeStatus = (
        _: (string | ContractorStatus)[],
        selectedOptions: { value: string | ContractorStatus; label: string | ContractorStatus }[]
    ) => {
        fetchChangeStatus(selectedOptions.map((o) => o.label).join(', '))
    };

    return (
        <>
            <div key={member.id} className={cl.blockContractorBack}>
                <div className={cl.blockContractorName}>
                    <div>{member.fio}</div>
                    <div className={cl.contractorStatus}>
                        <Cascader
                            options={statusOptions.filter(val => val.label !== member.memberStatus)}
                            onChange={onChangeStatus}
                            dropdownRender={(menu) => (
                                <div style={{ maxHeight: '80px', overflowY: 'hidden' }}>
                                    {menu}
                                </div>
                            )}
                        >
                            <a className={cl.statusLink}>{member.memberStatus}</a>
                        </Cascader>
                    </div>
                    <Dropdown menu={{ items }} trigger={['click', 'contextMenu']}>
                        <SettingOutlined className={cl.settingsContractorIcon}/>
                    </Dropdown>
                </div>
                <div className={cl.description}>
                    <div>{member.isChild ? 'Ребенок' : 'Взрослый'}, {member.isMale ? 'Мужской' : 'Женский'}</div>
                    <div>Комментарий: {member.comment}</div>
                    <div>Категория меню: {member.menuCategory}</div>
                    <div>Стол: {member.seat}</div>
                    <div>Группа: {member.memberCategory}</div>
                </div>
            </div>
            <MemberChangeModal eventId={eventId} visible={isChangeMemberModal} member={member} onChangeMember={onChangeMember} onCancel={handleChangeCreateMemberModal}/>
        </>
    );
};