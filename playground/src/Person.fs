module Person

open Feliz.Bulma
open Criipto.React
open Feliz
open Fable.Core.JsInterop

type Person = 
    {
        Name : string option
        ShoeSize : int option
    }

[<ReactComponent>]
let EditPerson<'manager>(manager : Types.IDataManager<'manager,Person>) = 
    let person = manager.Data
    Bulma.section [
        Bulma.field.div [
            Bulma.label "Name"
            Bulma.control.div [
                Bulma.input.text [
                    yield prop.placeholder "your name"
                    if person.Name.IsSome then yield prop.value person.Name.Value
                    yield prop.onChange(fun (ev : Browser.Types.Event) ->
                                let name : string = ev?target?value
                                manager.Data <- { person with Name = Some name}
                            )
                ]
            ]
        ]
        Bulma.field.div [
            Bulma.label "Shoe size"
            Bulma.control.div [
                Bulma.input.number [
                    yield prop.placeholder "52"
                    if person.ShoeSize.IsSome then yield prop.value person.ShoeSize.Value
                    yield prop.onChange(fun (ev : Browser.Types.Event) ->
                        let ok,shoeSize = System.Int32.TryParse (string ev?target?value)
                        manager.Data <- { person with ShoeSize = if ok then Some shoeSize else None}
                    )
                ]
            ]
        ]
    ]


[<ReactComponent>]
let DisplayPerson(manager : Types.IDataManager<'manager,Person>) =
    let person = manager.Data 
    Bulma.section [
        Bulma.field.div [
            Bulma.label "Name"
            Html.span (if person.Name.IsSome then person.Name.Value else "")
        ]
        Bulma.field.div [
            yield Bulma.label "Shoe size"
            if person.ShoeSize.IsSome then yield Html.span person.ShoeSize.Value
        ]
    ]