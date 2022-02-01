namespace Criipto.React

open Feliz
open Feliz.Bulma

module VerticalWizard = 
    type StepStatus = 
        Disabled = 0
        | Showing = 1
        | Summary = 2
    type IStep = 
        abstract member Status : StepStatus with get,set
        abstract member CurrentView : ReactElement with get
        abstract member PreviousButton : ReactElement option with get
        abstract member NextButton : ReactElement option with get
        
    [<AbstractClass>]
    type Step<'manager,'data>(manager) = 
        let mutable _status = StepStatus.Disabled
        
        abstract member DisabledView : ReactElement with get
        abstract member ShowingView : ReactElement with get
        abstract member SummaryView : ReactElement with get
        abstract member PreviousButton : ReactElement option with get
        abstract member NextButton : ReactElement  option with get
        
        interface IStep with
            member __.Status
                     with get() = _status
                     and set s = _status <- s
            member this.CurrentView
                     with get() = 
                         match _status with
                         StepStatus.Summary -> this.SummaryView
                         | StepStatus.Showing -> this.ShowingView
                         | _ -> this.DisabledView
            member this.PreviousButton with get() = this.PreviousButton
            member this.NextButton with get() = this.NextButton

        
    type VerticalWizardOptions<'err,'view,'user> = 
        {
            Manager : IManager<'err,'view,'user>
            Steps : IStep list
        }

    [<ReactComponent>]
    let VerticalWizard<'err,'view,'user>(options : VerticalWizardOptions<'err,'view,'user>) = 
        let steps,updateSteps = React.useState (options.Steps)
        //make sure at least one step is in showing state
        (match steps |> List.tryFind (fun step -> step.Status = StepStatus.Showing) with
         None -> 
            match steps with
            [] -> ()
            | s::teps -> 
                s.Status <- StepStatus.Showing
                s::teps |> updateSteps
        | _ -> ())
        let update primaryIndex secondaryIndex = 
            steps
            |> List.mapi(fun i step ->
                if  secondaryIndex = i then
                    step.Status <- StepStatus.Showing
                elif primaryIndex = i then
                    step.Status <- StepStatus.Summary
                step
            ) |> updateSteps
        let prev i = fun _ -> update i (i - 1)
        let next i = fun _ -> update i (i + 1)
        steps
        |> List.mapi(fun i step ->
            Bulma.section [
                prop.className "vertical-wizard-step"
                prop.children [
                    yield step.CurrentView
                    if step.Status = StepStatus.Showing then
                        yield Bulma.section [
                            prop.className "button-container"
                            prop.style [
                                style.width (length.percent(100))
                            ]
                            prop.children [
                                if step.PreviousButton.IsSome then 
                                    yield Bulma.button.button [
                                        prop.className "vertical-wizard-button is-pulled-left"
                                        prop.onClick (prev i)
                                        prop.children [
                                            step.PreviousButton.Value
                                        ]
                                    ]
                                if step.NextButton.IsSome then 
                                    yield Bulma.button.button [
                                        prop.className "vertical-wizard-button is-pulled-right"
                                        prop.onClick (next i)
                                        prop.children [
                                            step.NextButton.Value
                                        ]
                                    ]
                            ]
                        ]
                ]
            ]
        )
        |> Bulma.container