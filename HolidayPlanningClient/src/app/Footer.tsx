import { Layout } from "antd";
import cl from "./ui/Footer.module.css"

export const Footer = () => {
    return (
        <Layout.Footer className={cl.footer}>
            Holiday Planning Client ©{new Date().getFullYear()} &#129395;
        </Layout.Footer>
    );
};