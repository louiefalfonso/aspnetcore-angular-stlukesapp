import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-doctor-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './doctor-detail.component.html',
  styleUrl: './doctor-detail.component.css'
})
export class DoctorDetailComponent implements OnInit{

  DoctorsId!:string;
  PatientsId!:string; 
  BillingsId!:string;

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
