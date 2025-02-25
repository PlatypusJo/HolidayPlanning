export const formatTimeDifference = (startDate: Date, endDate: Date) => {
    const diffInMs = endDate.getTime() - startDate.getTime();

    const diffInSeconds = Math.floor(diffInMs / 1000);
    const diffInMinutes = Math.floor(diffInSeconds / 60);
    const diffInHours = Math.floor(diffInMinutes / 60);
    const diffInDays = Math.floor(diffInHours / 24);

    const remainingMinutes = diffInMinutes % 60;
    const remainingHours = diffInHours % 24;

    if (diffInMinutes <= 59) {
        return `${diffInMinutes} мин.`;
    } else if (diffInMinutes === 60) {
        return `1 ч.`;
    } else if (diffInHours < 24) {
        return `${diffInHours} ч. ${remainingMinutes > 0 ? `${remainingMinutes} мин.` : ''}`.trim();
    } else {
        return `${diffInDays} д. ${remainingHours > 0 ? `${remainingHours} ч.` : ''} ${
            remainingMinutes > 0 ? `${remainingMinutes} мин.` : ''
        }`.trim();
    }
}