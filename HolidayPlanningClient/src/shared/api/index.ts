import axios from "axios";
import {data} from "react-router-dom";

const apiUrl = 'https://localhost:7230/api'
const eventControllerUrl = `${apiUrl}/Holiday`
const contractorControllerUrl = `${apiUrl}/Contractor`
const authControllerUrl = `${apiUrl}/Auth`
const memberControllerUrl = `${apiUrl}/Member`


export function getEnumMapping<T extends object>(enumObj: T, value: keyof T | T[keyof T]): string | number | undefined {
    if (typeof value === "string") {
        // Если передана строка, ищем соответствующий числовой ключ
        return enumObj[value as keyof T] as number;
    } else if (typeof value === "number") {
        // Если передано число, ищем соответствующее строковое значение
        return Object.keys(enumObj).find(key => enumObj[key as keyof T] === value);
    }
    return undefined;
}

export interface EventDataResponse {
    id: string,
    title: string,
    startDate: string,
    endDate: string,
    budget: number
}

export interface EventData {
    id: string,
    title: string,
    startDate: Date,
    endDate: Date,
    budget: number
}

export const getAllEvents = async () => {
    return await axios.get(`${eventControllerUrl}/UserId/${localStorage.getItem('userId')}`, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => {
            const data = response.data as EventDataResponse[]
            const mapData: EventData[] = data.map(event => ({
                ...event,
                startDate: new Date(event.startDate),
                endDate: new Date(event.endDate)
            }));
            return mapData
        }
    ).catch(
        error => {
            console.error(`Ошибка при получении списка мероприятий: ${error}`)
            return undefined
        }
    )
}

export interface CreateEventData {
    id: string
    title: string,
    startDate: Date,
    endDate: Date,
    budget: number
}

export const createEvent = async (body: CreateEventData) => {
    return await axios.post(eventControllerUrl, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => {
            const data = response.data as EventDataResponse
            return {
                ...data,
                startDate: new Date(data.startDate),
                endDate: new Date(data.endDate)
            }
        }
    ).catch(
        error => {
            console.error(`Ошибка при создании мероприятия: ${error}`)
            return undefined
        }
    )
}

export const deleteEvent = async (eventId: string) => {
    return await axios.delete(`${eventControllerUrl}/${eventId}`, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при удалении мероприятия: ${error}`)
            return undefined
        }
    )
}

export const changeEvent = async (eventId: string, body: EventData) => {
    return await axios.put(`${eventControllerUrl}/${eventId}`, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при изменении мероприятия: ${error}`)
            return undefined
        }
    )
}

export enum ContractorCategory {
    "Одежда&Аксессуары" = 1,
    "Красота&Здоровье" = 2,
    "Музыка&Шоу"= 3,
    "Цветы&Декор" = 4,
    "Фото&Видео" = 5,
    "Банкет" = 6,
    "Ведущие" = 7,
    "Транспорт" = 8,
    "Жилье" = 9
}

export enum ContractorStatus {
    "в ожидании" = 1,
    "подтвержден" = 2,
    "отклонен" = 3
}

export enum MemberCategory {
    "семья" = 1,
    "друзья" = 2,
    "коллеги" = 3,
    "родственники" = 4,
    "группа не указана" = 5
}

export enum MenuCategory {
    "взрослое меню" = 1,
    "детское меню" = 2,
    "вегетарианское меню" = 3,
    "диетическое меню" = 4,
    "меню не указано" = 5
}

export const contractorCategories = Object.values(ContractorCategory).filter(
    value => typeof value === "string"
)

export const contractorStatus = Object.values(ContractorStatus).filter(
    value => typeof value === "string"
)

export const memberCategories = Object.values(MemberCategory).filter(
    value => typeof value === "string"
)

export const menuCategories = Object.values(MenuCategory).filter(
    value => typeof value === "string"
)

export interface ContractorsData{
    id: string
    name: string,
    description: string,
    category: string,
    phoneNumber: string,
    email: string,
    serviceCost: number,
    status: string
}

export const getEventContractors = async (eventId: string) => {
    return await axios.get(`${contractorControllerUrl}/HolidayId/${eventId}`, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => {
            const data = response.data
            const mapData: ContractorsData[] = data.map((contractor: ContractorDataResponse) => ({
                id: contractor.id,
                name: contractor.title,
                description: contractor.description,
                phoneNumber: contractor.phoneNumber,
                email: contractor.email,
                serviceCost: contractor.serviceCost,
                category: getEnumMapping(ContractorCategory, Number(contractor.сategoryId)),
                status: getEnumMapping(ContractorStatus, Number(contractor.statusId))
            }));
            return mapData
        }
    ).catch(
        error => {
            console.error(`Ошибка при получении списка подрядчиков: ${error}`)
            return undefined
        }
    )
}

export interface ContractorDataRequest {
    id: string
    holidayId: string,
    statusId: string,
    сategoryId: string,
    title: string,
    description: string,
    phoneNumber: string,
    email: string,
    serviceCost: number
}

