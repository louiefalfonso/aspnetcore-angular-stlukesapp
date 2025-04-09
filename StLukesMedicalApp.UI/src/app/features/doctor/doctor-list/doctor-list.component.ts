import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Doctor } from '../models/doctor.models';
import { Observable } from 'rxjs';
import { DoctorService } from '../services/doctor.service';

@Component({
  selector: 'app-doctor-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './doctor-list.component.html',
  styleUrl: './doctor-list.component.css'
})

export class DoctorListComponent implements OnInit {

  // use asnyc pipe instead of subscription
  doctors$?: Observable<Doctor[]>;

  // for sorting, filtering & pagination
  totalCount?: number;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

  // add constructor
  constructor(
   private doctorService: DoctorService,
  ) { }

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.doctorService.getDoctorCount()
    .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.doctors$ = this.doctorService.getAllDoctors(
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
    this.doctors$ = this.doctorService.getAllDoctors(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.doctors$ = this.doctorService.getAllDoctors(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.doctors$ = this.doctorService.getAllDoctors(undefined, sortBy, sortDirection);
  }

  // implement getPage
  getPage(pageNumber: number) {
      this.pageNumber = pageNumber;
  
      this.doctors$ = this.doctorService.getAllDoctors(
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
      this.doctors$ = this.doctorService.getAllDoctors(
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
      this.doctors$ = this.doctorService.getAllDoctors(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize
      );
  }

}
