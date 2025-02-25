

export interface EventData {
    id: number,
    name: string,
    type: string,
    startDate: Date,
    endDate: Date,
}

export const getAllEvents = async () => {
    const mockEventsData: EventData[] = [
        {
            id: 1,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 2,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 3,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 4,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 5,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 6,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 7,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 8,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 9,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        },{
            id: 10,
            name: "Новый год",
            type: "Ваше мерприятие",
            startDate: new Date(),
            endDate: new Date(Date.now() + 28800 * 1000)
        }
    ]
    return mockEventsData
}