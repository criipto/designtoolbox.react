namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types

module ViewPicker = 

    [<ReactComponent>]
    let ViewPicker<'view,'user when 'view : equality and 'view : comparison>(views : ('view option *  ViewRenderer<'view>) list) (manager : IManager<'view,'user>)= 
        let renderers = 
            views
            |> Map.ofList
        let fallback : ViewRenderer<'view> =
            fun _ -> renderers.[Some manager.ViewManager.CurrentView] manager.ViewManager.CurrentView
        let renderer = 
            renderers
            |> Map.tryFind (Some manager.ViewManager.CurrentView)
            |> Option.orElse(
                renderers.[None] |> Some
            ) |> Option.get
        
        renderer manager.ViewManager.CurrentView