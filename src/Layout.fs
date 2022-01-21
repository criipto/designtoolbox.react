namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types
open Criipto.React.ViewPicker

module Layout = 
    
    type LayoutOptions<'view,'user when 'view : equality> = {
        MenuItems : MenuItemOptions<'view> list
        View : ReactElement
        Manager : IManager<'view,'user>
    }
    
    [<ReactComponent>]
    let internal Layout<'view,'user when 'view: equality>(options : LayoutOptions<'view,'user>) =
        let menuItems, setMenuItems = React.useState options.MenuItems
        let views = 
            menuItems
            |> List.map(fun m -> m.Data)
        if views |> List.isEmpty then failwith "There must be at least one view"
        let view,_setView = 
            let v = 
                match 
                    menuItems
                    |> List.tryFind(fun mi -> mi.IsActive)
                    |> Option.map(fun mi -> mi.Data) with
                Some view -> view
                | None -> (views |> List.head)
            React.useState v

        let setView view = 
            if view |> Option.isSome then view.Value |> _setView
            menuItems
            |> List.map(fun mi ->
                {
                    mi with 
                       IsActive = (view |> Option.isSome) && mi.Data = view.Value
                }
            ) |> setMenuItems
        
        let userManager = options.Manager.UserManager
        match userManager.CurrentUser with
        None -> 
            if userManager.HasRequestedAuthentication() |> not then
                Html.div[
                    Navbar("Log on",fun _ -> userManager.LogIn())
                ]
            else
               userManager.Authenticate() 
               Html.div[]
        | Some user -> 
            Html.div[
                Navbar("Log off",userManager.LogOut)
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
                                    options.View
                                ]
                            ]
                        ]
                    ]
                ]
            ]
