module Page

open Feliz.Bulma
open Criipto.React
open Criipto.React.ViewPicker
open Feliz

type View =
    Hello
    | Goodbye

[<ReactComponent>]
let page() = 
    
    let currentView,setView = React.useState Hello
    let user,setUser = 0 |> Some |> React.useState
    let manager = {
        new IManager<_,_> with
            member __.UserManager with get() = 
                    {
                        new IUserManager<int> with
                            member __.HasRequestedAuthentication() = true
                            member __.LogIn() = ()
                            member __.LogOut() = ()
                            member __.Authenticate() = ()
                            member __.CurrentUser with get() = user
                    }
            member __.ViewManager with get() = 
                {
                    new IViewManager<View> with
                        member __.CurrentView
                            with get() = currentView
                            and set newView = setView newView
                }
    }

    let views = 
       [
           Hello
           Goodbye
       ] |> List.map(fun v ->
            let renderer = 
                match v with
                Hello -> fun _ ->
                            Bulma.container [
                                Bulma.title [
                                    prop.text "Hello"
                                ]
                                Bulma.button.a [
                                    prop.text "Say goodbye"
                                    prop.onClick (fun _ -> manager.ViewManager.CurrentView <- Goodbye)
                                ]
                            ]
                | Goodbye -> fun _ ->
                                Bulma.container [
                                    Bulma.title [
                                        prop.text "Goodbye"
                                    ]
                                    Bulma.button.a [
                                        prop.text "Say hello"
                                        prop.onClick (fun _ -> manager.ViewManager.CurrentView <- Hello)
                                    ]
                                ] 
            v,renderer
       )
    let menuItems = 
        views
        |> List.map(fun (view,_) ->
            {
                Data = view
                Notification = None
                IconName  = None
            } : SidePanelMenu.MenuItemOptions<_>
        )
    

    let layoutOptions  = 
        {
            MenuItems = menuItems
            View = ViewPicker<View,int> views manager
            Manager = manager
        } : Layout.LayoutOptions<View,int>
    Bulma.container [
        Layout layoutOptions
    ]