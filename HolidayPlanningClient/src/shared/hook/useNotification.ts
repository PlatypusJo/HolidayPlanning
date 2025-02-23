import {notification as antdNotification} from "antd";

type UseNotificationReturn = [React.ReactElement<any, string | React.JSXElementConstructor<any>>, {
    success: (description: string) => void;
    warning: (description: string) => void;
    error: (description: string) => void;
    info: (description: string) => void
}];

type NotificationType = 'success' | 'info' | 'warning' | 'error';

export const useNotification = (stackThreshold? : number): UseNotificationReturn => {
    const [api, contextHolder] = antdNotification.useNotification({
        stack: stackThreshold ? { threshold: stackThreshold } : false
    });

    const openNotification = (type: NotificationType, description: string) => {
        api[type]({
            message: type.charAt(0).toUpperCase() + type.slice(1),
            description,
            placement: 'bottomRight',
            showProgress: true,
            pauseOnHover: true
        });
    };

    const notification = {
        success: (description: string) => {openNotification('success', description)},
        info: (description: string) => {openNotification('info', description)},
        warning: (description: string) => {openNotification('warning', description)},
        error: (description: string) => {openNotification('error', description)},
    }

    return [contextHolder, notification];
}