import { Record } from "../fable_modules/fable-library.3.6.2/Types.js";
import { SidePanelMenuOptions$2, SidePanelMenu, MenuItemOptions$1$reflection } from "./SidePanelMenu.js";
import { record_type, class_type, list_type } from "../fable_modules/fable-library.3.6.2/Reflection.js";
import { useFeliz_React__React_useState_Static_1505 } from "../fable_modules/Feliz.1.57.0/React.fs.js";
import { value as value_18 } from "../fable_modules/fable-library.3.6.2/Option.js";
import { createElement } from "react";
import { Navbar } from "./Navbar.js";
import { Interop_reactApi } from "../fable_modules/Feliz.1.57.0/Interop.fs.js";
import { singleton, ofArray } from "../fable_modules/fable-library.3.6.2/List.js";
import { createObj } from "../fable_modules/fable-library.3.6.2/Util.js";
import { Helpers_combineClasses } from "../fable_modules/Feliz.Bulma.2.18.0/ElementBuilders.fs.js";

export class LayoutOptions$2 extends Record {
    constructor(MenuItems, View, Manager) {
        super();
        this.MenuItems = MenuItems;
        this.View = View;
        this.Manager = Manager;
    }
}

export function LayoutOptions$2$reflection(gen0, gen1) {
    return record_type("Criipto.React.Layout.LayoutOptions`2", [gen0, gen1], LayoutOptions$2, () => [["MenuItems", list_type(MenuItemOptions$1$reflection(gen0))], ["View", class_type("Fable.React.ReactElement")], ["Manager", class_type("Criipto.React.Types.IManager`2", [gen0, gen1])]]);
}

export function Layout(options) {
    let elms, props_6, props_2, props_4;
    const patternInput = useFeliz_React__React_useState_Static_1505(options.MenuItems);
    const setMenuItems = patternInput[1];
    const menuItems = patternInput[0];
    const userManager = options.Manager.UserManager;
    const matchValue = userManager.CurrentUser;
    if (matchValue != null) {
        const user = value_18(matchValue);
        const children_3 = ofArray([createElement(Navbar, {
            userButtonText: "Log off",
            action: () => {
                userManager.LogOut();
            },
        }), (elms = singleton((props_6 = ofArray([["style", {
            marginTop: 40,
        }], ["className", "is-centered"], ["children", Interop_reactApi.Children.toArray([(props_2 = ofArray([["style", {
            boxShadow: "none",
        }], ["className", "is-one-quarter"], ["className", "is-one-fifth-fullhd"], ["children", Interop_reactApi.Children.toArray([createElement(SidePanelMenu, new SidePanelMenuOptions$2(menuItems, options.Manager))])]]), createElement("div", createObj(Helpers_combineClasses("column", props_2)))), (props_4 = singleton(["children", Interop_reactApi.Children.toArray([options.View])]), createElement("div", createObj(Helpers_combineClasses("column", props_4))))])]]), createElement("div", createObj(Helpers_combineClasses("columns", props_6))))), createElement("div", {
            className: "container",
            children: Interop_reactApi.Children.toArray(Array.from(elms)),
        }))]);
        return createElement("div", {
            children: Interop_reactApi.Children.toArray(Array.from(children_3)),
        });
    }
    else if (!userManager.HasRequestedAuthentication()) {
        const children = singleton(createElement(Navbar, {
            userButtonText: "Log on",
            action: () => {
                userManager.LogIn();
            },
        }));
        return createElement("div", {
            children: Interop_reactApi.Children.toArray(Array.from(children)),
        });
    }
    else {
        userManager.Authenticate();
        return createElement("div", {});
    }
}

//# sourceMappingURL=Layout.js.map
