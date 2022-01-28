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

        let readFile (file : Browser.Types.File) = 
            let reader = Browser.Dom.FileReader.Create()
            reader.onload <- fun evt ->
                let content = 
                    (string evt.target?result).Split("base64,",2).[1].Trim()
                options.Manager.Data <- options.Manager.Data.Add(file.name,content)
            reader.onerror <- fun _ ->
                options.Manager.SystemManager.ErrorManager.AddError reader?error
            reader.readAsDataURL(file)
        
        Bulma.section [
            Html.div (
                fileNames
                |> List.map(fun file -> 
                    let iconName = 
                        match (file.Split(".") |> Array.last).ToLower() with
                        "jpg" | "png" | "jpeg" | "gif" -> "fa-file-image"
                        | "pdf" -> "fa-file-pdf"
                        | "doc" | "docx" -> "fa-file-word"
                        | _  -> "fa-file-check"
                    Html.div [
                        prop.className "file-upload-item"
                        prop.children [
                            Bulma.icon [
                                prop.children [
                                    Html.i [
                                         sprintf "fas %s" iconName |> prop.className
                                    ]
                                ]
                            ]
                            Html.span file
                        ]
                        
                    ]
                )
            )
            Html.div [ 
                "file " + if options.IsFullWidth then "is-fullwidth" else ""
                |> prop.className
                prop.onDrop (fun (ev : Browser.Types.DragEvent) ->
                  printfn "on drop"
                  ev.preventDefault()
                  let files = 
                    if ev.dataTransfer.items <> Fable.Core.JS.undefined then
                      [for i in 0..ev.dataTransfer.items.length - 1 do
                        let item = ev.dataTransfer.items.[i]
                        if item.kind = "file" then yield item.getAsFile()]
                    else
                      [for i in 0..ev.dataTransfer.files.length - 1 ->
                        ev.dataTransfer.files.[i]]
                  files |> List.iter readFile
                )
                prop.onDragOver (fun ev ->
                  printfn "on dragover"
                  ev.preventDefault()
                )
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
                                        options.Manager.Data |> Map.tryFind file.name |> Option.isNone
                                    ) |> List.iter readFile
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
