import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'milisecondsToTime'
})
export class MilisecondsToTimePipe implements PipeTransform {
    transform(value: number | null | undefined): string {
        if (value == null || isNaN(value)) return '0.000';

        const minutes = Math.floor(value / 60000);
        const seconds = Math.floor((value % 60000) / 1000);
        const milliseconds = value % 1000;

        if (minutes > 0) {
            return `${minutes}:${seconds.toString().padStart(2, '0')}.${milliseconds.toString().padStart(3, '0')}`;
        } else {
            return `${seconds}.${milliseconds.toString().padStart(3, '0')}`;
        }
    }
}
