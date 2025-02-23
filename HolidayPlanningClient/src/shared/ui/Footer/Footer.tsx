import React, {createContext, useContext, useEffect, useState} from "react";
import {Layout} from "antd";
import cl from "../../../app/ui/Footer.module.css";
import {FloatScrollTopButton} from "../FloatScrollTopButton/FloatScrollTopButton";

interface FooterContextType {
    showScrollTop: boolean;
    updateShowScrollTop: (value: boolean) => void;
}

const FooterContext = createContext<FooterContextType | null>(null);

export const Footer = () => {
    const { showScrollTop, updateShowScrollTop } = useFooterContext()

    useEffect(() => {
        const handleScroll = () => {
            updateShowScrollTop(window.scrollY > 300);
        };

        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);

    return (
        <Layout.Footer className={cl.footer}>
            Holiday Planning Client ©{new Date().getFullYear()} &#129395;
            {showScrollTop && <FloatScrollTopButton/>}
        </Layout.Footer>
    );
};

export const FooterProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [showScrollTop, setShowScrollTop] = useState(false);

    const updateShowScrollTop = (value: boolean) => {
        console.log(value)
        setShowScrollTop(value)
    }

    return (
        <FooterContext.Provider value={{ showScrollTop, updateShowScrollTop }}>
            {children}
        </FooterContext.Provider>
    )
}

export const useFooterContext = () => {
    const context = useContext(FooterContext);
    if (!context) {
        throw new Error("useFooterContext должен использоваться в FooterProvider!");
    }
    return context;
};