namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types

module ViewPicker = 


    [<ReactComponent>]
    let ViewPicker<'view,'user when 'view : equality>(views : ('view *  ViewRenderer<'view>) list) (manager : IManager<'view,'user>)= 
        
        let renderer = 
            (views
            |> List.find(fun (v,_) -> v = manager.ViewManager.CurrentView) 
            |> snd)
        
        renderer manager.ViewManager.CurrentView