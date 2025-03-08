export const getCountdown = (eventDate?: Date) => {
    if(!eventDate){
        return "Событие уже началось";
    }

    const now = new Date().getTime();
    const diff = eventDate.getTime() - now;

    if (diff <= 0) {
        return "Событие уже началось";
    }

    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
    const hours = Math.floor((diff / (1000 * 60 * 60)) % 24);
    const minutes = Math.floor((diff / (1000 * 60)) % 60);
    const seconds = Math.floor((diff / 1000) % 60);

    return `${days} дн. ${hours.toString().padStart(2, "0")} ч. ${minutes
        .toString().padStart(2, "0")} мин. ${seconds.toString().padStart(2, "0")} с.`;
};