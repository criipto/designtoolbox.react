import { Union } from "./fable_modules/fable-library.3.6.2/Types.js";
import { union_type } from "./fable_modules/fable-library.3.6.2/Reflection.js";
import { useFeliz_React__React_useState_Static_1505 } from "./fable_modules/Feliz.1.57.0/React.fs.js";
import { ofArray, singleton, map } from "./fable_modules/fable-library.3.6.2/List.js";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library.3.6.2/Util.js";
import { Helpers_combineClasses } from "./fable_modules/Feliz.Bulma.2.18.0/ElementBuilders.fs.js";
import { Interop_reactApi } from "./fable_modules/Feliz.1.57.0/Interop.fs.js";
import { MenuItemOptions$1 } from "./src/SidePanelMenu.js";
import { ViewPicker } from "./src/ViewPicker.js";
import { LayoutOptions$2 } from "./src/Layout.js";
import { Layout } from "./src/Index.js";

export class View extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Hello", "Goodbye"];
    }
}

export function View$reflection() {
    return union_type("Page.View", [], View, () => [[], []]);
}

export function page() {
    const patternInput = useFeliz_React__React_useState_Static_1505(new View(0));
    const setView = patternInput[1];
    const currentView = patternInput[0];
    const patternInput_1 = useFeliz_React__React_useState_Static_1505(0);
    const user = patternInput_1[0];
    const setUser = patternInput_1[1];
    const manager = new (class {
        get UserManager() {
            return {
                HasRequestedAuthentication() {
                    return true;
                },
                LogIn() {
                },
                LogOut() {
                },
                Authenticate() {
                },
                CurrentUser: user,
            };
        }
        get ViewManager() {
            return new (class {
                get CurrentView() {
                    return currentView;
                }
                set CurrentView(newView) {
                    setView(newView);
                }
            }
            )();
        }
    }
    )();
    const views = map((v) => {
        const renderer = (v.tag === 1) ? ((_arg4) => {
            const elms_1 = ofArray([createElement("h1", createObj(Helpers_combineClasses("title", singleton(["children", "Goodbye"])))), createElement("a", createObj(Helpers_combineClasses("button", ofArray([["children", "Say hello"], ["onClick", (_arg3) => {
                manager.ViewManager.CurrentView = (new View(0));
            }]]))))]);
            return createElement("div", {
                className: "container",
                children: Interop_reactApi.Children.toArray(Array.from(elms_1)),
            });
        }) : ((_arg2) => {
            const elms = ofArray([createElement("h1", createObj(Helpers_combineClasses("title", singleton(["children", "Hello"])))), createElement("a", createObj(Helpers_combineClasses("button", ofArray([["children", "Say goodbye"], ["onClick", (_arg1) => {
                manager.ViewManager.CurrentView = (new View(1));
            }]]))))]);
            return createElement("div", {
                className: "container",
                children: Interop_reactApi.Children.toArray(Array.from(elms)),
            });
        });
        return [v, renderer];
    }, ofArray([new View(0), new View(1)]));
    const menuItems = map((tupledArg) => {
        const view = tupledArg[0];
        return new MenuItemOptions$1(view, void 0, void 0);
    }, views);
    const layoutOptions = new LayoutOptions$2(menuItems, createElement(ViewPicker, {
        views: views,
        manager: manager,
    }), manager);
    const elms_2 = singleton(Layout(layoutOptions));
    return createElement("div", {
        className: "container",
        children: Interop_reactApi.Children.toArray(Array.from(elms_2)),
    });
}

//# sourceMappingURL=Page.js.map
