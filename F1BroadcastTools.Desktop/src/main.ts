import { app, BrowserWindow } from 'electron';
import { ChildProcess, spawn } from 'child_process';
import * as path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

let backendProcess: ChildProcess | null = null;

function createWindow() {
    const mainWindow = new BrowserWindow({
        width: 1200,
        height: 800,
        webPreferences: {
            contextIsolation: true,
        }
    });
    if (process.env.NODE_ENV === 'development')
        mainWindow.loadURL('http://localhost:4200');
    else {
        mainWindow.loadFile(
            path.join(__dirname, '../../F1BroadcastTools.Client/dist/f1-broadcast-tools-client/browser/index.html')
        );
    }
}

app.whenReady().then(() => {
    const backendPath = path.join(
        __dirname,
        '../../F1GameDataParser/bin/Release/net8.0/win-x64/publish/F1GameDataParser.exe'
    );

    backendProcess = spawn(backendPath, [], {
        stdio: 'ignore'
    });

    createWindow();
});

app.on('before-quit', () => {
    if (backendProcess) {
        try {
            backendProcess.kill(); // sends SIGTERM on unix, terminate on Windows
        } catch (err) {
            console.error('Failed to kill backend process', err);
        }
        backendProcess = null;
    }
});

// Also catch abnormal exits
app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});
