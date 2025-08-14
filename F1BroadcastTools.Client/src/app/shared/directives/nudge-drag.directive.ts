import { Directive, input, output, signal } from '@angular/core';
import { CdkDrag } from '@angular/cdk/drag-drop';
import { DragItemCoordinates } from '../models/common';

@Directive({
    selector: '[nudgeDrag]',
    standalone: true,
    host: {
        'tabindex': '0',
        // events
        '(click)': 'onClick()',
        '(document:click)': 'onDocClick($event)',
        '(keydown)': 'onKey($event)',
        // reactive host bindings
        '[class.nudge-selected]': 'selected()',
    },
    hostDirectives: [{
        directive: CdkDrag,
        inputs: ['cdkDragBoundary', 'cdkDragData', 'cdkDragFreeDragPosition'],
        outputs: ['cdkDragEnded']
    }]
})
export class NudgeDragDirective {
    constructor(private drag: CdkDrag) { }

    nudgeStep = input(1);
    nudgeStepFast = input(10);
    nudgeGrid = input<[number, number] | undefined>(undefined);

    nudgePositionChange = output<DragItemCoordinates>();

    selected = signal(false);

    onClick() {
        this.selected.set(true);
    }

    onDocClick(ev: MouseEvent) {
        const host = this.drag.element.nativeElement as HTMLElement;
        if (ev.target instanceof Node && !host.contains(ev.target)) {
            this.selected.set(false);
        }
    }

    onKey(e: KeyboardEvent) {
        if (!this.selected()) return;

        const target = e.target as HTMLElement | null;
        if (target && (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA' || target.isContentEditable))
            return;

        let dx = 0, dy = 0;
        const step = e.shiftKey ? this.nudgeStepFast() : this.nudgeStep();

        switch (e.key) {
            case 'ArrowUp': dy = -step; break;
            case 'ArrowDown': dy = step; break;
            case 'ArrowLeft': dx = -step; break;
            case 'ArrowRight': dx = step; break;
            case 'Escape': this.selected.set(false); return;
            default: return;
        }

        e.preventDefault();
        e.stopPropagation();

        const cur = this.drag.getFreeDragPosition();
        let next = { x: cur.x + dx, y: cur.y + dy };
        //next = this.snapXY(next.x, next.y);

        this.drag.setFreeDragPosition(next);
        this.nudgePositionChange.emit({ id: this.drag.data, position: next });
    }

    private snapXY(x: number, y: number) {
        const grid = this.nudgeGrid();
        if (!grid) return { x, y };
        const [gx, gy] = grid;
        const sx = gx ? Math.round(x / gx) * gx : x;
        const sy = gy ? Math.round(y / gy) * gy : y;
        return { x: sx, y: sy };
    }
}
