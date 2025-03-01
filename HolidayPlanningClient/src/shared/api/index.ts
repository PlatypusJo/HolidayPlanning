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

export const contractorStatus = [
    "в ожидании",
    "подтвержден",
    "отклонен"
]


// Тип, основанный на массиве
export type ContractorCategory = typeof contractorCategories[number];

export type ContractorStatus = typeof contractorStatus[number];

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
            name: "Loreal Professionnel & Kerastase",
            description: "Салон красоты: прическа и макияж",
            category: "Красота&Здоровье",
            phoneNumber: "89153431798",
            email: "loreal@mail.ru",
            serviceCost: 9000,
            status: "подтвержден"
        },
        {
            id: 2,
            name: "Татьяна Папаева",
            description: "Фотограф с хорошим стажем",
            category: "Фото&Видео",
            phoneNumber: "+79104532312",
            email: "papaeva@gmail.com",
            serviceCost: 10000,
            status: "в ожидании"
        }
    ]
    return mockData
}