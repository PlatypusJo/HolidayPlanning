export const formatDate = (date: Date | string) => {
    const _date = typeof date === "string" ? new Date(date) : date
    return _date.toLocaleDateString("ru-RU", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    });
};