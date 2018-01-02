import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { CounterService } from './counter.service';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    CounterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [CounterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
