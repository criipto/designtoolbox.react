module Page

open Feliz.Bulma
open Criipto.React
open Criipto.React.ViewPicker
open Feliz

type View =
    Hello
    | Goodbye

[<ReactComponent>]
let Page() = 
    
    let currentView,setView = React.useState Hello
    let user,setUser = 0 |> Some |> React.useState
    let errors,setErrors = React.useState []
    let manager = {
        new IManager<_,_,_> with
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
            member __.ErrorManager with get() = 
                {
                    new IErrorManager<_> with
                        member __.Errors with get() = errors
                        member __.AddError (err : string) = err::errors |> setErrors
                        member __.Clear() = [] |> setErrors
                }
    }
    let files,setFiles = React.useState Map.empty
    let views = 
       [
           Some Hello,fun _ ->
                            Bulma.container [
                                Bulma.title [
                                    prop.text "Hello"
                                ]
                                FileUpload ({
                                    Manager = {
                                        new IDataManager<_,Map<string,string>> with
                                            member __.SystemManager with get() = manager
                                            member __.Data 
                                                    with get() = files
                                                    and set value = setFiles value
                                    }
                                    InputFieldLabel = "Document to sign"
                                    IsFullWidth = false
                                } : FileUpload.FileUploadOptions<_>)
                                Bulma.button.a [
                                    prop.text "Say goodbye"
                                    prop.onClick (fun _ -> manager.ViewManager.CurrentView <- Goodbye)
                                ]
                            ]
           Some Goodbye,fun _ ->
                                Bulma.container [
                                    Bulma.title [
                                        prop.text "Goodbye"
                                    ]
                                    Bulma.button.a [
                                        prop.text "Say hello"
                                        prop.onClick (fun _ -> manager.ViewManager.CurrentView <- Hello)
                                    ]
                                ] 
       ]
    let menuItems = 
        views
        |> List.map(fun (view,_) ->
            {
                Data = view.Value
                Notification = None
                IconName  = None
            } : SidePanelMenu.MenuItemOptions<_>
        )
    

    let layoutOptions  = 
        {
            MenuItems = menuItems
            Element = ViewPicker<string,View,int> views manager
            Manager = manager
        } : Layout.LayoutOptions<string,View,int>
    Bulma.container [
        Layout layoutOptions
    ]