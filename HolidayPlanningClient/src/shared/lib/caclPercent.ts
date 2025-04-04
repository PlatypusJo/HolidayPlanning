export const calcPercent = (value: number, total: number) => {
    return total > 0
        ? Math.min((value / total) * 100, 100)
        : 0
}