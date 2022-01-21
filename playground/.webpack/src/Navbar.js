import { createElement } from "react";
import { createObj } from "../fable_modules/fable-library.3.6.2/Util.js";
import { Helpers_combineClasses } from "../fable_modules/Feliz.Bulma.2.18.0/ElementBuilders.fs.js";
import { ofArray, singleton } from "../fable_modules/fable-library.3.6.2/List.js";
import { Interop_reactApi } from "../fable_modules/Feliz.1.57.0/Interop.fs.js";
import { printf, toText } from "../fable_modules/fable-library.3.6.2/String.js";

export function Navbar(navbarInputProps) {
    let elms, props_4, elms_3, elms_2, elms_1, props_9;
    const action = navbarInputProps.action;
    const userButtonText = navbarInputProps.userButtonText;
    const elms_4 = ofArray([(elms = ofArray([createElement("div", createObj(Helpers_combineClasses("navbar-item", singleton(["className", "icon credit-card-logo"])))), (props_4 = ofArray([["className", "logo-text"], ["children", Interop_reactApi.Children.toArray([createElement("span", {
        className: "app-name",
        children: "%APPNAME%",
    }), createElement("span", {
        className: "bank",
        children: "Bank",
    })])]]), createElement("div", createObj(Helpers_combineClasses("navbar-item", props_4))))]), createElement("div", {
        className: "navbar-start",
        children: Interop_reactApi.Children.toArray(Array.from(elms)),
    })), (elms_3 = singleton((elms_2 = singleton((elms_1 = singleton((props_9 = ofArray([["onClick", (_arg1) => {
        action();
    }], ["style", {
        backgroundColor: "transparent",
        borderStyle: "none",
        fontSize: 18 + "px",
    }], ["children", Interop_reactApi.Children.toArray([createElement("span", {
        className: "navbar-item",
        children: userButtonText,
    }), createElement("div", {
        className: toText(printf("icon %s"))("power-off-white"),
    })])]]), createElement("a", createObj(Helpers_combineClasses("button", props_9))))), createElement("div", {
        className: "buttons",
        children: Interop_reactApi.Children.toArray(Array.from(elms_1)),
    }))), createElement("div", {
        className: "navbar-item",
        children: Interop_reactApi.Children.toArray(Array.from(elms_2)),
    }))), createElement("div", {
        className: "navbar-end",
        children: Interop_reactApi.Children.toArray(Array.from(elms_3)),
    }))]);
    return createElement("div", {
        className: "navbar-menu",
        children: Interop_reactApi.Children.toArray(Array.from(elms_4)),
    });
}

//# sourceMappingURL=Navbar.js.map
