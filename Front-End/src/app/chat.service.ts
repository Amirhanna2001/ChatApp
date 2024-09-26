import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

 @Injectable({
  providedIn: 'root'
})
export class ChatService {

  public connection : signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl('http://localhost:8000/chat')
  .configureLogging(signalR.LogLevel.Information)
  .build();
  constructor() { }

  public async start(){
    try{
      this.connection.start();
    }
    catch(err){
      console.log(err);
      
    }
  }
  public async joinRoom(user:string,room:string){
    return this.connection.invoke('JoinChat',{user,room});
  }
  public async joinSpecificChat(user:string,room:string){
    return this.connection.invoke('JoinSpecificChat',{user,room});
  }
  public async sendMessage(message:string){
    return this.connection.invoke('SendMessage',message);
  }

  public async leaveChat(){
    return this.connection.stop();
  }
}
