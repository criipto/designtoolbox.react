namespace Criipto.React

open Feliz
open Feliz.Bulma
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types

module Layout = 
    
    type LayoutOptions<'err,'view,'user> = {
        MenuItems : MenuItemOptions<'view> list
        Navbar : Navbar.NavbarOptions<'err,'view,'user>
        Element : ReactElement
    } with member this.Manager with get() = this.Navbar.Manager
    
    [<ReactComponent>]
    let internal Layout<'err,'view,'user when 'view: equality>(options : LayoutOptions<'err,'view,'user>) =
        let menuItems, setMenuItems = React.useState options.MenuItems
       
        let userManager = options.Manager.UserManager
        match userManager.CurrentUser with
        None -> 
            if userManager.HasRequestedAuthentication() |> not then
                Html.div[
                    Navbar(options.Navbar)
                ]
            else
               userManager.Authenticate() 
               Html.div[]
        | Some user -> 
            Html.div[
                Navbar(options.Navbar)
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
