import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'milisecondsToGap',
    standalone: false
})
export class MilisecondsToGapPipe implements PipeTransform {
    transform(milliseconds: number): string {
        if (milliseconds < 0) {
            throw new Error('Milliseconds cannot be negative');
        }

        const totalSeconds = Math.floor(milliseconds / 1000);
        const remainderMilliseconds = milliseconds % 1000;

        if (totalSeconds < 60) {
            // Less than 1 minute
            return `${totalSeconds}.${remainderMilliseconds.toString().padStart(3, '0')}`;
        }

        const minutes = Math.floor(totalSeconds / 60);
        const seconds = totalSeconds % 60;

        return `${minutes}:${seconds.toString().padStart(2, '0')}.${remainderMilliseconds.toString().padStart(3, '0')}`;
    }
}