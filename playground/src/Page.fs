module Page

open Feliz.Bulma
open Criipto.React
open Criipto.React.ViewPicker
open Feliz
open Person

type View =
    Hello
    | Goodbye
    | Error

type Step = 
    Files of (string * FileUpload.File) list
    | Person of Person

[<ReactComponent>]
let Page() = 
    
    let currentView,setView = React.useState Hello
    let user,setUser = 0 |> Some |> React.useState
    let errors,setErrors = React.useState []
    let wizardData,setWizardData = 
        React.useState
            [
                Files []
                Person {Name = None; ShoeSize = None}
            ]
    
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
    
    let inlineEditor manager = 
        InlineEditor {
            DisplayElement = DisplayPerson
            EditElement = EditPerson
            Manager = manager
        }
    let steps = 
        [
            fun activate (manager : Types.IDataManager<_,Step>)  -> 
                let files = 
                    match manager.Data with
                    Files files -> files |> Map.ofList
                    | step -> 
                       eprintfn "Could not consume %A" step
                       Map.empty
                    

                FileUpload ({
                    Manager = {
                        new Types.IDataManager<_,_> with
                            member __.Data 
                                     with get() = files
                                     and set files = 
                                            let filesList = 
                                                files
                                                |> Map.toList
                                            printfn "File list: %A" (filesList |> List.map fst)
                                            manager.Data <- Files filesList
                                            activate()
                            member __.SystemManager with get() = manager.SystemManager
                    }
                    InputFieldLabel = "Document to sign"
                    IsFullWidth = false
                } : FileUpload.FileUploadOptions<_>)
            fun activate (manager : Types.IDataManager<_,Step>) -> 
                let person = 
                    match manager.Data with
                    Person person -> person
                    | step ->
                        eprintfn "Could not consume %A" step
                        {
                            Name = None
                            ShoeSize = None
                        }
                inlineEditor 
                    {
                        new Types.IDataManager<_,_> with
                            member __.SystemManager with get() = manager.SystemManager
                            member __.Data
                                    with get() = person
                                    and set person = 
                                        manager.Data <- Person person
                                        activate()
                    }
        ]
    let views = 
       [
           Some Hello,fun _ ->
                            Bulma.container [
                                Bulma.title [
                                    prop.text "Hello"
                                ]
                                Bulma.button.a [
                                    prop.text "Say goodbye"
                                    prop.onClick (fun _ -> manager.ViewManager.CurrentView <- Goodbye)
                                ]
                                VerticalWizard({
                                    Manager = {
                                        new Types.IDataManager<_,_> with
                                            member __.SystemManager with get() = manager
                                            member __.Data 
                                                     with get() = wizardData
                                                     and set data = setWizardData data
                                    }
                                    Steps = steps
                                } : VerticalWizard.VerticalWizardOptions<_,_>)
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
           Some Error, fun _ ->
               Fable.Core.JS.setInterval(fun () -> manager.ErrorManager.AddError (sprintf "This is #%d error" (errors.Length + 1))) 1000 |> ignore
               Bulma.section []
                           
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
            Navbar = {
                LogOutText = "Log out"
                LogInText = "Log in"
                Manager = manager
                AppName = "Playground"
            }
        } : Layout.LayoutOptions<string,View,int>
    
    Bulma.container [
        Bulma.section [
            prop.className "errors"
            prop.children 
                ( errors
                  |> List.map (fun e -> Html.div [Html.span e]))   
        ]
        Layout layoutOptions
    ]