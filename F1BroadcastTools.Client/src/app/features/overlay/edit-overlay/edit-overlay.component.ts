import { Component } from '@angular/core';
import { CdkDrag, CdkDragDrop, CdkDragEnd } from '@angular/cdk/drag-drop';

@Component({
    selector: 'edit-overlay',
    imports: [CdkDrag],
    templateUrl: './edit-overlay.component.html',
    styleUrl: './edit-overlay.component.css'
})
export class EditOverlayComponent {

    onAdd() {

    }

    onSubmit() {
        
    }

    drop(event: CdkDragEnd) {
        console.log(event)
        console.log(event.source.data)
    }
}
