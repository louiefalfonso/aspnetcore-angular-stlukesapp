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
  pageSize = 10;

  // add constructor
  constructor(
    private nurseService: NurseService,
    private route: ActivatedRoute
  ) { }









  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
