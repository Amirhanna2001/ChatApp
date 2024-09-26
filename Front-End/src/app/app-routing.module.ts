import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JoinChatComponent } from './join-chat/join-chat.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { ChatComponent } from './chat/chat.component';

const routes: Routes = [
  {path:'',redirectTo:'join-chat',pathMatch:'full'},
  {path:'join-chat',component:JoinChatComponent},
  {path:'welcome',component:WelcomeComponent},
  {path:'chat',component:ChatComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
