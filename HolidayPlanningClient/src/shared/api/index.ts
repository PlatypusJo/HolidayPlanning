import axios from "axios";

const apiUrl = 'https://localhost:7248/api'
const eventControllerUrl = `${apiUrl}/Holiday`

export interface EventData {
    id: number,
    name: string,
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
        response => response.data as EventData[]
    ).catch(
        error => {
            console.error(`Ошибка при получении списка мероприятий: ${error}`)
            return [] as EventData[]
        }
    )
}

export interface CreateEventData {
    name: string,
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
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при получении списка мероприятий: ${error}`)
            return undefined
        }
    )
}