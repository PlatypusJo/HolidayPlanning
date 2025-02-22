import {useState} from "react";

type UseFetchingReturn<Args extends any[]> = [
    (...args: Args) => Promise<void>,
    boolean,
    string
];

export const useFetching = <Args extends any[]>(
    callback: (...args: Args) => Promise<void>
): UseFetchingReturn<Args> => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string>('');

    const fetching = async (...args: Args) => {
        try {
            setIsLoading(true);
            await callback(...args);
        } catch (e: unknown) {
            if (e instanceof Error) {
                setError(e.message);
            } else {
                setError('Произошла неизвестная ошибка');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return [fetching, isLoading, error];
};