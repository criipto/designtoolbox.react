namespace Criipto.React

open Feliz
open Feliz.Bulma
open Fable.Core
open Criipto.React.SidePanelMenu
open Criipto.React.Navbar
open Criipto.React.Types

module ViewPicker = 

    [<ReactComponent>]
    let ViewPicker<'view,'user when 'view : equality>(views : ('view option *  ViewRenderer<'view>) list) (manager : IManager<'view,'user>)= 
        
        let _,renderer = 
            views
            |> List.find(fun (v,_) -> 
                match v with
                Some v when v = manager.ViewManager.CurrentView -> true
                | _ -> false
            )
        
        renderer manager.ViewManager.CurrentView