export interface ContractorDataResponse {
    id: string
    holidayId: string,
    statusId: string,
    сategoryId: string,
    title: string,
    description: string,
    phoneNumber: string,
    email: string,
    serviceCost: number
}

export const createContractor = async (body: ContractorDataRequest): Promise<ContractorsData | undefined> => {
    return await axios.post(contractorControllerUrl, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => {
            const data = response.data as ContractorDataResponse
            return {
                id: data.id,
                name: data.title,
                description: data.description,
                phoneNumber: data.phoneNumber,
                email: data.email,
                serviceCost: data.serviceCost,
                category: getEnumMapping(ContractorCategory, Number(data.сategoryId)),
                status: getEnumMapping(ContractorStatus, Number(data.statusId))
            } as ContractorsData
        }
    ).catch(
        error => {
            console.error(`Ошибка при создании подрядчика: ${error}`)
            return undefined
        }
    )
}

export const deleteContractor = async (contractorId: string) => {
    return await axios.delete(`${contractorControllerUrl}/${contractorId}`, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при удалении подрядчика: ${error}`)
            return undefined
        }
    )
}

export const changeContractor = async (contractorId: string, body: ContractorDataResponse) => {
    return await axios.put(`${contractorControllerUrl}/${contractorId}`, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при изменении подрядчика: ${error}`)
            return undefined
        }
    )
}

export const changeContractorStatus = async (contractorId: string, newStatusId: number)=> {
    return await axios.patch(`${contractorControllerUrl}/${contractorId}`, {
        contractorId,
        contractorStatusId: `${newStatusId}`
    }, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при изменении статуса подрядчика: ${error}`)
            return undefined
        }
    )
}

export interface LoginData {
    login: string,
    password: string
}

export interface AuthResponse{
    userID: string,
    login: string
}

export const auth = async (data: LoginData)=> {
    return await axios.post(`${authControllerUrl}/login`, data, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(data => {
        return data.data as AuthResponse
    }).catch(error => {
        console.error(`Ошибка при авторизации: ${error}`)
        return undefined
    })
}

export interface RegistrationData {
    login: string,
    password: string,
    repeatPassword: string
}

export const registration = async (data: RegistrationData)=> {
    if(data.password === data.repeatPassword){
        return await axios.post(`${authControllerUrl}/register`, data, {
            headers: {
                "Content-Type": "application/json",
                Accept: 'application/json'
            }
        }).then(data => {
            return data.status === 200
        }).catch(error => {
            console.error(`Ошибка при авторизации: ${error}`)
            return undefined
        })
    }

    return false
}

export interface MemberResponseData {
    id: string,
    holidayId: string,
    memberCategoryId: string,
    memberStatusId: string,
    menuCategoryId: string,
    fio: string,
    phoneNumber: string,
    email: string,
    comment: string,
    isChild: boolean,
    isMale: boolean,
    seat: string
}

export interface MemberData {
    id: string,
    holidayId: string,
    memberCategory: string,
    memberStatus: string,
    menuCategory: string,
    fio: string,
    phoneNumber: string,
    email: string,
    comment: string,
    isChild: boolean,
    isMale: boolean,
    seat: string
}

export const getMembersByEventId = async (eventId: string) => {
    return await axios.get(`${memberControllerUrl}/HolidayId/${eventId}`).then(
        response => {
            const data = response.data
            const mapData: MemberData[] = data.map((member: MemberResponseData) => ({
                id: member.id,
                holidayId: member.holidayId,
                memberCategory: getEnumMapping(MemberCategory, Number(member.memberCategoryId)),
                memberStatus: getEnumMapping(ContractorStatus, Number(member.memberStatusId)),
                menuCategory: getEnumMapping(MenuCategory, Number(member.menuCategoryId)),
                fio: member.fio,
                phoneNumber: member.phoneNumber,
                email: member.email,
                comment: member.comment,
                isChild: member.isChild,
                isMale: member.isMale,
                seat: member.seat
            }));
            return mapData
        }
    )
}

export const changeMemberStatus = async (memberId: string, newStatusId: number)=> {
    return await axios.patch(`${memberControllerUrl}/${memberId}`, {
        memberId,
        memberStatusId: `${newStatusId}`
    }, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при изменении статуса гостя: ${error}`)
            return undefined
        }
    )
}

export const deleteMember = async (memberId: string) => {
    return await axios.delete(`${memberControllerUrl}/${memberId}`, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при удалении гостя: ${error}`)
            return undefined
        }
    )
}

export const createMember = async (body: MemberResponseData) => {
    return await axios.post(memberControllerUrl, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при создании гостя: ${error}`)
            return undefined
        }
    )
}

export const changeMember = async (memberId: string, body: MemberResponseData) => {
    return await axios.put(`${memberControllerUrl}/${memberId}`, body, {
        headers: {
            "Content-Type": "application/json",
            Accept: 'application/json'
        }
    }).then(
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при изменении гостя: ${error}`)
            return undefined
        }
    )
}