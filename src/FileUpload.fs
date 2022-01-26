namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core.JsInterop

module FileUpload = 

    type FileUploadOptions<'manager> = {
        Manager : Types.IDataManager<'manager,Map<string,string>>
        InputFieldLabel : string
        IsFullWidth : bool
    }
    
    [<ReactComponent>]
    let FileUpload<'err,'view,'user> (options : FileUploadOptions<Types.IManager<'err,'view,'user>>) = 
        let fileNames = 
            options.Manager.Data
            |> Map.toList
            |> List.map fst
        
        Bulma.section [
            Bulma.section (
                fileNames
                |> List.map(fun file -> 
                    Html.div [
                        prop.text file
                    ]
                )
            )
            Html.div [ 
                "file " + if options.IsFullWidth then "is-fullwidth" else ""
                |> prop.className
                prop.children [
                    Html.label [ 
                        prop.className "file-label"
                        prop.children [
                            Html.input [
                                prop.className "file-input" 
                                prop.type' "file" 
                                prop.name "resume"
                                prop.onInput (fun ev -> 
                                    let files = 
                                        let files : Browser.Types.FileList = ev.target?files
                                        [for i in 0..files.length - 1 -> files.Item i]    
                                    files
                                    |> List.filter(fun file -> 
                                        printfn "Found file: %s" file.name
                                        options.Manager.Data |> Map.tryFind file.name |> Option.isNone
                                    ) |> List.iter(fun file ->
                                        let reader = Browser.Dom.FileReader.Create()
                                        reader.onload <- fun evt ->
                                            let content = 
                                                (string evt.target?result).Split("base64,",2).[1]
                                            options.Manager.Data <- options.Manager.Data.Add(file.name,content)
                                        reader.onerror <- fun evt ->
                                            eprintfn "Error reading files"
                                        reader.readAsDataURL(file)
                                    )
                                )
                            ]
                            Html.span [
                                prop.className"file-cta"
                                prop.children [
                                    Html.span [
                                        prop.className"file-icon"
                                        prop.children [
                                            Html.i [ prop.className"fas fa-upload"]
                                        ]
                                    ]
                                    Html.span [
                                        prop.className"file-label"
                                        prop.text options.InputFieldLabel
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]