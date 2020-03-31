import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { TooltipModule, ModalModule, BsDropdownModule} from 'ngx-bootstrap';
import { BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { ContatosComponent } from './contatos/contatos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TituloComponent } from './_shared/titulo/titulo.component';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

import { EventoService } from './_services/evento.service';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      ContatosComponent,
      PalestrantesComponent,
      DashboardComponent,
      TituloComponent
   ],
   imports: [
      CommonModule,
      BrowserModule,
      AppRoutingModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      BsDatepickerModule.forRoot(),
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      ToastrModule.forRoot(),
      ReactiveFormsModule
   ],
   providers: [
      EventoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
