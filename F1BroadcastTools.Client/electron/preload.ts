import { contextBridge, ipcRenderer } from 'electron';

type Api = {
    openOverlay: (overlayId: number) => Promise<boolean>;
};

const api: Api = {
    openOverlay: (overlayId: number) => ipcRenderer.invoke('open-overlay', { overlayId }),
};

contextBridge.exposeInMainWorld('api', api);