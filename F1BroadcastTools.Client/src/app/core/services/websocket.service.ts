import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class WebSocketService<T> {
    private socket!: WebSocket;
    private messageSubject = new Subject<T>();
    private isConnected = false;

    connect(url: string): void {
        this.socket = new WebSocket(url);

        this.socket.onopen = () => {
            console.log('WebSocket connection established');
            this.isConnected = true;
        };

        this.socket.onmessage = (event: MessageEvent) => {
            const data: T = JSON.parse(event.data)
            this.messageSubject.next(data);
        };

        this.socket.onerror = (error) => {
            console.error('WebSocket error', error);
        };

        this.socket.onclose = () => {
            this.isConnected = false;
            console.log('WebSocket connection closed');
        };
    }
    /* DISABLED:
    sendMessage(message: string): void {
        if (this.isConnected && this.socket?.readyState === WebSocket.OPEN) {
            this.socket.send(message);
        } else {
            console.error('WebSocket is not open');
        }
    }
     */
    
    onMessage(): Observable<T> {
        return this.messageSubject.asObservable();
    }
}
