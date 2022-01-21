import { toString, Union, Record } from "../fable_modules/fable-library.3.6.2/Types.js";
import { unit_type, union_type, lambda_type, class_type, record_type, option_type, list_type, string_type } from "../fable_modules/fable-library.3.6.2/Reflection.js";
import { Interop_reactApi } from "../fable_modules/Feliz.1.57.0/Interop.fs.js";
import { append as append_1, singleton as singleton_1, ofArray, cons, map } from "../fable_modules/fable-library.3.6.2/List.js";
import { printf, toText, join } from "../fable_modules/fable-library.3.6.2/String.js";
import { createElement } from "react";
import { createObj } from "../fable_modules/fable-library.3.6.2/Util.js";
import { Helpers_combineClasses } from "../fable_modules/Feliz.Bulma.2.18.0/ElementBuilders.fs.js";
import { empty, singleton, append, delay, toList } from "../fable_modules/fable-library.3.6.2/Seq.js";
import { map as map_1, value as value_33 } from "../fable_modules/fable-library.3.6.2/Option.js";

export class Header extends Record {
    constructor(ClassNames, Text$) {
        super();
        this.ClassNames = ClassNames;
        this.Text = Text$;
    }
}

export function Header$reflection() {
    return record_type("Criipto.React.Table.Header", [], Header, () => [["ClassNames", option_type(list_type(string_type))], ["Text", string_type]]);
}

export class CellOptions$1 extends Record {
    constructor(Formatter, ClassNames) {
        super();
        this.Formatter = Formatter;
        this.ClassNames = ClassNames;
    }
}

export function CellOptions$1$reflection(gen0) {
    return record_type("Criipto.React.Table.CellOptions`1", [gen0], CellOptions$1, () => [["Formatter", lambda_type(gen0, class_type("Feliz.IReactProperty"))], ["ClassNames", option_type(list_type(string_type))]]);
}

export class ColumnOptions$1 extends Record {
    constructor(Header, Cell) {
        super();
        this.Header = Header;
        this.Cell = Cell;
    }
}

export function ColumnOptions$1$reflection(gen0) {
    return record_type("Criipto.React.Table.ColumnOptions`1", [gen0], ColumnOptions$1, () => [["Header", option_type(Header$reflection())], ["Cell", CellOptions$1$reflection(gen0)]]);
}

export class IconSize extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Is24x24", "Is32x32", "Is48x48"];
    }
    toString() {
        const x = this;
        switch (x.tag) {
            case 1: {
                return "is-32x32";
            }
            case 2: {
                return "is-48x48";
            }
            default: {
                return "is-24x24";
            }
        }
    }
}

export function IconSize$reflection() {
    return union_type("Criipto.React.Table.IconSize", [], IconSize, () => [[], [], []]);
}

export class IconOptions extends Record {
    constructor(Name, Size) {
        super();
        this.Name = Name;
        this.Size = Size;
    }
}

export function IconOptions$reflection() {
    return record_type("Criipto.React.Table.IconOptions", [], IconOptions, () => [["Name", string_type], ["Size", IconSize$reflection()]]);
}

export class TableOptions$1 extends Record {
    constructor(ColumnOptions, TableClass, RowClass, RowSelected, Icon, Title) {
        super();
        this.ColumnOptions = ColumnOptions;
        this.TableClass = TableClass;
        this.RowClass = RowClass;
        this.RowSelected = RowSelected;
        this.Icon = Icon;
        this.Title = Title;
    }
}

export function TableOptions$1$reflection(gen0) {
    return record_type("Criipto.React.Table.TableOptions`1", [gen0], TableOptions$1, () => [["ColumnOptions", list_type(ColumnOptions$1$reflection(gen0))], ["TableClass", option_type(string_type)], ["RowClass", option_type(string_type)], ["RowSelected", option_type(lambda_type(gen0, unit_type))], ["Icon", option_type(IconOptions$reflection())], ["Title", option_type(string_type)]]);
}

