import React from 'react';
import ReactDOM from 'react-dom';
import App from './App.jsx';
import './index.css';
import './styles/global.css';
import {Navbar,SidePanelMenu,Table,Layout,ViewPicker,InlineEditor,FileUpload,VerticalWizard} from "@criipto/designtoolbox.react";

function MyReactComponent(props) {
  return (<p>MyReactComponent</p>);
}

class UserManager {
  constructor() {
    this.CurrentUser = 'michel';
  }
  LogOut = () => { console.log("loggin out now!!"); }
  LogIn = () => { console.log("loggin in now!!"); }
  Authenticate = () => { console.log("authenticating..."); }
  HasRequestedAuthentication = () => { true }
}

class Manager {
  constructor() {
    this.UserManager = new UserManager();
  }
}

const MyManager = new Manager();

const layoutOptions = {
  MenuItems: [],
  Navbar: "",
  Element: MyReactComponent
}

console.log(layoutOptions);

ReactDOM.render(
  <Navbar Manager={MyManager} LogOutText='logout now!' LogInText='login now!' AppName='BankAwesome' />,
  document.getElementById('root2')
);

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root'),
);

// Hot Module Replacement (HMR) - Remove this snippet to remove HMR.
// Learn more: https://www.snowpack.dev/concepts/hot-module-replacement
if (import.meta.hot) {
  import.meta.hot.accept();
}
