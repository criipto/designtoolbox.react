import { createElement } from "react";
import { Navbar as Navbar_1 } from "./Navbar.js";
import { SidePanelMenu as SidePanelMenu_1 } from "./SidePanelMenu.js";
import { Table as Table_1 } from "./Table.js";
import { Layout as Layout_1 } from "./Layout.js";
import { ViewPicker as ViewPicker_1 } from "./ViewPicker.js";

export function Navbar(userButtonText, action) {
    return createElement(Navbar_1, {
        userButtonText: userButtonText,
        action: action,
    });
}

export function SidePanelMenu(options) {
    return createElement(SidePanelMenu_1, options);
}

export function Table(options, data) {
    return createElement(Table_1, {
        options: options,
        data: data,
    });
}

export function Layout(options) {
    return createElement(Layout_1, options);
}

export function ViewPicker() {
    return (views) => ((manager) => createElement(ViewPicker_1, {
        views: views,
        manager: manager,
    }));
}

//# sourceMappingURL=Index.js.map
