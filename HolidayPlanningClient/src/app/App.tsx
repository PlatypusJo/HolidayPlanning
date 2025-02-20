import { Layout } from 'antd';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import {AppRoutes} from "./AppRoutes";

function App() {
  return (
      <BrowserRouter>
          <Layout className="layout">
              <Layout.Content className="content">
                  <AppRoutes />
              </Layout.Content>
          </Layout>
      </BrowserRouter>
  );
}

export default App;
