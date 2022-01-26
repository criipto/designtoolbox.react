namespace Criipto.React

open Feliz
open Feliz.Bulma
open Criipto.React.Types

module SidePanelMenu =
    type MenuItemOptions<'a> = 
        {
          Data : 'a
          Notification : int option
          IconName : string option
        }
    type SidePanelMenuOptions<'err,'menuItem,'user when 'menuItem : equality> = 
        {
            MenuItems : MenuItemOptions<'menuItem> list
            Manager : IManager<'err,'menuItem,'user>
        }
    [<ReactComponent>]
    let internal SidePanelMenu<'err,'menuItem,'user when 'menuItem : equality>(options : SidePanelMenuOptions<'err,'menuItem,'user>) = 
        
        let createMenuItem (item : MenuItemOptions<_>) = 
            prop.children [
                Bulma.panelIcon [
                    Html.div [ 
                        match item.IconName with
                           None -> ()
                           | Some iconName -> yield iconName |> sprintf "icon %s" |> prop.className
                        match item.Notification with
                            None | Some 0 -> ()
                            | Some count ->
                                yield prop.children [
                                    Html.span [prop.className "badge is-danger"; prop.text count]
                                ]
                    ]
                ]
                Html.span [
                    prop.text (item.Data |> string)
                ]
            ]

        let menuItems = 
            options.MenuItems
            |> List.collect(fun menuItem ->
                let className =
                    if menuItem.Data = options.Manager.ViewManager.CurrentView then
                        "is-active menu-item"
                    else
                        "menu-item"
                Bulma.panelBlock.div [
                    prop.className className
                    prop.onClick(fun _ -> options.Manager.ViewManager.CurrentView <- menuItem.Data)
                    createMenuItem menuItem
                ]::[Html.br []]
            )
        Bulma.panel [
            prop.style [
                style.boxShadow.none
            ]
            prop.children menuItems
        ] 
