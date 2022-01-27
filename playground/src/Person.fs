module Person

open Feliz.Bulma
open Criipto.React
open Feliz
open Fable.Core.JsInterop

type Person = 
    {
        Name : string
        ShoeSize : int
    }

[<ReactComponent>]
let EditPerson<'manager>(manager : Types.IDataManager<'manager,Person>) = 
    let person = manager.Data
    Bulma.section [
        Bulma.field.div [
            Bulma.label "Name"
            Bulma.control.div [
                Bulma.input.text [
                    prop.placeholder "stylte"
                    prop.value person.Name
                    prop.onChange(fun (ev : Browser.Types.Event) ->
                        let name : string = ev?target?value
                        manager.Data <- { person with Name = name}
                    )
                ]
            ]
        ]
        Bulma.field.div [
            Bulma.label "Shoe size"
            Bulma.control.div [
                Bulma.input.number [
                    prop.placeholder "48"
                    prop.value person.ShoeSize
                    prop.onChange(fun (ev : Browser.Types.Event) ->
                        let shoeSize = ev?target?value |> int
                        manager.Data <- { person with ShoeSize = shoeSize}
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
            Html.span person.Name
        ]
        Bulma.field.div [
            Bulma.label "Shoe size"
            Html.span person.Name
        ]
    ]