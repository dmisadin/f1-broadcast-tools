import { Component, forwardRef, input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'switch',
    styleUrl: './switch.component.css',
    templateUrl: './switch.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SwitchComponent),
            multi: true,
        },
    ],
})
export class SwitchComponent implements ControlValueAccessor {
    id = input.required<string>();
    value = false;
    disabled = false;

    private onChange: (val: boolean) => void = () => { };
    onTouched: () => void = () => { };

    onInputChange(event: Event) {
        const checked = (event.target as HTMLInputElement).checked;
        this.value = checked;
        this.onChange(checked);
    }

    writeValue(val: boolean): void {
        this.value = val ?? false;
    }

    registerOnChange(fn: (val: boolean) => void): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }
}
