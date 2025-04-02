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
   pageSize = 10;


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

}
