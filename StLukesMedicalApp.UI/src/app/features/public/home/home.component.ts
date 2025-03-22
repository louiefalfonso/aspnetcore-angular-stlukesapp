import { Component } from '@angular/core';
import { AppointmentComponent } from '../appointment/appointment.component';

@Component({
  selector: 'app-home',
  imports: [AppointmentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {

}
