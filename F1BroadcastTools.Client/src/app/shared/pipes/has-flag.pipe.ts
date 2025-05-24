import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'hasFlag',
    standalone: false
})
export class HasFlagPipe implements PipeTransform {
    transform(value: number, flag: number): boolean {
        return (value & flag) !== 0;
    }
}
