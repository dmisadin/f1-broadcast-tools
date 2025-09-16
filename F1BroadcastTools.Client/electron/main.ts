import { app, BrowserWindow, ipcMain } from 'electron';
import * as path from 'node:path';
import * as url from 'node:url';
import { spawn, ChildProcess } from 'node:child_process';
import * as fs from 'node:fs';

const isDev = process.env['NODE_ENV'] === 'development';
let mainWindow: BrowserWindow | null = null;
let backendProcess: ChildProcess | null = null;

/** ---------- Paths ---------- **/
function getPreloadPath() {
    // Compiled by tsc to .electron/preload.js
    return path.join(__dirname, 'preload.js');
}

function getFrontendIndexFile() {
    // In production, electron-builder copies Angular build to resources/app (see notes below)
    return path.join(process.resourcesPath, 'app', 'index.html');
}

function getFrontendRootURL() {
    if (isDev) return 'http://localhost:4200';
    return url.pathToFileURL(getFrontendIndexFile()).toString();
}

function resolveBackendPath(): string {
    if (isDev) {
        // Your dev exe path (adjust if you use Debug)
        return path.join(
            __dirname,
            '../../F1GameDataParser/bin/Release/net8.0/win-x64/publish/F1GameDataParser.exe'
        );
    }
    // In production we ship the backend into resources/backend via extraResources.
    return path.join(process.resourcesPath, 'backend', 'F1GameDataParser.exe');
}

/** ---------- Windows ---------- **/
async function createWindow() {
    mainWindow = new BrowserWindow({
        width: 1200,
        height: 800,
        show: false,
        webPreferences: {
            preload: getPreloadPath(),
            contextIsolation: true,
            sandbox: true,
            nodeIntegration: false,
        },
    });

    if (isDev) {
        await mainWindow.loadURL(getFrontendRootURL());
        mainWindow.webContents.openDevTools({ mode: 'detach' });
    } else {
        await mainWindow.loadURL(getFrontendRootURL());
    }

    mainWindow.once('ready-to-show', () => mainWindow?.show());
}

function createWidgetWindow(widget: any) {
    const w = new BrowserWindow({
        width: 400,
        height: 500,
        x: widget.positionX,
        y: widget.positionY,
        frame: false,
        transparent: true,
        alwaysOnTop: true,
        resizable: true,
        movable: true,
        backgroundColor: '#00000000',
        webPreferences: {
            contextIsolation: true,
            sandbox: true,
            nodeIntegration: false,
        },
    });

    // Map your widget types to Angular routes
    let route = '/widgets/timing-tower';
    switch (widget.widgetType) {
        case 2: route = '/widgets/stopwatch'; break;
        case 3: route = '/widgets/minimap'; break;
        case 4: route = '/widgets/halo-hud'; break;
    }

    if (isDev) {
        w.loadURL(`http://localhost:4200${route}`);
    } else {
        // Use hash routes in prod so Angular loads correctly from file://
        // (Easiest is HashLocationStrategy in Angular; otherwise keep this # trick.)
        const base = getFrontendRootURL(); // file:///.../index.html
        w.loadURL(`${base}#${route}`);
    }
}

/** ---------- Backend (.NET) ---------- **/
function startBackend() {
    const exe = resolveBackendPath();

    if (!fs.existsSync(exe)) {
        console.error('[backend] exe not found at', exe);
        return;
    }
    if (backendProcess && !backendProcess.killed) return;

    backendProcess = spawn(exe, [], {
        stdio: 'ignore',
        windowsHide: true,
    });

    backendProcess.on('error', (err) => console.error('[backend] spawn error', err));
    backendProcess.on('exit', (code, signal) => {
        console.log(`[backend] exited code=${code} signal=${signal}`);
        backendProcess = null;
    });
}

function stopBackend() {
    if (backendProcess && !backendProcess.killed) {
        try { backendProcess.kill(); } catch (e) { /* noop */ }
        backendProcess = null;
    }
}

app.commandLine.appendSwitch('enable-gpu-rasterization');
app.commandLine.appendSwitch('enable-zero-copy');
app.commandLine.appendSwitch('ignore-gpu-blacklist');

/** ---------- App lifecycle ---------- **/
app.whenReady().then(() => {
    startBackend();
    createWindow();
});

app.on('before-quit', stopBackend);

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') app.quit();
});

app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) createWindow();
});

/** ---------- IPC ---------- **/
ipcMain.handle('open-overlay', async (_event, { overlayId }) => {
    try {
        const res = await fetch(`http://localhost:5000/api/overlay/get?id=${overlayId}`);
        const overlay = await res.json();

        overlay.widgets.forEach((widget: any) => createWidgetWindow(widget));

        return true; // resolves back to renderer
    } catch (err) {
        console.error('open-overlay failed', err);
        return false;
    }
});
