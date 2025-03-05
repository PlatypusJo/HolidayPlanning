import axios from "axios";

const apiUrl = 'https://localhost:7230/api'
const eventControllerUrl = `${apiUrl}/Holiday`
const contractorControllerUrl = `${apiUrl}/Contractor`


export function getEnumMapping<T extends object>(enumObj: T, value: keyof T | T[keyof T]): string | undefined {
    if (typeof value === "string") {
        // Если передана строка, ищем соответствующий числовой ключ
        // @ts-ignore
        return enumObj[value as keyof T];
    } else if (typeof value === "number") {
        // Если передано число, ищем соответствующее строковое значение
        return Object.keys(enumObj).find(key => enumObj[key as keyof T] === value);
    }
    return undefined;
}

export interface EventDataResponse {
    id: number,
    title: string,
    startDate: string,
    endDate: string,
    budget: number
}

export interface EventData {
    id: number,
    title: string,
    startDate: Date,
    endDate: Date,
    budget: number
}

export const getAllEvents = async () => {
    return await axios.get(eventControllerUrl, {
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

export const deleteEvent = async (eventId: number) => {
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

export const changeEvent = async (eventId: number, body: EventData) => {
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

export const contractorCategories = Object.values(ContractorCategory).filter(
    value => typeof value === "string"
)

export const contractorStatus = Object.values(ContractorStatus).filter(
    value => typeof value === "string"
)

export interface ContractorsData{
    id: number
    name: string,
    description: string,
    category: string,
    phoneNumber: string,
    email: string,
    serviceCost: number,
    status: string
}

export const getEventContractors = async (eventId: number) => {
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
                category: getEnumMapping(ContractorCategory, contractor.сategoryId),
                status: getEnumMapping(ContractorStatus, contractor.statusId)
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
    holidayId: number,
    statusId: number,
    сategoryId: number,
    title: string,
    description: string,
    phoneNumber: string,
    email: string,
    serviceCost: number
}

export interface ContractorDataResponse {
    id: number
    holidayId: number,
    statusId: number,
    сategoryId: number,
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
                category: getEnumMapping(ContractorCategory, data.сategoryId),
                status: getEnumMapping(ContractorStatus, data.statusId)
            } as ContractorsData
        }
    ).catch(
        error => {
            console.error(`Ошибка при создании мероприятия: ${error}`)
            return undefined
        }
    )
}

