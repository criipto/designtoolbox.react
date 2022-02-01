namespace Criipto.React

open Feliz
open Feliz.Bulma

module InlineEditor = 

    type InlineEditorOptions<'manager,'data> = {
        DisplayElement : DataManager<'manager,'data> -> ReactElement
        EditElement : DataManager<'manager,'data> -> ReactElement
        Manager : DataManager<'manager,'data>
    }

    [<ReactComponent>]
    let InlineEditor<'err,'view,'user,'data> (options : InlineEditorOptions<Types.IManager<'err,'view,'user>,'data>) = 
        let isEditing, changeMode = React.useState false
        let tempData, setTempData = React.useState options.Manager.Data
        let internalManager = 
            {
                new DataManager<_,_>(options.Manager.SystemManager) with
                    member __.Data
                            //edits are made to a temp copy of the data
                            with get() = tempData
                            and set value = setTempData value
            }
        let views = 
            [
                Some true, fun _ -> 
                        Bulma.container [
                            Bulma.button.a [
                                prop.onClick(fun _ -> 
                                    isEditing |> not |> changeMode
                                    //propagate the changes back to the source
                                    options.Manager.Data <- tempData
                                )
                                prop.children [
                                    Bulma.icon [
                                        prop.children [
                                            Html.i [
                                                prop.className "fas fa-close"
                                            ]
                                        ]
                                    ]
                                    Html.span "Save"
                                ]
                            ]
                            options.EditElement internalManager
                        ]
                Some false, fun _ -> 
                        Bulma.container [
                            Bulma.button.a [
                                prop.onClick(fun _ -> isEditing |> not |> changeMode )
                                prop.children [
                                    Bulma.icon [
                                        prop.children [
                                            Html.i [
                                                prop.className "fas fa-pen"
                                            ]
                                        ]
                                    ]
                                    Html.span "Edit"
                                ]
                            ]
                            //using the internal manager effectively makes it read only
                            //any accidental changes to the data in display mode will be disregarded
                            options.DisplayElement internalManager
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
