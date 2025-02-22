import {Layout} from 'antd';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './ui/App.css';
import {AppRoutes} from "./AppRoutes";
import {Header} from "./Header";
import {Footer} from "./Footer";

const {Content} = Layout

const App = () => {
  return (
      <BrowserRouter>
          <Layout className="layout">
              <Header/>
              <Content className="content">
                  <AppRoutes />
              </Content>
              <Footer/>
          </Layout>
      </BrowserRouter>
  );
}

export default App;
