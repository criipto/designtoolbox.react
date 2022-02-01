namespace Criipto.React

open Feliz
open Feliz.Bulma

[<AutoOpen>]
module Components = 
      
      let Navbar (options : {| 
            LogOutText : string
            LogInText : string
            AppName : string
            Manager : {| 
                         ViewManager : IViewManager<'view>
                         UserManager : IUserManager<'user>
                         ErrorManager : IErrorManager<'err>
                      |}
            |}) = 
            Navbar.Navbar(
                  { 
                      LogOutText = options.LogOutText
                      LogInText = options.LogInText
                      AppName = options.AppName
                      Manager = 
                          { new IManager<_,_,_> with
                              member __.ViewManager with get() = options.Manager.ViewManager
                              member __.UserManager with get() = options.Manager.UserManager
                              member __.ErrorManager with get() = options.Manager.ErrorManager
                          }
                  })
      let SidePanelMenu(options) = SidePanelMenu.SidePanelMenu(options)
      let Table(options) = Table.Table(options)
      let Layout(options) = Layout.Layout(options)
      let ViewPicker = ViewPicker.ViewPicker
      let InlineEditor(options) = InlineEditor.InlineEditor options
      let FileUpload(options) = FileUpload.FileUpload options
      let VerticalWizard(options) = VerticalWizard.VerticalWizard options
    
