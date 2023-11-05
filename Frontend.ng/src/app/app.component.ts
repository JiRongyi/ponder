import { Component, OnInit, OnDestroy } from '@angular/core';
import { WebSocketService } from './websocket.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'appRoot',
  template: `
    <h1>{{title}}</h1>
		<ul><li *ngFor="let cmd of commands">
      <button type="button" class="btn btn-primary" id="{{cmd}}" style="width: 100px; height: 50px; padding: 10px; margin: 5px;" (click)="onClick($event)">{{cmd}}</button>
    </li></ul>
      <div>
        <p *ngFor="let msg of messages">{{ msg }}</p>
      </div>
		`
})

export class AppComponent implements OnInit, OnDestroy {
  title: string = 'Fingerprint Sensor Command';
  public messages: string[] = [];
  public message: string = '';
  private messageSubscription!: Subscription;
  commands = ["GetID", "TestLED", "Enroll", "Match", "Delete"];

  constructor(private ws: WebSocketService) { }
  ngOnInit(): void {
    this.ws.connect();
    this.messageSubscription = this.ws.onMessage().subscribe(
      (message) => {
        this.messages.push(message);
      },
      (error) => { console.error('Error occurred: ', error); }
    );
  }
  ngOnDestroy(): void {
    this.ws.closeConnection();
    this.messageSubscription.unsubscribe();
  }
  onClick(ev: any) {
    console.log(ev.srcElement.id);
    this.ws.sendMessage(ev.srcElement.id);
  }
}
