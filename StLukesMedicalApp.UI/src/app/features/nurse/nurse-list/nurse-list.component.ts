import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { Nurse } from '../models/nurse.models';
import { Observable } from 'rxjs';
import { NurseService } from '../services/nurse.service';

@Component({
  selector: 'app-nurse-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './nurse-list.component.html',
  styleUrl: './nurse-list.component.css'
})
export class NurseListComponent implements OnInit {

 // use asnyc pipe instead of subscription
  nurses$?: Observable<Nurse[]>;

  // for sorting, filtering & pagination
  totalCount?: number;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 5;

  // add constructor
  constructor(
    private nurseService: NurseService,
    private route: ActivatedRoute
  ) { }


  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.nurseService.getNurseCount()
    .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.nurses$ = this.nurseService.getAllNurses(
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
      this.nurses$ = this.nurseService.getAllNurses(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.nurses$ = this.nurseService.getAllNurses(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

 // implement sorting
 sort(sortBy: string, sortDirection: string) {
  this.nurses$ = this.nurseService.getAllNurses(undefined, sortBy, sortDirection);
}

  // implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.nurses$ = this.nurseService.getAllNurses(
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
    this.nurses$ = this.nurseService.getAllNurses(
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
  this.nurses$ = this.nurseService.getAllNurses(
    undefined,
    undefined,
    undefined,
    this.pageNumber,
    this.pageSize
  );
}
  

}