export function Table(tableInputProps) {
    let elms_3;
    const data = tableInputProps.data;
    const options = tableInputProps.options;
    let header;
    const headerRow = ["children", Interop_reactApi.Children.toArray(Array.from(map((columnOption) => {
        let patternInput;
        const matchValue = columnOption.Header;
        if (matchValue != null) {
            const h = matchValue;
            patternInput = [h.Text, h.ClassNames];
        }
        else {
            patternInput = ["", void 0];
        }
        const headerText = patternInput[0];
        const classNames = patternInput[1];
        let className;
        const defaultClass_1 = "table-header-cell";
        const classNames_2 = classNames;
        if (classNames_2 != null) {
            const cls = classNames_2;
            className = join(" ", cons(defaultClass_1, cls));
        }
        else {
            className = defaultClass_1;
        }
        return createElement("div", createObj(Helpers_combineClasses("column", ofArray([["className", className], ["children", headerText]]))));
    }, options.ColumnOptions)))];
    header = createElement("div", createObj(Helpers_combineClasses("columns", ofArray([["className", "table-header"], headerRow]))));
    const rows = map((row) => {
        const cells = map((column) => {
            const cellOption = column.Cell;
            const cellFormatter = cellOption.Formatter;
            const valueRepresentation = cellFormatter(row);
            const classNames_3 = cellOption.ClassNames;
            let className_1;
            const defaultClass_3 = "table-cell";
            const classNames_5 = classNames_3;
            if (classNames_5 != null) {
                const cls_1 = classNames_5;
                className_1 = join(" ", cons(defaultClass_3, cls_1));
            }
            else {
                className_1 = defaultClass_3;
            }
            return createElement("div", createObj(Helpers_combineClasses("column", ofArray([["className", className_1], valueRepresentation]))));
        }, options.ColumnOptions);
        const props_6 = toList(delay(() => append(singleton(["className", "table-row " + ((options.RowClass != null) ? value_33(options.RowClass) : "")]), delay(() => append((options.RowSelected != null) ? singleton(["onClick", (_arg1) => {
            value_33(options.RowSelected)(row);
        }]) : empty(), delay(() => singleton(["children", Interop_reactApi.Children.toArray(Array.from(cells))])))))));
        return createElement("div", createObj(Helpers_combineClasses("columns", props_6)));
    }, data);
    const icon_1 = map_1((icon) => {
        let elms, props_8, arg10;
        const elms_1 = singleton_1((elms = singleton_1((props_8 = singleton_1(["className", (arg10 = toString(icon.Size), toText(printf("%s icon %s"))(arg10)(icon.Name))]), createElement("figure", createObj(Helpers_combineClasses("image", props_8))))), createElement("div", {
            className: "media-left",
            children: Interop_reactApi.Children.toArray(Array.from(elms)),
        })));
        return createElement("article", {
            className: "media",
            children: Interop_reactApi.Children.toArray(Array.from(elms_1)),
        });
    }, options.Icon);
    let content;
    const arg00_2 = append_1(toList(delay(() => {
        let props_12;
        return append((options.Title != null) ? singleton((props_12 = singleton_1(["children", value_33(options.Title)]), createElement("h1", createObj(Helpers_combineClasses("title", props_12))))) : empty(), delay(() => singleton(header)));
    })), rows);
    content = createElement("div", {
        className: "content",
        children: Interop_reactApi.Children.toArray(Array.from(arg00_2)),
    });
    const props_16 = ofArray([["style", {
        boxShadow: "none",
        backgroundColor: "#FFFFFF",
    }], ["children", Interop_reactApi.Children.toArray([(elms_3 = toList(delay(() => append((icon_1 != null) ? singleton(value_33(icon_1)) : empty(), delay(() => singleton(content))))), createElement("div", {
        className: "card-content",
        children: Interop_reactApi.Children.toArray(Array.from(elms_3)),
    }))])]]);
    return createElement("div", createObj(Helpers_combineClasses("card", props_16)));
}

//# sourceMappingURL=Table.js.map
