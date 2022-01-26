namespace Criipto.React

open Feliz

[<AutoOpen>]
module Types = 
    
    type IUserManager<'user> = 
        abstract member HasRequestedAuthentication : unit -> bool
        abstract member LogIn : unit -> unit
        abstract member LogOut : unit -> unit
        abstract member Authenticate : unit -> unit
        abstract member CurrentUser : Option<'user> with get

    type IViewManager<'view> =
        abstract member CurrentView : 'view with get,set
    type IErrorManager<'err> = 
        abstract member Errors : ('err list) with get
        abstract member AddError : 'err -> unit
        abstract member Clear : unit -> unit

    type IManager<'err,'view,'user> = 
        abstract member ViewManager : IViewManager<'view> with get
        abstract member UserManager : IUserManager<'user> with get
        abstract member ErrorManager : IErrorManager<'err> with get

    type IDataManager<'SystemManager,'data> = 
        abstract member Data : 'data with get,set
        abstract member SystemManager : 'SystemManager with get

    type ViewRenderer<'view> = 'view -> ReactElement