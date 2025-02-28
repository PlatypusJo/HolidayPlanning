import axios from "axios";

const apiUrl = 'https://localhost:7230/api'
const eventControllerUrl = `${apiUrl}/Holiday`

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

// Массив всех значений
export const contractorCategories = [
    "Одежда&Аксессуары",
    "Красота&Здоровье",
    "Музыка&Шоу",
    "Цветы&Декор",
    "Фото&Видео",
    "Банкет",
    "Ведущие",
    "Транспорт",
    "Жилье"
] as const;

// Тип, основанный на массиве
export type ContractorCategory = typeof contractorCategories[number];

export type ContractorStatus = "приглашен" | "отменен" | "принят"

export interface ContractorsData{
    id: number
    name: string,
    description: string,
    category: ContractorCategory,
    phoneNumber: string,
    email: string,
    serviceCost: number,
    status: ContractorStatus
}

export const getEventContractors = async (eventId: number) => {
    const mockData: ContractorsData[] = [
        {
            id: 1,
            name: "Подрядчик1",
            description: "Описание1",
            category: "Красота&Здоровье",
            phoneNumber: "",
            email: "",
            serviceCost: 5000,
            status: "приглашен"
        },
        {
            id: 2,
            name: "Подрядчик2",
            description: "Описание2",
            category: "Транспорт",
            phoneNumber: "",
            email: "dgd@mail.ru",
            serviceCost: 10000,
            status: "принят"
        }
    ]
    return mockData
}