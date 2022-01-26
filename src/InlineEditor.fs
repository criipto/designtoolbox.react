namespace Criipto.React

open Feliz
open Feliz.Bulma

module InlineEditor = 

    type InlineEditorOptions<'manager,'data> = {
        DisplayElement : 'data -> ReactElement
        EditElement : 'data -> ReactElement
        Manager : Types.IDataManager<'manager,'data>
    }

    [<ReactComponent>]
    let InlineEditor<'err,'view,'user,'data when 'view : equality> (options : InlineEditorOptions<Types.IManager<'err,'view,'user>,'data>) = 
        let isEditing, changeMode = React.useState false
        let views = 
            [
                Some true, fun _ -> 
                        Bulma.container [
                            Bulma.icon [
                                prop.onClick(fun _ -> isEditing |> not |> changeMode )
                                prop.children [
                                    Html.i [
                                        prop.className "fas fa-close"
                                    ]
                                ]
                            ]
                            options.EditElement options.Manager.Data
                        ]
                Some false, fun _ -> 
                        Bulma.container [
                            Bulma.icon [
                                prop.onClick(fun _ -> isEditing |> not |> changeMode )
                                prop.children [
                                    Html.i [
                                        prop.className "fas fa-pen"
                                    ]
                                ]
                            ]
                            options.DisplayElement options.Manager.Data
                        ]
            ]
        ViewPicker.ViewPicker(views) { new IManager<'err,bool,'user> with
                                            member __.UserManager with get() = options.Manager.SystemManager.UserManager
                                            member __.ViewManager with get() = 
                                                {new IViewManager<bool> with
                                                   member __.CurrentView
                                                           with get() = isEditing
                                                           and  set mode = changeMode mode}
                                            member __.ErrorManager with get() = options.Manager.SystemManager.ErrorManager
                                     }