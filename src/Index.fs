namespace Criipto.React

open Feliz
open Feliz.Bulma

[<AutoOpen>]
module Components = 
      let Navbar(userButtonText : string,action) = Navbar.Navbar(userButtonText,action)
      let SidePanelMenu<'menuItem,'user when 'menuItem : equality>(options : SidePanelMenu.SidePanelMenuOptions<'menuItem,'user>) = SidePanelMenu.SidePanelMenu(options)
      let Table<'a>(options : Table.TableOptions<'a>,data : 'a list) = Table.Table(options,data)
      let Layout<'view,'user when 'view: equality>(options : Layout.LayoutOptions<'view,'user>) = Layout.Layout(options)
      let ViewPicker<'view,'user when 'view: equality> = ViewPicker.ViewPicker<'view,'user>
    