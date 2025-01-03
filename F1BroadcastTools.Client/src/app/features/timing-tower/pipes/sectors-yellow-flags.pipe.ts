import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'sectorsYellowFlags',
    standalone: false
})
export class SectorsYellowFlagsPipe implements PipeTransform {

    transform(sectors: boolean[], ...args: unknown[]): string {
        const result = sectors
                        .map((sector, index) => (sector ? index + 1 : null))
                        .filter(index => index !== null);

        if (result.length === 1)
            return `Sector ${result.shift()}`;
        
        else if (result.length === 2)
            return `Sectors ${result.shift()} & ${result.shift()}`;

        return "Sectors 1, 2 & 3";
    }

}
