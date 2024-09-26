import { Component, inject, OnInit } from '@angular/core';
import {FormGroup ,FormBuilder, Validators} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-join-chat',
  templateUrl: './join-chat.component.html',
  styleUrl: './join-chat.component.scss'
})
export class JoinChatComponent implements OnInit {
  
  joinRoomForm! :FormGroup;
  formBuilder = inject(FormBuilder);
  router = inject(Router);

  ngOnInit(): void {
    this.joinRoomForm = this.formBuilder.group({
      user:['',Validators.required],
      room:['',Validators.required]
    })
  }

  joinRoom(){
    console.log(this.joinRoomForm.value);
    this.router.navigate(['chat']);
  }
}
