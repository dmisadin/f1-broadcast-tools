import { app, BrowserWindow } from 'electron';
import { ChildProcess, spawn } from 'child_process';
import * as path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

let backendProcess: ChildProcess;

function createWindow() {
    const win = new BrowserWindow({
        width: 1200,
        height: 800,
        webPreferences: {
            contextIsolation: true,
        }
    });

    win.loadFile(path.join(__dirname, '../../F1BroadcastTools.Client/dist/f1-broadcast-tools/browser/index.html'));
}

app.whenReady().then(() => {
    const backendPath = path.join(__dirname, '../../F1GameDataParser/bin/Release/net8.0/win-x64/publish/F1GameDataParser.exe');
    backendProcess = spawn(backendPath, [], {
        detached: true,
        stdio: 'ignore',
    });
    backendProcess.unref();

    createWindow();
});
