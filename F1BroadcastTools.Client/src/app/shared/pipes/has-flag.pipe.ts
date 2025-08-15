import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'hasFlag'
})
export class HasFlagPipe implements PipeTransform {
    transform(value: number, flag: number): boolean {
        return (value & flag) !== 0;
    }
}
