import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Admission } from '../models/adminssion.models';
import { Observable } from 'rxjs';
import { AdmissionService } from '../services/admission.service';

@Component({
  selector: 'app-admission-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './admission-list.component.html',
  styleUrl: './admission-list.component.css'
})
export class AdmissionListComponent implements OnInit  {

 // use asnyc pipe instead of subscription
  admissionss$?: Observable<Admission[]>;

   // for sorting, filtering & pagination
   totalCount?: number;
   list: number[] = [];
   pageNumber = 1;
   pageSize = 10;

  // add constructor
  constructor(
    private admissionService: AdmissionService,
    private route: ActivatedRoute
  ) { }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {
   this.admissionService.getAdmissionCount()
   .subscribe({
     next: (value) => {
       this.totalCount = value;
        this.list = new Array(Math.ceil(value / this.pageSize))

        this.admissionss$ = this.admissionService.getAllAdmissions(
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
    this.admissionss$ = this.admissionService.getAllAdmissions(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
      queryText.value = ''; 
      this.pageNumber = 1; 
      this.admissionss$ = this.admissionService.getAllAdmissions(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize
      ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.admissionss$ = this.admissionService.getAllAdmissions(undefined, sortBy, sortDirection);
  }

   // implement getPage
   getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.admissionss$ = this.admissionService.getAllAdmissions(
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
    this.admissionss$ = this.admissionService.getAllAdmissions(
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
    this.admissionss$ = this.admissionService.getAllAdmissions(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }


  

}
