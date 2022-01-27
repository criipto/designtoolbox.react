namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types
open Criipto.React.ViewPicker

module Layout = 
    
    type LayoutOptions<'err,'view,'user when 'view : equality> = {
        MenuItems : MenuItemOptions<'view> list
        Element : ReactElement
        Manager : IManager<'err,'view,'user>
    }
    
    [<ReactComponent>]
    let internal Layout<'err,'view,'user when 'view: equality>(options : LayoutOptions<'err,'view,'user>) =
        let menuItems, setMenuItems = React.useState options.MenuItems
       
        let userManager = options.Manager.UserManager
        match userManager.CurrentUser with
        None -> 
            if userManager.HasRequestedAuthentication() |> not then
                Html.div[
                  Navbar({UserButtonText = "Log on"; Action = userManager.LogIn})
                ]
            else
               userManager.Authenticate() 
               Html.div[]
        | Some user -> 
            Html.div[
                Navbar({UserButtonText = "Log off"; Action = userManager.LogOut})
                Bulma.container [
                    Bulma.columns [
                        prop.style [
                            style.marginTop 40
                        ]
                        columns.isCentered
                        prop.children[
                            Bulma.column [
                                prop.style[
                                    style.boxShadow.none
                                ]
                                column.isOneQuarter
                                column.isOneFifthFullHd
                                prop.children[
                                    SidePanelMenu({
                                        MenuItems = menuItems
                                        Manager = options.Manager 
                                    })
                                ]
                            ]
                            Bulma.column [
                                prop.children [
                                    options.Element
                                ]
                            ]
                        ]
                    ]
                ]
            ]
