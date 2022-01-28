namespace Criipto.React

open Feliz
open Feliz.Bulma

module VerticalWizard = 
    type VerticalWizardOptions<'manager,'data> = 
        {
            Manager : Types.IDataManager<'manager,'data list> 
            Steps :  ((unit -> unit) -> Types.IDataManager<'manager,'data> -> ReactElement) list
        }

    [<ReactComponent>]
    let VerticalWizard<'manager,'data>(options : VerticalWizardOptions<'manager,'data>) = 
        let activated,setActivated = React.useState [0]
        printfn "Activated: %A" activated
        let isActivated i = 
            activated |> List.contains i
        let activate i = 
            printfn "Activating step %d" i
            fun () -> 
                if i |> isActivated |> not then
                    printfn "Execute Execute Execute"
                    i::activated |> setActivated
        let steps = 
            options.Manager.Data
            |> List.zip options.Steps
            |> List.mapi(fun i (step,data) ->
                if i |> isActivated then
                    let mutable _data = data
                    {
                        new Types.IDataManager<'manager,'data> with
                            member __.Data 
                                    with get() = _data
                                    and set value = 
                                        _data <- value
                                        options.Manager.Data <-
                                            options.Manager.Data
                                            |> List.mapi(fun j d ->
                                                if j = i then value else d
                                            )
                            member __.SystemManager with get() = options.Manager.SystemManager
                    } |> step (activate (i + 1))
                    |> Some
                else
                    None
            )

        steps
        |> List.filter Option.isSome
        |> List.map Option.get
        |> List.map(fun e ->
            Html.div [
                prop.className "vertical-wizard-step"
                prop.children [
                    e
                ]
            ]
        )
        |> Bulma.container