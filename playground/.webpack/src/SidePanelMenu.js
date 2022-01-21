import { toString, Record } from "../fable_modules/fable-library.3.6.2/Types.js";
import { class_type, list_type, record_type, string_type, option_type, int32_type } from "../fable_modules/fable-library.3.6.2/Reflection.js";
import { Interop_reactApi } from "../fable_modules/Feliz.1.57.0/Interop.fs.js";
import { createElement } from "react";
import { equals, createObj } from "../fable_modules/fable-library.3.6.2/Util.js";
import { empty, singleton, append, delay, toList } from "../fable_modules/fable-library.3.6.2/Seq.js";
import { printf, toText } from "../fable_modules/fable-library.3.6.2/String.js";
import { ofArray, collect, singleton as singleton_1 } from "../fable_modules/fable-library.3.6.2/List.js";
import { Helpers_combineClasses } from "../fable_modules/Feliz.Bulma.2.18.0/ElementBuilders.fs.js";

export class MenuItemOptions$1 extends Record {
    constructor(Data, Notification, IconName) {
        super();
        this.Data = Data;
        this.Notification = Notification;
        this.IconName = IconName;
    }
}

export function MenuItemOptions$1$reflection(gen0) {
    return record_type("Criipto.React.SidePanelMenu.MenuItemOptions`1", [gen0], MenuItemOptions$1, () => [["Data", gen0], ["Notification", option_type(int32_type)], ["IconName", option_type(string_type)]]);
}

export class SidePanelMenuOptions$2 extends Record {
    constructor(MenuItems, Manager) {
        super();
        this.MenuItems = MenuItems;
        this.Manager = Manager;
    }
}

export function SidePanelMenuOptions$2$reflection(gen0, gen1) {
    return record_type("Criipto.React.SidePanelMenu.SidePanelMenuOptions`2", [gen0, gen1], SidePanelMenuOptions$2, () => [["MenuItems", list_type(MenuItemOptions$1$reflection(gen0))], ["Manager", class_type("Criipto.React.Types.IManager`2", [gen0, gen1])]]);
}

export function SidePanelMenu(options) {
    const createMenuItem = (item) => {
        let elms;
        return ["children", Interop_reactApi.Children.toArray([(elms = singleton_1(createElement("div", createObj(toList(delay(() => {
            let matchValue, iconName;
            return append((matchValue = item.IconName, (matchValue != null) ? ((iconName = matchValue, singleton(["className", toText(printf("icon %s"))(iconName)]))) : ((empty()))), delay(() => {
                const matchValue_1 = item.Notification;
                let pattern_matching_result, count;
                if (matchValue_1 != null) {
                    if (matchValue_1 === 0) {
                        pattern_matching_result = 0;
                    }
                    else {
                        pattern_matching_result = 1;
                        count = matchValue_1;
                    }
                }
                else {
                    pattern_matching_result = 0;
                }
                switch (pattern_matching_result) {
                    case 0: {
                        return empty();
                    }
                    case 1: {
                        return singleton(["children", Interop_reactApi.Children.toArray([createElement("span", {
                            className: "badge is-danger",
                            children: count,
                        })])]);
                    }
                }
            }));
        }))))), createElement("span", {
            className: "panel-icon",
            children: Interop_reactApi.Children.toArray(Array.from(elms)),
        })), createElement("span", {
            children: toString(item.Data),
        })])];
    };
    const menuItems = collect((menuItem) => {
        let props_4;
        const className = equals(menuItem.Data, options.Manager.ViewManager.CurrentView) ? "is-active menu-item" : "menu-item";
        return ofArray([(props_4 = ofArray([["className", className], ["onClick", (_arg1) => {
            options.Manager.ViewManager.CurrentView = menuItem.Data;
        }], createMenuItem(menuItem)]), createElement("div", createObj(Helpers_combineClasses("panel-block", props_4)))), createElement("br", {})]);
    }, options.MenuItems);
    const props_7 = ofArray([["style", {
        boxShadow: "none",
    }], ["children", Interop_reactApi.Children.toArray(Array.from(menuItems))]]);
    return createElement("nav", createObj(Helpers_combineClasses("panel", props_7)));
}

//# sourceMappingURL=SidePanelMenu.js.map
