import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Appointment } from '../models/appointment.models';
import { Observable } from 'rxjs';
import { AppointmentService } from '../services/appointment.service';

@Component({
  selector: 'app-appointment-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './appointment-list.component.html',
  styleUrl: './appointment-list.component.css'
})
export class AppointmentListComponent implements OnInit{

// use asnyc pipe instead of subscription
appointments$?: Observable<Appointment[]>;

// for sorting, filtering & pagination
totalCount?: number;
list: number[] = [];
pageNumber = 1;
pageSize = 10;

// add constructor
constructor(
  private appointmentService: AppointmentService,
  private route: ActivatedRoute
) { }

// implement ngOnInit lifecycle hook
ngOnInit(): void {
  this.appointmentService.getAppointmentCount()
   .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.appointments$ = this.appointmentService.getAllAppointments(
           undefined,
           undefined,
           undefined,
           this.pageNumber,
           this.pageSize
         );
      }
    })
}

  // implement search
  onSearch(query: string) {
    this.appointments$ = this.appointmentService.getAllAppointments(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
}

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.appointments$ = this.appointmentService.getAllAppointments(undefined, sortBy, sortDirection);
  }

  // implement getPage
  getPage(pageNumber: number) {
      this.pageNumber = pageNumber;
  
      this.appointments$ = this.appointmentService.getAllAppointments(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize
      );
  }

  // implemennt getPrevPage
  getPrevPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }

    this.pageNumber -= 1;
    this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

}
