import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css'
})
export class PatientListComponent implements OnInit {

   // use asnyc pipe instead of subscription
  patients$?: Observable<Patient[]>;

  // for sorting, filtering & pagination
  totalCount?: number;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

  // add constructor
  constructor(
    private patientService: PatientService,
    private route: ActivatedRoute
  ) { }


  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
   this.patientService.getPatientCount()
   .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.patients$ = this.patientService.getAllPatients(
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
    this.patients$ = this.patientService.getAllPatients(query);
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.patients$ = this.patientService.getAllPatients(undefined, sortBy, sortDirection);
  }

  // implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.patients$ = this.patientService.getAllPatients(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  
  // implement getNextPage
  getNextPage() {
    if (this.pageNumber + 1 > this.list.length) {
      return;
    }

    this.pageNumber += 1;
    this.patients$ = this.patientService.getAllPatients(
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
    this.patients$ = this.patientService.getAllPatients(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }



}
