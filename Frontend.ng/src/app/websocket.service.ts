import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';

@Injectable({
	providedIn: 'root'
})

export class WebSocketService {
	private ws!: WebSocketSubject<any>;
	private readonly url = 'ws://localhost:1016';
	constructor() { }

	public connect(): void {
		this.ws = webSocket(this.url);
	}

	public sendMessage(message: string): void {
		if (this.ws) {
			this.ws.next(message);
		}
	}

	public onMessage(): Observable<any> {
		if (this.ws) {
			return this.ws.asObservable();
		} else {
			throw new Error('Connect to WebSocket first.');
		}
	}

	public closeConnection(): void {
		if (this.ws) {
			this.ws.complete();
		}
	}
}
