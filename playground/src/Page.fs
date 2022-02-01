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
    let user,setUser = None|> React.useState
    let errors,setErrors = React.useState []
    let wizardData,setWizardData = 
        React.useState
            {|
               Files = [] 
               Person = {Name = None; ShoeSize = None}
            |}
            
    
    let manager = {
        new IManager<_,_,_> with
            member __.UserManager with get() = 
                    {
                        new IUserManager<int> with
                            member __.HasRequestedAuthentication() = false
                            member __.LogIn() = 
                                0 |> Some |> setUser
                            member __.LogOut() = 
                                None |> setUser
                            member this.Authenticate() = 
                                Fable.Core.JS.setTimeout(this.LogIn) 1000 |> ignore
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
        let stepsDataManager = 
            { new DataManager<_,_>(manager) with
                member __.Data 
                        with get() = wizardData
                        and set data = 
                            printfn "Updating wizard %A" data
                            setWizardData data
            }
        [
            {
                new VerticalWizard.Step<_,_>(stepsDataManager) with
                    
                    override __.DisabledView  with get() = 
                        Html.div [ prop.className "disabled"; prop.children [Html.h1 "Select documents to sign" ]]
                    override this.ShowingView with get() = 
                        FileUpload ({
                            Manager = {
                                new DataManager<_,_>(manager) with
                                    member __.Data 
                                            with get() = stepsDataManager.Data.Files
                                            and set files = 
                                                    stepsDataManager.Data <- {| stepsDataManager.Data with Files = files |}
                            }
                            InputFieldLabel = "Document to sign"
                            IsFullWidth = false
                        } : FileUpload.FileUploadOptions<_>)
                    override __.SummaryView with get() = 
                        Html.div [ prop.className "summary"; prop.children [Html.h1 "Select documents to sign" ]]
                    override __.PreviousButton = None
                    override __.NextButton = 
                        Html.span [
                            prop.className "is-pulled-right"
                            prop.text "Next"
                        ] |> Some
            } :> VerticalWizard.IStep
            
            {
                new VerticalWizard.Step<_,_>(stepsDataManager) with
                    
                    override __.DisabledView  with get() = 
                        Html.div [ prop.className "disabled"; prop.children [Html.h1 "Select documents to sign" ]]
                    override __.ShowingView with get() = 
                        
                        inlineEditor 
                            {
                                new DataManager<_,_>(manager) with
                                    member __.Data
                                            with get() = stepsDataManager.Data.Person
                                            and set person = 
                                                stepsDataManager.Data <- {| stepsDataManager.Data with Person = person |} 
                            }
                    override __.SummaryView with get() = 
                        Html.div [ prop.className "summary"; prop.children [Html.h1 "Select documents to sign" ]]
                    override __.PreviousButton = 
                        Html.span [
                            prop.className "is-pulled-left"
                            prop.text "Previous"
                        ] |> Some
                    override __.NextButton = 
                        Html.span [
                            prop.className "is-pulled-right"
                            prop.text "Next"
                        ] |> Some
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
                                    Manager = manager
                                    Steps = steps
                                } : VerticalWizard.VerticalWizardOptions<_,_,_>)
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
               Bulma.section [Html.h1 "Creating an error after 1s"]
            None, fun _ ->
                Bulma.section [Html.h1 "Default view"]
                           
       ]

    let menuItems = 
        views
        |> List.filter (fst >> Option.isSome)
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