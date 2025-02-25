import {Layout} from 'antd';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './ui/App.css';
import {AppRoutes} from "./AppRoutes";
import {Header} from "./Header";
import {Footer, FooterProvider} from "../shared/ui/Footer/Footer";

const {Content} = Layout

const App = () => {
  return (
      <BrowserRouter>
          <Layout className="layout">
              <Header/>
              <FooterProvider>
                  <Content className="content">
                      <AppRoutes />
                  </Content>
                  <Footer/>
              </FooterProvider>
          </Layout>
      </BrowserRouter>
  );
}

export default App;



