import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Prescription } from '../models/prescription.models';
import { Observable } from 'rxjs';
import { PrescriptionService } from '../services/prescription.service';

@Component({
  selector: 'app-prescription-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './prescription-list.component.html',
  styleUrl: './prescription-list.component.css'
})
export class PrescriptionListComponent implements OnInit  {

 // use asnyc pipe instead of subscription
  prescriptions$?: Observable<Prescription[]>;

   // for sorting, filtering & pagination
   totalCount?: number;
   list: number[] = [];
   pageNumber = 1;
   pageSize = 5;


  // add constructor
  constructor(
    private prescriptionService: PrescriptionService,
    private route: ActivatedRoute
  ) { }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.prescriptionService.getPrescriptionCount()
    .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.prescriptions$ = this.prescriptionService.getAllPrescriptions(
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
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions(query);
  }

   //implement reset
   onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

   // implement sorting
   sort(sortBy: string, sortDirection: string) {
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions(undefined, sortBy, sortDirection);
  }

   // implement getPage
   getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.prescriptions$ = this.prescriptionService.getAllPrescriptions(
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
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions(
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
      this.prescriptions$ = this.prescriptionService.getAllPrescriptions(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize
      );
    }

}
