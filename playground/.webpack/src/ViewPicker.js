import { find } from "../fable_modules/fable-library.3.6.2/List.js";
import { equals } from "../fable_modules/fable-library.3.6.2/Util.js";

export function ViewPicker(viewPickerInputProps) {
    const manager = viewPickerInputProps.manager;
    const views = viewPickerInputProps.views;
    const renderer = find((tupledArg) => {
        const v = tupledArg[0];
        return equals(v, manager.ViewManager.CurrentView);
    }, views)[1];
    return renderer(manager.ViewManager.CurrentView);
}

//# sourceMappingURL=ViewPicker.js.map
