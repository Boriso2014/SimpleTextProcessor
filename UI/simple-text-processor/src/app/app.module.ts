import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ListComponent } from './files/list/list.component';
import { HomeComponent } from './home/home.component';
import { AddComponent } from './files/add/add.component';
import { UpdateComponent } from './files/update/update.component';

@NgModule({
  declarations: [
    AppComponent,
    ListComponent,
    HomeComponent,
    AddComponent,
    UpdateComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
