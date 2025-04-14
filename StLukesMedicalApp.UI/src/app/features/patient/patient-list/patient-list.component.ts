import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { CommonModule } from '@angular/common';
import { ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-patient-list',
  imports: [RouterModule, CommonModule, BaseChartDirective],
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

   // Chart data for gender distribution
   genderChartLabels: string[] = ['Male', 'Female'];
   genderChartData: ChartData<'pie'> = {
     labels: this.genderChartLabels,
     datasets: [
       {
         data: [0, 0], // Placeholder data
         backgroundColor: ['#36A2EB', '#FF6384']
       }
     ]
   };
   genderChartOptions: ChartOptions<'pie'> = {
     responsive: true,
     plugins: {
       legend: {
         position: 'top'
       }
     }
   };

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

    this.patientService.getAllPatients().subscribe((patients) => {
      const maleCount = patients.filter((p: Patient) => p.sex === 'Male').length;
      const femaleCount = patients.filter((p: Patient) => p.sex === 'Female').length;
      this.genderChartData.datasets[0].data = [maleCount, femaleCount];
    });
  }

  // implement search
  onSearch(query: string) {
    this.patients$ = this.patientService.getAllPatients(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.patients$ = this.patientService.getAllPatients(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
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
