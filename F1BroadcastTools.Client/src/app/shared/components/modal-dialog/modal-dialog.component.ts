import { Component, HostListener, input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CdkTrapFocus } from '@angular/cdk/a11y';

@Component({
    selector: 'modal-dialog',
    standalone: true,
    imports: [CommonModule, CdkTrapFocus],
    templateUrl: './modal-dialog.component.html',
    styleUrls: ['./modal-dialog.component.css'],
    host: {
        '[class.open]': 'open()',
    }
})
export class ModalDialogComponent {
    isOverflowVisible = input(false);
    open = input(false);
    openChange = output<boolean>();
    discardLabel = input('Discard');
    confirmLabel = input('Save');
    guardClose = input(false);  // Does parent decide on when to close?

    requestClose = output<CloseType>();

    discard = output<void>();
    confirm = output<void>();

    private pointerDownStartedOnBackdrop = false;

    private attemptClose(reason: CloseType) {
        this.requestClose.emit(reason);
        if (!this.guardClose()) {
            this.openChange.emit(false);
        }
    }

    onBackdropPointerDown(e: PointerEvent) {
        this.pointerDownStartedOnBackdrop = e.target === e.currentTarget;
    }

    onBackdropClick(e: MouseEvent) {
        if (this.pointerDownStartedOnBackdrop && e.target === e.currentTarget) {
            this.discard.emit();
            this.attemptClose('backdrop');
        }
        this.pointerDownStartedOnBackdrop = false;
    }

    onDiscard() {
        this.discard.emit();
        this.attemptClose('discard');
    }

    onConfirm() {
        this.confirm.emit();
        this.attemptClose('confirm');
    }

    @HostListener('document:keydown', ['$event'])
    onKeydown(e: KeyboardEvent) {
        if (!this.open()) return;
        if (e.key === 'Escape') {
            e.preventDefault();
            this.attemptClose('esc');
        }
    }
}

export type CloseType = 'backdrop' | 'esc' | 'discard' | 'confirm';