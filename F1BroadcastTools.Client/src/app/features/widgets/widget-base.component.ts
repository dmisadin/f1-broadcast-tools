import { signal, inject, Component, Injectable, input } from "@angular/core";

@Component({ template: '' })
export abstract class WidgetBaseComponent<TViewModel> {
    placeholderData = input<TViewModel>();
}