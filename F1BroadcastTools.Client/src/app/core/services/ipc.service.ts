import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class IpcService {
    openOverlay(overlayId: number): Promise<boolean> {
        return window.api.openOverlay(overlayId);
    }
}
