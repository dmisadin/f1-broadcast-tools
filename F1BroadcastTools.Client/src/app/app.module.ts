import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from "./shared/shared.module";
import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        CommonModule,
        AppRoutingModule,
        SharedModule,
        BrowserModule
    ],
    providers: [
        provideHttpClient(),
        provideAnimations()
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
