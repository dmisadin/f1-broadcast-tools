import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'milisecondsToGap',
    standalone: false
})
export class MilisecondsToGapPipe implements PipeTransform {
    transform(milliseconds?: number): string {
        if (milliseconds === undefined)
            return "undefined";

        const isNegative = milliseconds < 0;
        const absMs = Math.abs(milliseconds);

        const totalSeconds = Math.floor(absMs / 1000);
        const remainderMilliseconds = absMs % 1000;

        let result: string;

        if (totalSeconds < 60) {
            // Less than 1 minute
            result = `${totalSeconds}.${remainderMilliseconds.toString().padStart(3, '0')}s`;
        } else {
            const minutes = Math.floor(totalSeconds / 60);
            const seconds = totalSeconds % 60;

            result = `${minutes}:${seconds.toString().padStart(2, '0')}.${remainderMilliseconds.toString().padStart(3, '0')}`;
        }

        return isNegative ? `-${result}` : result;
    }
}
