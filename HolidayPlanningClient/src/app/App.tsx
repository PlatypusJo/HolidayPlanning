import {Layout} from 'antd';
import React, {createContext, useContext, useState} from 'react';
import {BrowserRouter} from 'react-router-dom';
import './ui/App.css';
import {AppRoutes} from "./AppRoutes";
import {Header} from "./Header";
import {Footer, FooterProvider} from "../shared/ui/Footer/Footer";
import {NotificationProvider, useNotification} from "../shared/hook";

const { Content } = Layout

const App = () => {
    return (
        <BrowserRouter>
            <Layout className="layout">
                <FooterProvider>
                    <NotificationProvider>
                        <Header/>
                        <Content className="content">
                            <AppRoutes/>
                        </Content>
                        <Footer/>
                    </NotificationProvider>
                </FooterProvider>
            </Layout>
        </BrowserRouter>
    );
}

export default App;




