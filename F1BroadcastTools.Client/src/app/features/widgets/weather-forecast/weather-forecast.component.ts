import { Component, computed, OnInit, signal } from '@angular/core';
import { WidgetBaseComponent } from '../widget-base.component';
import { WeatherForecast, WeatherForecastSample } from '../../../shared/models/weather-forecast.model';
import { RestService } from '../../../core/services/rest.service';
import { SessionShortLabels, WeatherLabels } from '../../../shared/constants/widgets';
import { CommonModule } from '@angular/common';
import { SessionType } from '../../../shared/models/Enumerations';

@Component({
    selector: 'weather-forecast',
    imports: [CommonModule],
    templateUrl: './weather-forecast.component.html',
    styleUrl: './weather-forecast.component.css'
})
export class WeatherForecastComponent extends WidgetBaseComponent<WeatherForecast> implements OnInit {
    weatherForecast = signal<WeatherForecast | null>(null);
    WeatherLabels = WeatherLabels;
    SessionShortLabels = SessionShortLabels;

    constructor(private restService: RestService) { super(); }

    ngOnInit(): void {
        this.restService.get<WeatherForecast | null>("/static-widget/weather-forecast")
            .subscribe(data => data && this.setState(data));
    }

    protected override setState(data: WeatherForecast): void {
        this.weatherForecast.set(data);
    }
}
