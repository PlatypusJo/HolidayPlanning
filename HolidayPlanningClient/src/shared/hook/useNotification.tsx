import {notification as antdNotification} from "antd";
import React, {createContext, useContext} from "react";

type UseNotificationReturn = [React.ReactElement<any, string | React.JSXElementConstructor<any>>, {
    success: (description: string) => void;
    warning: (description: string) => void;
    error: (description: string) => void;
    info: (description: string) => void
}];

type NotificationType = 'success' | 'info' | 'warning' | 'error';

interface NotificationContextType {
    notification: {
        success: (description: string) => void,
        info: (description: string) => void,
        warning: (description: string) => void,
        error: (description: string) => void
    };
}

const NotificationContext = createContext<NotificationContextType | null>(null);

export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [api, contextHolder] = antdNotification.useNotification({
        stack: { threshold: 5 }
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
        success: (description: string) => {
            openNotification('success', description)
        },
        info: (description: string) => {
            openNotification('info', description)
        },
        warning: (description: string) => {
            openNotification('warning', description)
        },
        error: (description: string) => {
            openNotification('error', description)
        },
    }

    return (
        <NotificationContext.Provider value={{ notification }}>
            {contextHolder}
            {children}
        </NotificationContext.Provider>
    )
}

export const useNotification = () => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error("useNotificationContext должен использоваться в NotificationProvider!");
    }
    return context.notification;
};