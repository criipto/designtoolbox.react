namespace Criipto.React

open Feliz
open Feliz.Bulma

[<AutoOpen>]
module Components = 
      let Navbar(options) = Navbar.Navbar(options)
      let SidePanelMenu(options) = SidePanelMenu.SidePanelMenu(options)
      let Table(options) = Table.Table(options)
      let Layout(options) = Layout.Layout(options)
      let ViewPicker = ViewPicker.ViewPicker
      let InlineEditor(options) = InlineEditor.InlineEditor options
      let FileUpload(options) = FileUpload.FileUpload options
      let VerticalWizard(options) = VerticalWizard.VerticalWizard options
    
