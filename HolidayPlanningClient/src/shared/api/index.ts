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
            console.log(data)
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
        response => response
    ).catch(
        error => {
            console.error(`Ошибка при получении списка мероприятий: ${error}`)
            return undefined
        }
    )
}