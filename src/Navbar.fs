namespace Criipto.React

open Feliz
open Feliz.Bulma

module Navbar =
    type NavbarOptions<'err,'view,'user> = {
      LogOutText : string
      LogInText : string
      AppName : string
      Manager : IManager<'err,'view,'user>
    }
    [<ReactComponent>]
    let internal Navbar<'err,'view,'user>(options : NavbarOptions<'err,'view,'user>) =
        let userButtonText,action = 
            if options.Manager.UserManager.CurrentUser |> Option.isSome then
                options.LogOutText, (fun (_:Browser.Types.MouseEvent) -> options.Manager.UserManager.LogOut())
            else
                options.LogInText, (fun (_:Browser.Types.MouseEvent) -> options.Manager.UserManager.LogIn())
        Bulma.navbarMenu [
            Bulma.navbarStart.div [
                Bulma.navbarItem.div [
                    prop.className "icon credit-card-logo"
                ]
                Bulma.navbarItem.div[
                    prop.className "logo-text"
                    prop.children [
                        Html.span[
                            prop.className "app-name"
                            prop.text options.AppName
                        ]
                    ]
                ]
            ]
            Bulma.navbarEnd.div [
                Bulma.navbarItem.div [
                    Bulma.buttons [
                        Bulma.button.a [  
                            prop.onClick action
                            prop.style [
                                style.backgroundColor.transparent
                                style.borderStyle.none
                                style.fontSize 18
                            ]
                            prop.children [
                                Html.span [ 
                                    prop.className "navbar-item"
                                    prop.text userButtonText
                                ]
                                Html.div [
                                    "power-off-white" |> sprintf "icon %s" |> prop.className
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
