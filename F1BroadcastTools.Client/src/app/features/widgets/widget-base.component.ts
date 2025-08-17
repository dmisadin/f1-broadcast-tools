import { signal, inject, Component, Injectable, input } from "@angular/core";

@Component({ template: '' })
export abstract class WidgetBaseComponent<TViewModel> {
    placeholderData = input<TViewModel>();

    protected abstract setState(data: TViewModel): void;
}