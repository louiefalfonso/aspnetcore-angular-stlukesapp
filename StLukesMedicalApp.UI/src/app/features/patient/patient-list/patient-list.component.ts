import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';



export interface PatientElement {
  Id: number
  Status: string;
  Date: string;
  Doctor: string
  Patient: string
}

const ELEMENT_DATA: PatientElement[] = [
  {Id: 1, Status: 'Ongoing', Date: '03/22/2025', Doctor: 'Dr. Brian Lee', Patient: 'Jeremy Jones'},
  {Id: 2, Status: 'Completed', Date: '03/23/2025', Doctor: 'Dr. Daniel Ford', Patient: 'Michael Smith'},
  {Id: 3, Status: 'Cancelled', Date: '03/24/2025', Doctor: 'Dr. Lisa Green', Patient: 'John Doe'},
]
@Component({
  selector: 'app-patient-list',
  imports: [RouterModule,MatSidenavModule, MatTableModule, MatButtonModule],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css'
})
export class PatientListComponent {
  displayedColumns: string[] = ['Id', 'Status', 'Date', 'Doctor', 'Patient'];
  dataSource = ELEMENT_DATA;
}
