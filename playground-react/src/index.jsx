import React from 'react';
import ReactDOM from 'react-dom';
import App from './App.jsx';
import './index.css';
import './styles/global.css';
import {Navbar,SidePanelMenu,Table,Layout,ViewPicker,InlineEditor,FileUpload,VerticalWizard} from "@criipto/designtoolbox.react";


class MyLayoutWrapper extends React.Component {
  constructor(props) {
    super(props);
    const setState = (a) => this.setState(a);
    this.state = {
         "user" : props.user, 
         navbarOptions : {
          LogOutText: 'log out now!',
          LogInText: 'log in now!',
          AppName: 'BankAwesome',
          Manager: {
            UserManager : {
                CurrentUser : props.user || null,
                LogOut : function() { 
                  console.log("logging out now!!"); 
                  setState({"user" : null});
                },
                LogIn : function() { 
                          this.CurrentUser = "Michel"
                          setState(this.CurrentUser);
                          console.log("logging in now!!"); 
                        },
                Authenticate : () => { console.log("authenticating..."); },
                HasRequestedAuthentication : () => { false }
            }
          }
        },
        menuItems : [{Data: 'overview'}]
    }
  }

  render() {
    console.log("options",this.state.navbarOptions);
    return (
      <Layout Navbar={this.state.navbarOptions} MenuItems={this.state.menuItems} />
    );
  }
}

ReactDOM.render(
  <MyLayoutWrapper user="" />,
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
