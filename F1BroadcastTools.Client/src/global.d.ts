// src/global.d.ts
export { };

declare global {
    interface Window {
        api: {
            openOverlay(overlayId: number): Promise<boolean>;
        };
    }
}
