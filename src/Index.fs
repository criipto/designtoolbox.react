namespace Criipto.React

open Feliz
open Feliz.Bulma

[<AutoOpen>]
module Components = 
      let Navbar(userButtonText : string,action) = Navbar.Navbar(userButtonText,action)
      let SidePanelMenu(options) = SidePanelMenu.SidePanelMenu(options)
      let Table(options) = Table.Table(options)
      let Layout(options) = Layout.Layout(options)
      let ViewPicker = ViewPicker.ViewPicker
      let InlineEditor(options) = InlineEditor.InlineEditor options
      let FileUpload(options) = FileUpload.FileUpload options
    