import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Billing } from '../models/billing.models';
import { BillingService } from '../services/billing.service';

@Component({
  selector: 'app-billing-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './billing-list.component.html',
  styleUrl: './billing-list.component.css'
})
export class BillingListComponent implements OnInit {

  // use asnyc pipe instead of subscription
  billings$?: Observable<Billing[]>;

   // for sorting, filtering & pagination
   totalCount?: number;
   list: number[] = [];
   pageNumber = 1;
   pageSize = 5;

   // add constructor
   constructor(
    private billingService: BillingService,
    private route: ActivatedRoute
  ) { }

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.billingService.getBillingCount()
    .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.billings$ = this.billingService.getAllBillings(
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
    this.billings$ = this.billingService.getAllBillings(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.billings$ = this.billingService.getAllBillings(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.billings$ = this.billingService.getAllBillings(undefined, sortBy, sortDirection);
  }

   // implement getPage
   getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.billings$ = this.billingService.getAllBillings(
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
      this.billings$ = this.billingService.getAllBillings(
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
    this.billings$ = this.billingService.getAllBillings(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }


}
